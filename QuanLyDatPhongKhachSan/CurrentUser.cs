namespace QuanLyDatPhongKhachSan
{
    public static class CurrentUser
    {
        public static int MaTK { get; set; }
        public static string TenDangNhap { get; set; }
        public static int VaiTro { get; set; }   // 1: Quản lý, 2: Nhân viên, 3: Khách hàng
        public static int? MaNV { get; set; }     // null nếu là khách hàng
        public static int? MaKH { get; set; }     // null nếu là nhân viên
        public static string HoTen { get; set; }

        public static bool IsLoggedIn => MaTK > 0;

        public static string Loai { get; internal set; }

        public static void Logout()
        {
            MaTK = 0;
            TenDangNhap = "";
            VaiTro = 0;
            MaNV = null;
            MaKH = null;
            HoTen = "";
        }
    }
}