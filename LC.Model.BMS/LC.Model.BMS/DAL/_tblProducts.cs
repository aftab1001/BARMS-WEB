
/*
'===============================================================================
'  Generated From - CSharp_dOOdads_BusinessEntity.vbgen
' 
'  ** IMPORTANT  ** 
'  How to Generate your stored procedures:
' 
'  SQL        = SQL_StoredProcs.vbgen
'  ACCESS     = Access_StoredProcs.vbgen
'  ORACLE     = Oracle_StoredProcs.vbgen
'  FIREBIRD   = FirebirdStoredProcs.vbgen
'  POSTGRESQL = PostgreSQL_StoredProcs.vbgen
'
'  The supporting base class SqlClientEntity is in the Architecture directory in "dOOdads".
'  
'  This object is 'abstract' which means you need to inherit from it to be able
'  to instantiate it.  This is very easilly done. You can override properties and
'  methods in your derived class, this allows you to regenerate this class at any
'  time and not worry about overwriting custom code. 
'
'  NEVER EDIT THIS FILE.
'
'  public class YourObject :  _YourObject
'  {
'
'  }
'
'===============================================================================
*/

// Generated by MyGeneration Version # (1.1.7.1)

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;

using MyGeneration.dOOdads;

namespace LC.Model.BMS.DAL
{
	public abstract class _tblProducts : SqlClientEntity
	{
		public _tblProducts()
		{
			this.QuerySource = "tblProducts";
			this.MappingName = "tblProducts";

		}	

		//=================================================================
		//  public Overrides void AddNew()
		//=================================================================
		//
		//=================================================================
		public override void AddNew()
		{
			base.AddNew();
			
		}
		
		
		public override void FlushData()
		{
			this._whereClause = null;
			this._aggregateClause = null;
			base.FlushData();
		}
		
		//=================================================================
		//  	public Function LoadAll() As Boolean
		//=================================================================
		//  Loads all of the records in the database, and sets the currentRow to the first row
		//=================================================================
		public bool LoadAll() 
		{
			ListDictionary parameters = null;
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkProductID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkProductID, PkProductID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkProductID
			{
				get
				{
					return new SqlParameter("@PkProductID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkSubCategoryID
			{
				get
				{
					return new SqlParameter("@FkSubCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter SProductName
			{
				get
				{
					return new SqlParameter("@SProductName", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter FkProductPackageID
			{
				get
				{
					return new SqlParameter("@FkProductPackageID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkProductQuantityID
			{
				get
				{
					return new SqlParameter("@FkProductQuantityID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkVatid
			{
				get
				{
					return new SqlParameter("@FkVatid", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter IsActive
			{
				get
				{
					return new SqlParameter("@IsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter DCreatedDate
			{
				get
				{
					return new SqlParameter("@DCreatedDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter DModifiedDate
			{
				get
				{
					return new SqlParameter("@DModifiedDate", SqlDbType.DateTime, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkProductID = "pkProductID";
            public const string FkSubCategoryID = "fkSubCategoryID";
            public const string SProductName = "sProductName";
            public const string FkProductPackageID = "fkProductPackageID";
            public const string FkProductQuantityID = "fkProductQuantityID";
            public const string FkVatid = "fkVatid";
            public const string IsActive = "isActive";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkProductID] = _tblProducts.PropertyNames.PkProductID;
					ht[FkSubCategoryID] = _tblProducts.PropertyNames.FkSubCategoryID;
					ht[SProductName] = _tblProducts.PropertyNames.SProductName;
					ht[FkProductPackageID] = _tblProducts.PropertyNames.FkProductPackageID;
					ht[FkProductQuantityID] = _tblProducts.PropertyNames.FkProductQuantityID;
					ht[FkVatid] = _tblProducts.PropertyNames.FkVatid;
					ht[IsActive] = _tblProducts.PropertyNames.IsActive;
					ht[DCreatedDate] = _tblProducts.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblProducts.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkProductID = "PkProductID";
            public const string FkSubCategoryID = "FkSubCategoryID";
            public const string SProductName = "SProductName";
            public const string FkProductPackageID = "FkProductPackageID";
            public const string FkProductQuantityID = "FkProductQuantityID";
            public const string FkVatid = "FkVatid";
            public const string IsActive = "IsActive";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkProductID] = _tblProducts.ColumnNames.PkProductID;
					ht[FkSubCategoryID] = _tblProducts.ColumnNames.FkSubCategoryID;
					ht[SProductName] = _tblProducts.ColumnNames.SProductName;
					ht[FkProductPackageID] = _tblProducts.ColumnNames.FkProductPackageID;
					ht[FkProductQuantityID] = _tblProducts.ColumnNames.FkProductQuantityID;
					ht[FkVatid] = _tblProducts.ColumnNames.FkVatid;
					ht[IsActive] = _tblProducts.ColumnNames.IsActive;
					ht[DCreatedDate] = _tblProducts.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblProducts.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkProductID = "s_PkProductID";
            public const string FkSubCategoryID = "s_FkSubCategoryID";
            public const string SProductName = "s_SProductName";
            public const string FkProductPackageID = "s_FkProductPackageID";
            public const string FkProductQuantityID = "s_FkProductQuantityID";
            public const string FkVatid = "s_FkVatid";
            public const string IsActive = "s_IsActive";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkProductID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkProductID);
			}
			set
	        {
				base.Setint(ColumnNames.PkProductID, value);
			}
		}

		public virtual int FkSubCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkSubCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.FkSubCategoryID, value);
			}
		}

		public virtual string SProductName
	    {
			get
	        {
				return base.Getstring(ColumnNames.SProductName);
			}
			set
	        {
				base.Setstring(ColumnNames.SProductName, value);
			}
		}

		public virtual int FkProductPackageID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkProductPackageID);
			}
			set
	        {
				base.Setint(ColumnNames.FkProductPackageID, value);
			}
		}

		public virtual int FkProductQuantityID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkProductQuantityID);
			}
			set
	        {
				base.Setint(ColumnNames.FkProductQuantityID, value);
			}
		}

		public virtual int FkVatid
	    {
			get
	        {
				return base.Getint(ColumnNames.FkVatid);
			}
			set
	        {
				base.Setint(ColumnNames.FkVatid, value);
			}
		}

		public virtual bool IsActive
	    {
			get
	        {
				return base.Getbool(ColumnNames.IsActive);
			}
			set
	        {
				base.Setbool(ColumnNames.IsActive, value);
			}
		}

		public virtual DateTime DCreatedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DCreatedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DCreatedDate, value);
			}
		}

		public virtual DateTime DModifiedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DModifiedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DModifiedDate, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_PkProductID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkProductID) ? string.Empty : base.GetintAsString(ColumnNames.PkProductID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkProductID);
				else
					this.PkProductID = base.SetintAsString(ColumnNames.PkProductID, value);
			}
		}

		public virtual string s_FkSubCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkSubCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.FkSubCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkSubCategoryID);
				else
					this.FkSubCategoryID = base.SetintAsString(ColumnNames.FkSubCategoryID, value);
			}
		}

		public virtual string s_SProductName
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SProductName) ? string.Empty : base.GetstringAsString(ColumnNames.SProductName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SProductName);
				else
					this.SProductName = base.SetstringAsString(ColumnNames.SProductName, value);
			}
		}

		public virtual string s_FkProductPackageID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkProductPackageID) ? string.Empty : base.GetintAsString(ColumnNames.FkProductPackageID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkProductPackageID);
				else
					this.FkProductPackageID = base.SetintAsString(ColumnNames.FkProductPackageID, value);
			}
		}

		public virtual string s_FkProductQuantityID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkProductQuantityID) ? string.Empty : base.GetintAsString(ColumnNames.FkProductQuantityID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkProductQuantityID);
				else
					this.FkProductQuantityID = base.SetintAsString(ColumnNames.FkProductQuantityID, value);
			}
		}

		public virtual string s_FkVatid
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkVatid) ? string.Empty : base.GetintAsString(ColumnNames.FkVatid);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkVatid);
				else
					this.FkVatid = base.SetintAsString(ColumnNames.FkVatid, value);
			}
		}

		public virtual string s_IsActive
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.IsActive) ? string.Empty : base.GetboolAsString(ColumnNames.IsActive);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.IsActive);
				else
					this.IsActive = base.SetboolAsString(ColumnNames.IsActive, value);
			}
		}

		public virtual string s_DCreatedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DCreatedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DCreatedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DCreatedDate);
				else
					this.DCreatedDate = base.SetDateTimeAsString(ColumnNames.DCreatedDate, value);
			}
		}

		public virtual string s_DModifiedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DModifiedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DModifiedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DModifiedDate);
				else
					this.DModifiedDate = base.SetDateTimeAsString(ColumnNames.DModifiedDate, value);
			}
		}


		#endregion		
	
		#region Where Clause
		public class WhereClause
		{
			public WhereClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffWhereParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region WhereParameter TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}
				
				
				public WhereParameter PkProductID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkProductID, Parameters.PkProductID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkSubCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkSubCategoryID, Parameters.FkSubCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SProductName
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SProductName, Parameters.SProductName);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkProductPackageID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkProductPackageID, Parameters.FkProductPackageID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkProductQuantityID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkProductQuantityID, Parameters.FkProductQuantityID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkVatid
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkVatid, Parameters.FkVatid);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter IsActive
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.IsActive, Parameters.IsActive);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter DCreatedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DCreatedDate, Parameters.DCreatedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter DModifiedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DModifiedDate, Parameters.DModifiedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkProductID
		    {
				get
		        {
					if(_PkProductID_W == null)
	        	    {
						_PkProductID_W = TearOff.PkProductID;
					}
					return _PkProductID_W;
				}
			}

			public WhereParameter FkSubCategoryID
		    {
				get
		        {
					if(_FkSubCategoryID_W == null)
	        	    {
						_FkSubCategoryID_W = TearOff.FkSubCategoryID;
					}
					return _FkSubCategoryID_W;
				}
			}

			public WhereParameter SProductName
		    {
				get
		        {
					if(_SProductName_W == null)
	        	    {
						_SProductName_W = TearOff.SProductName;
					}
					return _SProductName_W;
				}
			}

			public WhereParameter FkProductPackageID
		    {
				get
		        {
					if(_FkProductPackageID_W == null)
	        	    {
						_FkProductPackageID_W = TearOff.FkProductPackageID;
					}
					return _FkProductPackageID_W;
				}
			}

			public WhereParameter FkProductQuantityID
		    {
				get
		        {
					if(_FkProductQuantityID_W == null)
	        	    {
						_FkProductQuantityID_W = TearOff.FkProductQuantityID;
					}
					return _FkProductQuantityID_W;
				}
			}

			public WhereParameter FkVatid
		    {
				get
		        {
					if(_FkVatid_W == null)
	        	    {
						_FkVatid_W = TearOff.FkVatid;
					}
					return _FkVatid_W;
				}
			}

			public WhereParameter IsActive
		    {
				get
		        {
					if(_IsActive_W == null)
	        	    {
						_IsActive_W = TearOff.IsActive;
					}
					return _IsActive_W;
				}
			}

			public WhereParameter DCreatedDate
		    {
				get
		        {
					if(_DCreatedDate_W == null)
	        	    {
						_DCreatedDate_W = TearOff.DCreatedDate;
					}
					return _DCreatedDate_W;
				}
			}

			public WhereParameter DModifiedDate
		    {
				get
		        {
					if(_DModifiedDate_W == null)
	        	    {
						_DModifiedDate_W = TearOff.DModifiedDate;
					}
					return _DModifiedDate_W;
				}
			}

			private WhereParameter _PkProductID_W = null;
			private WhereParameter _FkSubCategoryID_W = null;
			private WhereParameter _SProductName_W = null;
			private WhereParameter _FkProductPackageID_W = null;
			private WhereParameter _FkProductQuantityID_W = null;
			private WhereParameter _FkVatid_W = null;
			private WhereParameter _IsActive_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkProductID_W = null;
				_FkSubCategoryID_W = null;
				_SProductName_W = null;
				_FkProductPackageID_W = null;
				_FkProductQuantityID_W = null;
				_FkVatid_W = null;
				_IsActive_W = null;
				_DCreatedDate_W = null;
				_DModifiedDate_W = null;

				this._entity.Query.FlushWhereParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffWhereParameter _tearOff;
			
		}
	
		public WhereClause Where
		{
			get
			{
				if(_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}
		
				return _whereClause;
			}
		}
		
		private WhereClause _whereClause = null;	
		#endregion
	
		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffAggregateParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region AggregateParameter TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}
				
				
				public AggregateParameter PkProductID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkProductID, Parameters.PkProductID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkSubCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkSubCategoryID, Parameters.FkSubCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SProductName
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SProductName, Parameters.SProductName);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkProductPackageID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkProductPackageID, Parameters.FkProductPackageID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkProductQuantityID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkProductQuantityID, Parameters.FkProductQuantityID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkVatid
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkVatid, Parameters.FkVatid);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter IsActive
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.IsActive, Parameters.IsActive);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter DCreatedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DCreatedDate, Parameters.DCreatedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter DModifiedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DModifiedDate, Parameters.DModifiedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkProductID
		    {
				get
		        {
					if(_PkProductID_W == null)
	        	    {
						_PkProductID_W = TearOff.PkProductID;
					}
					return _PkProductID_W;
				}
			}

			public AggregateParameter FkSubCategoryID
		    {
				get
		        {
					if(_FkSubCategoryID_W == null)
	        	    {
						_FkSubCategoryID_W = TearOff.FkSubCategoryID;
					}
					return _FkSubCategoryID_W;
				}
			}

			public AggregateParameter SProductName
		    {
				get
		        {
					if(_SProductName_W == null)
	        	    {
						_SProductName_W = TearOff.SProductName;
					}
					return _SProductName_W;
				}
			}

			public AggregateParameter FkProductPackageID
		    {
				get
		        {
					if(_FkProductPackageID_W == null)
	        	    {
						_FkProductPackageID_W = TearOff.FkProductPackageID;
					}
					return _FkProductPackageID_W;
				}
			}

			public AggregateParameter FkProductQuantityID
		    {
				get
		        {
					if(_FkProductQuantityID_W == null)
	        	    {
						_FkProductQuantityID_W = TearOff.FkProductQuantityID;
					}
					return _FkProductQuantityID_W;
				}
			}

			public AggregateParameter FkVatid
		    {
				get
		        {
					if(_FkVatid_W == null)
	        	    {
						_FkVatid_W = TearOff.FkVatid;
					}
					return _FkVatid_W;
				}
			}

			public AggregateParameter IsActive
		    {
				get
		        {
					if(_IsActive_W == null)
	        	    {
						_IsActive_W = TearOff.IsActive;
					}
					return _IsActive_W;
				}
			}

			public AggregateParameter DCreatedDate
		    {
				get
		        {
					if(_DCreatedDate_W == null)
	        	    {
						_DCreatedDate_W = TearOff.DCreatedDate;
					}
					return _DCreatedDate_W;
				}
			}

			public AggregateParameter DModifiedDate
		    {
				get
		        {
					if(_DModifiedDate_W == null)
	        	    {
						_DModifiedDate_W = TearOff.DModifiedDate;
					}
					return _DModifiedDate_W;
				}
			}

			private AggregateParameter _PkProductID_W = null;
			private AggregateParameter _FkSubCategoryID_W = null;
			private AggregateParameter _SProductName_W = null;
			private AggregateParameter _FkProductPackageID_W = null;
			private AggregateParameter _FkProductQuantityID_W = null;
			private AggregateParameter _FkVatid_W = null;
			private AggregateParameter _IsActive_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkProductID_W = null;
				_FkSubCategoryID_W = null;
				_SProductName_W = null;
				_FkProductPackageID_W = null;
				_FkProductQuantityID_W = null;
				_FkVatid_W = null;
				_IsActive_W = null;
				_DCreatedDate_W = null;
				_DModifiedDate_W = null;

				this._entity.Query.FlushAggregateParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffAggregateParameter _tearOff;
			
		}
	
		public AggregateClause Aggregate
		{
			get
			{
				if(_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}
		
				return _aggregateClause;
			}
		}
		
		private AggregateClause _aggregateClause = null;	
		#endregion
	
		protected override IDbCommand GetInsertCommand() 
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkProductID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkProductID);
			p.SourceColumn = ColumnNames.PkProductID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkProductID);
			p.SourceColumn = ColumnNames.PkProductID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkSubCategoryID);
			p.SourceColumn = ColumnNames.FkSubCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SProductName);
			p.SourceColumn = ColumnNames.SProductName;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductPackageID);
			p.SourceColumn = ColumnNames.FkProductPackageID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductQuantityID);
			p.SourceColumn = ColumnNames.FkProductQuantityID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkVatid);
			p.SourceColumn = ColumnNames.FkVatid;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.IsActive);
			p.SourceColumn = ColumnNames.IsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DCreatedDate);
			p.SourceColumn = ColumnNames.DCreatedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DModifiedDate);
			p.SourceColumn = ColumnNames.DModifiedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}