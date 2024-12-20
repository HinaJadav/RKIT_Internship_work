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

-- # Dates and View ---------------------------  Remaining ---- Add after Data is inserted into table



