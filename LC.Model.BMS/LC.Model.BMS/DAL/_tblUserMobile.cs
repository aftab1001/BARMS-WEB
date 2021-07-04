
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
	public abstract class _tblUserMobile : SqlClientEntity
	{
		public _tblUserMobile()
		{
			this.QuerySource = "tblUserMobile";
			this.MappingName = "tblUserMobile";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblUserMobileLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkPhineID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkPhineID, PkPhineID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblUserMobileLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkPhineID
			{
				get
				{
					return new SqlParameter("@PkPhineID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkUserID
			{
				get
				{
					return new SqlParameter("@FkUserID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter SMobilePhone
			{
				get
				{
					return new SqlParameter("@SMobilePhone", SqlDbType.NVarChar, 100);
				}
			}
			
			public static SqlParameter BIsPrimary
			{
				get
				{
					return new SqlParameter("@BIsPrimary", SqlDbType.Bit, 0);
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
            public const string PkPhineID = "pkPhineID";
            public const string FkUserID = "fkUserID";
            public const string SMobilePhone = "sMobilePhone";
            public const string BIsPrimary = "bIsPrimary";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkPhineID] = _tblUserMobile.PropertyNames.PkPhineID;
					ht[FkUserID] = _tblUserMobile.PropertyNames.FkUserID;
					ht[SMobilePhone] = _tblUserMobile.PropertyNames.SMobilePhone;
					ht[BIsPrimary] = _tblUserMobile.PropertyNames.BIsPrimary;
					ht[DCreatedDate] = _tblUserMobile.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblUserMobile.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkPhineID = "PkPhineID";
            public const string FkUserID = "FkUserID";
            public const string SMobilePhone = "SMobilePhone";
            public const string BIsPrimary = "BIsPrimary";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkPhineID] = _tblUserMobile.ColumnNames.PkPhineID;
					ht[FkUserID] = _tblUserMobile.ColumnNames.FkUserID;
					ht[SMobilePhone] = _tblUserMobile.ColumnNames.SMobilePhone;
					ht[BIsPrimary] = _tblUserMobile.ColumnNames.BIsPrimary;
					ht[DCreatedDate] = _tblUserMobile.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblUserMobile.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkPhineID = "s_PkPhineID";
            public const string FkUserID = "s_FkUserID";
            public const string SMobilePhone = "s_SMobilePhone";
            public const string BIsPrimary = "s_BIsPrimary";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkPhineID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkPhineID);
			}
			set
	        {
				base.Setint(ColumnNames.PkPhineID, value);
			}
		}

		public virtual int FkUserID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkUserID);
			}
			set
	        {
				base.Setint(ColumnNames.FkUserID, value);
			}
		}

		public virtual string SMobilePhone
	    {
			get
	        {
				return base.Getstring(ColumnNames.SMobilePhone);
			}
			set
	        {
				base.Setstring(ColumnNames.SMobilePhone, value);
			}
		}

		public virtual bool BIsPrimary
	    {
			get
	        {
				return base.Getbool(ColumnNames.BIsPrimary);
			}
			set
	        {
				base.Setbool(ColumnNames.BIsPrimary, value);
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
	
		public virtual string s_PkPhineID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkPhineID) ? string.Empty : base.GetintAsString(ColumnNames.PkPhineID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkPhineID);
				else
					this.PkPhineID = base.SetintAsString(ColumnNames.PkPhineID, value);
			}
		}

		public virtual string s_FkUserID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkUserID) ? string.Empty : base.GetintAsString(ColumnNames.FkUserID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkUserID);
				else
					this.FkUserID = base.SetintAsString(ColumnNames.FkUserID, value);
			}
		}

		public virtual string s_SMobilePhone
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SMobilePhone) ? string.Empty : base.GetstringAsString(ColumnNames.SMobilePhone);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SMobilePhone);
				else
					this.SMobilePhone = base.SetstringAsString(ColumnNames.SMobilePhone, value);
			}
		}

		public virtual string s_BIsPrimary
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.BIsPrimary) ? string.Empty : base.GetboolAsString(ColumnNames.BIsPrimary);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.BIsPrimary);
				else
					this.BIsPrimary = base.SetboolAsString(ColumnNames.BIsPrimary, value);
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
				
				
				public WhereParameter PkPhineID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkPhineID, Parameters.PkPhineID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkUserID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkUserID, Parameters.FkUserID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SMobilePhone
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SMobilePhone, Parameters.SMobilePhone);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter BIsPrimary
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.BIsPrimary, Parameters.BIsPrimary);
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
		
			public WhereParameter PkPhineID
		    {
				get
		        {
					if(_PkPhineID_W == null)
	        	    {
						_PkPhineID_W = TearOff.PkPhineID;
					}
					return _PkPhineID_W;
				}
			}

			public WhereParameter FkUserID
		    {
				get
		        {
					if(_FkUserID_W == null)
	        	    {
						_FkUserID_W = TearOff.FkUserID;
					}
					return _FkUserID_W;
				}
			}

			public WhereParameter SMobilePhone
		    {
				get
		        {
					if(_SMobilePhone_W == null)
	        	    {
						_SMobilePhone_W = TearOff.SMobilePhone;
					}
					return _SMobilePhone_W;
				}
			}

			public WhereParameter BIsPrimary
		    {
				get
		        {
					if(_BIsPrimary_W == null)
	        	    {
						_BIsPrimary_W = TearOff.BIsPrimary;
					}
					return _BIsPrimary_W;
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

			private WhereParameter _PkPhineID_W = null;
			private WhereParameter _FkUserID_W = null;
			private WhereParameter _SMobilePhone_W = null;
			private WhereParameter _BIsPrimary_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkPhineID_W = null;
				_FkUserID_W = null;
				_SMobilePhone_W = null;
				_BIsPrimary_W = null;
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
				
				
				public AggregateParameter PkPhineID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkPhineID, Parameters.PkPhineID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkUserID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkUserID, Parameters.FkUserID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SMobilePhone
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SMobilePhone, Parameters.SMobilePhone);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter BIsPrimary
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.BIsPrimary, Parameters.BIsPrimary);
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
		
			public AggregateParameter PkPhineID
		    {
				get
		        {
					if(_PkPhineID_W == null)
	        	    {
						_PkPhineID_W = TearOff.PkPhineID;
					}
					return _PkPhineID_W;
				}
			}

			public AggregateParameter FkUserID
		    {
				get
		        {
					if(_FkUserID_W == null)
	        	    {
						_FkUserID_W = TearOff.FkUserID;
					}
					return _FkUserID_W;
				}
			}

			public AggregateParameter SMobilePhone
		    {
				get
		        {
					if(_SMobilePhone_W == null)
	        	    {
						_SMobilePhone_W = TearOff.SMobilePhone;
					}
					return _SMobilePhone_W;
				}
			}

			public AggregateParameter BIsPrimary
		    {
				get
		        {
					if(_BIsPrimary_W == null)
	        	    {
						_BIsPrimary_W = TearOff.BIsPrimary;
					}
					return _BIsPrimary_W;
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

			private AggregateParameter _PkPhineID_W = null;
			private AggregateParameter _FkUserID_W = null;
			private AggregateParameter _SMobilePhone_W = null;
			private AggregateParameter _BIsPrimary_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkPhineID_W = null;
				_FkUserID_W = null;
				_SMobilePhone_W = null;
				_BIsPrimary_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblUserMobileInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkPhineID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblUserMobileUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblUserMobileDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkPhineID);
			p.SourceColumn = ColumnNames.PkPhineID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkPhineID);
			p.SourceColumn = ColumnNames.PkPhineID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkUserID);
			p.SourceColumn = ColumnNames.FkUserID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SMobilePhone);
			p.SourceColumn = ColumnNames.SMobilePhone;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.BIsPrimary);
			p.SourceColumn = ColumnNames.BIsPrimary;
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