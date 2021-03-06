
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
	public abstract class _tblProductPackingQuantityRel : SqlClientEntity
	{
		public _tblProductPackingQuantityRel()
		{
			this.QuerySource = "tblProductPackingQuantityRel";
			this.MappingName = "tblProductPackingQuantityRel";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductPackingQuantityRelLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkProductPackingQuantityRelID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkProductPackingQuantityRelID, PkProductPackingQuantityRelID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblProductPackingQuantityRelLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkProductPackingQuantityRelID
			{
				get
				{
					return new SqlParameter("@PkProductPackingQuantityRelID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkProductID
			{
				get
				{
					return new SqlParameter("@FkProductID", SqlDbType.Int, 0);
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
			
			public static SqlParameter FkVatID
			{
				get
				{
					return new SqlParameter("@FkVatID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter IsActive
			{
				get
				{
					return new SqlParameter("@IsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter DModifiedDate
			{
				get
				{
					return new SqlParameter("@DModifiedDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter DCreatedDate
			{
				get
				{
					return new SqlParameter("@DCreatedDate", SqlDbType.DateTime, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkProductPackingQuantityRelID = "pkProductPackingQuantityRelID";
            public const string FkProductID = "fkProductID";
            public const string FkProductPackageID = "fkProductPackageID";
            public const string FkProductQuantityID = "fkProductQuantityID";
            public const string FkVatID = "fkVatID";
            public const string IsActive = "isActive";
            public const string DModifiedDate = "dModifiedDate";
            public const string DCreatedDate = "dCreatedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkProductPackingQuantityRelID] = _tblProductPackingQuantityRel.PropertyNames.PkProductPackingQuantityRelID;
					ht[FkProductID] = _tblProductPackingQuantityRel.PropertyNames.FkProductID;
					ht[FkProductPackageID] = _tblProductPackingQuantityRel.PropertyNames.FkProductPackageID;
					ht[FkProductQuantityID] = _tblProductPackingQuantityRel.PropertyNames.FkProductQuantityID;
					ht[FkVatID] = _tblProductPackingQuantityRel.PropertyNames.FkVatID;
					ht[IsActive] = _tblProductPackingQuantityRel.PropertyNames.IsActive;
					ht[DModifiedDate] = _tblProductPackingQuantityRel.PropertyNames.DModifiedDate;
					ht[DCreatedDate] = _tblProductPackingQuantityRel.PropertyNames.DCreatedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkProductPackingQuantityRelID = "PkProductPackingQuantityRelID";
            public const string FkProductID = "FkProductID";
            public const string FkProductPackageID = "FkProductPackageID";
            public const string FkProductQuantityID = "FkProductQuantityID";
            public const string FkVatID = "FkVatID";
            public const string IsActive = "IsActive";
            public const string DModifiedDate = "DModifiedDate";
            public const string DCreatedDate = "DCreatedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkProductPackingQuantityRelID] = _tblProductPackingQuantityRel.ColumnNames.PkProductPackingQuantityRelID;
					ht[FkProductID] = _tblProductPackingQuantityRel.ColumnNames.FkProductID;
					ht[FkProductPackageID] = _tblProductPackingQuantityRel.ColumnNames.FkProductPackageID;
					ht[FkProductQuantityID] = _tblProductPackingQuantityRel.ColumnNames.FkProductQuantityID;
					ht[FkVatID] = _tblProductPackingQuantityRel.ColumnNames.FkVatID;
					ht[IsActive] = _tblProductPackingQuantityRel.ColumnNames.IsActive;
					ht[DModifiedDate] = _tblProductPackingQuantityRel.ColumnNames.DModifiedDate;
					ht[DCreatedDate] = _tblProductPackingQuantityRel.ColumnNames.DCreatedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkProductPackingQuantityRelID = "s_PkProductPackingQuantityRelID";
            public const string FkProductID = "s_FkProductID";
            public const string FkProductPackageID = "s_FkProductPackageID";
            public const string FkProductQuantityID = "s_FkProductQuantityID";
            public const string FkVatID = "s_FkVatID";
            public const string IsActive = "s_IsActive";
            public const string DModifiedDate = "s_DModifiedDate";
            public const string DCreatedDate = "s_DCreatedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkProductPackingQuantityRelID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkProductPackingQuantityRelID);
			}
			set
	        {
				base.Setint(ColumnNames.PkProductPackingQuantityRelID, value);
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

		public virtual int FkVatID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkVatID);
			}
			set
	        {
				base.Setint(ColumnNames.FkVatID, value);
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


		#endregion
		
		#region String Properties
	
		public virtual string s_PkProductPackingQuantityRelID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkProductPackingQuantityRelID) ? string.Empty : base.GetintAsString(ColumnNames.PkProductPackingQuantityRelID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkProductPackingQuantityRelID);
				else
					this.PkProductPackingQuantityRelID = base.SetintAsString(ColumnNames.PkProductPackingQuantityRelID, value);
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

		public virtual string s_FkVatID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkVatID) ? string.Empty : base.GetintAsString(ColumnNames.FkVatID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkVatID);
				else
					this.FkVatID = base.SetintAsString(ColumnNames.FkVatID, value);
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
				
				
				public WhereParameter PkProductPackingQuantityRelID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkProductPackingQuantityRelID, Parameters.PkProductPackingQuantityRelID);
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

				public WhereParameter FkVatID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkVatID, Parameters.FkVatID);
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

				public WhereParameter DModifiedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DModifiedDate, Parameters.DModifiedDate);
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


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkProductPackingQuantityRelID
		    {
				get
		        {
					if(_PkProductPackingQuantityRelID_W == null)
	        	    {
						_PkProductPackingQuantityRelID_W = TearOff.PkProductPackingQuantityRelID;
					}
					return _PkProductPackingQuantityRelID_W;
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

			public WhereParameter FkVatID
		    {
				get
		        {
					if(_FkVatID_W == null)
	        	    {
						_FkVatID_W = TearOff.FkVatID;
					}
					return _FkVatID_W;
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

			private WhereParameter _PkProductPackingQuantityRelID_W = null;
			private WhereParameter _FkProductID_W = null;
			private WhereParameter _FkProductPackageID_W = null;
			private WhereParameter _FkProductQuantityID_W = null;
			private WhereParameter _FkVatID_W = null;
			private WhereParameter _IsActive_W = null;
			private WhereParameter _DModifiedDate_W = null;
			private WhereParameter _DCreatedDate_W = null;

			public void WhereClauseReset()
			{
				_PkProductPackingQuantityRelID_W = null;
				_FkProductID_W = null;
				_FkProductPackageID_W = null;
				_FkProductQuantityID_W = null;
				_FkVatID_W = null;
				_IsActive_W = null;
				_DModifiedDate_W = null;
				_DCreatedDate_W = null;

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
				
				
				public AggregateParameter PkProductPackingQuantityRelID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkProductPackingQuantityRelID, Parameters.PkProductPackingQuantityRelID);
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

				public AggregateParameter FkVatID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkVatID, Parameters.FkVatID);
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

				public AggregateParameter DModifiedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DModifiedDate, Parameters.DModifiedDate);
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


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkProductPackingQuantityRelID
		    {
				get
		        {
					if(_PkProductPackingQuantityRelID_W == null)
	        	    {
						_PkProductPackingQuantityRelID_W = TearOff.PkProductPackingQuantityRelID;
					}
					return _PkProductPackingQuantityRelID_W;
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

			public AggregateParameter FkVatID
		    {
				get
		        {
					if(_FkVatID_W == null)
	        	    {
						_FkVatID_W = TearOff.FkVatID;
					}
					return _FkVatID_W;
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

			private AggregateParameter _PkProductPackingQuantityRelID_W = null;
			private AggregateParameter _FkProductID_W = null;
			private AggregateParameter _FkProductPackageID_W = null;
			private AggregateParameter _FkProductQuantityID_W = null;
			private AggregateParameter _FkVatID_W = null;
			private AggregateParameter _IsActive_W = null;
			private AggregateParameter _DModifiedDate_W = null;
			private AggregateParameter _DCreatedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkProductPackingQuantityRelID_W = null;
				_FkProductID_W = null;
				_FkProductPackageID_W = null;
				_FkProductQuantityID_W = null;
				_FkVatID_W = null;
				_IsActive_W = null;
				_DModifiedDate_W = null;
				_DCreatedDate_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductPackingQuantityRelInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkProductPackingQuantityRelID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductPackingQuantityRelUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblProductPackingQuantityRelDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkProductPackingQuantityRelID);
			p.SourceColumn = ColumnNames.PkProductPackingQuantityRelID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkProductPackingQuantityRelID);
			p.SourceColumn = ColumnNames.PkProductPackingQuantityRelID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductID);
			p.SourceColumn = ColumnNames.FkProductID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductPackageID);
			p.SourceColumn = ColumnNames.FkProductPackageID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductQuantityID);
			p.SourceColumn = ColumnNames.FkProductQuantityID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkVatID);
			p.SourceColumn = ColumnNames.FkVatID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.IsActive);
			p.SourceColumn = ColumnNames.IsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DModifiedDate);
			p.SourceColumn = ColumnNames.DModifiedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DCreatedDate);
			p.SourceColumn = ColumnNames.DCreatedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
