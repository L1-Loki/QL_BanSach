using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Areas.Admin.Controllers
{
    public class Admin_SachController : Controller
    {
        // GET: Admin/Admin_Sach
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {
            var list_ADS = db.SACHes.ToList();
            return View(list_ADS);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB");
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Tensach ,Giaban ,Mota ,Anhbia ,Ngaycapnhat ,Soluongton ,MaCD ,MaNXB, MaTG")] SACH s, HttpPostedFileBase file)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB");
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG");

            if (file == null)
            {
                ViewBag.Thongbao = "Chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        file.SaveAs(path);
                    }
                    s.Anhbia = filename;
                    db.SACHes.Add(s);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            SACH s = db.SACHes.SingleOrDefault(n => n.Masach == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Masach = s.Masach;
            return View(s);
        }
        public ActionResult Delete(int? id)
        {
            SACH s = db.SACHes.SingleOrDefault(n => n.Masach == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Masach = s.Masach;
            db.SACHes.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SACH s = db.SACHes.SingleOrDefault(n => n.Masach == id);
            if (s == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Masach = s.Masach;
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB");
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG");
            return View(s);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(SACH s, HttpPostedFileBase file)
        {

            if (s != null || file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    file.SaveAs(path);
                }
                s.Anhbia = filename;
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}