/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- schemas
select distinct
u.name as SchemaName
from
dbo.sysusers u
inner join dbo.sysobjects o on o.uid = u.uid
where o.type = 'U'
order by u.name asc