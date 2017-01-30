using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using SMA.Web.Helpers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMA.Data;
using SMA.DTO;
using SMA.Web.Constants;
using SMA.Web.Services;
using Quote = SMA.Data.Quote;

namespace SMA.Web.Controllers
{
    public class QuoteController : ApiController
    {

        // GET: api/Quote
        public async Task<IHttpActionResult> Get()
        {
            var client = QuoteHttpClient.GetClient();
            IList<Quote> quotes = new List<Quote>();

            //Some example tickers
            quotes.Add(new Quote("AAPL"));
            quotes.Add(new Quote("MSFT"));
            quotes.Add(new Quote("INTC"));
            quotes.Add(new Quote("IBM"));
            quotes.Add(new Quote("RVBD"));
            quotes.Add(new Quote("AMZN"));
            quotes.Add(new Quote("BIDU"));
            quotes.Add(new Quote("SINA"));
            quotes.Add(new Quote("THI"));
            quotes.Add(new Quote("NVDA"));
            quotes.Add(new Quote("AMD"));
            quotes.Add(new Quote("DELL"));
            quotes.Add(new Quote("WMT"));
            quotes.Add(new Quote("GLD"));
            quotes.Add(new Quote("SLV"));
            quotes.Add(new Quote("V"));
            quotes.Add(new Quote("V"));
            quotes.Add(new Quote("MCD"));

            var url = YahooStockEngine.GetQuoteRequestUrl(StockMarketAnalysisConstants.YahooQuoteAPI, quotes);
             XDocument doc = XDocument.Load(url);
             YahooStockEngine.Parse(quotes, doc);
            string results = "";
            //            using (WebClient wc = new WebClient())
            //            {
            //                wc.Encoding = Encoding.UTF8;
            //               // wc.Headers.Add("Content-Type", "application/json");
            //                wc.Headers.Add("accept", "application/json");
            //                results = wc.DownloadString(url);
            //               // results = Content(data, "application/json");
            //            }
            //            JObject dataObject = JObject.Parse(results);
            //            JArray jsonArray = (JArray)dataObject["query"];

            var url2 = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22AAPL%22%20and%20startDate%20%3D%20%222012-09-11%22%20and%20endDate%20%3D%20%222014-02-11%22&format=xml&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
            XDocument doc2 = XDocument.Load(url2);
            YahooStockEngine.ParseHistorical(doc2);
            string results2 = "";




            var clientHistorical = QuoteHttpClient.GetClientHistorical();
            HttpResponseMessage response = await clientHistorical.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                //dynamic d = JObject.Parse(content);

                //Debug.WriteLine(d.results);
                 var model = JsonConvert.DeserializeObject<QuoteQuery>(content);
                return Ok("");

            }
            else
            {
                return InternalServerError();
            }
        }

        // GET: api/Quote/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Quote
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Quote/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Quote/5
        public void Delete(int id)
        {
        }
    }
}
