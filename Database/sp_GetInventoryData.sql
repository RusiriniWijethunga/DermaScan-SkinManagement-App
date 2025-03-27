USE [dermascan]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetInventoryData]    Script Date: 3/27/2025 10:08:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetInventoryData]
AS
BEGIN
	SELECT * FROM Inventory

END
GO

