/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- database (not catalog, actual dataabse)
select
	connection_property('ServerNodeAddress') as MachineName,
	null as InstanceName,
	@@version as ServerVersion,
	@@version as ServerLevel,
	@@version as ServerEdition,
	connection_property('DBNumber') as InitialCatalogName