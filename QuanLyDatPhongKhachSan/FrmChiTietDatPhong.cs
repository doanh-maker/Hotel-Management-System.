using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmChiTietDatPhong : Form
    {
        private int maDP;
        public FrmChiTietDatPhong(int maDP)
        {
            InitializeComponent();
            this.maDP = maDP;
        }

        private void FrmChiTietDatPhongNV_Load(object sender, EventArgs e)
        {
            SqlParameter[] parameters = { new SqlParameter("@MaDP", maDP) };
            DataTable dt=DatabaseHelper.ExecuteStoredProcedure("sp_ThongTinDatPhong", parameters);
            if(dt.Rows.Count >0)
            {
                DataRow row=dt.Rows[0];
                lblMaDP.Text = row["MaDP"].ToString();
                lblNgayDat.Text = Convert.ToDateTime(row["NgayDat"]).ToString("dd/MM/yyyy HH:mm");
                lblNgayNhan.Text = Convert.ToDateTime(row["NgayNhan"]).ToString("dd/MM/yyyy");
                lblNgayTra.Text = Convert.ToDateTime(row["NgayTra"]).ToString("dd/MM/yyyy");
                lblTrangThai.Text = row["TrangThaiDat"].ToString();
                lblGhiChu.Text = row["GhiChu"].ToString();
                lblHoTen.Text = row["HoTen"].ToString();
                lblCCCD.Text = row["CCCD"].ToString();
                lblSoDienThoai.Text = row["SoDienThoai"].ToString();
                lblEmail.Text = row["Email"].ToString();
                lblDiaChi.Text = row["DiaChi"].ToString();

                // Các label tổng tiền
                decimal tongPhong = Convert.ToDecimal(row["TongTienPhong"]);
                decimal tongDV;
                if (row["TongTienDichVu"] != DBNull.Value)
                {
                     tongDV = Convert.ToDecimal(row["TongTienDichVu"]);
                }
                else
                {
                     tongDV = 0;
                }

                    decimal tongCoc = Convert.ToDecimal(row["TongDaCoc"]);
                decimal tongChiPhi = Convert.ToDecimal(row["TongChiPhiPhatSinh"]);
                decimal tongCong = tongPhong + tongChiPhi+tongDV;
                decimal conNo = tongCong - tongCoc;

                lblTongTienPhong.Text = $"{tongPhong:N0} VNĐ";
                lblTongTienDichVu.Text = $"{tongDV:N0} VNĐ";
                lblTongChiPhi.Text = $"{tongChiPhi:N0} VNĐ";
                lblTongCong.Text = $"{tongCong:N0} VNĐ";
                lblDatCoc.Text = $"{tongCoc:N0} VNĐ";
                lblConNo.Text = $"{conNo:N0} VNĐ";
            }
        }
    }
}
