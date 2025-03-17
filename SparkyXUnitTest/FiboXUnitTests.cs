using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
	public class FiboXUnitTests
	{
		private Fibo fibo;

		public FiboXUnitTests()
		{
			fibo = new Fibo();
		}

		[Fact]
		public void GetFiboSeries_Input1_ReturnsFiboSeries()
		{

			fibo.Range = 1;
			List<int> expectedResult = new List<int> { 0 };

			var result = fibo.GetFiboSeries();

			Assert.NotEmpty(result);
			Assert.Equal(expectedResult.OrderBy(u=>u), result);
			Assert.True(result.SequenceEqual(expectedResult));
		}

		[Fact]
		public void GetFiboSeries_Input6_ReturnsFiboSeries()
		{
			fibo.Range = 6;
			List<int> expectedResult = new List<int> { 0, 1, 1, 2, 3, 5 };
			var result = fibo.GetFiboSeries();

			Assert.Multiple(() =>
			{
				Assert.Contains(3, result);
				Assert.Equal(6, result.Count);
				Assert.DoesNotContain(4, result);
				Assert.Equal(expectedResult, result);
			});

		}

	}
}
