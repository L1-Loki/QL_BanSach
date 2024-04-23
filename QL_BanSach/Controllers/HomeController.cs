using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Controllers
{
    public class HomeController : Controller
    {
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {
            Home_Model db_home = new Home_Model();
            db_home.list_Sach = db.SACHes.ToList();
            db_home.list_CD_Sach = db.CHUDEs.ToList();
            db_home.list_TacGia = db.TACGIAs.ToList();
            return View(db_home);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string Matkhau)
        {

            if (ModelState.IsValid)
            {
                var f_password = GetMD5(Matkhau);
                var data = db.KHACHHANGs.Where(s => s.Email.Equals(email) && s.Matkhau.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().Ho + "" + data.FirstOrDefault().Ten;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["MaKH"] = data.FirstOrDefault().MaKH;
                    Session["SDT"] = data.FirstOrDefault().DienthoaiKH;
                    Session["DiaChi"] = data.FirstOrDefault().DiachiKH;
                    return RedirectToAction("Index");
                }
                if (email == "admin")
                {
                    var kh = db.KHACHHANGs.ToList();
                    Session["MaKH"] = kh.FirstOrDefault().MaKH;
                    return Redirect("~/Admin/Home/Index");
                }
                else
                {
                    ViewBag.error = "Đăng nhập lại";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(KHACHHANG _khachhang)
        {
            if (ModelState.IsValid)
            {
                var check = db.KHACHHANGs.FirstOrDefault(s => s.Email == _khachhang.Email);
                if (check == null)
                {
                    _khachhang.Matkhau = GetMD5(_khachhang.Matkhau);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KHACHHANGs.Add(_khachhang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }


            }
            return View();
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        //Logout

        public ActionResult Logout()
        {
            if (Session["MaKH"] == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Session.Clear();//remove session
            return Redirect("/");
        }

    }
}