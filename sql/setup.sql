USE InsurancePortal;
GO

INSERT INTO Policyholders
(
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode,
    Email,
    Phone
)
VALUES
(
    '11111111-1111-1111-1111-111111111111',
    '6666',
    'VA6666667',
    'Chandler',
    'Bing',
    '11/20/2024',
    '99501',
    'chandler@example.com',
    '9004008000'
);
GO
USE InsurancePortal;
GO

SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
GO
SELECT *
FROM __EFMigrationsHistory;
GO
SELECT *
FROM Policyholders;
GO

SELECT * FROM Accounts;
GO

SELECT * FROM Contacts;
GO

SELECT * FROM SecurityInformation;
GO
SELECT * FROM Policyholders;
GO

SELECT * FROM Accounts;
GO

SELECT * FROM Contacts;
GO

SELECT * FROM SecurityInformation;
GO
USE InsurancePortal;
GO

SELECT Id, PolicyholderId, Username
FROM Accounts;
SELECT *
FROM Contacts;

USE InsurancePortal;
GO

SELECT DB_NAME() AS CurrentDatabase;
GO

SELECT COUNT(*) AS AccountCount
FROM Accounts;
GO

SELECT Id, PolicyholderId, Username
FROM Accounts
WHERE Username = 'chandler123';
GO

SELECT COUNT(*) AS ContactCount
FROM Contacts;
GO

SELECT COUNT(*) AS SecurityInformationCount
FROM SecurityInformation;
GO

USE InsurancePortal;
GO

INSERT INTO Policyholders
(
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode,
    Email,
    Phone
)
VALUES
(
    '22222222-2222-2222-2222-222222222222',
    '7777',
    'VA7777777',
    'Monica',
    'Geller',
    '06/18/1990',
    '99501',
    'monica@example.com',
    '9005009000'
);
GO

SELECT *
FROM Policyholders
WHERE SSN = '7777';


SELECT * FROM Accounts
WHERE Username = 'monica123';

SELECT * FROM Contacts
WHERE PolicyholderId = '22222222-2222-2222-2222-222222222222';

SELECT * FROM SecurityInformation
WHERE PolicyholderId = '22222222-2222-2222-2222-222222222222';

USE InsurancePortal;
GO

SELECT
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode
FROM Policyholders
WHERE SSN = '7777';
GO

SELECT
    Id,
    PolicyholderId,
    Username,
    PasswordHash
FROM Accounts
WHERE Username = 'monica123';
GO

SELECT
    Id,
    PolicyholderId,
    Email,
    Phone,
    City,
    State,
    ZipCode
FROM Contacts
WHERE PolicyholderId = '22222222-2222-2222-2222-222222222222';
GO

SELECT
    Id,
    PolicyholderId,
    NicknameHash,
    ChildhoodHeroHash
FROM SecurityInformation
WHERE PolicyholderId = '22222222-2222-2222-2222-222222222222';
GO

USE InsurancePortal;
GO

SELECT
    a.Username,
    a.PolicyholderId,
    c.Email,
    c.Phone
FROM Accounts a
INNER JOIN Contacts c
    ON a.PolicyholderId = c.PolicyholderId
WHERE a.Username = 'monica123';
GO
SELECT * FROM InsurancePortal

SELECT * FROM Policyholders;
GO

USE InsurancePortal;
GO

INSERT INTO Policyholders
(
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode,
    Email,
    Phone
)
VALUES
(
    '22222222-2222-2222-2222-222222222222',
    '7777',
    'VA7788995',
    'Joey',
    'Tribbiani',
    '11/20/2024',
    '99501',
    'joey@example.com',
    '9004008000'
),
(
    '33333333-3333-3333-3333-333333333333',
    '3333',
    'VA7788985',
    'Mohamad',
    'Nahal',
    '12/20/2024',
    '99501',
    'Nahal@example.com',
    '8888888888'
),
(
    '44444444-4444-4444-4444-444444444444',
    '4444',
    'VA8899006',
    'Dev',
    'krishna',
    '10/15/2024',
    '99501',
    'dev@example.com',
    '7777777777'
);
GO

USE InsurancePortal;
GO

SELECT
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName
FROM Policyholders
WHERE Id IN
(
    '22222222-2222-2222-2222-222222222222',
    '33333333-3333-3333-3333-333333333333',
    '44444444-4444-4444-4444-444444444444'
);
GO

USE InsurancePortal;
GO

INSERT INTO Policyholders
(
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode,
    Email,
    Phone
)
VALUES
(
    '33333333-3333-3333-3333-333333333333',
    '3333',
    'VA7788985',
    'Mohamad',
    'Nahal',
    '12/20/2024',
    '99501',
    'Nahal@example.com',
    '8888888888'
),
(
    '44444444-4444-4444-4444-444444444444',
    '4444',
    'VA8899006',
    'Dev',
    'krishna',
    '10/15/2024',
    '99501',
    'dev@example.com',
    '7777777777'
);
GO


SELECT
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName
FROM Policyholders;
GO

SELECT *
FROM Accounts
WHERE Username = 'mohamad123';

SELECT *
FROM Contacts
WHERE Email = 'mohamad.test@example.com';

SELECT *
FROM SecurityInformation
WHERE PolicyholderId = '33333333-3333-3333-3333-333333333333';
SELECT *
FROM Accounts;
SELECT *
FROM Contacts;

USE InsurancePortal;
GO
INSERT INTO Policyholders
(
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode,
    Email,
    Phone
)
VALUES
(
    '55555555-5555-5555-5555-555555555555',
    '3333',
    'VA7788985',
    'Mohamad',
    'Nahal',
    '12/20/2024',
    '99501',
    'Nahal@example.com',
    '8888888888'
);