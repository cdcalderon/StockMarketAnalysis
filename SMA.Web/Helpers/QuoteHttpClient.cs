using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using SMA.Data;
using SMA.Web.Constants;
using SMA.Web.Services;

namespace SMA.Web.Helpers
{
    public static class QuoteHttpClient
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();

                IList<Quote> quotes= new List<Quote>();

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
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpClient GetClientHistorical()
        {
            HttpClient client = new HttpClient();

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

            var url = YahooStockEngine.GetQuoteRequestUrl(StockMarketAnalysisConstants.YahooQuoteHistoricalAPI, quotes);
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


    }
}