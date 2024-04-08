DROP DATABASE DealerMangementSystem;
CREATE DATABASE DealerMangementSystem;

USE DealerMangementSystem;


-- Creating table for Vehicle Categories
CREATE TABLE CTG01 (
    G01F01 INT AUTO_INCREMENT PRIMARY KEY, -- CategoryID
    G01F02 VARCHAR(255) NOT NULL -- CategoryName
);

-- Creating table for Inventory Management: Vehicles
CREATE TABLE VEH01 (
    H01F01 INT AUTO_INCREMENT PRIMARY KEY, -- PartID
    H01F02 VARCHAR(255) NOT NULL, -- Name
    H01F03 TEXT, -- Description
    H01F04 DECIMAL(10, 2) NOT NULL, -- Price
    H01F05 INT NOT NULL, -- StockQuantity
    H01F06 INT, -- ReorderLevel
    H01F07 INT, -- CategoryID (FK from Vehicle Categories)
    FOREIGN KEY (H01F07) REFERENCES CTG01 (G01F01)
);

-- Creating table for Customer Relationship Management (CRM): Customer Profiles
CREATE TABLE CUS01 (
    S01F01 INT AUTO_INCREMENT PRIMARY KEY, -- CustomerID
    S01F02 VARCHAR(30) NOT NULL, -- FirstName
    S01F03 VARCHAR(30) NOT NULL, -- LastName
    S01F04 VARCHAR(50) NOT NULL, -- Email
    S01F05 VARCHAR(15), -- Phone
    S01F06 TEXT -- Address
);

-- Creating table for Sales and Deal Structuring
CREATE TABLE SAL01 (
    L01F01 INT AUTO_INCREMENT PRIMARY KEY, -- DealID
    L01F02 INT, -- CustomerID (FK from Customer Profiles)
    L01F03 INT, -- VehicleID (FK from Vehicle Inventory)
    L01F04 DECIMAL(10, 2) NOT NULL, -- SalePrice
    L01F05 DATE NOT NULL, -- Date
    L01F06 BOOLEAN, -- CreditApproved
    L01F07 BOOLEAN, -- ContractSigned
    FOREIGN KEY (L01F02) REFERENCES CUS01 (S01F01),
    FOREIGN KEY (L01F03) REFERENCES VEH01 (H01F01)
);

-- Creating table for User Management
CREATE TABLE USR01 (
    R01F01 INT AUTO_INCREMENT PRIMARY KEY, -- UserID
    R01F02 VARCHAR(30) NOT NULL, -- Username
    R01F03 VARCHAR(255) NOT NULL, -- PasswordHash
    R01F04 CHAR(1) NOT NULL, -- Role
    R01F05 VARCHAR(50) NOT NULL, -- Email
    R01F06 DATETIME NOT NULL -- CreatedAt
);

-- Creating table for Dealer Management
CREATE TABLE DEA01 (
    A01F01 INT AUTO_INCREMENT PRIMARY KEY, -- DealerID
    A01F02 VARCHAR(50) NOT NULL, -- DealerName
    A01F03 VARCHAR(255), -- ContactInfo
    A01F04 TEXT -- Address
);


-- Creating table for Dealer Transactions
CREATE TABLE DEA02 (
    A02F01 INT AUTO_INCREMENT PRIMARY KEY, -- DealerTransID
    A02F02 INT, -- DealerID (FK from Dealer Management)
    A02F03 INT, -- VehicleID (FK from Vehicle Inventory)
    A02F04 DECIMAL(10, 2) NOT NULL, -- PurchasePrice
    A02F05 DECIMAL(10, 2) NOT NULL, -- SellingPrice
    A02F06 DECIMAL(10, 2), -- TaxAmount
    A02F07 DECIMAL(10, 2), -- ProfitMargin
    A02F08 DATETIME NOT NULL, -- TransactionDate
    FOREIGN KEY (A02F02) REFERENCES DEA01 (A01F01),
    FOREIGN KEY (A02F03) REFERENCES VEH01 (H01F01)
);

-- Creating table for Customer Transactions
CREATE TABLE CUS02 (
    S02F01 INT AUTO_INCREMENT PRIMARY KEY, -- CustTransID
    S02F02 INT, -- CustomerID (FK from Customer Profiles)
    S02F03 INT, -- VehicleID (FK from Vehicle Inventory)
    S02F04 DECIMAL(10, 2) NOT NULL, -- SalePrice
    S02F05 DECIMAL(10, 2), -- TaxAmount
    S02F06 DATETIME NOT NULL, -- TransactionDate
    FOREIGN KEY (S02F02) REFERENCES DEA01 (A01F01),
    FOREIGN KEY (S02F03) REFERENCES VEH01 (H01F01)
);
