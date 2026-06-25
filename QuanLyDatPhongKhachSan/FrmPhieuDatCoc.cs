using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class FrmPhieuDatCoc : Form
    {
        private int maDatPhong;

        public FrmPhieuDatCoc()
        {
            InitializeComponent();
        }

        public FrmPhieuDatCoc(int maDatPhong)
        {
            InitializeComponent();
            this.maDatPhong = maDatPhong;
        }

        private void FrmPhieuDatCoc_Load(object sender, EventArgs e)
        {
            
            try
            {
                ReportDocument rpt = new ReportDocument();

                string path = @"C:\BaiTapLonQuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\rptDatCoc.rpt";
                rpt.Load(path);

                // MAIN
                
                rpt.SetParameterValue("@MaDP",maDatPhong);
               

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
    }
}
