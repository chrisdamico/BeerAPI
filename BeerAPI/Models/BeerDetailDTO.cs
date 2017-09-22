using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerAPI.Models
{
    public class BeerDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Style { get; set; }
        public double ABV { get; set; }
        public int IBU { get; set; }
        public int Rating { get; set; }
        public int BreweryId { get; set; }
    }
}