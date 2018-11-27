--Create the table for the Buyers
CREATE TABLE [dbo].[Buyers] (
	[BuyerID]		INT IDENTITY(0,1)	NOT NULL PRIMARY KEY,
	[BuyerName]		NVARCHAR(128)		NOT NULL
);

--Create the table for the Sellers
CREATE TABLE [dbo].[Sellers] (
	[SellerID]		INT IDENTITY(0,1)	NOT NULL PRIMARY KEY,
	[SellerName]	NVARCHAR(128)		NOT NULL,
);

--Create the table for the Items
CREATE TABLE [dbo].[Items] (
	[ItemID]			INT IDENTITY(1001,1)	NOT NULL PRIMARY KEY,
	[ItemName]			NVARCHAR(128)			NOT NULL,
	[ItemDescription]	NVARCHAR(256)			NOT NULL,
	[SellerID]			INT						NOT NULL,
	CONSTRAINT [FK_Items_Sellers] FOREIGN KEY ([SellerID]) REFERENCES [Sellers]([SellerID])
);

--Create a table for the Bids
CREATE TABLE [dbo].[Bids] (
	[BidID]				INT IDENTITY(0,1)	NOT NULL PRIMARY KEY,
	[ItemID]			INT					NOT NULL,
	[BuyerID]			INT					NOT NULL,
	[Price]				DECIMAL(25,2)		NOT NULL,
	[Timestamp]			DATETIME			NOT NULL
	CONSTRAINT [FK_Bids_Items] FOREIGN KEY ([ItemID]) REFERENCES [Items]([ItemID]),
	CONSTRAINT [FK_Bids_Buyers] FOREIGN KEY ([BuyerID]) REFERENCES [Buyers]([BuyerID])
);

-- Buyers
INSERT INTO [dbo].[Buyers](BuyerName) VALUES
('Jane Stone'),
('Tom McMasters'),
('Otto Vanderwall');

-- Sellers
INSERT INTO [dbo].[Sellers](SellerName) VALUES
('Gayle Hardy'),
('Lyle Banks'),
('Pearl Greene');

-- Items
INSERT INTO [dbo].[Items](ItemName, ItemDescription, SellerID) VALUES
('Abraham Lincoln Hammer'    ,'A bench mallet fashioned from a broken rail-splitting maul in 1829 and owned by Abraham Lincoln', 2),
('Albert Einsteins Telescope','A brass telescope owned by Albert Einstein in Germany, circa 1927', 0),
('Bob Dylan Love Poems'      ,'Five versions of an original unpublished, handwritten, love poem by Bob Dylan', 1);

--Bids
INSERT INTO [dbo].[Bids](ItemID, BuyerID, Price, Timestamp) VALUES
(1001, 2, 250000,'12/04/2017 09:04:22'),
(1003, 0, 95000 ,'12/04/2017 08:44:03');