USE [APIInvoices]
GO
/****** Object:  Table [dbo].[CreditNote]    Script Date: 2/07/2021 9:59:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditNote](
	[DbId] [bigint] IDENTITY(1,1) NOT NULL,
	[creditNumber] [varchar](50) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [decimal](18, 4) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_CreditNote] PRIMARY KEY CLUSTERED 
(
	[DbId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2/07/2021 9:59:36 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[DbId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [varchar](255) NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Value] [decimal](18, 4) NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[DbId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix_CreditNumber]    Script Date: 2/07/2021 9:59:36 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [ix_CreditNumber] ON [dbo].[CreditNote]
(
	[creditNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ix_CreditNumberId]    Script Date: 2/07/2021 9:59:36 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [ix_CreditNumberId] ON [dbo].[CreditNote]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ix_InvoiceId]    Script Date: 2/07/2021 9:59:36 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [ix_InvoiceId] ON [dbo].[Invoice]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix_InvoiceNumber]    Script Date: 2/07/2021 9:59:36 am ******/
CREATE UNIQUE NONCLUSTERED INDEX [ix_InvoiceNumber] ON [dbo].[Invoice]
(
	[InvoiceNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
