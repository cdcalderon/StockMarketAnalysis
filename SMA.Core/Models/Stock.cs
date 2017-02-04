using System.Collections.Generic;

namespace SMA.Core.Models
{
    public class Stock
    {
        public Stock()
        {
            
            WatchLists = new HashSet<WatchList>();
        }

        public int Id { get; set; }
        public decimal DayHigh { get; set; }
        public decimal DayLow { get; set; }
        public decimal Open { get; set; }
        public decimal Volume { get; set; }
        public decimal YearHigh { get; set; }
        public decimal YearLow { get; set; }
        public decimal AverageVolume { get; set; }
        public decimal MarketCap { get; set; }
        public int ExchangeId { get; set; }

        // Navigation properties
        public virtual Exchange Exchange { get; set; }

        public ICollection<WatchList> WatchLists { get; set; }
    }
}
