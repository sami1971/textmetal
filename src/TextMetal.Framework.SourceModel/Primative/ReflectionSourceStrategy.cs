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
			AssemblyName[] assemblyReferences;
			AssemblyName assemblyName;

			if ((object)assemblies == null)
				throw new ArgumentNullException("assemblies");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Assemblies";
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

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyManifestModuleFullyQualifiedName";
				propertyConstruct00.RawValue = assembly.ManifestModule.FullyQualifiedName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyManifestModuleMDStreamVersion";
				propertyConstruct00.RawValue = assembly.ManifestModule.MDStreamVersion;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyManifestModuleScopeName";
				propertyConstruct00.RawValue = assembly.ManifestModule.ScopeName;
				objectConstruct00.Items.Add(propertyConstruct00);

				//ModelCustomAttributes(assembly.ManifestModule, objectConstruct00);

				assemblyName = assembly.GetName();

				ModelAssemblyName(assemblyName, objectConstruct00);

				ModelCustomAttributes(assembly, objectConstruct00);

				types = assembly.GetExportedTypes();

				ModelTypes("Types", types, objectConstruct00);

				assemblyReferences = assembly.GetReferencedAssemblies();

				ModelAssemblyReferences(assemblyReferences, objectConstruct00);
			}
		}

		private static void ModelAssemblyName(AssemblyName assemblyName, AssociativeXmlObject parent)
		{
			PropertyConstruct propertyConstruct00;

			if ((object)assemblyName == null)
				throw new ArgumentNullException("assemblyName");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyVersion";
			propertyConstruct00.RawValue = assemblyName.Version;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyName";
			propertyConstruct00.RawValue = assemblyName.Name;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyCodeBase";
			propertyConstruct00.RawValue = assemblyName.CodeBase;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyContentType";
			propertyConstruct00.RawValue = assemblyName.ContentType;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyCultureInfoDisplayName";
			propertyConstruct00.RawValue = assemblyName.CultureInfo.DisplayName;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyCultureName";
			propertyConstruct00.RawValue = assemblyName.CultureName;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyHashAlgorithm";
			propertyConstruct00.RawValue = assemblyName.HashAlgorithm;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyFullName";
			propertyConstruct00.RawValue = assemblyName.FullName;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyFlags";
			propertyConstruct00.RawValue = assemblyName.Flags;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyVersionCompatibility";
			propertyConstruct00.RawValue = assemblyName.VersionCompatibility;
			parent.Items.Add(propertyConstruct00);

			propertyConstruct00 = new PropertyConstruct();
			propertyConstruct00.Name = "AssemblyProcessorArchitecture";
			propertyConstruct00.RawValue = assemblyName.ProcessorArchitecture;
			parent.Items.Add(propertyConstruct00);

			if ((object)assemblyName.KeyPair != null)
			{
				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "AssemblyKeyPairPublicKey";
				propertyConstruct00.RawValue = assemblyName.KeyPair.PublicKey;
				parent.Items.Add(propertyConstruct00);
			}
		}

		private static void ModelAssemblyReferences(AssemblyName[] assemblyReferences, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)assemblyReferences == null)
				throw new ArgumentNullException("assemblyReferences");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "AssemblyReferences";
			parent.Items.Add(arrayConstruct00);

			foreach (AssemblyName assemblyReference in assemblyReferences)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				ModelAssemblyName(assemblyReference, objectConstruct00);
			}
		}

		private static void ModelConstructors(ConstructorInfo[] constructorInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			ParameterInfo[] parameterInfos;

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

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorCallingConvention";
				propertyConstruct00.RawValue = constructorInfo.CallingConvention;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorContainsGenericParameters";
				propertyConstruct00.RawValue = constructorInfo.ContainsGenericParameters;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsGenericMethod";
				propertyConstruct00.RawValue = constructorInfo.IsGenericMethod;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsGenericMethodDefinition";
				propertyConstruct00.RawValue = constructorInfo.IsGenericMethodDefinition;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsAbstract";
				propertyConstruct00.RawValue = constructorInfo.IsAbstract;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsAssembly";
				propertyConstruct00.RawValue = constructorInfo.IsAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsFamily";
				propertyConstruct00.RawValue = constructorInfo.IsFamily;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsFamilyAndAssembly";
				propertyConstruct00.RawValue = constructorInfo.IsFamilyAndAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsFamilyOrAssembly";
				propertyConstruct00.RawValue = constructorInfo.IsFamilyOrAssembly;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsFinal";
				propertyConstruct00.RawValue = constructorInfo.IsFinal;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsHideBySig";
				propertyConstruct00.RawValue = constructorInfo.IsHideBySig;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsPrivate";
				propertyConstruct00.RawValue = constructorInfo.IsPrivate;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsPublic";
				propertyConstruct00.RawValue = constructorInfo.IsPublic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsSecurityCritical";
				propertyConstruct00.RawValue = constructorInfo.IsSecurityCritical;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsSecuritySafeCritical";
				propertyConstruct00.RawValue = constructorInfo.IsSecuritySafeCritical;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsSecurityTransparent";
				propertyConstruct00.RawValue = constructorInfo.IsSecurityTransparent;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsSpecialName";
				propertyConstruct00.RawValue = constructorInfo.IsSpecialName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsStatic";
				propertyConstruct00.RawValue = constructorInfo.IsStatic;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorIsVirtual";
				propertyConstruct00.RawValue = constructorInfo.IsVirtual;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ConstructorMethodImplementationFlags";
				propertyConstruct00.RawValue = constructorInfo.MethodImplementationFlags;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(constructorInfo, objectConstruct00);

				parameterInfos = constructorInfo.GetParameters();

				ModelParameters(parameterInfos, objectConstruct00);
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

			MethodInfo methodInfo;

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
				propertyConstruct00.Name = "EventHandlerTypeName";
				propertyConstruct00.RawValue = eventInfo.EventHandlerType.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventHandlerTypeNamespace";
				propertyConstruct00.RawValue = eventInfo.EventHandlerType.Namespace;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventHandlerTypeFullName";
				propertyConstruct00.RawValue = eventInfo.EventHandlerType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventHandlerTypeAssemblyQualifiedName";
				propertyConstruct00.RawValue = eventInfo.EventHandlerType.AssemblyQualifiedName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventIsMulticast";
				propertyConstruct00.RawValue = eventInfo.IsMulticast;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "EventIsSpecialName";
				propertyConstruct00.RawValue = eventInfo.IsSpecialName;
				objectConstruct00.Items.Add(propertyConstruct00);

				methodInfo = eventInfo.GetAddMethod();

				if ((object)methodInfo != null)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "EventAddMethodIsStatic";
					propertyConstruct00.RawValue = methodInfo.IsStatic;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

				methodInfo = eventInfo.GetRemoveMethod();

				if ((object)methodInfo != null)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "EventRemoveMethodIsStatic";
					propertyConstruct00.RawValue = methodInfo.IsStatic;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

				methodInfo = eventInfo.GetRaiseMethod();

				if ((object)methodInfo != null)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "EventRaiseMethodIsStatic";
					propertyConstruct00.RawValue = methodInfo.IsStatic;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

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
				propertyConstruct00.Name = "FieldTypeName";
				propertyConstruct00.RawValue = fieldInfo.FieldType.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldTypeNamespace";
				propertyConstruct00.RawValue = fieldInfo.FieldType.Namespace;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldTypeFullName";
				propertyConstruct00.RawValue = fieldInfo.FieldType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "FieldTypeAssemblyQualifiedName";
				propertyConstruct00.RawValue = fieldInfo.FieldType.AssemblyQualifiedName;
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

				if (fieldInfo.IsLiteral)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "FieldRawConstantValue";
					propertyConstruct00.RawValue = fieldInfo.GetRawConstantValue();
					objectConstruct00.Items.Add(propertyConstruct00);
				}

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

			ParameterInfo[] parameterInfos;
			Type[] childTypes;

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
				propertyConstruct00.Name = "MethodContainsGenericParameters";
				propertyConstruct00.RawValue = methodInfo.ContainsGenericParameters;
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
				propertyConstruct00.Name = "MethodReturnTypeName";
				propertyConstruct00.RawValue = methodInfo.ReturnType.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodReturnTypeNamespace";
				propertyConstruct00.RawValue = methodInfo.ReturnType.Namespace;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodReturnTypeFullName";
				propertyConstruct00.RawValue = methodInfo.ReturnType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "MethodReturnTypeAssemblyQualifiedName";
				propertyConstruct00.RawValue = methodInfo.ReturnType.AssemblyQualifiedName;
				objectConstruct00.Items.Add(propertyConstruct00);

				if (methodInfo.IsGenericMethod)
				{
					var _methodInfo = methodInfo.GetGenericMethodDefinition();

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "MethodGenericMethodDefinitionName";
					propertyConstruct00.RawValue = _methodInfo.DeclaringType.Name;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "MethodGenericMethodDefinitionNamespace";
					propertyConstruct00.RawValue = _methodInfo.DeclaringType.Namespace;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "MethodGenericMethodDefinitionFullName";
					propertyConstruct00.RawValue = _methodInfo.DeclaringType.FullName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "MethodGenericMethodDefinitionAssemblyQualifiedName";
					propertyConstruct00.RawValue = _methodInfo.DeclaringType.AssemblyQualifiedName;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

				ModelCustomAttributes(methodInfo, objectConstruct00);

				parameterInfos = methodInfo.GetParameters();
				Array.Resize(ref parameterInfos, parameterInfos.Length + 1);
				parameterInfos[parameterInfos.Length - 1] = methodInfo.ReturnParameter;

				ModelParameters(parameterInfos, objectConstruct00);

				childTypes = methodInfo.GetGenericArguments();

				ModelTypes("GenericArguments", childTypes, objectConstruct00);
			}
		}

		private static void ModelParameters(ParameterInfo[] parameterInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			if ((object)parameterInfos == null)
				throw new ArgumentNullException("parameterInfos");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = "Parameters";
			parent.Items.Add(arrayConstruct00);

			foreach (ParameterInfo parameterInfo in parameterInfos)
			{
				objectConstruct00 = new ObjectConstruct();
				arrayConstruct00.Items.Add(objectConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterName";
				propertyConstruct00.RawValue = parameterInfo.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterDefaultValue";
				propertyConstruct00.RawValue = parameterInfo.DefaultValue;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterHasDefaultValue";
				propertyConstruct00.RawValue = parameterInfo.HasDefaultValue;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterIsIn";
				propertyConstruct00.RawValue = parameterInfo.IsIn;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterIsLcid";
				propertyConstruct00.RawValue = parameterInfo.IsLcid;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterIsOptional";
				propertyConstruct00.RawValue = parameterInfo.IsOptional;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterIsOut";
				propertyConstruct00.RawValue = parameterInfo.IsOut;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterIsRetval";
				propertyConstruct00.RawValue = parameterInfo.IsRetval;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterTypeName";
				propertyConstruct00.RawValue = parameterInfo.ParameterType.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterTypeNamespace";
				propertyConstruct00.RawValue = parameterInfo.ParameterType.Namespace;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterTypeFullName";
				propertyConstruct00.RawValue = parameterInfo.ParameterType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterTypeAssemblyQualifiedName";
				propertyConstruct00.RawValue = parameterInfo.ParameterType.AssemblyQualifiedName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterIsByRef";
				propertyConstruct00.RawValue = parameterInfo.ParameterType.IsByRef;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "ParameterPosition";
				propertyConstruct00.RawValue = parameterInfo.Position;
				objectConstruct00.Items.Add(propertyConstruct00);

				ModelCustomAttributes(parameterInfo, objectConstruct00);
			}
		}

		private static void ModelProperties(PropertyInfo[] propertyInfos, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			ParameterInfo[] parameterInfos;
			MethodInfo methodInfo;

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
				propertyConstruct00.Name = "PropertyTypeName";
				propertyConstruct00.RawValue = propertyInfo.PropertyType.Name;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyTypeNamespace";
				propertyConstruct00.RawValue = propertyInfo.PropertyType.Namespace;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyTypeFullName";
				propertyConstruct00.RawValue = propertyInfo.PropertyType.FullName;
				objectConstruct00.Items.Add(propertyConstruct00);

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "PropertyTypeAssemblyQualifiedName";
				propertyConstruct00.RawValue = propertyInfo.PropertyType.AssemblyQualifiedName;
				objectConstruct00.Items.Add(propertyConstruct00);

				methodInfo = propertyInfo.GetGetMethod();

				if ((object)methodInfo != null)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "PropertyGetMethodIsStatic";
					propertyConstruct00.RawValue = methodInfo.IsStatic;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

				methodInfo = propertyInfo.GetSetMethod();

				if ((object)methodInfo != null)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "PropertySetMethodIsStatic";
					propertyConstruct00.RawValue = methodInfo.IsStatic;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

				ModelCustomAttributes(propertyInfo, objectConstruct00);

				parameterInfos = propertyInfo.GetIndexParameters();

				ModelParameters(parameterInfos, objectConstruct00);
			}
		}

		private static void ModelTypes(string arrayName, Type[] types, AssociativeXmlObject parent)
		{
			ArrayConstruct arrayConstruct00;
			PropertyConstruct propertyConstruct00;
			ObjectConstruct objectConstruct00;

			FieldInfo[] fieldInfos;
			PropertyInfo[] propertyInfos;
			MethodInfo[] methodInfos;
			EventInfo[] eventInfos;
			ConstructorInfo[] constructorInfos;

			Type[] childTypes;

			if ((object)types == null)
				throw new ArgumentNullException("types");

			if ((object)parent == null)
				throw new ArgumentNullException("parent");

			arrayConstruct00 = new ArrayConstruct();
			arrayConstruct00.Name = arrayName;
			parent.Items.Add(arrayConstruct00);

			foreach (Type type in types)
			{
				Console.WriteLine("{0} ==> '{1}'", arrayName, type.FullName);

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

				if ((object)type.BaseType != null)
				{
					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeBaseName";
					propertyConstruct00.RawValue = type.BaseType.Name;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeBaseNamespace";
					propertyConstruct00.RawValue = type.BaseType.Namespace;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeBaseFullName";
					propertyConstruct00.RawValue = type.BaseType.FullName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeBaseAssemblyQualifiedName";
					propertyConstruct00.RawValue = type.BaseType.AssemblyQualifiedName;
					objectConstruct00.Items.Add(propertyConstruct00);
				}

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

				propertyConstruct00 = new PropertyConstruct();
				propertyConstruct00.Name = "TypeContainsGenericParameters";
				propertyConstruct00.RawValue = type.ContainsGenericParameters;
				objectConstruct00.Items.Add(propertyConstruct00);

				if (type.IsGenericParameter)
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

				if (type.IsGenericType)
				{
					var _type = type.GetGenericTypeDefinition();

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeGenericTypeDefinitionName";
					propertyConstruct00.RawValue = _type.Name;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeGenericTypeDefinitionNamespace";
					propertyConstruct00.RawValue = _type.Namespace;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeGenericTypeDefinitionFullName";
					propertyConstruct00.RawValue = _type.FullName;
					objectConstruct00.Items.Add(propertyConstruct00);

					propertyConstruct00 = new PropertyConstruct();
					propertyConstruct00.Name = "TypeGenericTypeDefinitionAssemblyQualifiedName";
					propertyConstruct00.RawValue = _type.AssemblyQualifiedName;
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

				childTypes = type.GenericTypeArguments;

				ModelTypes("GenericTypeArguments", childTypes, objectConstruct00);

				childTypes = type.GetGenericArguments();

				ModelTypes("GenericArguments", childTypes, objectConstruct00);

				if (type.IsGenericParameter)
				{
					childTypes = type.GetGenericParameterConstraints();

					ModelTypes("GenericParameterConstraints", childTypes, objectConstruct00);
				}

				childTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

				ModelTypes("NestedTypes", childTypes, objectConstruct00);
			}
		}

		protected override object CoreGetSourceObject(string sourceFilePath, IDictionary<string, IList<string>> properties)
		{
			ModelConstruct modelConstruct;

			List<Assembly> assemblies;
			Assembly assembly;
			IEnumerable<string> filePaths;

			if ((object)sourceFilePath == null)
				throw new ArgumentNullException("sourceFilePath");

			if ((object)properties == null)
				throw new ArgumentNullException("properties");

			if (DataType.IsWhiteSpace(sourceFilePath))
				throw new ArgumentOutOfRangeException("sourceFilePath");

			assemblies = new List<Assembly>();
			sourceFilePath = Path.GetFullPath(sourceFilePath);

			if (File.Exists(sourceFilePath))
				filePaths = new string[] { sourceFilePath };
			else if (Directory.Exists(sourceFilePath))
				filePaths = Directory.EnumerateFiles(sourceFilePath, "*.dll", SearchOption.TopDirectoryOnly);
			else
				filePaths = null;

			if ((object)filePaths != null)
			{
				foreach (string filePath in filePaths)
				{
					assembly = Assembly.LoadFile(filePath);

					if ((object)assembly == null)
						throw new InvalidOperationException(string.Format("Failed to load the assembly file '{0}' via Assembly.LoadFile(..).", sourceFilePath));

					assemblies.Add(assembly);
				}
			}

			modelConstruct = new ModelConstruct();

			ModelAssemblies(assemblies.ToArray(), modelConstruct);

			return modelConstruct;
		}

		#endregion
	}
}