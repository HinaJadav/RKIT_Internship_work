-- DML: Handles data manipulation (inserting, updating, deleting).
-- Commands: SELECT, INSERT, UPDATE, DELETE.

-- SELECT
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


-- INSERT
-- Insert data into department table
insert into departments values (1, "Software Development", 50);
insert into departments values (2, "Marketing", 10);
insert into departments values (3, "Management", 5);

-- Insert data into employee table
insert into employee (ID, Name, Description, Salary, Email, Age) values (1,"Priyank", "Manager", 50000, "pu@gmail.com", 20);
insert into employee (ID, Name, Description, Salary, Email, Age) values (2,"Madhu", "HR", 55000, "madhu@gmail.com", 21);
insert into employee (ID, Name, Description, Salary, Email, Age) values (3,"Suresh", "Developer", 50000, "suresh@gmail.com", 23);
insert into employee (ID, Name, Description, Salary, Email, Age) values (4,"Anjali", "Developer", 45000, "anjali@gmail.com", 22);
insert into employee (ID, Name, Description, Salary, Email, Age) values (5,"Nishant", "Developer", 45000, "nishant@gmail.com", 20);
insert into employee (ID, Name, Description, Salary, Email, Age) values (6,"Nahi", "Developer", 65000, "nahi@gmail.com", 21);


-- UPDATE
-- UPDATE Statement (safe update only using primary key)
update employee set Description = "Senior Developer" where ID = 5; 

update employee set DepartmentID = 1 where (Description = "Developer" or Description = "Senior Developer") and (ID between 1 and 6);
update employee set DepartmentID = 2 where (ID between 1 and 2);


-- DELETE
-- DELETE Statement (delete data row from employee table)
insert into employee (ID, Name, Description, Salary, Email, Age) values (7,"Nahi", "Developer", 65000, "nahii@gmail.com", 21);
-- Q: delete employee with ID 7
delete from employee where ID = 7;
select * from employee where Name = "Nahi";