using System;
using System.Collections.Generic;
using Bank.Exceptions;
using Bank.Model;
using Bank.Repository;
using Bank.Service;
using Bank.Tests.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bank.Tests
{
    [TestClass]
    public class WithdrawTests
    {
        /// <summary>
        /// This simulates a normal withdraw and asserts that everything functions as expected.
        /// </summary>
        [TestMethod]
        public void Withdraw_Normal_Success()
        {
            // setup entities
            var account = ObjectBuilder.BuildAccount(null);
            account.Balance = 60;
            var date = DateTimeOffset.UtcNow.Date;
            var transactions = new List<Transaction>()
            {
                ObjectBuilder.BuildTransaction(account.Id, 10, date.AddHours(5))
            };

            // setup repo
            var mockRepo = new Mock<IAccountRepository>(MockBehavior.Strict);
            mockRepo.Setup(r => r.GetTransactionsForAccountOnDate(account.Id, date)).Returns(transactions);
            mockRepo.Setup(r => r.AddTransaction(It.IsAny<Transaction>())).Verifiable();

            // setup callback to catch the updated account
            Account caughtAccount = null;
            mockRepo.Setup(r => r.UpdateAccount(It.IsAny<Account>())).Callback<Account>(a =>
            {
                caughtAccount = a;
            }).Verifiable();

            Transaction returnedTransaction;
            var accountService = new AccountService(mockRepo.Object);

            // tell fluent assertions to check for event
            using (var subscriptionService = accountService.SubscriptionService.Monitor())
            {
                // perform the operation
                returnedTransaction = accountService.Withdraw(account, 50);

                // check the event was fired
                subscriptionService.Should().Raise("AccountChangedEvent", "because this event should be thrown when withdrawing from an account");
            }

            // assert transaction was created successfully
            mockRepo.Verify(r => r.AddTransaction(It.IsAny<Transaction>()), Times.Once, "Repository add transaction method should've run once");
            returnedTransaction.Should().NotBeNull("because repository add transaction method has been invoked");
            returnedTransaction.AccountId.Should().Be(account.Id, "because this is the id of the account the withdrawal was performed on");
            returnedTransaction.Amount.Should().Be(-50, "because this is the amount that was withdrawn from the account");
            returnedTransaction.Time.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000, "because the transaction should have the current time");

            // assert account was updated correctly
            mockRepo.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once, "Repository update account method should've run once");
            caughtAccount.Should().NotBeNull("because repository update account method has been invoked");
            caughtAccount.Id.Should().Be(account.Id, "because this is the id of the account we updated");
            caughtAccount.Balance.Should().Be(10, "because the account had 60, then had 50 withdrawn, leaving 10");
        }

        /// <summary>
        /// This simulates an attempt to withdraw with insufficient funds.
        /// </summary>
        [TestMethod]
        public void Withdraw_InsufficientFunds_Exception()
        {
            // setup entities
            var account = ObjectBuilder.BuildAccount(null);
            account.Balance = 50;

            // setup repo
            var mockRepo = new Mock<IAccountRepository>(MockBehavior.Strict);
            //mockRepo.Setup(r => r.GetByAccountNumber(account.AccountNumber)).Returns(account);
            mockRepo.Setup(r => r.GetTransactionsForAccountOnDate(It.IsAny<int>(), It.IsAny<DateTimeOffset>())).Returns(new List<Transaction>());
            mockRepo.Setup(r => r.AddTransaction(It.IsAny<Transaction>())).Verifiable();
            mockRepo.Setup(r => r.UpdateAccount(It.IsAny<Account>())).Verifiable();

            var accountService = new AccountService(mockRepo.Object);

            // attempt to withdraw amount, assert that exception is thrown
            accountService.Invoking(s =>
            {
                // tell fluent assertions to watch events
                using (var subscriptionService = s.SubscriptionService.Monitor())
                {
                    s.Withdraw(account, 60);
                    // assert that no event was raised (if for some reason we get this far)
                    subscriptionService.Should().NotRaise("AccountChangedEvent", "because this illegal action should not have caused any account change");
                }
            }).Should().ThrowExactly<InsufficientFundsException>();

            // check no changes made it to the repository
            mockRepo.Verify(r => r.AddTransaction(It.IsAny<Transaction>()), Times.Never, "The repository method add transaction shouldn't have been invoked as the illegal action shouldn't have been a resultant transaction");
            mockRepo.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never, "The repository method update account shouldn't have been invoked as the illegal action shouldn't have caused any account change");
        }

        /// <summary>
        /// This simulates an attempt to withdraw cash when the daily limit has been reached.
        /// </summary>
        [TestMethod]
        public void Withdraw_ExceedLimit_Exception()
        {
            // setup entities
            var account = ObjectBuilder.BuildAccount(null);
            account.Balance = 1000;
            var date = DateTimeOffset.UtcNow.Date;
            var transactions = new List<Transaction>()
            {
                ObjectBuilder.BuildTransaction(account.Id, -50, date.AddHours(5)),
                ObjectBuilder.BuildTransaction(account.Id, -100, date.AddHours(6)),
                ObjectBuilder.BuildTransaction(account.Id, -25, date.AddHours(8)),
                ObjectBuilder.BuildTransaction(account.Id, -25, date.AddHours(8)),
                ObjectBuilder.BuildTransaction(account.Id, -50, date.AddHours(9))
            };

            // setup repo
            var mockRepo = new Mock<IAccountRepository>(MockBehavior.Strict);
            mockRepo.Setup(r => r.GetTransactionsForAccountOnDate(account.Id, It.Is<DateTimeOffset>(x => x.Date == date))).Returns(transactions);
            mockRepo.Setup(r => r.UpdateAccount(It.IsAny<Account>())).Verifiable();
            mockRepo.Setup(r => r.AddTransaction(It.IsAny<Transaction>())).Verifiable();

            var accountService = new AccountService(mockRepo.Object);

            // attempt to withdraw amount, assert that exception is thrown
            accountService.Invoking(s =>
            {
                // tell fluent assertions to watch events
                using (var subscriptionService = s.SubscriptionService.Monitor())
                {
                    s.Withdraw(account, 10);
                    // assert that no event was raised (if for some reason we get this far)
                    subscriptionService.Should().NotRaise("AccountChangedEvent", "because the illegal action should not cause an update");
                }
            }).Should().ThrowExactly<WithdrawLimitException>("because the account has hit it's withdrawal limit for the day");

            // check no changes made it to the repository
            mockRepo.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never, "The repository method update account shouldn't have run as the action was illegal");
            mockRepo.Verify(r => r.AddTransaction(It.IsAny<Transaction>()), Times.Never, "The repository method add transaction shouldn't have been invoked as the action was illegal");
        }

        /// <summary>
        /// Tests that an exception is thrown when attempting to withdraw an amount which is not a multiple of 10 (it £12).
        /// </summary>
        [TestMethod]
        public void Withdraw_NotMultipleOfTen_Exception()
        {
            // setup entities
            var account = ObjectBuilder.BuildAccount(null);
            account.Balance = 1000;

            // setup repo
            var mockRepo = new Mock<IAccountRepository>(MockBehavior.Strict);
            mockRepo.Setup(r => r.UpdateAccount(It.IsAny<Account>())).Verifiable();
            mockRepo.Setup(r => r.AddTransaction(It.IsAny<Transaction>())).Verifiable();

            var accountService = new AccountService(mockRepo.Object);

            accountService.Invoking(s =>
            {
                using (var subscriptionService = s.SubscriptionService.Monitor())
                {
                    s.Withdraw(account, 12);
                    subscriptionService.Should().NotRaise("AccountChangedEvent", "because the illegal action should not cause an update");
                }
            }).Should().ThrowExactly<AmountException>("because we attempted to withdraw 12 from an account which is not a multiple of 10");

            // check no changes were made to the repository
            mockRepo.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never, "The repository method update account shouldn't have been invoked as the action was illegal");
            mockRepo.Verify(r => r.AddTransaction(It.IsAny<Transaction>()), Times.Never, "The repository method add transaction shouldn't have been invoked as the action was illegal");
        }
    }
}
