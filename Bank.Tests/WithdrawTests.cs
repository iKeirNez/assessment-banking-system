using System;
using System.Collections.Generic;
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
            var account = ObjectBuilder.BuildAccount(null, "0000");
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
                subscriptionService.Should().Raise("AccountChangedEvent", "because this event should be thrown when withdrawing from an account.");
            }

            // assert transaction was created successfully
            mockRepo.Verify(r => r.AddTransaction(It.IsAny<Transaction>()), Times.Once, "Repository add transaction method should've run once.");
            returnedTransaction.Should().NotBeNull("because repository add transaction method has been invoked.");
            returnedTransaction.AccountId.Should().Be(account.Id, "because this is the id of the account the withdrawal was performed on.");
            returnedTransaction.Amount.Should().Be(-50, "because this is the amount that was withdrawn from the account");
            returnedTransaction.Time.Should().BeCloseTo(DateTimeOffset.UtcNow, 1000, "because the transaction should have the current time.");

            // assert account was updated correctly
            mockRepo.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once, "Repository update account method should've run once.");
            caughtAccount.Should().NotBeNull("because repository update account method has been invoked.");
            caughtAccount.Id.Should().Be(account.Id, "because this is the id of the account we updated.");
            caughtAccount.Balance.Should().Be(10, "because the account had 60, then had 50 withdrawn, leaving 10.");
        }
    }
}
