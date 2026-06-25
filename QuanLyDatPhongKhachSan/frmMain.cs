using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyDatPhongKhachSan
{
    public partial class frmMain : Form
    {
        private int maTK;

        public frmMain(int maTK)
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.maTK = maTK;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            var mdiClient = this.Controls.OfType<MdiClient>().FirstOrDefault();

            if (mdiClient != null)
            {
                string path = @"C:\BaiTapLonQuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\QuanLyDatPhongKhachSan\Image\ks.png";

                // Xoá nền cũ
                mdiClient.BackgroundImage = null;

                // Load ảnh và scale theo kích thước MDI
                using (var img = Image.FromFile(path))
                {
                    Bitmap bmp = new Bitmap(mdiClient.Width, mdiClient.Height);

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(img, 0, 0, mdiClient.Width, mdiClient.Height);
                    }

                    mdiClient.BackgroundImage = bmp;
                }

                // Không cho tile nữa
                mdiClient.BackgroundImageLayout = ImageLayout.None;

                mdiClient.Refresh();
            }
        }

        // ================== HÀM MỞ FORM CHUẨN ==================
       public void OpenForm(Form frm)
        {
            // Nếu form đã mở thì không mở lại
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == frm.GetType())
                {
                    f.Activate();
                    return;
                }
            }

            // Đóng form cũ
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

            // Mở form mới full màn hình
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        // ================== PHÂN QUYỀN ==================
        private void ApplyPermissions()
        {
           if (CurrentUser.VaiTro == 3) // Nhân viên
            {
           
            
                quảnLýĐặtPhòngToolStripMenuItem.Visible = true;
                nghiệpVụLễTânToolStripMenuItem.Visible = false;           
                báoCáoToolStripMenuItem.Visible = false;
                hoáĐơnToolStripMenuItem.Visible = false;

            }
        }

        // ================== MENU ==================

        private void mnvDangNhap_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmDangNhap());
        }

      private void đặtPhòngMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmTaoDatPhongKH(maTK));
        }

        private void danhSáchĐặtPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.VaiTro == 3)
            {
                OpenForm(new FrmDanhSachDatPhongKH(maTK));
            }
            else
            {
                OpenForm(new FrmDanhSachDatPhongNV());
            }
        }

        private void xửLýYêuCầuHuỷToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmDanhSachYeuCauHuy());
        }

        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmCheckIn());
        }

        private void checkOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            OpenForm(new FrmCheckOut());
        }

        private void quảnLýPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void xemPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmXemPhongNV_QL());
        }

        private void quảnLýDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // nếu có form thì mở
            // OpenForm(new FrmDichVu());
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void quảnLýDịchVụToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }
        private void xemPhòngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmXemPhongNV_QL());
        }

        private void xemDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void xemVàInDanhSáchHoáĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmHoaDon());
        }

        private void sửDụngDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmSuDungDichVuNV());
        }

        private void giaHạnThêmNgàyỞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmGiaHanPhong());
        }

        private void chiPhíPhátSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmChiPhiPhatSinh());
        }

        private void báoCáoDoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmTongDoanhThu());
        }

        private void quảnLýDịchVụToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new FrmQuanLyDichVu());
        }
    }
}