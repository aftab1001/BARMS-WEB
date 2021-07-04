using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserAdvances : LC.Model.BMS.DAL._tblUserAdvances
    {
        public void getUserAdvances_for_OperatingCapital(DateTime start, DateTime end)
        {
            string query = string.Empty;
            query += " select u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName,u.pkuserid, sum( uadv.uAdvance ) as advance";
            query += " from tblUserWorkshifts uws  ";
            query += " inner join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID   ";
            query += " inner join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID   ";
            query += " inner join tblUsers u on u.pkUserID = uws.fkUserID  ";
            query += " inner join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid  ";
            query += " inner join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID   ";
            query += " inner join tblUserContract uc on uc.fkuserid = u.pkuserid   ";
            query += " inner join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID   ";
            query += " inner join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID   ";
            query += " where  uws.dWeekStartDate >= '" + start + "'and uws.dWeekStartDate <= '" + end + "' AND ud.fkdepartmentID = 1 And s.bIsIncomeSpecific = 1 And uws.fkuserid not in  ";
            query += " (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  and uadv.uAdvance > 0";
            query += " group by u.sFirstName + ' ' + isnull(U.sLastName,''),u.pkuserid ";
            query += " order by FullName";
            this.LoadFromRawSql(query);
        }

        public void getUserAdvances_for_Popup(DateTime start, DateTime end, int userid)
        {
            string query = string.Empty;
            query += " select  uadv.dmodifieddate ,uadv.uAdvance ";
            query += " from tblUserWorkshifts uws  ";
            query += " inner join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID   ";
            query += " inner join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID   ";
            query += " inner join tblUsers u on u.pkUserID = uws.fkUserID  ";
            query += " inner join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid  ";
            query += " inner join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID   ";
            query += " inner join tblUserContract uc on uc.fkuserid = u.pkuserid   ";
            query += " inner join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID   ";
            query += " inner join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID   ";
            query += " where  uws.dWeekStartDate >= '" + start + "'and uws.dWeekStartDate <= '" + end + "' AND ud.fkdepartmentID = 1 And s.bIsIncomeSpecific = 1 And uws.fkuserid not in  ";
            query += " (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  and u.pkuserid = " + userid + " and uadv.uAdvance > 0 ";
            query += " order by uadv.dmodifieddate";
            this.LoadFromRawSql(query);
        }
        public void GetUserAdvance(int userid, int weeknumber, int daynumber, DateTime weekStart, DateTime weekEnd)
        {
            string query = string.Empty;
            query += " select ud.* from  dbo.tblUserWorkshifts uws inner join ";
            query += " tbluseradvances ud on ud.fkuserworkshiftid = uws.pkUserWorkshiftID ";
            query += " where uws.dWeekStartDate >= '" + weekStart + "' ";
            query += " AND uws.dWeekEndDate <= '" + weekEnd + "' ";
            query += " And uws.iweeknumber = " + weeknumber;
            query += " And uws.fkuserid = " + userid;
            query += " And uws.idaynumber = " + daynumber;
            this.LoadFromRawSql(query);
        }
    }
}
