
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
	public abstract class _tblColorCodes : SqlClientEntity
	{
		public _tblColorCodes()
		{
			this.QuerySource = "tblColorCodes";
			this.MappingName = "tblColorCodes";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblColorCodesLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int Pkcolorcodeid)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.Pkcolorcodeid, Pkcolorcodeid);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblColorCodesLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter Pkcolorcodeid
			{
				get
				{
					return new SqlParameter("@Pkcolorcodeid", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter ColorCode
			{
				get
				{
					return new SqlParameter("@ColorCode", SqlDbType.NVarChar, 50);
				}
			}
			
			public static SqlParameter Fkuserid
			{
				get
				{
					return new SqlParameter("@Fkuserid", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter DCreateDate
			{
				get
				{
					return new SqlParameter("@DCreateDate", SqlDbType.DateTime, 0);
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
            public const string Pkcolorcodeid = "pkcolorcodeid";
            public const string ColorCode = "ColorCode";
            public const string Fkuserid = "fkuserid";
            public const string DCreateDate = "dCreateDate";
            public const string DModifiedDate = "dModifiedDate";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[Pkcolorcodeid] = _tblColorCodes.PropertyNames.Pkcolorcodeid;
					ht[ColorCode] = _tblColorCodes.PropertyNames.ColorCode;
					ht[Fkuserid] = _tblColorCodes.PropertyNames.Fkuserid;
					ht[DCreateDate] = _tblColorCodes.PropertyNames.DCreateDate;
					ht[DModifiedDate] = _tblColorCodes.PropertyNames.DModifiedDate;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string Pkcolorcodeid = "Pkcolorcodeid";
            public const string ColorCode = "ColorCode";
            public const string Fkuserid = "Fkuserid";
            public const string DCreateDate = "DCreateDate";
            public const string DModifiedDate = "DModifiedDate";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[Pkcolorcodeid] = _tblColorCodes.ColumnNames.Pkcolorcodeid;
					ht[ColorCode] = _tblColorCodes.ColumnNames.ColorCode;
					ht[Fkuserid] = _tblColorCodes.ColumnNames.Fkuserid;
					ht[DCreateDate] = _tblColorCodes.ColumnNames.DCreateDate;
					ht[DModifiedDate] = _tblColorCodes.ColumnNames.DModifiedDate;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string Pkcolorcodeid = "s_Pkcolorcodeid";
            public const string ColorCode = "s_ColorCode";
            public const string Fkuserid = "s_Fkuserid";
            public const string DCreateDate = "s_DCreateDate";
            public const string DModifiedDate = "s_DModifiedDate";

		}
		#endregion		
		
		#region Properties
	
		public virtual int Pkcolorcodeid
	    {
			get
	        {
				return base.Getint(ColumnNames.Pkcolorcodeid);
			}
			set
	        {
				base.Setint(ColumnNames.Pkcolorcodeid, value);
			}
		}

		public virtual string ColorCode
	    {
			get
	        {
				return base.Getstring(ColumnNames.ColorCode);
			}
			set
	        {
				base.Setstring(ColumnNames.ColorCode, value);
			}
		}

		public virtual int Fkuserid
	    {
			get
	        {
				return base.Getint(ColumnNames.Fkuserid);
			}
			set
	        {
				base.Setint(ColumnNames.Fkuserid, value);
			}
		}

		public virtual DateTime DCreateDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.DCreateDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.DCreateDate, value);
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
	
		public virtual string s_Pkcolorcodeid
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Pkcolorcodeid) ? string.Empty : base.GetintAsString(ColumnNames.Pkcolorcodeid);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Pkcolorcodeid);
				else
					this.Pkcolorcodeid = base.SetintAsString(ColumnNames.Pkcolorcodeid, value);
			}
		}

		public virtual string s_ColorCode
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.ColorCode) ? string.Empty : base.GetstringAsString(ColumnNames.ColorCode);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.ColorCode);
				else
					this.ColorCode = base.SetstringAsString(ColumnNames.ColorCode, value);
			}
		}

		public virtual string s_Fkuserid
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.Fkuserid) ? string.Empty : base.GetintAsString(ColumnNames.Fkuserid);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.Fkuserid);
				else
					this.Fkuserid = base.SetintAsString(ColumnNames.Fkuserid, value);
			}
		}

		public virtual string s_DCreateDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.DCreateDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.DCreateDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.DCreateDate);
				else
					this.DCreateDate = base.SetDateTimeAsString(ColumnNames.DCreateDate, value);
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
				
				
				public WhereParameter Pkcolorcodeid
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Pkcolorcodeid, Parameters.Pkcolorcodeid);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter ColorCode
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.ColorCode, Parameters.ColorCode);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter Fkuserid
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.Fkuserid, Parameters.Fkuserid);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter DCreateDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.DCreateDate, Parameters.DCreateDate);
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
		
			public WhereParameter Pkcolorcodeid
		    {
				get
		        {
					if(_Pkcolorcodeid_W == null)
	        	    {
						_Pkcolorcodeid_W = TearOff.Pkcolorcodeid;
					}
					return _Pkcolorcodeid_W;
				}
			}

			public WhereParameter ColorCode
		    {
				get
		        {
					if(_ColorCode_W == null)
	        	    {
						_ColorCode_W = TearOff.ColorCode;
					}
					return _ColorCode_W;
				}
			}

			public WhereParameter Fkuserid
		    {
				get
		        {
					if(_Fkuserid_W == null)
	        	    {
						_Fkuserid_W = TearOff.Fkuserid;
					}
					return _Fkuserid_W;
				}
			}

			public WhereParameter DCreateDate
		    {
				get
		        {
					if(_DCreateDate_W == null)
	        	    {
						_DCreateDate_W = TearOff.DCreateDate;
					}
					return _DCreateDate_W;
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

			private WhereParameter _Pkcolorcodeid_W = null;
			private WhereParameter _ColorCode_W = null;
			private WhereParameter _Fkuserid_W = null;
			private WhereParameter _DCreateDate_W = null;
			private WhereParameter _DModifiedDate_W = null;

			public void WhereClauseReset()
			{
				_Pkcolorcodeid_W = null;
				_ColorCode_W = null;
				_Fkuserid_W = null;
				_DCreateDate_W = null;
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
				
				
				public AggregateParameter Pkcolorcodeid
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Pkcolorcodeid, Parameters.Pkcolorcodeid);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter ColorCode
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.ColorCode, Parameters.ColorCode);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter Fkuserid
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.Fkuserid, Parameters.Fkuserid);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter DCreateDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.DCreateDate, Parameters.DCreateDate);
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
		
			public AggregateParameter Pkcolorcodeid
		    {
				get
		        {
					if(_Pkcolorcodeid_W == null)
	        	    {
						_Pkcolorcodeid_W = TearOff.Pkcolorcodeid;
					}
					return _Pkcolorcodeid_W;
				}
			}

			public AggregateParameter ColorCode
		    {
				get
		        {
					if(_ColorCode_W == null)
	        	    {
						_ColorCode_W = TearOff.ColorCode;
					}
					return _ColorCode_W;
				}
			}

			public AggregateParameter Fkuserid
		    {
				get
		        {
					if(_Fkuserid_W == null)
	        	    {
						_Fkuserid_W = TearOff.Fkuserid;
					}
					return _Fkuserid_W;
				}
			}

			public AggregateParameter DCreateDate
		    {
				get
		        {
					if(_DCreateDate_W == null)
	        	    {
						_DCreateDate_W = TearOff.DCreateDate;
					}
					return _DCreateDate_W;
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

			private AggregateParameter _Pkcolorcodeid_W = null;
			private AggregateParameter _ColorCode_W = null;
			private AggregateParameter _Fkuserid_W = null;
			private AggregateParameter _DCreateDate_W = null;
			private AggregateParameter _DModifiedDate_W = null;

			public void AggregateClauseReset()
			{
				_Pkcolorcodeid_W = null;
				_ColorCode_W = null;
				_Fkuserid_W = null;
				_DCreateDate_W = null;
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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblColorCodesInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.Pkcolorcodeid.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblColorCodesUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblColorCodesDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.Pkcolorcodeid);
			p.SourceColumn = ColumnNames.Pkcolorcodeid;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.Pkcolorcodeid);
			p.SourceColumn = ColumnNames.Pkcolorcodeid;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.ColorCode);
			p.SourceColumn = ColumnNames.ColorCode;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.Fkuserid);
			p.SourceColumn = ColumnNames.Fkuserid;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DCreateDate);
			p.SourceColumn = ColumnNames.DCreateDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.DModifiedDate);
			p.SourceColumn = ColumnNames.DModifiedDate;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
