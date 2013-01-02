/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign key columns (refs)[schema, foreign key]
select
	sys_s_fs.name as SchemaName,
	sys_t_fs.name as TableName,
	sys_fk.name as ForeignKeyName,
	sys_fkc.constraint_column_id as ForeignKeyOrdinal,		
	sys_fkc.parent_column_id as ColumnOrdinal,	
	null as ColumnName,
	null as PrimarySchemaName,
	sys_t_ps.name as PrimaryTableName,
	null as PrimaryKeyName,
	null as PrimaryKeyOrdinal,
	sys_fkc.referenced_column_id as PrimaryKeyColumnOrdinal,
	null as PrimaryKeyColumnName
from
	sys.foreign_key_columns sys_fkc
	inner join sys.foreign_keys sys_fk on sys_fk.object_id = sys_fkc.constraint_object_id
	inner join sys.schemas sys_s_fs on sys_s_fs.schema_id = sys_fk.schema_id
	inner join sys.tables sys_t_fs on sys_t_fs.object_id = sys_fkc.parent_object_id
	inner join sys.tables sys_t_ps on sys_t_ps.object_id = sys_fkc.referenced_object_id
where
	sys_fk.type in ('F')
	and sys_s_fs.name = @SchemaName
	and sys_t_fs.name = @TableName
	and sys_fk.name = @ForeignKeyName
order by
	sys_fkc.constraint_column_id asc