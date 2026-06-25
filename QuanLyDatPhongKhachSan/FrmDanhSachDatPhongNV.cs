using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmDanhSachDatPhongNV : Form
    {
        public FrmDanhSachDatPhongNV()
        {
            InitializeComponent();
        }

        private void FrmDanhSachDatPhongNV_Load(object sender, EventArgs e)
        {
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("pr_hienthidanhsachdatphong_all");
            //StringBuilder sb=new StringBuilder();
            //foreach (DataRow row in dt.Rows)
            //{
            //    sb.AppendLine("Phòng: " + row["MaPhong"]);
            //    sb.AppendLine("Ngày đặt: " + row["NgayDat"]);
            //    sb.AppendLine("Ngày nhận: "  +row["NgayNhan"]);
            //    sb.AppendLine("Ngày trả: " + row["NgayTra"]);
            //    sb.AppendLine("Số người: " + row["SoLuongNguoi"]);
            //    sb.AppendLine("======================");
            //}
            //lblDanhSachDatPhong.Text = sb.ToString();
            dgvDanhSachDatPhong.DataSource = dt;
            dgvDanhSachDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachDatPhong.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvDanhSachDatPhong.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDanhSachDatPhong.ReadOnly = true;
            dgvDanhSachDatPhong.AllowUserToDeleteRows = false;
            dgvDanhSachDatPhong.AllowUserToAddRows = false;
            dgvDanhSachDatPhong.Columns["btnChiTiet"].DisplayIndex = dgvDanhSachDatPhong.Columns.Count - 1;
        }

        private void dgvDanhSachDatPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDanhSachDatPhong.Columns[e.ColumnIndex].Name == "btnChiTiet" && e.RowIndex >= 0)
            {
                int maDP = Convert.ToInt32(dgvDanhSachDatPhong.Rows[e.RowIndex].Cells["MaDP"].Value);
                string trangThai = dgvDanhSachDatPhong.Rows[e.RowIndex].Cells["TrangThaiDat"].Value.ToString();
                if (trangThai == "Đã trả phòng")
                {

                    FrmChiTietDatPhongDaThanhToan frmChiTietDatPhongDaThanhToan = new FrmChiTietDatPhongDaThanhToan(maDP);
                    frmChiTietDatPhongDaThanhToan.ShowDialog();
                }
                else
                {

                    // Mở Form chi tiết (Lưu ý: Bạn nên tạo constructor nhận MaDP bên FrmChiTietDatPhongNV)
                    FrmChiTietDatPhong frm = new FrmChiTietDatPhong(maDP);
                    frm.ShowDialog();
                }
            }
        }
    }
}
