-- ==============================DQL================================
   

--1--
SELECT D.Dependent_name, D.Sex
FROM Dependent AS D
JOIN Employee AS E ON D.ESSN = E.SSN
WHERE D.Sex = 'F' AND E.Sex = 'F'
UNION
SELECT D.Dependent_name, D.Sex
FROM Dependent AS D
JOIN Employee AS E ON D.ESSN = E.SSN
WHERE D.Sex = 'M' AND E.Sex = 'M';

---2---
SELECT P.Pname, SUM(W.Hours) AS Total_Hours
FROM Project P
JOIN Works_for W ON P.Pnumber = W.Pno
GROUP BY P.Pname;

---3-
SELECT D.*
FROM Departments D
JOIN Employee E ON D.Dnum = E.Dno
WHERE E.SSN = (SELECT MIN(SSN) FROM Employee);

---4---
SELECT D.Dname,
       MAX(E.Salary) AS Max_Salary,
       MIN(E.Salary) AS Min_Salary,
       AVG(E.Salary) AS Avg_Salary
FROM Departments D
JOIN Employee E ON D.Dnum = E.Dno
GROUP BY D.Dname;

----5---
SELECT DISTINCT E.Lname
FROM Employee E
JOIN Departments D ON E.SSN = D.MGRSSN
WHERE E.SSN NOT IN (SELECT ESSN FROM Dependent);


---6---
SELECT D.Dnum, D.Dname, COUNT(E.SSN) AS Num_Employees
FROM Departments D
JOIN Employee E ON D.Dnum = E.Dno
GROUP BY D.Dnum, D.Dname
HAVING AVG(E.Salary) < (SELECT AVG(Salary) FROM Employee);

---7--
SELECT E.Fname, E.Lname, P.Pname, E.Dno
FROM Employee E
JOIN Works_for W ON E.SSN = W.ESSn
JOIN Project P ON W.Pno = P.Pnumber
ORDER BY E.Dno, E.Lname, E.Fname;

---8---
SELECT DISTINCT TOP 2 Salary
FROM Employee
ORDER BY Salary DESC;

---9---
SELECT E.Fname + ' ' + E.Lname AS Full_Name
FROM Employee E
WHERE E.Fname IN (SELECT Dependent_name FROM Dependent)
   OR E.Lname IN (SELECT Dependent_name FROM Dependent);

--10--
UPDATE Employee
SET Salary = Salary * 1.3
WHERE SSN IN (
    SELECT ESSn
    FROM Works_for W
    JOIN Project P ON W.Pno = P.Pnumber
    WHERE P.Pname = 'Al Rabwah'
);

---11---
SELECT E.SSN, E.Fname + ' ' + E.Lname AS Full_Name
FROM Employee E
WHERE EXISTS (
    SELECT 1 FROM Dependent D
    WHERE D.ESSN = E.SSN
);




-- ==============================DML==============================--

---1---
INSERT INTO Departments (Dname, Dnum, MGRSSN, [MGRStart Date])
VALUES ('DEPT IT', 100, 112233, '2006-11-01');

--2---
UPDATE Departments
SET MGRSSN = 968574
WHERE Dnum = 100;

--3---
UPDATE Departments
SET MGRSSN = 102672
WHERE Dnum = 20;

--4---
UPDATE Employee
SET Superssn = 102672
WHERE SSN = 102660;

--5--

/*  Remove his dependents */
DELETE FROM Dependent
WHERE ESSN = 223344;


UPDATE Departments
SET MGRSSN = 102672
WHERE MGRSSN = 223344;


UPDATE Employee
SET Superssn = 102672
WHERE Superssn = 223344;

/*  Remove from projects */
DELETE FROM Works_for
WHERE ESSn = 223344;

/*delete employee record */
DELETE FROM Employee
WHERE SSN = 223344;
