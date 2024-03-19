using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.RebateCalculators.Implementations;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
	public class RebateCalcImplementationsTests
	{
		[Theory]
		[InlineData(0, 0, 10, 100, IncentiveType.AmountPerUom, SupportedIncentiveType.AmountPerUom, false, 0)]
		[InlineData(0.1, 10, 10, 100, IncentiveType.AmountPerUom, SupportedIncentiveType.AmountPerUom, true, 100)]
		[InlineData(0.1, 10, 10, 100, IncentiveType.AmountPerUom, SupportedIncentiveType.FixedRateRebate, false, 0)]
		[InlineData(0.1, 10, 10, 100, IncentiveType.FixedCashAmount, SupportedIncentiveType.FixedCashAmount, true, 10)]
		[InlineData(0.2, 10, 0, 100, IncentiveType.FixedCashAmount, SupportedIncentiveType.FixedCashAmount, false, 0)]
		[InlineData(0.1, 10, 10, 100, IncentiveType.FixedCashAmount, SupportedIncentiveType.AmountPerUom | SupportedIncentiveType.FixedCashAmount, true, 10)]
		[InlineData(0.2, 10, 10, 100, IncentiveType.FixedRateRebate, SupportedIncentiveType.FixedRateRebate, true, 200)]
		[InlineData(0.2, 10, 10, 100, IncentiveType.FixedRateRebate, SupportedIncentiveType.FixedCashAmount, false, 0)]
		[InlineData(0.15, 20, 10, 100, IncentiveType.FixedRateRebate, SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedRateRebate, true, 300)]
		public void TestRebateImplementations(decimal percentage, decimal volume, decimal amount, decimal price,
			IncentiveType incentiveType, SupportedIncentiveType supportedIncentiveType, bool expectedSuccess, decimal expectedAmount)
		{
			// arrange
			var rebateCalculator = new RebateCalculatorFactory().GetRebateCalculator(incentiveType);
			var mockedRebate = new Rebate() { Amount = amount, Percentage = percentage, Incentive = incentiveType };
			var mockedProduct = new Product() { Price = price, SupportedIncentives = supportedIncentiveType };

			// act 
			var actual = rebateCalculator.Calculate(mockedRebate, mockedProduct, volume);

			// assert
			Assert.Equal(expectedSuccess, actual.Success);
			Assert.Equal(expectedAmount, actual.Amount);
		}
	}
}
