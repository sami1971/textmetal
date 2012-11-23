/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign keys[schema, table]
select
sys_ix.index_name as ForeignKeyName,
case
    when sys_ix.not_enforced = 'Y' then 1
    when sys_ix.not_enforced = 'N' then 0
    else null
end as ForeignKeyIsDisabled,
null as ForeignKeyIsForReplication,
null as ForeignKeyOnDeleteRefIntAction,
null as ForeignKeyOnDeleteRefIntActionSqlName,
null as ForeignKeyOnUpdateRefIntAction,
null as ForeignKeyOnUpdateRefIntActionSqlName
from
sys.sysfkey sys_fk
inner join sys.sysidx sys_ix on sys_ix.index_id = sys_fk.foreign_index_id and sys_ix.table_id = sys_fk.foreign_table_id
inner join sys.systable sys_t_fk on sys_t_fk.table_id = sys_fk.foreign_table_id
inner join dbo.sysusers sys_u_fk on sys_u_fk.uid = sys_t_fk.creator
where
sys_u_fk.name = ?
and sys_t_fk.table_name = ?
and sys_ix.index_category = 2