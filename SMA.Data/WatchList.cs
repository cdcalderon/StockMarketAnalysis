using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMA.Data
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
