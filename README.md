# Phần mềm Quản lý Quán Karaoke

[cite_start]Đây là một dự án ứng dụng Desktop (WinForms) được xây dựng để quản lý các hoạt động kinh doanh của một quán karaoke[cite: 25]. [cite_start]Dự án này là một phần của đồ án nhóm, tập trung vào việc rèn luyện kỹ năng lập trình C# và xử lý các nghiệp vụ phức tạp trong môi trường desktop[cite: 22, 26, 27].

## Các chức năng chính

* [cite_start]**Quản lý phòng hát**: Cho phép thực hiện các thao tác đặt phòng, trả phòng, chuyển phòng và cập nhật trạng thái phòng[cite: 30].
* [cite_start]**Quản lý dịch vụ & kho**: Thêm/xóa các món ăn, đồ uống và tự động trừ số lượng tồn kho khi bán hàng[cite: 31].
* [cite_start]**Thanh toán**: Hỗ trợ lập hóa đơn và thực hiện thanh toán cho khách hàng[cite: 32].
* [cite_start]**Báo cáo & Thống kê**: Cung cấp chức năng thống kê và báo cáo doanh thu theo ngày hoặc tháng[cite: 33].

## Công nghệ sử dụng

* [cite_start]**Ngôn ngữ**: C# [cite: 28]
* [cite_start]**Nền tảng**: .NET Framework, WinForms [cite: 28]
* [cite_start]**Cơ sở dữ liệu**: MS SQL Server [cite: 28]
* [cite_start]**Công cụ**: Visual Studio [cite: 16]

## Hướng dẫn cài đặt

1.  **Clone repository**
    ```bash
    git clone [https://github.com/nguyentruongduy/QuanLyQuanKaraoke.git](https://github.com/nguyentruongduy/QuanLyQuanKaraoke.git)
    ```
2.  **Mở dự án**
    * Mở file `.sln` bằng Visual Studio.

3.  **Cấu hình cơ sở dữ liệu**
    * Mở SQL Server Management Studio và chạy file script SQL có trong dự án để tạo cơ sở dữ liệu và các bảng cần thiết.
    * Cập nhật chuỗi kết nối (Connection String) trong file `App.config` để trỏ đến cơ sở dữ liệu của bạn.

4.  **Chạy ứng dụng**
    * Build và chạy dự án từ Visual Studio.
