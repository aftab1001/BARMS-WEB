
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
	public abstract class _tblCompanyEmails : SqlClientEntity
	{
		public _tblCompanyEmails()
		{
			this.QuerySource = "tblCompanyEmails";
			this.MappingName = "tblCompanyEmails";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblCompanyEmailsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkCompanyEmailID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkCompanyEmailID, PkCompanyEmailID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblCompanyEmailsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkCompanyEmailID
			{
				get
				{
					return new SqlParameter("@PkCompanyEmailID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter CEmails
			{
				get
				{
					return new SqlParameter("@CEmails", SqlDbType.NVarChar, 50);
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
			
			public static SqlParameter DCreateedDate
			{
				get
				{
					return new SqlParameter("@DCreateedDate", SqlDbType.DateTime, 0);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string PkCompanyEmailID = "pkCompanyEmailID";
            public const string CEmails = "cEmails";
            public const string CNote = "cNote";
            public const string FkCompanyID = "fkCompanyID";
            public const string BIsPrimary = "bIsPrimary";
            public const string DModifiedDate = "dModifiedDate";
            public const string DCreateedDate = "dCreateedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkCompanyEmailID] = _tblCompanyEmails.PropertyNames.PkCompanyEmailID;
					ht[CEmails] = _tblCompanyEmails.PropertyNames.CEmails;
					ht[CNote] = _tblCompanyEmails.PropertyNames.CNote;
					ht[FkCompanyID] = _tblCompanyEmails.PropertyNames.FkCompanyID;
					ht[BIsPrimary] = _tblCompanyEmails.PropertyNames.BIsPrimary;
					ht[DModifiedDate] = _tblCompanyEmails.PropertyNames.DModifiedDate;
					ht[DCreateedDate] = _tblCompanyEmails.PropertyNames.DCreateedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkCompanyEmailID = "PkCompanyEmailID";
            public const string CEmails = "CEmails";
            public const string CNote = "CNote";
            public const string FkCompanyID = "FkCompanyID";
            public const string BIsPrimary = "BIsPrimary";
            public const string DModifiedDate = "DModifiedDate";
            public const string DCreateedDate = "DCreateedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkCompanyEmailID] = _tblCompanyEmails.ColumnNames.PkCompanyEmailID;
					ht[CEmails] = _tblCompanyEmails.ColumnNames.CEmails;
					ht[CNote] = _tblCompanyEmails.ColumnNames.CNote;
					ht[FkCompanyID] = _tblCompanyEmails.ColumnNames.FkCompanyID;
					ht[BIsPrimary] = _tblCompanyEmails.ColumnNames.BIsPrimary;
					ht[DModifiedDate] = _tblCompanyEmails.ColumnNames.DModifiedDate;
					ht[DCreateedDate] = _tblCompanyEmails.ColumnNames.DCreateedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkCompanyEmailID = "s_PkCompanyEmailID";
            public const string CEmails = "s_CEmails";
            public const string CNote = "s_CNote";
            public const string FkCompanyID = "s_FkCompanyID";
            public const string BIsPrimary = "s_BIsPrimary";
            public const string DModifiedDate = "s_DModifiedDate";
            public const string DCreateedDate = "s_DCreateedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkCompanyEmailID
	    {
			get
	        {
				return base.Getint(ColumnNames.PkCompanyEmailID);
			}
			set
	        {
				base.Setint(ColumnNames.PkCompanyEmailID, value);
			}
		}

		public virtual string CEmails
	    {
			get
	        {
				return base.Getstring(ColumnNames.CEmails);
			}
			set
	        {
				base.Setstring(ColumnNames.CEmails, value);
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

		public virtual DateTime DCreateedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DCreateedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DCreateedDate, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_PkCompanyEmailID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkCompanyEmailID) ? string.Empty : base.GetintAsString(ColumnNames.PkCompanyEmailID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkCompanyEmailID);
				else
					this.PkCompanyEmailID = base.SetintAsString(ColumnNames.PkCompanyEmailID, value);
			}
		}

		public virtual string s_CEmails
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.CEmails) ? string.Empty : base.GetstringAsString(ColumnNames.CEmails);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.CEmails);
				else
					this.CEmails = base.SetstringAsString(ColumnNames.CEmails, value);
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

		public virtual string s_DCreateedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DCreateedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DCreateedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DCreateedDate);
				else
					this.DCreateedDate = base.SetDateTimeAsString(ColumnNames.DCreateedDate, value);
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
				
				
				public WhereParameter PkCompanyEmailID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkCompanyEmailID, Parameters.PkCompanyEmailID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter CEmails
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.CEmails, Parameters.CEmails);
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

				public WhereParameter DCreateedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DCreateedDate, Parameters.DCreateedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter PkCompanyEmailID
		    {
				get
		        {
					if(_PkCompanyEmailID_W == null)
	        	    {
						_PkCompanyEmailID_W = TearOff.PkCompanyEmailID;
					}
					return _PkCompanyEmailID_W;
				}
			}

			public WhereParameter CEmails
		    {
				get
		        {
					if(_CEmails_W == null)
	        	    {
						_CEmails_W = TearOff.CEmails;
					}
					return _CEmails_W;
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

			public WhereParameter DCreateedDate
		    {
				get
		        {
					if(_DCreateedDate_W == null)
	        	    {
						_DCreateedDate_W = TearOff.DCreateedDate;
					}
					return _DCreateedDate_W;
				}
			}

			private WhereParameter _PkCompanyEmailID_W = null;
			private WhereParameter _CEmails_W = null;
			private WhereParameter _CNote_W = null;
			private WhereParameter _FkCompanyID_W = null;
			private WhereParameter _BIsPrimary_W = null;
			private WhereParameter _DModifiedDate_W = null;
			private WhereParameter _DCreateedDate_W = null;

			public void WhereClauseReset()
			{
				_PkCompanyEmailID_W = null;
				_CEmails_W = null;
				_CNote_W = null;
				_FkCompanyID_W = null;
				_BIsPrimary_W = null;
				_DModifiedDate_W = null;
				_DCreateedDate_W = null;

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
				
				
				public AggregateParameter PkCompanyEmailID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkCompanyEmailID, Parameters.PkCompanyEmailID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter CEmails
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.CEmails, Parameters.CEmails);
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

				public AggregateParameter DCreateedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DCreateedDate, Parameters.DCreateedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter PkCompanyEmailID
		    {
				get
		        {
					if(_PkCompanyEmailID_W == null)
	        	    {
						_PkCompanyEmailID_W = TearOff.PkCompanyEmailID;
					}
					return _PkCompanyEmailID_W;
				}
			}

			public AggregateParameter CEmails
		    {
				get
		        {
					if(_CEmails_W == null)
	        	    {
						_CEmails_W = TearOff.CEmails;
					}
					return _CEmails_W;
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

			public AggregateParameter DCreateedDate
		    {
				get
		        {
					if(_DCreateedDate_W == null)
	        	    {
						_DCreateedDate_W = TearOff.DCreateedDate;
					}
					return _DCreateedDate_W;
				}
			}

			private AggregateParameter _PkCompanyEmailID_W = null;
			private AggregateParameter _CEmails_W = null;
			private AggregateParameter _CNote_W = null;
			private AggregateParameter _FkCompanyID_W = null;
			private AggregateParameter _BIsPrimary_W = null;
			private AggregateParameter _DModifiedDate_W = null;
			private AggregateParameter _DCreateedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkCompanyEmailID_W = null;
				_CEmails_W = null;
				_CNote_W = null;
				_FkCompanyID_W = null;
				_BIsPrimary_W = null;
				_DModifiedDate_W = null;
				_DCreateedDate_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCompanyEmailsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkCompanyEmailID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCompanyEmailsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblCompanyEmailsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkCompanyEmailID);
			p.SourceColumn = ColumnNames.PkCompanyEmailID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkCompanyEmailID);
			p.SourceColumn = ColumnNames.PkCompanyEmailID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.CEmails);
			p.SourceColumn = ColumnNames.CEmails;
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

			p = cmd.Parameters.Add(Parameters.DCreateedDate);
			p.SourceColumn = ColumnNames.DCreateedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
