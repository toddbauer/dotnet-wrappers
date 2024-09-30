CREATE TABLE TestUsers (
    Id NUMERIC IDENTITY(1, 1),
    [FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
    PRIMARY KEY (Id)
);
GO

INSERT INTO [TestUsers] ([FirstName], [LastName])
VALUES
('John', 'Doe'),
('Jane', 'Doe'),
('John', 'Smith');
GO
