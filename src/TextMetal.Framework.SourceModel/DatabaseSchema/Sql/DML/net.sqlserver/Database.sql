/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- database (not catalog, actual dataabse)
select
	SERVERPROPERTY('MachineName') as MachineName,
	SERVERPROPERTY('InstanceName') as InstanceName,
	SERVERPROPERTY('productversion') as ServerVersion,
	SERVERPROPERTY ('productlevel') as ServerLevel,
	SERVERPROPERTY ('edition') as ServerEdition,
	db_Name() as InitialCatalogName