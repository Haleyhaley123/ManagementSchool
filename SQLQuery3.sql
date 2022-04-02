CREATE Database SchoolManagement
GO

USE SchoolManagement
GO



CREATE TABLE UserName
(PK_ID INT IDENTITY PRIMARY KEY NOT NULL,
UserName nvarchar(20) NOT NULL,
PasswordHash nvarchar(110) NOT NULL,
Email nvarchar(50) NOT NULL,
NumberPhone char(10) NOT NULL,
CreateByUser INT, 
UpdatedByUser INT,
IsDeleted BIT NOT NULL,
)

DROP TABLE Employee
CREATE TABLE Employee
(
PK_EmployeeID INT IDENTITY PRIMARY KEY NOT NULL,
ID VARCHAR(100) NOT NULL,
Name nvarchar(110) NOT NULL,
Gender NVARCHAR (50) NOT NULL,
Age INT NOT NULL,
Department NVARCHAR (100) NOT NULL,
Birthday DATE NOT NULL,
NumberPhone VARCHAR(10),
CreateByUser INT,
UpdatedByUser INT,
FOREIGN KEY (PK_EmployeeID) REFERENCES UserName (PK_ID),
FOREIGN KEY (CreateByUser) REFERENCES UserName (PK_ID)
)

CREATE TABLE Student
(
PK_StudentID INT PRIMARY KEY NOT NULL,
Name nvarchar(50)NOT NULL,
Gender tinyint NOT NULL,
Age INT NOT NULL,
Department NVARCHAR (100) NOT NULL,
Birthday DATE NOT NULL,
NumberPhone char(20),
CreateByUser int,
UpdatedByUser int,
FOREIGN KEY (PK_StudentID) REFERENCES UserName (PK_ID)
)

CREATE TABLE Employee
(
PK_EmployeeID INT PRIMARY KEY NOT NULL,
Name nvarchar(50)NOT NULL,
Gender tinyint NOT NULL,
Age INT NOT NULL,
Department NVARCHAR (100) NOT NULL,
Birthday DATE NOT NULL,
NumberPhone char(20),
CreateByUser int,
UpdatedByUser int,
FOREIGN KEY (PK_EmployeeID) REFERENCES UserName (PK_ID)
)


USE SchoolManagement
GO
--Procedure UserName
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_UserNameInsert]  --'admin12345','123456789','kimconoc@gmail.com','0367823445'
	@UserName nvarchar(20),
	@PasswordHash nvarchar(110),
	@Email nvarchar(50),
	@NumberPhone char(10)	
AS
BEGIN
 DECLARE @Result INT 
 DECLARE @PK_UsersID INT 
   SET @Result = 0
	BEGIN TRY 	
	  IF (EXISTS (SELECT 1 FROM dbo.UserName WHERE UserName = @UserName))
		BEGIN
			SET @Result = 0
		END
	  ELSE
		BEGIN
			INSERT INTO UserName (UserName,PasswordHash,Email,NumberPhone,IsDeleted)
			VALUES (@UserName, @PasswordHash, @Email, @NumberPhone,1)
			SET @Result = 1
		END
	END TRY	
	BEGIN CATCH
	  	SET @Result = -400
	END CATCH
	SELECT @Result AS ResultSql
END

--Procedure Teacher
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE sp_TeacherInsert
	@PK_TeacherID INT,
	@Name nvarchar(50),
	@Gender tinyint,
	@Age INT,
	@Department NVARCHAR (100),
	@Birthday date,
	@NumberPhone char(10)
	
AS
BEGIN
 DECLARE @Result INT,

   SET @Result = 0
	BEGIN TRY 	
	  IF (EXISTS (SELECT 1 FROM dbo.Teacher WHERE PK_TeacherID = @PK_TeacherID))
		BEGIN
			SET @Result = 0
		END
	  ELSE
		BEGIN
			INSERT INTO Teacher (PK_TeacherID, Name, Gender, Department,Birthday, NumberPhone)
			VALUES (@PK_TeacherID, @Name, @Gender, @Department,@Birthday, @NumberPhone)
			SET @Result = 1
		END
	END TRY	
	BEGIN CATCH
	  	SET @Result = -400
	END CATCH
	SELECT @Result AS ResultSql
END

--Procedure Student
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE sp_StudentInsert
	@PK_StudentID INT,
	@Name nvarchar(50),
	@Gender tinyint,
	@Age INT,
	@Department NVARCHAR (100),
	@Birthday date,
	@NumberPhone char(10)
	
AS
BEGIN
 DECLARE @Result INT 
   SET @Result = 0
	BEGIN TRY 	
	  IF (EXISTS (SELECT 1 FROM dbo.Student WHERE PK_StudentID = @PK_StudentID))
		BEGIN
			SET @Result = 0
		END
	  ELSE
		BEGIN
			INSERT INTO Student (PK_StudentID, Name, Gender, Department,Birthday, NumberPhone)
			VALUES (@PK_StudentID, @Name, @Gender, @Department,@Birthday, @NumberPhone)
			SET @Result = 1
		END
	END TRY	
	BEGIN CATCH
	  	SET @Result = -400
	END CATCH
	SELECT @Result AS ResultSql
END

--Procedure Employee
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE sp_EmployeeInsert
	@PK_EmployeeID INT,
	@Name nvarchar(50),
	@Gender tinyint,
	@Age INT,
	@Department NVARCHAR (100),
	@Birthday date,
	@NumberPhone char(10)
	
AS
BEGIN
 DECLARE @Result INT 
   SET @Result = 0
	BEGIN TRY 	
	  IF (EXISTS (SELECT 1 FROM dbo.Employee WHERE PK_EmployeeID = @PK_EmployeeID))
		BEGIN
			SET @Result = 0
		END
	  ELSE
		BEGIN
			INSERT INTO Employee(PK_EmployeeID, Name, Gender, Department,Birthday, NumberPhone)
			VALUES (@PK_EmployeeID, @Name, @Gender, @Department,@Birthday, @NumberPhone)
			SET @Result = 1
		END
	END TRY	
	BEGIN CATCH
	  	SET @Result = -400
	END CATCH
	SELECT @Result AS ResultSql
END




DROP PROC sp_EmployeeInsert

select * from Student

