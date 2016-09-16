using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers
{
    public class TT3Controller : ApiController
    {
        private UsersContext2 db = new UsersContext2();

        // GET api/TT3
        public IEnumerable<Emp> GetEmps()
        {
            return db.Emps.AsEnumerable();
        }

        // GET api/TT3/5
        public Emp GetEmp(int id)
        {
            Emp emp = db.Emps.Find(id);
            if (emp == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return emp;
        }

        // PUT api/TT3/5
        public HttpResponseMessage PutEmp(int id, Emp emp)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != emp.EmpId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(emp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/TT3
        public HttpResponseMessage PostEmp(Emp emp)
        {
            if (ModelState.IsValid)
            {
                db.Emps.Add(emp);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, emp);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = emp.EmpId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/TT3/5
        public HttpResponseMessage DeleteEmp(int id)
        {
            Emp emp = db.Emps.Find(id);
            if (emp == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Emps.Remove(emp);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, emp);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}