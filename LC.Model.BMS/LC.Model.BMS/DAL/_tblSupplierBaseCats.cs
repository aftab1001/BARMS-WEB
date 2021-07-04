
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
	public abstract class _tblSupplierBaseCats : SqlClientEntity
	{
		public _tblSupplierBaseCats()
		{
			this.QuerySource = "tblSupplierBaseCats";
			this.MappingName = "tblSupplierBaseCats";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSupplierBaseCatsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkSupplierBaseCatID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkSupplierBaseCatID, PkSupplierBaseCatID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSupplierBaseCatsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkSupplierBaseCatID
			{
				get
				{
					return new SqlParameter("@PkSupplierBaseCatID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkSupplierID
			{
				get
				{
					return new SqlParameter("@FkSupplierID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkBaseCategoryID
			{
				get
				{
					return new SqlParameter("@FkBaseCategoryID", SqlDbType.Int, 0);
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
            public const string PkSupplierBaseCatID = "pkSupplierBaseCatID";
            public const string FkSupplierID = "fkSupplierID";
            public const string FkBaseCategoryID = "fkBaseCategoryID";
            public const string DCreatedDate = "dCreatedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSupplierBaseCatID] = _tblSupplierBaseCats.PropertyNames.PkSupplierBaseCatID;
					ht[FkSupplierID] = _tblSupplierBaseCats.PropertyNames.FkSupplierID;
					ht[FkBaseCategoryID] = _tblSupplierBaseCats.PropertyNames.FkBaseCategoryID;
					ht[DCreatedDate] = _tblSupplierBaseCats.PropertyNames.DCreatedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkSupplierBaseCatID = "PkSupplierBaseCatID";
            public const string FkSupplierID = "FkSupplierID";
            public const string FkBaseCategoryID = "FkBaseCategoryID";
            public const string DCreatedDate = "DCreatedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSupplierBaseCatID] = _tblSupplierBaseCats.ColumnNames.PkSupplierBaseCatID;
					ht[FkSupplierID] = _tblSupplierBaseCats.ColumnNames.FkSupplierID;
					ht[FkBaseCategoryID] = _tblSupplierBaseCats.ColumnNames.FkBaseCategoryID;
					ht[DCreatedDate] = _tblSupplierBaseCats.ColumnNames.DCreatedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkSupplierBaseCatID = "s_PkSupplierBaseCatID";
            public const string FkSupplierID = "s_FkSupplierID";
            public const string FkBaseCategoryID = "s_FkBaseCategoryID";
            public const string DCreatedDate = "s_DCreatedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkSupplierBaseCatID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkSupplierBaseCatID);
			}
			set
	        {
				base.Setint(ColumnNames.PkSupplierBaseCatID, value);
			}
		}

		public virtual int FkSupplierID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkSupplierID);
			}
			set
	        {
				base.Setint(ColumnNames.FkSupplierID, value);
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
	
		public virtual string s_PkSupplierBaseCatID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkSupplierBaseCatID) ? string.Empty : base.GetintAsString(ColumnNames.PkSupplierBaseCatID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkSupplierBaseCatID);
				else
					this.PkSupplierBaseCatID = base.SetintAsString(ColumnNames.PkSupplierBaseCatID, value);
			}
		}

		public virtual string s_FkSupplierID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkSupplierID) ? string.Empty : base.GetintAsString(ColumnNames.FkSupplierID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkSupplierID);
				else
					this.FkSupplierID = base.SetintAsString(ColumnNames.FkSupplierID, value);
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
				
				
				public WhereParameter PkSupplierBaseCatID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkSupplierBaseCatID, Parameters.PkSupplierBaseCatID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkSupplierID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkSupplierID, Parameters.FkSupplierID);
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
		
			public WhereParameter PkSupplierBaseCatID
		    {
				get
		        {
					if(_PkSupplierBaseCatID_W == null)
	        	    {
						_PkSupplierBaseCatID_W = TearOff.PkSupplierBaseCatID;
					}
					return _PkSupplierBaseCatID_W;
				}
			}

			public WhereParameter FkSupplierID
		    {
				get
		        {
					if(_FkSupplierID_W == null)
	        	    {
						_FkSupplierID_W = TearOff.FkSupplierID;
					}
					return _FkSupplierID_W;
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

			private WhereParameter _PkSupplierBaseCatID_W = null;
			private WhereParameter _FkSupplierID_W = null;
			private WhereParameter _FkBaseCategoryID_W = null;
			private WhereParameter _DCreatedDate_W = null;

			public void WhereClauseReset()
			{
				_PkSupplierBaseCatID_W = null;
				_FkSupplierID_W = null;
				_FkBaseCategoryID_W = null;
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
				
				
				public AggregateParameter PkSupplierBaseCatID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkSupplierBaseCatID, Parameters.PkSupplierBaseCatID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkSupplierID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkSupplierID, Parameters.FkSupplierID);
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
		
			public AggregateParameter PkSupplierBaseCatID
		    {
				get
		        {
					if(_PkSupplierBaseCatID_W == null)
	        	    {
						_PkSupplierBaseCatID_W = TearOff.PkSupplierBaseCatID;
					}
					return _PkSupplierBaseCatID_W;
				}
			}

			public AggregateParameter FkSupplierID
		    {
				get
		        {
					if(_FkSupplierID_W == null)
	        	    {
						_FkSupplierID_W = TearOff.FkSupplierID;
					}
					return _FkSupplierID_W;
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

			private AggregateParameter _PkSupplierBaseCatID_W = null;
			private AggregateParameter _FkSupplierID_W = null;
			private AggregateParameter _FkBaseCategoryID_W = null;
			private AggregateParameter _DCreatedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkSupplierBaseCatID_W = null;
				_FkSupplierID_W = null;
				_FkBaseCategoryID_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierBaseCatsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkSupplierBaseCatID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierBaseCatsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierBaseCatsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkSupplierBaseCatID);
			p.SourceColumn = ColumnNames.PkSupplierBaseCatID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkSupplierBaseCatID);
			p.SourceColumn = ColumnNames.PkSupplierBaseCatID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkSupplierID);
			p.SourceColumn = ColumnNames.FkSupplierID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkBaseCategoryID);
			p.SourceColumn = ColumnNames.FkBaseCategoryID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DCreatedDate);
			p.SourceColumn = ColumnNames.DCreatedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
