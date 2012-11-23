/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- unique keys[schema, table]
select
sys_ix.index_name as UniqueKeyName
from
sys.sysidx sys_ix
inner join sys.systable as sys_t_uk on sys_t_uk.table_id = sys_ix.table_id
inner join dbo.sysusers sys_u_uk on sys_u_uk.uid = sys_t_uk.creator
where
sys_u_uk.name = ?
and sys_t_uk.table_name = ?
and sys_ix.index_category = 3
and sys_ix."unique" = 2