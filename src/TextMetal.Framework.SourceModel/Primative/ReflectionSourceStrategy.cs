/*
	Copyright ©2002-2013 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using TextMetal.Common.Core;
using TextMetal.Framework.AssociativeModel;

namespace TextMetal.Framework.SourceModel.Primative
{
	public class ReflectionSourceStrategy : SourceStrategy
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the ReflectionSourceStrategy class.
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

		private static void ModelAssemblies(Assembly[] assemblies, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			Type[] types;

			if ((object)assemblies == null)
				throw new ArgumentNullException("assemblies");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Assmeblies";
			parent.Items.Add(arrayConstruct00);

			foreach (Assembly assembly in assemblies)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyLoadedFromGlobalAssemblyCache";
				propertyConstruct00.RawValue = assembly.GlobalAssemblyCache;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyImageRuntimeVersion";
				propertyConstruct00.RawValue = assembly.ImageRuntimeVersion;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyIsDynamic";
				propertyConstruct00.RawValue = assembly.IsDynamic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyIsFullyTrusted";
				propertyConstruct00.RawValue = assembly.IsFullyTrusted;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyReflectionOnly";
				propertyConstruct00.RawValue = assembly.ReflectionOnly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyVersion";
				propertyConstruct00.RawValue = assembly.GetName().Version;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyName";
				propertyConstruct00.RawValue = assembly.GetName().Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyProcessorArchitecture";
				propertyConstruct00.RawValue = assembly.GetName().ProcessorArchitecture;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyFullName";
				propertyConstruct00.RawValue = assembly.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyLocation";
				propertyConstruct00.RawValue = assembly.Location;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyManifestModuleName";
				propertyConstruct00.RawValue = assembly.ManifestModule.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyManifestModuleVersionId";
				propertyConstruct00.RawValue = assembly.ManifestModule.ModuleVersionId;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(assembly, objectConstruct00);

				types = assembly.GetExportedTypes();

				ModelTypes(types, objectConstruct00);
				
				// TODO
				//ModelAssemblies(assembly.GetReferencedAssemblies())
			}
		}

		private static void ModelConstructors(ConstructorInfo[] constructorInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)constructorInfos == null)
				throw new ArgumentNullException("constructorInfos");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Constructors";
			parent.Items.Add(arrayConstruct00);

			foreach (ConstructorInfo constructorInfo in constructorInfos)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorName";
				propertyConstruct00.RawValue = constructorInfo.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(constructorInfo, objectConstruct00);
			}
		}

		private static void ModelCustomAttributes(ICustomAttributeProvider customAttributeProvider, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;
			ArrayConstruct arrayConstruct01;
			ObjectConstruct objectConstruct01;
			PropertyConstruct propertyConstruct01;

			Attribute[] customAttributes;
			PropertyInfo[] publicPropertyInfos;
			object value;

			if ((object)customAttributeProvider == null)
				throw new ArgumentNullException("customAttributeProvider");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			customAttributes = Reflexion.GetAllAttributes<Attribute>(customAttributeProvider);

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "CustomAttributes";
			parent.Items.Add(arrayConstruct00);

			if ((object)customAttributes != null)
			{
				foreach (Attribute customAttribute in customAttributes)
				{
					objectConstruct00 = new ObjectConstruct();
					arrayConstruct00.Items.Add(objectConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "CustomAttributeTypeFullName";
					propertyConstruct00.RawValue = customAttribute.GetType().FullName;
					objectConstruct00.Items.Add(propertyConstruct00);

					publicPropertyInfos = customAttribute.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

					if ((object)publicPropertyInfos != null)
					{
						arrayConstruct01 = new ArrayConstruct();
						arrayConstruct01.Name = "CustomAttributeProperties";
						objectConstruct00.Items.Add(arrayConstruct01);

						foreach (PropertyInfo publicPropertyInfo in publicPropertyInfos)
						{
							objectConstruct01 = new ObjectConstruct();
							arrayConstruct01.Items.Add(objectConstruct01);

							propertyConstruct01 = new PropertyConstruct();
							propertyConstruct01.Name = "CustomAttributePropertyName";
							propertyConstruct01.RawValue = publicPropertyInfo.Name;
							objectConstruct01.Items.Add(propertyConstruct01);

							if (Reflexion.GetLogicalPropertyValue(customAttribute, publicPropertyInfo.Name, out value))
							{
								propertyConstruct01 = new PropertyConstruct();
								propertyConstruct01.Name = "CustomAttributePropertyValue";
								propertyConstruct01.RawValue = value.SafeToString();
								objectConstruct01.Items.Add(propertyConstruct01);
							}
						}
					}
				}
			}
		}

		private static void ModelEvents(EventInfo[] eventInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)eventInfos == null)
				throw new ArgumentNullException("eventInfos");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Events";
			parent.Items.Add(arrayConstruct00);

			foreach (EventInfo eventInfo in eventInfos)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventName";
				propertyConstruct00.RawValue = eventInfo.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventHandlerType";
				propertyConstruct00.RawValue = eventInfo.EventHandlerType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventIsMulticast";
				propertyConstruct00.RawValue = eventInfo.IsMulticast;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventIsSpecialName";
				propertyConstruct00.RawValue = eventInfo.IsSpecialName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventAddMethodIsStatic";
				propertyConstruct00.RawValue = eventInfo.GetAddMethod().IsStatic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventRemoveMethodIsStatic";
				propertyConstruct00.RawValue = eventInfo.GetRemoveMethod().IsStatic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventRaiseMethodIsStatic";
				propertyConstruct00.RawValue = eventInfo.GetRaiseMethod().IsStatic;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(eventInfo, objectConstruct00);
			}
		}

		private static void ModelFields(FieldInfo[] fieldInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)fieldInfos == null)
				throw new ArgumentNullException("fieldInfos");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Fields";
			parent.Items.Add(arrayConstruct00);

			foreach (FieldInfo fieldInfo in fieldInfos)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldName";
				propertyConstruct00.RawValue = fieldInfo.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldType";
				propertyConstruct00.RawValue = fieldInfo.FieldType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsAssembly";
				propertyConstruct00.RawValue = fieldInfo.IsAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsFamily";
				propertyConstruct00.RawValue = fieldInfo.IsFamily;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsFamilyAndAssembly";
				propertyConstruct00.RawValue = fieldInfo.IsFamilyAndAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsFamilyOrAssembly";
				propertyConstruct00.RawValue = fieldInfo.IsFamilyOrAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsInitOnly";
				propertyConstruct00.RawValue = fieldInfo.IsInitOnly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsLiteral";
				propertyConstruct00.RawValue = fieldInfo.IsLiteral;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsNotSerialized";
				propertyConstruct00.RawValue = fieldInfo.IsNotSerialized;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsPinvokeImpl";
				propertyConstruct00.RawValue = fieldInfo.IsPinvokeImpl;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsPrivate";
				propertyConstruct00.RawValue = fieldInfo.IsPrivate;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsPublic";
				propertyConstruct00.RawValue = fieldInfo.IsPublic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsSecurityCritical";
				propertyConstruct00.RawValue = fieldInfo.IsSecurityCritical;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsSecuritySafeCritical";
				propertyConstruct00.RawValue = fieldInfo.IsSecuritySafeCritical;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsSecurityTransparent";
				propertyConstruct00.RawValue = fieldInfo.IsSecurityTransparent;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsSpecialName";
				propertyConstruct00.RawValue = fieldInfo.IsSpecialName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldIsStatic";
				propertyConstruct00.RawValue = fieldInfo.IsStatic;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(fieldInfo, objectConstruct00);
			}
		}

		private static void ModelMethods(MethodInfo[] methodInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)methodInfos == null)
				throw new ArgumentNullException("methodInfos");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Methods";
			parent.Items.Add(arrayConstruct00);

			foreach (MethodInfo methodInfo in methodInfos)
			{
				if (!IsRealMemberInfo(methodInfo))
					continue;

				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodName";
				propertyConstruct00.RawValue = methodInfo.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodCallingConvention";
				propertyConstruct00.RawValue = methodInfo.CallingConvention;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsAbstract";
				propertyConstruct00.RawValue = methodInfo.IsAbstract;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsAssembly";
				propertyConstruct00.RawValue = methodInfo.IsAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsConstructor";
				propertyConstruct00.RawValue = methodInfo.IsConstructor;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsFamily";
				propertyConstruct00.RawValue = methodInfo.IsFamily;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsFamilyAndAssembly";
				propertyConstruct00.RawValue = methodInfo.IsFamilyAndAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsFamilyOrAssembly";
				propertyConstruct00.RawValue = methodInfo.IsFamilyOrAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsFinal";
				propertyConstruct00.RawValue = methodInfo.IsFinal;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsGenericMethod";
				propertyConstruct00.RawValue = methodInfo.IsGenericMethod;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsGenericMethodDefinition";
				propertyConstruct00.RawValue = methodInfo.IsGenericMethodDefinition;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsHideBySig";
				propertyConstruct00.RawValue = methodInfo.IsHideBySig;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsPrivate";
				propertyConstruct00.RawValue = methodInfo.IsPrivate;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsPublic";
				propertyConstruct00.RawValue = methodInfo.IsPublic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsSecurityCritical";
				propertyConstruct00.RawValue = methodInfo.IsSecurityCritical;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsSecuritySafeCritical";
				propertyConstruct00.RawValue = methodInfo.IsSecuritySafeCritical;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsSecurityTransparent";
				propertyConstruct00.RawValue = methodInfo.IsSecurityTransparent;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsSpecialName";
				propertyConstruct00.RawValue = methodInfo.IsSpecialName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsStatic";
				propertyConstruct00.RawValue = methodInfo.IsStatic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodIsVirtual";
				propertyConstruct00.RawValue = methodInfo.IsVirtual;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodImplementationFlags";
				propertyConstruct00.RawValue = methodInfo.MethodImplementationFlags;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodReturnType";
				propertyConstruct00.RawValue = methodInfo.ReturnType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				// TODO
				//propertyConstruct00.RawValue = methodInfo.ReturnParameter;
				//methodInfo.GetParameters()
				//methodInfo.GetGenericArguments();
				//methodInfo.GetGenericMethodDefinition();

				ModelCustomAttributes(methodInfo, objectConstruct00);
			}
		}

		private static void ModelProperties(PropertyInfo[] propertyInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)propertyInfos == null)
				throw new ArgumentNullException("propertyInfos");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Properties";
			parent.Items.Add(arrayConstruct00);

			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyName";
				propertyConstruct00.RawValue = propertyInfo.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyCanRead";
				propertyConstruct00.RawValue = propertyInfo.CanRead;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyCanWrite";
				propertyConstruct00.RawValue = propertyInfo.CanWrite;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyIsSpecialName";
				propertyConstruct00.RawValue = propertyInfo.IsSpecialName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyPropertyType";
				propertyConstruct00.RawValue = propertyInfo.PropertyType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(propertyInfo, objectConstruct00);
			}
		}

		private static void ModelTypes(Type[] types, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			FieldInfo[] fieldInfos;
			PropertyInfo[] propertyInfos;
			MethodInfo[] methodInfos;
			EventInfo[] eventInfos;
			ConstructorInfo[] constructorInfos;

			if ((object)types == null)
				throw new ArgumentNullException("types");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Types";
			parent.Items.Add(arrayConstruct00);

			if ((object)types != null)
			{
				foreach (Type type in types)
				{
					objectConstruct00 = new ObjectConstruct();
					arrayConstruct00.Items.Add(objectConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeName";
					propertyConstruct00.RawValue = type.Name;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeNamespace";
					propertyConstruct00.RawValue = type.Namespace;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeFullName";
					propertyConstruct00.RawValue = type.FullName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeAssemblyQualifiedName";
					propertyConstruct00.RawValue = type.AssemblyQualifiedName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeFullName";
					propertyConstruct00.RawValue = type.FullName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeGuid";
					propertyConstruct00.RawValue = type.GUID;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsAbstract";
					propertyConstruct00.RawValue = type.IsAbstract;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsAnsiClass";
					propertyConstruct00.RawValue = type.IsAnsiClass;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsArray";
					propertyConstruct00.RawValue = type.IsArray;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsAutoClass";
					propertyConstruct00.RawValue = type.IsAutoClass;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsAutoLayout";
					propertyConstruct00.RawValue = type.IsAutoLayout;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsByRef";
					propertyConstruct00.RawValue = type.IsByRef;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsClass";
					propertyConstruct00.RawValue = type.IsClass;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsComObject";
					propertyConstruct00.RawValue = type.IsCOMObject;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsConstructedGenericType";
					propertyConstruct00.RawValue = type.IsConstructedGenericType;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsContextful";
					propertyConstruct00.RawValue = type.IsContextful;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsEnum";
					propertyConstruct00.RawValue = type.IsEnum;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsExplicitLayout";
					propertyConstruct00.RawValue = type.IsExplicitLayout;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsGenericParameter";
					propertyConstruct00.RawValue = type.IsGenericParameter;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsGenericType";
					propertyConstruct00.RawValue = type.IsGenericType;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsGenericTypeDefinition";
					propertyConstruct00.RawValue = type.IsGenericTypeDefinition;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsImport";
					propertyConstruct00.RawValue = type.IsImport;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsInterface";
					propertyConstruct00.RawValue = type.IsInterface;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsLayoutSequential";
					propertyConstruct00.RawValue = type.IsLayoutSequential;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsMarshalByRef";
					propertyConstruct00.RawValue = type.IsMarshalByRef;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNested";
					propertyConstruct00.RawValue = type.IsNested;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNestedAssembly";
					propertyConstruct00.RawValue = type.IsNestedAssembly;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNestedFamANDAssem";
					propertyConstruct00.RawValue = type.IsNestedFamANDAssem;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNestedFamORAssem";
					propertyConstruct00.RawValue = type.IsNestedFamORAssem;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNestedFamily";
					propertyConstruct00.RawValue = type.IsNestedFamily;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNestedPrivate";
					propertyConstruct00.RawValue = type.IsNestedPrivate;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNestedPublic";
					propertyConstruct00.RawValue = type.IsNestedPublic;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsNotPublic";
					propertyConstruct00.RawValue = type.IsNotPublic;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsPointer";
					propertyConstruct00.RawValue = type.IsPointer;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsPrimitive";
					propertyConstruct00.RawValue = type.IsPrimitive;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsPublic";
					propertyConstruct00.RawValue = type.IsPublic;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsSealed";
					propertyConstruct00.RawValue = type.IsSealed;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsSecurityCritical";
					propertyConstruct00.RawValue = type.IsSecurityCritical;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsSecuritySafeCritical";
					propertyConstruct00.RawValue = type.IsSecuritySafeCritical;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsSecurityTransparent";
					propertyConstruct00.RawValue = type.IsSecurityTransparent;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsSerializable";
					propertyConstruct00.RawValue = type.IsSerializable;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsSpecialName";
					propertyConstruct00.RawValue = type.IsSpecialName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsUnicodeClass";
					propertyConstruct00.RawValue = type.IsUnicodeClass;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsValueType";
					propertyConstruct00.RawValue = type.IsValueType;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeIsVisible";
					propertyConstruct00.RawValue = type.IsVisible;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeNamespace";
					propertyConstruct00.RawValue = type.Namespace;
					objectConstruct00.Items.Add(propertyConstruct00);

					if (type.IsGenericType)
					{
						propertyConstruct00 = new PropertyConstruct();
						propertyConstruct00.Name = "TypeGenericParameterAttributes";
						propertyConstruct00.RawValue = type.GenericParameterAttributes;
						objectConstruct00.Items.Add(propertyConstruct00);

						propertyConstruct00 = new PropertyConstruct();
						propertyConstruct00.Name = "TypeGenericParameterPosition";
						propertyConstruct00.RawValue = type.GenericParameterPosition;
						objectConstruct00.Items.Add(propertyConstruct00);
					}

					ModelCustomAttributes(type, objectConstruct00);

					fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

					ModelFields(fieldInfos, objectConstruct00);

					propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

					ModelProperties(propertyInfos, objectConstruct00);

					methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

					ModelMethods(methodInfos, objectConstruct00);

					eventInfos = type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

					ModelEvents(eventInfos, objectConstruct00);

					constructorInfos = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

					ModelConstructors(constructorInfos, objectConstruct00);
					
					// TODO
					//type.GetNestedTypes()
					//ModelTypes(type.GenericTypeArguments, objectConstruct00);
					//type.GetGenericArguments();
					//type.GetGenericParameterConstraints();
					//type.GetGenericTypeDefinition();
				}
			}
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			ModelConstruct modelConstruct;

			Assembly[] assemblies;
			Assembly assembly;

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

			assemblies = new Assembly[] { assembly };

			modelConstruct = new ModelConstruct();

			ModelAssemblies(assemblies, modelConstruct);

			return modelConstruct;
		}

		#endregion
	}
}