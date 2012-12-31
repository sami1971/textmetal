/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- procedures[schema]
select	
	sys_s.name as SchemaName,
	sys_p.name as ProcedureName
from
    sys.procedures sys_p
	inner join sys.schemas sys_s on sys_s.schema_id = sys_p.schema_id
where
	sys_s.name = @SchemaName
order by
	sys_p.name asc