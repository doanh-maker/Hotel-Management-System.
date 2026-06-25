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
    public partial class FrmHoaDon : Form
    {
       

        public FrmHoaDon()
        {
            InitializeComponent();
        }

       
        private void dgvDanhSachHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if(dgvDanhSachHoaDon.Columns[e.ColumnIndex].Name == "btnXemChiTietVaInHoaDon")
            {
               var cellValue = dgvDanhSachHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                    return;
                if (!int.TryParse(cellValue.ToString(), out int maHD))
                    return;
             FrmReportHoaDon frmReport = new FrmReportHoaDon(maHD);
                frmReport.ShowDialog();
            }
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            string sql="sp_LayDanhSachHoaDon";
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure(sql);
            dgvDanhSachHoaDon.DataSource = dt;
                dgvDanhSachHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhSachHoaDon.ReadOnly = true;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btnXemChiTietVaInHoaDon";
            btn.HeaderText = "Xem chi tiết và in hoá đơn";
            btn.Text = "Xem và in";
            btn.UseColumnTextForButtonValue = true;
            dgvDanhSachHoaDon.Columns.Add(btn);
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = string.IsNullOrWhiteSpace(txtTimKiem.Text) ? null : txtTimKiem.Text.Trim();
            LoadDanhSach(tuKhoa);
        }
        private void LoadDanhSach(string tuKhoa = null)
        {
            SqlParameter[] p = { new SqlParameter("@TuKhoa", tuKhoa ?? (object)DBNull.Value) };
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_LayDanhSachHoaDon", p);
            dgvDanhSachHoaDon.DataSource = dt;
            dgvDanhSachHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachHoaDon.ReadOnly = true;
        }
    }
}
