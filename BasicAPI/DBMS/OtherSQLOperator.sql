-- LIKE Operator with Wildcard Characters(%, _)

-- Find employees which name start with "a"
SELECT 
    *
FROM
    employee
WHERE
    Name LIKE 'a%';
    
-- Find employees which name end with "i"
SELECT 
    *
FROM
    employee
WHERE
    Name LIKE '%i';
    
-- Find employees which name have h at any position 
SELECT 
    *
FROM
    employee
WHERE
    Name LIKE '%h%';
    
-- Find employees which name start with "a" and have atleast 6 characters
SELECT 
    *
FROM
    employee
WHERE
    Name LIKE 'a_____%';
    
-- Find employees which name second charcter is "a"
SELECT 
    *
FROM
    employee
WHERE
    Name LIKE '_a%';
    
-- Find employees which name start with "a" and end with "i"
SELECT 
    *
FROM
    employee
WHERE
    Name LIKE 'a%i';


-- AND, OR and NOT Operators

-- and
SELECT 
    Name
FROM
    employee
WHERE
    Description = 'Developer'
        AND Salary = 50000;
        
-- or
SELECT 
    *
FROM
    employee
WHERE
    Description = 'Developer'
        OR Salary = 50000;
        
-- not
SELECT 
    Name
FROM
    employee
WHERE
    NOT Description = 'Developer'; 