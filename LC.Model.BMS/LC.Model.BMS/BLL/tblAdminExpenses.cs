using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblAdminExpenses : LC.Model.BMS.DAL._tblAdminExpenses
    {
        public void GetExpenses(int year, int month, int day)
        {
            string query = string.Empty;
            query += " select * from dbo.tblAdminExpenses ";
          
            query += " where ";
            query += " year(tblAdminExpenses.bDateCreate) = '" + year + "' ";
            query += " and month(tblAdminExpenses.bDateCreate) = '" + month + "' ";
            query += " and day(tblAdminExpenses.bDateCreate) = '" + day + "' ";
            this.LoadFromRawSql(query);
        }
    }
   
}
