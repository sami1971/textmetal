/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- columns[schema, table|view]
select
	sys_u.user_name as SchemaName,
	sys_t.table_name as TableName,
	sys_c.column_name as ColumnName,
	sys_c.column_id as ColumnOrdinal,
	case
		when sys_c.nulls = 'Y' then 1
		when sys_c.nulls = 'N' then 0
		else null
	end as ColumnNullable,
	sys_c.width as ColumnSize,
	null as ColumnPrecision,
	sys_c.scale as ColumnScale,		
	sys_d.domain_name as ColumnSqlType,
	case
		when sys_c."default" = 'autoincrement' then 1
		else 0
	end as ColumnIsIdentity,
	case
		when sys_c.column_type = 'C' then 1
		when sys_c.column_type = 'R' then 0
		else null
	end as ColumnIsComputed,
	case
		when sys_c."default" is not null then 1
		else 0
	end as ColumnHasDefault,
	null as ColumnHasCheck,
	case
		when sys_ixc_ps.sequence is not null then 1	
		else 0
	end as ColumnIsPrimaryKey,
	case
		when sys_ixc_ps.sequence is not null then sys_ix_ps.index_name	 
		else null
	end as PrimaryKeyName,
	sys_ixc_ps.sequence as PrimaryKeyColumnOrdinal
from
	sys.systabcol sys_c
	inner join sys.sysdomain sys_d on sys_d.domain_id = sys_c.domain_id
	inner join sys.systab sys_t on sys_t.table_id = sys_c.table_id
	left outer join sys.sysidx sys_ix_ps on sys_ix_ps.table_id = sys_t.table_id
	left outer join sys.sysidxcol sys_ixc_ps on sys_ixc_ps.table_id = sys_ix_ps.table_id and sys_ixc_ps.index_id = sys_ix_ps.index_id and sys_ixc_ps.column_id = sys_c.column_id
	inner join sys.sysuser sys_u on sys_u.user_id = sys_t.creator
where
	sys_u.user_name = ?
	and sys_t.table_name = ?
	and sys_ix_ps.index_category = 1
order by
	sys_c.column_id asc
