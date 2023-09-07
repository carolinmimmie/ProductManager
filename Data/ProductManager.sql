-- SQL (SEQUEL)
-- * domänspecifikt programmerinngsspråk för att hantera RDBMS:en + för att
-- skapa upp tabeller, jobba med data, etc.
-- * uppdelat i subspråk
-- DDL = Data Definition Language (CREATE DATABASE, CREATE TABLE)
-- DML = Data Manipulation Language (INSERT INTO, UPDATE, DELETE)
-- DQL = Data Query Language (SELECT)



CREATE DATABASE ProductManager
GO

USE ProductManager
GO

DROP TABLE Product

-- Primärnyckel
-- * Måste vara unik (värdet)
-- * Får inte vara null (värdet)
-- * Får inte förändras vid någon tidpunkt (värdet)

-- Surogatnyckel
-- * fiktivt värde som är unikt

-- UNIQUE

CREATE TABLE Product
(
    -- Surogatnyckel = fiktiv nyckel/värde som vi använder som
    -- primärnyckel då det saknas en bra naturlig nyckel i detta fallet
    -- IDENTITY = genererar och sätter automatiskt ett heltal med start från 1 och
    -- uppåt
    Id INT IDENTITY,
    Name NVARCHAR(50) NOT NULL,
    Sku NVARCHAR(50) NOT NULL,
    -- Type INT NOT NULL,
    Description NVARCHAR(50) NOT NULL,
    Image NVARCHAR(50) NOT NULL,
    Price NVARCHAR(10) NOT NULL,
    -- Specificerar att Id ska vara primärnyckel
    PRIMARY KEY (Id),
    -- Specificerar att det enbart får finnas en rad i tabellen som har
    -- ett specifikt registreringsnummer och detta läggs direkt i databasen
    UNIQUE (Sku)
)

-- DML (Data Manipulation Language) (INSERT INTO, UPDATE, DELETE)
-- DQL (Data Query Language) (SELECT)

INSERT INTO Product
    (Name, Sku, Description, Image, Price)
VALUES
    ('Svart T-Shirt', 'ABC123', 'Stilren och tidlös svart t-shirt av finaste bomull', 'http://domain.com/image.png', '199');

UPDATE Product SET Name = 'Svart Tröja'
WHERE Sku = 'ABC123'

DELETE FROM Product 
WHERE Sku = 'BBB222'

DELETE FROM Product 
WHERE Sku = 'ABC123'

-- Select allting
SELECT *
FROM Product

-- Select specifics
SELECT Name, Sku
FROM Product

-- Select even more specific
SELECT Name, Description
FROM Product
WHERE Sku = 'ABC123'

