/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- tables[schema]
select
t.table_name as TableName,
case
    when t.table_type = 'VIEW' then 1
    when t.table_type = 'BASE' then 0
    else null
end as IsView
from
sys.systable t
inner join dbo.sysusers u on u.uid = t.creator
where
u.name = ?
order by t.table_name asc
