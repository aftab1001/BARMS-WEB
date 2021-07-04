using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblTransactions : LC.Model.BMS.DAL._tblTransactions
    {
        public void getAllTransactions(DateTime startDate, DateTime endDate)
        {
            string query = string.Empty;
            query += "select u.sfirstname + isnull(u.slastname,'') as FullName,t.* from dbo.tblTransactions t inner join tblusers u on u.pkuserid = t.fkdepartmentadminid ";
            query += " where t.dModifiedDate >= '" + startDate + "' and t.dModifiedDate <= '" + endDate + "' order by t.dModifiedDate desc";
            this.LoadFromRawSql(query);
        }
        public void getAllTransactions_without_condition()
        {
            string query = string.Empty;
            query += "select u.sfirstname + isnull(u.slastname,'') as FullName,t.* from dbo.tblTransactions t inner join tblusers u on u.pkuserid = t.fkdepartmentadminid ";
            this.LoadFromRawSql(query);
        }
    }
}
