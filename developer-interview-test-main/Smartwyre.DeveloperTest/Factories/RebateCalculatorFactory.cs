using Smartwyre.DeveloperTest.RebateCalculators.Abstract;
using Smartwyre.DeveloperTest.RebateCalculators.Implementations;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Factories
{
    public class RebateCalculatorFactory
    {
        public RebateCalculator GetRebateCalculator(IncentiveType incentiveType)
        {
            switch (incentiveType)
            {
                case IncentiveType.FixedRateRebate:
                    return new FixedRateRebateCalculator();
                case IncentiveType.AmountPerUom:
                    return new AmountPerUomRebateCalculator();
                case IncentiveType.FixedCashAmount:
                    return new FixedCashRebateCalculator();
                default:
                    throw new NotSupportedException($"Rebate for incentive type {incentiveType} is not supported");
            }
        }
    }
}
