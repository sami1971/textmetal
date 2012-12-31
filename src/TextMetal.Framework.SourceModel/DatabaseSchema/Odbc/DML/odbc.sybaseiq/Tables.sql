/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- tables[schema]
select
	sys_u.user_name as SchemaName,
	sys_t.table_name as TableName,
	case
		when sys_t.table_type = 21 then 1
		when sys_t.table_type = 2 then 1
		when sys_t.table_type = 1 then 0
		else null
	end as IsView
from
	sys.systab sys_t
	inner join sys.sysuser sys_u on sys_u.user_id = sys_t.creator
where
	sys_u.user_name =?
order by
	sys_t.table_name asc
