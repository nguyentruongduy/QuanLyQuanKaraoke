﻿drop database QL_KARAOKE

GO
CREATE DATABASE QL_KARAOKE

GO
USE QL_KARAOKE

GO
CREATE TABLE NHANVIEN
(
    MANV NCHAR(10) NOT NULL,
    TENNV NVARCHAR(50),
    SDT NVARCHAR(15),
    MAIL NVARCHAR(50),
    DIACHI NVARCHAR(50),
    CHUCVU NVARCHAR(30),
    GIOITINH NVARCHAR(10),
	TAIKHOAN CHAR (30),
	MATKHAU CHAR(30),
    CONSTRAINT PK_NHANVIEN PRIMARY KEY(MANV)
);

GO
CREATE TABLE KHACHHANG
(
    MAKH NCHAR(10) NOT NULL,
    TENKH NVARCHAR(50),
    SDT NVARCHAR(15),
    DIACHI NVARCHAR(50),
    CONSTRAINT PK_KHACHHANG PRIMARY KEY(MAKH)
);

GO
CREATE TABLE LOAIPHONG
(
    MALP NCHAR(10) NOT NULL,
    TENLP NVARCHAR(30),
    DONGIA INT,
    CONSTRAINT PK_LOAIPHONG PRIMARY KEY(MALP)
);

GO
CREATE TABLE PHONG
(
    MAPHONG NCHAR(10) NOT NULL,
    MALP NCHAR(10) NOT NULL,
	TENPHONG NVARCHAR(30),
    TRANGTHAI NVARCHAR(30),
    NGUOITAO NVARCHAR(50),
    NGAYTAO DATETIME,
    CONSTRAINT PK_PHONG PRIMARY KEY(MAPHONG),
    CONSTRAINT FK_PHONG_LOAIPHONG FOREIGN KEY (MALP) REFERENCES LOAIPHONG(MALP)
);

GO
CREATE TABLE DATPHONG (
	MADATPHONG CHAR(10) PRIMARY KEY,
    MAPHONG NCHAR(10),
    NGAYDAT DATETIME,
    THOIGIANDAT TIME ,
	GHICHU NVARCHAR(MAX),
	TENKH NVARCHAR(50),
	SDT NVARCHAR(15),
    CONSTRAINT FK_DatPhong_Phong FOREIGN KEY (MAPHONG) REFERENCES PHONG(MAPHONG)
);

GO
CREATE TABLE HOADON
(
    MAHD NCHAR(10) NOT NULL,
	MAPHONG NCHAR(10) NOT NULL,
	MANV NCHAR(10) NOT NULL,
	NGUOITAO NVARCHAR(50),
	NGAYLAP DATETIME,
	TONGTIEN FLOAT,
	CONSTRAINT PK_HOADON PRIMARY KEY(MAHD),
	CONSTRAINT FK_HOADON_PHONG 
	FOREIGN KEY (MAPHONG) REFERENCES PHONG(MAPHONG),
	CONSTRAINT FK_HOADON_NHANVIEN
	FOREIGN KEY (MANV) REFERENCES NHANVIEN(MANV)
)

GO
INSERT INTO NHANVIEN (MANV, TENNV ,SDT,MAIL , DIACHI , CHUCVU , GIOITINH , TAIKHOAN ,MATKHAU)
VALUES	('NV01',N'NGUYỄN TIẾN DŨNG',N'0123123123',N'ngtiendung012@gmail.com',N' TP.HỒ CHÍ MINH ', N'SẾP',N'NAM','sepdung','12345'),
		('NV02', N'NGUYỄN TƯỜNG VI', N'0987654321', N'ntv188002@gmail.com', N'TP.HỒ CHÍ MINH', N'NHÂN VIÊN', N'NỮ', 'vi123', '12345'),
		('NV03', N'ĐẶNG XUÂN BÌNH', N'0369696969', N'xuanbinh@example.com', N'Đà Nẵng', N'QUẢN LÍ', N'NAM', 'binh123', '123'),
		('NV04', N'NGUYỄN TRƯỜNG DUY', N'0123456789', N'truongduy@example.com', N'Hà Nội', N'NHÂN VIÊN', N'NỮ', 'DUY123', '123');

GO
INSERT INTO KHACHHANG (MAKH, TENKH, SDT, DIACHI) VALUES
    ('KH1', N'Nguyễn Văn A', '0987654321', N'Hà Nội'),
	('KH2', N'Nguyễn Văn B', '0987654683', N'TP HCM'),
	('KH3', N'Nguyễn Văn c', '0987654125', N'Hà Nội')

-- Insert data into LOAIPHONG table
INSERT INTO LOAIPHONG (MALP, TENLP, DONGIA)
VALUES
    ('LP01', N'Phòng VIP', 150000),  -- Example data for a VIP room with a price of 150,000
    ('LP02', N'Phòng Thường', 100000);  -- Example data for a regular room with a price of 100,000
GO
-- Insert data into PHONG table
INSERT INTO PHONG (MAPHONG, MALP, TENPHONG, TRANGTHAI, NGUOITAO, NGAYTAO)
VALUES
    ('P001', 'LP01', N'Phòng VIP 1',N'Đang hoạt động', N'Admin', GETDATE()),  -- Example data for a room with ID P001, type LP01 (VIP), empty status, created by Admin
    ('P002', 'LP02', N'Phòng Thường 1',N'Trống', N'Admin', GETDATE()),  -- Example data for another room with ID P002, type LP02 (regular), empty status, created by Admin
	('P003', 'LP01', N'Phòng VIP 2',N'Trống', N'Admin', GETDATE()),
	('P004', 'LP02', N'Phòng Thường 2',N'Trống', N'Admin', GETDATE()),
	('P005', 'LP01', N'Phòng VIP 3',N'Trống', N'Admin', GETDATE()),
	('P006', 'LP02', N'Phòng Thường 3',N'Trống', N'Admin', GETDATE()),
	('P007', 'LP01', N'Phòng VIP 4',N'Trống', N'Admin', GETDATE()),
	('P008', 'LP02', N'Phòng Thường 4',N'Trống', N'Admin', GETDATE());

GO
SET DATEFORMAT DMY;
INSERT INTO HOADON (MAHD, MAPHONG, MANV, NGUOITAO, NGAYLAP, TONGTIEN) VALUES
    ('HD1', 'P001', 'NV01', N'NGUYỄN TIẾN DŨNG' ,'01/01/2023', 300000),
    ('HD2', 'P001', 'NV02', N'NGUYỄN TƯỜNG VI'  ,'02/02/2023', 150000),
    ('HD3', 'P003', 'NV03', N'ĐẶNG XUÂN BÌNH'   ,'03/03/2023',450000),
    ('HD4', 'P004', 'NV01', N'NGUYỄN TIẾN DŨNG' ,'04/04/2023', 200000),
    ('HD5', 'P004', 'NV04', N'NGUYỄN TRƯỜNG DUY','05/05/2023', 600000),
    ('HD6', 'P006', 'NV02', N'NGUYỄN TƯỜNG VI'  ,'06/06/2023', 400000),
    ('HD7', 'P007', 'NV04', N'NGUYỄN TRƯỜNG DUY','07/07/2023', 750000),
    ('HD8', 'P008', 'NV03', N'ĐẶNG XUÂN BÌNH'   ,'08/08/2023',100000);
GO
INSERT INTO DATPHONG (MADATPHONG,MAPHONG,NGAYDAT,THOIGIANDAT,GHICHU,TENKH,SDT)
VALUES 
('DP001','P001','7/12/2023','07:23:40','',N'Nguyễn Trường Duy','0337842059');

--DROP TABLE HOADON
GO
SELECT * FROM NHANVIEN
SELECT * FROM KHACHHANG
SELECT * FROM LOAIPHONG
SELECT * FROM PHONG
SELECT * FROM DATPHONG
SELECT * FROM HOADON