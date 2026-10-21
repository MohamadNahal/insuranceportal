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
    '5555',
    'VA7788925',
    'Vishnu',
    'Nair',
    '12/10/2024',
    '99501',
    'vishnu@example.com',
    '8888888888'
);

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
    '66666666-6666-6666-6666-666666666666',
    '6666',
    'VA6677886',
    'Alen',
    'Thomas',
    '12/20/2024',
    '99501',
    'alen@example.com',
    '6666666666'
),
(
    '77777777-7777-7777-7777-777777777777',
    '7777',
    'VA7788997',
    'Alex',
    'Mathew',
    '10/15/2024',
    '99501',
    'alex@example.com',
    '7777777777'
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
    '90909090-9090-9090-9090-909090909090',
    '9090',
    'VA9090909',
    'Kenny',
    'George',
    '08/25/2024',
    '99502',
    'kenny@example.com',
    '9090909090'
),
(
    '99999999-9999-9999-9999-999999999999',
    '9999',
    'VA9999999',
    'Shahal',
    'Abdullah',
    '11/10/2024',
    '99503',
    'shahal@example.com',
    '9999999999'
);
GO

USE InsurancePortal;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '11111111-1111-1111-1111-111111111111'
)
BEGIN
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
        '11/20/1988',
        '99501',
        'chandler@example.com',
        '9004008000'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '22222222-2222-2222-2222-222222222222'
)
BEGIN
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
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '33333333-3333-3333-3333-333333333333'
)
BEGIN
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
        '11/14/1990',
        '99501',
        'Nahal@example.com',
        '8888888888'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '44444444-4444-4444-4444-444444444444'
)
BEGIN
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
        '44444444-4444-4444-4444-444444444444',
        '4444',
        'VA8899006',
        'Dev',
        'Krishna',
        '02/17/1987',
        '99501',
        'dev@example.com',
        '7777777777'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '55555555-5555-5555-5555-555555555555'
)
BEGIN
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
        '5555',
        'VA7788925',
        'Vishnu',
        'Nair',
        '01/19/1993',
        '99501',
        'vishnu@example.com',
        '8888888888'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '66666666-6666-6666-6666-666666666666'
)
BEGIN
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
        '66666666-6666-6666-6666-666666666666',
        '6666',
        'VA6677886',
        'Alen',
        'Thomas',
        '04/12/1988',
        '99501',
        'alen@example.com',
        '6666666666'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '77777777-7777-7777-7777-777777777777'
)
BEGIN
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
        '77777777-7777-7777-7777-777777777777',
        '7777',
        'VA7788997',
        'Alex',
        'Mathew',
        '09/25/1992',
        '99501',
        'alex@example.com',
        '7777777777'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '88888888-8888-8888-8888-888888888888'
)
BEGIN
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
        '88888888-8888-8888-8888-888888888888',
        '8888',
        'VA8899008',
        'Gokul',
        'Raj',
        '11/25/1991',
        '99501',
        'gokul@example.com',
        '8888888888'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '90909090-9090-9090-9090-909090909090'
)
BEGIN
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
        '90909090-9090-9090-9090-909090909090',
        '9090',
        'VA9090909',
        'Kenny',
        'George',
        '03/21/1985',
        '99502',
        'kenny@example.com',
        '9090909090'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '99999999-9999-9999-9999-999999999999'
)
BEGIN
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
        '99999999-9999-9999-9999-999999999999',
        '9999',
        'VA9999999',
        'Shahal',
        'Abdullah',
        '06/30/1989',
        '99503',
        'shahal@example.com',
        '9999999999'
    );
END;
GO

SELECT
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode,
    Email,
    Phone
FROM Policyholders
ORDER BY FirstName;
GO

USE InsurancePortal;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '88888888-8888-8888-8888-888888888888'
)
BEGIN
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
        '88888888-8888-8888-8888-888888888888',
        '8888',
        'VA8899008',
        'Gokul',
        'Raj',
        '11/25/1991',
        '99501',
        'gokul@example.com',
        '8888888888'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '90909090-9090-9090-9090-909090909090'
)
BEGIN
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
        '90909090-9090-9090-9090-909090909090',
        '9090',
        'VA9090909',
        'Kenny',
        'George',
        '03/21/1985',
        '99502',
        'kenny@example.com',
        '9090909090'
    );
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM Policyholders
    WHERE Id = '99999999-9999-9999-9999-999999999999'
)
BEGIN
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
        '99999999-9999-9999-9999-999999999999',
        '9999',
        'VA9999999',
        'Shahal',
        'Abdullah',
        '06/30/1989',
        '99503',
        'shahal@example.com',
        '9999999999'
    );
END;
GO

SELECT
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth
FROM Policyholders
ORDER BY FirstName;
GO




USE InsurancePortal;
GO

SELECT DB_NAME() AS CurrentDatabase;
GO

SELECT 'Policyholders' AS TableName, COUNT(*) AS TotalRows
FROM dbo.Policyholders

UNION ALL

SELECT 'Accounts', COUNT(*)
FROM dbo.Accounts

UNION ALL

SELECT 'Contacts', COUNT(*)
FROM dbo.Contacts

UNION ALL

SELECT 'SecurityInformation', COUNT(*)
FROM dbo.SecurityInformation;
GO

SELECT
    Id,
    FirstName,
    LastName,
    PolicyNumber,
    DateOfBirth
FROM dbo.Policyholders
ORDER BY LastName, FirstName;
GO


USE InsurancePortal;
GO

SELECT
    a.Username,
    p.FirstName,
    p.LastName
FROM dbo.Accounts AS a
INNER JOIN dbo.Policyholders AS p
    ON p.Id = a.PolicyholderId;
GO



USE InsurancePortal;
GO

SELECT
    p.FirstName,
    p.LastName,
    p.SSN,
    p.PolicyNumber,
    p.DateOfBirth,
    p.ZipCode,
    CASE
        WHEN a.Id IS NULL THEN 'Not Registered'
        ELSE 'Already Registered'
    END AS RegistrationStatus
FROM dbo.Policyholders AS p
LEFT JOIN dbo.Accounts AS a
    ON a.PolicyholderId = p.Id
ORDER BY p.FirstName;
GO



USE InsurancePortal;
GO

SELECT
    p.FirstName,
    p.LastName,
    a.Username,
    CASE WHEN c.PolicyholderId IS NOT NULL
        THEN 'Saved' ELSE 'Missing' END AS ContactStatus,
    CASE WHEN s.PolicyholderId IS NOT NULL
        THEN 'Saved' ELSE 'Missing' END AS SecurityStatus
FROM dbo.Policyholders AS p
INNER JOIN dbo.Accounts AS a
    ON a.PolicyholderId = p.Id
LEFT JOIN dbo.Contacts AS c
    ON c.PolicyholderId = p.Id
LEFT JOIN dbo.SecurityInformation AS s
    ON s.PolicyholderId = p.Id;
GO

SELECT PolicyholderId, COUNT(*) AS RecordCount
FROM Contacts
GROUP BY PolicyholderId
HAVING COUNT(*) > 1;


SELECT PolicyholderId, COUNT(*) AS RecordCount
FROM SecurityInformation
GROUP BY PolicyholderId
HAVING COUNT(*) > 1;

SELECT PolicyholderId, COUNT(*) AS RecordCount
FROM Contacts
GROUP BY PolicyholderId
HAVING COUNT(*) > 1;

SELECT PolicyholderId, COUNT(*) AS RecordCount
FROM SecurityInformation
GROUP BY PolicyholderId
HAVING COUNT(*) > 1;

SELECT MigrationId
FROM __EFMigrationsHistory
ORDER BY MigrationId;

SELECT
    t.name AS TableName,
    i.name AS IndexName,
    i.is_unique AS IsUnique
FROM sys.indexes i
JOIN sys.tables t ON i.object_id = t.object_id
WHERE t.name IN ('Contacts', 'SecurityInformation')
  AND i.name IN (
      'IX_Contacts_PolicyholderId',
      'IX_SecurityInformation_PolicyholderId'
  )
ORDER BY t.name;

SELECT Username
FROM Accounts
ORDER BY Username;


SELECT
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME LIKE '%Policyholder%'
ORDER BY TABLE_NAME, ORDINAL_POSITION;


SELECT
    Id,
    SSN,
    PolicyNumber,
    FirstName,
    LastName,
    DateOfBirth,
    ZipCode
FROM Policyholders
ORDER BY LastName, FirstName;

SELECT
    Username,
    PolicyholderId
FROM Accounts
ORDER BY Username;



SELECT
    p.FirstName,
    p.LastName,
    p.PolicyNumber,
    CASE
        WHEN a.PolicyholderId IS NULL THEN 'Not Registered'
        ELSE 'Already Registered'
    END AS RegistrationStatus,
    a.Username
FROM Policyholders p
LEFT JOIN Accounts a
    ON a.PolicyholderId = p.Id
ORDER BY p.LastName, p.FirstName;



SELECT
    p.FirstName,
    p.LastName,
    p.PolicyNumber,
    p.DateOfBirth,
    p.ZipCode
FROM Policyholders p
WHERE NOT EXISTS (
    SELECT 1
    FROM Accounts a
    WHERE a.PolicyholderId = p.Id
)
ORDER BY p.LastName, p.FirstName;


SELECT
    FirstName,
    LastName,
    SSN,
    PolicyNumber
FROM Policyholders
WHERE FirstName IN ('Kenny', 'Rahul', 'Cyril', 'Isaac')
ORDER BY FirstName;