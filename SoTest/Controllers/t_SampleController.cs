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
using SoTest.Models;

namespace SoTest.Controllers
{
    public class t_SampleController : ApiController
    {
        private SoilTestEntities1 db = new SoilTestEntities1();

        // GET: api/t_Sample
        public IQueryable<t_Sample> Gett_Sample()
        {
            return db.t_Sample;
        }

        // GET: api/t_Sample/5
        [ResponseType(typeof(t_Sample))]
        public IHttpActionResult Gett_Sample(long id)
        {
            t_Sample t_Sample = db.t_Sample.Find(id);
            if (t_Sample == null)
            {
                return NotFound();
            }

            return Ok(t_Sample);
        }

        // PUT: api/t_Sample/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_Sample(long id, t_Sample t_Sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_Sample.SampleID)
            {
                return BadRequest();
            }

            db.Entry(t_Sample).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_SampleExists(id))
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

        // POST: api/t_Sample
        [ResponseType(typeof(t_Sample))]
        public IHttpActionResult Postt_Sample(t_Sample t_Sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.t_Sample.Add(t_Sample);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (t_SampleExists(t_Sample.SampleID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = t_Sample.SampleID }, t_Sample);
        }

        // DELETE: api/t_Sample/5
        [ResponseType(typeof(t_Sample))]
        public IHttpActionResult Deletet_Sample(long id)
        {
            t_Sample t_Sample = db.t_Sample.Find(id);
            if (t_Sample == null)
            {
                return NotFound();
            }

            db.t_Sample.Remove(t_Sample);
            db.SaveChanges();

            return Ok(t_Sample);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_SampleExists(long id)
        {
            return db.t_Sample.Count(e => e.SampleID == id) > 0;
        }
    }
}