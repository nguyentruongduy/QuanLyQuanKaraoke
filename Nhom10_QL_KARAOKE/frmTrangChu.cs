using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nhom10_QL_KARAOKE
{
    public partial class frmTrangChu : Form
    {
        SqlConnection conn;
        SqlDataAdapter adapt;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        

        public frmTrangChu()
        {
            InitializeComponent();
            conn = new SqlConnection(ConnnentionString.Conn);
        }

        //private bool IsAllowedNhanVien()
        //{
        //    // Lấy chức vụ của người dùng
        //    string chucvu = "SELECT CHUCVU FROM NHANVIEN WHERE TAIKHOAN = @TaiKhoan";

        //    // Kiểm tra quyền truy cập
        //    return chucvu == ChucVu.Sep || chucvu == ChucVu.QuanLy;
        //}

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            //// Kiểm tra chức vụ của người dùng
            //string query = "SELECT CHUCVU FROM NHANVIEN WHERE TAIKHOAN = @TaiKhoan";
            //using (SqlConnection conn = new SqlConnection(ConnnentionString.Conn))
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@TaiKhoan", TenTaiKhoan);
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        if (reader.Read())
            //        {
            //            // Lấy chức vụ của người dùng
            //            string chucvu = reader["CHUCVU"].ToString();

            //            // Kiểm tra xem có được vào quản lý nhân viên hay không
            //            if (chucvu == "SẾP" || chucvu == "QUẢN LÍ")
            //            {
                            // Mở form quản lý nhân viên
                            frmNhanVien nhanvien = new frmNhanVien();
                            nhanvien.Show();
                            this.Hide();
                        //}
                    //    else
                    //    {
                    //        MessageBox.Show("Bạn không có quyền truy cập vào chức năng này.", "Thông báo");
                    //    }
                    //}
            //    }
            //}
        }

        private void labQL_Click(object sender, EventArgs e)
        {
           frmDangNhap dangNhap = new frmDangNhap();
            dangNhap.Show();
             this.Hide();   

        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang khachHang = new frmKhachHang();
            khachHang.Show();   
            this.Hide();
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            frmPhong phong = new frmPhong();    
            phong.Show();
            this.Hide();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            frmDatPhong datPhong = new frmDatPhong();   
            datPhong.Show();
            this.Hide();
        }

        private bool isExit;
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                isExit = false;
                this.Hide();
                frmDangNhap dangNhap = new frmDangNhap();
                dangNhap.Show();
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon hoaDon = new frmHoaDon();
            hoaDon.Show();
            this.Hide();
        }
    }
}
