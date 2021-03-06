﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SMA.Data;

using System.Linq.Expressions;
using SMA.Core.Models;

namespace SMA.StockManagerService.Utils
{
    public class YahooStockEngine
    {
        private const string BASE_URL = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        public static void Fetch(IEnumerable<Quote> quotes)
        {
        //    private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
        //public ObservableCollection<Quote> Quotes { get; set; }
        //Quotes = new ObservableCollection<Quote>();

        //    //Some example tickers
        //    Quotes.Add(new Quote("AAPL"));
        //    Quotes.Add(new Quote("MSFT"));
        //    Quotes.Add(new Quote("INTC"));
        //    Quotes.Add(new Quote("IBM"));
        //    Quotes.Add(new Quote("RVBD"));
        //    Quotes.Add(new Quote("AMZN"));
        //    Quotes.Add(new Quote("BIDU"));
        //    Quotes.Add(new Quote("SINA"));
        //    Quotes.Add(new Quote("THI"));
        //    Quotes.Add(new Quote("NVDA"));
        //    Quotes.Add(new Quote("AMD"));
        //    Quotes.Add(new Quote("DELL"));
        //    Quotes.Add(new Quote("WMT"));
        //    Quotes.Add(new Quote("GLD"));
        //    Quotes.Add(new Quote("SLV"));
        //    Quotes.Add(new Quote("V"));
        //    Quotes.Add(new Quote("V"));
        //    Quotes.Add(new Quote("MCD"));

        //    //get the data
        //    YahooStockEngine.Fetch(Quotes);

        //    //poll every 25 seconds
        //    timer.Interval = new TimeSpan(0, 0, 25);
        //timer.Tick += (o, e) => YahooStockEngine.Fetch(Quotes);
                              
        //    timer.Start();

            string symbolList = String.Join("%2C", quotes.Select(w => "%22" + w.Symbol + "%22").ToArray());
            string url = string.Format(BASE_URL, symbolList);

            XDocument doc = XDocument.Load(url);
            Parse(quotes, doc);
        }

        public  static string GetQuoteRequestUrl(string urlBase, IEnumerable<Quote> quotes)
        {
            string symbolList = String.Join("%2C", quotes.Select(w => "%22" + w.Symbol + "%22").ToArray());
            return string.Format(urlBase, symbolList);
        }

        public static void Parse(IEnumerable<Quote> quotes, XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            foreach (Quote quote in quotes)
            {
                XElement q = results.Elements("quote").First(w => w.Attribute("symbol").Value == quote.Symbol);

                quote.Ask = GetDecimal(q.Element("Ask").Value);
                quote.Bid = GetDecimal(q.Element("Bid").Value);
                quote.AverageDailyVolume = GetDecimal(q.Element("AverageDailyVolume").Value);
                quote.BookValue = GetDecimal(q.Element("BookValue").Value);
                quote.Change = GetDecimal(q.Element("Change").Value);
                quote.DividendShare = GetDecimal(q.Element("DividendShare").Value);
                quote.LastTradeDate = GetDateTime(q.Element("LastTradeDate") + " " + q.Element("LastTradeTime").Value);
                quote.EarningsShare = GetDecimal(q.Element("EarningsShare").Value);
                quote.EpsEstimateCurrentYear = GetDecimal(q.Element("EPSEstimateCurrentYear").Value);
                quote.EpsEstimateNextYear = GetDecimal(q.Element("EPSEstimateNextYear").Value);
                quote.EpsEstimateNextQuarter = GetDecimal(q.Element("EPSEstimateNextQuarter").Value);
                quote.DailyLow = GetDecimal(q.Element("DaysLow").Value);
                quote.DailyHigh = GetDecimal(q.Element("DaysHigh").Value);
                quote.YearlyLow = GetDecimal(q.Element("YearLow").Value);
                quote.YearlyHigh = GetDecimal(q.Element("YearHigh").Value);
                quote.MarketCapitalization = GetDecimal(q.Element("MarketCapitalization").Value);
                quote.Ebitda = GetDecimal(q.Element("EBITDA").Value);
                quote.ChangeFromYearLow = GetDecimal(q.Element("ChangeFromYearLow").Value);
                quote.PercentChangeFromYearLow = GetDecimal(q.Element("PercentChangeFromYearLow").Value);
                quote.ChangeFromYearHigh = GetDecimal(q.Element("ChangeFromYearHigh").Value);
                quote.LastTradePrice = GetDecimal(q.Element("LastTradePriceOnly").Value);
                quote.PercentChangeFromYearHigh = GetDecimal(q.Element("PercebtChangeFromYearHigh").Value); //missspelling in yahoo for field name
                quote.FiftyDayMovingAverage = GetDecimal(q.Element("FiftydayMovingAverage").Value);
                quote.TwoHunderedDayMovingAverage = GetDecimal(q.Element("TwoHundreddayMovingAverage").Value);
                quote.ChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value);
                quote.PercentChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
                quote.PercentChangeFromFiftyDayMovingAverage = GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value);
                quote.Name = q.Element("Name").Value;
                quote.Open = GetDecimal(q.Element("Open").Value);
                quote.PreviousClose = GetDecimal(q.Element("PreviousClose").Value);
                quote.ChangeInPercent = GetDecimal(q.Element("ChangeinPercent").Value);
                quote.PriceSales = GetDecimal(q.Element("PriceSales").Value);
                quote.PriceBook = GetDecimal(q.Element("PriceBook").Value);
                quote.ExDividendDate = GetDateTime(q.Element("ExDividendDate").Value);
                quote.PeRatio = GetDecimal(q.Element("PERatio").Value);
                quote.DividendPayDate = GetDateTime(q.Element("DividendPayDate").Value);
                quote.PegRatio = GetDecimal(q.Element("PEGRatio").Value);
                quote.PriceEpsEstimateCurrentYear = GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value);
                quote.PriceEpsEstimateNextYear = GetDecimal(q.Element("PriceEPSEstimateNextYear").Value);
                quote.ShortRatio = GetDecimal(q.Element("ShortRatio").Value);
                quote.OneYearPriceTarget = GetDecimal(q.Element("OneyrTargetPrice").Value);
                quote.Volume = GetDecimal(q.Element("Volume").Value);
                quote.StockExchange = q.Element("StockExchange").Value;

                quote.LastUpdate = DateTime.Now;
            }
        }


        public static IEnumerable<HQuote> ParseHistorical(XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            //var stockQuotes = results.Elements("quote").Select(q => new DTO.Quote
            //{
            //    Symbol = (string)q.Element("Symbol"),
            //    DailyHigh = (decimal)q.Element("High"),
            //}).ToList();

            var stockQuotes = results.Elements("quote").Select(q => new HQuote
            {
                Symbol = (string)q.Attribute("Symbol"),
                Date = Convert.ToDateTime((string)q.Element("Date")),
                Open = (decimal)q.Element("Open"),
                High = (decimal)q.Element("High"),
                Low = (decimal)q.Element("Low"),
                Close = (decimal)q.Element("Close"),
                Volume = (decimal)q.Element("Volume")
            }).ToList();

            return stockQuotes;
            // return stockQuotes;
        }

        
        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;

            input = input.Replace("%", "");

            decimal value;

            if (Decimal.TryParse(input, out value)) return value;
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }
    }
}
