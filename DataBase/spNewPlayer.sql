-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spNewPlayer
	-- Add the parameters for the stored procedure here
	@CellPhoneNo	nvarchar(50)='' 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    IF NOT EXISTS (SELECT * FROM dbo.Players WHERE CellPhoneNo = @CellPhoneNo)
	BEGIN
		INSERT INTO [dbo].[Players]
           ([CellPhoneNo]
           ,[LotteryCount]
           ,[LastLoginTime])
		VALUES
           (@CellPhoneNo
           ,3
           ,GETDATE())
	END 
	
END
GO
