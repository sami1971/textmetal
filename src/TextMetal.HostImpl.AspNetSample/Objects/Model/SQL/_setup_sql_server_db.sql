/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

PRINT 'TextMetalWebHostSample database setup...'	

/*
	========================================================================================================
	#IF !SQL_SERVER_COMPACT
	========================================================================================================
*/

use master
GO

if exists (select * from sysdatabases where name = 'TextMetalWebHostSample')
BEGIN

	PRINT 'Database [TextMetalWebHostSample] exists...'	
	
	PRINT 'Setting single user access on database [TextMetalWebHostSample]...'

	alter database [TextMetalWebHostSample] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	
	
	PRINT 'Dropping database [TextMetalWebHostSample]...'
	
	drop database [TextMetalWebHostSample]
	
END
GO

if exists (SELECT * FROM master.dbo.syslogins WHERE loginname = N'TextMetalWebHostSampleLogin')
BEGIN

	PRINT 'Login [TextMetalWebHostSampleLogin] exists...'	
	
	PRINT 'Dropping login [TextMetalWebHostSampleLogin]...'
	
	drop login [TextMetalWebHostSampleLogin]
	
END
GO

PRINT 'Creating database [TextMetalWebHostSample]...'
GO

CREATE DATABASE [TextMetalWebHostSample]
GO

PRINT N'Creating login [TextMetalWebHostSampleLogin]...'
GO
	
CREATE LOGIN [TextMetalWebHostSampleLogin] WITH PASSWORD = 'LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF, DEFAULT_LANGUAGE=[us_english]
GO

PRINT N'Granting connect to login [TextMetalWebHostSampleLogin]...'
GO
	
GRANT CONNECT SQL TO [TextMetalWebHostSampleLogin]

USE [TextMetalWebHostSample]
GO

PRINT N'Creating user in database...'
GO

CREATE USER [TextMetalWebHostSampleUser] FOR LOGIN [TextMetalWebHostSampleLogin] WITH DEFAULT_SCHEMA=[dbo]
GO

EXEC sp_addrolemember 'db_owner', 'TextMetalWebHostSampleUser'
GO


/*
	========================================================================================================
		#ENDIF
	========================================================================================================
*/



CREATE TABLE [EventLog]
(
	[EventLogId] [int] IDENTITY(1,1) NOT NULL,
	[EventText] [ntext] NOT NULL,	
	[CreationTimestamp] [datetime] NOT NULL,
	[ModificationTimestamp] [datetime] NOT NULL,
	[LogicalDelete] [bit] NOT NULL DEFAULT(0),
	
	CONSTRAINT [pk_EventLog] PRIMARY KEY
	(
		[EventLogId]
	)	
)
GO


CREATE TABLE [EmailMessage]
(
	[EmailMessageId] [int] IDENTITY(1,1) NOT NULL,
	[From] [nvarchar](2047) NOT NULL,
	[Sender] [nvarchar](2047) NULL,
	[ReplyTo] [nvarchar](2047) NULL,
	[To] [nvarchar](2047) NOT NULL,
	[CC] [nvarchar](2047) NULL,
	[BCC] [nvarchar](2047) NULL,
	[Subject] [nvarchar](2047) NOT NULL,	
	[IsBodyHtml] [bit] NOT NULL DEFAULT(0),
	[Body] [ntext] NOT NULL,
	[Processed] [bit] NOT NULL,
	[CreationTimestamp] [datetime] NOT NULL,
	[ModificationTimestamp] [datetime] NOT NULL,
	[LogicalDelete] [bit] NOT NULL DEFAULT(0),

	CONSTRAINT [pk_EmailMessage] PRIMARY KEY
	(
		[EmailMessageId]
	)
)
GO


CREATE TABLE [EmailAttachment]
(
	[EmailAttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[EmailMessageId] [int] NOT NULL,
	[MimeType] [nvarchar](2047) NOT NULL,
	[AttachmentBits] [image] NOT NULL,	
	[CreationTimestamp] [datetime] NOT NULL,
	[ModificationTimestamp] [datetime] NOT NULL,
	[LogicalDelete] [bit] NOT NULL DEFAULT(0),

	CONSTRAINT [pk_EmailAttachment] PRIMARY KEY
	(
		[EmailAttachmentId]
	),

	CONSTRAINT [fk_EmailAttachment_EmailMessage] FOREIGN KEY
	(
		[EmailMessageId]
	)
	REFERENCES [EmailMessage]
	(
		[EmailMessageId]
	)
)
GO


CREATE TABLE [TableWithPrimaryKeyAsIdentity]
(
	[PkId] [int] IDENTITY(1,1) NOT NULL,
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL,
	
	CONSTRAINT [pk_TableWithPrimaryKeyAsIdentity] PRIMARY KEY
	(
		[PkId]
	)	
)
GO


CREATE TABLE [TableWithPrimaryKeyAsDefault]
(
	[PkDf] [UNIQUEIDENTIFIER] NOT NULL DEFAULT(newsequentialid()),
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL,
	
	CONSTRAINT [pk_TableWithPrimaryKeyAsDefault] PRIMARY KEY
	(
		[PkDf]
	)	
)
GO


CREATE TABLE [TableWithPrimaryKeyWithDiffIdentity]
(
	[Pk] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL,
	
	CONSTRAINT [pk_TableWithPrimaryKeyWithDiffIdentity] PRIMARY KEY
	(
		[Pk]
	)	
)
GO


CREATE TABLE [TableNoPrimaryKeyWithIdentity]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL
)
GO


CREATE TABLE [TableWithPrimaryKeyNoIdentity]
(
	[Pk] [int] NOT NULL,
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL,
	
	CONSTRAINT [pk_TableWithPrimaryKeyNoIdentity] PRIMARY KEY
	(
		[Pk]
	)	
)
GO


CREATE TABLE [TableWithCompositePrimaryKeyNoIdentity]
(
	[Pk0] [int] NOT NULL,
	[Pk1] [int] NOT NULL,
	[Pk2] [int] NOT NULL,
	[Pk3] [int] NOT NULL,
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL,
	
	CONSTRAINT [pk_TableWithCompositePrimaryKeyNoIdentity] PRIMARY KEY
	(
		[Pk0],[Pk1],[Pk2],[Pk3]
	)	
)
GO


CREATE TABLE [TableNoPrimaryKeyNoIdentity]
(
	[Value] [int] NOT NULL,
	[Data01] [BIT] NULL,
	[Data02] [DATETIME] NULL,
	[Data03] [INT] NULL,
	[Data04] [NVARCHAR](100) NULL
)
GO


CREATE TABLE [TableTypeTest]
(
	[PkId] [int] IDENTITY(1,1) NOT NULL,
	[Data00] [BIGINT] NULL,
	[Data01] [BINARY] NULL,
	[Data02] [BIT] NULL,
	[Data03] [CHAR] NULL,
	--[Data04] [CURSOR] NULL,
	[Data05] [DATE] NULL,
	[Data06] [DATETIME] NULL,
	[Data07] [DATETIME2] NULL,
	[Data08] [DATETIMEOFFSET] NULL,
	[Data09] [DECIMAL] NULL,
	[Data10] [FLOAT] NULL,
	--[Data11] [HIERARCHYID] NULL,
	[Data12] [IMAGE] NULL,
	[Data13] [INT] NULL,
	[Data14] [MONEY] NULL,
	[Data15] [NCHAR] NULL,
	[Data16] [NTEXT] NULL,
	[Data17] [NUMERIC] NULL,
	[Data18] [NVARCHAR] NULL,
	[Data19] [REAL] NULL,
	[Data20] [SMALLDATETIME] NULL,
	[Data21] [SMALLINT] NULL,
	[Data22] [SMALLMONEY] NULL,
	--[Data23] [SQL_VARIANT] NULL,
	--[Data24] [SYSNAME] NULL,
	--[Data25] [TABLE] NULL,
	[Data26] [TEXT] NULL,
	[Data27] [TIME] NULL,
	--[Data28] [TIMESTAMP] NULL,
	[Data29] [TINYINT] NULL,
	[Data30] [UNIQUEIDENTIFIER] NULL,
	[Data31] [VARBINARY] NULL,
	[Data32] [VARCHAR] NULL,
	--[Data33] [XML] NULL,
	
	CONSTRAINT [pk_TableTypeTest] PRIMARY KEY
	(
		[PkId]
	)	
)
GO


CREATE VIEW [dbo].[EventLogAggregation] AS
	select
	min([CreationTimestamp]) as [MinCreationTimestamp],
	max([ModificationTimestamp]) as [MaxModificationTimestamp]
	from [dbo].[EventLog]
GO


CREATE PROCEDURE [dbo].[GetBlahBlahBlah]
(
	@pInt as int,
	@pImage as image,
	@pVarchar varchar(100) output
)
AS
BEGIN
	set @pVarchar = 'TEXTMETAL'

	SELECT 1 as a, 2 as b, @pInt as c, @pImage as d
	union all
	SELECT 1 as a, 2 as b, @pInt as c, @pImage as d 

	return 16
END
GO




/*
	========================================================================================================
	#IF !SQL_SERVER_COMPACT
	========================================================================================================
*/


PRINT N'Altering login [TextMetalWebHostSampleLogin]...'
GO	
	
ALTER LOGIN [TextMetalWebHostSampleLogin] WITH DEFAULT_DATABASE=[TextMetalWebHostSample]
GO


/*
	========================================================================================================
	#ENDIF
	========================================================================================================
*/

PRINT 'TextMetalWebHostSample database complete.'	
GO
