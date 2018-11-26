--Create the table for the Buyers
CREATE TABLE [dbo].[Buyers] (
	[BuyerID]	INT IDENTITY(0,1)	NOT NULL,
	[Buyer]		NVARCHAR(128)		NOT NULL
	CONSTRAINT	[PK_dbo.Buyers] PRIMARY KEY CLUSTERED ([BuyerID] ASC)
);

--Create the table for the Sellers
CREATE TABLE [dbo].[Sellers] (
	[SellerID]	INT IDENTITY(0,1)	NOT NULL,
	[Seller]	NVARCHAR(128)		NOT NULL,
	CONSTRAINT	[PK_dbo.Sellers] PRIMARY KEY CLUSTERED ([SellerID] ASC)
);

--Create the table for the Items
CREATE TABLE [dbo].[Items] (
	[ItemID]			INT IDENTITY(1001,1)	NOT NULL,
	[ItemName]			NVARCHAR(128)		NOT NULL,
	[ItemDescription]	NVARCHAR(256)		NOT NULL,
	[Seller]			INT					NOT NULL,
	CONSTRAINT	[PK_dbo.Items] PRIMARY KEY CLUSTERED ([ItemID] ASC),
	CONSTRAINT [FK_Items_Sellers] FOREIGN KEY ([Seller]) REFERENCES [Sellers]([SellerID])
);

--Create a table for the Bids
CREATE TABLE [dbo].[Bids] (
	[BidID]				INT IDENTITY(0,1)	NOT NULL,
	[ItemID]			INT					NOT NULL,
	[Buyer]				INT					NOT NULL,
	[Price]				DECIMAL(25,2)		NOT NULL,
	[Timestamp]			DATETIME			NOT NULL
	CONSTRAINT	[PK_dbo.Bids] PRIMARY KEY CLUSTERED ([BidID] ASC),
	CONSTRAINT [FK_Bids_Items] FOREIGN KEY ([ItemID]) REFERENCES [Items]([ItemID]),
	CONSTRAINT [FK_Bids_Buyers] FOREIGN KEY ([Buyer]) REFERENCES [Buyers]([BuyerID])
);