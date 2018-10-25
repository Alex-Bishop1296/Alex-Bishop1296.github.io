CREATE TABLE [dbo].[OrderSheets]
(	
    [ID]				INT IDENTITY (1,1)		NOT NULL,
    [FirstName]			NVARCHAR(64)			NOT NULL,
    [LastName]			NVARCHAR(128)			NOT NULL,
    [PhoneNumber]       NVARCHAR(11)			NOT NULL,
	[ApartmentName]		NVARCHAR(64)			NOT NULL,
	[UnitNumber]		INT						NOT NULL,
	[RequestDetails]	NVARCHAR(1000)			NOT NULL,
	[Permission]		BIT						NOT NULL,
	[SubmitTime]		DATETIME				NOT NULL
    CONSTRAINT [PK_dbo.OrderSheets] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

INSERT INTO [dbo].[OrderSheets] (FirstName, LastName, PhoneNumber, ApartmentName, UnitNumber, RequestDetails, Permission, SubmitTime) VALUES
	 ('James','Sunderland','14996678594','Tranquil Pyramid','43','Lost Key in the Toilet ... again.','1','2018-10-12 09:00:00'),
	 ('Henry','Townshed','18724789234','Morning Embrace','23','I can not get out of my room.','1','2018-10-12 11:00:00'),
	 ('Heather','Mason','13949394943','Wonders Park','89','Faucets are rusted shut, graffiti on bathroom mirrors','1','2018-10-12 19:00:00'),
	 ('Eileen','Galvin','18724491113','Morning Embrace','22','Please get Henry out of his room.','0','2018-10-12 13:00:00'),
	 ('Harry','Mason','13949394943','Wonders Park','89','Have you see a missing little girl, about 10 years old, this tall.','0','2018-10-12 21:00:00')
GO