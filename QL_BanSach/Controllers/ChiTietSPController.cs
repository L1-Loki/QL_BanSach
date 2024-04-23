using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Controllers
{
    public class ChiTietSPController : Controller
    {
        // GET: ChiTietSP
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult ChiTiet(int id)
        {
            var s = db.SACHes.Where(n => n.Masach == id).FirstOrDefault();
            return View(s);
        }
    }
}