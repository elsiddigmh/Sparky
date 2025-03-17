using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
	public class CustomerXUnitTests
	{
		private Customer customer;

		public CustomerXUnitTests()
		{
			customer = new Customer();
		}

		[Fact]
		public void CombineName_InputFirstAndLastName_ReturnFullName()
		{
			// Arrange
			// Act 
			customer.GreetAndCombineNames("Elsiddig", "Mohamed");

			// Assert
			Assert.Multiple(() =>
			{
				Assert.Equal("Hello, Elsiddig Mohamed", customer.GreetMessage);
				Assert.Equal("Hello, Elsiddig Mohamed", customer.GreetMessage);
				Assert.Contains("elsiddig".ToLower() ,customer.GreetMessage.ToLower());
				Assert.StartsWith("Hello,", customer.GreetMessage);
				Assert.EndsWith("Mohamed", customer.GreetMessage);
				Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]", customer.GreetMessage);
			});

		}

		[Fact]
		public void GreetMessage_NotGreeted_ReturnsNull()
		{
			// Arragne

			// Act

			// Assert
			Assert.Null(customer.GreetMessage);


		}


		[Fact]
		public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
		{
			var result = customer.Discount;
			Assert.InRange(result, 10, 25);
		}

		[Fact]
		public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
		{
			customer.GreetAndCombineNames("Elsiddig", "");
			Assert.NotNull(customer.GreetMessage);
			Assert.False(string.IsNullOrEmpty(customer.GreetMessage));

		}

		[Fact]
		public void GreetChecker_EmptyFirstName_ThrowsException()
		{
			var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Mohamed"));

			Assert.Equal("Invalid First Name", exceptionDetails.Message);

			Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Mohamed"));
		}


		[Fact]
		public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
		{
			customer.OrderTotal = 10;
			var result = customer.GetCustomerDetails();
			Assert.IsType<BasicCustomer>(result);

		}

		[Fact]
		public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnsPaltinumCustomer()
		{
			customer.OrderTotal = 110;
			var result = customer.GetCustomerDetails();
			Assert.IsType<PaltinumCustomer>(result);

		}


	}
}
