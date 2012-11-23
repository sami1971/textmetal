/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique key columns (refs)[schema, unique key]
select
sys_ixc.sequence as UniqueKeyColumnOrdinal,		
sys_ixc.column_id as UniqueKeyParentColumnOrdinal,	
case
    when sys_ixc."order" = 'D' then 1
    when sys_ixc."order" = 'A' then 0
    else null
end as UniqueKeyColumnDescendingSort
from
sys.sysidx sys_ix
inner join sys.systable sys_t_uk on sys_t_uk.table_id = sys_ix.table_id
inner join dbo.sysusers sys_u_uk on sys_u_uk.uid = sys_t_uk.creator
inner join sys.sysidxcol sys_ixc on sys_ixc.index_id = sys_ix.index_id and sys_ixc.table_id = sys_ix.table_id
where
sys_u_uk.name = ?
and sys_t_uk.table_name = ?
and sys_ix.index_category = 3
and sys_ix.index_name = ?
and sys_ix."unique" = 2