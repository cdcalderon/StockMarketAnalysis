using System;

namespace SMA.Core.DTOs
{
    public class QuoteChartItem
    {
        public string Symbol { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Price { get; set; }
    }
}