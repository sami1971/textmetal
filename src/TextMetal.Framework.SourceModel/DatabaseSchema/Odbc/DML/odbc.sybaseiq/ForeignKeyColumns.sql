/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign key columns (refs)[schema, foreign key]
select
	sys_u_fs.user_name as SchemaName,
	sys_t_fs.table_name as TableName,
	sys_ix_fs.index_name as ForeignKeyName,
	sys_ixc_fs.sequence as ForeignKeyOrdinal,
	sys_ixc_fs.column_id as ColumnOrdinal,
	sys_c_fs.column_name as ColumnName,
	sys_u_ps.user_name as PrimarySchemaName,
	sys_t_ps.table_name as PrimaryTableName,
	sys_ix_ps.index_name as PrimaryKeyName,
	sys_ixc_ps.sequence as PrimaryKeyOrdinal,
	sys_ixc_ps.column_id as PrimaryKeyColumnOrdinal,
	sys_c_ps.column_name as PrimaryKeyColumnName
from
	sys.sysfkey sys_fk
	inner join sys.systab sys_t_fs on sys_t_fs.table_id = sys_fk.foreign_table_id
	inner join sys.sysidx sys_ix_fs on sys_ix_fs.table_id = sys_fk.foreign_table_id and sys_ix_fs.index_id = sys_fk.foreign_index_id
	inner join sys.sysidxcol sys_ixc_fs on sys_ixc_fs.table_id = sys_ix_fs.table_id and sys_ixc_fs.index_id = sys_ix_fs.index_id
	inner join sys.systabcol sys_c_fs on sys_c_fs.table_id = sys_ixc_fs.table_id and sys_c_fs.column_id = sys_ixc_fs.column_id
	inner join sys.sysuser sys_u_fs on sys_u_fs.user_id = sys_t_fs.creator
	inner join sys.systab sys_t_ps on sys_t_ps.table_id = sys_fk.primary_table_id
	inner join sys.sysidx sys_ix_ps on sys_ix_ps.table_id = sys_fk.primary_table_id and sys_ix_ps.index_id = sys_fk.primary_index_id
	inner join sys.sysidxcol sys_ixc_ps on sys_ixc_ps.table_id = sys_ix_ps.table_id and sys_ixc_ps.index_id = sys_ix_ps.index_id
	inner join sys.systabcol sys_c_ps on sys_c_ps.table_id = sys_ixc_ps.table_id and sys_c_ps.column_id = sys_ixc_ps.column_id
	inner join sys.sysuser sys_u_ps on sys_u_ps.user_id = sys_t_ps.creator
where
	sys_u_fs.user_name = ?
	and sys_t_fs.table_name = ?
	and sys_ix_fs.index_name = ?
	and sys_ix_fs.index_category = 2
order by
	sys_ixc_fs.sequence asc