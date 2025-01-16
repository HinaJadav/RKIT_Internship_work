-- Department-Wise Overview of Students, Courses, and Enrollment Statistics

SELECT 
    d01.d01f02 AS DepartmentName,
    d01.d01f03 AS HODName,
    COUNT(DISTINCT s01.s01f01) AS TotalStudents,
    COUNT(DISTINCT c01.c01f01) AS TotalCourses,
    COUNT(e01.e01f01) AS TotalEnrollments,
    SUM(CASE
        WHEN e01.e01f05 = 'Active' THEN 1
        ELSE 0
    END) AS ActiveEnrollments,
    SUM(CASE
        WHEN s01.s01f10 = 'PAID' THEN 1
        ELSE 0
    END) AS FeesPaidStudents
FROM
    collegemanagementsystem.ymd01 d01
        LEFT JOIN
    collegemanagementsystem.ymc01 c01 ON d01.d01f01 = c01.c01f03
        LEFT JOIN
    collegemanagementsystem.yme01 e01 ON c01.c01f01 = e01.e01f03
        LEFT JOIN
    collegemanagementsystem.yms01 s01 ON e01.e01f02 = s01.s01f01
GROUP BY d01.d01f02 , d01.d01f03;   




-- CASE WHEN : conditional expression 
-- It allows apply logic within query 
-- It works like if-else statement