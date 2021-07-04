using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;

namespace LC.Model.BMS.BLL
{
    public class tblIncome : LC.Model.BMS.DAL._tblIncome
    {
        public void GetIncomeByWorkshiftID(int workshiftid)
        {
            string query = string.Empty;
            query += " select i.* from dbo.tblIncome i ";
            query += " where i.fkUserWorkshiftID = " + workshiftid;
            this.LoadFromRawSql(query);
        }
        public void CheckIncomTypeInUse(int incomType, int year, int month, int day)
        {
            string query = string.Empty;
            query += " select i.* from dbo.tblIncome i ";
            query += " where i.fkIncomeTypeID = " + incomType;
            query += " and year(i.dIncomeDate) = '" + year + "' ";
            query += " and month(i.dIncomeDate) = '" + month + "' ";
            query += " and day(i.dIncomeDate) = '" + day + "' ";
            this.LoadFromRawSql(query);
        }
        public void GetOtherIncomes(int year, int month, int day)
        {
            string query = string.Empty;
            query += " select itype.sIncomType, i.pkIncomID,i.fIncome,i.dIncomeDate,i.iComment from dbo.tblIncome i inner join ";
            query += " dbo.tblIncomTypes itype on itype.pkIncomeTypeID = i.fkIncomeTypeID ";
            query += " where ";
            query += " year(i.dIncomeDate) = '" + year + "' ";
            query += " and month(i.dIncomeDate) = '" + month + "' ";
            query += " and day(i.dIncomeDate) = '" + day + "' ";
            this.LoadFromRawSql(query);
        }
        public void GetOtherIncomeByID(int incomeid)
        {
            string query = string.Empty;
            query += " select itype.sIncomType, i.pkIncomID,i.fIncome,i.dIncomeDate,i.iComment from dbo.tblIncome i inner join ";
            query += " dbo.tblIncomTypes itype on itype.pkIncomeTypeID = i.fkIncomeTypeID ";
            query += " where i.pkIncomID = " + incomeid;
            this.LoadFromRawSql(query);
        }
        public void getMinumumPerDaySalary(int userid, int workshiftID)
        {
            string strQuery = string.Empty;
            strQuery += "select i.*  from dbo.tblIncome i where i.fkuserid = " + userid + " and i.fkUserWorkshiftID = " + workshiftID;
            this.LoadFromRawSql(strQuery);
        }
        public void getSubtotals(int departmentid, int year, int week, int day)
        {
            string query = string.Empty;
            query += " select spType.sSpecialityName, sum(CONVERT(float,isnull(i.fIncome,'0')))+ sum(CONVERT(float,isnull(  [dbo].[GetOtherIncomeByID](i.pkIncomID),'0'))) -sum(CONVERT(float,isnull(uadv.uAdvance,'0'))) as income ";
            query += " from tblUserWorkshifts uws ";
            query += " left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID  ";
            query += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID  ";
            query += " left join tblUsers u on u.pkUserID = uws.fkUserID  ";
            query += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid  ";
            query += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            query += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and uws.pkUserWorkshiftID = i.fkUserWorkshiftID ";
            query += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            query += " where uws.iyear = " + year + " AND uws.iWeekNumber = " + week + " and uws.iDayNumber = " + day;
            query += " and  ud.fkdepartmentID = " + departmentid + " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  ";
            query += " group by spType.sSpecialityName";
            this.LoadFromRawSql(query);
        }

        public string GetWeeklySalary(int[] userIDs, int[] salarytypeids, int[] iDay, DateTime[] dt)
        {
            string salaries = string.Empty;
            string conn = System.Configuration.ConfigurationSettings.AppSettings["dbConnection"];
            SqlConnection sqlConn = new SqlConnection(conn);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand("GetUserSalary", sqlConn); //testFunction is scalar
            cmd.CommandType = CommandType.StoredProcedure;
            ListDictionary Params = new ListDictionary();

            for (int a = 0; a < userIDs.Length; a++)
            {
                cmd.Parameters.Add("@userid", SqlDbType.Int).Value = userIDs[a];
                cmd.Parameters.Add("@salarytypeid", SqlDbType.Int).Value = salarytypeids[a];
                cmd.Parameters.Add("@iday", SqlDbType.Int).Value = iDay[a];
                cmd.Parameters.Add("@weekStart", SqlDbType.DateTime).Value = dt[a];
                cmd.Parameters["@userid"].Direction = ParameterDirection.Input;
                cmd.Parameters["@salarytypeid"].Direction = ParameterDirection.Input;
                cmd.Parameters["@iday"].Direction = ParameterDirection.Input;
                cmd.Parameters["@weekStart"].Direction = ParameterDirection.Input;
                cmd.Parameters.Add("@salary", SqlDbType.Int);
                cmd.Parameters["@salary"].Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteScalar();
                int aFunctionResult = (int)cmd.Parameters["@salary"].Value;
                salaries += aFunctionResult.ToString() + ",";
                cmd.Parameters.Clear();
            }
            sqlConn.Close();
            return salaries;
        }
        public void getDailyPreasentStaff(DateTime dWeekstart, DateTime dDate, int iDayNo)
        {
            string query = string.Empty;
            query += "select uws.fkuserid,uc.fkSalaryTypeID from tbluserworkshifts uws ";
            query += " left join dbo.tblUserContract uc";
            query += " on uws.fkuserid = uc.fkuserid";
            query += " where uws.dWeekStartDate = '" + dWeekstart + "'"; // AND uws.dWeekStartDate <= '" + dWeekend + "'";
            query += "and idaynumber =" + iDayNo + " union ";
            query += "select u.pkuserid,uc.fkSalaryTypeID from tblusers u ";
            query += "inner join dbo.tblUserAccessLevel al ";
            query += " on u.pkuserid= al.fkuserid ";
            query += " left join dbo.tblUserContract uc ";
            query += " on u.pkuserid = uc.fkuserid";
            query += " where al.fkAccessLevelID = 2 or al.fkAccessLevelID= 4 and pkuserid not in (select mdo.fkuserid from dbo.tblManagerDayOff mdo";
            query += " left join dbo.tblUserContract uc";
            query += " on mdo.fkuserid = uc.fkuserid";
            query += " where mSingleDate = '" + dDate + "')";
            this.LoadFromRawSql(query);
        }
        public void GetWeekly_Income_Salaries_for_turnover(DateTime start, DateTime end)
        {
            string query = string.Empty;
            query += " select  sum(i.fIncome) as income,sum([dbo].[GetUserSalary](u.pkUserID,	uc.fkSalaryTypeID,	uws.iDayNumber, uws.dWeekStartDate)) as salary,sum(distinct [dbo].[GetOtherIncomeByDate](uws.dWeekStartDate, uws.iDayNumber,uws.dWeekEndDate)) as OtherIncome, uws.iWeekNumber,uws.dWeekStartDate, uws.dWeekEndDate ";
            query += " from tblUserWorkshifts uws ";
            query += " left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID   ";
            query += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID   ";
            query += " left join tblUsers u on u.pkUserID = uws.fkUserID   ";
            query += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid   ";
            query += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID   ";
            query += " left join tblUserContract uc on uc.fkuserid = u.pkuserid   ";
            query += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID   ";
            query += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            query += " where  uws.dWeekStartDate >= '" + start + "'and uws.dWeekStartDate <= '" + end + "' AND ud.fkdepartmentID = 1 And s.bIsIncomeSpecific = 1 And uws.fkuserid  ";
            query += " not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  ";
            query += " group by uws.iWeekNumber,uws.dWeekStartDate,uws.dWeekEndDate order by uws.iWeekNumber desc";
            this.LoadFromRawSql(query);
        }

        public void GetWeekly_Income_Salaries_with_Status(DateTime start, DateTime end)
        {
            string query = string.Empty;
            query += " select  sum(i.fIncome) as income,sum([dbo].[GetUserSalary](u.pkUserID,	uc.fkSalaryTypeID,	uws.iDayNumber, uws.dWeekStartDate)) as salary, isnull([dbo].[GetUserSalaryStatus](uws.iWeekNumber,uws.dWeekStartDate),'0') as SalaryStatus, isnull([dbo].[GetSalaryPermissionByDepartmentAdmin](uws.iWeekNumber,uws.dWeekStartDate),'0') as permission,  uws.iWeekNumber,uws.dWeekStartDate, uws.dWeekEndDate ";
            query += " from tblUserWorkshifts uws ";
            query += " left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID   ";
            query += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID   ";
            query += " left join tblUsers u on u.pkUserID = uws.fkUserID   ";
            query += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid   ";
            query += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID   ";
            query += " left join tblUserContract uc on uc.fkuserid = u.pkuserid   ";
            query += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID   ";
            query += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            query += " where  uws.dWeekStartDate >= '" + start + "'and uws.dWeekStartDate <= '" + end + "' AND ud.fkdepartmentID = 1 And s.bIsIncomeSpecific = 1 And uws.fkuserid  ";
            query += " not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  ";
            query += " group by uws.iWeekNumber,uws.dWeekStartDate,uws.dWeekEndDate order by uws.iWeekNumber desc";
            this.LoadFromRawSql(query);
        }

        public void GetOtherIncome_for_turnover(int weeknumber, int startYear, int endYear)
        {
            string query = string.Empty;
            query += " select sum(distinct [dbo].[GetOtherIncomeByDate](uws.dWeekStartDate, uws.iDayNumber)) as OtherIncome ";
            query += " from tblUserWorkshifts uws ";
            query += " left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID   ";
            query += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID   ";
            query += " left join tblUsers u on u.pkUserID = uws.fkUserID  ";
            query += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid   ";
            query += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID  ";
            query += " left join tblUserContract uc on uc.fkuserid = u.pkuserid  ";
            query += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID  ";
            query += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            query += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear >= " + startYear + " And uws.iyear <=" + endYear + " AND ud.fkdepartmentID = 1 And s.bIsIncomeSpecific = 1 And uws.fkuserid ";
            query += " not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            query += " group by uws.iWeekNumber";
        }
        public void GetUserIncome(int userid, int weeknumber, int daynumber, DateTime weekStart, DateTime weekEnd)
        {
            string query = string.Empty;
            query += " select i.* from  dbo.tblUserWorkshifts uws inner join ";
            query += " dbo.tblIncome i on i.fkUserWorkshiftID =  uws.pkUserWorkshiftID ";
            query += " where uws.dWeekStartDate >= '" + weekStart + "' ";
            query += " AND uws.dWeekEndDate <= '" + weekEnd + "' ";
            query += " And uws.iweeknumber = " + weeknumber;
            query += " And uws.fkuserid = " + userid;
            query += " And uws.idaynumber = " + daynumber;
            this.LoadFromRawSql(query);
        }
        public void GetOtherIncomeWeekStats(DateTime weekstart, DateTime weekend, int othertypeid)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@otherType", othertypeid);
            this.LoadFromSql("spGetOtherIncomeForStats", Params);
        }
        public void GetPositionIncomeWeekStats(DateTime weekstart, DateTime weekend, int weeknumber, int department, string positionrange, int positionvalue, int year, int val)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@weeknumber", weeknumber);
            Params.Add("@department", department);
            Params.Add("@positionRange", positionrange);
            Params.Add("@positionvalue", positionvalue);
            Params.Add("@iyear", year);
            Params.Add("@pval", val);

            this.LoadFromSql("spGetPositionIncomeForStats", Params);
        }
    }
}
