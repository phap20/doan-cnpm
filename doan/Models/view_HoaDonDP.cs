﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace doan.Models
{

    [Table("view_HoaDonDP")]
    public class view_HoaDonDP
    {
        [Key]
        public int ID { get; set; }
        public int IDPhong { get; set; }
        public int IDNguoiDung { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string? YeuCau { get; set; }
        public int? ThanhTien { get; set; }

        /*
        Các trạng thái:
            -1: Phòng bị hủy
             0: Đang trong trạng thái chờ
             1: Đơn đặt phòng thành công
             2: Đơn đặt phòng đã được nhân viên duyệt, khách hàng đang sử dụng
        */  
        public int? TrangThai { get; set; }
        public string? GhiChu { get; set; }
        public int? Gia { get; set; }

        public string LayTrangThaiStr()
        {
            switch (this.TrangThai)
            {
                case -1:
                    return "Đơn đặt phòng đã bị hủy";
                case 0:
                    return "Đang chờ checkin";
                case 1:
                    return "Đơn đặt phòng thành công";
                case 2:
                    return "Đơn đặt phòng đã duyệt";
                default:
                    return "Trạng thái không xác định";
            }
        }

        public string? HoTen { get; set; }
        public string? DiaChi { get; set; }
        public string? SoDienThoai { get; set; }
        public string? TenPhong { get; set; }
        public int? SoGiuong { get; set; }
        public int? SoBonTam { get; set; }
        public bool? Wifi { get; set; }
        public string? MoTa { get; set; }
        public string? TenLoaiPhong { get; set; }
        public string? TenTang { get; set; }
    }

}