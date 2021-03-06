USE [RotaryDrawDB]
GO
SET LANGUAGE 简体中文

DROP TABLE [dbo].[Players]
DROP TABLE [dbo].[LotteryHistory]
DROP TABLE [dbo].[CardCodes]
DROP TABLE [dbo].[Awards]

--  Create Players
PRINT 'Start create table ''Players'''

CREATE TABLE [dbo].[Players](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [CellPhoneNo] [nvarchar](50) NOT NULL,
        [LotteryCount] [int] NOT NULL,
        [LastLoginTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


--  Create Awards
Print 'Start create table ''Awards''' 

CREATE TABLE [dbo].[Awards](
        [AwardID] [int] NOT NULL,
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


--  Create LotteryHistory
Print 'Start create table ''LotteryHistory''' 

SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[LotteryHistory](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [CellPhoneNo] [nvarchar](50) NOT NULL,        
        [LotteryTime] [datetime] NOT NULL,
        [AwardID] [int] NOT NULL,
        [CardCode] [nvarchar](255) ,
 CONSTRAINT [PK_WinInfo] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
--ALTER TABLE [dbo].[WinInfo]  WITH CHECK ADD  CONSTRAINT [FK_WinInfo_Players] FOREIGN KEY([CellPhoneNo]) REFERENCES [dbo].[Players] ([CellPhoneNo])
--ALTER TABLE [dbo].[WinInfo] CHECK CONSTRAINT [FK_WinInfo_Players]
ALTER TABLE [dbo].[LotteryHistory]  WITH CHECK ADD  CONSTRAINT [FK_LotteryHistory_Awards] FOREIGN KEY([AwardID]) REFERENCES [dbo].[Awards] ([AwardID])
ALTER TABLE [dbo].[LotteryHistory] CHECK CONSTRAINT [FK_LotteryHistory_Awards]

--  Create LotteryHistory
Print 'Start create table ''CardCodes''' 

CREATE TABLE [dbo].[CardCodes](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [AwardID] [int] NOT NULL,
        [CardCode] [nvarchar](255) NOT NULL,
        [Used] [bit] NOT NULL,
 CONSTRAINT [PK_CardCodes] PRIMARY KEY CLUSTERED 
(
        [ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [dbo].[CardCodes] ADD  CONSTRAINT [DF_CardCodes_Used]  DEFAULT ((0)) FOR [Used]
ALTER TABLE [dbo].[CardCodes]  WITH CHECK ADD  CONSTRAINT [FK_CardCodes_Awards] FOREIGN KEY([AwardID]) REFERENCES [dbo].[Awards] ([AwardID])
ALTER TABLE [dbo].[CardCodes] CHECK CONSTRAINT [FK_CardCodes_Awards]



Print 'Insert Awards' 
INSERT INTO [dbo].[Awards]([AwardID],[AwardName],[Angle],[Rate],[TotalCount],[SurplusCount])
VALUES(1,'1yuan',30,0.5,1000,1000)
   
INSERT INTO [dbo].[Awards] ([AwardID],[AwardName],[Angle],[Rate],[TotalCount],[SurplusCount])
VALUES(2,'2yuan',60,0.2,1000,1000)

INSERT INTO [dbo].[Awards] ([AwardID],[AwardName],[Angle],[Rate],[TotalCount],[SurplusCount])
VALUES(5,'5yuan',90,0.1,1000,0100)

INSERT INTO [dbo].[Awards] ([AwardID],[AwardName],[Angle],[Rate],[TotalCount],[SurplusCount])
VALUES(10,'10yuan',120,0.1,100,100)

INSERT INTO [dbo].[Awards] ([AwardID],[AwardName],[Angle],[Rate],[TotalCount],[SurplusCount])
VALUES(11,'iPad',150,0,0,0)

INSERT INTO [dbo].[Awards] ([AwardID],[AwardName],[Angle],[Rate],[TotalCount],[SurplusCount])
VALUES(12,'nothing',180,0.1,100,100)

INSERT INTO [dbo].[CardCodes] ([AwardID],[CardCode],[Used])
VALUES(2,'asdfasdf',0)

INSERT INTO [dbo].[CardCodes] ([AwardID],[CardCode],[Used])
VALUES(1,'asdf',0)

INSERT INTO [dbo].[CardCodes] ([AwardID],[CardCode],[Used])
VALUES(5,'asdhsdfhfasdf',0)

INSERT INTO [dbo].[CardCodes] ([AwardID],[CardCode],[Used])
VALUES(1,'asdfasdfaasdf',0)

INSERT INTO [dbo].[CardCodes] ([AwardID],[CardCode],[Used])
VALUES(1,'asdfsssssasdf',0)

INSERT INTO [dbo].[CardCodes] ([AwardID],[CardCode],[Used])
VALUES(1,'asdfffffffasdf',0)

