using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SMA.Core.Common;
using SMA.Core.Models;
using SMA.Data;
using SMA.StockManagerService.Constants;
using SMA.StockManagerService.Helpers;
using SMA.StockManagerService.Utils;

namespace SMA.StockManagerService
{
    class StockManagerService
    {
        private FileSystemWatcher _watcher;

        public bool Start()
        {
             GetQuote();
          //  PopulateHistoricalQuotes("DPZ");
          // FindGap();
             _watcher = new FileSystemWatcher(@"c:\temp\a", "*_in.txt");

            _watcher.Created += FileCreated;
            _watcher.IncludeSubdirectories = false;
            _watcher.EnableRaisingEvents = true;
            return true;
        }

        private void FileCreated(object sender, FileSystemEventArgs e)
        {
            string content = File.ReadAllText(e.FullPath);

            string upperContent = content.ToLowerInvariant();

            var dir = Path.GetDirectoryName(e.FullPath);

            var convertedFileName = Path.GetFileName(e.FullPath) + ".converted";

            var convertedPath = Path.Combine(dir, convertedFileName);

            File.WriteAllText(convertedPath,upperContent);
        }

        private void GetQuote()
        {
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
            
        }

        private IEnumerable<HQuote> GetHistoricalQuotes(string symbol, string startDate, string endDate)
        {
            var url = string.Format(StockMarketAnalysisConstants.YahooQuoteHistoricalAPI, symbol, startDate, endDate);
            XDocument doc = XDocument.Load(url);
           return YahooStockEngine.ParseHistorical(doc);
            
        }
        

        public void PopulateHistoricalQuotes(string symbol)
        {
            IEnumerable<HistoricalQuoteParams> hqps = new List<HistoricalQuoteParams>()
            {
                new HistoricalQuoteParams()
                {
                    Symbol = symbol,
                    StartDate = "2015-02-05",
                    EndDate = "2015-12-31"
                }
            };


            using (StockMarketAnalysis context = new StockMarketAnalysis())
            {
                foreach (var hQuoteParam in hqps)
                {
                    var hQuotes = GetHistoricalQuotes(hQuoteParam.Symbol, hQuoteParam.StartDate, hQuoteParam.EndDate);
                    foreach (var q in hQuotes)
                    {
                        HQuote hq = new HQuote();
                        hq.Symbol = q.Symbol;
                        hq.Date = q.Date;
                        hq.Open = q.Open;
                        hq.High = q.High;
                        hq.Low = q.Low;
                        hq.Close = q.Close;
                        hq.Volume = q.Volume;

                        context.HQuotes.Add(hq);
                    }
                    context.SaveChanges();

                }

            }
        }

        public void FindGap()
        {
            using (StockMarketAnalysis context = new StockMarketAnalysis())
            {
                StockGapEngine.FindGap(context.HQuotes.ToList());
            }
        }
        
        public bool Stop()
        {
            _watcher.Dispose();

            return true;
        }

    }
}
