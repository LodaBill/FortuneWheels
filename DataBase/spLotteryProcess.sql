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
-- Author:                <Author,,Name>
-- Create date: <Create Date,,>
-- Description:        <Description,,>
-- =============================================
IF OBJECT_ID('spLotteryProcess', 'P') IS NOT NULL
DROP PROCEDURE spLotteryProcess
GO
CREATE PROCEDURE spLotteryProcess
        -- Add the parameters for the stored procedure here
        @CellPhoneNo        nvarchar(50)='',
        @AwardID                int
AS
BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        SET NOCOUNT ON;

        DECLARE @CardCode nvarchar(255)

    -- Check if the phoneNo has the LotteryCount
    IF EXISTS (SELECT * FROM [dbo].[Players] WHERE CellPhoneNo = @CellPhoneNo) 
    AND (SELECT [LotteryCount] FROM [dbo].[Players] WHERE CellPhoneNo = @CellPhoneNo) > 0
        BEGIN
                --Check if the CardCode is exist
                IF (SELECT [SurplusCount] FROM [dbo].[Awards] WHERE [AwardID] = @AwardID) > 0
                BEGIN
                        --Get CardCode
                        	
                        SELECT TOP 1 @CardCode = [CardCode] FROM [dbo].[CardCodes] WHERE [AwardID] = @AwardID AND [Used] = 0 ORDER BY [ID] ASC
                        
                        IF (@CardCode = '' or @CardCode is null) and @AwardID <> 11 
						BEGIN
							SET @AwardID = 12
                        END
                        ELSE
                        BEGIN
							--Insert LotteryHistory
							INSERT INTO [dbo].[LotteryHistory]
							   ([CellPhoneNo]
							   ,[LotteryTime]
							   ,[AwardID]
							   ,[CardCode])
							VALUES
							   (@CellPhoneNo
							   ,GETDATE()
							   ,@AwardID
							   ,@CardCode)
	                        
							--Update the CardCode to used
							UPDATE        [dbo].[CardCodes]
							SET                [Used] = 1
							WHERE        [AwardID] = @AwardID 
							AND                [CardCode] = @CardCode
	                        
							--Update SurplusCount in Awards
							UPDATE        [dbo].[Awards]
							SET                [SurplusCount] = [SurplusCount] - 1
							WHERE        [AwardID] = @AwardID
						END
                        
                END
                ELSE
                BEGIN
					SET @AwardID = 12
                END
 
        --Reduce LotteryCount
        UPDATE        [dbo].[Players]
                SET                [LotteryCount] = [LotteryCount] - 1 
                WHERE        [CellPhoneNo] = @CellPhoneNo
                
        END 
        select @AwardID,@CardCode
END
GO
