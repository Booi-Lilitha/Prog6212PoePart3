USE ContractMonthlyClaimsDB;
GO

-- Drop tables in correct order
IF OBJECT_ID('dbo.Documents', 'U') IS NOT NULL DROP TABLE dbo.Documents;
IF OBJECT_ID('dbo.ClaimItems', 'U') IS NOT NULL DROP TABLE dbo.ClaimItems;
IF OBJECT_ID('dbo.Claims', 'U') IS NOT NULL DROP TABLE dbo.Claims;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

-- Users table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    HourlyRate DECIMAL(18,2) NOT NULL,
    Role INT NOT NULL
);
GO

-- Claims table
CREATE TABLE Claims (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LecturerUsername NVARCHAR(255) NOT NULL,
    SubmitDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    Month INT NOT NULL,
    Year INT NOT NULL,
    Hours DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    FilePath NVARCHAR(MAX) NULL,
    Status INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,  -- REQUIRED BY MODEL
    UserId INT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE SET NULL
);
GO

-- ClaimItems table (Amount INCLUDED → matches model)
CREATE TABLE ClaimItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClaimId INT NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Hours DECIMAL(18,2) NOT NULL,
    Rate DECIMAL(18,2) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,   -- REQUIRED
    FOREIGN KEY (ClaimId) REFERENCES Claims(Id) ON DELETE CASCADE
);
GO

-- Documents table
CREATE TABLE Documents (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ClaimId INT NOT NULL,
    FileName NVARCHAR(255) NOT NULL,
    StoredFileName NVARCHAR(255) NOT NULL,
    UploadDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    ContentType NVARCHAR(255) NOT NULL,
    FOREIGN KEY (ClaimId) REFERENCES Claims(Id) ON DELETE CASCADE
);
GO

/* Seed users */
INSERT INTO Users (Username, Password, FirstName, LastName, Email, HourlyRate, Role)
VALUES
('hradmin', 'Admin123!', 'System', 'Admin', 'hr@cmcs.local', 0, 3),
('lect1', 'Pass123!', 'John', 'Smith', 'john.smith@cmcs.local', 50, 0),
('lect2', 'Pass123!', 'Emily', 'Chen', 'emily.chen@cmcs.local', 50, 0),
('coord1', 'Pass123!', 'Jane', 'Miller', 'jane.miller@cmcs.local', 0, 1),
('man1', 'Pass123!', 'Sam', 'Newman', 'sam.newman@cmcs.local', 0, 2);

/* Sample claims */
INSERT INTO Claims (LecturerUsername, SubmitDate, Month, Year, Hours, Description, Status, Amount, UserId)
VALUES 
('lect1', GETDATE(), 10, 2024, 40, 'Monthly teaching claim', 0, 2000.00, 2),
('lect2', GETDATE(), 9, 2024, 36, 'September teaching hours', 0, 1800.00, 3);

/* ClaimItems WITH Amount values (REQUIRED) */
INSERT INTO ClaimItems (ClaimId, Description, Hours, Rate, Amount)
VALUES 
(1, 'Lecture Hours', 40, 50, 2000),
(2, 'Lecture Hours', 36, 50, 1800);

PRINT 'Database created and seeded successfully!';
GO
