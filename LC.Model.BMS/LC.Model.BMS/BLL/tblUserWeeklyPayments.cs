using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserWeeklyPayments : LC.Model.BMS.DAL._tblUserWeeklyPayments
    {

        public void GetWeeklySalariedUser(int userid, DateTime weekStart, DateTime weekEnd)
        {
            this.Where.FkUserID.Value = userid;
            this.Where.DWeekStartDate.Value = weekStart;
            this.Where.DWeekEndDate.Value = weekEnd;
            this.Query.Load();
        }
        public void getOneWeekUsers(int weeknumber, DateTime weekStart, DateTime weekEnd)
        {
            string query = string.Empty;
            query += " select uw.* from dbo.tblUserWeeklyPayments uw inner join dbo.tblUserDepartment ud on uw.fkUserID = ud.fkUserID     where uw.iweeknumber = " + weeknumber + " and uw.dweekstartdate='" + weekStart + "' and uw.dweekEndDate = '" + weekEnd + "' and ud.fkdepartmentid = 1";
            this.LoadFromRawSql(query);
        }
        public void getUniqueWeekUserStatus(int userid, DateTime weekStart, DateTime weekEnd)
        {
            string query = string.Empty;
            query += " select uw.* from dbo.tblUserWeeklyPayments uw where uw.dweekstartdate='" + weekStart + "' and uw.dweekEndDate = '" + weekEnd + "' and uw.fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }


    }
}
