using System;

using System.Data;
using System.Data.SqlClient;

using System.Windows.Forms;


namespace QuanLyDatPhongKhachSan
{
    public partial class FrmDatCocKH : Form
    {
        private int maKhachHang;
        private int maPhong;
        private DateTime tuNgay;
        private DateTime denNgay;
        private decimal tongTien;
        public FrmDatCocKH(int maKhachHang, int maPhong, DateTime tuNgay, DateTime denNgay, decimal tongTien)
        {
            InitializeComponent();
            this.maKhachHang = maKhachHang;
            this.maPhong = maPhong;
            this.tuNgay = tuNgay;
            this.denNgay = denNgay;
            this.tongTien = tongTien;
        }
        private void FrmDatCoc_Load(object sender, EventArgs e)
        {
            // hiển thị số tiền đặt cọc bằng 
             decimal tongtien= tongTien  * 0.3m;
 lblDatCoc.Text=tongtien.ToString();
            
        }

        private void btnDatCoc_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = new SqlParameter[]
              {
                new SqlParameter("@MaKH", SqlDbType.Int) { Value = maKhachHang },
                new SqlParameter("@NgayNhan", SqlDbType.Date) { Value = tuNgay },
                new SqlParameter("@NgayTra", SqlDbType.Date) { Value = denNgay },
                new SqlParameter("@MaPhong", SqlDbType.Int) { Value = maPhong },
                new SqlParameter("MaDP", SqlDbType.Int) { Direction = ParameterDirection.Output }
              };
            int ketQua = DatabaseHelper.ExecuteNonQuery("sp_ThemDatPhong", parameters, CommandType.StoredProcedure);
            if (parameters[4].Value != null && parameters[4].Value != DBNull.Value)
            {
               int maDatPhong = Convert.ToInt32(parameters[4].Value);
                MessageBox.Show("Đặt cọc thành công! Mã đặt phòng: " + maDatPhong, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                DialogResult di = MessageBox.Show(
                    $"Bạn có muốn in phiếu đặt cọc không?",
                    "In phiếu đặt cọc",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (di == DialogResult.Yes)
                {
                    

                    FrmPhieuDatCoc frmPhieuDatCoc=new FrmPhieuDatCoc(maDatPhong);
                    frmPhieuDatCoc.ShowDialog();
                }
                else
                {
                    // bỏ qua
                }
            }
            else
            {
                MessageBox.Show("Đặt cọc thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
