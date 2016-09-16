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
    public class AccountDataController : ApiController
    {
        private UsersContext2 db = new UsersContext2();

        // GET api/AccountData
        public IEnumerable<AccountMaster> GetAccountMasters()
        {
            var accounts = db.Accounts.Include(a => a.GroupMaster);
            return accounts.AsEnumerable();
        }

        // GET api/AccountData/5
        public AccountMaster GetAccountMaster(int id)
        {
            AccountMaster accountmaster = db.Accounts.Find(id);
            if (accountmaster == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return accountmaster;
        }

        // PUT api/AccountData/5
        public HttpResponseMessage PutAccountMaster(int id, AccountMaster accountmaster)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != accountmaster.AccountMasterId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(accountmaster).State = EntityState.Modified;

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

        // POST api/AccountData
        public HttpResponseMessage PostAccountMaster(AccountMaster accountmaster)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(accountmaster);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, accountmaster);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = accountmaster.AccountMasterId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AccountData/5
        public HttpResponseMessage DeleteAccountMaster(int id)
        {
            AccountMaster accountmaster = db.Accounts.Find(id);
            if (accountmaster == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Accounts.Remove(accountmaster);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, accountmaster);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}