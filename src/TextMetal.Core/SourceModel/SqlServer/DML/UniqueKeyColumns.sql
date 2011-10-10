/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique key columns (refs)[schema, unique key]
select	
	sys_ic.key_ordinal as UniqueKeyColumnOrdinal,
	sys_ic.column_id as UniqueKeyParentColumnOrdinal,
	sys_ic.is_descending_key as UniqueKeyColumnDescendingSort
from
	sys.index_columns as sys_ic -- unique key contraint columns	
	inner join sys.key_constraints as sys_kc on sys_kc.unique_index_id = sys_ic.index_id		 
	inner join sys.tables as sys_t on sys_t.object_id = sys_kc.parent_object_id and	sys_t.object_id = sys_ic.object_id
	inner join sys.schemas as sys_s on sys_s.schema_id = sys_t.schema_id
where	
	(sys_s.name = @SchemaName) and
	(sys_t.name = @TableName) and
	(sys_kc.name = @UniqueKeyName)
order by
	sys_ic.key_ordinal asc -- unique key contraint column ordinal