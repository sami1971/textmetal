/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- foreign key columns (refs)[schema, foreign key]
select		
	sys_fkc.constraint_column_id as ForeignKeyColumnOrdinal,		
	sys_fkc.parent_column_id as ForeignKeyParentColumnOrdinal,	
	sys_tc.name as ForeignKeyChildTableName,
	sys_fkc.referenced_column_id as ForeignKeyChildColumnOrdinal
from
	sys.foreign_key_columns sys_fkc -- foreign key columns
	inner join sys.foreign_keys sys_fk on sys_fk.object_id = sys_fkc.constraint_object_id -- owner foreign key
	inner join sys.schemas sys_s ON sys_s.schema_id = sys_fk.schema_id	-- owner schema
	inner join sys.tables sys_tp on sys_tp.object_id = sys_fkc.parent_object_id -- parent table
	inner join sys.tables sys_tc on sys_tc.object_id = sys_fkc.referenced_object_id -- child table
where	
	(sys_s.name = ?) and
	(sys_tp.name = ?) and
	(sys_fk.name = ?)
order by
	sys_fkc.constraint_column_id asc -- foreign key column ordinal