/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- schemas
select distinct
dbo_u.name as SchemaName
from
dbo.sysusers dbo_u
inner join dbo.sysobjects dbo_o on dbo_o.uid = dbo_u.uid
where dbo_o.type = 'U'
order by dbo_u.name asc