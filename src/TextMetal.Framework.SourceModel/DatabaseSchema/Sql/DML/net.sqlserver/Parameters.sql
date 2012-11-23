/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- parameters[schema, procedure]
select	
	sys_p.name as ParameterName,
	sys_p.parameter_id as ParameterOrdinal,
	sys_p.max_length as ParameterSize,
	sys_p.precision as ParameterPrecision,
	sys_p.scale as ParameterScale,		
	sys_t.name as ParameterSqlType,	
	sys_p.is_output as ParameterIsOutput,
	cast(0 as bit) as ParameterIsReadOnly,
	--sys_p.is_readonly as ParameterIsReadOnly, -- SQL 2008+
	sys_p.is_cursor_ref as ParameterIsCursorRef,
	cast(0 as bit) as ParameterIsReturnValue,
	null as ParameterDefaultValue,
	cast(0 as bit) as ParameterIsResultColumn
from
    sys.parameters sys_p -- parameters
	inner join sys.objects sys_o on sys_o.object_id = sys_p.object_id -- owner procedure
	inner join sys.schemas sys_s ON sys_s.schema_id = sys_o.schema_id -- owner schema
	inner join sys.types sys_t on sys_t.user_type_id = sys_p.user_type_id -- parameter type
where
	sys_o.type in ('P') and -- owner is procedure
	(sys_s.name = @SchemaName) and
	(sys_o.name = @ProcedureName)
order by	
	sys_p.parameter_id asc -- parameter ordinal