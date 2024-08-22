using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nhom10_QL_KARAOKE
{
    public partial class frmHoaDon : Form
    {
        SqlConnection conn;
        DataSet dataSet = new DataSet();
        SqlDataAdapter dap;
        DataColumn[] key = new DataColumn[1];
        DBConnect db = new DBConnect();

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {

            conn = new SqlConnection(ConnnentionString.Conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            LoadDGVHoaDon();
            LoadMaNV();
            LoadMaPhong();
            LoadMaHD();
        }

        void LoadDGVHoaDon()
        {
            dap = new SqlDataAdapter("select * from HOADON", conn);
            dap.Fill(dataSet, "HoaDon");
            dgvHoaDon.DataSource = dataSet.Tables["HoaDon"];
            key[0] = dataSet.Tables["HoaDon"].Columns[0];
            dataSet.Tables["HoaDon"].PrimaryKey = key;
        }
        //void LoadDGVCTHoaDon()
        //{
        //    DataTable data = db.getDataTable("Select CHITIETHD.MAHD,MAMH,SOLUONG, DONGIA, DONGIA*SOLUONG as TT from CHITIETHD");
        //    //dataGridView1.DataSource = data;
        //    DataView dataView = new DataView(data);
        //    dataView.RowFilter = "MAHD='" + maHD + "'";
        //    dgvHoaDon.DataSource = dataView;
        //}
        void LoadMaNV()
        {
            dap = new SqlDataAdapter("select * from NHANVIEN", conn);
            dap.Fill(dataSet, "NhanVien");
            cboMaNV.DataSource = dataSet.Tables["NhanVien"];
            cboMaNV.DisplayMember = "TENNV";
            cboMaNV.ValueMember = "MANV";


        }
        void LoadMaPhong()
        {
            dap = new SqlDataAdapter("select * from PHONG", conn);
            dap.Fill(dataSet, "Phong");
            cboMaPhong.DataSource = dataSet.Tables["Phong"];
            cboMaPhong.DisplayMember = "TENPHONG";
            cboMaPhong.ValueMember = "MAPHONG";
        }
        void LoadMaHD()
        {
            cboMaHD.DataSource = dataSet.Tables["HoaDon"];
            cboMaHD.DisplayMember = "MAHD";
            cboMaHD.ValueMember = "MAHD";
        }

        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            grpThongTinHD.Enabled = true;
            txtMaHD.Enabled = true;
        }

        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Enabled == true)//Them
            {
                DataRow r1 = dataSet.Tables["HoaDon"].Rows.Find(txtMaHD.Text);
                if (r1 != null)
                {
                    MessageBox.Show("Mã hóa đơn đã tồn tại!");
                    return;
                }
                SqlDataAdapter dapt = new SqlDataAdapter("select * from HOADON", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(dapt);
                DataRow r = dataSet.Tables["HoaDon"].NewRow();
                r[0] = txtMaHD.Text;
                r[1] = cboMaPhong.SelectedValue.ToString();
                r[2] = cboMaNV.SelectedValue.ToString();
                r[3] = cboMaNV.Text;
                r[4] = txtNgayBan.Value.Date;
                dataSet.Tables["HoaDon"].Rows.Add(r);
                // Cập nhật thay đổi từ DataTable vào cơ sở dữ liệu
                dapt.Update(dataSet, "HoaDon");
                // Vô hiệu hóa các nhóm và textbox sau khi thêm hóa đơn thành công
                grpThongTinHD.Enabled = false;
                txtMaHD.Enabled = false;
                // Load lại DataGridView sau khi thêm hóa đơn
                LoadDGVHoaDon();
            }
            else//Sua
            {

            }
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select * from HOADON", conn);
            string maHD = dgvHoaDon.SelectedRows[0].Cells[0].Value.ToString();
            DataRow r = dataSet.Tables["HoaDon"].Rows.Find(maHD);
            if (r == null)
            {
                return;
            }
            try
            {
                r.Delete();
                SqlCommandBuilder cmd = new SqlCommandBuilder(dapt);
                dapt.Update(dataSet, "HoaDon");
                LoadDGVHoaDon();
            }
            catch
            {
                MessageBox.Show("Dữ liệu đang được sử dụng. Không thể xóa!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHD.Focus();
                return;
            }
            SqlDataAdapter dapt = new SqlDataAdapter("select * from HOADON", conn);
            string maHD = cboMaHD.SelectedValue.ToString();
            DataRow r = dataSet.Tables["HoaDon"].Rows.Find(maHD);
            if (r == null) return;

            SqlDataAdapter dapter = new SqlDataAdapter("select * from HOADON where MAHD = '" + maHD + "'", conn);
            DataTable dt = new DataTable();
            dapter.Fill(dt);
            dgvHoaDon.DataSource = dt;
        }

        private void btnXemDS_Click(object sender, EventArgs e)
        {
            LoadDGVHoaDon();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQL_Click(object sender, EventArgs e)
        {
            frmTrangChu trangChu = new frmTrangChu();
            trangChu.Show();
            this.Hide();
        }


    }
}
