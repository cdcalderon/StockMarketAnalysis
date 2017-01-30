namespace SMA.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStockAndAddHQuote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Date = c.DateTime(),
                        Open = c.Decimal(precision: 18, scale: 2),
                        High = c.Decimal(precision: 18, scale: 2),
                        Low = c.Decimal(precision: 18, scale: 2),
                        Close = c.Decimal(precision: 18, scale: 2),
                        Volume = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Stocks", "Dividend");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stocks", "Dividend", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.HQuotes");
        }
    }
}
