
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
	public abstract class _tblDepartmentProducts : SqlClientEntity
	{
		public _tblDepartmentProducts()
		{
			this.QuerySource = "tblDepartmentProducts";
			this.MappingName = "tblDepartmentProducts";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblDepartmentProductsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkDepartmentProductID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkDepartmentProductID, PkDepartmentProductID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblDepartmentProductsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkDepartmentProductID
			{
				get
				{
					return new SqlParameter("@PkDepartmentProductID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkProductID
			{
				get
				{
					return new SqlParameter("@FkProductID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkDepartmentID
			{
				get
				{
					return new SqlParameter("@FkDepartmentID", SqlDbType.Int, 0);
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
            public const string PkDepartmentProductID = "pkDepartmentProductID";
            public const string FkProductID = "fkProductID";
            public const string FkDepartmentID = "fkDepartmentID";
            public const string DModifiedDate = "dModifiedDate";
            public const string DCreatedDate = "dCreatedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkDepartmentProductID] = _tblDepartmentProducts.PropertyNames.PkDepartmentProductID;
					ht[FkProductID] = _tblDepartmentProducts.PropertyNames.FkProductID;
					ht[FkDepartmentID] = _tblDepartmentProducts.PropertyNames.FkDepartmentID;
					ht[DModifiedDate] = _tblDepartmentProducts.PropertyNames.DModifiedDate;
					ht[DCreatedDate] = _tblDepartmentProducts.PropertyNames.DCreatedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkDepartmentProductID = "PkDepartmentProductID";
            public const string FkProductID = "FkProductID";
            public const string FkDepartmentID = "FkDepartmentID";
            public const string DModifiedDate = "DModifiedDate";
            public const string DCreatedDate = "DCreatedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkDepartmentProductID] = _tblDepartmentProducts.ColumnNames.PkDepartmentProductID;
					ht[FkProductID] = _tblDepartmentProducts.ColumnNames.FkProductID;
					ht[FkDepartmentID] = _tblDepartmentProducts.ColumnNames.FkDepartmentID;
					ht[DModifiedDate] = _tblDepartmentProducts.ColumnNames.DModifiedDate;
					ht[DCreatedDate] = _tblDepartmentProducts.ColumnNames.DCreatedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkDepartmentProductID = "s_PkDepartmentProductID";
            public const string FkProductID = "s_FkProductID";
            public const string FkDepartmentID = "s_FkDepartmentID";
            public const string DModifiedDate = "s_DModifiedDate";
            public const string DCreatedDate = "s_DCreatedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkDepartmentProductID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkDepartmentProductID);
			}
			set
	        {
				base.Setint(ColumnNames.PkDepartmentProductID, value);
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

		public virtual int FkDepartmentID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkDepartmentID);
			}
			set
	        {
				base.Setint(ColumnNames.FkDepartmentID, value);
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
	
		public virtual string s_PkDepartmentProductID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkDepartmentProductID) ? string.Empty : base.GetintAsString(ColumnNames.PkDepartmentProductID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkDepartmentProductID);
				else
					this.PkDepartmentProductID = base.SetintAsString(ColumnNames.PkDepartmentProductID, value);
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

		public virtual string s_FkDepartmentID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkDepartmentID) ? string.Empty : base.GetintAsString(ColumnNames.FkDepartmentID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkDepartmentID);
				else
					this.FkDepartmentID = base.SetintAsString(ColumnNames.FkDepartmentID, value);
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
				
				
				public WhereParameter PkDepartmentProductID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkDepartmentProductID, Parameters.PkDepartmentProductID);
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

				public WhereParameter FkDepartmentID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkDepartmentID, Parameters.FkDepartmentID);
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
		
			public WhereParameter PkDepartmentProductID
		    {
				get
		        {
					if(_PkDepartmentProductID_W == null)
	        	    {
						_PkDepartmentProductID_W = TearOff.PkDepartmentProductID;
					}
					return _PkDepartmentProductID_W;
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

			public WhereParameter FkDepartmentID
		    {
				get
		        {
					if(_FkDepartmentID_W == null)
	        	    {
						_FkDepartmentID_W = TearOff.FkDepartmentID;
					}
					return _FkDepartmentID_W;
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

			private WhereParameter _PkDepartmentProductID_W = null;
			private WhereParameter _FkProductID_W = null;
			private WhereParameter _FkDepartmentID_W = null;
			private WhereParameter _DModifiedDate_W = null;
			private WhereParameter _DCreatedDate_W = null;

			public void WhereClauseReset()
			{
				_PkDepartmentProductID_W = null;
				_FkProductID_W = null;
				_FkDepartmentID_W = null;
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
				
				
				public AggregateParameter PkDepartmentProductID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkDepartmentProductID, Parameters.PkDepartmentProductID);
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

				public AggregateParameter FkDepartmentID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkDepartmentID, Parameters.FkDepartmentID);
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
		
			public AggregateParameter PkDepartmentProductID
		    {
				get
		        {
					if(_PkDepartmentProductID_W == null)
	        	    {
						_PkDepartmentProductID_W = TearOff.PkDepartmentProductID;
					}
					return _PkDepartmentProductID_W;
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

			public AggregateParameter FkDepartmentID
		    {
				get
		        {
					if(_FkDepartmentID_W == null)
	        	    {
						_FkDepartmentID_W = TearOff.FkDepartmentID;
					}
					return _FkDepartmentID_W;
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

			private AggregateParameter _PkDepartmentProductID_W = null;
			private AggregateParameter _FkProductID_W = null;
			private AggregateParameter _FkDepartmentID_W = null;
			private AggregateParameter _DModifiedDate_W = null;
			private AggregateParameter _DCreatedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkDepartmentProductID_W = null;
				_FkProductID_W = null;
				_FkDepartmentID_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblDepartmentProductsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkDepartmentProductID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblDepartmentProductsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblDepartmentProductsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkDepartmentProductID);
			p.SourceColumn = ColumnNames.PkDepartmentProductID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkDepartmentProductID);
			p.SourceColumn = ColumnNames.PkDepartmentProductID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkProductID);
			p.SourceColumn = ColumnNames.FkProductID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkDepartmentID);
			p.SourceColumn = ColumnNames.FkDepartmentID;
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
