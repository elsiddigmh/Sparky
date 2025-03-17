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
	public class CustomerNUnitTests
	{
		private Customer customer;

		[SetUp]
		public void Setup()
		{
			customer = new Customer();
		}

		[Test]
		public void CombineName_InputFirstAndLastName_ReturnFullName()
		{
			// Arrange
			// Act 
			customer.GreetAndCombineNames("Elsiddig", "Mohamed");

			// Assert
			ClassicAssert.Multiple(() =>
			{
				ClassicAssert.AreEqual(customer.GreetMessage, "Hello, Elsiddig Mohamed");
				ClassicAssert.That(customer.GreetMessage, Is.EqualTo("Hello, Elsiddig Mohamed"));
				ClassicAssert.That(customer.GreetMessage, Does.Contain("elsiddig").IgnoreCase);
				ClassicAssert.That(customer.GreetMessage, Does.StartWith("Hello,"));
				ClassicAssert.That(customer.GreetMessage, Does.EndWith("Mohamed"));
				ClassicAssert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]"));
			});
			
		}

		[Test]
		public void GreetMessage_NotGreeted_ReturnsNull()
		{
			// Arragne

			// Act

			// Assert
			ClassicAssert.IsNull(customer.GreetMessage);


		}


		[Test]
		public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
		{
			var result = customer.Discount;
			ClassicAssert.That(result, Is.InRange(10, 25));
		}

		[Test]
		public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
		{
			customer.GreetAndCombineNames("Elsiddig", "");
			ClassicAssert.IsNotNull(customer.GreetMessage);
			ClassicAssert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));

		}

		[Test]
		public void GreetChecker_EmptyFirstName_ThrowsException()
		{
			var exceptionDetails = ClassicAssert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Mohamed"));

			// Test by using exception message
			ClassicAssert.AreEqual("Invalid First Name", exceptionDetails.Message);
			ClassicAssert.That(() => customer.GreetAndCombineNames("", "Mohammed"), 
				Throws.ArgumentException.With.Message.EqualTo("Invalid First Name"));

			// Test by using just exception type
			ClassicAssert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Mohamed"));
			ClassicAssert.That(() => customer.GreetAndCombineNames("", "Mohammed"),	Throws.ArgumentException);

		}


		[Test]
		public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
		{
			customer.OrderTotal = 10;
			var result = customer.GetCustomerDetails();
			ClassicAssert.That(result, Is.TypeOf<BasicCustomer>());

		}

		[Test]
		public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnsPaltinumCustomer()
		{
			customer.OrderTotal = 110;
			var result = customer.GetCustomerDetails();
			ClassicAssert.That(result, Is.TypeOf<PaltinumCustomer>());

		}


	}
}
