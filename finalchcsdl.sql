USE QL_QUANAN
GO

-- xoá hoá đơn sau khi thông tin hoá đơn không còn dữ liệu (*)

CREATE TRIGGER tr_DeleteHoaDon
ON QLQA_THONGTINHOADON
AFTER DELETE
AS
BEGIN

	IF NOT EXISTS (SELECT * FROM QLQA_THONGTINHOADON t JOIN DELETED d ON t.HoaDonID = d.HoaDonID)
	BEGIN
		DELETE FROM QLQA_HOADON WHERE EXISTS (SELECT 1 FROM DELETED d WHERE d.HoaDonID = MaHoaDon)
	END
END;

-- xoá hoá đơn sau khi thông tin hoá đơn không còn dữ liệu

CREATE PROCEDURE pr_DeleteHoaDon
@MaTTHD uniqueidentifier
AS
BEGIN
	DELETE QLQA_THONGTINHOADON WHERE @MaTTHD = MaThongTinHoaDon
	IF NOT EXISTS (SELECT * FROM QLQA_THONGTINHOADON t WHERE t.HoaDonID = @MaTTHD)
	BEGIN
		DELETE FROM QLQA_HOADON WHERE @MaTTHD = MaHoaDon
	END
END

 -- update sau khi thêm thongtinhoadon (*)

CREATE TRIGGER tr_AlterInsertThongTinHoaDon
on QLQA_THONGTINHOADON
AFTER INSERT
AS 
BEGIN
	UPDATE QLQA_THONGTINHOADON 
	SET Tong = SoLuong * (SELECT Gia FROM QLQA_MONAN m WHERE m.MaMonAn = MonAnID)
END

-- get tổng hoanh thu bằng function

CREATE FUNCTION fn_TongDoanhThu
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @Tong DECIMAL(18, 2);
    SET @Tong =	(SELECT SUM(TongTien) FROM QLQA_HOADON);
    RETURN @Tong;
END;
DECLARE @KetQua DECIMAL(18, 2);
SET @KetQua = dbo.fn_TongDoanhThu();

PRINT 'Tổng doanh thu: ' + CAST(@KetQua AS NVARCHAR(50));

-- get tổng số lượng đã bán của quán

CREATE PROCEDURE sp_LaySoLuongHoaDon
AS
BEGIN
	DECLARE @Tong int;
	SET @Tong = (SELECT SUM(SoLuong) FROM QLQA_THONGTINHOADON)
	PRINT N'Tổng số lượng đã bán: ' + CAST(@Tong AS NVARCHAR(50));
END

EXEC sp_LaySoLuongHoaDon

-- get tổng số lương tài khoản đã tạo hoá đơn

CREATE PROCEDURE sp_SoLuongTaiKhoanTaoHoaDon
    @MaTaiKhoan uniqueidentifier
AS
BEGIN
    DECLARE @TongHoaDon INT;
    SELECT @TongHoaDon = COUNT(*) FROM QLQA_HOADON WHERE CreatedBy = @MaTaiKhoan;
    
    PRINT N'Tổng số lượng mà tài khoản đã tạo hoá đơn: ' + CAST(@TongHoaDon AS NVARCHAR(50));
END;
EXEC sp_SoLuongTaiKhoanTaoHoaDon 'B8C72D34-4E47-4B69-AF12-6600473B39AC'


-- select tất cả những tài khoản thuộc loại tài khoản đó 
CREATE PROCEDURE sp_SelectTaiKhoan
@TenLoaiTaiKhoan nvarchar(255)
AS
BEGIN
	SELECT MaTaiKhoan, TenLoai ,HoTen , NgaySinh, DiaChi, Email, GioiTinh 
	FROM QLQA_TAIKHOAN t JOIN QLQA_LOAITAIKHOAN l 
	ON t.LoaiTaiKhoanID = l.MaLoaiTaiKhoan 
	WHERE l.TenLoai = @TenLoaiTaiKhoan
END;

EXEC sp_SelectTaiKhoan 'administrator'

-- get số lượng tài khoản có tuổi < 25 > 18
CREATE FUNCTION fn_getSoLuongTaiKhoanTheoDoTuoi()
RETURNS INT
AS
BEGIN
    DECLARE @TongSoLuongNhanVien INT;
    SELECT @TongSoLuongNhanVien = COUNT(*) 
    FROM QLQA_TAIKHOAN 
    WHERE DATEDIFF(YEAR, NgaySinh, GETDATE()) BETWEEN 18 AND 24;

    RETURN @TongSoLuongNhanVien;
END;

DECLARE @SoLuong INT;
SET @SoLuong = dbo.fn_getSoLuongTaiKhoanTheoDoTuoi();
PRINT N'Số lượng tài khoản trong độ tuổi từ 18 đến 24 là: ' + CAST(@SoLuong AS NVARCHAR(50));