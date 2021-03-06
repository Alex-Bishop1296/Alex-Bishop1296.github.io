﻿/* Tablename needs to be plural for all of the generated Controller to work */
CREATE TABLE [dbo].[OrderSheets]
(	
    [ID]				INT IDENTITY (1,1)		NOT NULL,
    [FirstName]			NVARCHAR(64)			NOT NULL,
    [LastName]			NVARCHAR(128)			NOT NULL,
    [PhoneNumber]       NVARCHAR(14)			NOT NULL,
	[ApartmentName]		NVARCHAR(64)			NOT NULL,
	[UnitNumber]		INT						NOT NULL,
	[RequestDetails]	NVARCHAR(1000)			NOT NULL,
	[Permission]		BIT						NOT NULL,
	[SubmitTime]		DATETIME				NOT NULL
    CONSTRAINT [PK_dbo.OrderSheets] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

/* Seeds the new table OrderSheets with 5 entries */
INSERT INTO [dbo].[OrderSheets] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, RequestDetails, Permission, SubmitTime) VALUES
	 ('James','Sunderland','1-499-667-8594','Tranquil Pyramid','43','Lost Key in the Toilet ... again.','1','2018-10-12 09:00:00'),
	 ('Henry','Townshed','1-872-478-9234','South Ashfield Heights','302','I can not get out of my room.','1','2018-10-12 11:00:00'),
	 ('Heather','Mason','1-394-939-4943','Wonders Park','89','Faucets are rusted shut, graffiti on bathroom mirrors','1','2018-10-12 19:00:00'),
	 ('Eileen','Galvin','1-872-449-1113','South Ashfield Heights','303','Please get Henry out of his room.','0','2018-10-12 13:00:00'),
	 ('Harry','Mason','1-394-939-4943','Wonders Park','89','Daughter keeps hiding in the open ventilation duck, please fix this.','0','2018-10-12 21:00:00')
GO