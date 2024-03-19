using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly RebateCalculatorFactory _rebateCalculatorFactory;
    private readonly IRebateDataStore _rebateDataStore;
    private readonly IProductDataStore _productDataStore;

    public RebateService(RebateCalculatorFactory rebateCalculatorFactory,
        IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
    {
        _rebateCalculatorFactory = rebateCalculatorFactory;
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        Product product = _productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null || product == null)
        {
            return new CalculateRebateResult() { Success = false, Amount = 0m };
        }

        var rebateCalculator = _rebateCalculatorFactory.GetRebateCalculator(rebate.Incentive);
        var result = rebateCalculator.Calculate(rebate, product, request.Volume);
       
        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, result.Amount);
        }

        return result;
    }
}
