
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
	public abstract class _tblTransactions : SqlClientEntity
	{
		public _tblTransactions()
		{
			this.QuerySource = "tblTransactions";
			this.MappingName = "tblTransactions";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblTransactionsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkTransactionID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkTransactionID, PkTransactionID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblTransactionsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkTransactionID
			{
				get
				{
					return new SqlParameter("@PkTransactionID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkAccountManagerID
			{
				get
				{
					return new SqlParameter("@FkAccountManagerID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkDepartmentAdminID
			{
				get
				{
					return new SqlParameter("@FkDepartmentAdminID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter TransactionType
			{
				get
				{
					return new SqlParameter("@TransactionType", SqlDbType.NVarChar, 30);
				}
			}
			
			public static SqlParameter Amount
			{
				get
				{
					return new SqlParameter("@Amount", SqlDbType.Float, 0);
				}
			}
			
			public static SqlParameter Notes
			{
				get
				{
					return new SqlParameter("@Notes", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter Received
			{
				get
				{
					return new SqlParameter("@Received", SqlDbType.Bit, 0);
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
            public const string PkTransactionID = "pkTransactionID";
            public const string FkAccountManagerID = "fkAccountManagerID";
            public const string FkDepartmentAdminID = "fkDepartmentAdminID";
            public const string TransactionType = "TransactionType";
            public const string Amount = "Amount";
            public const string Notes = "Notes";
            public const string Received = "Received";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkTransactionID] = _tblTransactions.PropertyNames.PkTransactionID;
					ht[FkAccountManagerID] = _tblTransactions.PropertyNames.FkAccountManagerID;
					ht[FkDepartmentAdminID] = _tblTransactions.PropertyNames.FkDepartmentAdminID;
					ht[TransactionType] = _tblTransactions.PropertyNames.TransactionType;
					ht[Amount] = _tblTransactions.PropertyNames.Amount;
					ht[Notes] = _tblTransactions.PropertyNames.Notes;
					ht[Received] = _tblTransactions.PropertyNames.Received;
					ht[DCreatedDate] = _tblTransactions.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblTransactions.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkTransactionID = "PkTransactionID";
            public const string FkAccountManagerID = "FkAccountManagerID";
            public const string FkDepartmentAdminID = "FkDepartmentAdminID";
            public const string TransactionType = "TransactionType";
            public const string Amount = "Amount";
            public const string Notes = "Notes";
            public const string Received = "Received";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkTransactionID] = _tblTransactions.ColumnNames.PkTransactionID;
					ht[FkAccountManagerID] = _tblTransactions.ColumnNames.FkAccountManagerID;
					ht[FkDepartmentAdminID] = _tblTransactions.ColumnNames.FkDepartmentAdminID;
					ht[TransactionType] = _tblTransactions.ColumnNames.TransactionType;
					ht[Amount] = _tblTransactions.ColumnNames.Amount;
					ht[Notes] = _tblTransactions.ColumnNames.Notes;
					ht[Received] = _tblTransactions.ColumnNames.Received;
					ht[DCreatedDate] = _tblTransactions.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblTransactions.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkTransactionID = "s_PkTransactionID";
            public const string FkAccountManagerID = "s_FkAccountManagerID";
            public const string FkDepartmentAdminID = "s_FkDepartmentAdminID";
            public const string TransactionType = "s_TransactionType";
            public const string Amount = "s_Amount";
            public const string Notes = "s_Notes";
            public const string Received = "s_Received";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkTransactionID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkTransactionID);
			}
			set
	        {
				base.Setint(ColumnNames.PkTransactionID, value);
			}
		}

		public virtual int FkAccountManagerID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkAccountManagerID);
			}
			set
	        {
				base.Setint(ColumnNames.FkAccountManagerID, value);
			}
		}

		public virtual int FkDepartmentAdminID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkDepartmentAdminID);
			}
			set
	        {
				base.Setint(ColumnNames.FkDepartmentAdminID, value);
			}
		}

		public virtual string TransactionType
	    {
			get
	        {
				return base.Getstring(ColumnNames.TransactionType);
			}
			set
	        {
				base.Setstring(ColumnNames.TransactionType, value);
			}
		}

		public virtual double Amount
	    {
			get
	        {
				return base.Getdouble(ColumnNames.Amount);
			}
			set
	        {
				base.Setdouble(ColumnNames.Amount, value);
			}
		}

		public virtual string Notes
	    {
			get
	        {
				return base.Getstring(ColumnNames.Notes);
			}
			set
	        {
				base.Setstring(ColumnNames.Notes, value);
			}
		}

		public virtual bool Received
	    {
			get
	        {
				return base.Getbool(ColumnNames.Received);
			}
			set
	        {
				base.Setbool(ColumnNames.Received, value);
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
	
		public virtual string s_PkTransactionID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkTransactionID) ? string.Empty : base.GetintAsString(ColumnNames.PkTransactionID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkTransactionID);
				else
					this.PkTransactionID = base.SetintAsString(ColumnNames.PkTransactionID, value);
			}
		}

		public virtual string s_FkAccountManagerID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkAccountManagerID) ? string.Empty : base.GetintAsString(ColumnNames.FkAccountManagerID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkAccountManagerID);
				else
					this.FkAccountManagerID = base.SetintAsString(ColumnNames.FkAccountManagerID, value);
			}
		}

		public virtual string s_FkDepartmentAdminID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkDepartmentAdminID) ? string.Empty : base.GetintAsString(ColumnNames.FkDepartmentAdminID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkDepartmentAdminID);
				else
					this.FkDepartmentAdminID = base.SetintAsString(ColumnNames.FkDepartmentAdminID, value);
			}
		}

		public virtual string s_TransactionType
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.TransactionType) ? string.Empty : base.GetstringAsString(ColumnNames.TransactionType);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.TransactionType);
				else
					this.TransactionType = base.SetstringAsString(ColumnNames.TransactionType, value);
			}
		}

		public virtual string s_Amount
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Amount) ? string.Empty : base.GetdoubleAsString(ColumnNames.Amount);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Amount);
				else
					this.Amount = base.SetdoubleAsString(ColumnNames.Amount, value);
			}
		}

		public virtual string s_Notes
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Notes) ? string.Empty : base.GetstringAsString(ColumnNames.Notes);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Notes);
				else
					this.Notes = base.SetstringAsString(ColumnNames.Notes, value);
			}
		}

		public virtual string s_Received
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Received) ? string.Empty : base.GetboolAsString(ColumnNames.Received);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Received);
				else
					this.Received = base.SetboolAsString(ColumnNames.Received, value);
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
				
				
				public WhereParameter PkTransactionID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkTransactionID, Parameters.PkTransactionID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkAccountManagerID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkAccountManagerID, Parameters.FkAccountManagerID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkDepartmentAdminID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkDepartmentAdminID, Parameters.FkDepartmentAdminID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter TransactionType
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.TransactionType, Parameters.TransactionType);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Amount
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Amount, Parameters.Amount);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Notes
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Notes, Parameters.Notes);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Received
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Received, Parameters.Received);
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
		
			public WhereParameter PkTransactionID
		    {
				get
		        {
					if(_PkTransactionID_W == null)
	        	    {
						_PkTransactionID_W = TearOff.PkTransactionID;
					}
					return _PkTransactionID_W;
				}
			}

			public WhereParameter FkAccountManagerID
		    {
				get
		        {
					if(_FkAccountManagerID_W == null)
	        	    {
						_FkAccountManagerID_W = TearOff.FkAccountManagerID;
					}
					return _FkAccountManagerID_W;
				}
			}

			public WhereParameter FkDepartmentAdminID
		    {
				get
		        {
					if(_FkDepartmentAdminID_W == null)
	        	    {
						_FkDepartmentAdminID_W = TearOff.FkDepartmentAdminID;
					}
					return _FkDepartmentAdminID_W;
				}
			}

			public WhereParameter TransactionType
		    {
				get
		        {
					if(_TransactionType_W == null)
	        	    {
						_TransactionType_W = TearOff.TransactionType;
					}
					return _TransactionType_W;
				}
			}

			public WhereParameter Amount
		    {
				get
		        {
					if(_Amount_W == null)
	        	    {
						_Amount_W = TearOff.Amount;
					}
					return _Amount_W;
				}
			}

			public WhereParameter Notes
		    {
				get
		        {
					if(_Notes_W == null)
	        	    {
						_Notes_W = TearOff.Notes;
					}
					return _Notes_W;
				}
			}

			public WhereParameter Received
		    {
				get
		        {
					if(_Received_W == null)
	        	    {
						_Received_W = TearOff.Received;
					}
					return _Received_W;
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

			private WhereParameter _PkTransactionID_W = null;
			private WhereParameter _FkAccountManagerID_W = null;
			private WhereParameter _FkDepartmentAdminID_W = null;
			private WhereParameter _TransactionType_W = null;
			private WhereParameter _Amount_W = null;
			private WhereParameter _Notes_W = null;
			private WhereParameter _Received_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkTransactionID_W = null;
				_FkAccountManagerID_W = null;
				_FkDepartmentAdminID_W = null;
				_TransactionType_W = null;
				_Amount_W = null;
				_Notes_W = null;
				_Received_W = null;
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
				
				
				public AggregateParameter PkTransactionID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkTransactionID, Parameters.PkTransactionID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkAccountManagerID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkAccountManagerID, Parameters.FkAccountManagerID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkDepartmentAdminID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkDepartmentAdminID, Parameters.FkDepartmentAdminID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter TransactionType
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.TransactionType, Parameters.TransactionType);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Amount
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Amount, Parameters.Amount);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Notes
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Notes, Parameters.Notes);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Received
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Received, Parameters.Received);
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
		
			public AggregateParameter PkTransactionID
		    {
				get
		        {
					if(_PkTransactionID_W == null)
	        	    {
						_PkTransactionID_W = TearOff.PkTransactionID;
					}
					return _PkTransactionID_W;
				}
			}

			public AggregateParameter FkAccountManagerID
		    {
				get
		        {
					if(_FkAccountManagerID_W == null)
	        	    {
						_FkAccountManagerID_W = TearOff.FkAccountManagerID;
					}
					return _FkAccountManagerID_W;
				}
			}

			public AggregateParameter FkDepartmentAdminID
		    {
				get
		        {
					if(_FkDepartmentAdminID_W == null)
	        	    {
						_FkDepartmentAdminID_W = TearOff.FkDepartmentAdminID;
					}
					return _FkDepartmentAdminID_W;
				}
			}

			public AggregateParameter TransactionType
		    {
				get
		        {
					if(_TransactionType_W == null)
	        	    {
						_TransactionType_W = TearOff.TransactionType;
					}
					return _TransactionType_W;
				}
			}

			public AggregateParameter Amount
		    {
				get
		        {
					if(_Amount_W == null)
	        	    {
						_Amount_W = TearOff.Amount;
					}
					return _Amount_W;
				}
			}

			public AggregateParameter Notes
		    {
				get
		        {
					if(_Notes_W == null)
	        	    {
						_Notes_W = TearOff.Notes;
					}
					return _Notes_W;
				}
			}

			public AggregateParameter Received
		    {
				get
		        {
					if(_Received_W == null)
	        	    {
						_Received_W = TearOff.Received;
					}
					return _Received_W;
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

			private AggregateParameter _PkTransactionID_W = null;
			private AggregateParameter _FkAccountManagerID_W = null;
			private AggregateParameter _FkDepartmentAdminID_W = null;
			private AggregateParameter _TransactionType_W = null;
			private AggregateParameter _Amount_W = null;
			private AggregateParameter _Notes_W = null;
			private AggregateParameter _Received_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkTransactionID_W = null;
				_FkAccountManagerID_W = null;
				_FkDepartmentAdminID_W = null;
				_TransactionType_W = null;
				_Amount_W = null;
				_Notes_W = null;
				_Received_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblTransactionsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkTransactionID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblTransactionsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblTransactionsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkTransactionID);
			p.SourceColumn = ColumnNames.PkTransactionID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkTransactionID);
			p.SourceColumn = ColumnNames.PkTransactionID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkAccountManagerID);
			p.SourceColumn = ColumnNames.FkAccountManagerID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkDepartmentAdminID);
			p.SourceColumn = ColumnNames.FkDepartmentAdminID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.TransactionType);
			p.SourceColumn = ColumnNames.TransactionType;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Amount);
			p.SourceColumn = ColumnNames.Amount;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Notes);
			p.SourceColumn = ColumnNames.Notes;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Received);
			p.SourceColumn = ColumnNames.Received;
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
