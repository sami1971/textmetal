/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- columns[schema, table|view]
select
	sys_s.name as SchemaName,
	sys_o.name as TableName,
	sys_c.name as ColumnName,
	sys_c.column_id as ColumnOrdinal,
	sys_c.is_nullable as ColumnNullable,
	sys_c.max_length as ColumnSize,
	sys_c.precision as ColumnPrecision,
	sys_c.scale as ColumnScale,		
	sys_t.name as ColumnSqlType,
	sys_c.is_identity as ColumnIsIdentity,
	sys_c.is_computed as ColumnIsComputed,
	cast(case
		when sys_c.default_object_id = 0 then 0
		else 1
	end as bit) as ColumnHasDefault,
	cast(case
		when sys_c.rule_object_id = 0 then 0
		else 1
	end as bit) as ColumnHasCheck,
	
	(select
		cast(case
			when count(sys2_c.name) > 0 then 1
			else 0
		end as bit)
	from
		sys.key_constraints sys2_kc
		inner join sys.tables sys2_t on sys2_t.object_id = sys2_kc.parent_object_id
		inner join sys.schemas sys2_s on sys2_s.schema_id = sys2_t.schema_id
		inner join sys.index_columns sys2_ic on sys2_ic.object_id = sys2_t.object_id and sys2_ic.index_id = sys2_kc.unique_index_id
		inner join sys.columns sys2_c on sys2_c.object_id = sys2_t.object_id and sys2_c.column_id = sys2_ic.column_id
	where
		sys2_kc.type = 'PK'		
		and sys2_s.name = ?
		and sys2_t.name = ?
		and sys2_c.name = sys_c.name
	) as ColumnIsPrimaryKey,
	
	null as PrimaryKeyName,
	null as PrimaryKeyColumnOrdinal	
from
	sys.columns sys_c
	inner join sys.objects sys_o on sys_o.object_id = sys_c.object_id
	inner join sys.schemas sys_s on sys_s.schema_id = sys_o.schema_id
	inner join sys.types sys_t on sys_t.user_type_id = sys_c.user_type_id
where
	sys_o.type in ('U', 'V')
	and sys_s.name = ?
	and sys_o.name = ?
order by
	sys_c.column_id asc