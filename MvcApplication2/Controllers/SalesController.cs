using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class SalesController : Controller
    {
        //
        // GET: /Sales/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {

            return View();
        }

        UsersContext2 db = new UsersContext2();
        [HttpPost]
        public JsonResult GetAllItem()
        {


            List<ItemMaster> ItemMasters = db.ItemMasters.ToList();
            
            return Json(ItemMasters);



        }
        [HttpPost]
        public JsonResult GetData()
        {
            List<TransMain> TransMains = db.TransMains.Where(p=>p.TypeMasterId==2).ToList() ;
            var query = from tranmain in TransMains 
                        select      new       { 
                            tranmain.TransMainId,
                            tranmain.BillNo, tranmain.BillDate,
                            tranmain.Remarks,
                           Amount= tranmain.Transcation.First(p=>p.AccountMasterId==19).Amount ,
                           PartyName=tranmain.Transcation.First(p=>p.AccountMasterId!=19 ).AccountMaster.AccountName
   
                        };

                     



            return Json(query);

        }
    }
}
