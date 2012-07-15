/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

-- schemas
select
    sys_s.name as SchemaName	
from
    sys.schemas sys_s -- schemas
order by
	sys_s.name asc