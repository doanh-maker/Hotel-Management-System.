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
    public partial class FrmXemPhongNV_QL : Form
    {
        public FrmXemPhongNV_QL()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = string.IsNullOrWhiteSpace(txtTimKiem.Text) ? null : txtTimKiem.Text.Trim();
            LoadDanhDach(tuKhoa);
        }
        private void LoadDanhDach(string tuKhoa = null)
        {
            SqlParameter[]parameters = { new SqlParameter("@TuKhoa", tuKhoa ?? (object)DBNull.Value) };
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemPhong",parameters);
            dgvTimKiem.DataSource = dt;
            dgvTimKiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimKiem.ReadOnly = true;
            dgvTimKiem.Columns["MaPhong"].Visible = false;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmXemPhongNV_QL_Load(object sender, EventArgs e)
        {
            
            DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_TimKiemPhong");
            dgvTimKiem.DataSource = dt;
            dgvTimKiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimKiem.ReadOnly = true;
            dgvTimKiem.Columns["MaPhong"].Visible = false;

        }

        private void dgvTimKiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnQuanLyPhong_Click(object sender, EventArgs e)
        {
       
        }
    }
}
