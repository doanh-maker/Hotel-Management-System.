-- Nếu database đã tồn tại thì xoá
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'QuanLyKhachSan')
BEGIN
    ALTER DATABASE QuanLyKhachSan SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyKhachSan;
END
GO

-- Tạo lại database
CREATE DATABASE QuanLyKhachSan;
GO

-- Sử dụng database
USE QuanLyKhachSan;
GO
-- 1. Tài khoản (phân quyền)
CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro INT NOT NULL CHECK (VaiTro IN (1,2,3)), -- 1: Quản lý, 2: Nhân viên, 3: Khách hàng
    TrangThai BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE()
);

-- 2. Khách hàng
CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    MaTK INT NULL FOREIGN KEY REFERENCES TaiKhoan(MaTK),
    HoTen NVARCHAR(100) NOT NULL,
    CCCD VARCHAR(12) NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NULL,
    DiaChi NVARCHAR(200) NULL,
    GhiChu NVARCHAR(500) NULL
);
-- 3. Nhân viên
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    MaTK INT NULL FOREIGN KEY REFERENCES TaiKhoan(MaTK),
    HoTen NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50) NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NULL,
    NgayVaoLam DATE NULL,
    TrangThai BIT DEFAULT 1
);
-- 4. Loại phòng
CREATE TABLE LoaiPhong (
    MaLoai INT IDENTITY(1,1) PRIMARY KEY,
    TenLoai NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    DonGiaTheoNgay DECIMAL(18,2) NOT NULL,
    SoLuongNguoi INT NOT NULL DEFAULT 1
);

-- 5. Phòng
CREATE TABLE Phong (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    MaLoai INT NOT NULL FOREIGN KEY REFERENCES LoaiPhong(MaLoai),
    TenPhong NVARCHAR(50) NOT NULL,
    TrangThai NVARCHAR(30) DEFAULT N'Trống',
    GhiChu NVARCHAR(255) NULL
);

-- 6. Đặt phòng (không có cột TongTienPhong)
CREATE TABLE DatPhong (
    MaDP INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    MaNV INT NULL FOREIGN KEY REFERENCES NhanVien(MaNV),
    NgayDat DATETIME DEFAULT GETDATE(),
    NgayNhan DATE NOT NULL,
    NgayTra DATE NOT NULL,
    TrangThaiDat NVARCHAR(30) DEFAULT N'Chờ duyệt',
    GhiChu NVARCHAR(500) NULL,
    CONSTRAINT CK_Ngay CHECK (NgayNhan < NgayTra)
);

-- 7. Chi tiết đặt phòng (không có SoNgayThue, ThanhTien)
CREATE TABLE ChiTietDatPhong (
    MaDP INT NOT NULL,
    MaPhong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,   -- giá tại thời điểm đặt
    PRIMARY KEY (MaDP, MaPhong),
    FOREIGN KEY (MaDP) REFERENCES DatPhong(MaDP),
    FOREIGN KEY (MaPhong) REFERENCES Phong(MaPhong)
);

-- 8. Dịch vụ
CREATE TABLE DichVu (
    MaDV INT IDENTITY(1,1) PRIMARY KEY,
    TenDV NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    DonViTinh NVARCHAR(20) NULL
);

-- 9. Sử dụng dịch vụ (không có cột ThanhTien)
CREATE TABLE SuDungDichVu (
    MaSD INT IDENTITY(1,1) PRIMARY KEY,
    MaDP INT NOT NULL FOREIGN KEY REFERENCES DatPhong(MaDP),
    MaDV INT NOT NULL FOREIGN KEY REFERENCES DichVu(MaDV),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    NgaySuDung DATE NOT NULL
);

-- 10. Hóa đơn (không có TongTienPhong, TongTienDichVu, TongCong)

CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaDP INT NOT NULL UNIQUE FOREIGN KEY REFERENCES DatPhong(MaDP),
    NgayLap DATETIME DEFAULT GETDATE(),
    TrangThaiThanhToan NVARCHAR(30) DEFAULT N'Chưa thanh toán'
);

-- 11. Thanh toán
CREATE TABLE ThanhToan (
    MaTT INT IDENTITY(1,1) PRIMARY KEY,
    MaHD INT NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD),
    SoTien DECIMAL(18,2) NOT NULL,
    PhuongThuc NVARCHAR(50) NOT NULL,
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(255) NULL
);
-- 12.Tiện nghi
CREATE TABLE TienNghi (
    MaTN INT IDENTITY(1,1) PRIMARY KEY,
    TenTN NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(255) NULL
);
-- 13. Chi tiết tiện nghi (bảng liên kết giữa Loại phòng và Tiện nghi)
CREATE TABLE ChiTietTienNghi (
    MaLoai INT NOT NULL FOREIGN KEY REFERENCES LoaiPhong(MaLoai),
    MaTN INT NOT NULL FOREIGN KEY REFERENCES TienNghi(MaTN),
    PRIMARY KEY (MaLoai, MaTN)
)
--14. tạo bảng đặt cọc
CREATE TABLE DatCoc (
    MaDC INT IDENTITY(1,1) PRIMARY KEY,
    MaDP INT NOT NULL FOREIGN KEY REFERENCES DatPhong(MaDP),
    SoTienCoc DECIMAL(18,2) NOT NULL,
    NgayCoc DATETIME DEFAULT GETDATE(),
    TrangThaiCoc NVARCHAR(30) DEFAULT N'Đã đặt cọc',
    GhiChu NVARCHAR(255) NULL
);

--15. tạo bảng chi phí phát sinh
CREATE TABLE ChiPhiPhatSinh (
    MaCPP INT IDENTITY(1,1) PRIMARY KEY,
    MaDP INT NOT NULL FOREIGN KEY REFERENCES DatPhong(MaDP),
    TenChiPhi NVARCHAR(100) NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    NgayPhatSinh DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(255) NULL
);
-- 16.tạo bảng huỷ
CREATE TABLE YeuCauHuy (
    MaYeuCau INT IDENTITY(1,1) PRIMARY KEY,
    MaDP INT NOT NULL FOREIGN KEY REFERENCES DatPhong(MaDP),
    LyDo NVARCHAR(255) NOT NULL,
    NgayYeuCau DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(30) DEFAULT N'Chờ duyệt', -- Chờ duyệt, Đã duyệt (hủy thành công), Từ chối
    GhiChu NVARCHAR(255) NULL
);
CREATE TABLE LichSuGiaHan (
    MaGiaHan INT PRIMARY KEY IDENTITY,
    MaDP INT NOT NULL FOREIGN KEY REFERENCES DatPhong(MaDP),
    MaNV INT NOT NULL FOREIGN KEY REFERENCES NhanVien(MaNV),
    NgayTraCu DATE,
    NgayTraMoi DATE,
    SoTienTang DECIMAL(18,2),
    NgayGiaHan DATETIME DEFAULT GETDATE()
);

USE QuanLyKhachSan;
GO

-- 1. TaiKhoan (5 tài khoản: 1 quản lý, 2 nhân viên, 2 khách hàng)
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, NgayTao) VALUES
('admin01', 'admin123', 1, 1, GETDATE()),
('nv001', 'nv123', 2, 1, GETDATE()),
('nv002', 'nv456', 2, 1, GETDATE()),
('kh001', 'kh123', 3, 1, GETDATE()),
('kh002', 'kh456', 3, 1, GETDATE());
GO

-- 2. KhachHang (5 khách, 2 có tài khoản, 3 không)
INSERT INTO KhachHang (MaTK, HoTen, CCCD, SoDienThoai, Email, DiaChi, GhiChu) VALUES
(4, N'Nguyễn Văn An', '012345678901', '0912345678', 'an.nguyen@email.com', N'Hà Nội', NULL),
(5, N'Trần Thị Bình', '023456789012', '0923456789', 'binh.tran@email.com', N'TP HCM', NULL),
(NULL, N'Lê Văn Cường', '034567890123', '0934567890', 'cuong.le@email.com', N'Đà Nẵng', N'Khách quen'),
(NULL, N'Phạm Thị Dung', '045678901234', '0945678901', 'dung.pham@email.com', N'Hải Phòng', NULL),
(NULL, N'Hoàng Văn Em', '056789012345', '0956789012', 'em.hoang@email.com', N'Cần Thơ', NULL);
GO

-- 3. NhanVien (5 nhân viên, 2 có tài khoản, 3 không)
INSERT INTO NhanVien (MaTK, HoTen, ChucVu, SoDienThoai, Email, NgayVaoLam, TrangThai) VALUES
(2, N'Nguyễn Thị Hoa', N'Lễ tân', '0961234567', 'hoa.nguyen@ks.com', '2023-01-15', 1),
(3, N'Trần Văn Bình', N'Buồng phòng', '0971234568', 'binh.tran@ks.com', '2023-03-20', 1),
(NULL, N'Lê Thị Cúc', N'Bếp', '0981234569', 'cuc.le@ks.com', '2023-05-10', 1),
(NULL, N'Phạm Văn Đức', N'Bảo vệ', '0991234570', 'duc.pham@ks.com', '2023-07-01', 1),
(NULL, N'Hoàng Thị Em', N'Lễ tân', '0901234571', 'em.hoang@ks.com', '2023-09-15', 1);
GO

-- 4. LoaiPhong (5 loại phòng)
INSERT INTO LoaiPhong (TenLoai, MoTa, DonGiaTheoNgay, SoLuongNguoi) VALUES
(N'Phòng đơn', N'Phòng 1 giường đơn, diện tích 20m2', 500000, 1),
(N'Phòng đôi', N'Phòng 1 giường đôi, diện tích 25m2', 800000, 2),
(N'Phòng gia đình', N'Phòng 2 giường đôi, diện tích 40m2', 1200000, 4),
(N'Phòng VIP', N'Phòng sang trọng, view biển', 2000000, 2),
(N'Phòng Suite', N'Phòng cao cấp, có phòng khách', 3000000, 4);
GO

-- 5. Phong (5 phòng)
INSERT INTO Phong (MaLoai, TenPhong, TrangThai, GhiChu) VALUES
(1, N'Phòng 101', N'Trống', NULL),
(2, N'Phòng 201', N'Trống', NULL),
(3, N'Phòng 301', N'Trống', NULL),
(4, N'Phòng 401', N'Trống', N'Phòng view đẹp'),
(5, N'Phòng 501', N'Trống', NULL);
GO

-- 6. DichVu (5 dịch vụ)
INSERT INTO DichVu (TenDV, DonGia, DonViTinh) VALUES
(N'Dọn phòng', 100000, N'lần'),
(N'Giặt ủi', 150000, N'kg'),
(N'Ăn sáng buffet', 200000, N'suất'),
(N'Ăn tối', 300000, N'suất'),
(N'Thuê xe máy', 120000, N'ngày');
GO

-- 7. TienNghi (5 tiện nghi)
INSERT INTO TienNghi (TenTN, MoTa) VALUES
(N'Điều hòa', N'Điều hòa 2 chiều'),
(N'Tivi', N'Tivi 50 inch, truyền hình cáp'),
(N'Minibar', N'Tủ lạnh mini có đồ uống'),
(N'Ban công', N'Ban công rộng, có ghế'),
(N'Wifi miễn phí', N'Tốc độ cao');
GO

-- 8. ChiTietTienNghi (liên kết loại phòng với tiện nghi, mỗi loại ít nhất 1 tiện nghi)
INSERT INTO ChiTietTienNghi (MaLoai, MaTN) VALUES
(1,1), (1,2), (1,5), -- Phòng đơn
(2,1), (2,2), (2,5), -- Phòng đôi
(3,1), (3,2), (3,3), (3,4), (3,5), -- Gia đình
(4,1), (4,2), (4,3), (4,4), (4,5), -- VIP
(5,1), (5,2), (5,3), (5,4), (5,5); -- Suite
GO

-- AE chú ý khi chạy cái này
/*
--==================== Xoá dữ liệu tất cả các bảng =============================
EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all";
EXEC sp_MSforeachtable "DELETE FROM ?";
EXEC sp_MSforeachtable "ALTER TABLE ? CHECK CONSTRAINT all";
--==================== Reset dữ liệu từ 0 ======================================
USE QuanLyKhachSan;

-- Reset tất cả bảng có identity (đặt giá trị hiện tại về 0, lần chèn tiếp theo sẽ là 1)
DBCC CHECKIDENT ('TaiKhoan', RESEED, 0);
DBCC CHECKIDENT ('KhachHang', RESEED, 0);
DBCC CHECKIDENT ('NhanVien', RESEED, 0);
DBCC CHECKIDENT ('LoaiPhong', RESEED, 0);
DBCC CHECKIDENT ('Phong', RESEED, 0);
DBCC CHECKIDENT ('DichVu', RESEED, 0);
DBCC CHECKIDENT ('TienNghi', RESEED, 0);
DBCC CHECKIDENT ('DatPhong', RESEED, 0);
DBCC CHECKIDENT ('SuDungDichVu', RESEED, 0);
DBCC CHECKIDENT ('HoaDon', RESEED, 0);
DBCC CHECKIDENT ('ThanhToan', RESEED, 0);
DBCC CHECKIDENT ('DatCoc', RESEED, 0);
DBCC CHECKIDENT ('ChiPhiPhatSinh', RESEED, 0);
DBCC CHECKIDENT ('YeuCauHuy', RESEED, 0);

DBCC CHECKIDENT ('LichSuGiaoDich', RESEED, 0);
*/
--viết trigger sử lý Đặt phòng
CREATE TRIGGER trg_CapNhatTrangThaiDat
ON DatCoc
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dp
    SET dp.TrangThaiDat = N'Đã đặt'
    FROM DatPhong dp
    INNER JOIN inserted i ON dp.MaDP = i.MaDP;
END
-- sửa  proc tìm kiếm theo ngày nhận ngày trả và số lượng người nếu 
--phòng đã đặt rồi thì không hiển thị nữa hiển thị thêm tổng tiền và 
--số đêm thuê nếu phòng đó có đặt rồi thì không hiển thị nữa
CREATE OR ALTER PROCEDURE sp_TimKiemPhongTheoNgayVaSoNguoi
    @NgayDat DATE,
    @NgayTra DATE,
    @SoNguoi INT,
    @MaLoai INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.MaPhong, 
        p.TenPhong, 
        lp.TenLoai, 
        lp.MoTa,
        p.TrangThai,
        lp.SoLuongNguoi,
        lp.DonGiaTheoNgay, 
        DATEDIFF(DAY, @NgayDat, @NgayTra) AS SoDem,
        (DATEDIFF(DAY, @NgayDat, @NgayTra) * lp.DonGiaTheoNgay) AS TongTien
    FROM Phong p
    JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
    WHERE lp.SoLuongNguoi BETWEEN @SoNguoi AND (@SoNguoi + 2)

    -- 👉 lọc loại phòng đúng chỗ
    AND (@MaLoai IS NULL OR p.MaLoai = @MaLoai)

    AND NOT EXISTS (
        SELECT 1
        FROM DatPhong db 
        JOIN ChiTietDatPhong ctdp ON db.MaDP = ctdp.MaDP
        WHERE ctdp.MaPhong = p.MaPhong
          AND db.TrangThaiDat IN (N'Đã đặt cọc', N'Đã đặt', N'Đã nhận phòng')
          AND (@NgayDat < db.NgayTra AND @NgayTra > db.NgayNhan)
    )
END;
Go
	
	-- tạo proc hiển thị tiện nghi của phòng theo mã phòng
	create or ALTER  PROCEDURE sp_HienThiTienNghiTheoMaPhong
	@MaPhong INT
	AS
	BEGIN
	SELECT  tn.MoTa
	FROM TienNghi tn
	JOIN ChiTietTienNghi cttn ON tn.MaTN = cttn.MaTN
	JOIN LoaiPhong lp ON lp.MaLoai = cttn.MaLoai
	JOIN Phong p ON p.MaLoai = lp.MaLoai
	WHERE p.MaPhong = @MaPhong;
	END;
	Go
	--========================================
    --Tạo proc điền thông tin khách hàng
    --========================================
	create or alter PROCEDURE sp_DienThongTinKhachHang
	@MaTK int,
	@HoTen NVARCHAR(255),
	@CCCD NVARCHAR(20),
	@SoDienThoai NVARCHAR(20),
	@Email NVARCHAR(255),
	@DiaChi NVARCHAR(255),
	@MaKH INT OUTPUT


	AS
	BEGIN
	set NOCOUNT ON;
	INSERT INTO KhachHang ( MaTK,HoTen, CCCD, SoDienThoai, Email, DiaChi)
	VALUES ( @MaTK,@HoTen, @CCCD, @SoDienThoai, @Email, @DiaChi);
	set @MaKH = SCOPE_IDENTITY(); -- Lấy MaKH vừa tạo
	END;
    Go
    /*====================================================================
	Viết proc tính tiền đặt cọc khi nhập mã đặt phòng tiền cọc bằng 30% 
    tổng tiền phòng( tổng tiền phòng = số đêm thuê * đơn giá phòng)
    --====================================================================*/
	CREATE  PROCEDURE sp_TinhTienDatCoc
    @MaDP INT,
    @TienDatCoc DECIMAL(18,2) OUTPUT
AS
BEGIN
    DECLARE @NgayNhan DATE, 
            @NgayTra DATE, 
            @DonGia DECIMAL(18,2);
    SELECT 
        @NgayNhan = dp.NgayNhan,
        @NgayTra = dp.NgayTra,
        @DonGia = lp.DonGiaTheoNgay
    FROM DatPhong dp
    JOIN ChiTietDatPhong ctdp ON dp.MaDP = ctdp.MaDP
    JOIN Phong p ON ctdp.MaPhong = p.MaPhong
    JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
    WHERE dp.MaDP = @MaDP;

    -- Kiểm tra dữ liệu
    IF @NgayNhan IS NULL OR @NgayTra IS NULL OR @DonGia IS NULL
    BEGIN
        SET @TienDatCoc = 0;
        RETURN;
    END

    DECLARE @SoDem INT = DATEDIFF(DAY, @NgayNhan, @NgayTra);

    IF @SoDem <= 0 SET @SoDem = 1; -- tránh lỗi

    DECLARE @TongTien DECIMAL(18,2) = @SoDem * @DonGia;

    SET @TienDatCoc = @TongTien * 0.3;
END;
Go
	/*================================================================
	viết view hiện thông thông tin đặt phòng bao gồm tên 
    khách hàng, tên phòng, ngày nhận, ngày trả, tổng tiền phòng
    ==================================================================*/
	CREATE  VIEW v_ThongTinDatPhong AS
	SELECT 
	dp.MaDP,
	kh.HoTen AS TenKhachHang,
	p.TenPhong,
	dp.NgayNhan,
	dp.NgayTra,
	(DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) * lp.DonGiaTheoNgay) AS TongTienPhong
	FROM DatPhong dp
	JOIN KhachHang kh ON dp.MaKH = kh.MaKH
	JOIN ChiTietDatPhong ctdp ON dp.MaDP = ctdp.MaDP
	JOIN Phong p ON ctdp.MaPhong = p.MaPhong
	JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai;
  /*=======================================
	viết proc insert dữ liệu đặt phòng
    =========================================*/
	create  PROCEDURE sp_ThemDatPhong
	@MaKH INT,
	@MaPhong INT,
	@NgayNhan Date,
	@NgayTra Date,
	@MaDP INT OUTPUT
	AS
	BEGIN
	BEGIN TRANSACTION;
	BEGIN TRY
		-- Thêm dữ liệu vào bảng DatPhong
		INSERT INTO DatPhong (MaKH, NgayNhan, NgayTra, TrangThaiDat)
		VALUES (@MaKH,  @NgayNhan, @NgayTra, N'Chưa duyệt');
		-- Lấy MaDP vừa tạo
		set @MaDP  = SCOPE_IDENTITY();
		-- Thêm dữ liệu vào bảng ChiTietDatPhong và đơn giá lấy từ bảng LoaiPhong
		iNSERT INTO ChiTietDatPhong (MaDP, MaPhong, DonGia)
		SELECT @MaDP, @MaPhong, lp.DonGiaTheoNgay
		FROM Phong p
		JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
		WHERE p.MaPhong = @MaPhong;

		-- Thêm dữ liệu vào bảng DatCoc số tiền tiền đặt cọc bằng tổng tiền phòng * 30%
		DECLARE @TongTienPhong DECIMAL(18,2) = (DATEDIFF(DAY, @NgayNhan, @NgayTra) * (SELECT DonGiaTheoNgay FROM Phong p JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai WHERE p.MaPhong = @MaPhong)) ;
		DECLARE @TienDatCoc DECIMAL(18,2) = @TongTienPhong * 0.3;
		INSERT INTO DatCoc (MaDP, SoTienCoc, TrangThaiCoc)
		VALUES (@MaDP, @TienDatCoc, N'Đã đặt cọc');
		
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
	END;
    Go
	
    --=================== viết proc hiện thị danh sách đặt phòng khách hàng ==============================
	CREATE  PROC pr_hienthidanhsachdatphong
    @MaTK INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        dp.MaDP,
        dp.NgayDat,
        dp.NgayNhan,
        dp.NgayTra,
        dp.TrangThaiDat,
        STUFF((SELECT ', ' + p.TenPhong
               FROM ChiTietDatPhong ct
               INNER JOIN Phong p ON ct.MaPhong = p.MaPhong
               WHERE ct.MaDP = dp.MaDP
               FOR XML PATH('')), 1, 2, '') AS DanhSachPhong,
        ISNULL(phong.TongTienPhong, 0) AS TongTienPhong,
        ISNULL(dichvu.TongTienDichVu, 0) AS TongTienDichVu,
        ISNULL(datcoc.TongDaCoc, 0) AS TongDaCoc,
        ISNULL(phatsinh.TongChiPhiPhatSinh, 0) AS TongChiPhiPhatSinh
    FROM DatPhong dp
    INNER JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    CROSS APPLY (SELECT DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS SoNgay) ngay
    OUTER APPLY (
       
    SELECT SUM(ct.DonGia) * ngay.SoNgay AS TongTienPhong
    FROM ChiTietDatPhong ct
    WHERE ct.MaDP = dp.MaDP
) phong
    OUTER APPLY (
        SELECT SUM(sd.SoLuong * dv.DonGia) AS TongTienDichVu
        FROM SuDungDichVu sd
        INNER JOIN DichVu dv ON sd.MaDV = dv.MaDV
        WHERE sd.MaDP = dp.MaDP
    ) dichvu
    OUTER APPLY (
        SELECT SUM(SoTienCoc) AS TongDaCoc
        FROM DatCoc
        WHERE MaDP = dp.MaDP AND TrangThaiCoc = N'Đã đặt cọc'
    ) datcoc
    OUTER APPLY (
        SELECT SUM(SoTien) AS TongChiPhiPhatSinh
        FROM ChiPhiPhatSinh
        WHERE MaDP = dp.MaDP
    ) phatsinh
    WHERE kh.MaTK = @MaTK
    ORDER BY dp.NgayDat DESC;
END;

/*===============================================
-- viết proc xem tất cả danh sách đặt phòng (nvnv)
=================================================*/
CREATE  PROC pr_hienthidanhsachdatphong_all
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        dp.MaDP,
        dp.NgayDat,
        dp.NgayNhan,
        dp.NgayTra,
        dp.TrangThaiDat,
        kh.HoTen AS TenKhachHang,  -- thêm tên khách hàng
        STUFF((SELECT ', ' + p.TenPhong
               FROM ChiTietDatPhong ct
               INNER JOIN Phong p ON ct.MaPhong = p.MaPhong
               WHERE ct.MaDP = dp.MaDP
               FOR XML PATH('')), 1, 2, '') AS DanhSachPhong,
        ISNULL(phong.TongTienPhong, 0) AS TongTienPhong,
        ISNULL(dichvu.TongTienDichVu, 0) AS TongTienDichVu,
        ISNULL(datcoc.TongDaCoc, 0) AS TongDaCoc,
        ISNULL(phatsinh.TongChiPhiPhatSinh, 0) AS TongChiPhiPhatSinh
    FROM DatPhong dp
    INNER JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    CROSS APPLY (SELECT DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS SoNgay) ngay
    OUTER APPLY (
        SELECT SUM(ct.DonGia) * ngay.SoNgay AS TongTienPhong
        FROM ChiTietDatPhong ct
        WHERE ct.MaDP = dp.MaDP
    ) phong
    OUTER APPLY (
        SELECT SUM(sd.SoLuong * dv.DonGia) AS TongTienDichVu
        FROM SuDungDichVu sd
        INNER JOIN DichVu dv ON sd.MaDV = dv.MaDV
        WHERE sd.MaDP = dp.MaDP
    ) dichvu
    OUTER APPLY (
        SELECT SUM(SoTienCoc) AS TongDaCoc
        FROM DatCoc
        WHERE MaDP = dp.MaDP AND TrangThaiCoc = N'Đã đặt cọc'
    ) datcoc
    OUTER APPLY (
        SELECT SUM(SoTien) AS TongChiPhiPhatSinh
        FROM ChiPhiPhatSinh
        WHERE MaDP = dp.MaDP
    ) phatsinh
    ORDER BY dp.NgayDat DESC;
END;
	


/*===================================
    Viết proc xem chi tiết đặt phòng của khách hàng
    =====================================*/

CREATE OR ALTER PROCEDURE sp_ThongTinDatPhong
    @MaDP INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        dp.MaDP,
        dp.NgayDat,
        dp.NgayNhan,
        dp.NgayTra,
        dp.TrangThaiDat,
        dp.GhiChu,
        kh.HoTen,
        kh.CCCD,
        kh.SoDienThoai,
        kh.Email,
        kh.DiaChi,
        
        -- SỬA LỖI TẠI ĐÂY: Tính tổng đơn giá rồi mới nhân với số ngày chênh lệch
        (SELECT SUM(ct.DonGia) * DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra)
         FROM ChiTietDatPhong ct 
         WHERE ct.MaDP = dp.MaDP) AS TongTienPhong,

        -- Tổng tiền dịch vụ (Giữ nguyên vì không bị lỗi outer reference phức tạp)
        (SELECT SUM(sd.SoLuong * dv.DonGia)
         FROM SuDungDichVu sd 
         JOIN DichVu dv ON sd.MaDV = dv.MaDV
         WHERE sd.MaDP = dp.MaDP) AS TongTienDichVu,

        -- Tổng đã cọc
        (SELECT ISNULL(SUM(SoTienCoc), 0) 
         FROM DatCoc 
         WHERE MaDP = dp.MaDP AND TrangThaiCoc = N'Đã đặt cọc') AS TongDaCoc,

        -- Tổng chi phí phát sinh
        (SELECT ISNULL(SUM(SoTien), 0) 
         FROM ChiPhiPhatSinh 
         WHERE MaDP = dp.MaDP) AS TongChiPhiPhatSinh
         
    FROM DatPhong dp
    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    WHERE dp.MaDP = @MaDP;
END;
exec sp_ThongTinDatPhong @MaDP = 1; -- Thay 1 bằng mã đặt phòng cần xem chi tiết
/*===============================================
viết proc khách hàng yêu cầu huỷ
=================================================*/

CREATE  PROCEDURE sp_KhachHangYeuCauHuy
    @MaDP INT,
    @LyDo NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    -- Kiểm tra đơn có tồn tại và chưa bị hủy/trả phòng (tùy chọn, có thể kiểm tra thêm)
    DECLARE @TrangThaiDat NVARCHAR(30);
    SELECT @TrangThaiDat = TrangThaiDat FROM DatPhong WHERE MaDP = @MaDP;
    IF @TrangThaiDat IN (N'Hủy', N'Đã trả phòng', N'Đã nhận phòng')
    BEGIN
        RAISERROR(N'Đơn đặt phòng không thể yêu cầu hủy ở trạng thái hiện tại.', 16, 1);
        RETURN;
    END

    -- Chèn yêu cầu hủy
    INSERT INTO YeuCauHuy (MaDP, LyDo, NgayYeuCau, TrangThai)
    VALUES (@MaDP, @LyDo, GETDATE(), N'Chờ duyệt');

    SELECT SCOPE_IDENTITY() AS MaYeuCau;
END;
/*==============================================================
viết proc lấy danh sách yêu cầu huỷ của khách hàng bên nhân viên và quản lý
  ===============================================================*/
CREATE PROCEDURE sp_LayDanhSachYeuCauHuy
    @TrangThai NVARCHAR(30) = NULL -- 'Chờ duyệt', 'Đã duyệt', 'Từ chối' (NULL: tất cả)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        yc.MaYeuCau,
        yc.MaDP,
        yc.LyDo,
        yc.NgayYeuCau,
        yc.TrangThai AS TrangThaiYeuCau,
        kh.HoTen AS TenKhachHang,
        kh.SoDienThoai,
        dp.NgayNhan,
        dp.NgayTra,
        dp.TrangThaiDat AS TrangThaiDatPhong,
        ISNULL(dc.TongDaCoc, 0) AS TongDaCoc
    FROM YeuCauHuy yc
    INNER JOIN DatPhong dp ON yc.MaDP = dp.MaDP
    INNER JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    OUTER APPLY (
        SELECT SUM(SoTienCoc) AS TongDaCoc 
        FROM DatCoc 
        WHERE MaDP = dp.MaDP AND TrangThaiCoc = N'Đã đặt cọc'
    ) dc
    WHERE (@TrangThai IS NULL OR yc.TrangThai = @TrangThai)
    ORDER BY yc.NgayYeuCau DESC;
END;
/*===================================
viết proc xử lý yêu cầu huỷ của nhân viên và quản lý
    =====================================*/
Create PROCEDURE sp_XuLyYeuCauHuy
    @MaYeuCau INT,
    @Duyet BIT,
    @MaNV INT,
    @LyDoTuChoi NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @MaDP INT, @TrangThaiHienTai NVARCHAR(30);

        SELECT @MaDP = MaDP, @TrangThaiHienTai = TrangThai 
        FROM YeuCauHuy WHERE MaYeuCau = @MaYeuCau;

        IF @TrangThaiHienTai != N'Chờ duyệt'
        BEGIN
            RAISERROR(N'Yêu cầu này đã được xử lý trước đó.', 16, 1);
            RETURN;
        END

        IF @Duyet = 1
        BEGIN
            EXEC sp_HuyDatPhong 
                @MaDP, 
                @LyDo = N'Khách yêu cầu hủy (đã duyệt)', 
                @MaNV = @MaNV;

            UPDATE YeuCauHuy 
            SET TrangThai = N'Đã duyệt' 
            WHERE MaYeuCau = @MaYeuCau;
        END
        ELSE
        BEGIN
            UPDATE YeuCauHuy 
            SET TrangThai = N'Từ chối', 
                GhiChu = @LyDoTuChoi 
            WHERE MaYeuCau = @MaYeuCau;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
/*===========================================
xử lý yêu cầu huỷ đặt phòng của nhân viên và quản lý
==============================================*/
CREATE  PROCEDURE sp_HuyDatPhong
    @MaDP INT,
    @LyDo NVARCHAR(255) = NULL,
    @MaNV INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Kiểm tra trạng thái cho phép hủy
        DECLARE @TrangThai NVARCHAR(30), @NgayNhan DATE;
        SELECT @TrangThai = TrangThaiDat, @NgayNhan = NgayNhan FROM DatPhong WHERE MaDP = @MaDP;
        IF @TrangThai NOT IN (N'Chờ duyệt', N'Đã đặt', N'Đã đặt cọc') OR @NgayNhan <= CAST(GETDATE() AS DATE)
        BEGIN
            RAISERROR(N'Không thể hủy đơn này do trạng thái hoặc đã quá hạn.', 16, 1);
            RETURN;
        END

        -- Cập nhật trạng thái đơn
        UPDATE DatPhong SET TrangThaiDat = N'Hủy', GhiChu = ISNULL(@LyDo, GhiChu) WHERE MaDP = @MaDP;

        -- Giải phóng phòng
        UPDATE p SET TrangThai = N'Trống'
        FROM Phong p
        INNER JOIN ChiTietDatPhong ct ON p.MaPhong = ct.MaPhong
        WHERE ct.MaDP = @MaDP;

        -- Xử lý cọc: hoàn trả (có thể tùy chính sách)
        UPDATE DatCoc SET TrangThaiCoc = N'Hoàn trả' WHERE MaDP = @MaDP AND TrangThaiCoc = N'Đã đặt cọc';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
/*===================================
 tìm kiếm phòng để CheckInt cho khách
    =====================================*/

CREATE  PROCEDURE sp_TimKiemDonDatPhong
    @TuKhoa NVARCHAR(100) = NULL,          -- Tìm theo tên, sđt, CCCD, mã đặt
    @TrangThai NVARCHAR(30) = N'Đã đặt cọc' -- Chỉ lấy đơn có thể check‑in
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        dp.MaDP,
        kh.HoTen,
        kh.SoDienThoai,
        kh.CCCD,
        dp.NgayNhan,
        dp.NgayTra,
        dp.TrangThaiDat,
        STUFF((SELECT ', ' + p.TenPhong FROM ChiTietDatPhong ct 
               JOIN Phong p ON ct.MaPhong = p.MaPhong WHERE ct.MaDP = dp.MaDP FOR XML PATH('')),1,2,'') AS DanhSachPhong
    FROM DatPhong dp
    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    WHERE dp.TrangThaiDat IN (N'Đã đặt cọc', N'Đã đặt')
      AND dp.NgayNhan <= CAST(GETDATE() AS DATE)
      AND (
          @TuKhoa IS NULL 
          OR kh.HoTen LIKE N'%' + @TuKhoa + '%'
          OR kh.SoDienThoai LIKE '%' + @TuKhoa + '%'
          OR kh.CCCD LIKE '%' + @TuKhoa + '%'
          OR CAST(dp.MaDP AS NVARCHAR) = @TuKhoa
      )
    ORDER BY dp.NgayNhan;
END;

/*===================================
viết proc xử lý CheckInt
    =====================================*/

CREATE PROCEDURE sp_CheckIn
    @MaDP INT,
    @MaNV INT
AS
BEGIN
   SET NOCOUNT OFF;

SELECT 1 AS Success;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @TrangThai NVARCHAR(30), @NgayNhan DATE;
        SELECT @TrangThai = TrangThaiDat, @NgayNhan = NgayNhan FROM DatPhong WHERE MaDP = @MaDP;
        
        IF @TrangThai NOT IN (N'Đã đặt cọc', N'Đã đặt')
            THROW 50001, N'Chỉ có thể check‑in đơn ở trạng thái "Đã đặt cọc" hoặc "Đã đặt".', 1;
        IF @NgayNhan > CAST(GETDATE() AS DATE)
            THROW 50002, N'Chưa đến ngày nhận phòng, không thể check‑in.', 1;
        
        -- Cập nhật chỉ khi vượt qua kiểm tra
        UPDATE DatPhong SET TrangThaiDat = N'Đã nhận phòng', MaNV = @MaNV WHERE MaDP = @MaDP;
        UPDATE p SET TrangThai = N'Đã thuê'
        FROM Phong p
        INNER JOIN ChiTietDatPhong ct ON p.MaPhong = ct.MaPhong
        WHERE ct.MaDP = @MaDP;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;

/*===================================
viết proc để tìm kiếm đơn đặt phòng cần  CheckOut 
    =====================================*/
--timkiem checkOut
CREATE PROCEDURE sp_TimKiemDonDangO
    @TuKhoa NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        dp.MaDP,
        kh.HoTen,
        kh.SoDienThoai,
        kh.CCCD,
        dp.NgayNhan,
        dp.NgayTra,
        STUFF((SELECT ', ' + p.TenPhong FROM ChiTietDatPhong ct 
               JOIN Phong p ON ct.MaPhong = p.MaPhong WHERE ct.MaDP = dp.MaDP FOR XML PATH('')),1,2,'') AS DanhSachPhong
    FROM DatPhong dp
    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    WHERE dp.TrangThaiDat = N'Đã nhận phòng'
      AND (@TuKhoa IS NULL 
           OR kh.HoTen LIKE N'%' + @TuKhoa + '%'
           OR kh.SoDienThoai LIKE '%' + @TuKhoa + '%'
           OR kh.CCCD LIKE '%' + @TuKhoa + '%'
           OR CAST(dp.MaDP AS NVARCHAR) = @TuKhoa)
    ORDER BY dp.NgayNhan;
END;
/*===================================
viết proc xử lý CheckOut
    =====================================*/
-- proc checkOut
CREATE  PROCEDURE sp_CheckOut
    @MaDP INT,
    @SoTienTra DECIMAL(18,2),
    @PhuongThuc NVARCHAR(50),
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Kiểm tra đơn đang ở trạng thái 'Đã nhận phòng'
        IF NOT EXISTS (SELECT 1 FROM DatPhong WHERE MaDP = @MaDP AND TrangThaiDat = N'Đã nhận phòng')
            THROW 50001, N'Đơn này chưa check‑in hoặc đã check‑out.', 1;

        -- Tính tổng tiền phòng
        DECLARE @TongTienPhong DECIMAL(18,2);
        SELECT @TongTienPhong = SUM(ct.DonGia * DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra))
        FROM ChiTietDatPhong ct
        JOIN DatPhong dp ON ct.MaDP = dp.MaDP
        WHERE ct.MaDP = @MaDP;

        -- Tính tổng tiền dịch vụ
        DECLARE @TongTienDichVu DECIMAL(18,2);
        SELECT @TongTienDichVu = SUM(sd.SoLuong * dv.DonGia)
        FROM SuDungDichVu sd
        JOIN DichVu dv ON sd.MaDV = dv.MaDV
        WHERE sd.MaDP = @MaDP;

        -- Tính tổng chi phí phát sinh
        DECLARE @TongChiPhi DECIMAL(18,2);
        SELECT @TongChiPhi = SUM(SoTien) FROM ChiPhiPhatSinh WHERE MaDP = @MaDP;

        -- Tính tổng tiền đã đặt cọc (chỉ tính các lần cọc thành công)
        DECLARE @TongCoc DECIMAL(18,2);
        SELECT @TongCoc = SUM(SoTienCoc) FROM DatCoc WHERE MaDP = @MaDP AND TrangThaiCoc = N'Đã đặt cọc';

        DECLARE @TongCong DECIMAL(18,2) = ISNULL(@TongTienPhong,0) + ISNULL(@TongTienDichVu,0) + ISNULL(@TongChiPhi,0);
        DECLARE @ConNo DECIMAL(18,2) = @TongCong - ISNULL(@TongCoc,0) - @SoTienTra;

        -- Tạo hóa đơn nếu chưa có
        DECLARE @MaHD INT;
        SELECT @MaHD = MaHD FROM HoaDon WHERE MaDP = @MaDP;
        IF @MaHD IS NULL
        BEGIN
            INSERT INTO HoaDon (MaDP, NgayLap, TrangThaiThanhToan)
            VALUES (@MaDP, GETDATE(), N'Chưa thanh toán');
            SET @MaHD = SCOPE_IDENTITY();
        END

        -- Ghi nhận thanh toán lần cuối
        INSERT INTO ThanhToan (MaHD, SoTien, PhuongThuc, NgayThanhToan, GhiChu)
        VALUES (@MaHD, @SoTienTra, @PhuongThuc, GETDATE(), N'Thanh toán tại quầy');

        -- Nếu đã thanh toán đủ, cập nhật trạng thái hóa đơn
        IF @ConNo <= 0
            UPDATE HoaDon SET TrangThaiThanhToan = N'Đã thanh toán' WHERE MaHD = @MaHD;

        -- Cập nhật trạng thái đơn và giải phóng phòng
        UPDATE DatPhong SET TrangThaiDat = N'Đã trả phòng', MaNV = @MaNV WHERE MaDP = @MaDP;

        UPDATE p SET TrangThai = N'Trống'
        FROM Phong p
        INNER JOIN ChiTietDatPhong ct ON p.MaPhong = ct.MaPhong
        WHERE ct.MaDP = @MaDP;

        COMMIT TRANSACTION;

        -- Trả về thông tin để hiển thị trên form
        SELECT @MaHD AS MaHD, @TongCong AS TongCong, ISNULL(@TongCoc,0) AS DaCoc, @SoTienTra AS VuaTra, @ConNo AS ConNo;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;



-- in hoá đơn 
CREATE OR ALTER PROCEDURE sp_LayHoaDonChiTiet @MaHD INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Thông tin hóa đơn, đặt phòng, khách hàng
    SELECT 
        hd.MaHD, hd.NgayLap, hd.TrangThaiThanhToan,
        dp.MaDP, dp.NgayNhan, dp.NgayTra,
        DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS SoDem,
        kh.HoTen, kh.SoDienThoai, kh.DiaChi, kh.CCCD,
        ISNULL((SELECT SUM(ct.DonGia) FROM ChiTietDatPhong ct WHERE ct.MaDP = dp.MaDP), 0) 
            * DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS TongTienPhong,
        ISNULL((SELECT SUM(sd.SoLuong * dv.DonGia) FROM SuDungDichVu sd JOIN DichVu dv ON sd.MaDV = dv.MaDV WHERE sd.MaDP = dp.MaDP), 0) AS TongTienDichVu,
        ISNULL((SELECT SUM(SoTien) FROM ChiPhiPhatSinh WHERE MaDP = dp.MaDP), 0) AS TongChiPhi,
        ISNULL((SELECT SUM(SoTienCoc) FROM DatCoc WHERE MaDP = dp.MaDP AND TrangThaiCoc = N'Đã đặt cọc'), 0) AS DaCoc
    FROM HoaDon hd
    JOIN DatPhong dp ON hd.MaDP = dp.MaDP
    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    WHERE hd.MaHD = @MaHD;

    -- 2. Chi tiết phòng đã đặt
    SELECT 
        p.TenPhong, lp.TenLoai, ct.DonGia,
        DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS SoNgay,
        ct.DonGia * DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS ThanhTien
    FROM ChiTietDatPhong ct
    JOIN Phong p ON ct.MaPhong = p.MaPhong
    JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
    JOIN DatPhong dp ON ct.MaDP = dp.MaDP
    WHERE dp.MaDP = (SELECT MaDP FROM HoaDon WHERE MaHD = @MaHD);

    -- 3. Chi tiết dịch vụ sử dụng (đã thêm DonViTinh)
    SELECT 
        dv.TenDV, dv.DonGia, sd.SoLuong, dv.DonViTinh, sd.NgaySuDung,
        sd.SoLuong * dv.DonGia AS ThanhTien
    FROM SuDungDichVu sd
    JOIN DichVu dv ON sd.MaDV = dv.MaDV
    WHERE sd.MaDP = (SELECT MaDP FROM HoaDon WHERE MaHD = @MaHD);

    -- 4. Chi phí phát sinh
    SELECT TenChiPhi, SoTien, NgayPhatSinh, GhiChu
    FROM ChiPhiPhatSinh
    WHERE MaDP = (SELECT MaDP FROM HoaDon WHERE MaHD = @MaHD);

    -- 5. Lịch sử thanh toán
    SELECT tt.SoTien, tt.PhuongThuc, tt.NgayThanhToan, tt.GhiChu
    FROM ThanhToan tt
    JOIN HoaDon hd ON tt.MaHD = hd.MaHD
    WHERE hd.MaHD = @MaHD;
END;

-- ======================viết proc chi tiết dịch vụ theo hoá đơn=============
--===========================================================================
create proc sp_ChiTietDichVu1

as 
begin
set nocount on;
select hd.MaHD, dv.TenDV,dv.DonGia,sd.SoLuong,dv.DonViTinh,sd.NgaySuDung ,sd.SoLuong*dv.DonGia as ThanhTien from 
DichVu dv
join SuDungDichVu sd on dv.MaDV=sd.MaDV 

join DatPhong dp on dp.MaDP=sd.MaDP
join HoaDon hd on dp.MaDP=hd.MaDP

end;

-- Chi tiết phòng
create PROC sp_ChiTietPhongTheoHoaDon 
AS
BEGIN
    SELECT 
    hd.MaHD,
        p.TenPhong,
        lp.TenLoai,
        ct.DonGia,
        DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS SoNgay,
        ct.DonGia * DATEDIFF(DAY, dp.NgayNhan, dp.NgayTra) AS ThanhTien
    FROM ChiTietDatPhong ct
    JOIN Phong p ON ct.MaPhong = p.MaPhong
    JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
    JOIN DatPhong dp ON ct.MaDP = dp.MaDP
    JOIN HoaDon hd ON dp.MaDP = hd.MaDP

END;

-- Chi phí phát sinh
CREATE PROC sp_ChiPhiPhatSinhTheoHoaDon @MaHD INT
AS
BEGIN
    SELECT TenChiPhi, SoTien, NgayPhatSinh, GhiChu
    FROM ChiPhiPhatSinh
    WHERE MaDP = (SELECT MaDP FROM HoaDon WHERE MaHD = @MaHD);
END;

-- Lịch sử thanh toán
CREATE PROC sp_LichSuThanhToanTheoHoaDon @MaHD INT
AS
BEGIN
    SELECT tt.SoTien, tt.PhuongThuc, tt.NgayThanhToan, tt.GhiChu
    FROM ThanhToan tt
    JOIN HoaDon hd ON tt.MaHD = hd.MaHD
    WHERE hd.MaHD = @MaHD;
END;


-- create proc phiếu đặt cọc
CREATE OR ALTER PROCEDURE sp_PhieuDatCoc
    @MaDP INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Lấy số ngày (để dùng chung)
    DECLARE @SoNgay INT;
    SELECT @SoNgay = DATEDIFF(DAY, NgayNhan, NgayTra) 
    FROM DatPhong 
    WHERE MaDP = @MaDP;

    -- 1. Thông tin chính và cọc
    SELECT 
        dp.MaDP,
        dp.NgayDat,
        dp.NgayNhan,
        dp.NgayTra,
        @SoNgay AS SoDem,
        kh.HoTen,
        kh.SoDienThoai,
        kh.Email,
        kh.DiaChi,
        kh.CCCD,
        dc.SoTienCoc,
        dc.NgayCoc,
        dc.GhiChu AS GhiChuCoc,
        
        -- Tổng tiền phòng dự kiến 
        ISNULL(TienPhong.TongGiaTheoNgay, 0) AS TongTienPhong,
        
        -- Tính tỷ lệ cọc (tránh chia cho 0)
        CASE 
            WHEN ISNULL(TienPhong.TongGiaTheoNgay,0) = 0 THEN 0
            ELSE (dc.SoTienCoc / TienPhong.TongGiaTheoNgay)
        END AS TyLeCoc

    FROM DatPhong dp
    JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    JOIN DatCoc dc ON dp.MaDP = dc.MaDP
    -- Dùng OUTER APPLY để tính tổng giá phòng 1 lần duy nhất để tái sử dụng
    OUTER APPLY (
        SELECT ISNULL(SUM(ct.DonGia), 0) * @SoNgay AS TongGiaTheoNgay
        FROM ChiTietDatPhong ct
        WHERE ct.MaDP = dp.MaDP
    ) AS TienPhong
    WHERE dp.MaDP = @MaDP
      AND dc.TrangThaiCoc = N'Đã đặt cọc';

    -- 2. Danh sách phòng đã đặt
    SELECT 
        p.TenPhong,
        lp.TenLoai,
        ct.DonGia,
        @SoNgay AS SoNgay,
        ct.DonGia * @SoNgay AS ThanhTien
    FROM ChiTietDatPhong ct
    JOIN Phong p ON ct.MaPhong = p.MaPhong
    JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
    JOIN DatPhong dp ON ct.MaDP = dp.MaDP
    WHERE dp.MaDP = @MaDP;
END;
-- tạo proc tìm kiếm phòng
create proc sp_TimKiemPhong
    @TuKhoa NVARCHAR(100) = NULL
    AS
    BEGIN
    SET NOCOUNT ON;
        SELECT 
            p.MaPhong,
            p.TenPhong,
            lp.TenLoai,
            p.TrangThai,
            lp.DonGiaTheoNgay,
            lp.SoLuongNguoi,
            p.GhiChu
        FROM Phong p
        JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
        WHERE @TuKhoa IS NULL 
           OR p.TenPhong LIKE N'%' + @TuKhoa + '%'
           OR lp.TenLoai LIKE N'%' + @TuKhoa + '%'
           OR p.TrangThai LIKE N'%' + @TuKhoa + '%'
    END;
  
         -- tạo proc xem thông tin phòng và loại phòng bằng mã LoaiPhong
                  CREATE OR ALTER PROCEDURE sp_XemThongTinPhong
                  @MaLoai INT
                  AS
                  BEGIN
                  SELECT
                  p.MaPhong,
                  p.TenPhong,
                  p.TrangThai,
                  p.GhiChu,
                  lp.TenLoai,
                  lp.MoTa,
                  lp.DonGiaTheoNgay,
                  lp.SoLuongNguoi
                  FROM Phong p
                  JOIN LoaiPhong lp ON p.MaLoai = lp.MaLoai
                  WHERE lp.MaLoai = @MaLoai;
                  END;
                  -- chạy proc trên
                  
--========================== Viết proc tìm kiếm dịch vụ ====================================

CREATE OR ALTER PROCEDURE sp_TimKiemDichVu
    @TuKhoa NVARCHAR(100) = NULL
    AS
    BEGIN
    SET NOCOUNT ON;
        SELECT 
            dv.MaDV,
            dv.TenDV,
            dv.DonGia,
            dv.DonViTinh
        FROM DichVu dv
        WHERE @TuKhoa IS NULL 
           OR dv.TenDV LIKE N'%' + @TuKhoa + '%'
           OR dv.DonViTinh LIKE N'%' + @TuKhoa + '%'
    END;

--===================== Kết thúc ======================================
--===================== viết proc lấy danh sách hoá đơn================
CREATE OR ALTER PROCEDURE sp_LayDanhSachHoaDon
    @TuNgay DATE = NULL,
    @DenNgay DATE = NULL,
    @TrangThai NVARCHAR(30) = NULL,
    @TuKhoa NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        hd.MaHD,
        hd.NgayLap,
        hd.TrangThaiThanhToan,
        dp.MaDP,
        kh.HoTen,
        kh.SoDienThoai,
        -- Tổng đã thanh toán (cọc + thanh toán tại quầy)
        ISNULL((
            SELECT SUM(tt.SoTien)
            FROM ThanhToan tt
            WHERE tt.MaHD = hd.MaHD
        ), 0) 
        + ISNULL((
            SELECT SUM(dc.SoTienCoc)
            FROM DatCoc dc
            WHERE dc.MaDP = dp.MaDP AND dc.TrangThaiCoc = N'Đã đặt cọc'
        ), 0) AS TongDaThanhToan,
        -- Tổng tiền phải trả (giữ nguyên)
        ISNULL((
            SELECT SUM(ct.DonGia * DATEDIFF(DAY, dp2.NgayNhan, dp2.NgayTra))
            FROM ChiTietDatPhong ct
            JOIN DatPhong dp2 ON ct.MaDP = dp2.MaDP
            WHERE dp2.MaDP = dp.MaDP
        ), 0)
        + ISNULL((
            SELECT SUM(sd.SoLuong * dv.DonGia)
            FROM SuDungDichVu sd
            JOIN DichVu dv ON sd.MaDV = dv.MaDV
            JOIN DatPhong dp2 ON sd.MaDP = dp2.MaDP
            WHERE dp2.MaDP = dp.MaDP
        ), 0)
        + ISNULL((
            SELECT SUM(cps.SoTien)
            FROM ChiPhiPhatSinh cps
            WHERE cps.MaDP = dp.MaDP
        ), 0) AS TongCong
    FROM HoaDon hd
    INNER JOIN DatPhong dp ON hd.MaDP = dp.MaDP
    INNER JOIN KhachHang kh ON dp.MaKH = kh.MaKH
    WHERE (@TuNgay IS NULL OR hd.NgayLap >= @TuNgay)
      AND (@DenNgay IS NULL OR hd.NgayLap <= @DenNgay)
      AND (@TrangThai IS NULL OR hd.TrangThaiThanhToan = @TrangThai)
      AND (@TuKhoa IS NULL 
           OR CAST(hd.MaHD AS NVARCHAR) LIKE N'%' + @TuKhoa + '%'
           OR kh.HoTen LIKE N'%' + @TuKhoa + '%'
           OR CAST(dp.MaDP AS NVARCHAR) LIKE N'%' + @TuKhoa + '%'
           OR kh.SoDienThoai LIKE N'%' + @TuKhoa + '%')
    ORDER BY hd.NgayLap DESC;
END;
--====================== Viết proc tìm kiếm tất cả đơn đặt phòng trạng thái đang thuê =========================
CREATE OR ALTER PROCEDURE sp_TimKiemDonDangThue
    @TuKhoa NVARCHAR(100) = NULL
    AS
    BEGIN
    SET NOCOUNT ON;
    SELECT 
            dp.MaDP,
            kh.HoTen,
            kh.SoDienThoai,
            kh.CCCD,
            dp.NgayNhan,
            dp.NgayTra,
            STUFF((SELECT ', ' + p.TenPhong FROM ChiTietDatPhong ct 
                   JOIN Phong p ON ct.MaPhong = p.MaPhong WHERE ct.MaDP = dp.MaDP FOR XML PATH('')),1,2,'') AS DanhSachPhong
        FROM DatPhong dp
        JOIN KhachHang kh ON dp.MaKH = kh.MaKH
        WHERE dp.TrangThaiDat = N'Đã nhận phòng'
          AND (@TuKhoa IS NULL 
               OR kh.HoTen LIKE N'%' + @TuKhoa + '%'
               OR kh.SoDienThoai LIKE '%' + @TuKhoa + '%'
               OR kh.CCCD LIKE '%' + @TuKhoa + '%'
               OR CAST(dp.MaDP AS NVARCHAR) = @TuKhoa)
    END;
    
   --====================== Viết proc gia hạn phòng =========================
CREATE OR ALTER PROCEDURE sp_GiaHanPhong
    @MaDP INT,
    @NgayTraMoi DATE,
    @MaNV INT,
    @SoTienTang DECIMAL(18,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @NgayTraCu DATE, @DonGia DECIMAL(18,2);
        SELECT @NgayTraCu = NgayTra FROM DatPhong WHERE MaDP = @MaDP;
        IF @NgayTraMoi <= @NgayTraCu
            THROW 50001, N'Ngày trả mới phải lớn hơn ngày trả cũ.', 1;
        
        SELECT @DonGia = SUM(DonGia) FROM ChiTietDatPhong WHERE MaDP = @MaDP;
        DECLARE @SoNgayTang INT = DATEDIFF(DAY, @NgayTraCu, @NgayTraMoi);
        SET @SoTienTang = @SoNgayTang * @DonGia;
        
        -- Cập nhật ngày trả (không ghi đè MaNV)
        UPDATE DatPhong SET NgayTra = @NgayTraMoi WHERE MaDP = @MaDP;
        
        -- KHÔNG chèn vào ChiPhiPhatSinh nữa
        -- Chỉ ghi nhận lịch sử gia hạn (nếu có bảng LichSuGiaHan)
        INSERT INTO LichSuGiaHan (MaDP, MaNV, NgayTraCu, NgayTraMoi, SoTienTang)
        VALUES (@MaDP, @MaNV, @NgayTraCu, @NgayTraMoi, @SoTienTang);
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
--====================== Viết proc thêm chi phí phát sinh =========================
CREATE OR ALTER PROCEDURE sp_ThemChiPhiPhatSinh
    @MaDP INT,
    @TenChiPhi NVARCHAR(100),
    @SoTien DECIMAL(18,2),
    @GhiChu NVARCHAR(255) = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM DatPhong WHERE MaDP = @MaDP AND TrangThaiDat = N'Đã nhận phòng')
        THROW 50001, N'Chỉ có thể thêm chi phí phát sinh cho đơn đang ở.', 1;
    INSERT INTO ChiPhiPhatSinh (MaDP, TenChiPhi, SoTien, NgayPhatSinh, GhiChu)
    VALUES (@MaDP, @TenChiPhi, @SoTien, GETDATE(), @GhiChu);
END;
SELECT name FROM sys.procedures WHERE name LIKE '%YeuCauHuy%';
select * from HoaDon;
select * from DatPhong;
--============================
CREATE OR ALTER PROCEDURE sp_thongkedoanhthu
    @TuNgay NVARCHAR(20) = NULL,
    @DenNgay NVARCHAR(20) = NULL,
    @TrongNgay NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        hd.MaHD,
        hd.NgayLap,
        hd.TrangThaiThanhToan,
        dp.MaDP,
        kh.HoTen,
        kh.SoDienThoai,

        ISNULL((
            SELECT SUM(tt.SoTien)
            FROM ThanhToan tt
            WHERE tt.MaHD = hd.MaHD
        ), 0) 
        + ISNULL((
            SELECT SUM(dc.SoTienCoc)
            FROM DatCoc dc
            WHERE dc.MaDP = dp.MaDP 
              AND dc.TrangThaiCoc = N'Đã đặt cọc'
        ), 0) AS TongDaThanhToan,

        ISNULL((
            SELECT SUM(ct.DonGia * DATEDIFF(DAY, dp2.NgayNhan, dp2.NgayTra))
            FROM ChiTietDatPhong ct
            JOIN DatPhong dp2 ON ct.MaDP = dp2.MaDP
            WHERE dp2.MaDP = dp.MaDP
        ), 0)
        + ISNULL((
            SELECT SUM(sd.SoLuong * dv.DonGia)
            FROM SuDungDichVu sd
            JOIN DichVu dv ON sd.MaDV = dv.MaDV
            JOIN DatPhong dp2 ON sd.MaDP = dp2.MaDP
            WHERE dp2.MaDP = dp.MaDP
        ), 0)
        + ISNULL((
            SELECT SUM(cps.SoTien)
            FROM ChiPhiPhatSinh cps
            WHERE cps.MaDP = dp.MaDP
        ), 0) AS TongCong
        
    FROM HoaDon hd
    INNER JOIN DatPhong dp ON hd.MaDP = dp.MaDP
    INNER JOIN KhachHang kh ON dp.MaKH = kh.MaKH

    WHERE 
    (
        @TrongNgay IS NOT NULL AND @TrongNgay <> ''
        AND CAST(hd.NgayLap AS DATE) = TRY_CONVERT(DATE, @TrongNgay, 23)
    )
    OR
    (
        (@TrongNgay IS NULL OR @TrongNgay = '')
        AND (@TuNgay IS NULL OR @TuNgay = '' 
             OR hd.NgayLap >= TRY_CONVERT(DATE, @TuNgay, 23))
        AND (@DenNgay IS NULL OR @DenNgay = '' 
             OR hd.NgayLap < DATEADD(DAY,1, TRY_CONVERT(DATE, @DenNgay, 23)))
    )
    ORDER BY hd.NgayLap DESC;
END;UPDATE NhanVien
SET CCCD = RIGHT('000000000000' + CAST(MaNV AS VARCHAR(12)), 12)
WHERE CCCD IS NULL;

ALTER TABLE NhanVien ADD CONSTRAINT UQ_NhanVien_CCCD UNIQUE (CCCD);

ALTER TABLE NhanVien ADD CCCD VARCHAR(12) NULL;

UPDATE NhanVien SET CCCD = '012345678901' WHERE MaNV = 1;
UPDATE NhanVien SET CCCD = '023456789012' WHERE MaNV = 2;
UPDATE NhanVien SET CCCD = '034567890123' WHERE MaNV = 3;
UPDATE NhanVien SET CCCD = '045678901234' WHERE MaNV = 4;
UPDATE NhanVien SET CCCD = '056789012345' WHERE MaNV = 5;
