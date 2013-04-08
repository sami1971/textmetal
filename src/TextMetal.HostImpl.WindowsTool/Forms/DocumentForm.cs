/*
	Copyright ©2010-2012 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.IO;
using System.Windows.Forms;

using TextMetal.Common.Core;
using TextMetal.Framework.AssociativeModel;
using TextMetal.Framework.SourceModel.Primative;
using TextMetal.Framework.TemplateModel;
using TextMetal.HostImpl.Tool;

using Message = TextMetal.Common.Core.Message;

namespace TextMetal.HostImpl.WindowsTool.Forms
{
	public partial class DocumentForm : TmForm
	{
		#region Constructors/Destructors

		public DocumentForm()
		{
			this.InitializeComponent();
			this.CoreIsDirtyIndicator = '*';
		}

		#endregion

		#region Fields/Constants

		private object document;
		private string documentFilePath;
		private DocumentSpecific.IDocumentStrategy documentStrategy;

		#endregion

		#region Properties/Indexers/Events

		private object Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
			}
		}

		public string DocumentFilePath
		{
			get
			{
				return this.documentFilePath;
			}
			set
			{
				this.documentFilePath = value;
			}
		}

		public DocumentSpecific.IDocumentStrategy DocumentStrategy
		{
			get
			{
				return this.documentStrategy;
			}
			set
			{
				this.documentStrategy = value;
			}
		}

		public string StatusText
		{
			get
			{
				return this.tsslMain.Text;
			}
			set
			{
				this.tsslMain.Text = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void ApplyModelToView()
		{
		}

		private void ApplyViewToModel()
		{
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			this.ApplyModelToView();
		}

		protected override void CoreShown()
		{
			DialogResult dialogResult;
			object asyncResult;
			bool asyncWasCanceled;
			Exception asyncExceptionOrNull;

			base.CoreShown();

			dialogResult = BackgroundTaskForm.Show(this, "Loading document...", o =>
			                                                                    {
				                                                                    //Thread.Sleep(1000);
				                                                                    return this.DocumentStrategy.LoadDocument(this.DocumentFilePath);
			                                                                    }, null, out asyncWasCanceled, out asyncExceptionOrNull, out asyncResult);

			if (asyncWasCanceled || dialogResult == DialogResult.Cancel)
				this.Close(); // direct

			if ((object)asyncExceptionOrNull != null)
			{
				Program.ShowNestedExceptionsAndThrowBrickAtProcess(asyncExceptionOrNull);
				// should never reach this point
			}

			this.Document = asyncResult;

			if ((object)this.Document == null)
				throw new InvalidOperationException("TODO: add meaningful message");

			this.CoreText = string.Format("{0}", this.DocumentFilePath.SafeToString(null, "<new>"));
			this.StatusText = this.DocumentStrategy.DisplayText;

			this.ApplyModelToView();
		}

		public bool SaveDocument(bool asCopy)
		{
			Message[] messages;
			string filePath;

			if ((object)this.Document == null)
				throw new InvalidOperationException("TODO: add meaningful message");

			this.ApplyViewToModel();

			if (asCopy && !DataType.IsNullOrWhiteSpace(this.DocumentFilePath))
			{
				if (MessageBox.Show(this, "Do you want to save a copy of the current document?", Program.AssemblyInformation.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
					return false;
			}

			messages = this.DocumentStrategy.ValidateDocument(this.document);

			if ((object)messages == null)
				throw new InvalidOperationException("TODO: add meaningful message");

			if (messages.Length != 0)
			{
				using (MessageForm messageForm = new MessageForm()
				                                 {
					                                 Message = "",
					                                 Messages = messages
				                                 })
					messageForm.ShowDialog(this);

				return false;
			}

			if (asCopy)
			{
				// get new file path
				if (!this.TryGetFilePath(out filePath))
					return false;

				this.DocumentFilePath = filePath;
			}
			else
			{
				if (DataType.IsNullOrWhiteSpace(this.DocumentFilePath))
				{
					if (!this.TryGetFilePath(out filePath))
						return false;

					this.DocumentFilePath = filePath;
				}
			}

			// save document
			this.DocumentStrategy.SaveDocument(this.Document, this.DocumentFilePath);

			this.CoreText = string.Format("{0}", this.DocumentFilePath.SafeToString(null, "<new>"));
			this.ApplyModelToView();

			return true;
		}

		private bool TryGetFilePath(out string filePath)
		{
			DialogResult dialogResult;

			this.sfdMain.FileName = filePath = null;
			dialogResult = this.sfdMain.ShowDialog(this);

			if (dialogResult != DialogResult.OK ||
			    DataType.IsNullOrWhiteSpace(this.sfdMain.FileName))
				return false;

			filePath = Path.GetFullPath(this.sfdMain.FileName);
			return true;
		}

		private void tsmiClose_Click(object sender, EventArgs e)
		{
			this.Close(); // direct
		}

		private void tsmiSaveAs_Click(object sender, EventArgs e)
		{
			this.SaveDocument(true);
		}

		private void tsmiSave_Click(object sender, EventArgs e)
		{
			this.SaveDocument(false);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		public static class DocumentSpecific
		{
			#region Classes/Structs/Interfaces/Enums/Delegates

			public sealed class AssociativeModelDocumentStrategy : IDocumentStrategy
			{
				#region Constructors/Destructors

				private AssociativeModelDocumentStrategy()
				{
				}

				#endregion

				#region Fields/Constants

				private static readonly IDocumentStrategy instance = new AssociativeModelDocumentStrategy();

				#endregion

				#region Properties/Indexers/Events

				public static IDocumentStrategy Instance
				{
					get
					{
						return instance;
					}
				}

				public string DisplayText
				{
					get
					{
						return "Associative Model";
					}
				}

				#endregion

				#region Methods/Operators

				public object LoadDocument(string filePath)
				{
					if (DataType.IsNullOrWhiteSpace(filePath))
						return new ObjectConstruct();
					else
						return new ToolHost().LoadModelOnly(filePath);
				}

				public void SaveDocument(object document, string filePath)
				{
					new ToolHost().SaveModelOnly((ObjectConstruct)document, filePath);
				}

				public object UpdateDocumentProps(object document)
				{
					throw new NotImplementedException();
				}

				public void UpdateDocumentTree(object document, TreeView tvDocument)
				{
					throw new NotImplementedException();
				}

				public Message[] ValidateDocument(object document)
				{
					return new Message[] { };
				}

				#endregion
			}

			public interface IDocumentStrategy
			{
				#region Properties/Indexers/Events

				string DisplayText
				{
					get;
				}

				#endregion

				#region Methods/Operators

				object LoadDocument(string filePath);

				void SaveDocument(object document, string filePath);

				object UpdateDocumentProps(object document);

				void UpdateDocumentTree(object document, TreeView tvDocument);

				Message[] ValidateDocument(object document);

				#endregion
			}

			public sealed class SqlQueryDocumentStrategy : IDocumentStrategy
			{
				#region Constructors/Destructors

				private SqlQueryDocumentStrategy()
				{
				}

				#endregion

				#region Fields/Constants

				private static readonly IDocumentStrategy instance = new SqlQueryDocumentStrategy();

				#endregion

				#region Properties/Indexers/Events

				public static IDocumentStrategy Instance
				{
					get
					{
						return instance;
					}
				}

				public string DisplayText
				{
					get
					{
						return "SQL Query";
					}
				}

				#endregion

				#region Methods/Operators

				public object LoadDocument(string filePath)
				{
					if (DataType.IsNullOrWhiteSpace(filePath))
						return new SqlQuery();
					else
						return new ToolHost().LoadSqlQueryOnly(filePath);
				}

				public void SaveDocument(object document, string filePath)
				{
					new ToolHost().SaveSqlQueryOnly((SqlQuery)document, filePath);
				}

				public object UpdateDocumentProps(object document)
				{
					throw new NotImplementedException();
				}

				public void UpdateDocumentTree(object document, TreeView tvDocument)
				{
					throw new NotImplementedException();
				}

				public Message[] ValidateDocument(object document)
				{
					return new Message[] { };
				}

				#endregion
			}

			public sealed class TemplateDocumentStrategy : IDocumentStrategy
			{
				#region Constructors/Destructors

				private TemplateDocumentStrategy()
				{
				}

				#endregion

				#region Fields/Constants

				private static readonly IDocumentStrategy instance = new TemplateDocumentStrategy();

				#endregion

				#region Properties/Indexers/Events

				public static IDocumentStrategy Instance
				{
					get
					{
						return instance;
					}
				}

				public string DisplayText
				{
					get
					{
						return "Template";
					}
				}

				#endregion

				#region Methods/Operators

				public object LoadDocument(string filePath)
				{
					if (DataType.IsNullOrWhiteSpace(filePath))
						return new TemplateConstruct();
					else
						return new ToolHost().LoadTemplateOnly(filePath);
				}

				public void SaveDocument(object document, string filePath)
				{
					new ToolHost().SaveTemplateOnly((TemplateConstruct)document, filePath);
				}

				public object UpdateDocumentProps(object document)
				{
					throw new NotImplementedException();
				}

				public void UpdateDocumentTree(object document, TreeView tvDocument)
				{
					throw new NotImplementedException();
				}

				public Message[] ValidateDocument(object document)
				{
					return new Message[] { };
				}

				#endregion
			}

			#endregion
		}

		#endregion
	}
}