using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner
{
    public class Options
    {
        [Option('p', "productId", Required = true, HelpText = "Provide product identifier")]
        public string ProductId { get; set; }

        [Option('v', "volume", Required = true, HelpText = "Provide volume")]
        public decimal Volume { get; set; }
    }
}
