using Smartwyre.DeveloperTest.RebateCalculators.Abstract;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.RebateCalculators.Implementations
{
	public class AmountPerUomRebateCalculator : RebateCalculator
	{
		public override bool IsIncentiveSupported(Product product)
		{
			return product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom);
		}

		public override bool IsApplicable(Rebate rebate, Product product, decimal volume)
		{
			return !(rebate.Amount == 0 || volume == 0);
		}

		public override decimal CalculateAmount(Rebate rebate, Product product, decimal volume)
		{
			return rebate.Amount * volume;
		}
	}
}
