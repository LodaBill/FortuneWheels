
use [RotaryDrawDB]

PRINT 'Start create table ''Players'''

CREATE TABLE [dbo].[Players](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CellPhoneNo] [int] NOT NULL,
	[LotteryCount] [int] NOT NULL,
	[LastLoginTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


Print 'Start create table ''Awards''' 

CREATE TABLE [dbo].[Awards](
	[AwardID] [int] IDENTITY(1,1) NOT NULL,
	[AwardName] [nvarchar](50) NOT NULL,
	[Angle] [int] NOT NULL,
	[Rate] [float] NOT NULL,
	[TotalCount] [int] NOT NULL,
	[SurplusCount] [int] NOT NULL,
 CONSTRAINT [PK_Awards] PRIMARY KEY CLUSTERED 
(
	[AwardID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


Print 'Start create table ''WinInfo''' 

SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LotteryHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CellPhoneNo] [int] NOT NULL,	
	[LotteryTime] [datetime] NOT NULL,
	[AwardID] [int] NOT NULL,
	[CardCode] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_WinInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
--ALTER TABLE [dbo].[WinInfo]  WITH CHECK ADD  CONSTRAINT [FK_WinInfo_Players] FOREIGN KEY([CellPhoneNo]) REFERENCES [dbo].[Players] ([CellPhoneNo])
--ALTER TABLE [dbo].[WinInfo] CHECK CONSTRAINT [FK_WinInfo_Players]
ALTER TABLE [dbo].[LotteryHistory]  WITH CHECK ADD  CONSTRAINT [FK_WinInfo_Awards] FOREIGN KEY([AwardID]) REFERENCES [dbo].[Awards] ([AwardID])
ALTER TABLE [dbo].[LotteryHistory] CHECK CONSTRAINT [FK_WinInfo_Awards]


Print 'Start create table ''CardCodes''' 

CREATE TABLE [dbo].[CardCodes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CardCode] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_CardCodes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
