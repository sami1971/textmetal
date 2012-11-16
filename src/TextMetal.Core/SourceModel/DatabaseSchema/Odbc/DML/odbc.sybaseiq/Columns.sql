/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- columns[schema, table|view]
select
c.cname as ColumnName,
c.colno as ColumnOrdinal,
case
    when c.nulls = 'Y' then 1
    when c.nulls = 'N' then 0
    else null
end as ColumnNullable,
c.length as ColumnSize,
null as ColumnPrecision,
null as ColumnScale,		
c.coltype as ColumnSqlType,
case
    when c.default_value = 'autoincrement' then 1
    else 0
end as ColumnIsIdentity,
null as ColumnIsComputed,
case
    when c.default_value is not null then 1
    else 0
end as ColumnHasDefault,
null as ColumnHasCheck,
case
    when c.in_primary_key = 'Y' then 1
    when c.in_primary_key = 'N' then 0
    else null
end as ColumnIsPrimaryKey
from
sys.syscolumns c
inner join sys.systable t on t.table_name = c.tname
inner join dbo.sysusers u on u.uid = t.creator
where
u.name = ?
and c.creator = u.name
and t.table_name = ?
order by c.colno asc
