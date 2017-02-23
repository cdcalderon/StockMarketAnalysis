using System;
using System.Collections.Generic;
using System.Linq;
using SMA.Core.DTOs;
using SMA.Core.Models;

namespace SMA.Core.Common
{
    public class StockGapEngine
    {
        public static IEnumerable<StockGap> FindGap(List<HQuote> quotes)
        {
            var quotesWDiffs = Enumerable.Select(quotes, (myObject, index) =>
                new
                {
                    myObject.Symbol,
                    myObject.Date,
                    Close = myObject.Close,
                    DiffToPrev =
                    (index > 0 ? Math.Abs((decimal) (myObject.Close.Value - quotes[index - 1].Close.Value)) : 0)
                }
            );

            var diffCriteria = 3.5M;
            return quotesWDiffs
                .Where(q => q.DiffToPrev > diffCriteria || (q.DiffToPrev >= (q.Close * .10M)))
                .OrderBy(x => x.Symbol).ThenBy(x => x.Date).Select(g => new StockGap
                {
                    Symbol = g.Symbol,
                    Date = g.Date,
                    LastPrice = g.Close,
                    GapSize = g.DiffToPrev
                })
                .ToList();
        }
    }
}
