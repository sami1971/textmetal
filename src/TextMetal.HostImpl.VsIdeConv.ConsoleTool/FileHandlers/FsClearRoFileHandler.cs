/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Diagnostics;
using System.IO;

namespace TextMetal.HostImpl.VsIdeConv.ConsoleTool.FileHandlers
{
	public class FsClearRoFileHandler : FileHandler
	{
		#region Constructors/Destructors

		private FsClearRoFileHandler()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly FsClearRoFileHandler instance = new FsClearRoFileHandler();

		#endregion

		#region Properties/Indexers/Events

		public static FsClearRoFileHandler Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void OnExecute(FileInfo fileInfo)
		{
			if ((object)fileInfo == null)
				throw new ArgumentNullException("fileInfo");

			Debug.WriteLine("Clearing read-only flag: " + fileInfo.FullName);

			fileInfo.IsReadOnly = false;
		}

		#endregion
	}
}