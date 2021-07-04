using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace LC.Model.BMS.BLL
{
    public class tblExpanses : LC.Model.BMS.DAL._tblExpanses
    {
        public void CheckExpansCatInUse(int EcatID)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.FkExpanseCategoryID.Value = EcatID;
            this.Query.Load();
        }
        public void CheckExpenseTypeInUse(int ExpenseType, int year, int month, int day)
        {
            string query = string.Empty;
            query += " select e.* from dbo.tblExpanses e ";
            query += " where e.fkExpanseCategoryID = " + ExpenseType;
            query += " and year(e.dCreateDate) = '" + year + "' ";
            query += " and month(e.dCreateDate) = '" + month + "' ";
            query += " and day(e.dCreateDate) = '" + day + "' ";
            this.LoadFromRawSql(query);
        }
        public void GetExpenses(int year, int month, int day)
        {
            string query = string.Empty;
            query += " select e.*,eCat.* from dbo.tblExpanses e inner join ";
            query += " dbo.tblExpanseCategory eCat on eCat.pkExpanseCategoryID = e.fkExpanseCategoryID ";
            query += " where ";
            query += " year(e.dCreateDate) = '" + year + "' ";
            query += " and month(e.dCreateDate) = '" + month + "' ";
            query += " and day(e.dCreateDate) = '" + day + "' ";
            this.LoadFromRawSql(query);
        }
        public void GetExpenseByID(int expenseid)
        {
            string query = string.Empty;
            query += " select e.*,eCat.* from dbo.tblExpanses e inner join ";
            query += " dbo.tblExpanseCategory eCat on eCat.pkExpanseCategoryID = e.fkExpanseCategoryID ";
            query += " where e.pkExpanseID = " + expenseid;
            this.LoadFromRawSql(query);
        }
        public void GetAllExpensesByWeek(int weekNumber)
        {
            string query = string.Empty;
            query += " select sum(e.ExpanseAmount) as expense from dbo.tblExpanses e inner join ";
            query += " dbo.tblExpanseCategory eCat on eCat.pkExpanseCategoryID = e.fkExpanseCategoryID ";
            query += " where DATEPART(wk, e.dCreateDate) = " + weekNumber;
            this.LoadFromRawSql(query);
        }

        public void GetExpenseWeekStats(DateTime weekstart, DateTime weekend, int expenseType)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@expensetypeid", expenseType);
            this.LoadFromSql("spExpensesForStats", Params);
        }

    }
}
