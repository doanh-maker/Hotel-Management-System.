using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmReportHoaDon : Form
    {
        private int maHD;
        

        public FrmReportHoaDon(int maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
      
        }

      

       
                private void FrmReportHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument rpt = new ReportDocument();

                string path = @"C:\BaiTapLonQuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\CrReportHoaDon.rpt";
                rpt.Load(path);

                // MAIN
                string hoTen = CurrentUser.HoTen;
                rpt.SetParameterValue("@MaHD", maHD);
                rpt.SetParameterValue("@NguoiLapHoaDon", hoTen);

                // SUBREPORT 1


                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load report: " + ex.Message);
            }

        }
            

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnIn_Click(object sender, EventArgs e)
        {
           
        }
    }
}