-- Dondolo Kazembe Web Application Database

USE master
-- The following is the creation of the database named "EPI_USE_DB"
IF EXISTS (SELECT*FROM sys.databases WHERE name = 'EPI_USE_DB')
DROP DATABASE EPI_USE_DB 
CREATE DATABASE EPI_USE_DB

-- This ensures that the EPI-USE database is used 
USE EPI_USE_DB

-- Creation of Employee Table
CREATE TABLE Employee(
EmployeeName varchar (30) NOT NULL,
EmployeeSurname varchar (30) NOT NULL,
EmployeeNumber varchar (20) NOT NULL,
BirthDate DATE NOT NULL,
ManagerNumber varchar (20),
EmployeeSalary int NOT NULL,
EmployeePosition varchar (50) NOT NULL,
PRIMARY KEY (EmployeeNumber),
FOREIGN KEY (ManagerNumber) REFERENCES Manager(ManagerNumber)
); 

CREATE TABLE Manager(
ManagerNumber varchar (20) NOT NULL,
ManagerName varchar (30) NOT NULL,
ManagerSurname varchar (30) NOT NULL,
ManagerSalary int NOT NULL,
PRIMARY KEY (ManagerNumber)
);

-- Insert values into Employee table

INSERT INTO Employee(EmployeeName,EmployeeSurname,EmployeeNumber,BirthDate,ManagerNumber,EmployeeSalary,EmployeePosition)
VALUES
('Damien','Mayer','EMP102','2022-08-12','MAN120','Travis Scott', 20000,'IT Technician')

-- Insert values into Manager table
INSERT INTO Manager(ManagerNumber,ManagerName,ManagerSurname,ManagerSalary)
VALUES
('MAN120','Travis','Scott',40000)

/*
Stored procedure to insert values into employee table (Tik, 2021)
*/

CREATE PROCEDURE InsertEmployeeInfo(
@EmployeeName varchar (30) = '',
@EmployeeSurname varchar (30) = '',
@EmployeeNumber varchar (20) = '',
@BirthDate DATE = '',
@ManagerNumber varchar (20) = '',
@EmployeeSalary int = '',
@EmployeePosition varchar (50) = ''
) AS
BEGIN
INSERT INTO Employee(EmployeeName,EmployeeSurname,EmployeeNumber,BirthDate,ManagerNumber,EmployeeSalary,EmployeePosition)
VALUES
(@EmployeeName,@EmployeeSurname,@EmployeeNumber,@BirthDate,@ManagerNumber,@EmployeeSalary,@EmployeePosition)

END

/*
Stored procedure to retrieve all employee information
*/

CREATE PROCEDURE GetALLEmployees
AS
BEGIN
SELECT * FROM Employee
END

/*
Stored Procedure to retrieve employee information by using their employee number
*/

CREATE PROCEDURE GetEmployeeByNumber(
@EmployeeNumber varchar (20) = ''
)AS
BEGIN
SELECT * FROM Employee 
WHERE EmployeeNumber = @EmployeeNumber
END

/*
Stored Procedure to update Employee table 
*/

CREATE PROCEDURE UpdateEmployee(
@EmployeeName varchar (30) = '',
@EmployeeSurname varchar (30) = '',
@EmployeeNumber varchar (20) = '',
@BirthDate DATE = '',
@ManagerNumber varchar (20) = '',
@EmployeeSalary int = '',
@EmployeePosition varchar (50) = ''
)
AS
BEGIN
UPDATE Employee
SET EmployeeName = @EmployeeName,
EmployeeSurname = @EmployeeSurname,
BirthDate = @BirthDate,
ManagerNumber = @ManagerNumber,
EmployeeSalary = @EmployeeSalary,
EmployeePosition = @EmployeePosition
WHERE EmployeeNumber = @EmployeeNumber
END

/*
Stored procedure to delete employee by employee number
*/

CREATE PROCEDURE DeleteEmployeeByNumber
(
@EmployeeNumber varchar(20) = ''
)
AS
BEGIN
--Delete from child table
DELETE FROM  Employee
WHERE EmployeeNumber = @EmployeeNumber
END

-- Creating Managers
CREATE PROCEDURE CreateManager(
@ManagerNumber varchar (20) = '',
@ManagerName varchar (30) = '',
@ManagerSurname varchar (30) = '',
@ManagerSalary int = ''
) AS
BEGIN
INSERT INTO Manager(ManagerNumber,ManagerName,ManagerSurname,ManagerSalary)
VALUES
(@ManagerNumber, @ManagerName, @ManagerSurname, @ManagerSalary)

END

/*
Stored procedure to retrieve all manager information
*/

CREATE PROCEDURE GetALLManagers
AS
BEGIN
SELECT * FROM Manager
END

/*
Stored Procedure to retrieve manager information by using their manager number
*/

CREATE PROCEDURE GetManagerByNumber(
@ManagerNumber varchar (20) = ''
)AS
BEGIN
SELECT * FROM Manager
WHERE ManagerNumber = @ManagerNumber
END

/*
Stored Procedure to update Manager table 
*/

CREATE PROCEDURE UpdateManager(
@ManagerNumber varchar (20) = '',
@ManagerName varchar (30) = '',
@ManagerSurname varchar (30) = '',
@ManagerSalary int = ''
)
AS
BEGIN
UPDATE Manager
SET ManagerName = @ManagerName,
ManagerSurname = @ManagerSurname,
ManagerSalary = @ManagerSalary
WHERE ManagerNumber = @ManagerNumber
END

/*
Stored procedure to delete manager by manager number
*/

CREATE PROCEDURE DeleteManagerByNumber
(
@ManagerNumber varchar(20) = ''
)
AS
BEGIN
--Delete from child table
DELETE FROM Manager
WHERE ManagerNumber = @ManagerNumber
END