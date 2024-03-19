using Smartwyre.DeveloperTest.RebateCalculators.Abstract;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.RebateCalculators.Implementations
{
    public class FixedRateRebateCalculator : RebateCalculator
    {
        public override bool IsIncentiveSupported(Product product)
        {
            return product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate);
        }

        public override bool IsApplicable(Rebate rebate, Product product, decimal volume)
        {
            return !(rebate.Percentage == 0 || product.Price == 0 || volume == 0);
        }

        public override decimal CalculateAmount(Rebate rebate, Product product, decimal volume)
        {
            return product.Price * rebate.Percentage * volume;
        }
    }
}
