using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_BanSach.Models
{
    public class Home_Data
    {
        [Display(Name = "Mã sách")]
        public int Masach { get; set; }
        [Display(Name = "Sách")]
        public string Tensach { get; set; }
        [Display(Name = "Giá")]
        public Nullable<decimal> Giaban { get; set; }
        [Display(Name = "Mô tả")]
        [AllowHtml]
        [DisplayFormat(HtmlEncode = false)]
        [DataType(DataType.MultilineText)]
        public string Mota { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Anhbia { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> Ngaycapnhat { get; set; }
        [Display(Name = "Số lượng")]
        public Nullable<int> Soluongton { get; set; }
        [Display(Name = "Chủ đề")]
        public int MaCD { get; set; }
        [Display(Name = "Nhà xuất bản")]
        public int MaNXB { get; set; }
    }

    public class ChuDe_Data
    {
        [Display(Name = "Chủ đề")]
        public int MaCD { get; set; }
        [Display(Name = "Chủ đề")]
        public string TenChuDe { get; set; }
    }
    public class NXB_Data
    {
        [Display(Name = "Nhà xuất bản")]
        public int MaNXB { get; set; }
        [Display(Name = "Nhà xuất bản")]
        public string TenNXB { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Diachi { get; set; }
        [Display(Name = "Số điện thoại")]
        public string DienThoai { get; set; }
    }
    public class TacGia_Data
    {
        [Display(Name = "Tác Giả")]
        public int MaTG { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Anhbia { get; set; }
        [Display(Name = "Tác giả")]
        public string TenTG { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Diachi { get; set; }
        [Display(Name = "Tiểu sử")]
        public string Tieusu { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Dienthoai { get; set; }
    }
}