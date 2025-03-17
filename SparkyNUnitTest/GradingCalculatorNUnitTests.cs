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
	public class GradingCalculatorNUnitTests
	{
		private GradingCalculator gradingCalculator;

		[SetUp]
		public void Setup()
		{
			gradingCalculator = new GradingCalculator();
		}

		[Test]
		public void GetGrade_InputScore95AndAttendancePercentage90_ReturnsA()
		{
			gradingCalculator.Score = 95;
			gradingCalculator.AttendancePercentage = 90;
			var result = gradingCalculator.GetGrade();

			ClassicAssert.That(result, Is.EqualTo("A"));
		}

		[Test]
		public void GetGrade_InputScore85AndAttendancePercentage90_ReturnsA()
		{
			gradingCalculator.Score = 85;
			gradingCalculator.AttendancePercentage = 90;
			var result = gradingCalculator.GetGrade();

			ClassicAssert.That(result, Is.EqualTo("B"));
		}

		[Test]
		public void GetGrade_InputScore65AndAttendancePercentage90_ReturnsA()
		{
			gradingCalculator.Score = 65;
			gradingCalculator.AttendancePercentage = 90;
			var result = gradingCalculator.GetGrade();

			ClassicAssert.That(result, Is.EqualTo("C"));
		}

		[Test]
		public void GetGrade_InputScore95AndAttendancePercentage65_ReturnsA()
		{
			gradingCalculator.Score = 95;
			gradingCalculator.AttendancePercentage = 65;
			var result = gradingCalculator.GetGrade();

			ClassicAssert.That(result, Is.EqualTo("B"));
		}

		[Test]
		[TestCase(95, 55)]
		[TestCase(65, 55)]
		[TestCase(50, 90)]
		public void GetGrade_InputScoreAndAttendancePercentage_ReturnsF(int score, int attendancePercentage)
		{
			gradingCalculator.Score = score;
			gradingCalculator.AttendancePercentage = attendancePercentage;
			var result = gradingCalculator.GetGrade();

			ClassicAssert.That(result, Is.EqualTo("F"));
		}


		// Second Way
		[Test]
		[TestCase(95, 90, ExpectedResult = "A")]
		[TestCase(85, 90, ExpectedResult = "B")]
		[TestCase(65, 90, ExpectedResult = "C")]
		[TestCase(95, 65, ExpectedResult = "B")]

		[TestCase(95, 55, ExpectedResult = "F")]
		[TestCase(65, 55, ExpectedResult = "F")]
		[TestCase(50, 90, ExpectedResult = "F")]
		public string GetGrade_InputScoreAndAttendancePercentage_ReturnsGrade(int score, int attendancePercentage)
		{
			gradingCalculator.Score = score;
			gradingCalculator.AttendancePercentage = attendancePercentage;
			return gradingCalculator.GetGrade();
		}




	}
}
