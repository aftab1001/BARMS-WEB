
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
	public abstract class _tblSubCategories : SqlClientEntity
	{
		public _tblSubCategories()
		{
			this.QuerySource = "tblSubCategories";
			this.MappingName = "tblSubCategories";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSubCategoriesLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkSubCategoryID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkSubCategoryID, PkSubCategoryID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSubCategoriesLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkSubCategoryID
			{
				get
				{
					return new SqlParameter("@PkSubCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkBaseCategoryID
			{
				get
				{
					return new SqlParameter("@FkBaseCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter CSubCategoryName
			{
				get
				{
					return new SqlParameter("@CSubCategoryName", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter SDescription
			{
				get
				{
					return new SqlParameter("@SDescription", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter IsActive
			{
				get
				{
					return new SqlParameter("@IsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter FkVatID
			{
				get
				{
					return new SqlParameter("@FkVatID", SqlDbType.Int, 0);
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
            public const string PkSubCategoryID = "pkSubCategoryID";
            public const string FkBaseCategoryID = "fkBaseCategoryID";
            public const string CSubCategoryName = "cSubCategoryName";
            public const string SDescription = "sDescription";
            public const string IsActive = "isActive";
            public const string FkVatID = "fkVatID";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSubCategoryID] = _tblSubCategories.PropertyNames.PkSubCategoryID;
					ht[FkBaseCategoryID] = _tblSubCategories.PropertyNames.FkBaseCategoryID;
					ht[CSubCategoryName] = _tblSubCategories.PropertyNames.CSubCategoryName;
					ht[SDescription] = _tblSubCategories.PropertyNames.SDescription;
					ht[IsActive] = _tblSubCategories.PropertyNames.IsActive;
					ht[FkVatID] = _tblSubCategories.PropertyNames.FkVatID;
					ht[DCreatedDate] = _tblSubCategories.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblSubCategories.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkSubCategoryID = "PkSubCategoryID";
            public const string FkBaseCategoryID = "FkBaseCategoryID";
            public const string CSubCategoryName = "CSubCategoryName";
            public const string SDescription = "SDescription";
            public const string IsActive = "IsActive";
            public const string FkVatID = "FkVatID";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSubCategoryID] = _tblSubCategories.ColumnNames.PkSubCategoryID;
					ht[FkBaseCategoryID] = _tblSubCategories.ColumnNames.FkBaseCategoryID;
					ht[CSubCategoryName] = _tblSubCategories.ColumnNames.CSubCategoryName;
					ht[SDescription] = _tblSubCategories.ColumnNames.SDescription;
					ht[IsActive] = _tblSubCategories.ColumnNames.IsActive;
					ht[FkVatID] = _tblSubCategories.ColumnNames.FkVatID;
					ht[DCreatedDate] = _tblSubCategories.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblSubCategories.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkSubCategoryID = "s_PkSubCategoryID";
            public const string FkBaseCategoryID = "s_FkBaseCategoryID";
            public const string CSubCategoryName = "s_CSubCategoryName";
            public const string SDescription = "s_SDescription";
            public const string IsActive = "s_IsActive";
            public const string FkVatID = "s_FkVatID";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkSubCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkSubCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.PkSubCategoryID, value);
			}
		}

		public virtual int FkBaseCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkBaseCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.FkBaseCategoryID, value);
			}
		}

		public virtual string CSubCategoryName
	    {
			get
	        {
				return base.Getstring(ColumnNames.CSubCategoryName);
			}
			set
	        {
				base.Setstring(ColumnNames.CSubCategoryName, value);
			}
		}

		public virtual string SDescription
	    {
			get
	        {
				return base.Getstring(ColumnNames.SDescription);
			}
			set
	        {
				base.Setstring(ColumnNames.SDescription, value);
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
	
		public virtual string s_PkSubCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkSubCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.PkSubCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkSubCategoryID);
				else
					this.PkSubCategoryID = base.SetintAsString(ColumnNames.PkSubCategoryID, value);
			}
		}

		public virtual string s_FkBaseCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkBaseCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.FkBaseCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkBaseCategoryID);
				else
					this.FkBaseCategoryID = base.SetintAsString(ColumnNames.FkBaseCategoryID, value);
			}
		}

		public virtual string s_CSubCategoryName
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.CSubCategoryName) ? string.Empty : base.GetstringAsString(ColumnNames.CSubCategoryName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.CSubCategoryName);
				else
					this.CSubCategoryName = base.SetstringAsString(ColumnNames.CSubCategoryName, value);
			}
		}

		public virtual string s_SDescription
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SDescription) ? string.Empty : base.GetstringAsString(ColumnNames.SDescription);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SDescription);
				else
					this.SDescription = base.SetstringAsString(ColumnNames.SDescription, value);
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
				
				
				public WhereParameter PkSubCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkSubCategoryID, Parameters.PkSubCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkBaseCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkBaseCategoryID, Parameters.FkBaseCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter CSubCategoryName
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.CSubCategoryName, Parameters.CSubCategoryName);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SDescription
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SDescription, Parameters.SDescription);
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

				public WhereParameter FkVatID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkVatID, Parameters.FkVatID);
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
		
			public WhereParameter PkSubCategoryID
		    {
				get
		        {
					if(_PkSubCategoryID_W == null)
	        	    {
						_PkSubCategoryID_W = TearOff.PkSubCategoryID;
					}
					return _PkSubCategoryID_W;
				}
			}

			public WhereParameter FkBaseCategoryID
		    {
				get
		        {
					if(_FkBaseCategoryID_W == null)
	        	    {
						_FkBaseCategoryID_W = TearOff.FkBaseCategoryID;
					}
					return _FkBaseCategoryID_W;
				}
			}

			public WhereParameter CSubCategoryName
		    {
				get
		        {
					if(_CSubCategoryName_W == null)
	        	    {
						_CSubCategoryName_W = TearOff.CSubCategoryName;
					}
					return _CSubCategoryName_W;
				}
			}

			public WhereParameter SDescription
		    {
				get
		        {
					if(_SDescription_W == null)
	        	    {
						_SDescription_W = TearOff.SDescription;
					}
					return _SDescription_W;
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

			private WhereParameter _PkSubCategoryID_W = null;
			private WhereParameter _FkBaseCategoryID_W = null;
			private WhereParameter _CSubCategoryName_W = null;
			private WhereParameter _SDescription_W = null;
			private WhereParameter _IsActive_W = null;
			private WhereParameter _FkVatID_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkSubCategoryID_W = null;
				_FkBaseCategoryID_W = null;
				_CSubCategoryName_W = null;
				_SDescription_W = null;
				_IsActive_W = null;
				_FkVatID_W = null;
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
				
				
				public AggregateParameter PkSubCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkSubCategoryID, Parameters.PkSubCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkBaseCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkBaseCategoryID, Parameters.FkBaseCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter CSubCategoryName
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.CSubCategoryName, Parameters.CSubCategoryName);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SDescription
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SDescription, Parameters.SDescription);
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

				public AggregateParameter FkVatID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkVatID, Parameters.FkVatID);
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
		
			public AggregateParameter PkSubCategoryID
		    {
				get
		        {
					if(_PkSubCategoryID_W == null)
	        	    {
						_PkSubCategoryID_W = TearOff.PkSubCategoryID;
					}
					return _PkSubCategoryID_W;
				}
			}

			public AggregateParameter FkBaseCategoryID
		    {
				get
		        {
					if(_FkBaseCategoryID_W == null)
	        	    {
						_FkBaseCategoryID_W = TearOff.FkBaseCategoryID;
					}
					return _FkBaseCategoryID_W;
				}
			}

			public AggregateParameter CSubCategoryName
		    {
				get
		        {
					if(_CSubCategoryName_W == null)
	        	    {
						_CSubCategoryName_W = TearOff.CSubCategoryName;
					}
					return _CSubCategoryName_W;
				}
			}

			public AggregateParameter SDescription
		    {
				get
		        {
					if(_SDescription_W == null)
	        	    {
						_SDescription_W = TearOff.SDescription;
					}
					return _SDescription_W;
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

			private AggregateParameter _PkSubCategoryID_W = null;
			private AggregateParameter _FkBaseCategoryID_W = null;
			private AggregateParameter _CSubCategoryName_W = null;
			private AggregateParameter _SDescription_W = null;
			private AggregateParameter _IsActive_W = null;
			private AggregateParameter _FkVatID_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkSubCategoryID_W = null;
				_FkBaseCategoryID_W = null;
				_CSubCategoryName_W = null;
				_SDescription_W = null;
				_IsActive_W = null;
				_FkVatID_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSubCategoriesInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkSubCategoryID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSubCategoriesUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSubCategoriesDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkSubCategoryID);
			p.SourceColumn = ColumnNames.PkSubCategoryID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkSubCategoryID);
			p.SourceColumn = ColumnNames.PkSubCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkBaseCategoryID);
			p.SourceColumn = ColumnNames.FkBaseCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.CSubCategoryName);
			p.SourceColumn = ColumnNames.CSubCategoryName;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SDescription);
			p.SourceColumn = ColumnNames.SDescription;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.IsActive);
			p.SourceColumn = ColumnNames.IsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkVatID);
			p.SourceColumn = ColumnNames.FkVatID;
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
