/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign key columns (refs)[schema, foreign key]
select
sys_ixc.sequence as ForeignKeyColumnOrdinal,		
sys_ixc.column_id as ForeignKeyParentColumnOrdinal,	
sys_t_pk.table_name as ForeignKeyChildTableName,
sys_ixc.primary_column_id as ForeignKeyChildColumnOrdinal
from
sys.sysfkey sys_fk
inner join sys.sysidx sys_ix on sys_ix.index_id = sys_fk.foreign_index_id and sys_ix.table_id = sys_fk.foreign_table_id
inner join sys.systable sys_t_fk on sys_t_fk.table_id = sys_fk.foreign_table_id
inner join sys.systable sys_t_pk on sys_t_pk.table_id = sys_fk.primary_table_id
inner join dbo.sysusers sys_u_fk on sys_u_fk.uid = sys_t_fk.creator
inner join dbo.sysusers sys_u_pk on sys_u_pk.uid = sys_t_pk.creator
inner join sys.sysidxcol sys_ixc on sys_ixc.index_id = sys_fk.foreign_index_id and sys_ixc.table_id = sys_fk.foreign_table_id
where
sys_u_fk.name = ?
and sys_t_fk.table_name = ?
and sys_ix.index_category = 2
and sys_ix.index_name = ?