-- 1--
SELECT COUNT(*) AS StudentsWithAge
FROM Student
WHERE St_Age IS NOT NULL;

-- ===============================================
-- 2---
SELECT DISTINCT Ins_Name
FROM Instructor;

-- ===============================================
-- 3--
SELECT 
    St_Id,
    ISNULL(St_Fname, '') + ' ' + ISNULL(St_Lname, '') AS FullName,
    ISNULL(Department.Dept_Name, 'No Department') AS DepartmentName
FROM Student
LEFT JOIN Department ON Student.Dept_Id = Department.Dept_Id;

-- ===============================================
-- 4--
SELECT 
    Instructor.Ins_Name,
    ISNULL(Department.Dept_Name, 'No Department') AS DepartmentName
FROM Instructor
LEFT JOIN Department ON Instructor.Dept_Id = Department.Dept_Id;

-- ===============================================
-- 5--
SELECT 
    S.St_Fname + ' ' + S.St_Lname AS FullName,
    C.Crs_Name AS CourseName
FROM Student S
JOIN Stud_Course SC ON S.St_Id = SC.St_Id
JOIN Course C ON SC.Crs_Id = C.Crs_Id
WHERE SC.Grade IS NOT NULL;

-- ===============================================
-- 6--
SELECT 
    T.Top_Name,
    COUNT(C.Crs_Id) AS CourseCount
FROM Topic T
LEFT JOIN Course C ON T.Top_Id = C.Top_ID
GROUP BY T.Top_Name;

-- ===============================================
-- 7--
SELECT 
    MAX(Salary) AS MaxSalary,
    MIN(Salary) AS MinSalary
FROM Instructor;

-- ===============================================
--8---
SELECT *
FROM Instructor
WHERE Salary < (SELECT AVG(Salary) FROM Instructor WHERE Salary IS NOT NULL);

-- ===============================================
-- 9---
SELECT DISTINCT D.Dept_Name
FROM Department D
JOIN Instructor I ON D.Dept_Id = I.Dept_Id
WHERE I.Salary = (SELECT MIN(Salary) FROM Instructor WHERE Salary IS NOT NULL);

-- ===============================================
-- 10---
SELECT DISTINCT Salary
FROM Instructor
WHERE Salary IS NOT NULL
ORDER BY Salary DESC
OFFSET 0 ROWS FETCH NEXT 2 ROWS ONLY;

-- ===============================================
-- 11--
SELECT 
    Ins_Name,
    COALESCE(Salary, 0) AS Payment
FROM Instructor;

-- ===============================================
-- 12--
SELECT AVG(Salary) AS AverageSalary
FROM Instructor
WHERE Salary IS NOT NULL;

-- ===============================================
-- 13---
SELECT 
    S.St_Fname AS StudentFirstName,
    Sup.*
FROM Student S
JOIN Instructor Sup ON S.St_super = Sup.Ins_Id;

-- ===============================================
-- 14---
SELECT *
FROM (
    SELECT 
        Dept_Id,
        Ins_Name,
        Salary,
        RANK() OVER (PARTITION BY Dept_ID ORDER BY Salary DESC) AS SalaryRank
    FROM Instructor
    WHERE Salary IS NOT NULL
) AS Ranked
WHERE SalaryRank <= 2;

-- ===============================================
-- 15--
SELECT *
FROM (
    SELECT 
        Dept_Id,
        St_Id,
        St_Fname,
        St_Lname,
        ROW_NUMBER() OVER (PARTITION BY Dept_ID ORDER BY NEWID()) AS RandomRank
    FROM Student
) AS RandomStudents
WHERE RandomRank = 1;
