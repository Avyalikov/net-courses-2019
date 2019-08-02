﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LINQ_SQL
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Northwind")]
	public partial class NorthwindDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertEmployee(Employee instance);
    partial void UpdateEmployee(Employee instance);
    partial void DeleteEmployee(Employee instance);
    partial void InsertOrder(Order instance);
    partial void UpdateOrder(Order instance);
    partial void DeleteOrder(Order instance);
    #endregion
		
		public NorthwindDataContext() : 
				base(global::LINQ_SQL.Properties.Settings.Default.NorthwindConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public NorthwindDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public NorthwindDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public NorthwindDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public NorthwindDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Employee> Employees
		{
			get
			{
				return this.GetTable<Employee>();
			}
		}
		
		public System.Data.Linq.Table<Order> Orders
		{
			get
			{
				return this.GetTable<Order>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Employees")]
	public partial class Employee : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _EmployeeID;
		
		private string _LastName;
		
		private string _FirstName;
		
		private string _Title;
		
		private string _TitleOfCourtesy;
		
		private System.Nullable<System.DateTime> _BirthDate;
		
		private System.Nullable<System.DateTime> _HireDate;
		
		private string _Address;
		
		private string _City;
		
		private string _Region;
		
		private string _PostalCode;
		
		private string _Country;
		
		private string _HomePhone;
		
		private string _Extension;
		
		private System.Data.Linq.Binary _Photo;
		
		private string _Notes;
		
		private System.Nullable<int> _ReportsTo;
		
		private string _PhotoPath;
		
		private EntitySet<Employee> _Employees;
		
		private EntitySet<Order> _Orders;
		
		private EntityRef<Employee> _Employee1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnEmployeeIDChanging(int value);
    partial void OnEmployeeIDChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnTitleOfCourtesyChanging(string value);
    partial void OnTitleOfCourtesyChanged();
    partial void OnBirthDateChanging(System.Nullable<System.DateTime> value);
    partial void OnBirthDateChanged();
    partial void OnHireDateChanging(System.Nullable<System.DateTime> value);
    partial void OnHireDateChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnRegionChanging(string value);
    partial void OnRegionChanged();
    partial void OnPostalCodeChanging(string value);
    partial void OnPostalCodeChanged();
    partial void OnCountryChanging(string value);
    partial void OnCountryChanged();
    partial void OnHomePhoneChanging(string value);
    partial void OnHomePhoneChanged();
    partial void OnExtensionChanging(string value);
    partial void OnExtensionChanged();
    partial void OnPhotoChanging(System.Data.Linq.Binary value);
    partial void OnPhotoChanged();
    partial void OnNotesChanging(string value);
    partial void OnNotesChanged();
    partial void OnReportsToChanging(System.Nullable<int> value);
    partial void OnReportsToChanged();
    partial void OnPhotoPathChanging(string value);
    partial void OnPhotoPathChanged();
    #endregion
		
		public Employee()
		{
			this._Employees = new EntitySet<Employee>(new Action<Employee>(this.attach_Employees), new Action<Employee>(this.detach_Employees));
			this._Orders = new EntitySet<Order>(new Action<Order>(this.attach_Orders), new Action<Order>(this.detach_Orders));
			this._Employee1 = default(EntityRef<Employee>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int EmployeeID
		{
			get
			{
				return this._EmployeeID;
			}
			set
			{
				if ((this._EmployeeID != value))
				{
					this.OnEmployeeIDChanging(value);
					this.SendPropertyChanging();
					this._EmployeeID = value;
					this.SendPropertyChanged("EmployeeID");
					this.OnEmployeeIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="NVarChar(10) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(30)")]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TitleOfCourtesy", DbType="NVarChar(25)")]
		public string TitleOfCourtesy
		{
			get
			{
				return this._TitleOfCourtesy;
			}
			set
			{
				if ((this._TitleOfCourtesy != value))
				{
					this.OnTitleOfCourtesyChanging(value);
					this.SendPropertyChanging();
					this._TitleOfCourtesy = value;
					this.SendPropertyChanged("TitleOfCourtesy");
					this.OnTitleOfCourtesyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BirthDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> BirthDate
		{
			get
			{
				return this._BirthDate;
			}
			set
			{
				if ((this._BirthDate != value))
				{
					this.OnBirthDateChanging(value);
					this.SendPropertyChanging();
					this._BirthDate = value;
					this.SendPropertyChanged("BirthDate");
					this.OnBirthDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HireDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> HireDate
		{
			get
			{
				return this._HireDate;
			}
			set
			{
				if ((this._HireDate != value))
				{
					this.OnHireDateChanging(value);
					this.SendPropertyChanging();
					this._HireDate = value;
					this.SendPropertyChanged("HireDate");
					this.OnHireDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="NVarChar(60)")]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="NVarChar(15)")]
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Region", DbType="NVarChar(15)")]
		public string Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				if ((this._Region != value))
				{
					this.OnRegionChanging(value);
					this.SendPropertyChanging();
					this._Region = value;
					this.SendPropertyChanged("Region");
					this.OnRegionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PostalCode", DbType="NVarChar(10)")]
		public string PostalCode
		{
			get
			{
				return this._PostalCode;
			}
			set
			{
				if ((this._PostalCode != value))
				{
					this.OnPostalCodeChanging(value);
					this.SendPropertyChanging();
					this._PostalCode = value;
					this.SendPropertyChanged("PostalCode");
					this.OnPostalCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Country", DbType="NVarChar(15)")]
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				if ((this._Country != value))
				{
					this.OnCountryChanging(value);
					this.SendPropertyChanging();
					this._Country = value;
					this.SendPropertyChanged("Country");
					this.OnCountryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HomePhone", DbType="NVarChar(24)")]
		public string HomePhone
		{
			get
			{
				return this._HomePhone;
			}
			set
			{
				if ((this._HomePhone != value))
				{
					this.OnHomePhoneChanging(value);
					this.SendPropertyChanging();
					this._HomePhone = value;
					this.SendPropertyChanged("HomePhone");
					this.OnHomePhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Extension", DbType="NVarChar(4)")]
		public string Extension
		{
			get
			{
				return this._Extension;
			}
			set
			{
				if ((this._Extension != value))
				{
					this.OnExtensionChanging(value);
					this.SendPropertyChanging();
					this._Extension = value;
					this.SendPropertyChanged("Extension");
					this.OnExtensionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Photo", DbType="Image", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Photo
		{
			get
			{
				return this._Photo;
			}
			set
			{
				if ((this._Photo != value))
				{
					this.OnPhotoChanging(value);
					this.SendPropertyChanging();
					this._Photo = value;
					this.SendPropertyChanged("Photo");
					this.OnPhotoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Notes", DbType="NText", UpdateCheck=UpdateCheck.Never)]
		public string Notes
		{
			get
			{
				return this._Notes;
			}
			set
			{
				if ((this._Notes != value))
				{
					this.OnNotesChanging(value);
					this.SendPropertyChanging();
					this._Notes = value;
					this.SendPropertyChanged("Notes");
					this.OnNotesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReportsTo", DbType="Int")]
		public System.Nullable<int> ReportsTo
		{
			get
			{
				return this._ReportsTo;
			}
			set
			{
				if ((this._ReportsTo != value))
				{
					if (this._Employee1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnReportsToChanging(value);
					this.SendPropertyChanging();
					this._ReportsTo = value;
					this.SendPropertyChanged("ReportsTo");
					this.OnReportsToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhotoPath", DbType="NVarChar(255)")]
		public string PhotoPath
		{
			get
			{
				return this._PhotoPath;
			}
			set
			{
				if ((this._PhotoPath != value))
				{
					this.OnPhotoPathChanging(value);
					this.SendPropertyChanging();
					this._PhotoPath = value;
					this.SendPropertyChanged("PhotoPath");
					this.OnPhotoPathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Employee_Employee", Storage="_Employees", ThisKey="EmployeeID", OtherKey="ReportsTo")]
		public EntitySet<Employee> Employees
		{
			get
			{
				return this._Employees;
			}
			set
			{
				this._Employees.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Employee_Order", Storage="_Orders", ThisKey="EmployeeID", OtherKey="EmployeeID")]
		public EntitySet<Order> Orders
		{
			get
			{
				return this._Orders;
			}
			set
			{
				this._Orders.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Employee_Employee", Storage="_Employee1", ThisKey="ReportsTo", OtherKey="EmployeeID", IsForeignKey=true)]
		public Employee Employee1
		{
			get
			{
				return this._Employee1.Entity;
			}
			set
			{
				Employee previousValue = this._Employee1.Entity;
				if (((previousValue != value) 
							|| (this._Employee1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Employee1.Entity = null;
						previousValue.Employees.Remove(this);
					}
					this._Employee1.Entity = value;
					if ((value != null))
					{
						value.Employees.Add(this);
						this._ReportsTo = value.EmployeeID;
					}
					else
					{
						this._ReportsTo = default(Nullable<int>);
					}
					this.SendPropertyChanged("Employee1");
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
		
		private void attach_Employees(Employee entity)
		{
			this.SendPropertyChanging();
			entity.Employee1 = this;
		}
		
		private void detach_Employees(Employee entity)
		{
			this.SendPropertyChanging();
			entity.Employee1 = null;
		}
		
		private void attach_Orders(Order entity)
		{
			this.SendPropertyChanging();
			entity.Employee = this;
		}
		
		private void detach_Orders(Order entity)
		{
			this.SendPropertyChanging();
			entity.Employee = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Orders")]
	public partial class Order : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _OrderID;
		
		private string _CustomerID;
		
		private System.Nullable<int> _EmployeeID;
		
		private System.Nullable<System.DateTime> _OrderDate;
		
		private System.Nullable<System.DateTime> _RequiredDate;
		
		private System.Nullable<System.DateTime> _ShippedDate;
		
		private System.Nullable<int> _ShipVia;
		
		private System.Nullable<decimal> _Freight;
		
		private string _ShipName;
		
		private string _ShipAddress;
		
		private string _ShipCity;
		
		private string _ShipRegion;
		
		private string _ShipPostalCode;
		
		private string _ShipCountry;
		
		private EntityRef<Employee> _Employee;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOrderIDChanging(int value);
    partial void OnOrderIDChanged();
    partial void OnCustomerIDChanging(string value);
    partial void OnCustomerIDChanged();
    partial void OnEmployeeIDChanging(System.Nullable<int> value);
    partial void OnEmployeeIDChanged();
    partial void OnOrderDateChanging(System.Nullable<System.DateTime> value);
    partial void OnOrderDateChanged();
    partial void OnRequiredDateChanging(System.Nullable<System.DateTime> value);
    partial void OnRequiredDateChanged();
    partial void OnShippedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnShippedDateChanged();
    partial void OnShipViaChanging(System.Nullable<int> value);
    partial void OnShipViaChanged();
    partial void OnFreightChanging(System.Nullable<decimal> value);
    partial void OnFreightChanged();
    partial void OnShipNameChanging(string value);
    partial void OnShipNameChanged();
    partial void OnShipAddressChanging(string value);
    partial void OnShipAddressChanged();
    partial void OnShipCityChanging(string value);
    partial void OnShipCityChanged();
    partial void OnShipRegionChanging(string value);
    partial void OnShipRegionChanged();
    partial void OnShipPostalCodeChanging(string value);
    partial void OnShipPostalCodeChanged();
    partial void OnShipCountryChanging(string value);
    partial void OnShipCountryChanged();
    #endregion
		
		public Order()
		{
			this._Employee = default(EntityRef<Employee>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int OrderID
		{
			get
			{
				return this._OrderID;
			}
			set
			{
				if ((this._OrderID != value))
				{
					this.OnOrderIDChanging(value);
					this.SendPropertyChanging();
					this._OrderID = value;
					this.SendPropertyChanged("OrderID");
					this.OnOrderIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerID", DbType="NChar(5)")]
		public string CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				if ((this._CustomerID != value))
				{
					this.OnCustomerIDChanging(value);
					this.SendPropertyChanging();
					this._CustomerID = value;
					this.SendPropertyChanged("CustomerID");
					this.OnCustomerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmployeeID", DbType="Int")]
		public System.Nullable<int> EmployeeID
		{
			get
			{
				return this._EmployeeID;
			}
			set
			{
				if ((this._EmployeeID != value))
				{
					if (this._Employee.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnEmployeeIDChanging(value);
					this.SendPropertyChanging();
					this._EmployeeID = value;
					this.SendPropertyChanged("EmployeeID");
					this.OnEmployeeIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> OrderDate
		{
			get
			{
				return this._OrderDate;
			}
			set
			{
				if ((this._OrderDate != value))
				{
					this.OnOrderDateChanging(value);
					this.SendPropertyChanging();
					this._OrderDate = value;
					this.SendPropertyChanged("OrderDate");
					this.OnOrderDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequiredDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> RequiredDate
		{
			get
			{
				return this._RequiredDate;
			}
			set
			{
				if ((this._RequiredDate != value))
				{
					this.OnRequiredDateChanging(value);
					this.SendPropertyChanging();
					this._RequiredDate = value;
					this.SendPropertyChanged("RequiredDate");
					this.OnRequiredDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShippedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> ShippedDate
		{
			get
			{
				return this._ShippedDate;
			}
			set
			{
				if ((this._ShippedDate != value))
				{
					this.OnShippedDateChanging(value);
					this.SendPropertyChanging();
					this._ShippedDate = value;
					this.SendPropertyChanged("ShippedDate");
					this.OnShippedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipVia", DbType="Int")]
		public System.Nullable<int> ShipVia
		{
			get
			{
				return this._ShipVia;
			}
			set
			{
				if ((this._ShipVia != value))
				{
					this.OnShipViaChanging(value);
					this.SendPropertyChanging();
					this._ShipVia = value;
					this.SendPropertyChanged("ShipVia");
					this.OnShipViaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Freight", DbType="Money")]
		public System.Nullable<decimal> Freight
		{
			get
			{
				return this._Freight;
			}
			set
			{
				if ((this._Freight != value))
				{
					this.OnFreightChanging(value);
					this.SendPropertyChanging();
					this._Freight = value;
					this.SendPropertyChanged("Freight");
					this.OnFreightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipName", DbType="NVarChar(40)")]
		public string ShipName
		{
			get
			{
				return this._ShipName;
			}
			set
			{
				if ((this._ShipName != value))
				{
					this.OnShipNameChanging(value);
					this.SendPropertyChanging();
					this._ShipName = value;
					this.SendPropertyChanged("ShipName");
					this.OnShipNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipAddress", DbType="NVarChar(60)")]
		public string ShipAddress
		{
			get
			{
				return this._ShipAddress;
			}
			set
			{
				if ((this._ShipAddress != value))
				{
					this.OnShipAddressChanging(value);
					this.SendPropertyChanging();
					this._ShipAddress = value;
					this.SendPropertyChanged("ShipAddress");
					this.OnShipAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipCity", DbType="NVarChar(15)")]
		public string ShipCity
		{
			get
			{
				return this._ShipCity;
			}
			set
			{
				if ((this._ShipCity != value))
				{
					this.OnShipCityChanging(value);
					this.SendPropertyChanging();
					this._ShipCity = value;
					this.SendPropertyChanged("ShipCity");
					this.OnShipCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipRegion", DbType="NVarChar(15)")]
		public string ShipRegion
		{
			get
			{
				return this._ShipRegion;
			}
			set
			{
				if ((this._ShipRegion != value))
				{
					this.OnShipRegionChanging(value);
					this.SendPropertyChanging();
					this._ShipRegion = value;
					this.SendPropertyChanged("ShipRegion");
					this.OnShipRegionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipPostalCode", DbType="NVarChar(10)")]
		public string ShipPostalCode
		{
			get
			{
				return this._ShipPostalCode;
			}
			set
			{
				if ((this._ShipPostalCode != value))
				{
					this.OnShipPostalCodeChanging(value);
					this.SendPropertyChanging();
					this._ShipPostalCode = value;
					this.SendPropertyChanged("ShipPostalCode");
					this.OnShipPostalCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShipCountry", DbType="NVarChar(15)")]
		public string ShipCountry
		{
			get
			{
				return this._ShipCountry;
			}
			set
			{
				if ((this._ShipCountry != value))
				{
					this.OnShipCountryChanging(value);
					this.SendPropertyChanging();
					this._ShipCountry = value;
					this.SendPropertyChanged("ShipCountry");
					this.OnShipCountryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Employee_Order", Storage="_Employee", ThisKey="EmployeeID", OtherKey="EmployeeID", IsForeignKey=true)]
		public Employee Employee
		{
			get
			{
				return this._Employee.Entity;
			}
			set
			{
				Employee previousValue = this._Employee.Entity;
				if (((previousValue != value) 
							|| (this._Employee.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Employee.Entity = null;
						previousValue.Orders.Remove(this);
					}
					this._Employee.Entity = value;
					if ((value != null))
					{
						value.Orders.Add(this);
						this._EmployeeID = value.EmployeeID;
					}
					else
					{
						this._EmployeeID = default(Nullable<int>);
					}
					this.SendPropertyChanged("Employee");
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
}
#pragma warning restore 1591
