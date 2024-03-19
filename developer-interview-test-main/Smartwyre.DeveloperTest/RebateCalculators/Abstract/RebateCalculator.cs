using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.RebateCalculators.Abstract
{
    public abstract class RebateCalculator
    {
        public CalculateRebateResult Calculate(Rebate rebate, Product product, decimal volume)
        {
            var result = new CalculateRebateResult() { Success = false, Amount = 0m };

            if (!IsIncentiveSupported(product) || !IsApplicable(rebate, product, volume))
            {
                return result;
            }
            result.Success = true;
            result.Amount = CalculateAmount(rebate, product, volume);

            return result;
        }

        public abstract bool IsIncentiveSupported(Product product);
        public abstract bool IsApplicable(Rebate rebate, Product product, decimal volume);
        public abstract decimal CalculateAmount(Rebate rebate, Product product, decimal volume);
    }
}
