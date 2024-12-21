-- Create Database
create schema `rkitdatabase`;

create database test1;

-- Drop Database
drop database test1;

-- Create Table
CREATE TABLE `rkitdatabase`.`employee` (
    `ID` INT NOT NULL,
    `Name` VARCHAR(45) NOT NULL,
    `Decription` VARCHAR(50) NOT NULL,
    `Salary` INT NOT NULL,
    PRIMARY KEY (`ID`)
) COMMENT = ' ';

create table department (
	Name varchar(50) not null primary key,
    NoOfEmployees int not null,
    WorkDescription varchar(100)
);

-- Drop Table
drop table department;

-- Truncate Table
truncate table employee;

-- Alter Table
-- Add new column
alter table employee add email varchar(40); 
alter table employee add Age varchar(3);
-- Change column name
alter table employee change column Decription Description varchar(40);
-- Modify column name
alter table employee modify column Age int;
-- Drop column
alter table employee drop column age;

-- Contraints 
-- 1) unique  
alter table employee add constraint unique (ID);
-- Drop unique constraint
alter table employee drop index ID;

-- 2) not null
alter table employee modify Age int not null;

-- 3) primary key
alter table employee add primary key (Name);
-- Add multiple primary key
alter table employee add primary key (ID, Name);
-- Drop primary key
alter table employee drop primary key;

-- 4) foreign key
create table departments (
	DepartmentID int not null, 
    DepartmentName varchar(50) not null,
    NoOfEmployee int,
    primary key(DepartmentID)
);
alter table employee add DepartmentID int;
-- Add foreign key 
alter table employee add foreign key (DepartmentID) references departments(DepartmentID);
-- Drop foreign key
alter table employee drop foreign key DepartmentID; -- ? check it again after enter data

-- 5) check 
alter table employee add check (Age >= 20);

-- 6) default
alter table employee alter Description set default "Enter your description"; -- ? check after data entry
alter table employee alter Description drop default;

-- 7) check index
-- create index
create index nameIndex on employee (Name);
-- create unique index
create unique index emailIndex on employee (Email);
-- drop index
alter table employee drop index nameIndex;


-- auto increment field 
alter table employee drop primary key;
alter table employee modify ID int auto_increment primary key;
alter table employee auto_increment = 1;

-- Insert data into table
insert into employee (ID, Name, Description, Salary, Email, Age) values (1,"Priyank", "Manager", 50000, "pu@gmail.com", 20);
insert into employee (ID, Name, Description, Salary, Email, Age) values (2,"Madhu", "HR", 55000, "madhu@gmail.com", 21);
insert into employee (ID, Name, Description, Salary, Email, Age) values (3,"Suresh", "Developer", 50000, "suresh@gmail.com", 23);
insert into employee (ID, Name, Description, Salary, Email, Age) values (4,"Anjali", "Developer", 45000, "anjali@gmail.com", 22);
insert into employee (ID, Name, Description, Salary, Email, Age) values (5,"Nishant", "Developer", 45000, "nishant@gmail.com", 20);
insert into employee (ID, Name, Description, Salary, Email, Age) values (6,"Nahi", "Developer", 65000, "nahi@gmail.com", 21);

-- Select
-- Select all data 
select * from employee;
-- Select some columns from table
select Name, Description, Email from employee;
-- Select Distict 
select distinct Description from employee;

-- Where
-- operator: =
select * from employee where Description = "Developer";  
-- operator: >
select Name from employee where Salary > 50000;
-- operator: >=
select Name from employee where Salary >= 50000;
-- operator: <
select Name from employee where Salary < 50000;
-- operator: <=
select Name from employee where Salary <= 50000;
-- operator: <>
select Name from employee where Description <> "Developer";
-- operator: BETWEEN
select Name from employee where Salary between 40000 and 50000;
-- operator: LIKE
select * from employee where Name like 'N%';
-- operator: IN
select * from employee where Description in ("Developer", "HR");

-- AND, OR and NOT Operators
-- and
select Name from employee where Description = "Developer" and Salary = 50000;
-- or
select * from employee where Description = "Developer" or Salary = 50000;
-- not
select Name from employee where not Description = "Developer"; 

-- ORDER BY Keyword
-- default order is "ASC"
select * from employee order by Salary;
select * from employee order by Salary desc;
select * from employee order by Age asc, Salary desc;

-- NULL Values
-- is null
select * from employee where DepartmentID is null;
-- is not null
select * from employee where DepartmentID is not null;

-- UPDATE Statement (safe update only using primary key)
update employee set Description = "Senior Developer" where ID = 5; 

-- DELETE Statement (delete data row from employee table)
insert into employee (ID, Name, Description, Salary, Email, Age) values (7,"Nahi", "Developer", 65000, "nahii@gmail.com", 21);
-- Q: delete employee with ID 7
delete from employee where ID = 7;
select * from employee where Name = "Nahi";

-- LIMIT Clause
-- Q: find first 2 developers
select Name from employee where Description = "Developer" limit 2; 

-- MIN() and MAX() Functions
-- Q: find developer's minimum salary
select min(Salary) from employee where Description = "Developer";
-- Q: find developer's maximum salary
select max(Salary) from employee where Description = "Developer";

-- COUNT(), AVG() and SUM() Functions
-- Q: find total number of employees
select count(ID) from employee where Description = "Developer";
-- Q: find average salary of employee
select avg(Salary) from employee where Description = "Developer";
-- Q: find total salary amount company needs to pay to all employees
select sum(Salary) from employee;

-- LIKE Operator with Wildcard Characters(%, _)
-- Q: find employees which name start with "a"
select * from employee where Name like 'a%';
-- Q: find employees which name end with "i"
select * from employee where Name like '%i';
-- Q: find employees which name have h at any position 
select * from employee where Name like '%h%';
-- Q: find employees which name start with "a" and have atleast 6 characters
select * from employee where Name like 'a_____%';
-- Q: find employees which name second charcter is "a"
select * from employee where Name like '_a%';
-- Q: find employees which name start with "a" and end with "i"
select * from employee where Name like 'a%i';

-- Insert data into department table
insert into departments values (1, "Software Development", 50);
insert into departments values (2, "Marketing", 10);
insert into departments values (3, "Management", 5);

-- Alter employee table column DepartmentID based on departments table
update employee set DepartmentID = 1 where (Description = "Developer" or Description = "Senior Developer") and (ID between 1 and 6);
update employee set DepartmentID = 2 where (ID between 1 and 2);

-- IN Operator for nested query
select Name, Description, Salary from employee where DepartmentID in (select DepartmentID from departments where DepartmentName = "Software Development");

-- Aliases













-- # Dates and View ---------------------------  Remaining ---- Add after Data is inserted into table




