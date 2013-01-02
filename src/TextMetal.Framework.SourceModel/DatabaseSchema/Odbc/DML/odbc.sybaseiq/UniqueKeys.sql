/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique keys[schema, table]
select
	sys_u_us.user_name as SchemaName,
	sys_t_us.table_name as TableName,
	sys_ix_us.index_name as UniqueKeyName,
	case
		when sys_ix_us.not_enforced = 'Y' then 1
		when sys_ix_us.not_enforced = 'N' then 0
		else null
	end as UniqueKeyIsDisabled
from
	sys.sysidx sys_ix_us
	inner join sys.systab sys_t_us on sys_t_us.table_id = sys_ix_us.table_id
	inner join sys.sysuser sys_u_us on sys_u_us.user_id = sys_t_us.creator
where
	sys_u_us.user_name = ?
	and sys_t_us.table_name = ?
	and sys_ix_us.index_category = 3
	and (sys_ix_us."unique" = 1 or sys_ix_us."unique" = 2)
order by
	sys_ix_us.index_name asc