
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
	public abstract class _tblBaseOrder : SqlClientEntity
	{
		public _tblBaseOrder()
		{
			this.QuerySource = "tblBaseOrder";
			this.MappingName = "tblBaseOrder";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblBaseOrderLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkBaseOrderID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkBaseOrderID, PkBaseOrderID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblBaseOrderLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkBaseOrderID
			{
				get
				{
					return new SqlParameter("@PkBaseOrderID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter SessionOrderID
			{
				get
				{
					return new SqlParameter("@SessionOrderID", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter GrandSubtotal
			{
				get
				{
					return new SqlParameter("@GrandSubtotal", SqlDbType.Float, 0);
				}
			}
			
			public static SqlParameter OrderReceivedByUser
			{
				get
				{
					return new SqlParameter("@OrderReceivedByUser", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter CashAmount
			{
				get
				{
					return new SqlParameter("@CashAmount", SqlDbType.Float, 0);
				}
			}
			
			public static SqlParameter AllowEdit
			{
				get
				{
					return new SqlParameter("@AllowEdit", SqlDbType.Bit, 0);
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
            public const string PkBaseOrderID = "pkBaseOrderID";
            public const string SessionOrderID = "SessionOrderID";
            public const string GrandSubtotal = "GrandSubtotal";
            public const string OrderReceivedByUser = "OrderReceivedByUser";
            public const string CashAmount = "CashAmount";
            public const string AllowEdit = "allowEdit";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkBaseOrderID] = _tblBaseOrder.PropertyNames.PkBaseOrderID;
					ht[SessionOrderID] = _tblBaseOrder.PropertyNames.SessionOrderID;
					ht[GrandSubtotal] = _tblBaseOrder.PropertyNames.GrandSubtotal;
					ht[OrderReceivedByUser] = _tblBaseOrder.PropertyNames.OrderReceivedByUser;
					ht[CashAmount] = _tblBaseOrder.PropertyNames.CashAmount;
					ht[AllowEdit] = _tblBaseOrder.PropertyNames.AllowEdit;
					ht[DCreatedDate] = _tblBaseOrder.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblBaseOrder.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkBaseOrderID = "PkBaseOrderID";
            public const string SessionOrderID = "SessionOrderID";
            public const string GrandSubtotal = "GrandSubtotal";
            public const string OrderReceivedByUser = "OrderReceivedByUser";
            public const string CashAmount = "CashAmount";
            public const string AllowEdit = "AllowEdit";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkBaseOrderID] = _tblBaseOrder.ColumnNames.PkBaseOrderID;
					ht[SessionOrderID] = _tblBaseOrder.ColumnNames.SessionOrderID;
					ht[GrandSubtotal] = _tblBaseOrder.ColumnNames.GrandSubtotal;
					ht[OrderReceivedByUser] = _tblBaseOrder.ColumnNames.OrderReceivedByUser;
					ht[CashAmount] = _tblBaseOrder.ColumnNames.CashAmount;
					ht[AllowEdit] = _tblBaseOrder.ColumnNames.AllowEdit;
					ht[DCreatedDate] = _tblBaseOrder.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblBaseOrder.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkBaseOrderID = "s_PkBaseOrderID";
            public const string SessionOrderID = "s_SessionOrderID";
            public const string GrandSubtotal = "s_GrandSubtotal";
            public const string OrderReceivedByUser = "s_OrderReceivedByUser";
            public const string CashAmount = "s_CashAmount";
            public const string AllowEdit = "s_AllowEdit";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkBaseOrderID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkBaseOrderID);
			}
			set
	        {
				base.Setint(ColumnNames.PkBaseOrderID, value);
			}
		}

		public virtual string SessionOrderID
	    {
			get
	        {
				return base.Getstring(ColumnNames.SessionOrderID);
			}
			set
	        {
				base.Setstring(ColumnNames.SessionOrderID, value);
			}
		}

		public virtual double GrandSubtotal
	    {
			get
	        {
				return base.Getdouble(ColumnNames.GrandSubtotal);
			}
			set
	        {
				base.Setdouble(ColumnNames.GrandSubtotal, value);
			}
		}

		public virtual int OrderReceivedByUser
	    {
			get
	        {
				return base.Getint(ColumnNames.OrderReceivedByUser);
			}
			set
	        {
				base.Setint(ColumnNames.OrderReceivedByUser, value);
			}
		}

		public virtual double CashAmount
	    {
			get
	        {
				return base.Getdouble(ColumnNames.CashAmount);
			}
			set
	        {
				base.Setdouble(ColumnNames.CashAmount, value);
			}
		}

		public virtual bool AllowEdit
	    {
			get
	        {
				return base.Getbool(ColumnNames.AllowEdit);
			}
			set
	        {
				base.Setbool(ColumnNames.AllowEdit, value);
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
	
		public virtual string s_PkBaseOrderID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkBaseOrderID) ? string.Empty : base.GetintAsString(ColumnNames.PkBaseOrderID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkBaseOrderID);
				else
					this.PkBaseOrderID = base.SetintAsString(ColumnNames.PkBaseOrderID, value);
			}
		}

		public virtual string s_SessionOrderID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SessionOrderID) ? string.Empty : base.GetstringAsString(ColumnNames.SessionOrderID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SessionOrderID);
				else
					this.SessionOrderID = base.SetstringAsString(ColumnNames.SessionOrderID, value);
			}
		}

		public virtual string s_GrandSubtotal
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.GrandSubtotal) ? string.Empty : base.GetdoubleAsString(ColumnNames.GrandSubtotal);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.GrandSubtotal);
				else
					this.GrandSubtotal = base.SetdoubleAsString(ColumnNames.GrandSubtotal, value);
			}
		}

		public virtual string s_OrderReceivedByUser
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.OrderReceivedByUser) ? string.Empty : base.GetintAsString(ColumnNames.OrderReceivedByUser);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.OrderReceivedByUser);
				else
					this.OrderReceivedByUser = base.SetintAsString(ColumnNames.OrderReceivedByUser, value);
			}
		}

		public virtual string s_CashAmount
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.CashAmount) ? string.Empty : base.GetdoubleAsString(ColumnNames.CashAmount);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.CashAmount);
				else
					this.CashAmount = base.SetdoubleAsString(ColumnNames.CashAmount, value);
			}
		}

		public virtual string s_AllowEdit
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AllowEdit) ? string.Empty : base.GetboolAsString(ColumnNames.AllowEdit);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AllowEdit);
				else
					this.AllowEdit = base.SetboolAsString(ColumnNames.AllowEdit, value);
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
				
				
				public WhereParameter PkBaseOrderID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkBaseOrderID, Parameters.PkBaseOrderID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SessionOrderID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SessionOrderID, Parameters.SessionOrderID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter GrandSubtotal
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.GrandSubtotal, Parameters.GrandSubtotal);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter OrderReceivedByUser
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.OrderReceivedByUser, Parameters.OrderReceivedByUser);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter CashAmount
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.CashAmount, Parameters.CashAmount);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AllowEdit
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AllowEdit, Parameters.AllowEdit);
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
		
			public WhereParameter PkBaseOrderID
		    {
				get
		        {
					if(_PkBaseOrderID_W == null)
	        	    {
						_PkBaseOrderID_W = TearOff.PkBaseOrderID;
					}
					return _PkBaseOrderID_W;
				}
			}

			public WhereParameter SessionOrderID
		    {
				get
		        {
					if(_SessionOrderID_W == null)
	        	    {
						_SessionOrderID_W = TearOff.SessionOrderID;
					}
					return _SessionOrderID_W;
				}
			}

			public WhereParameter GrandSubtotal
		    {
				get
		        {
					if(_GrandSubtotal_W == null)
	        	    {
						_GrandSubtotal_W = TearOff.GrandSubtotal;
					}
					return _GrandSubtotal_W;
				}
			}

			public WhereParameter OrderReceivedByUser
		    {
				get
		        {
					if(_OrderReceivedByUser_W == null)
	        	    {
						_OrderReceivedByUser_W = TearOff.OrderReceivedByUser;
					}
					return _OrderReceivedByUser_W;
				}
			}

			public WhereParameter CashAmount
		    {
				get
		        {
					if(_CashAmount_W == null)
	        	    {
						_CashAmount_W = TearOff.CashAmount;
					}
					return _CashAmount_W;
				}
			}

			public WhereParameter AllowEdit
		    {
				get
		        {
					if(_AllowEdit_W == null)
	        	    {
						_AllowEdit_W = TearOff.AllowEdit;
					}
					return _AllowEdit_W;
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

			private WhereParameter _PkBaseOrderID_W = null;
			private WhereParameter _SessionOrderID_W = null;
			private WhereParameter _GrandSubtotal_W = null;
			private WhereParameter _OrderReceivedByUser_W = null;
			private WhereParameter _CashAmount_W = null;
			private WhereParameter _AllowEdit_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_PkBaseOrderID_W = null;
				_SessionOrderID_W = null;
				_GrandSubtotal_W = null;
				_OrderReceivedByUser_W = null;
				_CashAmount_W = null;
				_AllowEdit_W = null;
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
				
				
				public AggregateParameter PkBaseOrderID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkBaseOrderID, Parameters.PkBaseOrderID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SessionOrderID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SessionOrderID, Parameters.SessionOrderID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter GrandSubtotal
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.GrandSubtotal, Parameters.GrandSubtotal);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter OrderReceivedByUser
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.OrderReceivedByUser, Parameters.OrderReceivedByUser);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter CashAmount
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.CashAmount, Parameters.CashAmount);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AllowEdit
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AllowEdit, Parameters.AllowEdit);
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
		
			public AggregateParameter PkBaseOrderID
		    {
				get
		        {
					if(_PkBaseOrderID_W == null)
	        	    {
						_PkBaseOrderID_W = TearOff.PkBaseOrderID;
					}
					return _PkBaseOrderID_W;
				}
			}

			public AggregateParameter SessionOrderID
		    {
				get
		        {
					if(_SessionOrderID_W == null)
	        	    {
						_SessionOrderID_W = TearOff.SessionOrderID;
					}
					return _SessionOrderID_W;
				}
			}

			public AggregateParameter GrandSubtotal
		    {
				get
		        {
					if(_GrandSubtotal_W == null)
	        	    {
						_GrandSubtotal_W = TearOff.GrandSubtotal;
					}
					return _GrandSubtotal_W;
				}
			}

			public AggregateParameter OrderReceivedByUser
		    {
				get
		        {
					if(_OrderReceivedByUser_W == null)
	        	    {
						_OrderReceivedByUser_W = TearOff.OrderReceivedByUser;
					}
					return _OrderReceivedByUser_W;
				}
			}

			public AggregateParameter CashAmount
		    {
				get
		        {
					if(_CashAmount_W == null)
	        	    {
						_CashAmount_W = TearOff.CashAmount;
					}
					return _CashAmount_W;
				}
			}

			public AggregateParameter AllowEdit
		    {
				get
		        {
					if(_AllowEdit_W == null)
	        	    {
						_AllowEdit_W = TearOff.AllowEdit;
					}
					return _AllowEdit_W;
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

			private AggregateParameter _PkBaseOrderID_W = null;
			private AggregateParameter _SessionOrderID_W = null;
			private AggregateParameter _GrandSubtotal_W = null;
			private AggregateParameter _OrderReceivedByUser_W = null;
			private AggregateParameter _CashAmount_W = null;
			private AggregateParameter _AllowEdit_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkBaseOrderID_W = null;
				_SessionOrderID_W = null;
				_GrandSubtotal_W = null;
				_OrderReceivedByUser_W = null;
				_CashAmount_W = null;
				_AllowEdit_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblBaseOrderInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkBaseOrderID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblBaseOrderUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblBaseOrderDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkBaseOrderID);
			p.SourceColumn = ColumnNames.PkBaseOrderID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkBaseOrderID);
			p.SourceColumn = ColumnNames.PkBaseOrderID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SessionOrderID);
			p.SourceColumn = ColumnNames.SessionOrderID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.GrandSubtotal);
			p.SourceColumn = ColumnNames.GrandSubtotal;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.OrderReceivedByUser);
			p.SourceColumn = ColumnNames.OrderReceivedByUser;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.CashAmount);
			p.SourceColumn = ColumnNames.CashAmount;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AllowEdit);
			p.SourceColumn = ColumnNames.AllowEdit;
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