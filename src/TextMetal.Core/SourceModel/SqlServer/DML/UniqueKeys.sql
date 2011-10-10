/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique keys[schema, table]
select
	sys_kc.name as UniqueKeyName
from
	sys.key_constraints as sys_kc -- unique key constraints
	inner join sys.tables as sys_t on sys_t.object_id = sys_kc.parent_object_id
	inner join sys.schemas as sys_s on sys_s.schema_id = sys_t.schema_id
where
	sys_kc.type = 'UQ' and -- is unique key
	sys_s.name = @SchemaName and
	sys_t.name = @TableName
order by
	sys_kc.name asc