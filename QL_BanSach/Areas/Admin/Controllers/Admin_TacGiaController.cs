using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Areas.Admin.Controllers
{
    public class Admin_TacGiaController : Controller
    {
        // GET: Admin/Admin_TacGia
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {
            var list_ADTG = db.TACGIAs.ToList();
            return View(list_ADTG);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Anhbia,TenTG,Diachi,Tieusu,Dienthoai")] TACGIA tg, HttpPostedFileBase file, VIETSACH vs)
        {

        
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
                    tg.Anhbia = filename;
                    db.TACGIAs.Add(tg);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            TACGIA tg = db.TACGIAs.SingleOrDefault(n => n.MaTG == id);
            if (tg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaTG = tg.MaTG;
            db.TACGIAs.Remove(tg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TACGIA tg = db.TACGIAs.SingleOrDefault(n => n.MaTG == id);
            if (tg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaTG = tg.MaTG;
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB");
            return View(tg);
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