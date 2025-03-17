using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
	public class BankAccount
	{
        private readonly ILogBook _logBook;
        public int Balance { get; set; }
        public BankAccount(ILogBook logBook)
        {
            Balance = 0;
            _logBook = logBook;
        }

		public int GetBalance()
		{
			return Balance;
		}

		public bool Deposit(int amount)
        {
            _logBook.Message("Deposit Invoked");
            _logBook.Message("Test");
            _logBook.LogServerity = 101;
            var temp = _logBook.LogServerity;
            Balance += amount;
            return true;
        }

		public bool Withrdraw(int amount)
		{
            if (amount <= Balance) { 
                _logBook.LogToDb("Withdrawal Amount: " + amount.ToString());
                Balance -= amount;
                return _logBook.LogBalanceAfterWithdrawal(Balance);
            }
            return _logBook.LogBalanceAfterWithdrawal(Balance - amount);
		}




	}
}
