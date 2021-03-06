
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
	public abstract class _tblECUserAssignments : SqlClientEntity
	{
		public _tblECUserAssignments()
		{
			this.QuerySource = "tblECUserAssignments";
			this.MappingName = "tblECUserAssignments";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblECUserAssignmentsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkECAssignedUserID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkECAssignedUserID, PkECAssignedUserID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblECUserAssignmentsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkECAssignedUserID
			{
				get
				{
					return new SqlParameter("@PkECAssignedUserID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkSpecialtyID
			{
				get
				{
					return new SqlParameter("@FkSpecialtyID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkRegisterID
			{
				get
				{
					return new SqlParameter("@FkRegisterID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter ECUserID
			{
				get
				{
					return new SqlParameter("@ECUserID", SqlDbType.Int, 0);
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
			
			public static SqlParameter DRegisterDate
			{
				get
				{
					return new SqlParameter("@DRegisterDate", SqlDbType.DateTime, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkECAssignedUserID = "pkECAssignedUserID";
            public const string FkSpecialtyID = "fkSpecialtyID";
            public const string FkRegisterID = "fkRegisterID";
            public const string ECUserID = "ECUserID";
            public const string DCreatedDate = "dCreatedDate";
            public const string DModifiedDate = "dModifiedDate";
            public const string DRegisterDate = "dRegisterDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkECAssignedUserID] = _tblECUserAssignments.PropertyNames.PkECAssignedUserID;
					ht[FkSpecialtyID] = _tblECUserAssignments.PropertyNames.FkSpecialtyID;
					ht[FkRegisterID] = _tblECUserAssignments.PropertyNames.FkRegisterID;
					ht[ECUserID] = _tblECUserAssignments.PropertyNames.ECUserID;
					ht[DCreatedDate] = _tblECUserAssignments.PropertyNames.DCreatedDate;
					ht[DModifiedDate] = _tblECUserAssignments.PropertyNames.DModifiedDate;
					ht[DRegisterDate] = _tblECUserAssignments.PropertyNames.DRegisterDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkECAssignedUserID = "PkECAssignedUserID";
            public const string FkSpecialtyID = "FkSpecialtyID";
            public const string FkRegisterID = "FkRegisterID";
            public const string ECUserID = "ECUserID";
            public const string DCreatedDate = "DCreatedDate";
            public const string DModifiedDate = "DModifiedDate";
            public const string DRegisterDate = "DRegisterDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkECAssignedUserID] = _tblECUserAssignments.ColumnNames.PkECAssignedUserID;
					ht[FkSpecialtyID] = _tblECUserAssignments.ColumnNames.FkSpecialtyID;
					ht[FkRegisterID] = _tblECUserAssignments.ColumnNames.FkRegisterID;
					ht[ECUserID] = _tblECUserAssignments.ColumnNames.ECUserID;
					ht[DCreatedDate] = _tblECUserAssignments.ColumnNames.DCreatedDate;
					ht[DModifiedDate] = _tblECUserAssignments.ColumnNames.DModifiedDate;
					ht[DRegisterDate] = _tblECUserAssignments.ColumnNames.DRegisterDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkECAssignedUserID = "s_PkECAssignedUserID";
            public const string FkSpecialtyID = "s_FkSpecialtyID";
            public const string FkRegisterID = "s_FkRegisterID";
            public const string ECUserID = "s_ECUserID";
            public const string DCreatedDate = "s_DCreatedDate";
            public const string DModifiedDate = "s_DModifiedDate";
            public const string DRegisterDate = "s_DRegisterDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkECAssignedUserID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkECAssignedUserID);
			}
			set
	        {
				base.Setint(ColumnNames.PkECAssignedUserID, value);
			}
		}

		public virtual int FkSpecialtyID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkSpecialtyID);
			}
			set
	        {
				base.Setint(ColumnNames.FkSpecialtyID, value);
			}
		}

		public virtual int FkRegisterID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkRegisterID);
			}
			set
	        {
				base.Setint(ColumnNames.FkRegisterID, value);
			}
		}

		public virtual int ECUserID
	    {
			get
	        {
				return base.Getint(ColumnNames.ECUserID);
			}
			set
	        {
				base.Setint(ColumnNames.ECUserID, value);
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

		public virtual DateTime DRegisterDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DRegisterDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DRegisterDate, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_PkECAssignedUserID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkECAssignedUserID) ? string.Empty : base.GetintAsString(ColumnNames.PkECAssignedUserID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkECAssignedUserID);
				else
					this.PkECAssignedUserID = base.SetintAsString(ColumnNames.PkECAssignedUserID, value);
			}
		}

		public virtual string s_FkSpecialtyID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkSpecialtyID) ? string.Empty : base.GetintAsString(ColumnNames.FkSpecialtyID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkSpecialtyID);
				else
					this.FkSpecialtyID = base.SetintAsString(ColumnNames.FkSpecialtyID, value);
			}
		}

		public virtual string s_FkRegisterID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkRegisterID) ? string.Empty : base.GetintAsString(ColumnNames.FkRegisterID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkRegisterID);
				else
					this.FkRegisterID = base.SetintAsString(ColumnNames.FkRegisterID, value);
			}
		}

		public virtual string s_ECUserID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.ECUserID) ? string.Empty : base.GetintAsString(ColumnNames.ECUserID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.ECUserID);
				else
					this.ECUserID = base.SetintAsString(ColumnNames.ECUserID, value);
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

		public virtual string s_DRegisterDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DRegisterDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DRegisterDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DRegisterDate);
				else
					this.DRegisterDate = base.SetDateTimeAsString(ColumnNames.DRegisterDate, value);
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
				
				
				public WhereParameter PkECAssignedUserID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkECAssignedUserID, Parameters.PkECAssignedUserID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkSpecialtyID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkSpecialtyID, Parameters.FkSpecialtyID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkRegisterID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkRegisterID, Parameters.FkRegisterID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter ECUserID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.ECUserID, Parameters.ECUserID);
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

				public WhereParameter DRegisterDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DRegisterDate, Parameters.DRegisterDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkECAssignedUserID
		    {
				get
		        {
					if(_PkECAssignedUserID_W == null)
	        	    {
						_PkECAssignedUserID_W = TearOff.PkECAssignedUserID;
					}
					return _PkECAssignedUserID_W;
				}
			}

			public WhereParameter FkSpecialtyID
		    {
				get
		        {
					if(_FkSpecialtyID_W == null)
	        	    {
						_FkSpecialtyID_W = TearOff.FkSpecialtyID;
					}
					return _FkSpecialtyID_W;
				}
			}

			public WhereParameter FkRegisterID
		    {
				get
		        {
					if(_FkRegisterID_W == null)
	        	    {
						_FkRegisterID_W = TearOff.FkRegisterID;
					}
					return _FkRegisterID_W;
				}
			}

			public WhereParameter ECUserID
		    {
				get
		        {
					if(_ECUserID_W == null)
	        	    {
						_ECUserID_W = TearOff.ECUserID;
					}
					return _ECUserID_W;
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

			public WhereParameter DRegisterDate
		    {
				get
		        {
					if(_DRegisterDate_W == null)
	        	    {
						_DRegisterDate_W = TearOff.DRegisterDate;
					}
					return _DRegisterDate_W;
				}
			}

			private WhereParameter _PkECAssignedUserID_W = null;
			private WhereParameter _FkSpecialtyID_W = null;
			private WhereParameter _FkRegisterID_W = null;
			private WhereParameter _ECUserID_W = null;
			private WhereParameter _DCreatedDate_W = null;
			private WhereParameter _DModifiedDate_W = null;
			private WhereParameter _DRegisterDate_W = null;

			public void WhereClauseReset()
			{
				_PkECAssignedUserID_W = null;
				_FkSpecialtyID_W = null;
				_FkRegisterID_W = null;
				_ECUserID_W = null;
				_DCreatedDate_W = null;
				_DModifiedDate_W = null;
				_DRegisterDate_W = null;

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
				
				
				public AggregateParameter PkECAssignedUserID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkECAssignedUserID, Parameters.PkECAssignedUserID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkSpecialtyID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkSpecialtyID, Parameters.FkSpecialtyID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkRegisterID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkRegisterID, Parameters.FkRegisterID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter ECUserID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.ECUserID, Parameters.ECUserID);
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

				public AggregateParameter DRegisterDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DRegisterDate, Parameters.DRegisterDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkECAssignedUserID
		    {
				get
		        {
					if(_PkECAssignedUserID_W == null)
	        	    {
						_PkECAssignedUserID_W = TearOff.PkECAssignedUserID;
					}
					return _PkECAssignedUserID_W;
				}
			}

			public AggregateParameter FkSpecialtyID
		    {
				get
		        {
					if(_FkSpecialtyID_W == null)
	        	    {
						_FkSpecialtyID_W = TearOff.FkSpecialtyID;
					}
					return _FkSpecialtyID_W;
				}
			}

			public AggregateParameter FkRegisterID
		    {
				get
		        {
					if(_FkRegisterID_W == null)
	        	    {
						_FkRegisterID_W = TearOff.FkRegisterID;
					}
					return _FkRegisterID_W;
				}
			}

			public AggregateParameter ECUserID
		    {
				get
		        {
					if(_ECUserID_W == null)
	        	    {
						_ECUserID_W = TearOff.ECUserID;
					}
					return _ECUserID_W;
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

			public AggregateParameter DRegisterDate
		    {
				get
		        {
					if(_DRegisterDate_W == null)
	        	    {
						_DRegisterDate_W = TearOff.DRegisterDate;
					}
					return _DRegisterDate_W;
				}
			}

			private AggregateParameter _PkECAssignedUserID_W = null;
			private AggregateParameter _FkSpecialtyID_W = null;
			private AggregateParameter _FkRegisterID_W = null;
			private AggregateParameter _ECUserID_W = null;
			private AggregateParameter _DCreatedDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;
			private AggregateParameter _DRegisterDate_W = null;

			public void AggregateClauseReset()
			{
				_PkECAssignedUserID_W = null;
				_FkSpecialtyID_W = null;
				_FkRegisterID_W = null;
				_ECUserID_W = null;
				_DCreatedDate_W = null;
				_DModifiedDate_W = null;
				_DRegisterDate_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblECUserAssignmentsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkECAssignedUserID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblECUserAssignmentsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblECUserAssignmentsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkECAssignedUserID);
			p.SourceColumn = ColumnNames.PkECAssignedUserID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkECAssignedUserID);
			p.SourceColumn = ColumnNames.PkECAssignedUserID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkSpecialtyID);
			p.SourceColumn = ColumnNames.FkSpecialtyID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkRegisterID);
			p.SourceColumn = ColumnNames.FkRegisterID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.ECUserID);
			p.SourceColumn = ColumnNames.ECUserID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DCreatedDate);
			p.SourceColumn = ColumnNames.DCreatedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DModifiedDate);
			p.SourceColumn = ColumnNames.DModifiedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DRegisterDate);
			p.SourceColumn = ColumnNames.DRegisterDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
