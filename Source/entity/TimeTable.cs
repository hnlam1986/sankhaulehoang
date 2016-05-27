using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGioiSanKhau.entity
{
    public class TimeTable
    {
        public DateTime SuatDien { get; set; }
        public string ShowId { get; set; }
        public string ShowName { get; set; }
        public string TheLoai { get; set; }
        public string GiaVe { get; set; }
        public string SDT { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public DateTime NgayDien { get; set; }
        public string Link { get; set; }
        public bool CanBooking { 
            get {
                DateTime suatdien = NgayDien;
                suatdien = suatdien.AddHours(SuatDien.Hour).AddMinutes(SuatDien.Minute);
                if (suatdien >= DateTime.Now)
                {
                    return true;
                }
                return false;
                } 
        }
    }
}