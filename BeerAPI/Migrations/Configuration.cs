namespace BeerAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BeerAPI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BeerAPI.Models.BeerAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BeerAPI.Models.BeerAPIContext context)
        {
           context.Beers.AddOrUpdate(p => p.Id,
                new Beer { ABV = 6.4, BreweryId = 1, Description = "Estery, Delicious. Nice fruity notes.", IBU = 20, Id = 1, Name = "Saison", Rating = 92, Style = "Saison/Farmhouse Ale" },
                new Beer { ABV = 5.5, BreweryId = 1, Description = "Sessionable, well balanced.", IBU = 45, Id = 2, Name = "Strip Mall Pale", Rating = 90, Style = "American Pale Ale" },
                new Beer { ABV = 8.4, BreweryId = 1, Description = "Booze is well hidden. Sipper for sure.", IBU = 60, Id = 3, Name = "Big Hoppy Brown", Rating = 0, Style = "American Brown Ale" },
                new Beer { ABV = 7, BreweryId = 2, Description = "One of the best IPAs in the city.", IBU = 80, Id = 4, Name = "Antidote", Rating = 94, Style = "American IPA" },
                new Beer { ABV = 5.1, BreweryId = 2, Description = "Great banana esters. Nice grainy finish.", IBU = 14, Id = 5, Name = "Handwritten", Rating = 95, Style = "Hefeweizen" },
                new Beer { ABV = 6, BreweryId = 2, Description = "Low on hops, big on esters. Slight tartness.", IBU = 13, Id = 6, Name = "Dear You", Rating = 90, Style = "Saison/Farmhouse Ale" },
                new Beer { ABV = 6.5, BreweryId = 3, Description = "Hoppy, metal.", IBU = 90, Id = 7, Name = "Tunnel of Trees", Rating = 85, Style = "American IPA" },
                new Beer { ABV = 6, BreweryId = 3, Description = "Roasty with slight licorice hints.", IBU = 35, Id = 8, Name = "Stout O)))", Rating = 87, Style = "Stout" });

            context.Breweries.AddOrUpdate(b => b.Id,
                new Brewery { Id = 1, Name = "Baere Brewing Co.", Address = "320 Broadway, Denver, CO", Neighborhood = "South Broadway", FoodTrucks = false, Patio = true, Wifi = true },
                 new Brewery { Id = 2, Name = "Ratio Beerworks", Address = "2920 Larimer Street, Denver, CO", Neighborhood = "RiNo", FoodTrucks = true, Patio = true, Wifi = true },
                  new Brewery { Id = 3, Name = "Trve Brewing", Address = "227 Broadway, Denver, CO", Neighborhood = "South Broadway", FoodTrucks = false, Patio = false, Wifi = false });

        }
    }
}
