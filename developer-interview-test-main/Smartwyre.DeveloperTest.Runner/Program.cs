using CommandLine;
using CommandLine.Text;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
	static void Main(string[] args)
	{
		var result = Parser.Default.ParseArguments<Options>(args);

		result.WithParsed(options =>
		{
			var rebateCalculatorFactory = new RebateCalculatorFactory();
			var rebateDataStore = new RebateDataStore(); // replace with real store or imposter/mock
			var productDataStore = new ProductDataStore(); // replace with real store or imposter/mock
			var request = new CalculateRebateRequest
			{
				Volume = options.Volume,
				ProductIdentifier = options.ProductId
			};
			var rebateService = new RebateService(rebateCalculatorFactory, rebateDataStore, productDataStore);
			try
			{
				var res = rebateService.Calculate(request);
				Console.WriteLine($"result success: {res.Success} and amount: {res.Amount}");
			}
			catch (Exception)
			{
				// todo: handle specific exceptions and write out messages accordingly
				// todo: add logging
				Console.WriteLine("Something went wrong");
			}
		});

		result.WithNotParsed(errors =>
		{
			var helpText = HelpText.AutoBuild(result);
		});
	}
}
