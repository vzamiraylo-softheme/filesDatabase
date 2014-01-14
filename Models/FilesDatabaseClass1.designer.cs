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
    partial void InsertFileToCategory(FileToCategory instance);
    partial void UpdateFileToCategory(FileToCategory instance);
    partial void DeleteFileToCategory(FileToCategory instance);
    partial void InsertCategory(Category instance);
    partial void UpdateCategory(Category instance);
    partial void DeleteCategory(Category instance);
    partial void InsertsharedContent(sharedContent instance);
    partial void UpdatesharedContent(sharedContent instance);
    partial void DeletesharedContent(sharedContent instance);
    partial void Insertsubscriber(subscriber instance);
    partial void Updatesubscriber(subscriber instance);
    partial void Deletesubscriber(subscriber instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
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
		
		public System.Data.Linq.Table<FileToCategory> FileToCategories
		{
			get
			{
				return this.GetTable<FileToCategory>();
			}
		}
		
		public System.Data.Linq.Table<Category> Categories
		{
			get
			{
				return this.GetTable<Category>();
			}
		}
		
		public System.Data.Linq.Table<sharedContent> sharedContents
		{
			get
			{
				return this.GetTable<sharedContent>();
			}
		}
		
		public System.Data.Linq.Table<subscriber> subscribers
		{
			get
			{
				return this.GetTable<subscriber>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Categories")]
	public partial class Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _CatName;
		
		private string _UserName;
		
		private EntitySet<FileToCategory> _FileToCategories;
		
		private EntitySet<User> _Users;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnCatNameChanging(string value);
    partial void OnCatNameChanged();
    partial void OnUserNameChanging(string value);
    partial void OnUserNameChanged();
    #endregion
		
		public Category()
		{
			this._FileToCategories = new EntitySet<FileToCategory>(new Action<FileToCategory>(this.attach_FileToCategories), new Action<FileToCategory>(this.detach_FileToCategories));
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_User", Storage="_Users", ThisKey="UserName", OtherKey="userName")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
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
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.Category = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.Category = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.sharedContent")]
	public partial class sharedContent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _fileId;
		
		private int _userId;
		
		private EntityRef<User> _User;
		
		private EntityRef<filesTable> _filesTable;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnfileIdChanging(int value);
    partial void OnfileIdChanged();
    partial void OnuserIdChanging(int value);
    partial void OnuserIdChanged();
    #endregion
		
		public sharedContent()
		{
			this._User = default(EntityRef<User>);
			this._filesTable = default(EntityRef<filesTable>);
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fileId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int fileId
		{
			get
			{
				return this._fileId;
			}
			set
			{
				if ((this._fileId != value))
				{
					if (this._filesTable.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnfileIdChanging(value);
					this.SendPropertyChanging();
					this._fileId = value;
					this.SendPropertyChanged("fileId");
					this.OnfileIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userId", DbType="Int NOT NULL", IsPrimaryKey=true)]
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
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnuserIdChanging(value);
					this.SendPropertyChanging();
					this._userId = value;
					this.SendPropertyChanged("userId");
					this.OnuserIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_sharedContent", Storage="_User", ThisKey="userId", OtherKey="userID", IsForeignKey=true)]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.sharedContents.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.sharedContents.Add(this);
						this._userId = value.userID;
					}
					else
					{
						this._userId = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_sharedContent", Storage="_filesTable", ThisKey="fileId", OtherKey="id", IsForeignKey=true)]
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
						previousValue.sharedContents.Remove(this);
					}
					this._filesTable.Entity = value;
					if ((value != null))
					{
						value.sharedContents.Add(this);
						this._fileId = value.id;
					}
					else
					{
						this._fileId = default(int);
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.subscribers")]
	public partial class subscriber : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _subscriberId;
		
		private int _subscribedToId;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnsubscriberIdChanging(int value);
    partial void OnsubscriberIdChanged();
    partial void OnsubscribedToIdChanging(int value);
    partial void OnsubscribedToIdChanged();
    #endregion
		
		public subscriber()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_subscriberId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int subscriberId
		{
			get
			{
				return this._subscriberId;
			}
			set
			{
				if ((this._subscriberId != value))
				{
					this.OnsubscriberIdChanging(value);
					this.SendPropertyChanging();
					this._subscriberId = value;
					this.SendPropertyChanged("subscriberId");
					this.OnsubscriberIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_subscribedToId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int subscribedToId
		{
			get
			{
				return this._subscribedToId;
			}
			set
			{
				if ((this._subscribedToId != value))
				{
					this.OnsubscribedToIdChanging(value);
					this.SendPropertyChanging();
					this._subscribedToId = value;
					this.SendPropertyChanged("subscribedToId");
					this.OnsubscribedToIdChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _userID;
		
		private string _userName;
		
		private string _password;
		
		private string _userEmail;
		
		private string _role;
		
		private System.Nullable<bool> _rememberMe;
		
		private string _avatar;
		
		private System.Nullable<System.DateTime> _lastSeenOn;
		
		private EntitySet<sharedContent> _sharedContents;
		
		private EntityRef<Category> _Category;
		
		private EntityRef<filesTable> _filesTable;
		
		private EntityRef<filesTable> _filesTable1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnuserIDChanging(int value);
    partial void OnuserIDChanged();
    partial void OnuserNameChanging(string value);
    partial void OnuserNameChanged();
    partial void OnpasswordChanging(string value);
    partial void OnpasswordChanged();
    partial void OnuserEmailChanging(string value);
    partial void OnuserEmailChanged();
    partial void OnroleChanging(string value);
    partial void OnroleChanged();
    partial void OnrememberMeChanging(System.Nullable<bool> value);
    partial void OnrememberMeChanged();
    partial void OnavatarChanging(string value);
    partial void OnavatarChanged();
    partial void OnlastSeenOnChanging(System.Nullable<System.DateTime> value);
    partial void OnlastSeenOnChanged();
    #endregion
		
		public User()
		{
			this._sharedContents = new EntitySet<sharedContent>(new Action<sharedContent>(this.attach_sharedContents), new Action<sharedContent>(this.detach_sharedContents));
			this._Category = default(EntityRef<Category>);
			this._filesTable = default(EntityRef<filesTable>);
			this._filesTable1 = default(EntityRef<filesTable>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int userID
		{
			get
			{
				return this._userID;
			}
			set
			{
				if ((this._userID != value))
				{
					if (this._filesTable.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnuserIDChanging(value);
					this.SendPropertyChanging();
					this._userID = value;
					this.SendPropertyChanged("userID");
					this.OnuserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userName", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
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
					if ((this._Category.HasLoadedOrAssignedValue || this._filesTable1.HasLoadedOrAssignedValue))
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnuserNameChanging(value);
					this.SendPropertyChanging();
					this._userName = value;
					this.SendPropertyChanged("userName");
					this.OnuserNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_password", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string password
		{
			get
			{
				return this._password;
			}
			set
			{
				if ((this._password != value))
				{
					this.OnpasswordChanging(value);
					this.SendPropertyChanging();
					this._password = value;
					this.SendPropertyChanged("password");
					this.OnpasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_userEmail", DbType="NVarChar(50)")]
		public string userEmail
		{
			get
			{
				return this._userEmail;
			}
			set
			{
				if ((this._userEmail != value))
				{
					this.OnuserEmailChanging(value);
					this.SendPropertyChanging();
					this._userEmail = value;
					this.SendPropertyChanged("userEmail");
					this.OnuserEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_role", DbType="NVarChar(50)")]
		public string role
		{
			get
			{
				return this._role;
			}
			set
			{
				if ((this._role != value))
				{
					this.OnroleChanging(value);
					this.SendPropertyChanging();
					this._role = value;
					this.SendPropertyChanged("role");
					this.OnroleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rememberMe", DbType="Bit")]
		public System.Nullable<bool> rememberMe
		{
			get
			{
				return this._rememberMe;
			}
			set
			{
				if ((this._rememberMe != value))
				{
					this.OnrememberMeChanging(value);
					this.SendPropertyChanging();
					this._rememberMe = value;
					this.SendPropertyChanged("rememberMe");
					this.OnrememberMeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_avatar", DbType="NVarChar(MAX)")]
		public string avatar
		{
			get
			{
				return this._avatar;
			}
			set
			{
				if ((this._avatar != value))
				{
					this.OnavatarChanging(value);
					this.SendPropertyChanging();
					this._avatar = value;
					this.SendPropertyChanged("avatar");
					this.OnavatarChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lastSeenOn", DbType="DateTime")]
		public System.Nullable<System.DateTime> lastSeenOn
		{
			get
			{
				return this._lastSeenOn;
			}
			set
			{
				if ((this._lastSeenOn != value))
				{
					this.OnlastSeenOnChanging(value);
					this.SendPropertyChanging();
					this._lastSeenOn = value;
					this.SendPropertyChanged("lastSeenOn");
					this.OnlastSeenOnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_sharedContent", Storage="_sharedContents", ThisKey="userID", OtherKey="userId")]
		public EntitySet<sharedContent> sharedContents
		{
			get
			{
				return this._sharedContents;
			}
			set
			{
				this._sharedContents.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_User", Storage="_Category", ThisKey="userName", OtherKey="UserName", IsForeignKey=true)]
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
						previousValue.Users.Remove(this);
					}
					this._Category.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._userName = value.UserName;
					}
					else
					{
						this._userName = default(string);
					}
					this.SendPropertyChanged("Category");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_User", Storage="_filesTable", ThisKey="userID", OtherKey="userId", IsForeignKey=true)]
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
						previousValue.Users.Remove(this);
					}
					this._filesTable.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._userID = value.userId;
					}
					else
					{
						this._userID = default(int);
					}
					this.SendPropertyChanged("filesTable");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_User1", Storage="_filesTable1", ThisKey="userName", OtherKey="userName", IsForeignKey=true)]
		public filesTable filesTable1
		{
			get
			{
				return this._filesTable1.Entity;
			}
			set
			{
				filesTable previousValue = this._filesTable1.Entity;
				if (((previousValue != value) 
							|| (this._filesTable1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._filesTable1.Entity = null;
						previousValue.Users1.Remove(this);
					}
					this._filesTable1.Entity = value;
					if ((value != null))
					{
						value.Users1.Add(this);
						this._userName = value.userName;
					}
					else
					{
						this._userName = default(string);
					}
					this.SendPropertyChanged("filesTable1");
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
		
		private void attach_sharedContents(sharedContent entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_sharedContents(sharedContent entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
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
		
		private System.Nullable<System.DateTime> _uploadTime;
		
		private string _userName;
		
		private EntitySet<FileToCategory> _FileToCategories;
		
		private EntitySet<sharedContent> _sharedContents;
		
		private EntitySet<User> _Users;
		
		private EntitySet<User> _Users1;
		
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
    partial void OnuploadTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnuploadTimeChanged();
    partial void OnuserNameChanging(string value);
    partial void OnuserNameChanged();
    #endregion
		
		public filesTable()
		{
			this._FileToCategories = new EntitySet<FileToCategory>(new Action<FileToCategory>(this.attach_FileToCategories), new Action<FileToCategory>(this.detach_FileToCategories));
			this._sharedContents = new EntitySet<sharedContent>(new Action<sharedContent>(this.attach_sharedContents), new Action<sharedContent>(this.detach_sharedContents));
			this._Users = new EntitySet<User>(new Action<User>(this.attach_Users), new Action<User>(this.detach_Users));
			this._Users1 = new EntitySet<User>(new Action<User>(this.attach_Users1), new Action<User>(this.detach_Users1));
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_uploadTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> uploadTime
		{
			get
			{
				return this._uploadTime;
			}
			set
			{
				if ((this._uploadTime != value))
				{
					this.OnuploadTimeChanging(value);
					this.SendPropertyChanging();
					this._uploadTime = value;
					this.SendPropertyChanged("uploadTime");
					this.OnuploadTimeChanged();
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
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_sharedContent", Storage="_sharedContents", ThisKey="id", OtherKey="fileId")]
		public EntitySet<sharedContent> sharedContents
		{
			get
			{
				return this._sharedContents;
			}
			set
			{
				this._sharedContents.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_User", Storage="_Users", ThisKey="userId", OtherKey="userID")]
		public EntitySet<User> Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				this._Users.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="filesTable_User1", Storage="_Users1", ThisKey="userName", OtherKey="userName")]
		public EntitySet<User> Users1
		{
			get
			{
				return this._Users1;
			}
			set
			{
				this._Users1.Assign(value);
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
		
		private void attach_sharedContents(sharedContent entity)
		{
			this.SendPropertyChanging();
			entity.filesTable = this;
		}
		
		private void detach_sharedContents(sharedContent entity)
		{
			this.SendPropertyChanging();
			entity.filesTable = null;
		}
		
		private void attach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.filesTable = this;
		}
		
		private void detach_Users(User entity)
		{
			this.SendPropertyChanging();
			entity.filesTable = null;
		}
		
		private void attach_Users1(User entity)
		{
			this.SendPropertyChanging();
			entity.filesTable1 = this;
		}
		
		private void detach_Users1(User entity)
		{
			this.SendPropertyChanging();
			entity.filesTable1 = null;
		}
	}
}
#pragma warning restore 1591
