using QL_BanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_BanSach.Models
{
    public class Home_Model
    {
        public List<SACH> list_Sach { get; set; }

        public List<CHUDE> list_CD_Sach { get; set; }
        public List<TACGIA> list_TacGia { get; set; }
        public List<DONDATHANG> list_dh { get; set; }
    }
}