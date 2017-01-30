namespace SMA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.Int(nullable: false),
                        CustomerCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WatchLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayHigh = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DayLow = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Dividend = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Open = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Volume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YearHigh = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YearLow = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AverageVolume = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarketCap = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExchangeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exchanges", t => t.ExchangeId, cascadeDelete: true)
                .Index(t => t.ExchangeId);
            
            CreateTable(
                "dbo.Exchanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        AverageDailyVolume = c.Decimal(precision: 18, scale: 2),
                        Bid = c.Decimal(precision: 18, scale: 2),
                        Ask = c.Decimal(precision: 18, scale: 2),
                        BookValue = c.Decimal(precision: 18, scale: 2),
                        ChangePercent = c.Decimal(precision: 18, scale: 2),
                        Change = c.Decimal(precision: 18, scale: 2),
                        DividendShare = c.Decimal(precision: 18, scale: 2),
                        LastTradeDate = c.DateTime(),
                        EarningsShare = c.Decimal(precision: 18, scale: 2),
                        EpsEstimateCurrentYear = c.Decimal(precision: 18, scale: 2),
                        EpsEstimateNextYear = c.Decimal(precision: 18, scale: 2),
                        EpsEstimateNextQuarter = c.Decimal(precision: 18, scale: 2),
                        DailyLow = c.Decimal(precision: 18, scale: 2),
                        DailyHigh = c.Decimal(precision: 18, scale: 2),
                        YearlyLow = c.Decimal(precision: 18, scale: 2),
                        YearlyHigh = c.Decimal(precision: 18, scale: 2),
                        MarketCapitalization = c.Decimal(precision: 18, scale: 2),
                        Ebitda = c.Decimal(precision: 18, scale: 2),
                        ChangeFromYearLow = c.Decimal(precision: 18, scale: 2),
                        PercentChangeFromYearLow = c.Decimal(precision: 18, scale: 2),
                        ChangeFromYearHigh = c.Decimal(precision: 18, scale: 2),
                        PercentChangeFromYearHigh = c.Decimal(precision: 18, scale: 2),
                        LastTradePrice = c.Decimal(precision: 18, scale: 2),
                        FiftyDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        TwoHunderedDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        ChangeFromTwoHundredDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        PercentChangeFromFiftyDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        Name = c.String(),
                        Open = c.Decimal(precision: 18, scale: 2),
                        PreviousClose = c.Decimal(precision: 18, scale: 2),
                        ChangeInPercent = c.Decimal(precision: 18, scale: 2),
                        PriceSales = c.Decimal(precision: 18, scale: 2),
                        PriceBook = c.Decimal(precision: 18, scale: 2),
                        ExDividendDate = c.DateTime(),
                        PegRatio = c.Decimal(precision: 18, scale: 2),
                        PriceEpsEstimateCurrentYear = c.Decimal(precision: 18, scale: 2),
                        PriceEpsEstimateNextYear = c.Decimal(precision: 18, scale: 2),
                        ShortRatio = c.Decimal(precision: 18, scale: 2),
                        OneYearPriceTarget = c.Decimal(precision: 18, scale: 2),
                        DividendYield = c.Decimal(precision: 18, scale: 2),
                        DividendPayDate = c.DateTime(),
                        PercentChangeFromTwoHundredDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        PeRatio = c.Decimal(precision: 18, scale: 2),
                        Volume = c.Decimal(precision: 18, scale: 2),
                        StockExchange = c.String(),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WatchListStock",
                c => new
                    {
                        WatchListId = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchListId, t.StockId })
                .ForeignKey("dbo.WatchLists", t => t.WatchListId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.WatchListId)
                .Index(t => t.StockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WatchLists", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.WatchListStock", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.WatchListStock", "WatchListId", "dbo.WatchLists");
            DropForeignKey("dbo.Stocks", "ExchangeId", "dbo.Exchanges");
            DropIndex("dbo.WatchListStock", new[] { "StockId" });
            DropIndex("dbo.WatchListStock", new[] { "WatchListId" });
            DropIndex("dbo.Stocks", new[] { "ExchangeId" });
            DropIndex("dbo.WatchLists", new[] { "Customer_Id" });
            DropTable("dbo.WatchListStock");
            DropTable("dbo.Quotes");
            DropTable("dbo.Exchanges");
            DropTable("dbo.Stocks");
            DropTable("dbo.WatchLists");
            DropTable("dbo.Customers");
        }
    }
}
