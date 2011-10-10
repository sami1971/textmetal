/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
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





CREATE PROCEDURE [dbo].[BLAH_BLAH_BLAHS]
(
	@i as int,
	@o as image,
	@j varchar(100) output
)
AS
BEGIN
	set @j = 'ZXC'

	SELECT 1 as a, 2 as b, @i as III, @o as OOO	union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO union all
	SELECT 1 as a, 2 as b, @i as III, @o as OOO 

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
