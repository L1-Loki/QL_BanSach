using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var list = (List<GioHang_Model>)Session["GioHang"];
                DONDATHANG ddh = new DONDATHANG();
                ddh.Dathanhtoan = true;
                ddh.Tinhtranggiaohang = false;
                ddh.MaKH = int.Parse(Session["MaKH"].ToString());
                ddh.Ngaydat = DateTime.Now;
                ddh.Ngaygiao = DateTime.Now.AddDays(7);
                db.DONDATHANGs.Add(ddh);
                db.SaveChanges();

                var idDH = ddh.MaDonHang;

                List<CHITIETDONTHANG> listCT = new List<CHITIETDONTHANG>();
                foreach (var item in list)
                {
                    CHITIETDONTHANG ct = new CHITIETDONTHANG();
                    ct.Soluong = item.soluong;
                    ct.Masach= item.SACH.Masach;
                    ct.MaDonHang= idDH;
                    ct.Dongia = item.SACH.Giaban;
                    db.CHITIETDONTHANGs.AddOrUpdate(ct);

                }
                db.CHITIETDONTHANGs.AddRange(listCT);
                db.SaveChanges();
            }
            return View();
        }
    }
}