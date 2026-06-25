using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmInDV : Form
    {
        private DateTime ngayHetHan;

        // Constructor nhận ngày hết hạn cần in
        public FrmInDV(DateTime ngayHetHan)
        {
            InitializeComponent();
            this.ngayHetHan = ngayHetHan;
        }

        private void FrmInDV_Load(object sender, EventArgs e)
        {
            try
            {
                string ngayString = ngayHetHan.ToString("yyyy-MM-dd");
                ReportDocument rpt = new ReportDocument();
                string path = @"C:\BaiTapLonQuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\CrInDV.rpt";
                rpt.Load(path);
                rpt.SetParameterValue("@NgayHetHan", ngayString);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            // Không cần xử lý thêm
        }
    }
}