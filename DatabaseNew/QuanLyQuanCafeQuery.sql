CREATE DATABASE QuanLyQuanCafe
ON PRIMARY 
(NAME='QuanLyQuanCafe_DAT', FILENAME='E:\project\QuanLyQuanCafe\QuanLyQuanCafe\DatabaseNew\QuanLyQuanCafe_DAT.MDF')
LOG ON 
(NAME='QuanLyQuanCafe_LOG', FILENAME='E:\project\QuanLyQuanCafe\QuanLyQuanCafe\DatabaseNew\QuanLyQuanCafe_LOG.LDF')

use QuanLyQuanCafe
go

-- FoodTable
create table FoodTable(
	id int IDENTITY(1,1) PRIMARY KEY,
	TableName nvarchar(100) not null DEFAULT N'Đặt tên vào đi',
	status nvarchar(100) not null default N'Trống' -- Trống || Có người
)

insert into FoodTable values('Bàn 1', N'Trống')
insert into FoodTable values('Bàn 2', N'Có người')
insert into FoodTable values('Bàn 3', N'Trống')
insert into FoodTable values('Bàn 4', N'Trống')


-- Account
create table Account(
	Username varchar(100) PRIMARY KEY,
	FullName nvarchar(100) not null DEFAULT N'Staff',
	Password varchar(100) not null DEFAULT '123456',
	address nvarchar(100) not null,
	phone int not null,
	gender bit not null,
	TypeAccount int not null default 0 -- 1 <=> admin, 0 <=> staff
)

insert into Account values('huyhop', N'Huy Hợp', '123456', N'Hà Nội', 01648956325, 'true', 1)
insert into Account values('tuanngoc', N'Tuấn Ngọc', '123456', N'Hưng Yên', 0975853528, 'true', 1)
insert into Account values('minhduc', N'Minh Đức', '123456', N'Thanh Hóa', 095856258, 'true', 1)
insert into Account values('staff', N'Thằng Test', '123456',  N'Việt Nam', 123456789, 'false', 0)


-- FoodCategory
create table FoodCategory(
	id int IDENTITY(1,1) PRIMARY KEY,
	name nvarchar(100) not null DEFAULT N'Đặt tên vào đi'
)

insert into FoodCategory values(N'Đồ ăn')
insert into FoodCategory values(N'Đồ uống')


-- Food
create table Food(
	id int IDENTITY(1,1) PRIMARY KEY,
	name nvarchar(100) not null DEFAULT N'Đặt tên vào đi',
	price float not null DEFAULT 0,
	idFoodCategory int not null,
	constraint FK_idFoodCategory_Food foreign key(idFoodCategory) references FoodCategory(id)
)

insert into Food values(N'Cafe đen', 20000, 2)
insert into Food values(N'Xúc xích', 10000, 1)
insert into Food values(N'Cafe trắng', 52000, 2)


-- FoodOrder
create table FoodOrder(
	id int IDENTITY(1,1) PRIMARY KEY,
	TimeCheckIn date not null DEFAULT GETDATE(),
	TimeCheckOut date,
	Status int not null DEFAULT 0, -- 0: Chưa thanh toán && 1 Đã thanh toán
	idFoodTable int not null,
	totalPrice float,
	constraint FK_idFoodTable_FoodOrder foreign key(idFoodTable) references FoodTable(id)
)

insert into FoodOrder values('2018-05-15', '2018-05-15', 0, 1, 150000)
insert into FoodOrder values('2018-05-14', '2018-05-14', 1, 2, 200000)
insert into FoodOrder values('2018-05-17', '2018-05-17', 0, 3, 100000)


-- OrderDetail
create table OrderDetail(
	id int IDENTITY(1,1) PRIMARY KEY,
	idFoodOrder int not null,
	idFood int not null,
	amount int not null DEFAULT 0,
	constraint FK_idFoodOrder_OrderDetail foreign key(idFoodOrder) references FoodOrder(id),
	constraint FK_idFood_OrderDetail foreign key(idFood) references Food(id)
)

insert into OrderDetail values(5, 1, 3)
insert into OrderDetail values(4, 2, 5)



----------------- Sql query
Select * From FoodTable
Select * From Account
Select * From FoodOrder
Select * From OrderDetail
Select * From Food



-- Bảng FoodOrder thêm 2 cột là totalPrice và discount nữa!
ALTER TABLE FoodOrder ADD totalPrice Float 
ALTER TABLE FoodOrder ADD discount INT
UPDATE FoodOrder SET discount = 0
GO


select * from FoodOrder
select * from OrderDetail




-- Code Sql của Ngọc
-- Thống kê doanh thu theo ngày	
CREATE PROC SP_LayDSThongKeTheoNgay @TimeCheckIn date, @TimeCheckOut date
AS
BEGIN
	SELECT FoodTable.TableName, FoodOrder.totalPrice, TimeCheckIn, TimeCheckOut, discount
	FROM FoodTable, FoodOrder 
	WHERE FoodTable.id = FoodOrder.idFoodTable 
	AND TimeCheckIn >= @TimeCheckIn AND TimeCheckOut <= @TimeCheckOut AND FoodOrder.status = 1
END
GO

SELECT FoodTable.TableName, FoodOrder.totalPrice, TimeCheckIn, TimeCheckOut, discount 
FROM FoodTable, FoodOrder 
WHERE FoodTable.id = FoodOrder.idFoodTable AND TimeCheckIn >= '2018-05-14' AND TimeCheckOut <=  '2018-05-21' AND FoodOrder.status = 1


SELECT f.id AS 'Mã món', f.name AS 'Tên món', f.price AS 'Giá', c.name AS 'Tên danh mục' FROM Food AS f, FoodCategory AS c WHERE f.idFoodCategory = c.id


Select * From FoodCategory Where name = N'Đồ uống'

Update Food Set name = N'', idFoodCategory = , price = 0 Where id = 


SELECT f.id AS 'Mã món', f.name AS 'Tên món', f.price AS 'Giá', c.name AS 'Tên danh mục' FROM Food AS f, FoodCategory AS c WHERE f.idFoodCategory = c.id AND f.name LIKE N'%en%'


Select * From FoodCategory
Delete FoodCategory Where id = 5




