use DatabaseProjectFinalExam
CREATE TABLE UserAccount (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    UserName VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL,
    Gmail NVARCHAR(100) NOT NULL UNIQUE
);


INSERT INTO UserAccount(Name, UserName, Password, Gmail)
VALUES 
	(N'User1', '1', '1', 'example1@gmail.com'),
	(N'User2', '2', '2', 'example2@gmail.com'),
	(N'User3', '3', '3', 'example3@gmail.com');