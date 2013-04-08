/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign keys[schema, table]
select
	sys_u_fs.user_name as SchemaName,
	sys_t_fs.table_name as TableName,
	sys_ix_fs.index_name as ForeignKeyName,
	case
		when sys_ix_fs.not_enforced = 'Y' then 1
		when sys_ix_fs.not_enforced = 'N' then 0
		else null
	end as ForeignKeyIsDisabled,
	null as ForeignKeyIsForReplication,
	null as ForeignKeyOnDeleteRefIntAction,
	null as ForeignKeyOnDeleteRefIntActionSqlName,
	null as ForeignKeyOnUpdateRefIntAction,
	null as ForeignKeyOnUpdateRefIntActionSqlName
from
	sys.sysfkey sys_fk
	inner join sys.systab sys_t_fs on sys_t_fs.table_id = sys_fk.foreign_table_id
	inner join sys.sysidx sys_ix_fs on sys_ix_fs.table_id = sys_fk.foreign_table_id and sys_ix_fs.index_id = sys_fk.foreign_index_id
	inner join sys.sysuser sys_u_fs on sys_u_fs.user_id = sys_t_fs.creator
where
	sys_u_fs.user_name = ?
	and sys_t_fs.table_name = ?
	and sys_ix_fs.index_category = 2
order by
	sys_ix_fs.index_name asc