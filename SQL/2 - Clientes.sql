USE [GTI]
GO

/****** Object:  Table [dbo].[Clientes]    Script Date: 12/01/2026 15:41:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[CPF] [nvarchar](20) NOT NULL,
	[RG] [nvarchar](20) NULL,
	[DataExpedicao] [date] NULL,
	[OrgaoExpedicao] [nvarchar](20) NULL,
	[UFExpedicao] [nvarchar](2) NULL,
	[DataNascimento] [date] NULL,
	[Sexo] [nvarchar](10) NULL,
	[EstadoCivil] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO