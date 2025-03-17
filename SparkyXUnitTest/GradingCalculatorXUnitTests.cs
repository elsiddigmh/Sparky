using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
	public class GradingCalculatorXUnitTests
	{
		private GradingCalculator gradingCalculator;
        public GradingCalculatorXUnitTests()
        {
			gradingCalculator = new GradingCalculator();
		}


		[Fact]
		public void GetGrade_InputScore95AndAttendancePercentage90_ReturnsA()
		{
			gradingCalculator.Score = 95;
			gradingCalculator.AttendancePercentage = 90;
			var result = gradingCalculator.GetGrade();

			Assert.Equal("A", result);
		}

		[Fact]
		public void GetGrade_InputScore85AndAttendancePercentage90_ReturnsA()
		{
			gradingCalculator.Score = 85;
			gradingCalculator.AttendancePercentage = 90;
			var result = gradingCalculator.GetGrade();

			Assert.Equal("B", result);
		}

		[Fact]
		public void GetGrade_InputScore65AndAttendancePercentage90_ReturnsA()
		{
			gradingCalculator.Score = 65;
			gradingCalculator.AttendancePercentage = 90;
			var result = gradingCalculator.GetGrade();

			Assert.Equal("C", result);
		}

		[Fact]
		public void GetGrade_InputScore95AndAttendancePercentage65_ReturnsA()
		{
			gradingCalculator.Score = 95;
			gradingCalculator.AttendancePercentage = 65;
			var result = gradingCalculator.GetGrade();

			Assert.Equal("B", result);
		}

		[Theory]
		[InlineData(95, 55)]
		[InlineData(65, 55)]
		[InlineData(50, 90)]
		public void GetGrade_InputScoreAndAttendancePercentage_ReturnsF(int score, int attendancePercentage)
		{
			gradingCalculator.Score = score;
			gradingCalculator.AttendancePercentage = attendancePercentage;
			var result = gradingCalculator.GetGrade();

			Assert.Equal("F", result);
		}


		// Second Way
		[Theory]
		[InlineData(95, 90, "A")]
		[InlineData(85, 90, "B")]
		[InlineData(65, 90, "C")]
		[InlineData(95, 65, "B")]
		[InlineData(95, 55, "F")]

		[InlineData(65, 55, "F")]
		[InlineData(50, 90, "F")]
		public void GetGrade_InputScoreAndAttendancePercentage_ReturnsGrade(int score, int attendancePercentage, string expectedResult)
		{
			gradingCalculator.Score = score;
			gradingCalculator.AttendancePercentage = attendancePercentage;
			var result = gradingCalculator.GetGrade();
			Assert.Equal(expectedResult, result);

		}




	}
}
