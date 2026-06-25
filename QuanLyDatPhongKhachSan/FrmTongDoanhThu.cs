using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmTongDoanhThu : Form
    {
        public FrmTongDoanhThu()
        {
            InitializeComponent();
        }

        private void FrmTongDoanhThu_Load(object sender, EventArgs e)
        {
            // Disable ban đầu
            dtpTuNgay.Enabled = false;
            dtpDenNgay.Enabled = false;
            dtpTrongNgay.Enabled = false;
        }

        private void LoadReport()
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                string path = @"C:\BaiTapLonQuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\prtThongKeDoanhThu.rpt";
                rpt.Load(path);

                string hoTen = CurrentUser.HoTen;

                // 👉 Truyền STRING
                if (radKhoangNgay.Checked)
                {
                    rpt.SetParameterValue("@TuNgay", dtpTuNgay.Value.ToString("yyyy-MM-dd"));
                    rpt.SetParameterValue("@DenNgay", dtpDenNgay.Value.ToString("yyyy-MM-dd"));
                    rpt.SetParameterValue("@TrongNgay", "");
                }
                else if (radTrongNgay.Checked)
                {
                    rpt.SetParameterValue("@TuNgay", "");
                    rpt.SetParameterValue("@DenNgay", "");
                    rpt.SetParameterValue("@TrongNgay", dtpTrongNgay.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    rpt.SetParameterValue("@TuNgay", "");
                    rpt.SetParameterValue("@DenNgay", "");
                    rpt.SetParameterValue("@TrongNgay", "");
                }

                rpt.SetParameterValue("@NguoiLapPhieu", hoTen);

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load report: " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (!radKhoangNgay.Checked && !radTrongNgay.Checked)
            {
                MessageBox.Show("Vui lòng chọn kiểu lọc!");
                return;
            }

            if (radKhoangNgay.Checked)
            {
                if (dtpDenNgay.Value.Date < dtpTuNgay.Value.Date)
                {
                    MessageBox.Show("Ngày đến phải lớn hơn hoặc bằng ngày từ");
                    return;
                }
            }

            LoadReport();
        }

        private void radKhoangNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (radKhoangNgay.Checked)
            {
                dtpTuNgay.Enabled = true;
                dtpDenNgay.Enabled = true;
                dtpTrongNgay.Enabled = false;

                dtpTuNgay.Value = DateTime.Now;
                dtpDenNgay.Value = DateTime.Now;
            }
        }

        private void radTrongNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (radTrongNgay.Checked)
            {
                dtpTuNgay.Enabled = false;
                dtpDenNgay.Enabled = false;
                dtpTrongNgay.Enabled = true;

                dtpTrongNgay.Value = DateTime.Now;
            }
        }
    }
}