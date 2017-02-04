using System.Collections.Generic;

namespace SMA.Core.Models
{
    public class WatchList
    {
        public WatchList()
        {
            Stocks = new HashSet<Stock>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
    
}
