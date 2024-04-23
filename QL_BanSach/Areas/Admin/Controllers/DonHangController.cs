using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {

            var dh = db.DONDATHANGs.Where(x => x.Tinhtranggiaohang == false).ToList();

            return View(dh);
        }
        /*      public ActionResult Details(int id)
              {
                  if (id == null)
                  {
                      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                  }
                  DONDATHANG pb = db.DONDATHANGs.Find(id);
                  if (pb == null)
                  {
                      return HttpNotFound();
                  }
                  return View(pb);
              }
              [HttpGet]
              public ActionResult Edit(int id)
              {
                  DONDATHANG d = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
                  if (d == null)
                  {
                      Response.StatusCode = 404;
                      return null;
                  }
                  ViewBag.MaDH = d.MaDonHang;
                  return View(d);
              }
              [HttpPost]
              public ActionResult Edit(DONDATHANG dh)
              {

                  if (ModelState.IsValid)
                  {

                      var existing = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == dh.MaDonHang);
                      if (existing != null)
                      {
                          dh.Tinhtranggiaohang = true;
                          db.DONDATHANGs.AddOrUpdate(dh);
                      }
                      else
                      {
                          db.DONDATHANGs.AddOrUpdate(dh);
                      }
                      db.SaveChanges();
                      return RedirectToAction("Index");
                  }
                  return View(dh);
              }*/
        /*    [HttpGet]
            public ActionResult update(int id)
            {
                DONDATHANG d = db.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
                if (d == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.MaDH = d.MaDonHang;
                return View(d);
            }*/

        public ActionResult update(int id)
        {
            var order = db.DONDATHANGs.Find(id);
            if (order != null)
            {
                order.Tinhtranggiaohang = true;
                db.DONDATHANGs.AddOrUpdate();
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}