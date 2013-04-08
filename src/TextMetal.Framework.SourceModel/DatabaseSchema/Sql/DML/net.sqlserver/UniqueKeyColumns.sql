/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique key columns (refs)[schema, unique key]
select
	sys_s_us.name as SchemaName,	
	sys_t_us.name as TableName,
	sys_kc.name as UniqueKeyName,	
	sys_ic.key_ordinal as UniqueKeyOrdinal,
	sys_ic.column_id as ColumnOrdinal,	
	null as ColumnName,
	null as UniqueKeyColumnDescendingSort
from
	sys.index_columns as sys_ic
	inner join sys.key_constraints as sys_kc on sys_kc.unique_index_id = sys_ic.index_id		 
	inner join sys.tables as sys_t_us on sys_t_us.object_id = sys_kc.parent_object_id and sys_t_us.object_id = sys_ic.object_id
	inner join sys.schemas as sys_s_us on sys_s_us.schema_id = sys_t_us.schema_id
where
	sys_kc.type = 'UQ'
	and sys_s_us.name = @SchemaName
	and sys_t_us.name = @TableName
	and sys_kc.name = @UniqueKeyName
order by
	sys_ic.key_ordinal asc