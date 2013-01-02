/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace TextMetal.Common.Data.Advanced
{
	public interface INativeDatabaseFileCreator
	{
		#region Methods/Operators

		bool CreateNativeDatabaseFile(string databaseFilePath);

		#endregion
	}
}