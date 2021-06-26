using PurchaseOrder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurchaseOrder.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Purchasedetail> itmlist = db.Purchasedetails.ToList<Purchasedetail>();
                return Json(new { data = itmlist }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ItemInsert(int id = 0)
        {
            if (id == 0)
                return View(new Purchasedetail());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Purchasedetails.Where(x => x.ItemID == id).FirstOrDefault<Purchasedetail>());
                }
            }

        }
        [HttpPost]
        public ActionResult ItemInsert(Purchasedetail itm)
        {
            using (DBModel db = new DBModel())
            {
                if (itm.ItemID == 0)
                {
                    db.Purchasedetails.Add(itm);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Save Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(itm).State = EntityState.Modified;
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
                Purchasedetail itm = db.Purchasedetails.Where(x => x.ItemID == id).FirstOrDefault<Purchasedetail>();
                db.Purchasedetails.Remove(itm);
                db.SaveChanges();
                return Json(new { success = true, message = "Delete Sucessfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}