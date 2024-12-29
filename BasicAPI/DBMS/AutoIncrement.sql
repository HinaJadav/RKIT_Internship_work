-- Auto-increment field

-- Remove the existing primary key
ALTER TABLE employee DROP PRIMARY KEY;

-- Add a new primary key with auto-increment value
ALTER TABLE employee MODIFY ID INT AUTO_INCREMENT PRIMARY KEY;

-- Set the starting value for the auto-increment process
ALTER TABLE employee AUTO_INCREMENT = 1;
