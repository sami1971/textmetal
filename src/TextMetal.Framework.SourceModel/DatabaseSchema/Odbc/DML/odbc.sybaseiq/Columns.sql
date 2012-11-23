/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- columns[schema, table|view]
select
sys_c.cname as ColumnName,
sys_c.colno as ColumnOrdinal,
case
    when sys_c.nulls = 'Y' then 1
    when sys_c.nulls = 'N' then 0
    else null
end as ColumnNullable,
sys_c.length as ColumnSize,
null as ColumnPrecision,
null as ColumnScale,		
sys_c.coltype as ColumnSqlType,
case
    when sys_c.default_value = 'autoincrement' then 1
    else 0
end as ColumnIsIdentity,
null as ColumnIsComputed,
case
    when sys_c.default_value is not null then 1
    else 0
end as ColumnHasDefault,
null as ColumnHasCheck,
case
    when sys_c.in_primary_key = 'Y' then 1
    when sys_c.in_primary_key = 'N' then 0
    else null
end as ColumnIsPrimaryKey
from
sys.syscolumns sys_c
inner join sys.systable sys_t on sys_t.table_name = sys_c.tname
inner join dbo.sysusers dbo_u on dbo_u.uid = sys_t.creator
where
dbo_u.name = ?
and sys_c.creator = dbo_u.name
and sys_t.table_name = ?
order by sys_c.colno asc
