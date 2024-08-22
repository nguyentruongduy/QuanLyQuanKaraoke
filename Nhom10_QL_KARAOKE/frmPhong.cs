using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nhom10_QL_KARAOKE
{
    public partial class frmPhong : Form
    {
        DBConnect dbConnect;

        public frmPhong()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                dbConnect.open();

                string maPhong = txtMaPhong.Text.Trim();
                string maLoaiPhong = cmbLoaiphong.SelectedValue.ToString(); // Mã loại phòng
                string trangThai = txtTrangThai.Text; // Trạng thái phòng


                if (maPhong != "") // Kiểm tra nếu txtMaPhong không rỗng
                {
                    string sql = "INSERT INTO PHONG (MAPHONG, MALP, TRANGTHAI, NGUOITAO, NGAYTAO) VALUES (@ma_phong, @ma_lp, @trang_thai, 'Admin', GETDATE())";
                    SqlCommand command = new SqlCommand(sql, dbConnect.Conn);
                    command.Parameters.AddWithValue("@ma_phong", maPhong);
                    command.Parameters.AddWithValue("@ma_lp", maLoaiPhong);
                    command.Parameters.AddWithValue("@trang_thai", trangThai);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Thêm phòng thành công!");
                    LoadDataToDataGridView();
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã phòng!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                dbConnect.close();
            }

        }


        // Trong lớp frmPhong của bạn
        private void LoadLoaiPhong()
        {
            string sql = "SELECT MaLP, TenLP FROM LoaiPhong";
            DataTable dtLoaiPhong = dbConnect.getDataTable(sql);

            // Gán dữ liệu từ DataTable vào ComboBox
            cmbLoaiphong.DisplayMember = "TenLP"; // Hiển thị tên loại phòng
            cmbLoaiphong.ValueMember = "MaLP"; // Giá trị chọn là mã loại phòng
            cmbLoaiphong.DataSource = dtLoaiPhong; // Gán dữ liệu vào ComboBox
        }

        // Trong phương thức Load của form hoặc một sự kiện khác

        private void LoadDataToDataGridView()
        {
            // Thực hiện truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu
            string sql = "SELECT * FROM PHONG"; // Thay đổi câu truy vấn tùy thuộc vào cấu trúc cơ sở dữ liệu của bạn
            DataTable dtPhong = dbConnect.getDataTable(sql);

            // Gán dữ liệu từ DataTable vào DataGridView
            dgvDanhSachPhong.DataSource = dtPhong;
            dgvDanhSachPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnXemDanhSach_Click(object sender, EventArgs e)
        {
            LoadDataToDataGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhong.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string maPhong = dgvDanhSachPhong.SelectedRows[0].Cells["MAPHONG"].Value.ToString();
                    // Thực hiện xóa dữ liệu từ cơ sở dữ liệu
                    string sqlDelete = "DELETE FROM PHONG WHERE MAPHONG = '{maPhong}'"; // Thay đổi tùy theo cấu trúc cơ sở dữ liệu của bạn
                    int rowsAffected = dbConnect.getNonQuery(sqlDelete);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa phòng thành công!");
                        LoadDataToDataGridView(); // Reload DataGridView sau khi xóa
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa phòng!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần xóa!");
            }
        }

        private void frmPhong_Load(object sender, EventArgs e)
        {
            LoadLoaiPhong();
            txtTrangThai.Text = "Trống";

        }

        private void dgvDanhSachPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu dòng được chọn là dòng thực sự trong DataGridView
            {
                DataGridViewRow row = dgvDanhSachPhong.Rows[e.RowIndex];

                // Lấy dữ liệu từ dòng được chọn và hiển thị lên các điều khiển
                txtMaPhong.Text = row.Cells["MAPHONG"].Value.ToString();
                cmbLoaiphong.SelectedValue = row.Cells["MALP"].Value.ToString(); // Giả sử ValueMember của cmbLoaiPhong là MALP
                txtTrangThai.Text = row.Cells["TRANGTHAI"].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhong.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ các điều khiển (txtMaPhong, cmbLoaiPhong, cmbTrangThai)
                string maPhong = txtMaPhong.Text.Trim();
                string maLoaiPhong = cmbLoaiphong.SelectedValue.ToString(); // Giả sử ValueMember của cmbLoaiPhong là MALP
                string trangThai = txtTrangThai.Text;
                if (maPhong != "") // Kiểm tra nếu txtMaPhong không rỗng
                {
                    string maPhongSelected = dgvDanhSachPhong.SelectedRows[0].Cells["MAPHONG"].Value.ToString();

                    // Thực hiện cập nhật thông tin phòng trong cơ sở dữ liệu
                    string sqlUpdate = "UPDATE PHONG SET MAPHONG = '{maPhong}', MALP = '{maLoaiPhong}', TRANGTHAI = N'{trangThai}' WHERE MAPHONG = '{maPhongSelected}'"; // Thay đổi tùy theo cấu trúc cơ sở dữ liệu của bạn
                    int rowsAffected = dbConnect.getNonQuery(sqlUpdate);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thông tin phòng thành công!");
                        LoadDataToDataGridView(); // Reload DataGridView sau khi sửa
                    }
                    else
                    {
                        MessageBox.Show("Không thể sửa thông tin phòng!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã phòng!");
                }

            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa!");
            }
        }

        private void btnQL_Click(object sender, EventArgs e)
        {
            frmTrangChu trangChu = new frmTrangChu();
            trangChu.Show();
            this.Hide();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachPhong.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ các điều khiển (txtMaPhong, cmbLoaiPhong, cmbTrangThai)
                string maPhong = txtMaPhong.Text.Trim();
                string maLoaiPhong = cmbLoaiphong.SelectedValue.ToString(); // Giả sử ValueMember của cmbLoaiPhong là MALP
                string trangThai = txtTrangThai.Text;
                if (maPhong != "") // Kiểm tra nếu txtMaPhong không rỗng
                {
                    string maPhongSelected = dgvDanhSachPhong.SelectedRows[0].Cells["MAPHONG"].Value.ToString();

                    // Thực hiện cập nhật thông tin phòng trong cơ sở dữ liệu
                    string sqlUpdate = "UPDATE PHONG SET MAPHONG = '{maPhong}', MALP = '{maLoaiPhong}', TRANGTHAI = N'{trangThai}' WHERE MAPHONG = '{maPhongSelected}'"; // Thay đổi tùy theo cấu trúc cơ sở dữ liệu của bạn
                    int rowsAffected = dbConnect.getNonQuery(sqlUpdate);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thông tin phòng thành công!");
                        LoadDataToDataGridView(); // Reload DataGridView sau khi sửa
                    }
                    else
                    {
                        MessageBox.Show("Không thể sửa thông tin phòng!");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã phòng!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng cần sửa!");
            }
        }

        private void dgvDanhSachPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvDanhSachPhong_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DataGridViewColumn column = dgvDanhSachPhong.Columns[e.ColumnIndex];
                if (column != null)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Đặt cột tự động thay đổi kích thước để hiển thị toàn bộ nội dung
                }
            }
        }

        private void brnDatPhong_Click(object sender, EventArgs e)
        {
            frmDatPhong datPhong = new frmDatPhong();
            datPhong.Show();
            this.Hide();
        }

    }
}
