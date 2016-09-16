using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MvcApplication2.Controllers
{
    public class ReportController : ApiController
    {
      //   GET api/<controller>
        [HttpGet]
        public async Task<HttpResponseMessage> GetPDFReport()
        {
            string fileName = string.Concat("ReportJv.pdf");
            string filePath = HttpContext.Current.Server.MapPath("~/Report/" + fileName);
            UsersContext2 db = new UsersContext2();
            List<TransMain> TransMains = db.TransMains.Where(p => p.TypeMasterId == 1).ToList();
            
            await MvcApplication2.Report.ReportGenerator.GeneratePDF(TransMains, filePath);

            HttpResponseMessage result = null;
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new FileStream(filePath, FileMode.Open));
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = fileName;

            return result;
        }
    }
}
