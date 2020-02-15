using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTapNhom
{
    class NhanSu
    {
        private string stt, hoTen, cmnd, maNhanVien, gioiTinh, ngaySinh, queQuan, soDienThoai, boPhan, chucVu, ngayVao;
        public NhanSu()
        {

        }
        public string STT { get; set; }
        public string HoTen { get; set; }
        public string CMND { get; set; }
        public string MaNhanVien { get; set; }
        public string GioiTinh { get; set; }
        public string NgaySinh { get; set; }
        public string QueQuan { get; set; }        
        public string SoDienThoai { get; set; }
        public string BoPhan { get; set; }
        public string ChucVu { get; set; }
        public string NgayVao { get; set; }
    }
}
