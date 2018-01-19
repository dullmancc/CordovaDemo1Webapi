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
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SoTest.Controllers
{
    public class t_TaskController : ApiController
    {
        private SoilTestEntities1 db = new SoilTestEntities1();

        // GET: api/t_Task
        public IQueryable<t_Task> Gett_Task()
        {
            return db.t_Task;
        }

        // GET: api/t_Task/5
        [ResponseType(typeof(t_Task))]
        public IHttpActionResult Gett_Task(string id)
        {
            t_Task t_Task = db.t_Task.Find(id);
            if (t_Task == null)
            {
                return NotFound();
            }

            return Ok(t_Task);
        }
        [HttpPost]
        public async Task<UploadFile> Post([FromUri]PostPhoto postPhoto)
        {

            var re = db.t_Task.Find(postPhoto .panzhangid+ "");

            if (re.PictureURL == null)
            {
                re.PictureURL = "";
            }
            String imp = re.PictureURL;
            int ic = imp.Split(',').Length;

            //new folder
            string newRoot = @"D:/Uploads/";

            List<string> fileNameList = new List<string>();

            StringBuilder sb = new StringBuilder();

            long fileTotalSize = 0;

            int fileIndex = 1;
            UploadFile us = new UploadFile();
            //get files from request
            HttpFileCollection files = HttpContext.Current.Request.Files;

            await Task.Run(() =>
            {
                foreach (var f in files.AllKeys)
                {
                    HttpPostedFile file = files[f];
                    if (!string.IsNullOrEmpty(file.FileName))
                    {

                        string fileLocalFullName = newRoot + postPhoto.panzhangid + "_" + postPhoto.EmployeeId + "_" + (ic + fileIndex) + ".png";

                        file.SaveAs(fileLocalFullName);

                        fileNameList.Add(fileLocalFullName);

                        FileInfo fileInfo = new FileInfo(fileLocalFullName);

                        fileTotalSize += fileInfo.Length;

                        sb.Append($" #{fileIndex} Uploaded file: {file.FileName} ({ fileInfo.Length} bytes)");

                        fileIndex++;

                        Trace.WriteLine("1 file copied , filePath=" + fileLocalFullName);
                    }
                }
                us.ErrorCode = 0;
                us.Info = sb.ToString();
            });
            String IMGPATH;
            if (fileNameList.Count == 1)
            {
                IMGPATH = ",";
            }
            else
            {
                IMGPATH = "";
            }


            for (int i = 0; i < fileNameList.Count; i++)
            {
                IMGPATH += fileNameList[i];
                if (i != fileNameList.Count - 1)
                {
                    IMGPATH += ",";
                }
            }


           re.PictureURL += IMGPATH;

            db.SaveChanges();

            return us;
        }

        public IHttpActionResult Test(string id, t_Task t_Task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_Task.TaskID)
            {
                return BadRequest();
            }

            db.Entry(t_Task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_TaskExists(id))
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

        [HttpPut]
        public IHttpActionResult ReSetTask(string id, t_Task t_Task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != t_Task.TaskID)
            {
                return BadRequest();
            }

            db.Entry(t_Task).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!t_TaskExists(id))
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

        // POST: api/t_Task
        [ResponseType(typeof(t_Task))]
        public IHttpActionResult Postt_Task(t_Task t_Task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.t_Task.Add(t_Task);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (t_TaskExists(t_Task.TaskID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = t_Task.TaskID }, t_Task);
        }

        // DELETE: api/t_Task/5
        [ResponseType(typeof(t_Task))]
        public IHttpActionResult Deletet_Task(string id)
        {
            t_Task t_Task = db.t_Task.Find(id);
            if (t_Task == null)
            {
                return NotFound();
            }

            db.t_Task.Remove(t_Task);
            db.SaveChanges();

            return Ok(t_Task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool t_TaskExists(string id)
        {
            return db.t_Task.Count(e => e.TaskID == id) > 0;
        }
    }
}