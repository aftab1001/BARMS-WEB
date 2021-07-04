using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblIncomTypes : LC.Model.BMS.DAL._tblIncomTypes
    {
        public void CheckIncomeType(string strIncomeType)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SIncomType.Value = strIncomeType;
            this.Query.Load();

        }
        public void GetActiveIncomeTypes(int year, int month, int day)
        {
            string query = string.Empty;
            query += " select itype .* from dbo.tblIncomTypes itype ";
            query += " where itype.bisactive = 1 and  itype.pkIncomeTypeID ";
            query += " not in(select i.fkIncomeTypeID from dbo.tblIncome i ";
            query += " where i.fkIncomeTypeID = itype.pkIncomeTypeID ";
            query += " and year(i.dIncomeDate) = '" + year + "' ";
            query += " and month(i.dIncomeDate) = '" + month + "' ";
            query += " and day(i.dIncomeDate) = '" + day + "' )";
            this.LoadFromRawSql(query, true);
        }

    }
}
