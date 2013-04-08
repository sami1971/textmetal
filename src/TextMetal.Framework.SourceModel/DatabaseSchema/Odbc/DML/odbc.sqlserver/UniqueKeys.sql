/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique keys[schema, table]
select
	sys_s_us.name as SchemaName,	
	sys_t_us.name as TableName,
	sys_kc.name as UniqueKeyName,
	null as UniqueKeyIsDisabled
from
	sys.key_constraints as sys_kc
	inner join sys.tables as sys_t_us on sys_t_us.object_id = sys_kc.parent_object_id
	inner join sys.schemas as sys_s_us on sys_s_us.schema_id = sys_t_us.schema_id
where
	sys_kc.type = 'UQ'
	and sys_s_us.name = ?
	and sys_t_us.name = ?
order by
	sys_kc.name asc