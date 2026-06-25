using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmThongTinKhachHangKH : Form
    {
        private readonly int _maPhong;
        private readonly DateTime _tuNgay;
        private readonly DateTime _denNgay;
        private readonly int _maTaiKhoan;
        private readonly decimal _tongTien;

        public FrmThongTinKhachHangKH(int maPhong, DateTime tuNgay, DateTime denNgay, int maTaiKhoan)
        {
            InitializeComponent();
            _maPhong = maPhong;
            _tuNgay = tuNgay;
            _denNgay = denNgay;
            _maTaiKhoan = maTaiKhoan;
        }

        public FrmThongTinKhachHangKH(int maPhong, DateTime tuNgay, DateTime denNgay, int maTaiKhoan, decimal tongTien)
            : this(maPhong, tuNgay, denNgay, maTaiKhoan)
        {
            _tongTien = tongTien;
        }

        private void FrmThongTinKhachHang_Load(object sender, EventArgs e)
        {
            txtTen.Focus();

            // Gắn sự kiện validate realtime
            txtTen.TextChanged += (s, ev) => ValidateTen();
            txtCCCD.TextChanged += (s, ev) => ValidateCCCD();
            txtSDT.TextChanged += (s, ev) => ValidateSDT();
            txtEmail.TextChanged += (s, ev) => ValidateEmail();
            txtDiaChi.TextChanged += (s, ev) => ValidateDiaChi();
        }

        // ================= VALIDATE =================

        private bool ValidateTen()
        {
            string hoTen = txtTen.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                errorProvider1.SetError(txtTen, "Không được để trống");
                return false;
            }

            if (!Regex.IsMatch(hoTen, @"^[\p{L} .'-]+$"))
            {
                errorProvider1.SetError(txtTen, "Tên không hợp lệ");
                return false;
            }

            errorProvider1.SetError(txtTen, "");
            return true;
        }

        private bool ValidateCCCD()
        {
            string cccd = txtCCCD.Text.Trim();

            if (!Regex.IsMatch(cccd, @"^\d{9}$|^\d{12}$"))
            {
                errorProvider1.SetError(txtCCCD, "CCCD phải 9 hoặc 12 số");
                return false;
            }

            errorProvider1.SetError(txtCCCD, "");
            return true;
        }

        private bool ValidateSDT()
        {
            string sdt = txtSDT.Text.Trim();

            if (!Regex.IsMatch(sdt, @"^0\d{9,10}$"))
            {
                errorProvider1.SetError(txtSDT, "SĐT phải 10-11 số, bắt đầu bằng 0");
                return false;
            }

            errorProvider1.SetError(txtSDT, "");
            return true;
        }

        private bool ValidateEmail()
        {
            string email = txtEmail.Text.Trim();

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                errorProvider1.SetError(txtEmail, "");
                return true;
            }
            catch
            {
                errorProvider1.SetError(txtEmail, "Email không hợp lệ");
                return false;
            }
        }

        private bool ValidateDiaChi()
        {
            string diaChi = txtDiaChi.Text.Trim();

            if (string.IsNullOrWhiteSpace(diaChi))
            {
                errorProvider1.SetError(txtDiaChi, "Không được để trống");
                return false;
            }

            errorProvider1.SetError(txtDiaChi, "");
            return true;
        }

        // ================= BUTTON =================

        private void btnTiep_Click(object sender, EventArgs e)
        {
            if (!(ValidateTen() &&
                  ValidateCCCD() &&
                  ValidateSDT() &&
                  ValidateEmail() &&
                  ValidateDiaChi()))
            {
                MessageBox.Show("Vui lòng nhập đúng thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtTen.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            int? maKhachHang = LuuThongTinKhachHang(hoTen, cccd, soDienThoai, email, diaChi);
            if (maKhachHang.HasValue)
            {
                MoFormDatCoc(maKhachHang.Value);
            }
        }

        // ================= DATABASE =================

        private int? LuuThongTinKhachHang(string hoTen, string cccd, string soDienThoai, string email, string diaChi)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaTK", SqlDbType.Int) { Value = _maTaiKhoan },
                    new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = hoTen },
                    new SqlParameter("@CCCD", SqlDbType.VarChar) { Value = cccd },
                    new SqlParameter("@SoDienThoai", SqlDbType.VarChar) { Value = soDienThoai },
                    new SqlParameter("@Email", SqlDbType.VarChar) { Value = email },
                    new SqlParameter("@DiaChi", SqlDbType.NVarChar) { Value = diaChi },
                    new SqlParameter("@MaKH", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                DatabaseHelper.ExecuteNonQuery("sp_DienThongTinKhachHang", parameters, CommandType.StoredProcedure);

                if (parameters[6].Value != DBNull.Value)
                    return Convert.ToInt32(parameters[6].Value);

                MessageBox.Show("Không lấy được mã KH!", "Lỗi");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return null;
            }
        }

        // ================= FORM =================

        private void MoFormDatCoc(int maKhachHang)
        {
          
            if (this.MdiParent is frmMain main)
            {
                main.OpenForm(new FrmDatCocKH(maKhachHang, _maPhong, _tuNgay, _denNgay, _tongTien));
            }
        }
    }
}