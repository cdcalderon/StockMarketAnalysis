using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMA.Core.DTOs;
using SMA.Data;

namespace SMA.Web.Controllers
{
    public class QuoteController : ApiController
    {
        [HttpGet]
        [Route("api/quote/{symbol}")]
        public IHttpActionResult Get(string symbol)
        {
            using (StockMarketAnalysis context = new StockMarketAnalysis())
            {

                CalculateMovingAverage(context.HQuotes.Where(q => q.Symbol == symbol).Select(q => new QuoteChartItem
                {
                    Symbol = q.Symbol,
                    Date = q.Date,
                    Price = q.Close
                }).ToList());

                return Ok(context.HQuotes.Where(q => q.Symbol == symbol).Select(q => new QuoteChartItem
                {
                    Symbol = q.Symbol,
                    Date = q.Date,
                    Price = q.Close
                }).ToList());
            }

            
        }

        public void CalculateMovingAverage(IList<QuoteChartItem> quotes)
        {
            var ps = quotes.Select(x => x.Price).ToList();
            int periodLength = 7;

            var temp = Enumerable
                .Range(0, quotes.Count - periodLength)
                .Select(n => quotes.Skip(n).Take(periodLength).Average(x => x.Price))
                .ToList();
        }
    }
}
