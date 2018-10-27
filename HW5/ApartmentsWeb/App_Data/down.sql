IF EXISTS(	SELECT *
			FROM [dbo].[OrderSheets]
		 )
	DROP TABLE [dbo].[OrderSheets]
GO