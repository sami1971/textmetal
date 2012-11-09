/*
	Copyright ©2002-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using TextMetal.Core.AssociativeModel;
using TextMetal.Plumbing.CommonFacilities;

namespace TextMetal.Core.SourceModel.Primative
{
	public class ReflectionSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// 	Initializes a new instance of the ReflectionSourceStrategy class.
		/// </summary>
		public ReflectionSourceStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static bool IsRealMemberInfo(MethodInfo methodInfo)
		{
			PropertyInfo[] propertyInfos;
			EventInfo[] eventInfos;
			MethodInfo accessorMethodInfo = null;

			if ((object)methodInfo == null)
				throw new ArgumentNullException("methodInfo");

			propertyInfos = methodInfo.DeclaringType.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

			if ((object)propertyInfos != null)
			{
				foreach (PropertyInfo propertyInfo in propertyInfos)
				{
					accessorMethodInfo = propertyInfo.GetGetMethod(true);

					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return false;

					accessorMethodInfo = propertyInfo.GetSetMethod(true);
					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return false;
				}
			}

			eventInfos = methodInfo.DeclaringType.GetEvents(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);

			if ((object)eventInfos != null)
			{
				foreach (EventInfo eventInfo in eventInfos)
				{
					accessorMethodInfo = eventInfo.GetAddMethod(true);

					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return false;

					accessorMethodInfo = eventInfo.GetRemoveMethod(true);
					if ((object)accessorMethodInfo != null && accessorMethodInfo.Equals(methodInfo))
						return false;
				}
			}

			return true;
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			Assembly assembly;
			ModelConstruct modelConstruct;
			ArrayConstruct arrayConstruct;
			PropertyConstruct propertyConstruct;
			ObjectConstruct objectConstruct;

			ArrayConstruct arrayConstruct01;
			PropertyConstruct propertyConstruct01;
			ObjectConstruct objectConstruct01;

			Type[] publicTypes;
			FieldInfo[] publicFieldInfos;
			PropertyInfo[] publicPropertyInfos;
			MethodInfo[] publicMethodInfos;
			EventInfo[] publicEventInfos;
			ConstructorInfo[] publicConstructorInfos;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			sourceFilePath = Path.GetFullPath(sourceFilePath);

			assembly = Assembly.LoadFile(sourceFilePath);

			if ((object)assembly == null)
				throw new InvalidOperationException(string.Format("Failed to load the assembly file '{0}' via Assembly.LoadFile(..).", sourceFilePath));

			modelConstruct = new ModelConstruct();

			propertyConstruct = new PropertyConstruct();
			propertyConstruct.Name = "AssemblyFullName";
			propertyConstruct.Value = assembly.FullName;
			modelConstruct.Items.Add(propertyConstruct);

			propertyConstruct = new PropertyConstruct();
			propertyConstruct.Name = "AssemblyLocation";
			propertyConstruct.Value = assembly.Location;
			modelConstruct.Items.Add(propertyConstruct);

			arrayConstruct = new ArrayConstruct();
			arrayConstruct.Name = "PublicTypes";
			modelConstruct.Items.Add(arrayConstruct);

			publicTypes = assembly.GetExportedTypes();

			if ((object)publicTypes != null)
			{
				foreach (Type publicType in publicTypes)
				{
					objectConstruct = new ObjectConstruct();
					arrayConstruct.Items.Add(objectConstruct);

					propertyConstruct = new PropertyConstruct();
					propertyConstruct.Name = "PublicTypeName";
					propertyConstruct.Value = publicType.Name;
					objectConstruct.Items.Add(propertyConstruct);

					propertyConstruct = new PropertyConstruct();
					propertyConstruct.Name = "PublicTypeNamespace";
					propertyConstruct.Value = publicType.Namespace;
					objectConstruct.Items.Add(propertyConstruct);

					propertyConstruct = new PropertyConstruct();
					propertyConstruct.Name = "PublicTypeFullName";
					propertyConstruct.Value = publicType.FullName;
					objectConstruct.Items.Add(propertyConstruct);

					propertyConstruct = new PropertyConstruct();
					propertyConstruct.Name = "PublicTypeAssemblyQualifiedName";
					propertyConstruct.Value = publicType.AssemblyQualifiedName;
					objectConstruct.Items.Add(propertyConstruct);

					publicFieldInfos = publicType.GetFields(BindingFlags.Public | BindingFlags.Instance);

					if ((object)publicFieldInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicInstanceFields";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (FieldInfo publicFieldInfo in publicFieldInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicInstanceFieldName";
							propertyConstruct01.Value = publicFieldInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicFieldInfos = publicType.GetFields(BindingFlags.Public | BindingFlags.Static);

					if ((object)publicFieldInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicStaticFields";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (FieldInfo publicFieldInfo in publicFieldInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicStaticFieldName";
							propertyConstruct01.Value = publicFieldInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicPropertyInfos = publicType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

					if ((object)publicPropertyInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicInstanceProperties";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (PropertyInfo publicPropertyInfo in publicPropertyInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicInstancePropertyName";
							propertyConstruct01.Value = publicPropertyInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicPropertyInfos = publicType.GetProperties(BindingFlags.Public | BindingFlags.Static);

					if ((object)publicPropertyInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicStaticProperties";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (PropertyInfo publicPropertyInfo in publicPropertyInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicStaticPropertyName";
							propertyConstruct01.Value = publicPropertyInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicMethodInfos = publicType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

					if ((object)publicMethodInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicInstanceMethods";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (MethodInfo publicMethodInfo in publicMethodInfos)
						{
							if (!IsRealMemberInfo(publicMethodInfo))
								continue;

							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicInstanceMethodName";
							propertyConstruct01.Value = publicMethodInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicMethodInfos = publicType.GetMethods(BindingFlags.Public | BindingFlags.Static);

					if ((object)publicMethodInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicStaticMethods";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (MethodInfo publicMethodInfo in publicMethodInfos)
						{
							if (!IsRealMemberInfo(publicMethodInfo))
								continue;

							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicStaticMethodName";
							propertyConstruct01.Value = publicMethodInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicEventInfos = publicType.GetEvents(BindingFlags.Public | BindingFlags.Instance);

					if ((object)publicEventInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicInstanceEvents";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (EventInfo publicEventInfo in publicEventInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicInstanceEventName";
							propertyConstruct01.Value = publicEventInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicEventInfos = publicType.GetEvents(BindingFlags.Public | BindingFlags.Static);

					if ((object)publicEventInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicStaticEvents";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (EventInfo publicEventInfo in publicEventInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicStaticEventName";
							propertyConstruct01.Value = publicEventInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicConstructorInfos = publicType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

					if ((object)publicConstructorInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicInstanceConstructors";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (ConstructorInfo publicConstructorInfo in publicConstructorInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicInstanceConstructorName";
							propertyConstruct01.Value = publicConstructorInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}

					publicConstructorInfos = publicType.GetConstructors(BindingFlags.Public | BindingFlags.Static);

					if ((object)publicConstructorInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "PublicStaticConstructors";
						objectConstruct.Items.Add(arrayConstruct01);

						foreach (ConstructorInfo publicConstructorInfo in publicConstructorInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "PublicStaticConstructorName";
							propertyConstruct01.Value = publicConstructorInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);
						}
					}
				}
			}

			return modelConstruct;
		}

		#endregion
	}
}