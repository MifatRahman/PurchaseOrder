
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PurchaseOrder.Models;

namespace PurchaseOrder.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Purchaseorder> odrlist = db.Purchaseorders.ToList<Purchaseorder>();
                return Json(new { data = odrlist }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Purchaseorder());
            else 
            {
                using (DBModel db = new DBModel()) 
                {
                    return View(db.Purchaseorders.Where(x=>x.RefID==id).FirstOrDefault<Purchaseorder>());
                }
            }
            
        }
        [HttpPost]
        public ActionResult AddOrEdit(Purchaseorder emp)
        {
            using (DBModel db = new DBModel())
            {
                if (emp.RefID == 0)
                {
                    db.Purchaseorders.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Save Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                else 
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Update Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                
            }

        }
        [HttpPost]
        public ActionResult Delete(int id) 
        {
            using (DBModel db = new DBModel())
            {
                Purchaseorder emp = db.Purchaseorders.Where(x => x.RefID == id).FirstOrDefault<Purchaseorder>();
                db.Purchaseorders.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Delete Sucessfully" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult NewItem() 
        {
            return View();
        }
    }
}