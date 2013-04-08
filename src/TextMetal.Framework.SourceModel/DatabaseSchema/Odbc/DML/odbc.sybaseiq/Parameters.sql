/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- parameters[schema, procedure]
select
	sys_u.user_name as SchemaName,
	sys_p.proc_name as ProcedureName,
	'@' + sys_pp.parm_name as ParameterName,
	sys_pp.parm_id as ParameterOrdinal,
	sys_pp.width as ParameterSize,
	null as ParameterPrecision,
	sys_pp.scale as ParameterScale,		
	sys_d.domain_name as ParameterSqlType,	
	case
		when sys_pp.parm_mode_out = 'Y' then 1
		else 0
	end as ParameterIsOutput,
	case
		when sys_pp.parm_mode_in = 'N' then 1
		else 0
	end as ParameterIsReadOnly,
	null as ParameterIsCursorRef,
	case	
		when sys_pp.parm_type = 4 then 1
		else 0
	end as ParameterIsReturnValue,
	sys_pp."default" as ParameterDefaultValue,
	case	
		when sys_pp.parm_type = 1 then 1
		else 0
	end as ParameterIsResultColumn
from
	sys.sysprocparm sys_pp
	inner join sys.sysdomain sys_d on sys_d.domain_id = sys_pp.domain_id
	inner join sys.sysprocedure sys_p on sys_p.proc_id = sys_pp.proc_id
	inner join sys.sysuser sys_u on sys_u.user_id = sys_p.creator
where
	sys_u.user_name = ?
	and sys_p.proc_name = ?
	and sys_pp.parm_type in (0, 1, 4)
order by
	sys_pp.parm_id asc