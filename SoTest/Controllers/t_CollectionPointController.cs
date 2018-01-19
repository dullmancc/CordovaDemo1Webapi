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
    public class t_CollectionPointController : ApiController
    {
        private SoilTestEntities1 db = new SoilTestEntities1();

        // GET: api/t_CollectionPoint
        public IQueryable<t_CollectionPoint> Gett_CollectionPoint()
        {
            return db.t_CollectionPoint;
        }

        // GET: api/t_CollectionPoint/5
        [ResponseType(typeof(t_CollectionPoint))]
        public IHttpActionResult Gett_CollectionPoint(string id)
        {
            t_CollectionPoint t_CollectionPoint = db.t_CollectionPoint.Find(id);
            if (t_CollectionPoint == null)
            {
                return NotFound();
            }

            return Ok(t_CollectionPoint);
        }

        // PUT: api/t_CollectionPoint/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_CollectionPoint(string id, t_CollectionPoint t_CollectionPoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_CollectionPoint.CollectionPointID)
            {
                return BadRequest();
            }

            db.Entry(t_CollectionPoint).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_CollectionPointExists(id))
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

        // POST: api/t_CollectionPoint
        [ResponseType(typeof(t_CollectionPoint))]
        public IHttpActionResult Postt_CollectionPoint(t_CollectionPoint t_CollectionPoint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.t_CollectionPoint.Add(t_CollectionPoint);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (t_CollectionPointExists(t_CollectionPoint.CollectionPointID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = t_CollectionPoint.CollectionPointID }, t_CollectionPoint);
        }

        // DELETE: api/t_CollectionPoint/5
        [ResponseType(typeof(t_CollectionPoint))]
        public IHttpActionResult Deletet_CollectionPoint(string id)
        {
            t_CollectionPoint t_CollectionPoint = db.t_CollectionPoint.Find(id);
            if (t_CollectionPoint == null)
            {
                return NotFound();
            }

            db.t_CollectionPoint.Remove(t_CollectionPoint);
            db.SaveChanges();

            return Ok(t_CollectionPoint);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_CollectionPointExists(string id)
        {
            return db.t_CollectionPoint.Count(e => e.CollectionPointID == id) > 0;
        }
    }
}