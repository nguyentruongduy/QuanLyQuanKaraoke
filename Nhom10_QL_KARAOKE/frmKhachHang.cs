using System;
using System.Collections.Generic;
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
    public partial class frmKhachHang : Form
    {
        SqlConnection conn;
        SqlDataAdapter adapt;
        DataTable dataTable;
        int customerId;
        public frmKhachHang()
        {
            InitializeComponent();
            conn = new SqlConnection(ConnnentionString.Conn);
            adapt = new SqlDataAdapter("SELECT * from KHACHHANG", conn);
            dataTable = new DataTable();
            adapt.Fill(dataTable);
            dgvKhachhang.DataSource = dataTable;
        }
        private void LoadCustomerData()
        {
            dataTable.Clear();
            adapt.Fill(dataTable);
        }
        private bool IsEmployeeExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM KHACHHANG WHERE MAKH = @MAKH";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@MAKH", employeeId);

                conn.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();

                return count > 0;
            }
        }
        private void ClearInputs()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";

            txtSDT.Text = "";
            txtDC.Text = "";

        }
        private DataGridViewRow r;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string employeeId = txtMaKH.Text.Trim().ToLower();
            string TENNV = txtTenKH.Text;
            string DIACHI = txtDC.Text;
            string SDT = txtSDT.Text;

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng", "Ràng buộc dữ liệu");
                txtMaKH.Select();
                return;
            }

            if (IsEmployeeExists(employeeId))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại trong cơ sở dữ liệu", "Ràng buộc dữ liệu");
                txtMaKH.Select();
                return;
            }

            string query = "INSERT INTO KHACHHANG (MAKH, TENKH, DIACHI, SDT) " +
                           "VALUES (@MAKH, @TENKH, @DIACHI, @SDT)";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@MAKH", employeeId);
                command.Parameters.AddWithValue("@TENKH", TENNV);
                command.Parameters.AddWithValue("@DIACHI", DIACHI);
                command.Parameters.AddWithValue("@SDT", SDT);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Thêm Khách Hàng Thành Công!", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomerData();
            ClearInputs();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string customerId = txtMaKH.Text.Trim();

            if (string.IsNullOrEmpty(customerId))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng  này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM KHACHHANG WHERE MAKH = @MAKH";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@MAKH", customerId);

                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Xóa khách hàng  Thành Công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void dgvKhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachhang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MAKH"].Value.ToString();
                txtTenKH.Text = row.Cells["TENKH"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtDC.Text = row.Cells["DIACHI"].Value.ToString();

            }
        }

        private void btnQL_Click(object sender, EventArgs e)
        {
            frmTrangChu TrangChu = new frmTrangChu();
            TrangChu.Show();
            this.Hide();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string customerId = txtMaKH.Text.Trim();

            if (string.IsNullOrEmpty(customerId))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa thông tin.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string TENKH = txtTenKH.Text;
            string DIACHI = txtDC.Text;
            string SDT = txtSDT.Text;

            string query = "UPDATE KHACHHANG SET TENKH = @TENKH, DIACHI = @DIACHI, SDT = @SDT WHERE MAKH = @MAKH";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@MAKH", customerId);
                command.Parameters.AddWithValue("@TENKH", TENKH);
                command.Parameters.AddWithValue("@DIACHI", DIACHI);
                command.Parameters.AddWithValue("@SDT", SDT);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Cập nhật Khách Hàng Thành Công!", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomerData();
            ClearInputs();
        }
    }

}