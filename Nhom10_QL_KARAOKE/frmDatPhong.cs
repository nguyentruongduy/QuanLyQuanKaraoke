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
    public partial class frmDatPhong : Form
    {

        DBConnect dbConnect;
        public frmDatPhong()
        {
            InitializeComponent();
            dbConnect = new DBConnect();
        }
        private void LoadPhongThuongTrong()
        {
            DBConnect dbConnect = new DBConnect();
            dbConnect.open();

            // Truy vấn SQL để lấy thông tin về các phòng trống
            string query = "SELECT MAPHONG, TRANGTHAI FROM PHONG WHERE MALP = 'LP02' AND TRANGTHAI = N'Trống'";

            // Thực thi truy vấn và lấy dữ liệu
            DataTable dt = dbConnect.getDataTable(query);

            // Xóa dữ liệu cũ trên ListView trước khi thêm dữ liệu mới
            livPhong.Items.Clear();

            // Hiển thị dữ liệu từ DataTable lên ListView
            foreach (DataRow row in dt.Rows)
            {
                string maPhong = row["MAPHONG"].ToString();
                string trangThai = row["TRANGTHAI"].ToString();

                ListViewItem item = new ListViewItem(maPhong);
                item.SubItems.Add(trangThai);
                item.ImageIndex = 3;


                livPhong.Items.Add(item);
            }

            dbConnect.close();
        }
        private void LoadPhongVIPTrong()
        {
            DBConnect dbConnect = new DBConnect();
            dbConnect.open();

            // Truy vấn SQL để lấy thông tin về các phòng trống
            string query = "SELECT MAPHONG, TRANGTHAI FROM PHONG WHERE MALP = 'LP01' AND TRANGTHAI = N'Trống'";

            // Thực thi truy vấn và lấy dữ liệu
            DataTable dt = dbConnect.getDataTable(query);

            // Xóa dữ liệu cũ trên ListView trước khi thêm dữ liệu mới
            livVip.Items.Clear();

            // Hiển thị dữ liệu từ DataTable lên ListView
            foreach (DataRow row in dt.Rows)
            {
                string maPhong = row["MAPHONG"].ToString();
                string trangThai = row["TRANGTHAI"].ToString();

                ListViewItem item = new ListViewItem(maPhong);
                item.SubItems.Add(trangThai);
                item.ImageIndex = 3;


                livVip.Items.Add(item);
            }

            dbConnect.close();
        }

        private void loadMaDP()
        {
            int currentCount = 0;
            string queryCount = "SELECT COUNT(*) FROM DATPHONG";


            dbConnect.open();

            object result = dbConnect.getScalar(queryCount);

            if (result != null && int.TryParse(result.ToString(), out currentCount))
            {
                // Tạo mã mới dựa trên số lượng hiện tại và tăng lên 1
                int nextCount = currentCount + 1;
                string maDatPhong = "DP" + nextCount.ToString("D3"); // Định dạng số thành chuỗi với độ dài 3 ký tự

                // Hiển thị mã mới trong textbox
                txtMaDatPhong.Text = maDatPhong;
                txtMaDatPhong.Enabled = false;
            }
            else
            {
                // Xử lý khi không thể lấy được số đếm từ cơ sở dữ liệu
                // Ví dụ: hiển thị một giá trị mặc định hoặc thông báo lỗi
                txtMaDatPhong.Text = "DP001"; // Hoặc giá trị mặc định khác
            }

            dbConnect.close();
        }
        private void LoadDgvPhongDaDat()
        {
            // Kết nối cơ sở dữ liệu và truy vấn
            string query = "SELECT * FROM DATPHONG";

            DBConnect dbConnect = new DBConnect();
            dbConnect.open();

            DataTable dt = dbConnect.getDataTable(query);

            dgvPhongDaDat.DataSource = dt;
            dgvPhongDaDat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dbConnect.close();
        }
        private void loadDatPhong()
        {
            string maPhong = txtMaPhong.Text;

            string queryUpdate = "UPDATE PHONG SET TRANGTHAI = @trangThai WHERE MAPHONG = @maPhong";

            DBConnect dbConnect = new DBConnect();
            dbConnect.open();

            SqlCommand commandUpdate = new SqlCommand(queryUpdate, dbConnect.Conn);
            commandUpdate.Parameters.AddWithValue("@trangThai", "Đang hoạt động");
            commandUpdate.Parameters.AddWithValue("@maPhong", maPhong);
            dbConnect.close();
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // Đây là một ví dụ kiểm tra đơn giản, bạn có thể thay đổi logic kiểm tra tùy theo yêu cầu cụ thể của mình
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length == 10; // Kiểm tra xem số điện thoại có phải là số và có đúng 10 chữ số không
        }


        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            string maPhong = txtMaPhong.Text;
            string maDatPhong = txtMaDatPhong.Text;
            string tenkh = txtTenKH.Text;
            string sdt = txtSDT.Text;
            if (string.IsNullOrEmpty(tenkh) || string.IsNullOrWhiteSpace(tenkh))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.");
                return;
            }

            if (string.IsNullOrEmpty(sdt) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.");
                return;
            }

            if (!IsPhoneNumberValid(sdt))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng kiểm tra lại.");
                return;
            }
            string ngayDat = DateTime.Now.ToString("yyyy-MM-dd"); // Lấy ngày hiện tại
            string gioDat = DateTime.Now.ToString("HH:mm:ss");


            string ghichu = txtGhiChu.Text;

            DBConnect dbConnect = new DBConnect();
            dbConnect.open();

            // Thêm dữ liệu vào bảng DatPhong
            string queryInsertDatPhong = "INSERT INTO DATPHONG (MADATPHONG, MAPHONG, NGAYDAT, THOIGIANDAT,GHICHU,TENKH,SDT) VALUES (@maDatPhong, @maPhong, @ngayDat, @gioDat,@ghichu,@tenkh,@sdt)";
            SqlCommand commandInsertDatPhong = new SqlCommand(queryInsertDatPhong, dbConnect.Conn);
            commandInsertDatPhong.Parameters.AddWithValue("@maDatPhong", maDatPhong);
            commandInsertDatPhong.Parameters.AddWithValue("@maPhong", maPhong);
            commandInsertDatPhong.Parameters.AddWithValue("@ngayDat", ngayDat);
            commandInsertDatPhong.Parameters.AddWithValue("@gioDat", gioDat);
            commandInsertDatPhong.Parameters.AddWithValue("@ghichu", ghichu);
            commandInsertDatPhong.Parameters.AddWithValue("@tenkh", tenkh);
            commandInsertDatPhong.Parameters.AddWithValue("@sdt", sdt);
            // Cập nhật trạng thái của phòng từ "Trống" sang "Đang hoạt động"
            string queryUpdatePhong = "UPDATE PHONG SET TRANGTHAI = @trangThai WHERE MAPHONG = @maPhong";
            SqlCommand commandUpdatePhong = new SqlCommand(queryUpdatePhong, dbConnect.Conn);
            commandUpdatePhong.Parameters.AddWithValue("@trangThai", "Đang hoạt động");
            commandUpdatePhong.Parameters.AddWithValue("@maPhong", maPhong);

            try
            {
                // Bắt đầu giao dịch
                SqlTransaction transaction = dbConnect.Conn.BeginTransaction();
                commandInsertDatPhong.Transaction = transaction;
                commandUpdatePhong.Transaction = transaction;

                // Thực hiện truy vấn
                commandInsertDatPhong.ExecuteNonQuery();
                commandUpdatePhong.ExecuteNonQuery();

                // Kết thúc giao dịch
                transaction.Commit();

                MessageBox.Show("Đặt phòng thành công!");
                LoadDgvPhongDaDat();
                LoadPhongThuongTrong();
                LoadPhongVIPTrong();
                // Gọi hàm để cập nhật lại DataGridView hoặc thực hiện các công việc cần thiết sau khi đặt phòng thành công
            }
            catch (Exception ex)
            {
                // Rollback giao dịch nếu có lỗi xảy ra

                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                dbConnect.close();
            }
        }

      

        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            LoadPhongThuongTrong();
            LoadPhongVIPTrong();
            loadMaDP();
            LoadDgvPhongDaDat();
        }

        private int GetDonGiaByMaLoaiPhong(string maLoaiPhong)
        {
            int donGia = 0;

            try
            {
                DBConnect dbConnect = new DBConnect();
                dbConnect.open();

                string query = "SELECT DONGIA FROM LOAIPHONG WHERE MALP = @maLoaiPhong";
                SqlCommand command = new SqlCommand(query, dbConnect.Conn);
                command.Parameters.AddWithValue("@maLoaiPhong", maLoaiPhong);

                // Thực hiện truy vấn và lấy đơn giá từ cơ sở dữ liệu
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    donGia = Convert.ToInt32(result);
                }

                dbConnect.close();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra trong quá trình truy vấn
                // Ví dụ: log lỗi, thông báo người dùng, v.v.
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return donGia;
        }

        private void livPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (livPhong.SelectedItems.Count > 0)
            {
                // Lấy mã phòng từ mục đã chọn trong ListView và hiển thị nó trong TextBox txtMaPhong
                string maPhong = livPhong.SelectedItems[0].Text;
                txtMaPhong.Text = maPhong;
                txtMaPhong.Enabled = false;
                livVip.SelectedItems.Clear();
                string query = "SELECT DONGIA FROM LOAIPHONG WHERE MALP = (SELECT MALP FROM PHONG WHERE MAPHONG = @maPhong)";


                dbConnect.open();

                SqlCommand command = new SqlCommand(query, dbConnect.Conn);
                command.Parameters.AddWithValue("@maPhong", maPhong);

                int donGia = (int)command.ExecuteScalar();
                txtDonGia.Text = donGia.ToString();
                txtDonGia.Enabled = false;
                dbConnect.close();
            }
        }

        private void livVip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (livVip.SelectedItems.Count > 0)
            {
                // Lấy mã phòng từ mục đã chọn trong ListView và hiển thị nó trong TextBox txtMaPhong
                string maPhong = livVip.SelectedItems[0].Text;
                txtMaPhong.Text = maPhong;
                livPhong.SelectedItems.Clear();
                string query = "SELECT DONGIA FROM LOAIPHONG WHERE MALP = (SELECT MALP FROM PHONG WHERE MAPHONG = @maPhong)";
                SqlCommand command = new SqlCommand(query, dbConnect.Conn);
                command.Parameters.AddWithValue("@maPhong", maPhong);
                dbConnect.open();
                int donGia = (int)command.ExecuteScalar();
                txtDonGia.Text = donGia.ToString();
                txtDonGia.Enabled = false;
                dbConnect.close();
            }
        }

        private void btnQL_Click(object sender, EventArgs e)
        {
            frmTrangChu trangChu = new frmTrangChu();
            trangChu.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPhong frmPhong = new frmPhong();
            frmPhong.Show();
            this.Hide();
        }
        public int GetDonGiaByMaPhong(string maPhong)
        {
            int donGia = 0;
            try
            {
                // Thực hiện kết nối đến cơ sở dữ liệu
                dbConnect.open();

                // Xây dựng truy vấn SQL để lấy giá phòng dựa trên mã phòng
                string query = "SELECT DONGIA FROM LOAIPHONG WHERE MALP = (SELECT MALP FROM PHONG WHERE MAPHONG = @maPhong)";
                SqlCommand command = new SqlCommand(query,dbConnect.Conn);
                command.Parameters.AddWithValue("@maPhong", maPhong);

                // Thực thi truy vấn và lấy giá trị đơn giá từ cơ sở dữ liệu
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    donGia = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu có
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối sau khi hoàn thành công việc
                dbConnect.close();
            }

            return donGia;
        }


        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvPhongDaDat.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng đã chọn trong DataGridView
                DataGridViewRow selectedRow = dgvPhongDaDat.SelectedRows[0];
                string maPhong = selectedRow.Cells["MAPHONG"].Value.ToString();
                string gioDat = selectedRow.Cells["THOIGIANDAT"].Value.ToString();

                // Lấy đơn giá của phòng đã chọn từ DBConnect
                DBConnect dbConnect = new DBConnect();
                int donGia = GetDonGiaByMaPhong(maPhong);

                // Tính số giờ/thời gian thuê phòng
                DateTime gioDatPhong = DateTime.Parse(gioDat);
                DateTime gioThanhToan = DateTime.Now;
                TimeSpan thoiGianThue = gioThanhToan - gioDatPhong;
                double tongTien = (double)thoiGianThue.TotalHours * donGia;
                // Thực hiện xóa dữ liệu của mã đặt phòng đã thanh toán trong bảng DATPHONG
                dbConnect.open();
                string maDatPhong = dgvPhongDaDat.SelectedRows[0].Cells["MADATPHONG"].Value.ToString();
                string queryXoaDatPhong = "DELETE FROM DATPHONG WHERE MADATPHONG = @maDatPhong";
                SqlCommand commandXoaDatPhong = new SqlCommand(queryXoaDatPhong, dbConnect.Conn);
                commandXoaDatPhong.Parameters.AddWithValue("@maDatPhong", maDatPhong);
                commandXoaDatPhong.ExecuteNonQuery();
                // Cập nhật trạng thái phòng thành "Trống"
                string queryUpdatePhong = "UPDATE PHONG SET TRANGTHAI = @trangThai WHERE MAPHONG = @maPhong";
                SqlCommand commandUpdatePhong = new SqlCommand(queryUpdatePhong, dbConnect.Conn);
                commandUpdatePhong.Parameters.AddWithValue("@trangThai", "Trống");
                commandUpdatePhong.Parameters.AddWithValue("@maPhong", maPhong);
                loadDatPhong();
                LoadDgvPhongDaDat();
                LoadPhongThuongTrong();
                LoadPhongVIPTrong();

                try
                {
                    dbConnect.open();
                    // Thực hiện cập nhật trạng thái phòng
                    commandUpdatePhong.ExecuteNonQuery();
                    MessageBox.Show($"Giờ đặt phòng:{gioDatPhong}\n Giờ trả phòng:{gioThanhToan}\n Mã Phòng: {maPhong}\n Thời gian thuê:{thoiGianThue}\n Đơn giá:{donGia}\n Thành Tiền: {tongTien.ToString("N0")} VND");

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
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để thanh toán.");
            }


        }

        private void dgvPhongDaDat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvPhongDaDat_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhongDaDat.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPhongDaDat.SelectedRows[0];

                // Lấy mã phòng từ cột có tên "MAPHONG"
                string maPhong = selectedRow.Cells["MAPHONG"].Value.ToString();

                // Sau khi có mã phòng, truy vấn để lấy giá phòng từ cơ sở dữ liệu
                int donGia = GetDonGiaByMaPhong(maPhong);

                // Hiển thị giá phòng lên textbox txtDonGia
                txtDonGia.Text = donGia.ToString();
            }
        }
    }
}
