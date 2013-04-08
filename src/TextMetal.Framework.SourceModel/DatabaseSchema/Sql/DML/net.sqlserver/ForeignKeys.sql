/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign keys[schema, table]
select	
	sys_s_fs.name as SchemaName,
	sys_t_fs.name as TableName,
	sys_fk.name as ForeignKeyName,	
	sys_fk.is_disabled as ForeignKeyIsDisabled,
	sys_fk.is_not_for_replication as ForeignKeyIsForReplication,
	sys_fk.delete_referential_action as ForeignKeyOnDeleteRefIntAction,
	sys_fk.delete_referential_action_desc as ForeignKeyOnDeleteRefIntActionSqlName,
	sys_fk.update_referential_action as ForeignKeyOnUpdateRefIntAction,
	sys_fk.update_referential_action_desc as ForeignKeyOnUpdateRefIntActionSqlName
from
    sys.foreign_keys sys_fk
	inner join sys.tables sys_t_fs on sys_t_fs.object_id = sys_fk.parent_object_id
	inner join sys.schemas sys_s_fs on sys_s_fs.schema_id = sys_fk.schema_id
where
	sys_fk.type in ('F')
	and sys_s_fs.name = @SchemaName
	and sys_t_fs.name = @TableName
order by
	sys_fk.name asc