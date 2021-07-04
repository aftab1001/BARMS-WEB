
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
	public abstract class _tblCompanyPhones : SqlClientEntity
	{
		public _tblCompanyPhones()
		{
			this.QuerySource = "tblCompanyPhones";
			this.MappingName = "tblCompanyPhones";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblCompanyPhonesLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkCompanyPhoneID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkCompanyPhoneID, PkCompanyPhoneID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblCompanyPhonesLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkCompanyPhoneID
			{
				get
				{
					return new SqlParameter("@PkCompanyPhoneID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter CPhones
			{
				get
				{
					return new SqlParameter("@CPhones", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter CNote
			{
				get
				{
					return new SqlParameter("@CNote", SqlDbType.NText, 1073741823);
				}
			}
			
			public static SqlParameter FkCompanyID
			{
				get
				{
					return new SqlParameter("@FkCompanyID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter BIsPrimary
			{
				get
				{
					return new SqlParameter("@BIsPrimary", SqlDbType.Bit, 0);
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
            public const string PkCompanyPhoneID = "pkCompanyPhoneID";
            public const string CPhones = "cPhones";
            public const string CNote = "cNote";
            public const string FkCompanyID = "fkCompanyID";
            public const string BIsPrimary = "bIsPrimary";
            public const string DModifiedDate = "dModifiedDate";
            public const string DCreatedDate = "dCreatedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkCompanyPhoneID] = _tblCompanyPhones.PropertyNames.PkCompanyPhoneID;
					ht[CPhones] = _tblCompanyPhones.PropertyNames.CPhones;
					ht[CNote] = _tblCompanyPhones.PropertyNames.CNote;
					ht[FkCompanyID] = _tblCompanyPhones.PropertyNames.FkCompanyID;
					ht[BIsPrimary] = _tblCompanyPhones.PropertyNames.BIsPrimary;
					ht[DModifiedDate] = _tblCompanyPhones.PropertyNames.DModifiedDate;
					ht[DCreatedDate] = _tblCompanyPhones.PropertyNames.DCreatedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkCompanyPhoneID = "PkCompanyPhoneID";
            public const string CPhones = "CPhones";
            public const string CNote = "CNote";
            public const string FkCompanyID = "FkCompanyID";
            public const string BIsPrimary = "BIsPrimary";
            public const string DModifiedDate = "DModifiedDate";
            public const string DCreatedDate = "DCreatedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkCompanyPhoneID] = _tblCompanyPhones.ColumnNames.PkCompanyPhoneID;
					ht[CPhones] = _tblCompanyPhones.ColumnNames.CPhones;
					ht[CNote] = _tblCompanyPhones.ColumnNames.CNote;
					ht[FkCompanyID] = _tblCompanyPhones.ColumnNames.FkCompanyID;
					ht[BIsPrimary] = _tblCompanyPhones.ColumnNames.BIsPrimary;
					ht[DModifiedDate] = _tblCompanyPhones.ColumnNames.DModifiedDate;
					ht[DCreatedDate] = _tblCompanyPhones.ColumnNames.DCreatedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkCompanyPhoneID = "s_PkCompanyPhoneID";
            public const string CPhones = "s_CPhones";
            public const string CNote = "s_CNote";
            public const string FkCompanyID = "s_FkCompanyID";
            public const string BIsPrimary = "s_BIsPrimary";
            public const string DModifiedDate = "s_DModifiedDate";
            public const string DCreatedDate = "s_DCreatedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkCompanyPhoneID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkCompanyPhoneID);
			}
			set
	        {
				base.Setint(ColumnNames.PkCompanyPhoneID, value);
			}
		}

		public virtual string CPhones
	    {
			get
	        {
				return base.Getstring(ColumnNames.CPhones);
			}
			set
	        {
				base.Setstring(ColumnNames.CPhones, value);
			}
		}

		public virtual string CNote
	    {
			get
	        {
				return base.Getstring(ColumnNames.CNote);
			}
			set
	        {
				base.Setstring(ColumnNames.CNote, value);
			}
		}

		public virtual int FkCompanyID
	    {
			get
	        {
				return base.Getint(ColumnNames.FkCompanyID);
			}
			set
	        {
				base.Setint(ColumnNames.FkCompanyID, value);
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
	
		public virtual string s_PkCompanyPhoneID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkCompanyPhoneID) ? string.Empty : base.GetintAsString(ColumnNames.PkCompanyPhoneID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkCompanyPhoneID);
				else
					this.PkCompanyPhoneID = base.SetintAsString(ColumnNames.PkCompanyPhoneID, value);
			}
		}

		public virtual string s_CPhones
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.CPhones) ? string.Empty : base.GetstringAsString(ColumnNames.CPhones);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.CPhones);
				else
					this.CPhones = base.SetstringAsString(ColumnNames.CPhones, value);
			}
		}

		public virtual string s_CNote
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.CNote) ? string.Empty : base.GetstringAsString(ColumnNames.CNote);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.CNote);
				else
					this.CNote = base.SetstringAsString(ColumnNames.CNote, value);
			}
		}

		public virtual string s_FkCompanyID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.FkCompanyID) ? string.Empty : base.GetintAsString(ColumnNames.FkCompanyID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.FkCompanyID);
				else
					this.FkCompanyID = base.SetintAsString(ColumnNames.FkCompanyID, value);
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
				
				
				public WhereParameter PkCompanyPhoneID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkCompanyPhoneID, Parameters.PkCompanyPhoneID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter CPhones
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.CPhones, Parameters.CPhones);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter CNote
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.CNote, Parameters.CNote);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter FkCompanyID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.FkCompanyID, Parameters.FkCompanyID);
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
		
			public WhereParameter PkCompanyPhoneID
		    {
				get
		        {
					if(_PkCompanyPhoneID_W == null)
	        	    {
						_PkCompanyPhoneID_W = TearOff.PkCompanyPhoneID;
					}
					return _PkCompanyPhoneID_W;
				}
			}

			public WhereParameter CPhones
		    {
				get
		        {
					if(_CPhones_W == null)
	        	    {
						_CPhones_W = TearOff.CPhones;
					}
					return _CPhones_W;
				}
			}

			public WhereParameter CNote
		    {
				get
		        {
					if(_CNote_W == null)
	        	    {
						_CNote_W = TearOff.CNote;
					}
					return _CNote_W;
				}
			}

			public WhereParameter FkCompanyID
		    {
				get
		        {
					if(_FkCompanyID_W == null)
	        	    {
						_FkCompanyID_W = TearOff.FkCompanyID;
					}
					return _FkCompanyID_W;
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

			private WhereParameter _PkCompanyPhoneID_W = null;
			private WhereParameter _CPhones_W = null;
			private WhereParameter _CNote_W = null;
			private WhereParameter _FkCompanyID_W = null;
			private WhereParameter _BIsPrimary_W = null;
			private WhereParameter _DModifiedDate_W = null;
			private WhereParameter _DCreatedDate_W = null;

			public void WhereClauseReset()
			{
				_PkCompanyPhoneID_W = null;
				_CPhones_W = null;
				_CNote_W = null;
				_FkCompanyID_W = null;
				_BIsPrimary_W = null;
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
				
				
				public AggregateParameter PkCompanyPhoneID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkCompanyPhoneID, Parameters.PkCompanyPhoneID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter CPhones
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.CPhones, Parameters.CPhones);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter CNote
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.CNote, Parameters.CNote);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter FkCompanyID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.FkCompanyID, Parameters.FkCompanyID);
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
		
			public AggregateParameter PkCompanyPhoneID
		    {
				get
		        {
					if(_PkCompanyPhoneID_W == null)
	        	    {
						_PkCompanyPhoneID_W = TearOff.PkCompanyPhoneID;
					}
					return _PkCompanyPhoneID_W;
				}
			}

			public AggregateParameter CPhones
		    {
				get
		        {
					if(_CPhones_W == null)
	        	    {
						_CPhones_W = TearOff.CPhones;
					}
					return _CPhones_W;
				}
			}

			public AggregateParameter CNote
		    {
				get
		        {
					if(_CNote_W == null)
	        	    {
						_CNote_W = TearOff.CNote;
					}
					return _CNote_W;
				}
			}

			public AggregateParameter FkCompanyID
		    {
				get
		        {
					if(_FkCompanyID_W == null)
	        	    {
						_FkCompanyID_W = TearOff.FkCompanyID;
					}
					return _FkCompanyID_W;
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

			private AggregateParameter _PkCompanyPhoneID_W = null;
			private AggregateParameter _CPhones_W = null;
			private AggregateParameter _CNote_W = null;
			private AggregateParameter _FkCompanyID_W = null;
			private AggregateParameter _BIsPrimary_W = null;
			private AggregateParameter _DModifiedDate_W = null;
			private AggregateParameter _DCreatedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkCompanyPhoneID_W = null;
				_CPhones_W = null;
				_CNote_W = null;
				_FkCompanyID_W = null;
				_BIsPrimary_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCompanyPhonesInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkCompanyPhoneID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCompanyPhonesUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCompanyPhonesDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkCompanyPhoneID);
			p.SourceColumn = ColumnNames.PkCompanyPhoneID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkCompanyPhoneID);
			p.SourceColumn = ColumnNames.PkCompanyPhoneID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.CPhones);
			p.SourceColumn = ColumnNames.CPhones;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.CNote);
			p.SourceColumn = ColumnNames.CNote;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkCompanyID);
			p.SourceColumn = ColumnNames.FkCompanyID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.BIsPrimary);
			p.SourceColumn = ColumnNames.BIsPrimary;
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