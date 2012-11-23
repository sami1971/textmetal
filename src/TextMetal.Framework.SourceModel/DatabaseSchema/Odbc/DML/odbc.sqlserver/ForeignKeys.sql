/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign keys[schema, table]
select	
	sys_fk.name as ForeignKeyName,	
	sys_fk.is_disabled as ForeignKeyIsDisabled,
	sys_fk.is_not_for_replication as ForeignKeyIsForReplication,
	sys_fk.delete_referential_action as ForeignKeyOnDeleteRefIntAction,
	sys_fk.delete_referential_action_desc as ForeignKeyOnDeleteRefIntActionSqlName,
	sys_fk.update_referential_action as ForeignKeyOnUpdateRefIntAction,
	sys_fk.update_referential_action_desc as ForeignKeyOnUpdateRefIntActionSqlName
from
    sys.foreign_keys sys_fk -- foreign key	
	inner join sys.tables as sys_t on sys_t.object_id = sys_fk.parent_object_id -- owner table
	inner join sys.schemas sys_s ON sys_s.schema_id = sys_fk.schema_id -- owner schema
where
	sys_fk.type in ('F') and -- is foreign key
	(sys_s.name = ?) and
	(sys_t.name = ?)
order by
	sys_fk.name asc