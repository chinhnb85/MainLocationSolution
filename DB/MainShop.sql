CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Type] [int] NULL,
	[DisplayName] [nvarchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Account_CreatedDate]  DEFAULT (getdate()),
	[Status] [bit] NULL CONSTRAINT [DF_Account_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[AccountDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nchar](10) NULL,
 CONSTRAINT [PK_AccountDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NULL,
	[Lag] [int] NULL,
	[Lng] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
-- =============================================
-- Author:		CHINHNB
-- Create date: 12/07/2016
-- Description:	Get All Account
-- =============================================
Create PROCEDURE [dbo].[Sp_Account_ListAll] 	
AS
BEGIN	
	SET NOCOUNT ON;    
	SELECT * FROM Account
END
GO
-- =============================================
-- Author:		CHINHNB
-- Create date: 12/07/2016
-- Description:	login
-- =============================================
Create PROCEDURE [dbo].[Sp_Account_Login]
 	@userName varchar(50),
 	@password varchar(50)
AS
BEGIN	
	SET NOCOUNT ON;    
	SELECT * FROM Account where (UserName=@userName or Email=@userName) and [Password]=@password
END