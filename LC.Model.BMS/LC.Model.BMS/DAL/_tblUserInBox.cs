
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
	public abstract class _tblUserInBox : SqlClientEntity
	{
		public _tblUserInBox()
		{
			this.QuerySource = "tblUserInBox";
			this.MappingName = "tblUserInBox";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblUserInBoxLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkInBoxID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkInBoxID, PkInBoxID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblUserInBoxLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkInBoxID
			{
				get
				{
					return new SqlParameter("@PkInBoxID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkFromUserID
			{
				get
				{
					return new SqlParameter("@FkFromUserID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkToUserID
			{
				get
				{
					return new SqlParameter("@FkToUserID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter SSubject
			{
				get
				{
					return new SqlParameter("@SSubject", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter SMessage
			{
				get
				{
					return new SqlParameter("@SMessage", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter DReceivedDate
			{
				get
				{
					return new SqlParameter("@DReceivedDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter BIsread
			{
				get
				{
					return new SqlParameter("@BIsread", SqlDbType.Bit, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkInBoxID = "pkInBoxID";
            public const string FkFromUserID = "fkFromUserID";
            public const string FkToUserID = "fkToUserID";
            public const string SSubject = "sSubject";
            public const string SMessage = "sMessage";
            public const string DReceivedDate = "dReceivedDate";
            public const string BIsread = "bIsread";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkInBoxID] = _tblUserInBox.PropertyNames.PkInBoxID;
					ht[FkFromUserID] = _tblUserInBox.PropertyNames.FkFromUserID;
					ht[FkToUserID] = _tblUserInBox.PropertyNames.FkToUserID;
					ht[SSubject] = _tblUserInBox.PropertyNames.SSubject;
					ht[SMessage] = _tblUserInBox.PropertyNames.SMessage;
					ht[DReceivedDate] = _tblUserInBox.PropertyNames.DReceivedDate;
					ht[BIsread] = _tblUserInBox.PropertyNames.BIsread;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkInBoxID = "PkInBoxID";
            public const string FkFromUserID = "FkFromUserID";
            public const string FkToUserID = "FkToUserID";
            public const string SSubject = "SSubject";
            public const string SMessage = "SMessage";
            public const string DReceivedDate = "DReceivedDate";
            public const string BIsread = "BIsread";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkInBoxID] = _tblUserInBox.ColumnNames.PkInBoxID;
					ht[FkFromUserID] = _tblUserInBox.ColumnNames.FkFromUserID;
					ht[FkToUserID] = _tblUserInBox.ColumnNames.FkToUserID;
					ht[SSubject] = _tblUserInBox.ColumnNames.SSubject;
					ht[SMessage] = _tblUserInBox.ColumnNames.SMessage;
					ht[DReceivedDate] = _tblUserInBox.ColumnNames.DReceivedDate;
					ht[BIsread] = _tblUserInBox.ColumnNames.BIsread;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkInBoxID = "s_PkInBoxID";
            public const string FkFromUserID = "s_FkFromUserID";
            public const string FkToUserID = "s_FkToUserID";
            public const string SSubject = "s_SSubject";
            public const string SMessage = "s_SMessage";
            public const string DReceivedDate = "s_DReceivedDate";
            public const string BIsread = "s_BIsread";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkInBoxID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkInBoxID);
			}
			set
	        {
				base.Setint(ColumnNames.PkInBoxID, value);
			}
		}

		public virtual int FkFromUserID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkFromUserID);
			}
			set
	        {
				base.Setint(ColumnNames.FkFromUserID, value);
			}
		}

		public virtual int FkToUserID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkToUserID);
			}
			set
	        {
				base.Setint(ColumnNames.FkToUserID, value);
			}
		}

		public virtual string SSubject
	    {
			get
	        {
				return base.Getstring(ColumnNames.SSubject);
			}
			set
	        {
				base.Setstring(ColumnNames.SSubject, value);
			}
		}

		public virtual string SMessage
	    {
			get
	        {
				return base.Getstring(ColumnNames.SMessage);
			}
			set
	        {
				base.Setstring(ColumnNames.SMessage, value);
			}
		}

		public virtual DateTime DReceivedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DReceivedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DReceivedDate, value);
			}
		}

		public virtual bool BIsread
	    {
			get
	        {
				return base.Getbool(ColumnNames.BIsread);
			}
			set
	        {
				base.Setbool(ColumnNames.BIsread, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_PkInBoxID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkInBoxID) ? string.Empty : base.GetintAsString(ColumnNames.PkInBoxID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkInBoxID);
				else
					this.PkInBoxID = base.SetintAsString(ColumnNames.PkInBoxID, value);
			}
		}

		public virtual string s_FkFromUserID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkFromUserID) ? string.Empty : base.GetintAsString(ColumnNames.FkFromUserID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkFromUserID);
				else
					this.FkFromUserID = base.SetintAsString(ColumnNames.FkFromUserID, value);
			}
		}

		public virtual string s_FkToUserID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkToUserID) ? string.Empty : base.GetintAsString(ColumnNames.FkToUserID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkToUserID);
				else
					this.FkToUserID = base.SetintAsString(ColumnNames.FkToUserID, value);
			}
		}

		public virtual string s_SSubject
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SSubject) ? string.Empty : base.GetstringAsString(ColumnNames.SSubject);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SSubject);
				else
					this.SSubject = base.SetstringAsString(ColumnNames.SSubject, value);
			}
		}

		public virtual string s_SMessage
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SMessage) ? string.Empty : base.GetstringAsString(ColumnNames.SMessage);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SMessage);
				else
					this.SMessage = base.SetstringAsString(ColumnNames.SMessage, value);
			}
		}

		public virtual string s_DReceivedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DReceivedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DReceivedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DReceivedDate);
				else
					this.DReceivedDate = base.SetDateTimeAsString(ColumnNames.DReceivedDate, value);
			}
		}

		public virtual string s_BIsread
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.BIsread) ? string.Empty : base.GetboolAsString(ColumnNames.BIsread);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.BIsread);
				else
					this.BIsread = base.SetboolAsString(ColumnNames.BIsread, value);
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
				
				
				public WhereParameter PkInBoxID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkInBoxID, Parameters.PkInBoxID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkFromUserID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkFromUserID, Parameters.FkFromUserID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkToUserID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkToUserID, Parameters.FkToUserID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SSubject
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SSubject, Parameters.SSubject);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter SMessage
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SMessage, Parameters.SMessage);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter DReceivedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DReceivedDate, Parameters.DReceivedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter BIsread
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.BIsread, Parameters.BIsread);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkInBoxID
		    {
				get
		        {
					if(_PkInBoxID_W == null)
	        	    {
						_PkInBoxID_W = TearOff.PkInBoxID;
					}
					return _PkInBoxID_W;
				}
			}

			public WhereParameter FkFromUserID
		    {
				get
		        {
					if(_FkFromUserID_W == null)
	        	    {
						_FkFromUserID_W = TearOff.FkFromUserID;
					}
					return _FkFromUserID_W;
				}
			}

			public WhereParameter FkToUserID
		    {
				get
		        {
					if(_FkToUserID_W == null)
	        	    {
						_FkToUserID_W = TearOff.FkToUserID;
					}
					return _FkToUserID_W;
				}
			}

			public WhereParameter SSubject
		    {
				get
		        {
					if(_SSubject_W == null)
	        	    {
						_SSubject_W = TearOff.SSubject;
					}
					return _SSubject_W;
				}
			}

			public WhereParameter SMessage
		    {
				get
		        {
					if(_SMessage_W == null)
	        	    {
						_SMessage_W = TearOff.SMessage;
					}
					return _SMessage_W;
				}
			}

			public WhereParameter DReceivedDate
		    {
				get
		        {
					if(_DReceivedDate_W == null)
	        	    {
						_DReceivedDate_W = TearOff.DReceivedDate;
					}
					return _DReceivedDate_W;
				}
			}

			public WhereParameter BIsread
		    {
				get
		        {
					if(_BIsread_W == null)
	        	    {
						_BIsread_W = TearOff.BIsread;
					}
					return _BIsread_W;
				}
			}

			private WhereParameter _PkInBoxID_W = null;
			private WhereParameter _FkFromUserID_W = null;
			private WhereParameter _FkToUserID_W = null;
			private WhereParameter _SSubject_W = null;
			private WhereParameter _SMessage_W = null;
			private WhereParameter _DReceivedDate_W = null;
			private WhereParameter _BIsread_W = null;

			public void WhereClauseReset()
			{
				_PkInBoxID_W = null;
				_FkFromUserID_W = null;
				_FkToUserID_W = null;
				_SSubject_W = null;
				_SMessage_W = null;
				_DReceivedDate_W = null;
				_BIsread_W = null;

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
				
				
				public AggregateParameter PkInBoxID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkInBoxID, Parameters.PkInBoxID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkFromUserID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkFromUserID, Parameters.FkFromUserID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkToUserID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkToUserID, Parameters.FkToUserID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SSubject
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SSubject, Parameters.SSubject);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter SMessage
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SMessage, Parameters.SMessage);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter DReceivedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DReceivedDate, Parameters.DReceivedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter BIsread
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.BIsread, Parameters.BIsread);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkInBoxID
		    {
				get
		        {
					if(_PkInBoxID_W == null)
	        	    {
						_PkInBoxID_W = TearOff.PkInBoxID;
					}
					return _PkInBoxID_W;
				}
			}

			public AggregateParameter FkFromUserID
		    {
				get
		        {
					if(_FkFromUserID_W == null)
	        	    {
						_FkFromUserID_W = TearOff.FkFromUserID;
					}
					return _FkFromUserID_W;
				}
			}

			public AggregateParameter FkToUserID
		    {
				get
		        {
					if(_FkToUserID_W == null)
	        	    {
						_FkToUserID_W = TearOff.FkToUserID;
					}
					return _FkToUserID_W;
				}
			}

			public AggregateParameter SSubject
		    {
				get
		        {
					if(_SSubject_W == null)
	        	    {
						_SSubject_W = TearOff.SSubject;
					}
					return _SSubject_W;
				}
			}

			public AggregateParameter SMessage
		    {
				get
		        {
					if(_SMessage_W == null)
	        	    {
						_SMessage_W = TearOff.SMessage;
					}
					return _SMessage_W;
				}
			}

			public AggregateParameter DReceivedDate
		    {
				get
		        {
					if(_DReceivedDate_W == null)
	        	    {
						_DReceivedDate_W = TearOff.DReceivedDate;
					}
					return _DReceivedDate_W;
				}
			}

			public AggregateParameter BIsread
		    {
				get
		        {
					if(_BIsread_W == null)
	        	    {
						_BIsread_W = TearOff.BIsread;
					}
					return _BIsread_W;
				}
			}

			private AggregateParameter _PkInBoxID_W = null;
			private AggregateParameter _FkFromUserID_W = null;
			private AggregateParameter _FkToUserID_W = null;
			private AggregateParameter _SSubject_W = null;
			private AggregateParameter _SMessage_W = null;
			private AggregateParameter _DReceivedDate_W = null;
			private AggregateParameter _BIsread_W = null;

			public void AggregateClauseReset()
			{
				_PkInBoxID_W = null;
				_FkFromUserID_W = null;
				_FkToUserID_W = null;
				_SSubject_W = null;
				_SMessage_W = null;
				_DReceivedDate_W = null;
				_BIsread_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblUserInBoxInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkInBoxID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblUserInBoxUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblUserInBoxDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkInBoxID);
			p.SourceColumn = ColumnNames.PkInBoxID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkInBoxID);
			p.SourceColumn = ColumnNames.PkInBoxID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkFromUserID);
			p.SourceColumn = ColumnNames.FkFromUserID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkToUserID);
			p.SourceColumn = ColumnNames.FkToUserID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SSubject);
			p.SourceColumn = ColumnNames.SSubject;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SMessage);
			p.SourceColumn = ColumnNames.SMessage;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DReceivedDate);
			p.SourceColumn = ColumnNames.DReceivedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.BIsread);
			p.SourceColumn = ColumnNames.BIsread;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}