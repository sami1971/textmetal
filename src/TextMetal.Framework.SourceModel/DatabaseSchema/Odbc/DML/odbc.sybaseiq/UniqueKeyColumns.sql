/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique key columns (refs)[schema, unique key]
select
	sys_u_us.user_name as SchemaName,
	sys_t_us.table_name as TableName,
	sys_ix_us.index_name as UniqueKeyName,
	sys_ixc_us.sequence as UniqueKeyOrdinal,
	sys_ixc_us.column_id as ColumnOrdinal,	
	sys_c_us.column_name as ColumnName,
	case
		when sys_ixc_us."order" = 'D' then 1
		when sys_ixc_us."order" = 'A' then 0
		else null
	end as UniqueKeyColumnDescendingSort
from
	sys.sysidx sys_ix_us
	inner join sys.sysidxcol sys_ixc_us on sys_ixc_us.table_id = sys_ix_us.table_id and sys_ixc_us.index_id = sys_ix_us.index_id
	inner join sys.systab sys_t_us on sys_t_us.table_id = sys_ix_us.table_id
	inner join sys.systabcol sys_c_us on sys_c_us.table_id = sys_ixc_us.table_id and sys_c_us.column_id = sys_ixc_us.column_id
	inner join sys.sysuser sys_u_us on sys_u_us.user_id = sys_t_us.creator
where
	sys_u_us.user_name = ?
	and sys_t_us.table_name = ?
	and sys_ix_us.index_name = ?
	and sys_ix_us.index_category = 3
	and (sys_ix_us."unique" = 1 or sys_ix_us."unique" = 2)
order by
	sys_ixc_us.sequence asc