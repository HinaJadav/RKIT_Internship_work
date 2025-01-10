-- Index:
-- An index in DBMS is a data structure that improves the speed of data retrieval operations on a database table at the cost of additional storage and maintenance overhead.

-- Create a regular index on the 'Name' column of the 'employee' table.
-- This helps in speeding up queries that filter or sort based on the 'Name' column.
CREATE INDEX nameIndex ON employee (Name);

-- Create a unique index on the 'Email' column of the 'employee' table.
-- This ensures that the 'Email' column values are unique, preventing duplicate entries.
CREATE UNIQUE INDEX emailIndex ON employee (Email);

-- Drop the index named 'nameIndex' from the 'employee' table.
-- This is typically done to remove an unused or unnecessary index to optimize performance.
ALTER TABLE employee DROP INDEX nameIndex;

-- Custom Index: A user-defined index tailored to improve the performance of specific queries by indexing relevant columns.

-- Forced Index: A hint given to the database to explicitly use a specified index, overriding the database's automatic index selection.