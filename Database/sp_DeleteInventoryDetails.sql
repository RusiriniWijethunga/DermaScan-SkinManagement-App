USE [dermascan]
GO

/****** Object:  StoredProcedure [dbo].[sp_DeleteInventoryDetails]    Script Date: 3/27/2025 10:08:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DeleteInventoryDetails]
@ProductId INT
AS
BEGIN
DELETE FROM Inventory
WHERE ProductId= @ProductId
END
GO

