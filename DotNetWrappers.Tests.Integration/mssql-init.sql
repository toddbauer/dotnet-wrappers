CREATE TABLE TestUsers (
    Id UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
    PRIMARY KEY (Id)
);
GO

INSERT INTO [TestUsers] ([Id], [FirstName], [LastName])
VALUES
(NEWID(), 'John', 'Doe'),
(NEWID(), 'Jane', 'Doe'),
(NEWID(), 'John', 'Smith');
GO
