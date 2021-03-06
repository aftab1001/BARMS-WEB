
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
    public abstract class _tblAdminExpenses : SqlClientEntity
    {
        public _tblAdminExpenses()
        {
            this.QuerySource = "tblAdminExpenses";
            this.MappingName = "tblAdminExpenses";

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

            return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblAdminExpensesLoadAll]", parameters);
        }

        //=================================================================
        // public Overridable Function LoadByPrimaryKey()  As Boolean
        //=================================================================
        //  Loads a single row of via the primary key
        //=================================================================
        public virtual bool LoadByPrimaryKey(int ExpId)
        {
            ListDictionary parameters = new ListDictionary();
            parameters.Add(Parameters.ExpId, ExpId);


            return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblAdminExpensesLoadByPrimaryKey]", parameters);
        }

        #region Parameters
        protected class Parameters
        {

            public static SqlParameter ExpId
            {
                get
                {
                    return new SqlParameter("@ExpId", SqlDbType.Int, 0);
                }
            }

            public static SqlParameter Amount
            {
                get
                {
                    return new SqlParameter("@Amount", SqlDbType.Float, 0);
                }
            }

            public static SqlParameter Note
            {
                get
                {
                    return new SqlParameter("@Note", SqlDbType.Text, 2147483647);
                }
            }

            public static SqlParameter CreatedDate
            {
                get
                {
                    return new SqlParameter("@CreatedDate", SqlDbType.DateTime, 0);
                }
            }

            public static SqlParameter ModifyDate
            {
                get
                {
                    return new SqlParameter("@ModifyDate", SqlDbType.DateTime, 0);
                }
            }

            public static SqlParameter IsPaid
            {
                get
                {
                    return new SqlParameter("@IsPaid", SqlDbType.Bit, 0);
                }
            }

            public static SqlParameter BDateCreate
            {
                get
                {
                    return new SqlParameter("@BDateCreate", SqlDbType.DateTime, 0);
                }
            }

            public static SqlParameter ExpenseName
            {
                get
                {
                    return new SqlParameter("@ExpenseName", SqlDbType.VarChar, 550);
                }
            }

        }
        #endregion

        #region ColumnNames
        public class ColumnNames
        {
            public const string ExpId = "ExpId";
            public const string Amount = "Amount";
            public const string Note = "Note";
            public const string CreatedDate = "CreatedDate";
            public const string ModifyDate = "ModifyDate";
            public const string IsPaid = "IsPaid";
            public const string BDateCreate = "bDateCreate";
            public const string ExpenseName = "ExpenseName";

            static public string ToPropertyName(string columnName)
            {
                if (ht == null)
                {
                    ht = new Hashtable();

                    ht[ExpId] = _tblAdminExpenses.PropertyNames.ExpId;
                    ht[Amount] = _tblAdminExpenses.PropertyNames.Amount;
                    ht[Note] = _tblAdminExpenses.PropertyNames.Note;
                    ht[CreatedDate] = _tblAdminExpenses.PropertyNames.CreatedDate;
                    ht[ModifyDate] = _tblAdminExpenses.PropertyNames.ModifyDate;
                    ht[IsPaid] = _tblAdminExpenses.PropertyNames.IsPaid;
                    ht[BDateCreate] = _tblAdminExpenses.PropertyNames.BDateCreate;
                    ht[ExpenseName] = _tblAdminExpenses.PropertyNames.ExpenseName;

                }
                return (string)ht[columnName];
            }

            static private Hashtable ht = null;
        }
        #endregion

        #region PropertyNames
        public class PropertyNames
        {
            public const string ExpId = "ExpId";
            public const string Amount = "Amount";
            public const string Note = "Note";
            public const string CreatedDate = "CreatedDate";
            public const string ModifyDate = "ModifyDate";
            public const string IsPaid = "IsPaid";
            public const string BDateCreate = "BDateCreate";
            public const string ExpenseName = "ExpenseName";

            static public string ToColumnName(string propertyName)
            {
                if (ht == null)
                {
                    ht = new Hashtable();

                    ht[ExpId] = _tblAdminExpenses.ColumnNames.ExpId;
                    ht[Amount] = _tblAdminExpenses.ColumnNames.Amount;
                    ht[Note] = _tblAdminExpenses.ColumnNames.Note;
                    ht[CreatedDate] = _tblAdminExpenses.ColumnNames.CreatedDate;
                    ht[ModifyDate] = _tblAdminExpenses.ColumnNames.ModifyDate;
                    ht[IsPaid] = _tblAdminExpenses.ColumnNames.IsPaid;
                    ht[BDateCreate] = _tblAdminExpenses.ColumnNames.BDateCreate;
                    ht[ExpenseName] = _tblAdminExpenses.ColumnNames.ExpenseName;

                }
                return (string)ht[propertyName];
            }

            static private Hashtable ht = null;
        }
        #endregion

        #region StringPropertyNames
        public class StringPropertyNames
        {
            public const string ExpId = "s_ExpId";
            public const string Amount = "s_Amount";
            public const string Note = "s_Note";
            public const string CreatedDate = "s_CreatedDate";
            public const string ModifyDate = "s_ModifyDate";
            public const string IsPaid = "s_IsPaid";
            public const string BDateCreate = "s_BDateCreate";
            public const string ExpenseName = "s_ExpenseName";

        }
        #endregion

        #region Properties

        public virtual int ExpId
        {
            get
            {
                return base.Getint(ColumnNames.ExpId);
            }
            set
            {
                base.Setint(ColumnNames.ExpId, value);
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

        public virtual string Note
        {
            get
            {
                return base.Getstring(ColumnNames.Note);
            }
            set
            {
                base.Setstring(ColumnNames.Note, value);
            }
        }

        public virtual DateTime CreatedDate
        {
            get
            {
                return base.GetDateTime(ColumnNames.CreatedDate);
            }
            set
            {
                base.SetDateTime(ColumnNames.CreatedDate, value);
            }
        }

        public virtual DateTime ModifyDate
        {
            get
            {
                return base.GetDateTime(ColumnNames.ModifyDate);
            }
            set
            {
                base.SetDateTime(ColumnNames.ModifyDate, value);
            }
        }

        public virtual bool IsPaid
        {
            get
            {
                return base.Getbool(ColumnNames.IsPaid);
            }
            set
            {
                base.Setbool(ColumnNames.IsPaid, value);
            }
        }

        public virtual DateTime BDateCreate
        {
            get
            {
                return base.GetDateTime(ColumnNames.BDateCreate);
            }
            set
            {
                base.SetDateTime(ColumnNames.BDateCreate, value);
            }
        }

        public virtual string ExpenseName
        {
            get
            {
                return base.Getstring(ColumnNames.ExpenseName);
            }
            set
            {
                base.Setstring(ColumnNames.ExpenseName, value);
            }
        }


        #endregion

        #region String Properties

        public virtual string s_ExpId
        {
            get
            {
                return this.IsColumnNull(ColumnNames.ExpId) ? string.Empty : base.GetintAsString(ColumnNames.ExpId);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.ExpId);
                else
                    this.ExpId = base.SetintAsString(ColumnNames.ExpId, value);
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
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.Amount);
                else
                    this.Amount = base.SetdoubleAsString(ColumnNames.Amount, value);
            }
        }

        public virtual string s_Note
        {
            get
            {
                return this.IsColumnNull(ColumnNames.Note) ? string.Empty : base.GetstringAsString(ColumnNames.Note);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.Note);
                else
                    this.Note = base.SetstringAsString(ColumnNames.Note, value);
            }
        }

        public virtual string s_CreatedDate
        {
            get
            {
                return this.IsColumnNull(ColumnNames.CreatedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.CreatedDate);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.CreatedDate);
                else
                    this.CreatedDate = base.SetDateTimeAsString(ColumnNames.CreatedDate, value);
            }
        }

        public virtual string s_ModifyDate
        {
            get
            {
                return this.IsColumnNull(ColumnNames.ModifyDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.ModifyDate);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.ModifyDate);
                else
                    this.ModifyDate = base.SetDateTimeAsString(ColumnNames.ModifyDate, value);
            }
        }

        public virtual string s_IsPaid
        {
            get
            {
                return this.IsColumnNull(ColumnNames.IsPaid) ? string.Empty : base.GetboolAsString(ColumnNames.IsPaid);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.IsPaid);
                else
                    this.IsPaid = base.SetboolAsString(ColumnNames.IsPaid, value);
            }
        }

        public virtual string s_BDateCreate
        {
            get
            {
                return this.IsColumnNull(ColumnNames.BDateCreate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.BDateCreate);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.BDateCreate);
                else
                    this.BDateCreate = base.SetDateTimeAsString(ColumnNames.BDateCreate, value);
            }
        }

        public virtual string s_ExpenseName
        {
            get
            {
                return this.IsColumnNull(ColumnNames.ExpenseName) ? string.Empty : base.GetstringAsString(ColumnNames.ExpenseName);
            }
            set
            {
                if (string.Empty == value)
                    this.SetColumnNull(ColumnNames.ExpenseName);
                else
                    this.ExpenseName = base.SetstringAsString(ColumnNames.ExpenseName, value);
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
                    if (_tearOff == null)
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


                public WhereParameter ExpId
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.ExpId, Parameters.ExpId);
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

                public WhereParameter Note
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.Note, Parameters.Note);
                        this._clause._entity.Query.AddWhereParameter(where);
                        return where;
                    }
                }

                public WhereParameter CreatedDate
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.CreatedDate, Parameters.CreatedDate);
                        this._clause._entity.Query.AddWhereParameter(where);
                        return where;
                    }
                }

                public WhereParameter ModifyDate
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.ModifyDate, Parameters.ModifyDate);
                        this._clause._entity.Query.AddWhereParameter(where);
                        return where;
                    }
                }

                public WhereParameter IsPaid
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.IsPaid, Parameters.IsPaid);
                        this._clause._entity.Query.AddWhereParameter(where);
                        return where;
                    }
                }

                public WhereParameter BDateCreate
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.BDateCreate, Parameters.BDateCreate);
                        this._clause._entity.Query.AddWhereParameter(where);
                        return where;
                    }
                }

                public WhereParameter ExpenseName
                {
                    get
                    {
                        WhereParameter where = new WhereParameter(ColumnNames.ExpenseName, Parameters.ExpenseName);
                        this._clause._entity.Query.AddWhereParameter(where);
                        return where;
                    }
                }


                private WhereClause _clause;
            }
            #endregion

            public WhereParameter ExpId
            {
                get
                {
                    if (_ExpId_W == null)
                    {
                        _ExpId_W = TearOff.ExpId;
                    }
                    return _ExpId_W;
                }
            }

            public WhereParameter Amount
            {
                get
                {
                    if (_Amount_W == null)
                    {
                        _Amount_W = TearOff.Amount;
                    }
                    return _Amount_W;
                }
            }

            public WhereParameter Note
            {
                get
                {
                    if (_Note_W == null)
                    {
                        _Note_W = TearOff.Note;
                    }
                    return _Note_W;
                }
            }

            public WhereParameter CreatedDate
            {
                get
                {
                    if (_CreatedDate_W == null)
                    {
                        _CreatedDate_W = TearOff.CreatedDate;
                    }
                    return _CreatedDate_W;
                }
            }

            public WhereParameter ModifyDate
            {
                get
                {
                    if (_ModifyDate_W == null)
                    {
                        _ModifyDate_W = TearOff.ModifyDate;
                    }
                    return _ModifyDate_W;
                }
            }

            public WhereParameter IsPaid
            {
                get
                {
                    if (_IsPaid_W == null)
                    {
                        _IsPaid_W = TearOff.IsPaid;
                    }
                    return _IsPaid_W;
                }
            }

            public WhereParameter BDateCreate
            {
                get
                {
                    if (_BDateCreate_W == null)
                    {
                        _BDateCreate_W = TearOff.BDateCreate;
                    }
                    return _BDateCreate_W;
                }
            }

            public WhereParameter ExpenseName
            {
                get
                {
                    if (_ExpenseName_W == null)
                    {
                        _ExpenseName_W = TearOff.ExpenseName;
                    }
                    return _ExpenseName_W;
                }
            }

            private WhereParameter _ExpId_W = null;
            private WhereParameter _Amount_W = null;
            private WhereParameter _Note_W = null;
            private WhereParameter _CreatedDate_W = null;
            private WhereParameter _ModifyDate_W = null;
            private WhereParameter _IsPaid_W = null;
            private WhereParameter _BDateCreate_W = null;
            private WhereParameter _ExpenseName_W = null;

            public void WhereClauseReset()
            {
                _ExpId_W = null;
                _Amount_W = null;
                _Note_W = null;
                _CreatedDate_W = null;
                _ModifyDate_W = null;
                _IsPaid_W = null;
                _BDateCreate_W = null;
                _ExpenseName_W = null;

                this._entity.Query.FlushWhereParameters();

            }

            private BusinessEntity _entity;
            private TearOffWhereParameter _tearOff;

        }

        public WhereClause Where
        {
            get
            {
                if (_whereClause == null)
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
                    if (_tearOff == null)
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


                public AggregateParameter ExpId
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.ExpId, Parameters.ExpId);
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

                public AggregateParameter Note
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.Note, Parameters.Note);
                        this._clause._entity.Query.AddAggregateParameter(aggregate);
                        return aggregate;
                    }
                }

                public AggregateParameter CreatedDate
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.CreatedDate, Parameters.CreatedDate);
                        this._clause._entity.Query.AddAggregateParameter(aggregate);
                        return aggregate;
                    }
                }

                public AggregateParameter ModifyDate
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.ModifyDate, Parameters.ModifyDate);
                        this._clause._entity.Query.AddAggregateParameter(aggregate);
                        return aggregate;
                    }
                }

                public AggregateParameter IsPaid
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.IsPaid, Parameters.IsPaid);
                        this._clause._entity.Query.AddAggregateParameter(aggregate);
                        return aggregate;
                    }
                }

                public AggregateParameter BDateCreate
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.BDateCreate, Parameters.BDateCreate);
                        this._clause._entity.Query.AddAggregateParameter(aggregate);
                        return aggregate;
                    }
                }

                public AggregateParameter ExpenseName
                {
                    get
                    {
                        AggregateParameter aggregate = new AggregateParameter(ColumnNames.ExpenseName, Parameters.ExpenseName);
                        this._clause._entity.Query.AddAggregateParameter(aggregate);
                        return aggregate;
                    }
                }


                private AggregateClause _clause;
            }
            #endregion

            public AggregateParameter ExpId
            {
                get
                {
                    if (_ExpId_W == null)
                    {
                        _ExpId_W = TearOff.ExpId;
                    }
                    return _ExpId_W;
                }
            }

            public AggregateParameter Amount
            {
                get
                {
                    if (_Amount_W == null)
                    {
                        _Amount_W = TearOff.Amount;
                    }
                    return _Amount_W;
                }
            }

            public AggregateParameter Note
            {
                get
                {
                    if (_Note_W == null)
                    {
                        _Note_W = TearOff.Note;
                    }
                    return _Note_W;
                }
            }

            public AggregateParameter CreatedDate
            {
                get
                {
                    if (_CreatedDate_W == null)
                    {
                        _CreatedDate_W = TearOff.CreatedDate;
                    }
                    return _CreatedDate_W;
                }
            }

            public AggregateParameter ModifyDate
            {
                get
                {
                    if (_ModifyDate_W == null)
                    {
                        _ModifyDate_W = TearOff.ModifyDate;
                    }
                    return _ModifyDate_W;
                }
            }

            public AggregateParameter IsPaid
            {
                get
                {
                    if (_IsPaid_W == null)
                    {
                        _IsPaid_W = TearOff.IsPaid;
                    }
                    return _IsPaid_W;
                }
            }

            public AggregateParameter BDateCreate
            {
                get
                {
                    if (_BDateCreate_W == null)
                    {
                        _BDateCreate_W = TearOff.BDateCreate;
                    }
                    return _BDateCreate_W;
                }
            }

            public AggregateParameter ExpenseName
            {
                get
                {
                    if (_ExpenseName_W == null)
                    {
                        _ExpenseName_W = TearOff.ExpenseName;
                    }
                    return _ExpenseName_W;
                }
            }

            private AggregateParameter _ExpId_W = null;
            private AggregateParameter _Amount_W = null;
            private AggregateParameter _Note_W = null;
            private AggregateParameter _CreatedDate_W = null;
            private AggregateParameter _ModifyDate_W = null;
            private AggregateParameter _IsPaid_W = null;
            private AggregateParameter _BDateCreate_W = null;
            private AggregateParameter _ExpenseName_W = null;

            public void AggregateClauseReset()
            {
                _ExpId_W = null;
                _Amount_W = null;
                _Note_W = null;
                _CreatedDate_W = null;
                _ModifyDate_W = null;
                _IsPaid_W = null;
                _BDateCreate_W = null;
                _ExpenseName_W = null;

                this._entity.Query.FlushAggregateParameters();

            }

            private BusinessEntity _entity;
            private TearOffAggregateParameter _tearOff;

        }

        public AggregateClause Aggregate
        {
            get
            {
                if (_aggregateClause == null)
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
            cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblAdminExpensesInsert]";

            CreateParameters(cmd);

            SqlParameter p;
            p = cmd.Parameters[Parameters.ExpId.ParameterName];
            p.Direction = ParameterDirection.Output;

            return cmd;
        }

        protected override IDbCommand GetUpdateCommand()
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblAdminExpensesUpdate]";

            CreateParameters(cmd);

            return cmd;
        }

        protected override IDbCommand GetDeleteCommand()
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblAdminExpensesDelete]";

            SqlParameter p;
            p = cmd.Parameters.Add(Parameters.ExpId);
            p.SourceColumn = ColumnNames.ExpId;
            p.SourceVersion = DataRowVersion.Current;


            return cmd;
        }

        private IDbCommand CreateParameters(SqlCommand cmd)
        {
            SqlParameter p;

            p = cmd.Parameters.Add(Parameters.ExpId);
            p.SourceColumn = ColumnNames.ExpId;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.Amount);
            p.SourceColumn = ColumnNames.Amount;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.Note);
            p.SourceColumn = ColumnNames.Note;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.CreatedDate);
            p.SourceColumn = ColumnNames.CreatedDate;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.ModifyDate);
            p.SourceColumn = ColumnNames.ModifyDate;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.IsPaid);
            p.SourceColumn = ColumnNames.IsPaid;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.BDateCreate);
            p.SourceColumn = ColumnNames.BDateCreate;
            p.SourceVersion = DataRowVersion.Current;

            p = cmd.Parameters.Add(Parameters.ExpenseName);
            p.SourceColumn = ColumnNames.ExpenseName;
            p.SourceVersion = DataRowVersion.Current;


            return cmd;
        }
    }
}
