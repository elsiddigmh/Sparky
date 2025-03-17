using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
	[TestFixture]
	public class BankAccountNUnitTests
	{
		private BankAccount account;

		[SetUp]
		public void Setup()
		{
		}

		//[Test]
		//public void BankDepositLogFaker_Add100_ReturnTrue()
		//{
		//	BankAccount bankAccount = new BankAccount(new LogFaker());

		//	var result = bankAccount.Deposit(100);
		//	ClassicAssert.IsTrue(result);
		//	ClassicAssert.That(bankAccount.GetBalance(), Is.EqualTo(100));
		//}


		[Test]
		public void BankDeposit_Add100_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			BankAccount bankAccount = new BankAccount(logMock.Object);

			var result = bankAccount.Deposit(100);
			ClassicAssert.IsTrue(result);
			ClassicAssert.That(bankAccount.GetBalance(), Is.EqualTo(100));
		}


		[Test]
		[TestCase(200,100)]
		[TestCase(200,300)]
		[TestCase(200,200)]
		[TestCase(400, 200)]
		public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw)
		{
			var logMock = new Mock<ILogBook>();
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
			logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x=> x >= 0))).Returns(true);

			BankAccount bankAccount = new BankAccount(logMock.Object);
			bankAccount.Deposit(balance);
			var result = bankAccount.Withrdraw(withdraw);
			ClassicAssert.IsTrue(result);

		}

		[Test]
		public void BankWithdraw_Withdraw300With200Balance_ReturnsFalse()
		{
			var logMock = new Mock<ILogBook>();
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
			logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);
			//logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x=> x <= 0))).Returns(false);
			logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue,-1,Moq.Range.Inclusive))).Returns(false);


			BankAccount bankAccount = new BankAccount(logMock.Object);
			bankAccount.Deposit(200);
			var result = bankAccount.Withrdraw(300);
			ClassicAssert.IsFalse(result);

		}

		[Test]
		public void BankLogDummy_LogMockString_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			var desiredOutput = "hello";

			// Return null if test any value other than "Hi"
			//logMock.Setup(u => u.MessageWithReturnStr("Hi")).Returns((string str) => str.ToLower());

			logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());
			
			ClassicAssert.That(logMock.Object.MessageWithReturnStr("HelLo"), Is.EqualTo(desiredOutput));

		}


		[Test]
		public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			string desiredOutput = "hello";

			logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(),out desiredOutput)).Returns(true);
			string result = "";
			ClassicAssert.IsTrue(logMock.Object.LogWithOutputResult("Ben", out result));
			ClassicAssert.That(result, Is.EqualTo(desiredOutput));

		}


		[Test]
		public void BankLogDummy_LogRefChecker_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			Customer customer = new();
			Customer customerNotUsed = new();

			logMock.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

			ClassicAssert.IsTrue(logMock.Object.LogWithRefObject(ref customer));
			ClassicAssert.IsFalse(logMock.Object.LogWithRefObject(ref customerNotUsed));

		}


		[Test]
		public void BankLogDummy_SetAndGetLogTypeAndServerityMock_MockTest()
		{
			var logMock = new Mock<ILogBook>();
			logMock.SetupAllProperties();
			//logMock.Setup(u => u.LogServerity).Returns(10);
			logMock.Setup(u => u.LogType).Returns("warning");

			logMock.Object.LogServerity = 100;
			ClassicAssert.That(logMock.Object.LogServerity, Is.EqualTo(100));
			ClassicAssert.That(logMock.Object.LogType, Is.EqualTo("warning"));


			// Callbacks
			string logTemp = "Hello, ";
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true)
								.Callback((string str) => logTemp += str);
			logMock.Object.LogToDb("Ben");
			ClassicAssert.That(logTemp, Is.EqualTo("Hello, Ben"));

			// Callbacks
			int counter = 5;
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Callback(() => counter++)
								.Returns(true).Callback(() => counter++);
			logMock.Object.LogToDb("Ben");
			logMock.Object.LogToDb("Ben");
			ClassicAssert.That(counter, Is.EqualTo(9));
		}



		[Test]
		public void BankLogDummy_VerfiyExample()
		{
			var logMock = new Mock<ILogBook>();
			BankAccount bankAccount = new(logMock.Object);
			bankAccount.Deposit(100);

			ClassicAssert.That(bankAccount.GetBalance(), Is.EqualTo(100));
			
			//Verification
			logMock.Verify(u=> u.Message(It.IsAny<string>()), Times.Exactly(2));
			logMock.Verify(u=> u.Message("Test"), Times.AtLeastOnce);
			logMock.VerifySet(u=> u.LogServerity = 101, Times.Once);
			logMock.VerifyGet(u=> u.LogServerity, Times.Once);


		}



	}
}
