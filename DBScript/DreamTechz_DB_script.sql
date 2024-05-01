USE [DreamTechz]
GO
/****** Object:  Table [dbo].[Tbl_City]    Script Date: 01-05-2024 19:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_City](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[City] [varchar](50) NULL,
 CONSTRAINT [PK_Tbl_City] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Student]    Script Date: 01-05-2024 19:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Student](
	[S_id] [int] IDENTITY(1,1) NOT NULL,
	[S_Name] [varchar](50) NOT NULL,
	[S_Age] [int] NULL,
	[S_Address] [int] NULL,
	[Subject_opted] [int] NOT NULL,
 CONSTRAINT [PK_TBL_Student] PRIMARY KEY CLUSTERED 
(
	[S_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Subject]    Script Date: 01-05-2024 19:28:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Subject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](50) NULL,
 CONSTRAINT [PK_Tbl_Subject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_City] ON 

INSERT [dbo].[Tbl_City] ([id], [City]) VALUES (1, N'Noida')
INSERT [dbo].[Tbl_City] ([id], [City]) VALUES (2, N'Panipat')
INSERT [dbo].[Tbl_City] ([id], [City]) VALUES (3, N'Jammu')
SET IDENTITY_INSERT [dbo].[Tbl_City] OFF
GO
SET IDENTITY_INSERT [dbo].[TBL_Student] ON 

INSERT [dbo].[TBL_Student] ([S_id], [S_Name], [S_Age], [S_Address], [Subject_opted]) VALUES (1, N'Adam', 16, 1, 1)
INSERT [dbo].[TBL_Student] ([S_id], [S_Name], [S_Age], [S_Address], [Subject_opted]) VALUES (3, N'Alex', 18, 2, 2)
INSERT [dbo].[TBL_Student] ([S_id], [S_Name], [S_Age], [S_Address], [Subject_opted]) VALUES (4, N'Stuart', 15, 3, 2)
INSERT [dbo].[TBL_Student] ([S_id], [S_Name], [S_Age], [S_Address], [Subject_opted]) VALUES (5, N'Maac', 16, 1, 3)
SET IDENTITY_INSERT [dbo].[TBL_Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Tbl_Subject] ON 

INSERT [dbo].[Tbl_Subject] ([id], [Subject]) VALUES (1, N'Bio')
INSERT [dbo].[Tbl_Subject] ([id], [Subject]) VALUES (2, N'Math')
INSERT [dbo].[Tbl_Subject] ([id], [Subject]) VALUES (3, N'Physics')
SET IDENTITY_INSERT [dbo].[Tbl_Subject] OFF
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertOrUpdateStudent]    Script Date: 01-05-2024 19:28:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_InsertOrUpdateStudent]
(
	@S_ID INT,
	@S_name VARCHAR(50),
	@S_Age INT,
	@S_Address INT,
	@S_Subject INT,
	@opsection VARCHAR(50),
	@ERRORCODE INT OUT
)
AS
BEGIN TRY
	BEGIN TRAN
	IF @opsection = 'INSERT'
	BEGIN
		INSERT INTO [TBL_Student]([S_Name],[S_Age],[S_Address],[Subject_opted])
		VALUES(@S_name, @S_Age, @S_Address, @S_Subject)
	END
	ELSE
	BEGIN
		UPDATE [TBL_Student] SET
		[S_Name] = @S_name,
		[S_Age] = @S_Age,
		[S_Address] = @S_Address,
		[Subject_opted] = @S_Subject
		WHERE S_id = @S_ID
	END
	COMMIT TRAN
	SET @ERRORCODE = 0
END TRY
BEGIN CATCH
	SET @ERRORCODE = @@ERROR
	ROLLBACK TRAN
END CATCH
GO
