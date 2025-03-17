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
	public class CalculatorNUnitTests
	{
		//private List<int> ex

		//[SetUp]
		//public void Setup()
		//{

		//}

		[Test]
		public void AddNumbers_InputTwoInt_GetCorrectAddiation()
		{
			// Arrange
			Calculator calc = new();

			// Act
			int result = calc.AddNumbers(10, 20);

			// Assert
			ClassicAssert.AreEqual(30, result);

		}

		[Test]
		public void IsOddChecker_InputEvenNumber_ReturnFalse()
		{
			// Arrange
			Calculator calc = new();

			// Act
			bool isOdd = calc.IsOddNumber(10);

			// Assert
			ClassicAssert.That(isOdd, Is.EqualTo(false));
			ClassicAssert.IsFalse(isOdd);
		}

		[Test]
		[TestCase(11)]
		[TestCase(13)]
		public void IsOddChecker_InputOddNumber_ReturnTrue(int a)
		{
			// Arrange
			Calculator calc = new();

			// Act
			bool isOdd = calc.IsOddNumber(a);

			// Assert
			ClassicAssert.That(isOdd, Is.EqualTo(true));
			ClassicAssert.IsTrue(isOdd);
		}

		[Test]
		[TestCase(10, ExpectedResult = false)]
		[TestCase(11, ExpectedResult = true)]
		public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a) { 
			
			Calculator calc = new();

			return calc.IsOddNumber(a);
		
		}

		[Test]
		[TestCase(5.4, 10.5)] //15.9
		[TestCase(5.43, 10.53)] //15.96
		[TestCase(5.49, 10.59)] //16.08
		public void AddNumbersDouble_InputTwoDouble_GetCorrectAddiation(double a, double b)
		{
			// Arrange
			Calculator calc = new();

			// Act
			double result = calc.AddNumbersDouble(a, b);

			// Assert
			ClassicAssert.AreEqual(15.9, result,.2);

		}


		[Test]
		public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
		{
			// Arrange
			Calculator calc = new();
			List<int> expectedOddRange = new() { 5, 7, 9 }; // 5->10

			// Act
			List<int> result = calc.GetOddRange(5, 10);

			// Assert
			ClassicAssert.That(result, Is.EquivalentTo(expectedOddRange));
			//ClassicAssert.AreEqual(expectedOddRange, result);
			//ClassicAssert.Contains(7, result);
			ClassicAssert.That(result, Does.Contain(7));
			ClassicAssert.That(result, Is.Not.Empty);
			ClassicAssert.That(result.Count, Is.EqualTo(3));
			ClassicAssert.That(result, Has.No.Member(6));
			ClassicAssert.That(result, Is.Ordered);
			ClassicAssert.That(result, Is.Unique);
		}



	}
}
