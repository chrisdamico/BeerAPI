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
    public class BeersController : ApiController
    {
        private BeerAPIContext db = new BeerAPIContext();

        // GET: api/Beers
        public IQueryable<BeerDTO> GetBeers()
        {
            IQueryable<BeerDTO> beers = from b in db.Beers
                                        select new BeerDTO()
                                        {
                                            Id = b.Id,
                                            Name = b.Name,
                                            Style = b.Style,
                                            Description = b.Description,
                                            ABV = b.ABV,
                                            IBU = b.IBU,
                                            Rating = b.Rating,
                                            BreweryId = b.BrewedBy.Id



                                        };
            return beers;
        }

        // GET: api/Beers/5
        [ResponseType(typeof(BeerDetailDTO))]
        public async Task<IHttpActionResult> GetBeer(int id)
        {
            BeerDetailDTO beer = await db.Beers.Include(b => b.BrewedBy).Select(b =>
                  new BeerDetailDTO()
                  {
                      Id = b.Id,
                      Name = b.Name,
                      Style = b.Style,
                      Description = b.Description,
                      ABV = b.ABV,
                      IBU = b.IBU,
                      Rating = b.Rating,
                      BreweryId = b.BrewedBy.Id

                  }).SingleOrDefaultAsync(b => b.Id == id);

            if (beer == null)
            {
                return NotFound();
            }

            return Ok(beer);
        }

        // PUT: api/Beers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBeer(int id, BeerDetailDTO beer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != beer.Id)
            {
                return BadRequest();
            }

            Beer beerBase = new Beer
            {
                Style = beer.Style,
                Name = beer.Name,
                Id = beer.Id,
                ABV = beer.ABV,
                BreweryId = beer.BreweryId,
                Description = beer.Description,
                IBU = beer.IBU,
                Rating = beer.Rating
            };

            db.Entry(beerBase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
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

        // POST: api/Beers
        [ResponseType(typeof(BeerDTO))]
        public async Task<IHttpActionResult> PostBeer(Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Beers.Add(beer);
            await db.SaveChangesAsync();

            db.Entry(beer).Reference(b => b.BrewedBy).Load();

            BeerDTO returnBeer = new BeerDTO()
            {
                Id = beer.Id,
                Name = beer.Name,
                Style = beer.Style
            };

            return CreatedAtRoute("DefaultApi", new { id = beer.Id }, returnBeer);
        }

        // DELETE: api/Beers/5
        [ResponseType(typeof(Beer))]
        public async Task<IHttpActionResult> DeleteBeer(int id)
        {
            Beer beer = await db.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            db.Beers.Remove(beer);
            await db.SaveChangesAsync();

            return Ok(beer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BeerExists(int id)
        {
            return db.Beers.Count(e => e.Id == id) > 0;
        }
    }
}