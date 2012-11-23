/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- procedures[schema]
select
sys_p.proc_name as ProcedureName
from
sys.sysprocedure sys_p
inner join dbo.sysusers sys_u on sys_u.uid = sys_p.creator
where
sys_u.name = ?
order by sys_p.proc_name asc