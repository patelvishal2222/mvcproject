using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class FinancialController : Controller
    {
        UsersContext2 db = new UsersContext2();
        //
        // GET: /Financial/

        public ActionResult Index( int id=0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {

                return RedirectToAction(Server.HtmlDecode("Create%3fId%3d" + id.ToString()));
              

               // return null;
            }
            
        }

        public ActionResult Create()
        {
            return View(); 
        }

        
        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetData()
        {
            List<TransMain> TransMains = db.TransMains.Where(p => p.TypeMasterId == 3).ToList();
            var query = from tranmain in TransMains
                        select new
                        {
                            tranmain.TransMainId,
                            tranmain.BillNo,
                            tranmain.BillDate,
                            tranmain.Remarks,
                            

                        };





            return Json(query);

        }

    }
}
