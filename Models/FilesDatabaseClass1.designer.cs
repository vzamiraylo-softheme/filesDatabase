﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace filesDatabase.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="filesDatabase")]
	public partial class FilesDatabaseClass1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCategory(Category instance);
    partial void UpdateCategory(Category instance);
    partial void DeleteCategory(Category instance);
    partial void InsertFileToCategory(FileToCategory instance);
    partial void UpdateFileToCategory(FileToCategory instance);
    partial void DeleteFileToCategory(FileToCategory instance);
    partial void InsertfilesTable(filesTable instance);
    partial void UpdatefilesTable(filesTable instance);
    partial void DeletefilesTable(filesTable instance);
    #endregion
		
		public FilesDatabaseClass1DataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["filesDatabaseConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDatabaseClass1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDatabaseClass1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDatabaseClass1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FilesDatabaseClass1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Category> Categories
		{
			get
			{
				return this.GetTable<Category>();
			}
		}
		
		public System.Data.Linq.Table<FileToCategory> FileToCategories
		{
			get
			{
				return this.GetTable<FileToCategory>();
			}
		}
		
		public System.Data.Linq.Table<filesTable> filesTables
		{
			get
			{
				return this.GetTable<filesTable>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Categories")]
	public partial class Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _CatName;
		
		private EntitySet<FileToCategory> _FileToCategories;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCatNameChanging(string value);
    partial void OnCatNameChanged();
    #endregion
		
		public Category()
		{
			this._FileToCategories = new EntitySet<FileToCategory>(new Action<FileToCategory>(this.attach_FileToCategories), new Action<FileToCategory>(this.detach_FileToCategories));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CatName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string CatName
		{
			get
			{
				return this._CatName;
			}
			set
			{
				if ((this._CatName != value))
				{
					this.OnCatNameChanging(value);
					this.SendPropertyChanging();
					this._CatName = value;
					this.SendPropertyChanged("CatName");
					this.OnCatNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_FileToCategory", Storage="_FileToCategories", ThisKey="Id", OtherKey="CatId")]
		public EntitySet<FileToCategory> FileToCategories
		{
			get
			{
				return this._FileToCategories;
			}
			set
			{
				this._FileToCategories.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_FileToCategories(FileToCategory entity)
		{
			this.SendPropertyChanging();
			entity.Category = this;
		}
		
		private void detach_FileToCategories(FileToCategory entity)
		{
			this.SendPropertyChanging();
			entity.Category = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.FileToCategory")]
	public partial class FileToCategory : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _FileId;
		
		private int _CatId;
		
		private EntityRef<Category> _Category;
		
		private EntityRef<filesTable> _filesTable;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFileIdChanging(int value);
    partial void OnFileIdChanged();
    partial void OnCatIdChanging(int value);
    partial void OnCatIdChanged();
    #endregion
		
		public FileToCategory()
		{
			this._Category = default(EntityRef<Category>);
			this._filesTable = default(EntityRef<filesTable>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int FileId
		{
			get
			{
				return this._FileId;
			}
			set
			{
				if ((this._FileId != value))
				{
					if (this._filesTable.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFileIdChanging(value);
					this.SendPropertyChanging();
					this._FileId = value;
					this.SendPropertyChanged("FileId");
					this.OnFileIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CatId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CatId
		{
			get
			{
				return this._CatId;
			}
			set
			{
				if ((this._CatId != value))
				{
					if (this._Category.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCatIdChanging(value);
					this.SendPropertyChanging();
					this._CatId = value;
					this.SendPropertyChanged("CatId");
					this.OnCatIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_FileToCategory", Storage="_Category", ThisKey="CatId", OtherKey="Id", IsForeignKey=true)]
		public Category Category
		{
			get
			{
				return this._Category.Entity;
			}
			set
			{
				Category previousValue = this._Category.Entity;
				if (((previousValue != value) 
							|| (this._Category.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Category.Entity = null;
						previousValue.FileToCategories.Remove(this);
					}
					this._Category.Entity = value;
					if ((value != null))
					{
						value.FileToCategories.Add(this);
						this._CatId = value.Id;
					}
					else
					{
						this._CatId = default(int);
					}
					this.SendPropertyChanged("Category");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_FileToCategory", Storage="_filesTable", ThisKey="FileId", OtherKey="id", IsForeignKey=true)]
		public filesTable filesTable
		{
			get
			{
				return this._filesTable.Entity;
			}
			set
			{
				filesTable previousValue = this._filesTable.Entity;
				if (((previousValue != value) 
							|| (this._filesTable.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._filesTable.Entity = null;
						previousValue.FileToCategories.Remove(this);
					}
					this._filesTable.Entity = value;
					if ((value != null))
					{
						value.FileToCategories.Add(this);
						this._FileId = value.id;
					}
					else
					{
						this._FileId = default(int);
					}
					this.SendPropertyChanged("filesTable");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.filesTable")]
	public partial class filesTable : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id;
		
		private string _fileName;
		
		private string _fileDescription;
		
		private string _filePath;
		
		private int _userId;
		
		private string _userName;
		
		private EntitySet<FileToCategory> _FileToCategories;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(int value);
    partial void OnidChanged();
    partial void OnfileNameChanging(string value);
    partial void OnfileNameChanged();
    partial void OnfileDescriptionChanging(string value);
    partial void OnfileDescriptionChanged();
    partial void OnfilePathChanging(string value);
    partial void OnfilePathChanged();
    partial void OnuserIdChanging(int value);
    partial void OnuserIdChanged();
    partial void OnuserNameChanging(string value);
    partial void OnuserNameChanged();
    #endregion
		
		public filesTable()
		{
			this._FileToCategories = new EntitySet<FileToCategory>(new Action<FileToCategory>(this.attach_FileToCategories), new Action<FileToCategory>(this.detach_FileToCategories));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this.OnidChanging(value);
					this.SendPropertyChanging();
					this._id = value;
					this.SendPropertyChanged("id");
					this.OnidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fileName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string fileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				if ((this._fileName != value))
				{
					this.OnfileNameChanging(value);
					this.SendPropertyChanging();
					this._fileName = value;
					this.SendPropertyChanged("fileName");
					this.OnfileNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fileDescription", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string fileDescription
		{
			get
			{
				return this._fileDescription;
			}
			set
			{
				if ((this._fileDescription != value))
				{
					this.OnfileDescriptionChanging(value);
					this.SendPropertyChanging();
					this._fileDescription = value;
					this.SendPropertyChanged("fileDescription");
					this.OnfileDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_filePath", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string filePath
		{
			get
			{
				return this._filePath;
			}
			set
			{
				if ((this._filePath != value))
				{
					this.OnfilePathChanging(value);
					this.SendPropertyChanging();
					this._filePath = value;
					this.SendPropertyChanged("filePath");
					this.OnfilePathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userId", DbType="Int NOT NULL")]
		public int userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				if ((this._userId != value))
				{
					this.OnuserIdChanging(value);
					this.SendPropertyChanging();
					this._userId = value;
					this.SendPropertyChanged("userId");
					this.OnuserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string userName
		{
			get
			{
				return this._userName;
			}
			set
			{
				if ((this._userName != value))
				{
					this.OnuserNameChanging(value);
					this.SendPropertyChanging();
					this._userName = value;
					this.SendPropertyChanged("userName");
					this.OnuserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_FileToCategory", Storage="_FileToCategories", ThisKey="id", OtherKey="FileId")]
		public EntitySet<FileToCategory> FileToCategories
		{
			get
			{
				return this._FileToCategories;
			}
			set
			{
				this._FileToCategories.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_FileToCategories(FileToCategory entity)
		{
			this.SendPropertyChanging();
			entity.filesTable = this;
		}
		
		private void detach_FileToCategories(FileToCategory entity)
		{
			this.SendPropertyChanging();
			entity.filesTable = null;
		}
	}
}
#pragma warning restore 1591
