using MvcApplication2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class EmpController : Controller
    {
        //
        // GET: /Emp/
        UsersContext2 db = new UsersContext2();

        public ActionResult  Index()
        {

            List<Emp> EmpList = db.Emps.ToList();



           return View(EmpList);

        }
        public ActionResult IndexAngularJs()
        {

            return View();
        }
        public JsonResult GetAll()
        {


            List<Emp> Empes = db.Emps.ToList();
            List<EmpBase> Empes2 = new List<EmpBase>();
            foreach (Emp emp in Empes)
            {
                EmpBase emp2 = new EmpBase();
                emp2.EmpId = emp.EmpId;
                emp2.EmpName = emp.EmpName;
                emp2.Amount = emp.Amount;
                emp2.ImagePath = emp.ImagePath;
 
                Empes2.Add(emp2); 
            
            }
            return Json(Empes2, JsonRequestBehavior.AllowGet);
                
            
            
        }
        public JsonResult GetEmp(int id = 0)
        {
            Emp emp = db.Emps.Find(id);
            if (emp == null)
            {

                return null;
            }

            return Json(emp);

        }

        public ActionResult Create()
        {

            List<SelectListItem> list = new List<SelectListItem>() {
                new SelectListItem(){ Value="1", Text="ActionScript"},
                new SelectListItem(){ Value="2", Text="AppleScript"},
                new SelectListItem(){ Value="3", Text="Asp"},
                new SelectListItem(){ Value="4", Text="BASIC"},
                new SelectListItem(){ Value="5", Text="C"},
                new SelectListItem(){ Value="6", Text="C++"},
                new SelectListItem(){ Value="7", Text="Clojure"},
                new SelectListItem(){ Value="8", Text="COBOL"},
                new SelectListItem(){ Value="9", Text="ColdFusion"},
                new SelectListItem(){ Value="10", Text="Erlang"},
                new SelectListItem(){ Value="11", Text="Fortran"},
                new SelectListItem(){ Value="12", Text="Groovy"},
                new SelectListItem(){ Value="13", Text="Haskell"}, 
                new SelectListItem(){ Value="14", Text="instinctcoder.com"},
                new SelectListItem(){ Value="15", Text="Java"},
                new SelectListItem(){ Value="16", Text="JavaScript"},
                new SelectListItem(){ Value="17", Text="Lisp"},
                new SelectListItem(){ Value="18", Text="Perl"},
                new SelectListItem(){ Value="19", Text="PHP"},
                new SelectListItem(){ Value="20", Text="Python"},
                new SelectListItem(){ Value="21", Text="Ruby"},
                new SelectListItem(){ Value="22", Text="Scala"},
                new SelectListItem(){ Value="23", Text="Scheme"},
            };
            ViewBag.CityData = list;
 
            return View();
        }

          [HttpPost]
        public ActionResult Create(Emp emp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/UpLoad/")
                                                          + file.FileName);
                    emp.ImagePath = file.FileName;
                }
                db.Emps.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
          public string AddUpdateEmp(Emp Emp)
          {
              String str = string.Empty;
              if (Emp != null)
              {
                  db.Emps.Add(Emp);
                  if(Emp.EmpId==0)
                  {
                      str="Add Record";

                  }
                  else
                  {
                      str = "Update Record";
                      db.Entry(Emp).State = EntityState.Modified;
                  }
                  
                      db.SaveChanges();
                      return str;
                  
              }
              else
              {
                  return "Invalid Employee";
              }
          }
        public ActionResult Edit(int id=0)
          {
              Emp emp = db.Emps.Find(id);

            if(emp==null)
            {

                return HttpNotFound();
            }


            List<SelectListItem> list = new List<SelectListItem>() {
                new SelectListItem(){ Value="1", Text="ActionScript"},
                new SelectListItem(){ Value="2", Text="AppleScript"},
                new SelectListItem(){ Value="3", Text="Asp"},
                new SelectListItem(){ Value="4", Text="BASIC"},
                new SelectListItem(){ Value="5", Text="C"},
                new SelectListItem(){ Value="6", Text="C++"},
                new SelectListItem(){ Value="7", Text="Clojure"},
                new SelectListItem(){ Value="8", Text="COBOL"},
                new SelectListItem(){ Value="9", Text="ColdFusion"},
                new SelectListItem(){ Value="10", Text="Erlang"},
                new SelectListItem(){ Value="11", Text="Fortran"},
                new SelectListItem(){ Value="12", Text="Groovy"},
                new SelectListItem(){ Value="13", Text="Haskell"}, 
                new SelectListItem(){ Value="14", Text="instinctcoder.com"},
                new SelectListItem(){ Value="15", Text="Java"},
                new SelectListItem(){ Value="16", Text="JavaScript"},
                new SelectListItem(){ Value="17", Text="Lisp"},
                new SelectListItem(){ Value="18", Text="Perl"},
                new SelectListItem(){ Value="19", Text="PHP"},
                new SelectListItem(){ Value="20", Text="Python"},
                new SelectListItem(){ Value="21", Text="Ruby"},
                new SelectListItem(){ Value="22", Text="Scala"},
                new SelectListItem(){ Value="23", Text="Scheme"},
            };
            ViewBag.CityData = list;
              return View(emp);

          }

        public ActionResult Details(int id=0)
        {
            Emp emp = db.Emps.Find(id);
            if(emp==null)
            {

                return HttpNotFound();
            }
            return View(emp);

        }

        public ActionResult Delete(int id=0)
        {
            Emp emp = db.Emps.Find(id);
            if(emp==null)
            {
                return HttpNotFound();

            }
            return View(emp);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            Emp emp = db.Emps.Find(id);
            db.Emps.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(Emp emp)
        {
        try
        {
            
            //if (ModelState.IsValid)
            {
             //   var oldEmp = db.Emps.Find(emp.EmpId);
                //if (oldEmp != null)
                //{
                //    var attachedEntry = db.Entry(oldEmp);

                //    attachedEntry.CurrentValues.SetValues(emp);
                //}
                //else
                //{

                //    db.Emps.Add(emp);
                //}

              //  var PhonesSub = db.Phones.Where(p => p.EmpId == emp.EmpId);

                //foreach (Phone ss in PhonesSub)
                //{

                //    var test = emp.phones.Where(p => p.PhoneId == ss.PhoneId);
                //    if (test.Count() == 0)
                //    {
                //        db.Phones.Remove(ss);
                //    }
                //}

                //foreach (Phone newPhone in emp.phones)
                //{


                //    if (newPhone.PhoneId == 0)
                //    {
                //        db.Entry(newPhone).State = EntityState.Added;
                //        db.Phones.Add(newPhone);

                //    }
                //    else
                //    {
                //        var oldPhone = db.Phones.Find(newPhone.PhoneId );
                //        var attachedEntry = db.Entry(oldPhone);

                //        attachedEntry.CurrentValues.SetValues(newPhone);
                        


                //    }

                //}
                //emp.phones.Clear();  
               // db.Emps.Attach(emp); 
                //db.Entry(emp).State = EntityState.Modified;
               // db.Emps.Attach(emp);
//                System.Collections.Generic.ICollection <string> a=  new List<string>();
              //  a.Add("Phones");
               

            //  db.Entry(oldEmp).CurrentValues.SetValues(emp);
              //  db.Entry(oldEmp).Property("Phones").IsModified = true;
               // UsersContext2.SetProperties(emp, oldEmp,a);
              //  emp.phones.Clear(); 
                //db.DetailUpdate<Emp,Phone >(emp,"phones");
               db.Entry(emp).State = EntityState.Modified; 
                db.SaveChanges();
                 // If Sucess== 1 then Save/Update Successfull else there it has Exception
                return Json(new { Success = 1, EmpId = emp.EmpId, ex = "" });
                }
            //else
            //{
            //    return Json(new { Success = 0, ex ="Error" });
            //}
            }
            catch (Exception ex) 
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

        }
       

        public ActionResult PhoneDetails(List<Phone>  Phones )
        {

            return PartialView("PhoneDetails", Phones);
        }


    }
}
