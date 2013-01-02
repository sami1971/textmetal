/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- procedures[schema]
select
	sys_u.user_name as SchemaName,
	sys_p.proc_name as ProcedureName
from
	sys.sysprocedure sys_p
	inner join sys.sysuser sys_u on sys_u.user_id = sys_p.creator
where
	sys_u.user_name = ?
order by
	sys_p.proc_name asc