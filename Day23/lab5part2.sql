--1--
SELECT SalesOrderID, ShipDate
FROM SalesLT.SalesOrderHeader
WHERE OrderDate BETWEEN '2002-07-28' AND '2014-07-29';

--2--
SELECT ProductID, Name
FROM SalesLT.Product
WHERE StandardCost < 110.00;

-- 3 --
SELECT ProductID, Name
FROM SalesLT.Product
WHERE Weight IS NULL;

-- 4️--
SELECT *
FROM SalesLT.Product
WHERE Color IN ('Silver', 'Black', 'Red');

-- 5️--
SELECT *
FROM SalesLT.Product
WHERE Name LIKE 'B%';

-- 6️ --
UPDATE SalesLT.ProductDescription
SET Description = 'Chromoly steel_High of defects'
WHERE ProductDescriptionID = 3;

SELECT *
FROM SalesLT.ProductDescription
WHERE Description LIKE '%\_%' ESCAPE '\';

-- 7--
SELECT OrderDate, SUM(TotalDue) AS TotalSales
FROM SalesLT.SalesOrderHeader
WHERE OrderDate BETWEEN '2001-07-01' AND '2014-07-31'
GROUP BY OrderDate
ORDER BY OrderDate;

-- 8️--


-- 9️ --
SELECT AVG(DISTINCT ListPrice) AS AvgUniqueListPrice
FROM SalesLT.Product;

-- 10--
SELECT 'The ' + Name + ' is only! ' + CAST(ListPrice AS VARCHAR) AS ProductInfo
FROM SalesLT.Product
WHERE ListPrice BETWEEN 100 AND 120
ORDER BY ListPrice;

--11 --


-- 12---

-- 13--
SELECT CONVERT(VARCHAR, GETDATE(), 101) AS TodayDate
UNION
SELECT CONVERT(VARCHAR, GETDATE(), 102)
UNION
SELECT CONVERT(VARCHAR, GETDATE(), 103)
UNION
SELECT CONVERT(VARCHAR, GETDATE(), 120);
