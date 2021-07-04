
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
	public abstract class _tblSupplierEmails : SqlClientEntity
	{
		public _tblSupplierEmails()
		{
			this.QuerySource = "tblSupplierEmails";
			this.MappingName = "tblSupplierEmails";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSupplierEmailsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int PkSupplierEmails)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.PkSupplierEmails, PkSupplierEmails);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblSupplierEmailsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter PkSupplierEmails
			{
				get
				{
					return new SqlParameter("@PkSupplierEmails", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter FkSupplierID
			{
				get
				{
					return new SqlParameter("@FkSupplierID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter SEmail
			{
				get
				{
					return new SqlParameter("@SEmail", SqlDbType.NVarChar, 50);
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
            public const string PkSupplierEmails = "pkSupplierEmails";
            public const string FkSupplierID = "fkSupplierID";
            public const string SEmail = "sEmail";
            public const string BIsPrimary = "bIsPrimary";
            public const string DModifiedDate = "dModifiedDate";
            public const string DCreatedDate = "dCreatedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSupplierEmails] = _tblSupplierEmails.PropertyNames.PkSupplierEmails;
					ht[FkSupplierID] = _tblSupplierEmails.PropertyNames.FkSupplierID;
					ht[SEmail] = _tblSupplierEmails.PropertyNames.SEmail;
					ht[BIsPrimary] = _tblSupplierEmails.PropertyNames.BIsPrimary;
					ht[DModifiedDate] = _tblSupplierEmails.PropertyNames.DModifiedDate;
					ht[DCreatedDate] = _tblSupplierEmails.PropertyNames.DCreatedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string PkSupplierEmails = "PkSupplierEmails";
            public const string FkSupplierID = "FkSupplierID";
            public const string SEmail = "SEmail";
            public const string BIsPrimary = "BIsPrimary";
            public const string DModifiedDate = "DModifiedDate";
            public const string DCreatedDate = "DCreatedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[PkSupplierEmails] = _tblSupplierEmails.ColumnNames.PkSupplierEmails;
					ht[FkSupplierID] = _tblSupplierEmails.ColumnNames.FkSupplierID;
					ht[SEmail] = _tblSupplierEmails.ColumnNames.SEmail;
					ht[BIsPrimary] = _tblSupplierEmails.ColumnNames.BIsPrimary;
					ht[DModifiedDate] = _tblSupplierEmails.ColumnNames.DModifiedDate;
					ht[DCreatedDate] = _tblSupplierEmails.ColumnNames.DCreatedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string PkSupplierEmails = "s_PkSupplierEmails";
            public const string FkSupplierID = "s_FkSupplierID";
            public const string SEmail = "s_SEmail";
            public const string BIsPrimary = "s_BIsPrimary";
            public const string DModifiedDate = "s_DModifiedDate";
            public const string DCreatedDate = "s_DCreatedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int PkSupplierEmails
	    {
			get
	        {
				return base.Getint(ColumnNames.PkSupplierEmails);
			}
			set
	        {
				base.Setint(ColumnNames.PkSupplierEmails, value);
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

		public virtual string SEmail
	    {
			get
	        {
				return base.Getstring(ColumnNames.SEmail);
			}
			set
	        {
				base.Setstring(ColumnNames.SEmail, value);
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
	
		public virtual string s_PkSupplierEmails
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.PkSupplierEmails) ? string.Empty : base.GetintAsString(ColumnNames.PkSupplierEmails);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.PkSupplierEmails);
				else
					this.PkSupplierEmails = base.SetintAsString(ColumnNames.PkSupplierEmails, value);
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

		public virtual string s_SEmail
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.SEmail) ? string.Empty : base.GetstringAsString(ColumnNames.SEmail);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.SEmail);
				else
					this.SEmail = base.SetstringAsString(ColumnNames.SEmail, value);
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
				
				
				public WhereParameter PkSupplierEmails
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.PkSupplierEmails, Parameters.PkSupplierEmails);
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

				public WhereParameter SEmail
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.SEmail, Parameters.SEmail);
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
		
			public WhereParameter PkSupplierEmails
		    {
				get
		        {
					if(_PkSupplierEmails_W == null)
	        	    {
						_PkSupplierEmails_W = TearOff.PkSupplierEmails;
					}
					return _PkSupplierEmails_W;
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

			public WhereParameter SEmail
		    {
				get
		        {
					if(_SEmail_W == null)
	        	    {
						_SEmail_W = TearOff.SEmail;
					}
					return _SEmail_W;
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

			private WhereParameter _PkSupplierEmails_W = null;
			private WhereParameter _FkSupplierID_W = null;
			private WhereParameter _SEmail_W = null;
			private WhereParameter _BIsPrimary_W = null;
			private WhereParameter _DModifiedDate_W = null;
			private WhereParameter _DCreatedDate_W = null;

			public void WhereClauseReset()
			{
				_PkSupplierEmails_W = null;
				_FkSupplierID_W = null;
				_SEmail_W = null;
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
				
				
				public AggregateParameter PkSupplierEmails
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.PkSupplierEmails, Parameters.PkSupplierEmails);
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

				public AggregateParameter SEmail
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.SEmail, Parameters.SEmail);
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
		
			public AggregateParameter PkSupplierEmails
		    {
				get
		        {
					if(_PkSupplierEmails_W == null)
	        	    {
						_PkSupplierEmails_W = TearOff.PkSupplierEmails;
					}
					return _PkSupplierEmails_W;
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

			public AggregateParameter SEmail
		    {
				get
		        {
					if(_SEmail_W == null)
	        	    {
						_SEmail_W = TearOff.SEmail;
					}
					return _SEmail_W;
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

			private AggregateParameter _PkSupplierEmails_W = null;
			private AggregateParameter _FkSupplierID_W = null;
			private AggregateParameter _SEmail_W = null;
			private AggregateParameter _BIsPrimary_W = null;
			private AggregateParameter _DModifiedDate_W = null;
			private AggregateParameter _DCreatedDate_W = null;

			public void AggregateClauseReset()
			{
				_PkSupplierEmails_W = null;
				_FkSupplierID_W = null;
				_SEmail_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierEmailsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.PkSupplierEmails.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierEmailsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblSupplierEmailsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.PkSupplierEmails);
			p.SourceColumn = ColumnNames.PkSupplierEmails;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.PkSupplierEmails);
			p.SourceColumn = ColumnNames.PkSupplierEmails;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.FkSupplierID);
			p.SourceColumn = ColumnNames.FkSupplierID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.SEmail);
			p.SourceColumn = ColumnNames.SEmail;
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
