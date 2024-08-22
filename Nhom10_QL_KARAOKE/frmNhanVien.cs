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

    public partial class frmNhanVien : Form
    {
        
        SqlConnection conn;
        SqlDataAdapter adapt;
        DataTable dataTable;
        int selectedEmployeeId;
        
        public frmNhanVien()
        {
            InitializeComponent();
            conn = new SqlConnection(ConnnentionString.Conn);
            adapt = new SqlDataAdapter("SELECT * from NHANVIEN", conn);
            dataTable = new DataTable();
            adapt.Fill(dataTable);
            dgvNhanVien.DataSource = dataTable;
        }
        

        private void LoadEmployeeData()
        {
            dataTable.Clear();
            adapt.Fill(dataTable);
        }
        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = row.Cells["MANV"].Value.ToString();
                txtTenNV.Text = row.Cells["TENNV"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtEM.Text = row.Cells["MAIL"].Value.ToString();
                txtCV.Text = row.Cells["CHUCVU"].Value.ToString();
                txtDC.Text = row.Cells["DIACHI"].Value.ToString();
                string gioiTinh = row.Cells["GIOITINH"].Value.ToString();
                if (gioiTinh == "NAM")
                {
                    rabNam.Checked = true;
                    rabNu.Checked = false;
                }
                else if (gioiTinh == "NỮ")
                {
                    rabNam.Checked = false;
                    rabNu.Checked = true;
                }
            }

        }
        private bool IsEmployeeExists(string employeeId)
        {
            string query = "SELECT COUNT(*) FROM NHANVIEN WHERE MANV = @MANV";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@MANV", employeeId);

                conn.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();

                return count > 0;
            }
        }
        private void ClearInputs()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtEM.Text = "";
            txtSDT.Text = "";
            txtCV.Text = "";
            txtDC.Text = "";
            rabNam.Checked = false; // Đặt RadioButton "Nam" về trạng thái không được chọn
            rabNu.Checked = false; // Đặt RadioButton "Nữ" về trạng thái không được chọn
        }

        private DataGridViewRow r;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string employeeId = txtMaNV.Text.Trim().ToLower();
            string TENNV = txtTenNV.Text;
            string MAIL = txtEM.Text;
            string CHUCVU = txtCV.Text;
            string DIACHI = txtDC.Text;
            string SDT = txtSDT.Text;
            string GIOITINH = rabNam.Checked ? "NAM" : "NỮ";


            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên", "Ràng buộc dữ liệu");
                txtMaNV.Select();
                return;
            }
            if (IsEmployeeExists(employeeId))
            {
                MessageBox.Show("Mã nhân viên đã tồn tại trong cơ sở dữ liệu", "Ràng buộc dữ liệu");
                txtMaNV.Select();
                return;
            }

            string query = "INSERT INTO NHANVIEN (MANV, TENNV, MAIL, CHUCVU, DIACHI,SDT,GIOITINH) " +
                            "VALUES (@MANV, @TENNV, @MAIL, @CHUCVU, @DIACHI,@SDT,@GIOITINH)";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@MANV", employeeId);
                command.Parameters.AddWithValue("@TENNV", TENNV);
                command.Parameters.AddWithValue("@MAIL", MAIL);
                command.Parameters.AddWithValue("@CHUCVU", CHUCVU);
                command.Parameters.AddWithValue("@DIACHI", DIACHI);
                command.Parameters.AddWithValue("@SDT", SDT);
                command.Parameters.AddWithValue("@GIOITINH", GIOITINH);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Thêm Nhân Viên Thành Công !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadEmployeeData();
            txtDC.Text = txtSDT.Text = txtEM.Text = txtCV.Text = txtTenNV.Text = txtMaNV.Text = string.Empty;
        }


        private void btnQL_Click(object sender, EventArgs e)
        {
            frmTrangChu TrangChu = new frmTrangChu();
            TrangChu.Show();
            this.Hide();

        }


        private void btnThem_Click_1(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            
            string employeeId = txtMaNV.Text.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Vui lòng chọn Nhân viên cần sửa thông tin.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string TENNV = txtTenNV.Text;
            string MAIL = txtEM.Text;
            string CHUCVU = txtCV.Text;
            string DIACHI = txtDC.Text;
            string SDT = txtSDT.Text;
            string GIOITINH = rabNam.Checked ? "NAM" : "NỮ";

            string query = "UPDATE NHANVIEN SET TENNV = @TENNV, MAIL = @MAIL, CHUCVU = @CHUCVU, DIACHI = @DIACHI, SDT = @SDT, GIOITINH = @GIOITINH WHERE MANV = @MANV";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@MANV", employeeId);
                command.Parameters.AddWithValue("@TENNV", TENNV);
                command.Parameters.AddWithValue("@MAIL", MAIL);
                command.Parameters.AddWithValue("@CHUCVU", CHUCVU);
                command.Parameters.AddWithValue("@DIACHI", DIACHI);
                command.Parameters.AddWithValue("@SDT", SDT);
                command.Parameters.AddWithValue("@GIOITINH", GIOITINH);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }

            MessageBox.Show("Cập nhật Nhân Viên Thành Công !", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadEmployeeData();
            txtDC.Text = txtSDT.Text = txtEM.Text = txtCV.Text = txtTenNV.Text = txtMaNV.Text = string.Empty;
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string employeeId = txtMaNV.Text.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM NhanVien WHERE MANV = @MANV";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@MANV", employeeId);

                        conn.Open();
                        command.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Xóa Nhân Viên Thành Công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}

