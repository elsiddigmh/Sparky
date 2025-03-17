using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
	public class BankAccountXUnitTests
	{
		private BankAccount account;

		public BankAccountXUnitTests()
		{
		}


		[Fact]
		public void BankDeposit_Add100_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			BankAccount bankAccount = new BankAccount(logMock.Object);

			var result = bankAccount.Deposit(100);
			Assert.True(result);
			Assert.Equal(100, bankAccount.GetBalance());
		}


		[Theory]
		[InlineData(200, 100)]
		[InlineData(200, 150)]
		[InlineData(200, 200)]
		[InlineData(400, 200)]
		public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw)
		{
			var logMock = new Mock<ILogBook>();
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
			logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);

			BankAccount bankAccount = new BankAccount(logMock.Object);
			bankAccount.Deposit(balance);
			var result = bankAccount.Withrdraw(withdraw);
			Assert.True(result);

		}

		[Fact]
		public void BankWithdraw_Withdraw300With200Balance_ReturnsFalse()
		{
			var logMock = new Mock<ILogBook>();
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
			logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x => x >= 0))).Returns(true);
			//logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<int>(x=> x <= 0))).Returns(false);
			logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange<int>(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);


			BankAccount bankAccount = new BankAccount(logMock.Object);
			bankAccount.Deposit(200);
			var result = bankAccount.Withrdraw(300);
			Assert.False(result);

		}

		[Fact]
		public void BankLogDummy_LogMockString_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			var desiredOutput = "hello";

			// Return null if test any value other than "Hi"
			//logMock.Setup(u => u.MessageWithReturnStr("Hi")).Returns((string str) => str.ToLower());

			logMock.Setup(u => u.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());

			Assert.Equal(desiredOutput, logMock.Object.MessageWithReturnStr("HelLo"));

		}


		[Fact]
		public void BankLogDummy_LogMockStringOutputStr_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			string desiredOutput = "hello";

			logMock.Setup(u => u.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
			string result = "";
			Assert.True(logMock.Object.LogWithOutputResult("Ben", out result));
			Assert.Equal(desiredOutput, result);

		}


		[Fact]
		public void BankLogDummy_LogRefChecker_ReturnTrue()
		{
			var logMock = new Mock<ILogBook>();
			Customer customer = new();
			Customer customerNotUsed = new();

			logMock.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

			Assert.True(logMock.Object.LogWithRefObject(ref customer));
			Assert.False(logMock.Object.LogWithRefObject(ref customerNotUsed));

		}


		[Fact]
		public void BankLogDummy_SetAndGetLogTypeAndServerityMock_MockTest()
		{
			var logMock = new Mock<ILogBook>();
			logMock.SetupAllProperties();
			//logMock.Setup(u => u.LogServerity).Returns(10);
			logMock.Setup(u => u.LogType).Returns("warning");

			logMock.Object.LogServerity = 100;
			Assert.Equal(100, logMock.Object.LogServerity);
			Assert.Equal("warning", logMock.Object.LogType);


			// Callbacks
			string logTemp = "Hello, ";
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true)
								.Callback((string str) => logTemp += str);
			logMock.Object.LogToDb("Ben");
			Assert.Equal("Hello, Ben", logTemp);

			// Callbacks
			int counter = 5;
			logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Callback(() => counter++)
								.Returns(true).Callback(() => counter++);
			logMock.Object.LogToDb("Ben");
			logMock.Object.LogToDb("Ben");
			Assert.Equal(9, counter);
		}



		[Fact]
		public void BankLogDummy_VerfiyExample()
		{
			var logMock = new Mock<ILogBook>();
			BankAccount bankAccount = new(logMock.Object);
			bankAccount.Deposit(100);

			Assert.Equal(100, bankAccount.GetBalance());

			//Verification
			logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
			logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
			logMock.VerifySet(u => u.LogServerity = 101, Times.Once);
			logMock.VerifyGet(u => u.LogServerity, Times.Once);


		}



	}
}
