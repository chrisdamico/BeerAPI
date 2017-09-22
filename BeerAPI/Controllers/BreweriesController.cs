using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BeerAPI.Models;

namespace BeerAPI.Controllers
{
    public class BreweriesController : ApiController
    {
        private BeerAPIContext db = new BeerAPIContext();

        // GET: api/Breweries
        public IQueryable<BreweryDTO> GetBreweries()
        {
            IQueryable<BreweryDTO> breweryList = from b in db.Breweries
                                                 select new BreweryDTO()
                                                 {
                                                     Address = b.Address,
                                                     FoodTrucks = b.FoodTrucks,
                                                     Id = b.Id,
                                                     Name = b.Name,
                                                     Neighborhood = b.Neighborhood,
                                                     Patio = b.Patio,
                                                     Wifi = b.Wifi
                                                 };
            return breweryList;
        }

        // GET: api/Breweries/5
        [ResponseType(typeof(BreweryDTO))]
        public async Task<IHttpActionResult> GetBrewery(int id)
        {
            BreweryDTO brewery = await db.Breweries.Select(b =>
                    new BreweryDTO()
                    {
                        Address = b.Address,
                        FoodTrucks = b.FoodTrucks,
                        Id = b.Id,
                        Name = b.Name,
                        Neighborhood = b.Neighborhood,
                        Patio = b.Patio,
                        Wifi = b.Wifi
                    }).SingleOrDefaultAsync(b => b.Id == id);

            if (brewery == null)
            {
                return NotFound();
            }

            return Ok(brewery);
        }

        // PUT: api/Breweries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBrewery(int id, BreweryDTO brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brewery.Id)
            {
                return BadRequest();
            }

            Brewery breweryBase = new Brewery()
            {
                Address = brewery.Address,
                FoodTrucks = brewery.FoodTrucks,
                Id = brewery.Id,
                Name = brewery.Name,
                Neighborhood = brewery.Neighborhood,
                Patio = brewery.Patio,
                Wifi = brewery.Wifi
            };

            db.Entry(breweryBase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Breweries
        [ResponseType(typeof(Brewery))]
        public async Task<IHttpActionResult> PostBrewery(Brewery brewery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Breweries.Add(brewery);
            await db.SaveChangesAsync();


            BreweryDTO returnBrewery = new BreweryDTO()
            {
                Address = brewery.Address,
                FoodTrucks = brewery.FoodTrucks,
                Id = brewery.Id,
                Name = brewery.Name,
                Neighborhood = brewery.Neighborhood,
                Patio = brewery.Patio,
                Wifi = brewery.Wifi
            };


            return CreatedAtRoute("DefaultApi", new { id = brewery.Id }, returnBrewery);
        }

        // DELETE: api/Breweries/5
        [ResponseType(typeof(Brewery))]
        public async Task<IHttpActionResult> DeleteBrewery(int id)
        {
            Brewery brewery = await db.Breweries.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }

            db.Breweries.Remove(brewery);
            await db.SaveChangesAsync();

            return Ok(brewery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreweryExists(int id)
        {
            return db.Breweries.Count(e => e.Id == id) > 0;
        }
    }
}