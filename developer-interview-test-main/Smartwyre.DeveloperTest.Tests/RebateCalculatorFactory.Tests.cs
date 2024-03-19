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
    public class RebateCalculatorFactoryTests
    {
        [Theory]
        [InlineData(IncentiveType.AmountPerUom, typeof(AmountPerUomRebateCalculator))]
        [InlineData(IncentiveType.FixedRateRebate, typeof(FixedRateRebateCalculator))]
        [InlineData(IncentiveType.FixedCashAmount, typeof(FixedCashRebateCalculator))]
        public void TestCalculatorCreation(IncentiveType incentiveType, Type type)
        {
            // arrange
            var factory = new RebateCalculatorFactory();
            var obj = Activator.CreateInstance(type);

            // act
            var calculator = factory.GetRebateCalculator(incentiveType);

            // assert
            Assert.Equal(obj.GetType(), calculator.GetType());
        }
    }
}
