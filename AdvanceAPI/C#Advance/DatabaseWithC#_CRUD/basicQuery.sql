CREATE TABLE YMT01 (
    T01F01 INT AUTO_INCREMENT PRIMARY KEY,               -- Primary key, auto-incremented
    T01F02 VARCHAR(50) NOT NULL,                        -- Required string with max length of 50
    T01F03 INT NOT NULL,                                -- Required integer (Foreign key to GAM01)
    T01F04 VARCHAR(50) NOT NULL,                       -- Required string with max length of 50
    T01F05 DATETIME DEFAULT CURRENT_TIMESTAMP,         -- Defaults to the current timestamp
    CONSTRAINT FK_YMT01_GAM01 FOREIGN KEY (T01F03) REFERENCES GAM01(M01F01) -- Foreign key
);

INSERT INTO YMT01 (T01F02, T01F03, T01F04) VALUES 
('Team A', 1, 'Captain A');

INSERT INTO YMT01 (T01F02, T01F03, T01F04) VALUES 
('Team B', 2, 'Captain B');

INSERT INTO YMT01 (T01F02, T01F03, T01F04) VALUES 
('Team C', 3, 'Captain C');

INSERT INTO YMT01 (T01F02, T01F03, T01F04) VALUES 
('Team D', 4, 'Captain D');

INSERT INTO YMT01 (T01F02, T01F03, T01F04) VALUES 
('Team E', 5, 'Captain E');
