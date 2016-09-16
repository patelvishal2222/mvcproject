using MvcApplication2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class JvController : Controller
    {
        //
        // GET: /Jv/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }


     
        public ActionResult ExportReport()
        {
           
            try
            {
            List<TransMain> TransMains = db.TransMains.Where(p => p.TypeMasterId == 1).ToList();
            //ReportJv ReportJv = new ReportJv();
            
            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rpt_EverestList.rpt"));
            //rd.SetDataSource(allEverest);
           
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //try
            //{
            //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //    stream.Seek(0, SeekOrigin.Begin);
            //    return File(stream, "application/pdf", "EverestList.pdf");

            return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        UsersContext2 db = new UsersContext2();
        
        public JsonResult GetAll()
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<TransMain> TransMains = db.TransMains.Where(p=>p.TypeMasterId==1).ToList()  ;
            db.Configuration.ProxyCreationEnabled = true;
            return Json(TransMains,JsonRequestBehavior.AllowGet );
           
        }
        [HttpPost]
        public JsonResult GetData(int id = 0)
        {
            TransMain transMain = new TransMain(); 
            if(id>0)

            {
                transMain = db.TransMains.Find(id);
                if (transMain == null)
            {

                return null;
            }

         
        }

            JsonResult data = Json(transMain);
            return Json(transMain);

        }

       
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            if (id > 0)
            {
                TransMain ac = db.TransMains.Find(id);
                db.TransMains.Remove(ac);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public string Save(Newtonsoft.Json.Linq.JToken objectData)
        {
            TransMain TransMain = new TransMain();

            String str = string.Empty;
            if (TransMain.TransDetails!=null)
            TransMain.TransDetails.Clear();
            if (TransMain.Transcation!=null)
            {
            foreach (Transcation Transcation in TransMain.Transcation)
            {
                Transcation.AccountMaster = null;
                if (Transcation.TransDetails != null)
                foreach (TransDetails TransDetails in  Transcation.TransDetails  )
                TransDetails.ItemMaster = null;

            }
        }

            if (TransMain != null)
          
            
            
            {


                if (TransMain.TransMainId == 0)
                {
                    str = "Add Record";
                    db.TransMains.Add(TransMain);
                }
                else
                {
                    var oldTranscation = db.Transcations.Where(p => p.TransMainId == TransMain.TransMainId);

                    foreach (Transcation oldTrans in oldTranscation)
                    {

                        var TempTranscation = TransMain.Transcation.Where(p => p.TranscationId == oldTrans.TranscationId);
                        if (TempTranscation.Count() == 0)
                        {
                            db.Transcations.Remove(oldTrans);
                        }
                    }

                    foreach (Transcation newTranscation in TransMain.Transcation)
                    {


                        if (newTranscation.TranscationId == 0)
                        {
                            db.Entry(newTranscation).State = EntityState.Added;
                            newTranscation.TransMainId = TransMain.TransMainId;
                            db.Transcations.Add(newTranscation);

                        }
                        else
                        {

                            var oldTranDetails = db.TransDetails.Where(p => p.TranscationId == newTranscation.TranscationId);

                            foreach (TransDetails oldTranDet in oldTranDetails)
                            {

                                var TempTransDetails = newTranscation.TransDetails.Where(p => p.TransDetailsId == oldTranDet.TransDetailsId);
                                if (TempTransDetails.Count() == 0)
                                {
                                    db.TransDetails.Remove(oldTranDet);
                                }
                            }
                            if (newTranscation.TransDetails !=null)
                            {
                            foreach (TransDetails newTransDetails in newTranscation.TransDetails)
                            {


                                if (newTransDetails.TransDetailsId == 0)
                                {
                                    db.Entry(newTransDetails).State = EntityState.Added;
                                    
                                    newTransDetails.TransMainId = TransMain.TransMainId;
                                    newTransDetails.TranscationId = newTranscation.TranscationId;
                                    db.TransDetails.Add(newTransDetails);
                                }
                                else
                                {
                                    //var oldTranDetail = db.TransDetails.Find(newTransDetails.TransDetailsId);
                                    //var attachedEntryDetails = db.Entry(oldTranDetail);
                                    //attachedEntryDetails.CurrentValues.SetValues(newTransDetails);
                                    db.Entry(db.TransDetails.Find(newTransDetails)).CurrentValues.SetValues(newTransDetails);
                                }
                            }   
                            
                            
                            newTranscation.TransDetails.Clear();  
                          }
                            //var oldTran = db.Transcations.Find(newTranscation.TranscationId);
                            //var attachedEntry = db.Entry(oldTran);
                            //attachedEntry.CurrentValues.SetValues(newTranscation);
                            db.Entry(db.Transcations.Find(newTranscation.TranscationId)).CurrentValues.SetValues(newTranscation);


                        }

                    }
                    TransMain.Transcation.Clear();
                    
                    str = "Update Record";

                    //var oldTransMain = db.TransMains.Find(TransMain.TransMainId);
                    //var attachedEntryTransMain = db.Entry(oldTransMain);
                    //attachedEntryTransMain.CurrentValues.SetValues(TransMain);
                     db.Entry(db.TransMains.Find(TransMain.TransMainId)).CurrentValues.SetValues(TransMain); 
                }

                db.SaveChanges();
                return str;

            }
            else
            {
                return "Invalid Employee";
            }
        }
    }
}
