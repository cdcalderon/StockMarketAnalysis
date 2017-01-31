using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMA.StockManagerService.Helpers
{
    public class HistoricalQuoteParams
    {
        public string Symbol { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
