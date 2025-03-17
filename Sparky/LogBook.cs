using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
	public interface ILogBook
	{
		public int LogServerity { get; set; }
		public string LogType { get; set; }

		 void Message(string message);
		 bool LogToDb(string message);
		 bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal);
		 string MessageWithReturnStr(string message);
		 bool LogWithOutputResult(string str, out string outputStr);
		 bool LogWithRefObject(ref Customer customer); 
	}
	public class LogBook : ILogBook
	{
		public int LogServerity { get; set; }
		public string LogType { get; set; }

		public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
		{
			if (balanceAfterWithdrawal >= 0) {
				Console.WriteLine("Success");
				return true;
			}
			else
			{
				Console.WriteLine("Failure");
				return false;
			}
		}


		public bool LogToDb(string message)
		{
			Console.WriteLine(message);
			return true;
		}

		public bool LogToDb(int balanceAfterWithdrawal)
		{
			throw new NotImplementedException();
		}

		public bool LogWithOutputResult(string str, out string outputStr)
		{
			outputStr = "Hello " + str;
			return true;
		}

		public bool LogWithRefObject(ref Customer customer)
		{
			return true;
		}

		public void Message(string message)
		{
			Console.WriteLine(message);
		}

		public string MessageWithReturnStr(string message)
		{
			Console.WriteLine($"{message}");
			return message.ToLower();
		}
	}


	//public class LogFaker : ILogBook
	//{
	//	public void Message(string message)
	//	{
	//	}
	//}
}
