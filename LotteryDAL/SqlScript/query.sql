CREATE DataBase Lottery;
Use Lottery;
GO

CREATE TABLE dbo.NumberType(
	NumberTypeId smallint Primary key identity(1,1),
	Label nvarchar(50) not null,
	DateCreated DateTime null default GetDate()
)

GO
INSERT INTO NumberType (Label) VALUES ('6/45');
INSERT INTO NumberType (Label) VALUES ('4D Max');

GO
CREATE TABLE dbo.NumberWinLevel(
	NumberWinLevelId smallint Primary key identity(1,1),	
	Label nvarchar(50) not null,
	DateCreated DateTime null default GetDate()
)
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('DacBiet')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiNhat')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiNhi')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiBa')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiTu')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiNam')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiSau')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiBay')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiKhuyenKhich1')
INSERT INTO dbo.NumberWinLevel (Label) VALUES ('GiaiKhuyenKhich2')

GO
CREATE TABLE [dbo].[Number](
	[NumberId] [bigint] IDENTITY(1,1) NOT NULL,
	[NumberTypeId] [smallint] NOT NULL FOREIGN KEY REFERENCES dbo.NumberType(NumberTypeId),
	[NumberWinLevelId] smallint NOT NULL FOREIGN KEY REFERENCES dbo.NumberWinLevel(NumberWinLevelId),
	[LotNumber] [int] NOT NULL,
	[DatePublish] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL default getDate(),
	[DateUpdated] [datetime] NULL,
CONSTRAINT [PK_Number] PRIMARY KEY CLUSTERED 
(	
	[NumberTypeId] ASC,
	[NumberWinLevelId] ASC,
	[LotNumber] ASC,
	[DatePublish] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
