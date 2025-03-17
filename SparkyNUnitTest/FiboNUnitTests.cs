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
	public class FiboNUnitTests
	{
		private Fibo fibo;
		[SetUp]
		public void Setup()
		{
			fibo = new Fibo();
		}

		[Test]
		public void GetFiboSeries_Input1_ReturnsFiboSeries()
		{

			fibo.Range = 1;
			List<int> expectedResult = new List<int> {0};

			var result = fibo.GetFiboSeries();

			ClassicAssert.That(result, Is.Not.Empty);
			ClassicAssert.That(result, Is.Ordered);
			ClassicAssert.That(result, Is.EquivalentTo(expectedResult));
		}

		[Test]
		public void GetFiboSeries_Input6_ReturnsFiboSeries()
		{
			fibo.Range = 6;
			List<int> expectedResult = new List<int> { 0, 1, 1, 2, 3, 5 };
			var result = fibo.GetFiboSeries();

			ClassicAssert.Multiple(() =>
			{
				ClassicAssert.That(result, Does.Contain(3));
				ClassicAssert.That(result.Count, Is.EqualTo(6));
				ClassicAssert.That(result, Has.No.Member(4));
				ClassicAssert.That(result, Is.EquivalentTo(expectedResult));
			});

		}

	}
}
