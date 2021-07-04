
using System;
using System.Collections.Generic;
using System.Text;
using MyGeneration.dOOdads;

namespace LC.Model.BMS.BLL
{
    public class tblDays : LC.Model.BMS.DAL._tblDays
    {
        public void CheckDoublecatio(DateTime DateDay, int fkManagerUserID,int pkDepartmentID)
        {
            string query = string.Empty;
            query += "select * from tblDays where DateDay='" + DateDay + "' and fkManagerUserID='" + fkManagerUserID + "' and pkDepartmentID='" + pkDepartmentID + "'";
            this.LoadFromRawSql(query);
        }
        public void getSalaryandBonus(int UserId)
        {
            string query = string.Empty;
            query += "select * from tblUserWorkshifts where pkUserWorkshiftID= (SELECT MAX(pkUserWorkshiftID) AS Expr1 ";
            query += " FROM  tblUserWorkshifts ";
            query += " WHERE (fkUserID = '" + UserId + "') AND (Bonus > 0))and bOnTime = '1' and bLate='0'";
            this.LoadFromRawSql(query);
        }
    }
}
