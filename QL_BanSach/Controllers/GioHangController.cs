using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        QLBansachEntities db = new QLBansachEntities();
        public ActionResult Index()
        {

            return View((List<GioHang_Model>)Session["GioHang"]);
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["GioHang"] == null)
            {
                List<GioHang_Model> cart = new List<GioHang_Model>();
                cart.Add(new GioHang_Model { SACH = db.SACHes.Find(id), soluong = quantity });
                Session["GioHang"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<GioHang_Model> cart = (List<GioHang_Model>)Session["GioHang"];
              

                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    cart[index].soluong += quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new GioHang_Model { SACH = db.SACHes.Find(id), soluong = quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["GioHang"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            // Lấy giỏ hàng từ Session
            var cart = Session["GioHang"] as List<GioHang_Model>;

            if (cart != null)
            {
                // Tìm sản phẩm trong giỏ hàng
                var item = cart.SingleOrDefault(i => i.SACH.Masach == id);

                if (item != null)
                {
                    // Cập nhật số lượng sản phẩm
                    item.soluong = quantity;

                    // Tính lại tổng giá trị giỏ hàng
                    var totalPrice = cart.Sum(i => i.SACH.Giaban * i.soluong);

                    return Json(totalPrice);
                }
            }

            return HttpNotFound();
        }
        private int isExist(int id)
        {
            List<GioHang_Model> cart = (List<GioHang_Model>)Session["GioHang"];
           /* int newQuantity = int.Parse(Request["quantity"]);

            // Cập nhật số lượng sản phẩm trong giỏ hàng
            var item = cart.Find(x => x.SACH.Masach == id);
            item.soluong = newQuantity;*/
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].SACH.Masach.Equals(id))
                    return i;
             return -1;
        }

        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int Id)
        {
            List<GioHang_Model> li = (List<GioHang_Model>)Session["GioHang"];
            li.RemoveAll(x => x.SACH.Masach == Id);
            Session["GioHang"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }

}