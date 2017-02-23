using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMA.Core.Common;
using SMA.Core.DTOs;
using SMA.Data;

namespace SMA.Web.Controllers
{
    public class GapController : ApiController
    {
        [HttpGet]
        [Route("api/gap/{symbol}")]
        public IHttpActionResult Get(string symbol)
        {
            IEnumerable<StockGap> gaps;
            using (StockMarketAnalysis context = new StockMarketAnalysis())
            {
                gaps = StockGapEngine.FindGap(context.HQuotes.Where(q => q.Symbol == symbol).ToList()).Select(g => new StockGap
                {
                    Symbol = g.Symbol,
                    Date = g.Date,
                    LastPrice = g.LastPrice,
                    GapSize = g.GapSize
                });
            }

           return Ok(gaps.ToList());
        }
    }
}
