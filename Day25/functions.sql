CREATE FUNCTION GetMonthName
(
    @InputDate DATE
)
RETURNS NVARCHAR(20)
AS
BEGIN
    DECLARE @MonthName NVARCHAR(20);

    SET @MonthName = DATENAME(MONTH, @InputDate);

    RETURN @MonthName;
END;
SELECT GetMonthName('2025-03-18') AS MonthName;
------------------------------------------------------------------------
create function GetNumbersBetween
(@Start INT , @End INT)
returns @numbers table (number INT)
as
begin 
declare @i int =@start;
while @i<@End
BEGIN
        INSERT INTO @Numbers(Number) VALUES(@i);
        SET @i = @i + 1;
    END
return
end
go
select * from GetNumbersBetween(5,15) as Nums_between;
-------------------------------------------------------------------------
create function GetStudentDept
(@StudentNo INT)
returns table
as
return
( select St_Fname+' '+St_Lname as fullname ,Department.Dept_Name
from Student
join Department on Student.Dept_Id=Department.Dept_Id
where St_Id=@StudentNo)

select * from GetStudentDept(12);
-----------------------------------------------------------------------
CREATE FUNCTION dbo.GetMessageName 
(
    @StudentNo INT
)
RETURNS NVARCHAR(150)
AS
BEGIN
    DECLARE @first NVARCHAR(50);
    DECLARE @last NVARCHAR(50);
    DECLARE @message NVARCHAR(150);

    SELECT
        @first = St_Fname,
        @last = St_Lname 
    FROM Student
    WHERE St_Id = @StudentNo;

    IF (@first IS NULL AND @last IS NULL)
        SET @message = 'First name & last name are null';
    ELSE IF (@first IS NULL AND @last IS NOT NULL)
        SET @message = 'First name is null';
    ELSE IF (@first IS NOT NULL AND @last IS NULL)
        SET @message = 'Last name is null';
    ELSE
        SET @message = 'First name & last name are not null';

    RETURN @message;
END;
GO

SELECT dbo.GetMessageName(13) AS StatusMessage;
select * from Student
--------------------------------------------------------------------------

create function dbo.manager_detials
(@ManagerNo Int)
returns table 
as 
return
(select Manager_hiredate,Dept_Name from Department
where Dept_Manager=@ManagerNo)
select dbo.manager_detials(1) as details;
drop function dbo.manager_detials
----------------------------------------------------

CREATE FUNCTION dbo.GetStudentByString
(
    @String NVARCHAR(50)
)
RETURNS @Result TABLE 
(
    StudentID INT,
    StudentValue NVARCHAR(200)
)
AS
BEGIN
    INSERT INTO @Result(StudentID, StudentValue)
    SELECT 
        St_Id,
        CASE 
            WHEN @String = 'first name' THEN ISNULL(St_Fname, 'NULL')
            WHEN @String = 'last name' THEN ISNULL(St_Lname, 'NULL')
            WHEN @String = 'full name' THEN ISNULL(St_Fname, 'NULL') + ' ' + ISNULL(St_Lname, 'NULL')
            ELSE 'Invalid input'
        END
    FROM Student;

    RETURN;
END;
GO

SELECT * FROM Student;
SELECT * FROM dbo.GetStudentByString('first name');
SELECT * FROM dbo.GetStudentByString('last name');
SELECT * FROM dbo.GetStudentByString('full name');
--------------------------------------------------------------------------------------------

SELECT 
    St_Id AS StudentNo,
    LEFT(St_Fname, LEN(St_Fname) - 1) AS FirstNameWithoutLastChar
FROM Student;
---------------------------------------------------------
delete gra from Stud_Course
join Student on Stud_Course.St_Id=Student.St_Id
join Department on Student.Dept_Id=Department.Dept_Id
where Dept_Name='SD'
DELETE sc
FROM Stud_Course sc
JOIN Student s ON sc.St_Id = s.St_Id
JOIN Department d ON s.Dept_Id = d.Dept_Id
WHERE d.Dept_Name = 'SD';
select * from Student
select * from Stud_Course
select * from Department
--------------------------------------------------------------