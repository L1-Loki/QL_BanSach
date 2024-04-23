using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Areas.Admin.Controllers
{
    public class Admin_NXBController : Controller
    {
        // GET: Admin/Admin_NXB
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {
            var list_ADNXB = db.NHAXUATBANs.ToList();
            return View(list_ADNXB);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenNXB,Diachi,DienThoai")] NHAXUATBAN nxb)
        {
            db.NHAXUATBANs.Add(nxb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            NHAXUATBAN nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNXB = nxb.MaNXB;
            db.NHAXUATBANs.Remove(nxb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            NHAXUATBAN nxb = db.NHAXUATBANs.SingleOrDefault(n => n.MaNXB == id);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaNXB = nxb.MaNXB;
            return View(nxb);
        }
        [HttpPost]
        public ActionResult Edit(NHAXUATBAN nxb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nxb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}