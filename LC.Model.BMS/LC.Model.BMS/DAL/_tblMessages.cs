
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
	public abstract class _tblMessages : SqlClientEntity
	{
		public _tblMessages()
		{
			this.QuerySource = "tblMessages";
			this.MappingName = "tblMessages";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblMessagesLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkMessageID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkMessageID, PkMessageID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblMessagesLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkMessageID
			{
				get
				{
					return new SqlParameter("@PkMessageID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkInboxID
			{
				get
				{
					return new SqlParameter("@FkInboxID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkSentBoxID
			{
				get
				{
					return new SqlParameter("@FkSentBoxID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter DMessageDate
			{
				get
				{
					return new SqlParameter("@DMessageDate", SqlDbType.DateTime, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkMessageID = "pkMessageID";
            public const string FkInboxID = "fkInboxID";
            public const string FkSentBoxID = "fkSentBoxID";
            public const string DMessageDate = "dMessageDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkMessageID] = _tblMessages.PropertyNames.PkMessageID;
					ht[FkInboxID] = _tblMessages.PropertyNames.FkInboxID;
					ht[FkSentBoxID] = _tblMessages.PropertyNames.FkSentBoxID;
					ht[DMessageDate] = _tblMessages.PropertyNames.DMessageDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkMessageID = "PkMessageID";
            public const string FkInboxID = "FkInboxID";
            public const string FkSentBoxID = "FkSentBoxID";
            public const string DMessageDate = "DMessageDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkMessageID] = _tblMessages.ColumnNames.PkMessageID;
					ht[FkInboxID] = _tblMessages.ColumnNames.FkInboxID;
					ht[FkSentBoxID] = _tblMessages.ColumnNames.FkSentBoxID;
					ht[DMessageDate] = _tblMessages.ColumnNames.DMessageDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkMessageID = "s_PkMessageID";
            public const string FkInboxID = "s_FkInboxID";
            public const string FkSentBoxID = "s_FkSentBoxID";
            public const string DMessageDate = "s_DMessageDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkMessageID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkMessageID);
			}
			set
	        {
				base.Setint(ColumnNames.PkMessageID, value);
			}
		}

		public virtual int FkInboxID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkInboxID);
			}
			set
	        {
				base.Setint(ColumnNames.FkInboxID, value);
			}
		}

		public virtual int FkSentBoxID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkSentBoxID);
			}
			set
	        {
				base.Setint(ColumnNames.FkSentBoxID, value);
			}
		}

		public virtual DateTime DMessageDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DMessageDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DMessageDate, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_PkMessageID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkMessageID) ? string.Empty : base.GetintAsString(ColumnNames.PkMessageID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkMessageID);
				else
					this.PkMessageID = base.SetintAsString(ColumnNames.PkMessageID, value);
			}
		}

		public virtual string s_FkInboxID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkInboxID) ? string.Empty : base.GetintAsString(ColumnNames.FkInboxID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkInboxID);
				else
					this.FkInboxID = base.SetintAsString(ColumnNames.FkInboxID, value);
			}
		}

		public virtual string s_FkSentBoxID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkSentBoxID) ? string.Empty : base.GetintAsString(ColumnNames.FkSentBoxID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkSentBoxID);
				else
					this.FkSentBoxID = base.SetintAsString(ColumnNames.FkSentBoxID, value);
			}
		}

		public virtual string s_DMessageDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DMessageDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DMessageDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DMessageDate);
				else
					this.DMessageDate = base.SetDateTimeAsString(ColumnNames.DMessageDate, value);
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
				
				
				public WhereParameter PkMessageID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkMessageID, Parameters.PkMessageID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkInboxID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkInboxID, Parameters.FkInboxID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkSentBoxID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkSentBoxID, Parameters.FkSentBoxID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter DMessageDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DMessageDate, Parameters.DMessageDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkMessageID
		    {
				get
		        {
					if(_PkMessageID_W == null)
	        	    {
						_PkMessageID_W = TearOff.PkMessageID;
					}
					return _PkMessageID_W;
				}
			}

			public WhereParameter FkInboxID
		    {
				get
		        {
					if(_FkInboxID_W == null)
	        	    {
						_FkInboxID_W = TearOff.FkInboxID;
					}
					return _FkInboxID_W;
				}
			}

			public WhereParameter FkSentBoxID
		    {
				get
		        {
					if(_FkSentBoxID_W == null)
	        	    {
						_FkSentBoxID_W = TearOff.FkSentBoxID;
					}
					return _FkSentBoxID_W;
				}
			}

			public WhereParameter DMessageDate
		    {
				get
		        {
					if(_DMessageDate_W == null)
	        	    {
						_DMessageDate_W = TearOff.DMessageDate;
					}
					return _DMessageDate_W;
				}
			}

			private WhereParameter _PkMessageID_W = null;
			private WhereParameter _FkInboxID_W = null;
			private WhereParameter _FkSentBoxID_W = null;
			private WhereParameter _DMessageDate_W = null;

			public void WhereClauseReset()
			{
				_PkMessageID_W = null;
				_FkInboxID_W = null;
				_FkSentBoxID_W = null;
				_DMessageDate_W = null;

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
				
				
				public AggregateParameter PkMessageID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkMessageID, Parameters.PkMessageID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkInboxID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkInboxID, Parameters.FkInboxID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkSentBoxID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkSentBoxID, Parameters.FkSentBoxID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter DMessageDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DMessageDate, Parameters.DMessageDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkMessageID
		    {
				get
		        {
					if(_PkMessageID_W == null)
	        	    {
						_PkMessageID_W = TearOff.PkMessageID;
					}
					return _PkMessageID_W;
				}
			}

			public AggregateParameter FkInboxID
		    {
				get
		        {
					if(_FkInboxID_W == null)
	        	    {
						_FkInboxID_W = TearOff.FkInboxID;
					}
					return _FkInboxID_W;
				}
			}

			public AggregateParameter FkSentBoxID
		    {
				get
		        {
					if(_FkSentBoxID_W == null)
	        	    {
						_FkSentBoxID_W = TearOff.FkSentBoxID;
					}
					return _FkSentBoxID_W;
				}
			}

			public AggregateParameter DMessageDate
		    {
				get
		        {
					if(_DMessageDate_W == null)
	        	    {
						_DMessageDate_W = TearOff.DMessageDate;
					}
					return _DMessageDate_W;
				}
			}

			private AggregateParameter _PkMessageID_W = null;
			private AggregateParameter _FkInboxID_W = null;
			private AggregateParameter _FkSentBoxID_W = null;
			private AggregateParameter _DMessageDate_W = null;

			public void AggregateClauseReset()
			{
				_PkMessageID_W = null;
				_FkInboxID_W = null;
				_FkSentBoxID_W = null;
				_DMessageDate_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblMessagesInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkMessageID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblMessagesUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblMessagesDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkMessageID);
			p.SourceColumn = ColumnNames.PkMessageID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkMessageID);
			p.SourceColumn = ColumnNames.PkMessageID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkInboxID);
			p.SourceColumn = ColumnNames.FkInboxID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkSentBoxID);
			p.SourceColumn = ColumnNames.FkSentBoxID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DMessageDate);
			p.SourceColumn = ColumnNames.DMessageDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}