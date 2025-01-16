-- Full Query: This query will use JOIN, LIMIT, SUBQUERY, AGGREGATION FUNCTION, and UNION.
-- The name of each department along with the total number of students in that department who are enrolled in a course. For each department, display the course name.
-- For the "Mechanical Engineering" department, find the total number of students who are not enrolled in any course, and display 'No Course' as the course name for them.
SELECT 
    d01.d01f02 AS DepartmentName,
    COUNT(s01.s01f01) AS TotalStudents,
    c01.c01f02 AS CourseName
FROM 
    yms01 s01
JOIN 
    ymd01 d01 ON s01.s01f11 = d01.d01f01
LEFT JOIN 
    yme01 e01 ON s01.s01f01 = e01.e01f02
LEFT JOIN 
    ymc01 c01 ON e01.e01f03 = c01.c01f01
GROUP BY 
    d01.d01f02, c01.c01f02
UNION
SELECT 
    'No Course' AS DepartmentName, 
    COUNT(s01.s01f01) AS TotalStudents, 
    NULL AS CourseName
FROM 
    yms01 s01
JOIN 
    ymd01 d01 ON s01.s01f11 = d01.d01f01
WHERE 
    d01.d01f02 = 'Mechanical Engineering'
    AND s01.s01f01 NOT IN (SELECT e01f02 FROM yme01)
GROUP BY 
    d01.d01f02
ORDER BY 
    DepartmentName
LIMIT 5;
