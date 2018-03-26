using Bank.Model;
using System;
namespace Bank.Service
{
    /// <summary>
    /// Represents a user logged in session.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// The guid.
        /// </summary>
        public Guid Guid { get; private set; }

        /// <summary>
        /// The account that this session is logged in to.
        /// </summary>
        public Account Account { get; private set; }

        /// <summary>
        /// The date/time the session began.
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="guid">the guid</param>
        /// <param name="account">the account</param>
        /// <param name="startTime">the start time</param>
        public Session(Guid guid, Account account, DateTime startTime)
        {
            Guid = guid;
            Account = account;
            StartTime = startTime;
        }
    }
}
