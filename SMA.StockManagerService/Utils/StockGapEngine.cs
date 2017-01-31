using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMA.Data;

namespace SMA.StockManagerService.Utils
{
    public class StockGapEngine
    {
        public void FindGap(List<HQuote> quotes)
        {
            var quotesWDiffs = quotes.Select(
                (myObject, index) =>
                    new
                    {
                        ID = myObject.Symbol,
                        Date = myObject.Date,
                        Value = myObject.Close,
                        DiffToPrev = (index > 0 ? myObject.Close - quotes[index - 1].Close : 0)
                    }


            );
            var diffCriteria = 3.5M;
            var superGaps = quotesWDiffs
                .Where(q => q.DiffToPrev > diffCriteria || (q.DiffToPrev >= (q.Value * .10M)))
                .OrderBy(x => x.ID).ThenBy(x => x.Date)
                .ToList();


        }
    }
}
