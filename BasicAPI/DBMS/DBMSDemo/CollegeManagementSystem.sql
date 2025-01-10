-- College Management System
-- This system focuses on storing, retrieving, and managing data for students, departments, courses, and enrollments, ensuring data consistency, security, and ease of access. 
-- By leveraging relational database management principles, this project aims to streamline college-related data handling and reporting processes.


-- Create database for college management system
-- create database collegemanagementsystem;

-- Use collegemanagementsystem database for further operation
use collegemanagementsystem;

-- Create yms01(student) table which stores all information related to college student
CREATE TABLE `collegemanagementsystem`.`yms01` (
  `s01f01` INT NOT NULL AUTO_INCREMENT COMMENT 'Original Column: EnrollmentID',
  `s01f02` VARCHAR(45) NOT NULL COMMENT 'Original Column: Name',
  `s01f03` VARCHAR(100) NOT NULL COMMENT 'Original Column: Email',
  `s01f04` VARCHAR(15) NULL DEFAULT 'Not Provided' COMMENT 'Original Column: ContactInformation',
  `s01f05` DATE NULL COMMENT 'Original Column: DateOfBirth',
  `s01f06` ENUM('Male', 'Female', 'Other') NULL DEFAULT 'Other' COMMENT 'Original Column: Gender',
  `s01f07` TEXT NULL COMMENT 'Original Column: Address',
  `s01f08` YEAR NULL DEFAULT NULL COMMENT 'Original Column: YearOfGraduation',
  `s01f09` VARCHAR(45) NULL DEFAULT 'GIA' COMMENT 'Original Column: StudentSeatType',
  `s01f10` ENUM('PAID', 'UNPAID') NOT NULL DEFAULT 'UNPAID' COMMENT 'Original Column: FeesStatus',
  `s01f11` INT NULL DEFAULT NULL COMMENT 'Original Column: DepartmentID',
  `s01f12` TINYINT NULL DEFAULT 1 COMMENT 'Original Column: IsActive',
  PRIMARY KEY (`s01f01`),
  UNIQUE INDEX `Email_UNIQUE` (`s01f03` ASC) VISIBLE
)
ENGINE = InnoDB COMMENT = 'Original Table: Student';

-- create ymd01(department) table which stores all information related to college departments
CREATE TABLE `collegemanagementsystem`.`ymd01` (
  `d01f01` INT NOT NULL AUTO_INCREMENT  COMMENT 'Original Column: DepartmentID',
  `d01f02` VARCHAR(45) NOT NULL  COMMENT 'Original Column: Name',
  `d01f03` VARCHAR(45) NULL DEFAULT 'Not Provided'  COMMENT 'Original Column: HODName',
  `d01f04` INT NULL  COMMENT 'Original Column: NumberOfFacultyMembers',
  `d01f05` INT NULL DEFAULT 0  COMMENT 'Original Column: NumberOfStudent',
  PRIMARY KEY (`d01f01`),
  UNIQUE INDEX `d01f02_UNIQUE` (`d01f02` ASC) VISIBLE) COMMENT = 'Original Table: Department';

-- Create ymc01(courses) table which stores all information related to courses provied by each departments
CREATE TABLE `collegemanagementsystem`.`ymc01` (
  `c01f01` INT NOT NULL AUTO_INCREMENT COMMENT 'Original Column: CourseID',
  `c01f02` VARCHAR(45) NULL COMMENT 'Original Column: CourseName',
  `c01f03` INT NULL COMMENT 'Original Column: DepartmentID',
  `c01f04` INT NULL COMMENT 'Original Column: Credits',
  `c01f05` TEXT NULL COMMENT 'Original Column: CourseDescription',
  `c01f06` VARCHAR(45) NULL COMMENT 'Original Column: InstructorName',
  PRIMARY KEY (`c01f01`),
  UNIQUE INDEX `c01f02_UNIQUE` (`c01f02` ASC) VISIBLE,
  INDEX `fk_DepartmentID_idx` (`c01f03` ASC) VISIBLE,
  CONSTRAINT `fk_DepartmentIDInCourses`
    FOREIGN KEY (`c01f03`)
    REFERENCES `collegemanagementsystem`.`ymd01` (`d01f01`)
    ON DELETE CASCADE
    ON UPDATE CASCADE) COMMENT = 'Original Table: Courses';

-- Create yme01(Enrollment) table which stores information about students enrollment information.
CREATE TABLE `collegemanagementsystem`.`yme01` (
  `e01f01` INT NOT NULL AUTO_INCREMENT COMMENT 'Original Column: EnrollmentID',
  `e01f02` INT NULL COMMENT 'Original Column: StudentID',
  `e01f03` INT NULL COMMENT 'Original Column: CourseID',
  `e01f04` DATE NULL COMMENT 'Original Column: EnrollmentDate',
  `e01f05` ENUM('Active', 'Completed', 'Dropped') NULL COMMENT 'Original Column: EnrollmentStatus',
  PRIMARY KEY (`e01f01`),
  INDEX `fk_StudentIDInEnrollments_idx` (`e01f02` ASC) VISIBLE,
  INDEX `fk_CourseIDInEnrollments_idx` (`e01f03` ASC) VISIBLE,
  CONSTRAINT `fk_StudentIDInEnrollments`
    FOREIGN KEY (`e01f02`)
    REFERENCES `collegemanagementsystem`.`yms01` (`s01f01`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_CourseIDInEnrollments`
    FOREIGN KEY (`e01f03`)
    REFERENCES `collegemanagementsystem`.`ymc01` (`c01f01`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION) COMMENT = 'Original Table: Enrollment';

-- Insert data into ymd01
INSERT INTO `ymd01` (`d01f02`, `d01f03`, `d01f04`, `d01f05`)
VALUES 
('Computer Engineering', 'Dr. Priyank Jadav', 20, 150),
('Mechanical Engineering', 'Dr. Anjali Shah', 25, 200),
('Electrical Engineering', 'Dr. Nahii Oza', 18, 180),
('Civil Engineering', 'Dr. Madhu Koriya', 22, 170),
('Information Technology', 'Dr. Suresh Mehta', 15, 120);

-- Display table ymd01 with data
SELECT 
    *
FROM
    ymd01;
 
-- Add foriegn key into student table with column departmentID which refers to department table departmentID column
ALTER TABLE `yms01`
ADD CONSTRAINT `fk_DepartmentID`
FOREIGN KEY (`s01f11`)
REFERENCES `ymd01` (`d01f01`)
ON DELETE CASCADE
ON UPDATE CASCADE;

-- ON DELETE CASCADE: Automatically deletes rows in the child table when the referenced row in the parent table is deleted.
-- ON UPDATE CASCADE: Automatically updates rows in the child table when the referenced row in the parent table is updated.
 
-- Insert data into yms01 table
INSERT INTO `yms01` (`s01f02`, `s01f03`, `s01f04`, `s01f05`, `s01f06`, `s01f07`, `s01f08`, `s01f09`, `s01f10`, `s01f11`, `s01f12`)
VALUES
('Aarav Sharma', 'aarav.sharma@example.com', '9876543210', '2000-06-15', 'Male', '12 MG Road, Bengaluru', 2024, 'GIA', 'PAID', 1, 1),
('Ananya Iyer', 'ananya.iyer@example.com', '9123456789', '1999-09-20', 'Female', '56 Residency Road, Chennai', 2023, 'Self-Financed', 'UNPAID', 2, 1),
('Rohan Gupta', 'rohan.gupta@example.com', NULL, '2001-03-25', 'Male', '123 Nehru Nagar, Mumbai', 2025, 'GIA', 'PAID', 3, 1),
('Ishita Roy', 'ishita.roy@example.com', '9988776655', '2000-02-10', 'Female', '45 Salt Lake, Kolkata', 2024, 'Management', 'UNPAID', 4, 1),
('Aryan Verma', 'aryan.verma@example.com', '9871234567', '1998-11-15', 'Male', '23 Civil Lines, Lucknow', 2022, 'GIA', 'PAID', 5, 1),
('Sanya Mehta', 'sanya.mehta@example.com', '8765432190', '1997-05-05', 'Female', '78 Lajpat Nagar, Delhi', 2021, 'Self-Financed', 'UNPAID', 1, 1),
('Kabir Singh', 'kabir.singh@example.com', NULL, '1999-07-07', 'Male', NULL, 2023, 'GIA', 'PAID', 5, 1),
('Aditi Rao', 'aditi.rao@example.com', '7654321987', NULL, 'Female', '34 Banjara Hills, Hyderabad', 2025, 'Management', 'UNPAID', 4, 1),
('Vihan Nair', 'vihan.nair@example.com', '8765123498', '2003-12-31', 'Male', '12 Model Town, Chandigarh', 2026, 'GIA', 'PAID', 3, 1),
('Priya Desai', 'priya.desai@example.com', '9877896543', '2004-04-18', 'Female', '67 Koregaon Park, Pune', 2027, 'Self-Financed', 'UNPAID', 2, 1),
('Aditya Malhotra', 'aditya.malhotra@example.com', NULL, '2000-11-20', 'Male', NULL, 2024, 'Management', 'PAID', 1, 1),
('Neha Aggarwal', 'neha.aggarwal@example.com', '9123654780', '1999-01-15', 'Female', '89 Greater Kailash, Delhi', 2023, 'GIA', 'UNPAID', 5, 1),
('Ritvik Chatterjee', 'ritvik.chatterjee@example.com', '9090909090', '1998-05-30', 'Male', '120 Park Street, Kolkata', 2022, 'GIA', 'PAID', 1, 1),
('Tanvi Joshi', 'tanvi.joshi@example.com', '8080808080', NULL, 'Female', '56 Juhu Beach, Mumbai', NULL, 'Self-Financed', 'UNPAID', 4, 1),
('Arjun Das', 'arjun.das@example.com', '7070707070', '2002-02-25', 'Male', '23 Kothrud, Pune', 2024, 'GIA', 'PAID', 3, 1),
('Meera Kale', 'meera.kale@example.com', '6060606060', NULL, 'Female', '12 Indira Nagar, Bengaluru', 2023, 'Self-Financed', 'UNPAID', 1, 1),
('Vivaan Reddy', 'vivaan.reddy@example.com', '5050505050', '2001-11-18', 'Male', '45 Jubilee Hills, Hyderabad', 2025, 'Management', 'PAID', 2, 1),
('Kavya Bhat', 'kavya.bhat@example.com', '4040404040', '1998-07-07', 'Female', '90 Andheri East, Mumbai', 2022, 'GIA', 'UNPAID', 2, 1),
('Dev Sharma', 'dev.sharma@example.com', '3030303030', '2000-12-12', 'Male', '34 Vasant Vihar, Delhi', 2024, 'Self-Financed', 'PAID', 2, 1),
('Sia Kapoor', 'sia.kapoor@example.com', '2020202020', '2004-01-10', 'Female', '56 Gachibowli, Hyderabad', 2026, 'GIA', 'UNPAID', 4, 1),
('Aniket Jain', 'aniket.jain@example.com', NULL, '2003-03-03', 'Male', '78 Whitefield, Bengaluru', 2027, 'Management', 'PAID', 5, 1),
('Tanya Sehgal', 'tanya.sehgal@example.com', '1010101010', '2005-05-15', 'Female', '45 Anna Nagar, Chennai', 2028, 'GIA', 'UNPAID', 4, 1),
('Krish Patel', 'krish.patel@example.com', '1122334455', '2001-08-08', 'Male', '23 CG Road, Ahmedabad', 2025, 'Self-Financed', 'PAID', 3, 1),
('Nisha Nair', 'nisha.nair@example.com', '2233445566', '1999-09-19', 'Female', '120 Brigade Road, Bengaluru', 2023, 'Management', 'UNPAID', 1, 1),
('Arya Mishra', 'arya.mishra@example.com', '3344556677', '2000-10-10', 'Female', '89 Boring Road, Patna', 2024, 'GIA', 'PAID', 2, 1),
('Rudra Sen', 'rudra.sen@example.com', '4455667788', '1998-12-01', 'Male', '34 Rajarhat, Kolkata', 2022, 'Self-Financed', 'UNPAID', 1, 1),
('Saanvi Yadav', 'saanvi.yadav@example.com', '5566778899', '2003-01-01', 'Female', '67 Vikas Puri, Delhi', 2026, 'GIA', 'PAID', 5, 1),
('Omkar Kulkarni', 'omkar.kulkarni@example.com', '6677889900', '2004-07-07', 'Male', '23 Deccan Gymkhana, Pune', 2027, 'Management', 'UNPAID', 1, 1),
('Tara Shetty', 'tara.shetty@example.com', '7788990011', NULL, 'Female', '56 Electronic City, Bengaluru', NULL, 'GIA', 'PAID', 4, 1);

-- Display table yms01 with data
SELECT 
    *
FROM
    yms01;

-- Insert data into table ymc01
INSERT INTO `collegemanagementsystem`.`ymc01` (`c01f02`, `c01f03`, `c01f04`, `c01f05`, `c01f06`)
VALUES
('Data Structures', 1, 4, 'Study of algorithms and data organization techniques.', 'Dr. Ramesh Kumar'),
('Thermodynamics', 2, 3, 'Basics of thermodynamic principles and applications.', 'Dr. Anjali Shah'),
('Circuit Analysis', 3, 3, 'Introduction to electric circuit design and analysis.', 'Dr. Nisha Patel'),
('Structural Engineering', 4, 4, 'Study of structures and building design principles.', 'Dr. Pravin Mehta'),
('Web Development', 5, 3, 'Basics of HTML, CSS, JavaScript, and modern frameworks.', 'Dr. Suresh Mehta'),
('Machine Learning', 1, 4, 'Introduction to ML algorithms and data analysis.', 'Dr. Priya Sharma'),
('Fluid Mechanics', 2, 3, 'Study of fluids and their mechanical properties.', 'Dr. Rajesh Solanki'),
('Power Systems', 3, 4, 'Electrical power generation and distribution systems.', 'Dr. Meera Patel'),
('Geotechnical Engineering', 4, 3, 'Foundational concepts in soil mechanics and earthwork.', 'Dr. Madhu Koriya'),
('Cloud Computing', 5, 4, 'Basics of cloud services and distributed computing.', 'Dr. Snehal Gupta');

-- Insert data into table yme01
INSERT INTO `collegemanagementsystem`.`yme01` (`e01f02`, `e01f03`, `e01f04`, `e01f05`)
VALUES
(1, 2, '2024-02-15', 'Active'),
(2, 3, '2024-03-20', 'Completed'),
(3, 4, '2024-04-05', 'Active'),
(4, 5, '2024-05-12', 'Dropped'),
(5, 6, '2024-06-08', 'Active'),
(6, 7, '2024-07-19', 'Completed'),
(7, 8, '2024-08-22', 'Dropped'),
(8, 9, '2024-09-11', 'Active'),
(9, 10, '2024-10-14', 'Active'),
(10, 1, '2024-01-18', 'Active'),
(11, 2, '2024-02-25', 'Completed'),
(12, 3, '2024-03-30', 'Dropped'),
(13, 4, '2024-04-17', 'Active'),
(14, 5, '2024-05-22', 'Active'),
(15, 6, '2024-06-25', 'Dropped'),
(16, 7, '2024-07-05', 'Completed'),
(17, 8, '2024-08-15', 'Active'),
(18, 9, '2024-09-20', 'Completed'),
(19, 10, '2024-10-25', 'Dropped'),
(20, 1, '2024-01-30', 'Active'),
(21, 2, '2024-02-12', 'Active'),
(22, 3, '2024-03-05', 'Completed'),
(23, 4, '2024-04-08', 'Active'),
(24, 5, '2024-05-10', 'Dropped'),
(25, 6, '2024-06-14', 'Active'),
(26, 7, '2024-07-20', 'Completed'),
(27, 8, '2024-08-25', 'Dropped'),
(28, 9, '2024-09-02', 'Active');

-- Basic SQL:
-- List all students in the Computer Engineering department.
SELECT 
    s01.s01f01 AS StudentID, s01.s01f02 AS Name, s01.s01f03 AS Email
FROM
    yms01 s01
        JOIN
    ymd01 d01 ON s01.s01f11 = d01.d01f01
WHERE
    d01.d01f02 = 'Computer Engineering';
    
-- Find students whose fees are unpaid.
SELECT 
    s01f01 AS StudentID,
    s01f02 AS Name,
    s01f03 AS Email,
    s01f04 AS ContactInfo
FROM
    yms01
WHERE
    s01f10 = 'UNPAID';
  
-- Data Sorting: 
-- List students sorted by their department and then by their names
SELECT 
    s01.s01f01 AS StudentID,
    s01.s01f02 AS StudentName,
    d01.d01f02 AS DepartmentName
FROM
    yms01 s01
        JOIN
    ymd01 d01
WHERE
    s01.s01f11 = d01.d01f01
ORDER BY d01.d01f02 ASC , s01.s01f02 ASC;

-- first order alpply on d01f02 and data sort by that and after that into same value of d01f02 all rows data sort by s01f02

-- Null Values:
-- Find all courses that have no assigned department
SELECT 
    c01f02 AS CourseName
FROM
    ymc01
WHERE
    c01f03 IS NULL;

-- DDL:
-- Add a new column 'Attendance' to track student regularity in the 'students' table
ALTER TABLE yms01 
ADD COLUMN s01f13 INT;

-- DML:
-- Update a student's department based on their student id (safe update)
UPDATE yms01 SET s01f11 = (SELECT d01f01 FROM ymd01 WHERE d01f02 = 'Computer Engineering') WHERE s01f01 = 1;
SELECT 
    *
FROM
    yms01
WHERE
    s01f01 = 1;

-- TCL:
-- Transaction for fee payment updates
START TRANSACTION;

UPDATE yms01 
SET 
    s01f10 = 'PAID'
WHERE
    s01f01 = 1;
    
COMMIT;

SELECT 
    *
FROM
    yms01
WHERE
    s01f01 = 1;
    
-- Aggregate Functions: 
-- Calculate the total number of students per department 
SELECT 
    d01.d01f02 AS DepartmentName, COUNT(s01.s01f01) AS TotalStudents
FROM
    yms01 s01
        JOIN
    ymd01 d01 ON s01.s01f11 = d01.d01f01
GROUP BY d01.d01f02;

-- Sub-Queries: 
-- Find the courses taken by students in the 'Computer Science' department
SELECT DISTINCT
    c01f02 AS CourseName
FROM
    ymc01
WHERE
    c01f03 IN (SELECT 
            d01f01
        FROM
            ymd01
        WHERE
            d01f02 = 'Computer Engineering');
            
-- Limit:
-- Fetch the top 3 students with the highest attendance percentage
SELECT 
    s01f02 AS StudentName, s01f13 AS Attendance
FROM
    yms01
ORDER BY s01f13 DESC
LIMIT 3 , 10;

-- Joins:
-- Fetch students' names, their department name, and enrolled courses, even if the student is not enrolled in any course (LEFT JOIN)
SELECT 
    s01.s01f01 AS StudentID,
    s01.s01f02 AS StudentName,
    d01.d01f02 AS DepartmentName,
    c01.c01f02 AS CourseName
FROM
    yms01 s01
        LEFT JOIN
    ymd01 d01 ON s01.s01f11 = d01.d01f01
        LEFT JOIN
    yme01 e01 ON s01.s01f01 = e01.e01f02
        LEFT JOIN
    ymc01 c01 ON e01.e01f03 = c01.c01f01
LIMIT 0 , 1000;

-- Identify students who have not enrolled in any course.
SELECT 
    s01.s01f01 AS StudentID,
    s01.s01f02 AS StudentName,
    d01.d01f02 AS DepartmentName
FROM
    yms01 s01
        LEFT JOIN
    yme01 e01 ON s01.s01f01 = e01.e01f02
        LEFT JOIN
    ymd01 d01 ON s01.s01f11 = d01.d01f01
WHERE
    e01.e01f02 IS NULL;

-- Unions: 
-- Combine lists of all departments and unique student names
SELECT d01f02 AS Name FROM ymd01
UNION
SELECT s01f02 AS Name FROM yms01;

-- Index:
-- Add an index to the s01f03 (email) column in the students table
CREATE INDEX idx_student_email ON yms01(s01f03);

-- View: 
-- Create a View for Students Enrolled in 'Data Structures' Courses
CREATE VIEW DataStructuresStudents AS
    SELECT 
        s01.s01f01 AS StudentID,
        s01.s01f02 AS StudentName,
        d01.d01f02 AS DepartmentName,
        c01.c01f02 AS CourseName
    FROM
        yms01 s01
            INNER JOIN
        yme01 e01 ON s01.s01f01 = e01.e01f02
            INNER JOIN
        ymc01 c01 ON e01.e01f03 = c01.c01f01
            INNER JOIN
        ymd01 d01 ON s01.s01f11 = d01.d01f01
    WHERE
        c01.c01f02 = 'Data Structures';

-- Retrieve Data from the View
SELECT * FROM DataStructuresStudents;

-- Drop the View
DROP VIEW DataStructuresStudents;

        
-- Explain Keyword: 
-- Analyze a query for performance
EXPLAIN
SELECT s01.s01f02 AS StudentName, d01.d01f02 AS Department
FROM yms01 s01
JOIN ymd01 d01 ON s01.s01f11 = d01.d01f01;


