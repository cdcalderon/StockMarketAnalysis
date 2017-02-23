using System;

namespace SMA.Core.DTOs
{

    public class StockGap
    {
        public string Symbol { get; set; }
        public DateTime? Date { get; set; }
        public decimal? LastPrice{ get; set; }
        public decimal? GapSize { get; set; }
    }
}