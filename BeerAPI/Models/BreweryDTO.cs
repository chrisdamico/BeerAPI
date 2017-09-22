using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerAPI.Models
{
    public class BreweryDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }
        public bool Patio { get; set; }
        public bool Wifi { get; set; }
        public bool FoodTrucks { get; set; }
    }
}