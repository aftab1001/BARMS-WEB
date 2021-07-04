using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblExpanseCategory : LC.Model.BMS.DAL._tblExpanseCategory
    {
        public void CheckExpanseCategory(string expanse)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SExpanseCategory.Value = expanse;
            this.Query.Load();

        }
        public void GetActiveExpenseCategories(int year, int month, int day)
        {
            string query = string.Empty;
            query += " select ecat.* from dbo.tblExpanseCategory ecat ";
            query += " where ecat.bIsActive = 1 and ecat.pkExpanseCategoryID ";
            query += " not in (select ex.fkExpanseCategoryID from  dbo.tblExpanses ex ";
            query += " where ex.fkExpanseCategoryID = ecat.pkExpanseCategoryID ";
            query += " and year(ex.dCreateDate) = '" + year + "' ";
            query += " and month(ex.dCreateDate) = '" + month + "' ";
            query += " and day(ex.dCreateDate) = '" + day + "' )";
            this.LoadFromRawSql(query);
        }
    }
}
