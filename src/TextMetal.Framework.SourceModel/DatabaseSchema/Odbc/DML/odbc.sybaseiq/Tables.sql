/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- tables[schema]
select
sys_t.table_name as TableName,
case
    when sys_t.table_type = 'VIEW' then 1
    when sys_t.table_type = 'BASE' then 0
    else null
end as IsView
from
sys.systable sys_t
inner join dbo.sysusers sys_u on sys_u.uid = sys_t.creator
where
sys_u.name = ?
order by sys_t.table_name asc
