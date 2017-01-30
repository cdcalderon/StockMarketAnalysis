using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SMA.StockManagerService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.Service<StockManagerService>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(
                        () => new StockManagerService());

                    serviceInstance.WhenStarted(execute => execute.Start());

                    serviceInstance.WhenStopped(execute => execute.Stop());

                });

                serviceConfig.SetServiceName("StockQuotesManagerService");
                serviceConfig.SetDisplayName("Stock Quote Manager Service");
                serviceConfig.SetDescription("A Stock Quote Manager Service");

                serviceConfig.StartAutomatically();
            });
        }
    }
}
