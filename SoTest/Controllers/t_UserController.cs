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
    public class t_UserController : ApiController
    {
        private SoilTestEntities1 db = new SoilTestEntities1();

        // GET: api/t_User
        public IQueryable<t_User> Gett_User()
        {
            return db.t_User;
        }

        // GET: api/t_User/5
        [ResponseType(typeof(t_User))]
        public IHttpActionResult Gett_User(string id)
        {
            t_User t_User = db.t_User.Find(id);
            if (t_User == null)
            {
                return NotFound();
            }

            return Ok(t_User);
        }

        // PUT: api/t_User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putt_User(string id, t_User t_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_User.UserID)
            {
                return BadRequest();
            }

            db.Entry(t_User).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_UserExists(id))
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

        // POST: api/t_User
        [ResponseType(typeof(t_User))]
        public IHttpActionResult Postt_User(t_User t_User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.t_User.Add(t_User);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (t_UserExists(t_User.UserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = t_User.UserID }, t_User);
        }

        // DELETE: api/t_User/5
        [ResponseType(typeof(t_User))]
        public IHttpActionResult Deletet_User(string id)
        {
            t_User t_User = db.t_User.Find(id);
            if (t_User == null)
            {
                return NotFound();
            }

            db.t_User.Remove(t_User);
            db.SaveChanges();

            return Ok(t_User);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_UserExists(string id)
        {
            return db.t_User.Count(e => e.UserID == id) > 0;
        }
    }
}