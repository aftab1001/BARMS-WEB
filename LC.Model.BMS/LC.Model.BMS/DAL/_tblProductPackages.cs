
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
	public abstract class _tblProductPackages : SqlClientEntity
	{
		public _tblProductPackages()
		{
			this.QuerySource = "tblProductPackages";
			this.MappingName = "tblProductPackages";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductPackagesLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkProductPackageID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkProductPackageID, PkProductPackageID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductPackagesLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkProductPackageID
			{
				get
				{
					return new SqlParameter("@PkProductPackageID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkProductID
			{
				get
				{
					return new SqlParameter("@FkProductID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter PName
			{
				get
				{
					return new SqlParameter("@PName", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter QName
			{
				get
				{
					return new SqlParameter("@QName", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter PDescription
			{
				get
				{
					return new SqlParameter("@PDescription", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter POrder
			{
				get
				{
					return new SqlParameter("@POrder", SqlDbType.Int, 0);
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
            public const string PkProductPackageID = "pkProductPackageID";
            public const string FkProductID = "fkProductID";
            public const string PName = "pName";
            public const string QName = "qName";
            public const string PDescription = "pDescription";
            public const string POrder = "pOrder";
            public const string IsActive = "isActive";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkProductPackageID] = _tblProductPackages.PropertyNames.PkProductPackageID;
					ht[FkProductID] = _tblProductPackages.PropertyNames.FkProductID;
					ht[PName] = _tblProductPackages.PropertyNames.PName;
					ht[QName] = _tblProductPackages.PropertyNames.QName;
					ht[PDescription] = _tblProductPackages.PropertyNames.PDescription;
					ht[POrder] = _tblProductPackages.PropertyNames.POrder;
					ht[IsActive] = _tblProductPackages.PropertyNames.IsActive;
					ht[DCreatedDate] = _tblProductPackages.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblProductPackages.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkProductPackageID = "PkProductPackageID";
            public const string FkProductID = "FkProductID";
            public const string PName = "PName";
            public const string QName = "QName";
            public const string PDescription = "PDescription";
            public const string POrder = "POrder";
            public const string IsActive = "IsActive";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkProductPackageID] = _tblProductPackages.ColumnNames.PkProductPackageID;
					ht[FkProductID] = _tblProductPackages.ColumnNames.FkProductID;
					ht[PName] = _tblProductPackages.ColumnNames.PName;
					ht[QName] = _tblProductPackages.ColumnNames.QName;
					ht[PDescription] = _tblProductPackages.ColumnNames.PDescription;
					ht[POrder] = _tblProductPackages.ColumnNames.POrder;
					ht[IsActive] = _tblProductPackages.ColumnNames.IsActive;
					ht[DCreatedDate] = _tblProductPackages.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblProductPackages.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkProductPackageID = "s_PkProductPackageID";
            public const string FkProductID = "s_FkProductID";
            public const string PName = "s_PName";
            public const string QName = "s_QName";
            public const string PDescription = "s_PDescription";
            public const string POrder = "s_POrder";
            public const string IsActive = "s_IsActive";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkProductPackageID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkProductPackageID);
			}
			set
	        {
				base.Setint(ColumnNames.PkProductPackageID, value);
			}
		}

		public virtual int FkProductID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkProductID);
			}
			set
	        {
				base.Setint(ColumnNames.FkProductID, value);
			}
		}

		public virtual string PName
	    {
			get
	        {
				return base.Getstring(ColumnNames.PName);
			}
			set
	        {
				base.Setstring(ColumnNames.PName, value);
			}
		}

		public virtual string QName
	    {
			get
	        {
				return base.Getstring(ColumnNames.QName);
			}
			set
	        {
				base.Setstring(ColumnNames.QName, value);
			}
		}

		public virtual string PDescription
	    {
			get
	        {
				return base.Getstring(ColumnNames.PDescription);
			}
			set
	        {
				base.Setstring(ColumnNames.PDescription, value);
			}
		}

		public virtual int POrder
	    {
			get
	        {
				return base.Getint(ColumnNames.POrder);
			}
			set
	        {
				base.Setint(ColumnNames.POrder, value);
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
	
		public virtual string s_PkProductPackageID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkProductPackageID) ? string.Empty : base.GetintAsString(ColumnNames.PkProductPackageID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkProductPackageID);
				else
					this.PkProductPackageID = base.SetintAsString(ColumnNames.PkProductPackageID, value);
			}
		}

		public virtual string s_FkProductID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkProductID) ? string.Empty : base.GetintAsString(ColumnNames.FkProductID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkProductID);
				else
					this.FkProductID = base.SetintAsString(ColumnNames.FkProductID, value);
			}
		}

		public virtual string s_PName
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PName) ? string.Empty : base.GetstringAsString(ColumnNames.PName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PName);
				else
					this.PName = base.SetstringAsString(ColumnNames.PName, value);
			}
		}

		public virtual string s_QName
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.QName) ? string.Empty : base.GetstringAsString(ColumnNames.QName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.QName);
				else
					this.QName = base.SetstringAsString(ColumnNames.QName, value);
			}
		}

		public virtual string s_PDescription
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PDescription) ? string.Empty : base.GetstringAsString(ColumnNames.PDescription);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PDescription);
				else
					this.PDescription = base.SetstringAsString(ColumnNames.PDescription, value);
			}
		}

		public virtual string s_POrder
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.POrder) ? string.Empty : base.GetintAsString(ColumnNames.POrder);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.POrder);
				else
					this.POrder = base.SetintAsString(ColumnNames.POrder, value);
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
				
				
				public WhereParameter PkProductPackageID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkProductPackageID, Parameters.PkProductPackageID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkProductID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkProductID, Parameters.FkProductID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter PName
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PName, Parameters.PName);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter QName
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.QName, Parameters.QName);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter PDescription
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PDescription, Parameters.PDescription);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter POrder
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.POrder, Parameters.POrder);
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
		
			public WhereParameter PkProductPackageID
		    {
				get
		        {
					if(_PkProductPackageID_W == null)
	        	    {
						_PkProductPackageID_W = TearOff.PkProductPackageID;
					}
					return _PkProductPackageID_W;
				}
			}

			public WhereParameter FkProductID
		    {
				get
		        {
					if(_FkProductID_W == null)
	        	    {
						_FkProductID_W = TearOff.FkProductID;
					}
					return _FkProductID_W;
				}
			}

			public WhereParameter PName
		    {
				get
		        {
					if(_PName_W == null)
	        	    {
						_PName_W = TearOff.PName;
					}
					return _PName_W;
				}
			}

			public WhereParameter QName
		    {
				get
		        {
					if(_QName_W == null)
	        	    {
						_QName_W = TearOff.QName;
					}
					return _QName_W;
				}
			}

			public WhereParameter PDescription
		    {
				get
		        {
					if(_PDescription_W == null)
	        	    {
						_PDescription_W = TearOff.PDescription;
					}
					return _PDescription_W;
				}
			}

			public WhereParameter POrder
		    {
				get
		        {
					if(_POrder_W == null)
	        	    {
						_POrder_W = TearOff.POrder;
					}
					return _POrder_W;
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

			private WhereParameter _PkProductPackageID_W = null;
			private WhereParameter _FkProductID_W = null;
			private WhereParameter _PName_W = null;
			private WhereParameter _QName_W = null;
			private WhereParameter _PDescription_W = null;
			private WhereParameter _POrder_W = null;
			private WhereParameter _IsActive_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkProductPackageID_W = null;
				_FkProductID_W = null;
				_PName_W = null;
				_QName_W = null;
				_PDescription_W = null;
				_POrder_W = null;
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
				
				
				public AggregateParameter PkProductPackageID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkProductPackageID, Parameters.PkProductPackageID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkProductID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkProductID, Parameters.FkProductID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter PName
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PName, Parameters.PName);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter QName
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.QName, Parameters.QName);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter PDescription
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PDescription, Parameters.PDescription);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter POrder
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.POrder, Parameters.POrder);
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
		
			public AggregateParameter PkProductPackageID
		    {
				get
		        {
					if(_PkProductPackageID_W == null)
	        	    {
						_PkProductPackageID_W = TearOff.PkProductPackageID;
					}
					return _PkProductPackageID_W;
				}
			}

			public AggregateParameter FkProductID
		    {
				get
		        {
					if(_FkProductID_W == null)
	        	    {
						_FkProductID_W = TearOff.FkProductID;
					}
					return _FkProductID_W;
				}
			}

			public AggregateParameter PName
		    {
				get
		        {
					if(_PName_W == null)
	        	    {
						_PName_W = TearOff.PName;
					}
					return _PName_W;
				}
			}

			public AggregateParameter QName
		    {
				get
		        {
					if(_QName_W == null)
	        	    {
						_QName_W = TearOff.QName;
					}
					return _QName_W;
				}
			}

			public AggregateParameter PDescription
		    {
				get
		        {
					if(_PDescription_W == null)
	        	    {
						_PDescription_W = TearOff.PDescription;
					}
					return _PDescription_W;
				}
			}

			public AggregateParameter POrder
		    {
				get
		        {
					if(_POrder_W == null)
	        	    {
						_POrder_W = TearOff.POrder;
					}
					return _POrder_W;
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

			private AggregateParameter _PkProductPackageID_W = null;
			private AggregateParameter _FkProductID_W = null;
			private AggregateParameter _PName_W = null;
			private AggregateParameter _QName_W = null;
			private AggregateParameter _PDescription_W = null;
			private AggregateParameter _POrder_W = null;
			private AggregateParameter _IsActive_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkProductPackageID_W = null;
				_FkProductID_W = null;
				_PName_W = null;
				_QName_W = null;
				_PDescription_W = null;
				_POrder_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductPackagesInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkProductPackageID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductPackagesUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductPackagesDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkProductPackageID);
			p.SourceColumn = ColumnNames.PkProductPackageID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkProductPackageID);
			p.SourceColumn = ColumnNames.PkProductPackageID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductID);
			p.SourceColumn = ColumnNames.FkProductID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.PName);
			p.SourceColumn = ColumnNames.PName;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.QName);
			p.SourceColumn = ColumnNames.QName;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.PDescription);
			p.SourceColumn = ColumnNames.PDescription;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.POrder);
			p.SourceColumn = ColumnNames.POrder;
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