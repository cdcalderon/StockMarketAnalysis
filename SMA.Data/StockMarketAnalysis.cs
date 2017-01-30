using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMA.Data
{
    public class StockMarketAnalysis : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<HQuote> HQuotes { get; set; }

        public StockMarketAnalysis()
            :base("name=StockMarketAnalysis")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WatchList>()
                .HasMany(w => w.Stocks)
                .WithMany(s => s.WatchLists)
                .Map(map =>
                {
                    map.ToTable("WatchListStock");
                    map.MapLeftKey("WatchListId");
                    map.MapRightKey("StockId");
                });

        }
    }
}
