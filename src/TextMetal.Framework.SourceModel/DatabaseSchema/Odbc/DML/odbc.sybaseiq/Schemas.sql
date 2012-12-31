/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- schemas
select 
	sys_u.user_name as SchemaName
from
	sys.sysuser sys_u
order by
	sys_u.user_name asc