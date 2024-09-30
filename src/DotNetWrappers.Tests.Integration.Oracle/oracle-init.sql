CREATE TABLE TestUsers (
    Id NUMBER GENERATED ALWAYS AS IDENTITY,
    FirstName NVARCHAR2(50) NOT NULL,
	LastName NVARCHAR2(50) NOT NULL
);

INSERT INTO TestUsers (FirstName, LastName) VALUES ('John', 'Doe');
INSERT INTO TestUsers (FirstName, LastName) VALUES ('Jane', 'Doe');
INSERT INTO TestUsers (FirstName, LastName) VALUES ('John', 'Smith');