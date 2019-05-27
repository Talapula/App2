using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonDetailsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PersonDetails
        public IEnumerable<PersonDetail> GetPersonDetails()
        {
            return db.PersonDetails;
        }

        // GET: api/PersonDetails/5
        [ResponseType(typeof(PersonDetail))]
        public IHttpActionResult GetPersonDetail(int id)
        {
            PersonDetail personDetail = db.PersonDetails.Find(id);
            if (personDetail == null)
            {
                return NotFound();
            }

            return Ok(personDetail);
        }

        // PUT: api/PersonDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersonDetail(PersonDetail personDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(personDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonDetailExists(personDetail.Id))
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

        // POST: api/PersonDetails
        [ResponseType(typeof(PersonDetail))]
        public IHttpActionResult PostPersonDetail(PersonDetail personDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PersonDetails.Add(personDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonDetailExists(personDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = personDetail.Id }, personDetail);
        }

        // DELETE: api/PersonDetails/5
        [ResponseType(typeof(PersonDetail))]
        public IHttpActionResult DeletePersonDetail(int id)
        {
            PersonDetail personDetail = db.PersonDetails.Find(id);
            if (personDetail == null)
            {
                return NotFound();
            }

            db.PersonDetails.Remove(personDetail);
            db.SaveChanges();

            return Ok(personDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonDetailExists(int id)
        {
            return db.PersonDetails.Count(e => e.Id == id) > 0;
        }
    }
}