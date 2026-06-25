using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmDangNhap : Form
    {
        
        public FrmDangNhap()
        {
            InitializeComponent();
          
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // 🔥 Hash mật khẩu trước khi so sánh (nếu DB có hash)
           

            string sql = @"SELECT MaTK, VaiTro 
                           FROM TaiKhoan 
                           WHERE TenDangNhap = @user 
                           AND MatKhau = @pass 
                           AND TrangThai = 1";

            SqlParameter[] pars = {
                new SqlParameter("@user", username),
                new SqlParameter("@pass", password)
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, pars);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu, hoặc tài khoản bị khóa!");
                return;
            }

            DataRow row = dt.Rows[0];
            CurrentUser.MaTK = Convert.ToInt32(row["MaTK"]);
            CurrentUser.TenDangNhap = username;
            CurrentUser.VaiTro = Convert.ToInt32(row["VaiTro"]);

            // 🔹 Lấy thông tin chi tiết
            if (CurrentUser.VaiTro == 1 || CurrentUser.VaiTro == 2)
            {
                string sqlNV = "SELECT MaNV, HoTen FROM NhanVien WHERE MaTK = @tk";
                SqlParameter[] p = { new SqlParameter("@tk", CurrentUser.MaTK) };

                DataTable dtNV = DatabaseHelper.ExecuteQuery(sqlNV, p);

                if (dtNV.Rows.Count > 0)
                {
                    CurrentUser.MaNV = Convert.ToInt32(dtNV.Rows[0]["MaNV"]);
                    CurrentUser.HoTen = dtNV.Rows[0]["HoTen"].ToString();
                }
            }
            else if (CurrentUser.VaiTro == 3)
            {
                string sqlKH = "SELECT MaKH, HoTen FROM KhachHang WHERE MaTK = @tk";
                SqlParameter[] p = { new SqlParameter("@tk", CurrentUser.MaTK) };

                DataTable dtKH = DatabaseHelper.ExecuteQuery(sqlKH, p);

                if (dtKH.Rows.Count > 0)
                {
                    CurrentUser.MaKH = Convert.ToInt32(dtKH.Rows[0]["MaKH"]);
                    CurrentUser.HoTen = dtKH.Rows[0]["HoTen"].ToString();
                }
            }
             
            int maTK = CurrentUser.MaTK;
           
            frmMain frmMain = new frmMain(maTK);
            frmMain.ShowDialog();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}