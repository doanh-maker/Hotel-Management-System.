using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmSuDungDichVu : Form
    {
        private int maDP;

        public FrmSuDungDichVu()
        {
           
        }

        public FrmSuDungDichVu(int maDP)
        {
            InitializeComponent();
            this.maDP = maDP;
          

        }

        private void FrmSuDungDichVu_Load(object sender, EventArgs e)
        {
  LoadDanhSachDichVu();
            HienThiThongTinDon();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int inserted = 0;
            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (row.Cells["Chon"] is DataGridViewCheckBoxCell chk && Convert.ToBoolean(chk.Value) == true)
                {
                    int maDV = Convert.ToInt32(row.Cells["MaDV"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    if (soLuong <= 0) soLuong = 1;

                    string sql = @"IF EXISTS (SELECT 1 FROM SuDungDichVu WHERE MaDP = @MaDP AND MaDV = @MaDV)
BEGIN
    UPDATE SuDungDichVu
    SET SoLuong =  @SoLuong
    WHERE MaDP = @MaDP AND MaDV = @MaDV
END
ELSE
BEGIN
    INSERT INTO SuDungDichVu (MaDP, MaDV, SoLuong, NgaySuDung)
    VALUES (@MaDP, @MaDV, @SoLuong, @NgaySuDung)
END";
                    SqlParameter[] p = {
                    new SqlParameter("@MaDP", maDP),
                    new SqlParameter("@MaDV", maDV),
                    new SqlParameter("@SoLuong", soLuong),
                    new SqlParameter("@NgaySuDung", DateTime.Now.Date)
                };
                    try
                    {
                        DatabaseHelper.ExecuteNonQuery(sql, p);
                        inserted++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm dịch vụ {row.Cells["TenDV"].Value}: {ex.Message}");
                    }
                }
            }
            MessageBox.Show($"Đã thêm {inserted} dịch vụ thành công.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void HienThiThongTinDon()
        {
            int MaDP = maDP;
            string sql = @"
            SELECT dp.MaDP, kh.HoTen 
            FROM DatPhong dp 
            JOIN KhachHang kh ON dp.MaKH = kh.MaKH 
            WHERE dp.MaDP = @MaDP";
            SqlParameter[] parameters = {
                            new SqlParameter("@MaDP", MaDP)
                        };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                lblThongTin.Text = $"Đơn {MaDP} - Khách: {dt.Rows[0]["HoTen"]}";
            }
        }

        private void LoadDanhSachDichVu()
        {
            
           string sql = @"
SELECT dv.MaDV, dv.TenDV, dv.DonGia, dv.DonViTinh,
       ISNULL(sddv.SoLuong, 0) AS SoLuong
FROM DichVu dv
LEFT JOIN SuDungDichVu sddv 
    ON dv.MaDV = sddv.MaDV AND sddv.MaDP = @MaDP
";
            SqlParameter[] p = {
    new SqlParameter("@MaDP", maDP)
};

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);
            dgvDichVu.DataSource = dt;
            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDichVu.ReadOnly = false;
            dgvDichVu.AllowUserToAddRows = false;
            
          
            // Thêm cột checkbox chọn
            if (dgvDichVu.Columns["Chon"] == null)
            {
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.Name = "Chon";
                chk.HeaderText = "Chọn";
                chk.Width = 50;
                dgvDichVu.Columns.Insert(0, chk);
            }
            // Thêm cột số lượng (nếu chưa có)
            if (dgvDichVu.Columns["SoLuong"] == null)
            {
                DataGridViewTextBoxColumn sl = new DataGridViewTextBoxColumn();
                sl.Name = "SoLuong";
                sl.HeaderText = "Số lượng";
                sl.DefaultCellStyle.Format = "N0";
                sl.Width = 80;
                dgvDichVu.Columns.Add(sl);
            }
            // Đặt giá trị mặc định cho cột Số lượng = 1
            foreach (DataGridViewRow row in dgvDichVu.Rows)
            {
                if (row.Cells["SoLuong"].Value == null || row.Cells["SoLuong"].Value.ToString() == "")
                    row.Cells["SoLuong"].Value = 1;
            }
            // Nút +
            if (dgvDichVu.Columns["Tang"] == null)
            {
                DataGridViewButtonColumn btnTang = new DataGridViewButtonColumn();
                btnTang.Name = "Tang";
                btnTang.HeaderText = "+";
                btnTang.Text = "+";
                btnTang.UseColumnTextForButtonValue = true;
                btnTang.Width = 40;
                dgvDichVu.Columns.Add(btnTang);
            }

            // Nút -
            if (dgvDichVu.Columns["Giam"] == null)
            {
                DataGridViewButtonColumn btnGiam = new DataGridViewButtonColumn();
                btnGiam.Name = "Giam";
                btnGiam.HeaderText = "-";
                btnGiam.Text = "-";
                btnGiam.UseColumnTextForButtonValue = true;
                btnGiam.Width = 40;
                dgvDichVu.Columns.Add(btnGiam);
            }
        }

        private void lblThongTin_Click(object sender, EventArgs e)
        {

        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDichVu.Rows[e.RowIndex];
            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);

            // Nút +
            if (dgvDichVu.Columns[e.ColumnIndex].Name == "Tang")
            {
                soLuong++;
                row.Cells["SoLuong"].Value = soLuong;
            }

            // Nút -
            if (dgvDichVu.Columns[e.ColumnIndex].Name == "Giam")
            {
                if (soLuong > 1) // không cho nhỏ hơn 1
                {
                    soLuong--;
                    row.Cells["SoLuong"].Value = soLuong;
                }
            }
        }
    }
}
