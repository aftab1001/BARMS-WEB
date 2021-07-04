
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
	public abstract class _tblOrderStatus : SqlClientEntity
	{
		public _tblOrderStatus()
		{
			this.QuerySource = "tblOrderStatus";
			this.MappingName = "tblOrderStatus";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblOrderStatusLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey()
		{
			ListDictionary parameters = new ListDictionary();
					
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblOrderStatusLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkOrderStatusID
			{
				get
				{
					return new SqlParameter("@PkOrderStatusID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter OrderStatus
			{
				get
				{
					return new SqlParameter("@OrderStatus", SqlDbType.NVarChar, 50);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkOrderStatusID = "pkOrderStatusID";
            public const string OrderStatus = "OrderStatus";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkOrderStatusID] = _tblOrderStatus.PropertyNames.PkOrderStatusID;
					ht[OrderStatus] = _tblOrderStatus.PropertyNames.OrderStatus;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkOrderStatusID = "PkOrderStatusID";
            public const string OrderStatus = "OrderStatus";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkOrderStatusID] = _tblOrderStatus.ColumnNames.PkOrderStatusID;
					ht[OrderStatus] = _tblOrderStatus.ColumnNames.OrderStatus;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkOrderStatusID = "s_PkOrderStatusID";
            public const string OrderStatus = "s_OrderStatus";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkOrderStatusID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkOrderStatusID);
			}
			set
	        {
				base.Setint(ColumnNames.PkOrderStatusID, value);
			}
		}

		public virtual string OrderStatus
	    {
			get
	        {
				return base.Getstring(ColumnNames.OrderStatus);
			}
			set
	        {
				base.Setstring(ColumnNames.OrderStatus, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_PkOrderStatusID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkOrderStatusID) ? string.Empty : base.GetintAsString(ColumnNames.PkOrderStatusID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkOrderStatusID);
				else
					this.PkOrderStatusID = base.SetintAsString(ColumnNames.PkOrderStatusID, value);
			}
		}

		public virtual string s_OrderStatus
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.OrderStatus) ? string.Empty : base.GetstringAsString(ColumnNames.OrderStatus);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.OrderStatus);
				else
					this.OrderStatus = base.SetstringAsString(ColumnNames.OrderStatus, value);
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
				
				
				public WhereParameter PkOrderStatusID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkOrderStatusID, Parameters.PkOrderStatusID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter OrderStatus
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.OrderStatus, Parameters.OrderStatus);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkOrderStatusID
		    {
				get
		        {
					if(_PkOrderStatusID_W == null)
	        	    {
						_PkOrderStatusID_W = TearOff.PkOrderStatusID;
					}
					return _PkOrderStatusID_W;
				}
			}

			public WhereParameter OrderStatus
		    {
				get
		        {
					if(_OrderStatus_W == null)
	        	    {
						_OrderStatus_W = TearOff.OrderStatus;
					}
					return _OrderStatus_W;
				}
			}

			private WhereParameter _PkOrderStatusID_W = null;
			private WhereParameter _OrderStatus_W = null;

			public void WhereClauseReset()
			{
				_PkOrderStatusID_W = null;
				_OrderStatus_W = null;

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
				
				
				public AggregateParameter PkOrderStatusID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkOrderStatusID, Parameters.PkOrderStatusID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter OrderStatus
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.OrderStatus, Parameters.OrderStatus);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkOrderStatusID
		    {
				get
		        {
					if(_PkOrderStatusID_W == null)
	        	    {
						_PkOrderStatusID_W = TearOff.PkOrderStatusID;
					}
					return _PkOrderStatusID_W;
				}
			}

			public AggregateParameter OrderStatus
		    {
				get
		        {
					if(_OrderStatus_W == null)
	        	    {
						_OrderStatus_W = TearOff.OrderStatus;
					}
					return _OrderStatus_W;
				}
			}

			private AggregateParameter _PkOrderStatusID_W = null;
			private AggregateParameter _OrderStatus_W = null;

			public void AggregateClauseReset()
			{
				_PkOrderStatusID_W = null;
				_OrderStatus_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblOrderStatusInsert]";
	
			CreateParameters(cmd);
			    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblOrderStatusUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblOrderStatusDelete]";
	
			SqlParameter p;
  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkOrderStatusID);
			p.SourceColumn = ColumnNames.PkOrderStatusID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.OrderStatus);
			p.SourceColumn = ColumnNames.OrderStatus;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
