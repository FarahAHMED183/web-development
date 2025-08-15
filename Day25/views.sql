CREATE VIEW gradeabove50 AS
SELECT 
    St_Fname + ' ' + St_Lname AS FullName,
    Course.Crs_name
FROM 
    Student
JOIN 
    Stud_Course ON Student.St_Id = Stud_Course.St_Id
JOIN 
    Course ON Stud_Course.Crs_Id = Course.Crs_Id
WHERE 
    grade > 50;
select * from gradeabove50;
---------------------------------------------------------------------
CREATE VIEW insTech
WITH ENCRYPTION
AS
SELECT 
    Instructor.Ins_Name,
    Ins_Course.Crs_Id,
    Course.Crs_Name
FROM 
    Instructor
JOIN 
    Ins_Course ON Instructor.Ins_Id = Ins_Course.Ins_Id
JOIN 
    Course ON Ins_Course.Crs_Id = Course.Crs_Id;
select * from insTech
---------------------------------------------------------------
create view depDiss as
select Ins_Name,Dept_Name
from Instructor
join Department On Instructor.Dept_Id=Department.Dept_Id
where Dept_Name in ('SD','Java')

select * from depDiss
---------------------------------------------------------------

create view showStudent as
select * from Student 
where St_Address in ('Alex','Cairo')
select * from showStudent

Update showStudent set st_address='Tanta'
Where st_address='Alex'
select * from Student
-------------------------------------------------------------------
create view totalEmp as
select Pname from Project

---------------------------
CREATE CLUSTERED INDEX idx_Department_HireDate
ON Departments([MGRStart Date]);  --cant make it because i have PK index and i can only have one clustered index
-----------------------------------------------

CREATE UNIQUE INDEX idx_Age
ON Student(St_Age);  -- can't perform it beacuse the column has already duplicate values 

--------------------------------------------------------------------
-- Target table
CREATE TABLE LastTransactions (
    UserID INT PRIMARY KEY,
    TransactionAmount DECIMAL(10,2)
);

-- Source table
CREATE TABLE DailyTransactions (
    UserID INT PRIMARY KEY,
    TransactionAmount DECIMAL(10,2)
);
-- Existing data in LastTransactions
INSERT INTO LastTransactions (UserID, TransactionAmount)
VALUES
(1, 4000),
(4,2000),
(2, 10000);

-- New data in DailyTransactions
INSERT INTO DailyTransactions (UserID, TransactionAmount)
VALUES
(1, 1000),  -- Existing user, will update
(2, 2000),  -- Existing user, will update
(3, 1000);  -- New user, will insert

MERGE LastTransactions AS target
USING DailyTransactions AS source
ON target.UserID = source.UserID
WHEN MATCHED THEN
    UPDATE SET target.TransactionAmount = source.TransactionAmount
WHEN NOT MATCHED BY TARGET THEN
    INSERT (UserID, TransactionAmount)
    VALUES (source.UserID, source.TransactionAmount)
-- Optional delete if needed
-- WHEN NOT MATCHED BY SOURCE THEN
--     DELETE;
;

select * from LastTransactions
----------------------------------
create view v_clerk as 
select *from Works_on
where job='clerk'
select *from v_clerk
-------------------------------------------------------
create view v_without_budget as 
select ProjectNo,ProjectName from HR.Project
select * from v_without_budget

------------------------------------------------------
CREATE VIEW v_count AS
SELECT 
    p.ProjectName,
    COUNT(w.Job) AS JobCount
FROM 
    HR.Project p
JOIN 
    dbo.Works_on w ON p.ProjectNo = w.ProjectNo
GROUP BY 
    p.ProjectName;

	
	select * from v_count
	--------------------------------------------------------------------

create view v_project_p2 as
select * from v_clerk where ProjectNo=2
-------------------------------------------------------------------------
ALTER VIEW v_without_budget AS
SELECT *
FROM HR.Project
WHERE ProjectNo IN ('1','2');
-------------------------------------------------------------------------
drop view v_clerk
drop view v_count
----------------------------------------------------------------------
CREATE VIEW d2emp AS
SELECT 
    e.EmpLname,
    COUNT(e.EmpNo) AS EmployeeCount
FROM 
    HR.Employee e
JOIN 
    Department d ON e.DeptNo = d.DeptNo
WHERE 
    e.DeptNo = 2
GROUP BY 
    e.EmpLname;

	select * from d2emp
	select * from HR.Employee
	-------------------------------------------
	CREATE VIEW Jview AS
SELECT EmpLname
FROM d2emp
WHERE EmpLname LIKE '%J%';
select * from Jview
--------------------------------------------------------
create view view_dept as
select DeptName,COUNT(DeptNo) as total_dept from Department
group by DeptName
select * from view_dept
select * from Department

INSERT INTO Department (DeptNo, DeptName)
VALUES (4, 'Development');  -- can't duplicate the PK

------------------------------------------------------------------------------
CREATE VIEW v_2006_check AS
SELECT 
    EmpNo,
    ProjectNo,
    Enter_Date
FROM Works_on
WHERE Enter_Date >= '2006-01-01'
  AND Enter_Date <= '2006-12-31'
WITH CHECK OPTION;

select * from v_2006_check

INSERT INTO v_2006_check (EmpNo, ProjectNo, Enter_Date)
VALUES (22222, 1, '2006-05-15'); 

INSERT INTO v_2006_check (EmpNo, ProjectNo, Enter_Date)
VALUES (25348, 2, '2007-01-10');  ---fails because it is not in 2006
 select * from HR.Employee
 select * from HR.Project
 -------------------------------