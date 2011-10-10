/*
	Copyright ©2002-2011 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- tables[schema]
select	
	sys_t.name as TableName,
	cast(0 as bit) as IsView
from
    sys.tables sys_t -- tables
	inner join sys.schemas sys_s ON sys_s.schema_id = sys_t.schema_id -- owner schema
where
	sys_s.name = @SchemaName

union all

-- views[schema]
select	
    sys_v.name as TableName,
	cast(1 as bit) as IsView
from
    sys.views sys_v -- views
	inner join sys.schemas sys_s ON sys_s.schema_id = sys_v.schema_id -- owner schema
where
	sys_s.name = @SchemaName
