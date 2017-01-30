using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SMA.Data;
using SMA.StockManagerService.Constants;
using SMA.StockManagerService.Utils;

namespace SMA.StockManagerService
{
    class StockManagerService
    {
        private FileSystemWatcher _watcher;

        public bool Start()
        {
            GetQuote();
            GetHistoricalQuotes();
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

        private void GetHistoricalQuotes()
        {
            XDocument doc = XDocument.Load(StockMarketAnalysisConstants.YahooQuoteHistoricalAPI);
            var stockHistoricalQuote = YahooStockEngine.ParseHistorical(doc);
            
        }

        public bool Stop()
        {
            _watcher.Dispose();

            return true;
        }

    }
}
