CREATE DATABASE SchoolManagement
GO

USE SchoolManagement
GO

CREATE TABLE CheckAdmin 
(
PK_ID CHAR (10) PRIMARY KEY,
Name NVARCHAR (100)
)

CREATE TABLE UserName
(PK_UsersID CHAR (10) PRIMARY KEY,
IncreaseUser INT IDENTITY,
CheckAdmin CHAR (10) NOT NULL,
UserName CHAR(100),
PasswordHash CHAR(100),
Email Char(100),
NumberPhone INT,
FOREIGN KEY (CheckAdmin) REFERENCES CheckAdmin (PK_ID)
)

CREATE TABLE Teacher
(
PK_IDTeacher CHAR(10) NOT NULL PRIMARY KEY,
Name NVARCHAR (100),
Gender NVARCHAR (10),
Age INT,
Department NVARCHAR (100),
Birthday DATE,
NumberPhone INT,
CreatByUser CHAR (10) NOT NULL,
UpdatedByUser CHAR (10) NOT NULL,
FOREIGN KEY (CreatByUser) REFERENCES UserName (PK_UsersID),
FOREIGN KEY (UpdatedByUser) REFERENCES UserName (PK_UsersID)
)

CREATE TABLE Student
(
PK_IDStudent CHAR(10) NOT NULL PRIMARY KEY,
Name NVARCHAR (100),
Gender NVARCHAR (10),
Age INT,
Department NVARCHAR (100),
Birthday DATE,
NumberPhone INT,
CreatByUser CHAR (10) NOT NULL,
UpdatedByUser CHAR (10) NOT NULL,
FOREIGN KEY (CreatByUser) REFERENCES UserName (PK_UsersID),
FOREIGN KEY (UpdatedByUser) REFERENCES UserName (PK_UsersID)
)


CREATE TABLE Employee
(
PK_IDEmployee CHAR(10) NOT NULL PRIMARY KEY,
Name NVARCHAR (100),
Gender NVARCHAR (10),
Age INT,
Department NVARCHAR (100),
Birthday DATE,
NumberPhone INT,
CreatByUser CHAR (10) NOT NULL,
UpdatedByUser CHAR (10) NOT NULL,
FOREIGN KEY (CreatByUser) REFERENCES UserName (PK_UsersID),
FOREIGN KEY (UpdatedByUser) REFERENCES UserName (PK_UsersID)
)


SELECT * FROM dbo.CheckAdmin

ALTER TABLE UserName ADD isdelete NVARCHAR (10) DEFAULT '1'

INSERT INTO dbo.CheckAdmin (PK_ID, Name)
VALUES (N'CA01', N'Hà Lê')

INSERT INTO dbo.CheckAdmin (PK_ID, Name)
VALUES (N'CA02', N'Hà Lê Thị')

INSERT INTO UserName
(PK_UsersID, CheckAdmin, UserName, PasswordHash, Email, NumberPhone)
VALUES (
N'001', N'CA01', 'Haley', 'Hale4004', 'lethiha.hlu@gmail.com', '0923822526'
)
INSERT INTO UserName
(PK_UsersID, CheckAdmin, UserName, PasswordHash, Email, NumberPhone)
VALUES (
N'002', N'CA02', 'Haleyu', 'Hale40', 'lethiha.hlu@gmail.com', '0923802526'
)

INSERT INTO Teacher (PK_IDTeacher,Name,Gender,Age,Department,Birthday,NumberPhone, CreatByUser,UpdatedByUser)
VALUES (N'GV001', N'Hoàng Thị Nhung', N'Nữ', 24, N'Giáo viên', '19970419', 0992847742, N'001', N'001')

INSERT INTO Teacher (PK_IDTeacher,Name,Gender,Age,Department,Birthday,NumberPhone, CreatByUser,UpdatedByUser)
VALUES (N'GV002', N'Hoàng Thị Hường', N'Nữ', 25, N'Giáo viên', '19970410', 0992887742, N'002', N'002')


INSERT INTO Student (PK_IDStudent,Name,Gender,Age,Department,Birthday,NumberPhone, CreatByUser,UpdatedByUser)
VALUES (N'HS001', N'Hoàng Thị Nhung', N'Nữ', 24, N'Học sinh', '19970419', 0992847742, N'001', N'001')
INSERT INTO Student (PK_IDStudent,Name,Gender,Age,Department,Birthday,NumberPhone, CreatByUser,UpdatedByUser)
VALUES (N'HS002', N'Hoàng Thị Nhung', N'Nữ', 24, N'Học sinh', '19970419', 0992847742, N'001', N'001')


INSERT INTO Employee (PK_IDEmployee,Name,Gender,Age,Department,Birthday,NumberPhone, CreatByUser,UpdatedByUser)
VALUES (N'NV001', N'Hoàng Thị Nhung', N'Nữ', 24, N'Nhân viên', '19970419', 0992847742, N'001', N'001')
INSERT INTO Employee (PK_IDEmployee,Name,Gender,Age,Department,Birthday,NumberPhone, CreatByUser,UpdatedByUser)
VALUES (N'NV002', N'Hoàng Thị Nhung', N'Nữ', 24, N'Nhân viên', '19970419', 0992847742, N'001', N'001')


--Đổi tên cột
sp_rename 'Teacher.PK_IDTeacher', 'PK_ID', 'COLUMN';

sp_rename 'Student.PK_IDStudent', 'PK_ID', 'COLUMN';

sp_rename 'Employee.PK_IDEmployee', 'PK_ID', 'COLUMN';