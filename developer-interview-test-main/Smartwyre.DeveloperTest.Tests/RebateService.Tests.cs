using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    private RebateCalculatorFactory _factory;
    private Mock<IRebateDataStore> _rebateDataStore;
    private Mock<IProductDataStore> _productDataStore;
    private CalculateRebateRequest _calculateRebateRequest;

    public RebateServiceTests()
    {
        // init some mocks
        _factory = new RebateCalculatorFactory();
        _rebateDataStore = new Mock<IRebateDataStore>();
        _rebateDataStore.Setup(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()));
        _productDataStore = new Mock<IProductDataStore>();
        _calculateRebateRequest = new CalculateRebateRequest()
        {
            ProductIdentifier = "ProductSampleId",
            RebateIdentifier = "RebateId",
            Volume = 100
        };
    }
    /// note: the rebate calc logic is covered in the RebateCalcImplementations.Tests
    [Fact]
    public void TestRebateService_NullProduct()
    {
        // arrange
        var mockedRebate = new Rebate() {Amount = 10, Percentage = 0.2m, Incentive = IncentiveType.FixedRateRebate}; 
        _rebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(() => mockedRebate);
        _productDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(() => null);

        var rebateService = new RebateService(_factory, _rebateDataStore.Object, _productDataStore.Object);

        // act
        var actual = rebateService.Calculate(_calculateRebateRequest);

        // assert
        Assert.False(actual.Success);
        Assert.Equal(0, actual.Amount);
        _rebateDataStore.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never);
    }

    [Fact]
    public void TestRebateService_NonEmptyProduct()
    {
        // arrange
        var mockedRebate = new Rebate() { Amount = 10, Percentage = 0.2m, Incentive = IncentiveType.FixedRateRebate };
        var mockedProduct = new Product() { Price = 100, SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

        _rebateDataStore.Setup(x => x.GetRebate(It.IsAny<string>())).Returns(() => mockedRebate);
        _productDataStore.Setup(x => x.GetProduct(It.IsAny<string>())).Returns(() => mockedProduct);

        var rebateService = new RebateService(_factory, _rebateDataStore.Object, _productDataStore.Object);

        // act
        var actual = rebateService.Calculate(_calculateRebateRequest);

        // assert
        Assert.True(actual.Success);
        Assert.Equal(2000, actual.Amount);
        _rebateDataStore.Verify(x => x.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Once);
    }
}
