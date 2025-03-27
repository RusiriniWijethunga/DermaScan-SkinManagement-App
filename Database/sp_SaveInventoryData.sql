USE [dermascan]
GO

/****** Object:  StoredProcedure [dbo].[sp_SaveInventoryData]    Script Date: 3/27/2025 10:09:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveInventoryData]
	@ProductID INT OUTPUT,	
	@ProductType VARCHAR(100),
    @ProductName VARCHAR(100),
    @ExpireDate DATE
    
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 INSERT Inventory (
        ProductType,
        ProductName,
        ExpireDate
    )
    VALUES (
        @ProductType,
        @ProductName, 
        @ExpireDate
    )

	SET @ProductID = SCOPE_IDENTITY()
END
GO

