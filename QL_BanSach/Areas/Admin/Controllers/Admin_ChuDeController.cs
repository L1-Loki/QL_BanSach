using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Areas.Admin.Controllers
{
    public class Admin_ChuDeController : Controller
    {
        // GET: Admin/Admin_ChuDe
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {
            var list_ADCD = db.CHUDEs.ToList();
            return View(list_ADCD);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenChuDe")] CHUDE cd)
        {
            db.CHUDEs.Add(cd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            CHUDE cd = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = cd.MaCD;
            db.CHUDEs.Remove(cd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            CHUDE cd = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = cd.MaCD;
            return View(cd);
        }
        [HttpPost]
        public ActionResult Edit(CHUDE cd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
       
    }
}