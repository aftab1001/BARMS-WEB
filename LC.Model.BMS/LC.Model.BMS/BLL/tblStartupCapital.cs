using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblStartupCapital : LC.Model.BMS.DAL._tblStartupCapital
    {
        public void getAllStartupCapital()
        {
            string query = string.Empty;
            query += "select sc.* from dbo.tblStartupCapital sc ";
            query += "order by sc.dmodifieddate desc";
            this.LoadFromRawSql(query);
        }
        public void getAllStartupCapital_ByDatePeriod(DateTime startDate, DateTime endDate)
        {
            string query = string.Empty;
            query += " select sc.* from dbo.tblStartupCapital sc ";
            query += " where sc.dmodifieddate >= '" + startDate + "' and sc.dmodifieddate <= '" + endDate + "' ";
            query += " order by sc.dmodifieddate desc ";
            this.LoadFromRawSql(query);
        }
    }
}
