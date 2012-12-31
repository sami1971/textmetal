/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- tables[schema]
select
	sys_s.name as SchemaName,
	sys_t.name as TableName,
	cast(0 as bit) as IsView
from
    sys.tables sys_t
	inner join sys.schemas sys_s on sys_s.schema_id = sys_t.schema_id
where
	sys_s.name = @SchemaName

union all

select
	sys_s.name as SchemaName,
    sys_v.name as TableName,
	cast(1 as bit) as IsView
from
    sys.views sys_v
	inner join sys.schemas sys_s on sys_s.schema_id = sys_v.schema_id
where
	sys_s.name = @SchemaName
