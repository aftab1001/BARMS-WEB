
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
	public abstract class _tblExpanseCategory : SqlClientEntity
	{
		public _tblExpanseCategory()
		{
			this.QuerySource = "tblExpanseCategory";
			this.MappingName = "tblExpanseCategory";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblExpanseCategoryLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkExpanseCategoryID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkExpanseCategoryID, PkExpanseCategoryID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblExpanseCategoryLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkExpanseCategoryID
			{
				get
				{
					return new SqlParameter("@PkExpanseCategoryID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter SExpanseCategory
			{
				get
				{
					return new SqlParameter("@SExpanseCategory", SqlDbType.NVarChar, 100);
				}
			}
			
			public static SqlParameter Description
			{
				get
				{
					return new SqlParameter("@Description", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter BIsActive
			{
				get
				{
					return new SqlParameter("@BIsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter DCreateDate
			{
				get
				{
					return new SqlParameter("@DCreateDate", SqlDbType.DateTime, 0);
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
            public const string PkExpanseCategoryID = "pkExpanseCategoryID";
            public const string SExpanseCategory = "sExpanseCategory";
            public const string Description = "Description";
            public const string BIsActive = "bIsActive";
            public const string DCreateDate = "dCreateDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkExpanseCategoryID] = _tblExpanseCategory.PropertyNames.PkExpanseCategoryID;
					ht[SExpanseCategory] = _tblExpanseCategory.PropertyNames.SExpanseCategory;
					ht[Description] = _tblExpanseCategory.PropertyNames.Description;
					ht[BIsActive] = _tblExpanseCategory.PropertyNames.BIsActive;
					ht[DCreateDate] = _tblExpanseCategory.PropertyNames.DCreateDate;
					ht[DModifiedDate] = _tblExpanseCategory.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkExpanseCategoryID = "PkExpanseCategoryID";
            public const string SExpanseCategory = "SExpanseCategory";
            public const string Description = "Description";
            public const string BIsActive = "BIsActive";
            public const string DCreateDate = "DCreateDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkExpanseCategoryID] = _tblExpanseCategory.ColumnNames.PkExpanseCategoryID;
					ht[SExpanseCategory] = _tblExpanseCategory.ColumnNames.SExpanseCategory;
					ht[Description] = _tblExpanseCategory.ColumnNames.Description;
					ht[BIsActive] = _tblExpanseCategory.ColumnNames.BIsActive;
					ht[DCreateDate] = _tblExpanseCategory.ColumnNames.DCreateDate;
					ht[DModifiedDate] = _tblExpanseCategory.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkExpanseCategoryID = "s_PkExpanseCategoryID";
            public const string SExpanseCategory = "s_SExpanseCategory";
            public const string Description = "s_Description";
            public const string BIsActive = "s_BIsActive";
            public const string DCreateDate = "s_DCreateDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkExpanseCategoryID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkExpanseCategoryID);
			}
			set
	        {
				base.Setint(ColumnNames.PkExpanseCategoryID, value);
			}
		}

		public virtual string SExpanseCategory
	    {
			get
	        {
				return base.Getstring(ColumnNames.SExpanseCategory);
			}
			set
	        {
				base.Setstring(ColumnNames.SExpanseCategory, value);
			}
		}

		public virtual string Description
	    {
			get
	        {
				return base.Getstring(ColumnNames.Description);
			}
			set
	        {
				base.Setstring(ColumnNames.Description, value);
			}
		}

		public virtual bool BIsActive
	    {
			get
	        {
				return base.Getbool(ColumnNames.BIsActive);
			}
			set
	        {
				base.Setbool(ColumnNames.BIsActive, value);
			}
		}

		public virtual DateTime DCreateDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DCreateDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DCreateDate, value);
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
	
		public virtual string s_PkExpanseCategoryID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkExpanseCategoryID) ? string.Empty : base.GetintAsString(ColumnNames.PkExpanseCategoryID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkExpanseCategoryID);
				else
					this.PkExpanseCategoryID = base.SetintAsString(ColumnNames.PkExpanseCategoryID, value);
			}
		}

		public virtual string s_SExpanseCategory
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SExpanseCategory) ? string.Empty : base.GetstringAsString(ColumnNames.SExpanseCategory);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SExpanseCategory);
				else
					this.SExpanseCategory = base.SetstringAsString(ColumnNames.SExpanseCategory, value);
			}
		}

		public virtual string s_Description
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Description) ? string.Empty : base.GetstringAsString(ColumnNames.Description);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Description);
				else
					this.Description = base.SetstringAsString(ColumnNames.Description, value);
			}
		}

		public virtual string s_BIsActive
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.BIsActive) ? string.Empty : base.GetboolAsString(ColumnNames.BIsActive);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.BIsActive);
				else
					this.BIsActive = base.SetboolAsString(ColumnNames.BIsActive, value);
			}
		}

		public virtual string s_DCreateDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DCreateDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DCreateDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DCreateDate);
				else
					this.DCreateDate = base.SetDateTimeAsString(ColumnNames.DCreateDate, value);
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
				
				
				public WhereParameter PkExpanseCategoryID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkExpanseCategoryID, Parameters.PkExpanseCategoryID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SExpanseCategory
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SExpanseCategory, Parameters.SExpanseCategory);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Description
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Description, Parameters.Description);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter BIsActive
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.BIsActive, Parameters.BIsActive);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter DCreateDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DCreateDate, Parameters.DCreateDate);
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
		
			public WhereParameter PkExpanseCategoryID
		    {
				get
		        {
					if(_PkExpanseCategoryID_W == null)
	        	    {
						_PkExpanseCategoryID_W = TearOff.PkExpanseCategoryID;
					}
					return _PkExpanseCategoryID_W;
				}
			}

			public WhereParameter SExpanseCategory
		    {
				get
		        {
					if(_SExpanseCategory_W == null)
	        	    {
						_SExpanseCategory_W = TearOff.SExpanseCategory;
					}
					return _SExpanseCategory_W;
				}
			}

			public WhereParameter Description
		    {
				get
		        {
					if(_Description_W == null)
	        	    {
						_Description_W = TearOff.Description;
					}
					return _Description_W;
				}
			}

			public WhereParameter BIsActive
		    {
				get
		        {
					if(_BIsActive_W == null)
	        	    {
						_BIsActive_W = TearOff.BIsActive;
					}
					return _BIsActive_W;
				}
			}

			public WhereParameter DCreateDate
		    {
				get
		        {
					if(_DCreateDate_W == null)
	        	    {
						_DCreateDate_W = TearOff.DCreateDate;
					}
					return _DCreateDate_W;
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

			private WhereParameter _PkExpanseCategoryID_W = null;
			private WhereParameter _SExpanseCategory_W = null;
			private WhereParameter _Description_W = null;
			private WhereParameter _BIsActive_W = null;
			private WhereParameter _DCreateDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkExpanseCategoryID_W = null;
				_SExpanseCategory_W = null;
				_Description_W = null;
				_BIsActive_W = null;
				_DCreateDate_W = null;
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
				
				
				public AggregateParameter PkExpanseCategoryID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkExpanseCategoryID, Parameters.PkExpanseCategoryID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SExpanseCategory
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SExpanseCategory, Parameters.SExpanseCategory);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Description
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Description, Parameters.Description);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter BIsActive
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.BIsActive, Parameters.BIsActive);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter DCreateDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DCreateDate, Parameters.DCreateDate);
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
		
			public AggregateParameter PkExpanseCategoryID
		    {
				get
		        {
					if(_PkExpanseCategoryID_W == null)
	        	    {
						_PkExpanseCategoryID_W = TearOff.PkExpanseCategoryID;
					}
					return _PkExpanseCategoryID_W;
				}
			}

			public AggregateParameter SExpanseCategory
		    {
				get
		        {
					if(_SExpanseCategory_W == null)
	        	    {
						_SExpanseCategory_W = TearOff.SExpanseCategory;
					}
					return _SExpanseCategory_W;
				}
			}

			public AggregateParameter Description
		    {
				get
		        {
					if(_Description_W == null)
	        	    {
						_Description_W = TearOff.Description;
					}
					return _Description_W;
				}
			}

			public AggregateParameter BIsActive
		    {
				get
		        {
					if(_BIsActive_W == null)
	        	    {
						_BIsActive_W = TearOff.BIsActive;
					}
					return _BIsActive_W;
				}
			}

			public AggregateParameter DCreateDate
		    {
				get
		        {
					if(_DCreateDate_W == null)
	        	    {
						_DCreateDate_W = TearOff.DCreateDate;
					}
					return _DCreateDate_W;
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

			private AggregateParameter _PkExpanseCategoryID_W = null;
			private AggregateParameter _SExpanseCategory_W = null;
			private AggregateParameter _Description_W = null;
			private AggregateParameter _BIsActive_W = null;
			private AggregateParameter _DCreateDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkExpanseCategoryID_W = null;
				_SExpanseCategory_W = null;
				_Description_W = null;
				_BIsActive_W = null;
				_DCreateDate_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblExpanseCategoryInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkExpanseCategoryID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblExpanseCategoryUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblExpanseCategoryDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkExpanseCategoryID);
			p.SourceColumn = ColumnNames.PkExpanseCategoryID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkExpanseCategoryID);
			p.SourceColumn = ColumnNames.PkExpanseCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SExpanseCategory);
			p.SourceColumn = ColumnNames.SExpanseCategory;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Description);
			p.SourceColumn = ColumnNames.Description;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.BIsActive);
			p.SourceColumn = ColumnNames.BIsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DCreateDate);
			p.SourceColumn = ColumnNames.DCreateDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DModifiedDate);
			p.SourceColumn = ColumnNames.DModifiedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}