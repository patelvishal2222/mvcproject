using MvcApplication2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/
        UsersContext2 db = new UsersContext2();
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }


        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Detail(int Id)
        {
            Image image = db.Images.Find(Id);
            if(image==null)
            {
                return HttpNotFound(); 

            }

            return View(image);
        }

        [HttpPost ]
        public ActionResult Create(Image img, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                          + file.FileName);
                    img.ImagePath = file.FileName;
                }
                db.Images.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(img);
        }

    }
}
