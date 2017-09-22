namespace BeerAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Style = c.String(),
                        ABV = c.Double(nullable: false),
                        IBU = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        BreweryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Breweries", t => t.BreweryId, cascadeDelete: true)
                .Index(t => t.BreweryId);
            
            CreateTable(
                "dbo.Breweries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Neighborhood = c.String(),
                        Address = c.String(),
                        Patio = c.Boolean(nullable: false),
                        Wifi = c.Boolean(nullable: false),
                        FoodTrucks = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Beers", "BreweryId", "dbo.Breweries");
            DropIndex("dbo.Beers", new[] { "BreweryId" });
            DropTable("dbo.Breweries");
            DropTable("dbo.Beers");
        }
    }
}
