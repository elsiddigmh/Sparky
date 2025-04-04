﻿using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
	[TestFixture]
	public class ProductNUnitTests
	{
		[Test]
		public void GetProductPrice_PlatinumCustomer_ReturnPriceWith20Discount()
		{
			var product = new Product() { Price = 50};
			var result = product.GetPrice(new Customer() { IsPlatinum = true });

			ClassicAssert.That(result, Is.EqualTo(40)); 

		}
	}
}
