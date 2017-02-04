using System;
using System.Collections.Generic;
using System.Linq;
using SMA.Core.Models;

namespace SMA.Core.Common
{
    public class StockGapEngine
    {
        public void FindGap(List<HQuote> quotes)
        {
            var quotesWDiffs = Enumerable.Select(quotes, (myObject, index) =>
                    new
                    {
                        ID = myObject.Symbol,
                        Date = myObject.Date,
                        Value = myObject.Close,
                        DiffToPrev = (index > 0 ? Math.Abs((decimal) (myObject.Close.Value - quotes[index - 1].Close.Value)) : 0)
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
