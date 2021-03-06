
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
	public abstract class _tblSupplierPhone : SqlClientEntity
	{
		public _tblSupplierPhone()
		{
			this.QuerySource = "tblSupplierPhone";
			this.MappingName = "tblSupplierPhone";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSupplierPhoneLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkSupplierPhoneID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkSupplierPhoneID, PkSupplierPhoneID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSupplierPhoneLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkSupplierPhoneID
			{
				get
				{
					return new SqlParameter("@PkSupplierPhoneID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkSupplierID
			{
				get
				{
					return new SqlParameter("@FkSupplierID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter Phone
			{
				get
				{
					return new SqlParameter("@Phone", SqlDbType.NVarChar, 50);
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
            public const string PkSupplierPhoneID = "pkSupplierPhoneID";
            public const string FkSupplierID = "fkSupplierID";
            public const string Phone = "phone";
            public const string BIsPrimary = "bIsPrimary";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSupplierPhoneID] = _tblSupplierPhone.PropertyNames.PkSupplierPhoneID;
					ht[FkSupplierID] = _tblSupplierPhone.PropertyNames.FkSupplierID;
					ht[Phone] = _tblSupplierPhone.PropertyNames.Phone;
					ht[BIsPrimary] = _tblSupplierPhone.PropertyNames.BIsPrimary;
					ht[DCreatedDate] = _tblSupplierPhone.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblSupplierPhone.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkSupplierPhoneID = "PkSupplierPhoneID";
            public const string FkSupplierID = "FkSupplierID";
            public const string Phone = "Phone";
            public const string BIsPrimary = "BIsPrimary";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSupplierPhoneID] = _tblSupplierPhone.ColumnNames.PkSupplierPhoneID;
					ht[FkSupplierID] = _tblSupplierPhone.ColumnNames.FkSupplierID;
					ht[Phone] = _tblSupplierPhone.ColumnNames.Phone;
					ht[BIsPrimary] = _tblSupplierPhone.ColumnNames.BIsPrimary;
					ht[DCreatedDate] = _tblSupplierPhone.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblSupplierPhone.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkSupplierPhoneID = "s_PkSupplierPhoneID";
            public const string FkSupplierID = "s_FkSupplierID";
            public const string Phone = "s_Phone";
            public const string BIsPrimary = "s_BIsPrimary";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkSupplierPhoneID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkSupplierPhoneID);
			}
			set
	        {
				base.Setint(ColumnNames.PkSupplierPhoneID, value);
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

		public virtual string Phone
	    {
			get
	        {
				return base.Getstring(ColumnNames.Phone);
			}
			set
	        {
				base.Setstring(ColumnNames.Phone, value);
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
	
		public virtual string s_PkSupplierPhoneID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkSupplierPhoneID) ? string.Empty : base.GetintAsString(ColumnNames.PkSupplierPhoneID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkSupplierPhoneID);
				else
					this.PkSupplierPhoneID = base.SetintAsString(ColumnNames.PkSupplierPhoneID, value);
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

		public virtual string s_Phone
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Phone) ? string.Empty : base.GetstringAsString(ColumnNames.Phone);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Phone);
				else
					this.Phone = base.SetstringAsString(ColumnNames.Phone, value);
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
				
				
				public WhereParameter PkSupplierPhoneID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkSupplierPhoneID, Parameters.PkSupplierPhoneID);
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

				public WhereParameter Phone
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Phone, Parameters.Phone);
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
		
			public WhereParameter PkSupplierPhoneID
		    {
				get
		        {
					if(_PkSupplierPhoneID_W == null)
	        	    {
						_PkSupplierPhoneID_W = TearOff.PkSupplierPhoneID;
					}
					return _PkSupplierPhoneID_W;
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

			public WhereParameter Phone
		    {
				get
		        {
					if(_Phone_W == null)
	        	    {
						_Phone_W = TearOff.Phone;
					}
					return _Phone_W;
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

			private WhereParameter _PkSupplierPhoneID_W = null;
			private WhereParameter _FkSupplierID_W = null;
			private WhereParameter _Phone_W = null;
			private WhereParameter _BIsPrimary_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkSupplierPhoneID_W = null;
				_FkSupplierID_W = null;
				_Phone_W = null;
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
				
				
				public AggregateParameter PkSupplierPhoneID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkSupplierPhoneID, Parameters.PkSupplierPhoneID);
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

				public AggregateParameter Phone
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Phone, Parameters.Phone);
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
		
			public AggregateParameter PkSupplierPhoneID
		    {
				get
		        {
					if(_PkSupplierPhoneID_W == null)
	        	    {
						_PkSupplierPhoneID_W = TearOff.PkSupplierPhoneID;
					}
					return _PkSupplierPhoneID_W;
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

			public AggregateParameter Phone
		    {
				get
		        {
					if(_Phone_W == null)
	        	    {
						_Phone_W = TearOff.Phone;
					}
					return _Phone_W;
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

			private AggregateParameter _PkSupplierPhoneID_W = null;
			private AggregateParameter _FkSupplierID_W = null;
			private AggregateParameter _Phone_W = null;
			private AggregateParameter _BIsPrimary_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkSupplierPhoneID_W = null;
				_FkSupplierID_W = null;
				_Phone_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierPhoneInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkSupplierPhoneID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierPhoneUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierPhoneDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkSupplierPhoneID);
			p.SourceColumn = ColumnNames.PkSupplierPhoneID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkSupplierPhoneID);
			p.SourceColumn = ColumnNames.PkSupplierPhoneID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkSupplierID);
			p.SourceColumn = ColumnNames.FkSupplierID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Phone);
			p.SourceColumn = ColumnNames.Phone;
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
