using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace QL_BanSach.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: DanhMucSP
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index(int id)
        {
           
           var tg= db.SACHes.Where(n => n.MaTG == id).ToList();
            return View(tg);
        }
        public ActionResult ChuDe(int id)
        {
            var listCD = db.SACHes.Where(n => n.MaCD == id).ToList();

            return View(listCD);
        }
        private List<SACH> LaySachMoi(int count)
        {
            return db.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult phantrang(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var listSachMoi = LaySachMoi(15);
            return View(listSachMoi.ToPagedList(pageNumber, pageSize));
        }

    }
}