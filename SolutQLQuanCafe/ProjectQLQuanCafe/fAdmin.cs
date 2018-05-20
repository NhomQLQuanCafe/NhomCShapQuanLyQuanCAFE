using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ProjectQLQuanCafe.DAL;
using ProjectQLQuanCafe.BLL;

namespace ProjectQLQuanCafe
{
    public partial class fAdmin : Form
    {
        //BindingSource foodList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            LoadAllData();
        }
        private void LoadAllData()
        {
            DoanhThuLoadDateTimePicker();
            DoanhThuLoadList(dtpNgayBD.Value, dtpNgayKT.Value);
            
            MonAnLoadList();
            //dgvMonAn.DataSource = foodList;
            //MonAnClickDataBinding();
            MonAnLoadDanhMuc(cmbMonAnDanhMuc);

            DanhMucLoadList();

            LoadBanAn();
            LoadTaiKhoan();
        }



        // ---------- MonAn Load list Món ăn
        AdminMonAn monAn = new AdminMonAn();
        private void btnMonAnXem_Click(object sender, EventArgs e)
        {
            MonAnLoadList();
        }
        void MonAnLoadList()
        {
            DataTable dt = new DataTable();
            dt = monAn.GetListMonAn();
            dgvMonAn.DataSource = dt;
        }
        void MonAnLoadDanhMuc(ComboBox cmb)
        {
            cmb.DataSource = danhMuc.GetListdanhMuc();
            cmb.DisplayMember = "Name";
        }
        // Click DataGridView: DataBinding
        //void MonAnClickDataBinding()
        //{
        //    txtMonAnID.DataBindings.Add(new Binding("Text", dgvMonAn.DataSource, "Mã món", true, DataSourceUpdateMode.Never));
        //    txtMonAnTenMon.DataBindings.Add(new Binding("Text", dgvMonAn.DataSource, "Tên món", true, DataSourceUpdateMode.Never));
        //    numMonAnGia.DataBindings.Add(new Binding("Value", dgvMonAn.DataSource, "Giá", true, DataSourceUpdateMode.Never));
        //    cmbMonAnDanhMuc.DataBindings.Add(new Binding("Text", dgvMonAn.DataSource, "Tên danh mục", true, DataSourceUpdateMode.Never));
        //}
        int vt = 0;
        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            try
            {
                if (vt >= 0)
                {
                    txtMonAnID.Text = dgvMonAn.Rows[vt].Cells[0].Value.ToString();
                    txtMonAnTenMon.Text = dgvMonAn.Rows[vt].Cells[1].Value.ToString();
                    numMonAnGia.Value = Convert.ToInt32(dgvMonAn.Rows[vt].Cells[2].Value.ToString());
                    cmbMonAnDanhMuc.Text = dgvMonAn.Rows[vt].Cells[3].Value.ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi Click!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ResetTextBox()
        {
            txtMonAnTenMon.Focus();
            txtMonAnTenMon.Clear();
            numMonAnGia.Value = 0;
        }
        private void btnMonAnThem_Click(object sender, EventArgs e)
        {
            if (txtMonAnTenMon.Text == "" || cmbMonAnDanhMuc.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                try
                {
                    int idFoodCategory = 0;
                    DataTable dt = new DataTable();
                    dt = danhMuc.GetIDCategoryByName(cmbMonAnDanhMuc.Text);
                    
                    DataRow dr = dt.Rows[0];
                    idFoodCategory = Convert.ToInt32(dr[0].ToString());
                    monAn.insertMonAn(txtMonAnTenMon.Text, Convert.ToInt32(numMonAnGia.Text), idFoodCategory);
                    MessageBox.Show("Thêm món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ResetTextBox();
                    MonAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi thêm", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnMonAnSua_Click(object sender, EventArgs e)
        {
            if (txtMonAnTenMon.Text == "" || cmbMonAnDanhMuc.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int idFoodCategory = 0;
                    DataTable dt = new DataTable();
                    dt = danhMuc.GetIDCategoryByName(cmbMonAnDanhMuc.Text);

                    DataRow dr = dt.Rows[0];
                    idFoodCategory = Convert.ToInt32(dr[0].ToString());
                    monAn.updateMonAn(txtMonAnID.Text, txtMonAnTenMon.Text, Convert.ToInt32(numMonAnGia.Text), idFoodCategory);
                    MessageBox.Show("Sửa món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    MonAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnMonAnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                monAn.deleteMonAn(txtMonAnID.Text);
                MessageBox.Show("Xóa món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                MonAnLoadList();
            }
            catch (Exception)
            {
                MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi xóa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnMonAnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtMonAnTimKiem.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập từ khóa tìm kiếm", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                DataTable dt = new DataTable();
                dt = monAn.searchMonAn(txtMonAnTimKiem.Text);
                dgvMonAn.DataSource = dt;
            }
        }
        // ---------- MonAn




        // ---------- DanhMuc
        AdminDanhMuc danhMuc = new AdminDanhMuc();
        private void btnDanhMucXem_Click(object sender, EventArgs e)
        {
            DanhMucLoadList();
        }
        void DanhMucLoadList()
        {
            DataTable dt = new DataTable();
            dt = danhMuc.GetListdanhMuc();
            dgvDanhMuc.DataSource = dt;
        }
        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            try
            {
                if (vt >= 0)
                {
                    txtDanhMucID.Text = dgvDanhMuc.Rows[vt].Cells[0].Value.ToString();
                    txtDanhMucTenDM.Text = dgvDanhMuc.Rows[vt].Cells[1].Value.ToString();
                }
            }
            catch (Exception){}
        }
        private void btnDanhMucThem_Click(object sender, EventArgs e)
        {
            if (txtDanhMucTenDM.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    danhMuc.insertDanhMuc(txtDanhMucTenDM.Text);
                    MessageBox.Show("Thêm danh mục thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtDanhMucTenDM.Clear();
                    DanhMucLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi thêm", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDanhMucSua_Click(object sender, EventArgs e)
        {
            if (txtDanhMucTenDM.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    danhMuc.updateDanhMuc(txtDanhMucID.Text, txtDanhMucTenDM.Text);
                    MessageBox.Show("Sửa danh mục thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    DanhMucLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDanhMucXoa_Click(object sender, EventArgs e)
        {
            try
            {
                danhMuc.deleteDanhMuc(txtDanhMucID.Text);
                MessageBox.Show("Xóa danh mục thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                DanhMucLoadList();
            }
            catch (Exception)
            {
                MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi xóa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        // ---------- DanhMuc





        // ---------- BanAn
        void LoadBanAn()
        {
            string cmdText = "Select * From FoodTable";
            DataProvider dp = new DataProvider();
            dgvBanAn.DataSource = dp.ExecuteQuery(cmdText);
        }


        // ---------- BanAn



        // ---------- TaiKhoan
        void LoadTaiKhoan()
        {
            string cmdText = "Select * From Account";
            DataProvider dp = new DataProvider();
            dgvTaiKhoan.DataSource = dp.ExecuteQuery(cmdText);
        }


        // ---------- TaiKhoan



        // ---------- DoanhThu
        // Load ra list doanh thu theo tháng
        ThongKeDoanhThu thongKe = new ThongKeDoanhThu();
        void DoanhThuLoadList(DateTime TimeCheckIn, DateTime TimeCheckOut)
        {
            DataTable dt = new DataTable();
            dt = thongKe.GetListDoanhThu(TimeCheckIn, TimeCheckOut);
            dgvDoanhThu.DataSource = dt;
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                DoanhThuLoadList(dtpNgayBD.Value, dtpNgayKT.Value);
            }
            catch (Exception)
            {
                MessageBox.Show("Rất tiếc. Đã sẩy ra lỗi gì đó!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void DoanhThuLoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpNgayBD.Value = new DateTime(today.Year, today.Month, 1);
            dtpNgayKT.Value = dtpNgayBD.Value.AddMonths(1).AddDays(-1);
        }

 







        // ---------- DoanhThu

    }
}
