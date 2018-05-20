using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectQLQuanCafe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                e.Cancel = true;
            }
            */
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            /*
            if (txtTaiKhoan.Text=="" || txtMatKhau.Text=="")
            {
                MessageBox.Show("Tài khoản và mật khẩu là bắt buộc", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else if (txtTaiKhoan.Text == "admin" && txtMatKhau.Text == "123456")
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                fTableManager fTableManager = new fTableManager();
                this.Hide();
                fTableManager.ShowDialog();
                this.Show();
            } else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
            fQuanLyDatMon fTableManager = new fQuanLyDatMon();
            this.Hide();
            fTableManager.ShowDialog();
            this.Show();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
