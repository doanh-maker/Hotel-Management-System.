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
    public partial class FrmSuDungDichVuNV : Form
    {
        public FrmSuDungDichVuNV()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachDichVu();
        }
        private void LoadDanhSachDichVu()
        {
            SqlParameter[] p = {new SqlParameter("@TuKhoa", txtTimKiem.Text.Trim() ?? (object)DBNull.Value) };
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemDonDangThue", p);
            dgvDanhSachDonDatCanSuDungDV.DataSource = dt;
            dgvDanhSachDonDatCanSuDungDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachDonDatCanSuDungDV.ReadOnly = true;
            if (dgvDanhSachDonDatCanSuDungDV.Columns["btnChon"] == null)
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "btnChon";
                    btn.HeaderText = "Thao tác";
                    btn.Text = "Sử dụng dịch vụ";
                    btn.UseColumnTextForButtonValue = true;
                    dgvDanhSachDonDatCanSuDungDV.Columns.Add(btn);
            }
        }

        private void dgvDanhSachDonDatCanSuDungDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0) return;
            if(dgvDanhSachDonDatCanSuDungDV.Columns[e.ColumnIndex].Name == "btnChon")
            {
                var cellValue = dgvDanhSachDonDatCanSuDungDV.Rows[e.RowIndex].Cells["MaDP"].Value;
                if (cellValue == null || cellValue == DBNull.Value)
                    return;
                if(!int.TryParse(cellValue.ToString(), out int maDP))
                {
                   
                    return;
                }
                FrmSuDungDichVu frmSuDungDichVu = new FrmSuDungDichVu(maDP);
                frmSuDungDichVu.ShowDialog();

            }
        }

        private void FrmSuDungDichVuNV_Load(object sender, EventArgs e)
        {
            LoadDanhSachDichVu();
        }
    }
}