using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.Specialized;

namespace LC.Model.BMS.BLL
{
    public class tblUserWorkshifts : LC.Model.BMS.DAL._tblUserWorkshifts
    {
        public void CheckSpecialityInUse(int SpecialityID)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.FkSpecialityID.Value = SpecialityID;
            this.Query.Load();

        }
        public void CkeckWorkShiftExist(int week, int UserID)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.IWeekNumber.Value = week;
            this.Where.FkUserID.Value = UserID;
            this.Query.Load();
        }
        public void LoadUserWorkShift(int weeknumber, int UserID, int year)
        {
            string strQuery = string.Empty;
            strQuery += " select s.Abbrv + ' ' + uws.sStartTime + '-' + uws.sEndTime as Timing ,uws.* , s.Abbrv ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";

            strQuery += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " and uws.fkuserid = " + UserID + " order by uws.iDayNumber ";

            this.LoadFromRawSql(strQuery);

        }
        public void LoadOtherUsersWorkShift(int weeknumber, int UserID, int departmentid, int year, int day)
        {
            string strQuery = string.Empty;
            strQuery += " select  s.Abbrv + ' ' + uws.sStartTime + '-' + sEndTime + ' ' + u.sFirstName + isnull(U.sLastName,'') as FullName ,u.sFirstName +' '+ isnull(U.sLastName,'') as Name,uws.* , s.Abbrv ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel UAL on UAL.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " and uws.fkuserid <> " + UserID + " AND ud.fkdepartmentID = " + departmentid;
            strQuery += " and uws.idaynumber = " + day;
            strQuery += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";

            this.LoadFromRawSql(strQuery);

        }
        public void LoadOtherManagersWorkShift(int weeknumber, int UserID, int departmentid, int year, int day)
        {
            string strQuery = string.Empty;
            strQuery += " select  s.Abbrv + ' ' + uws.sStartTime + '-' + sEndTime + ' ' + u.sFirstName + isnull(U.sLastName,'') as FullName ,u.sFirstName +' '+ isnull(U.sLastName,'') as Name,uws.* , s.Abbrv ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel UAL on UAL.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " and uws.fkuserid <> " + UserID + " AND ud.fkdepartmentID = " + departmentid;
            strQuery += " and uws.idaynumber = " + day;
            strQuery += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid < 4 and fkaccesslevelid > 4) ";

            this.LoadFromRawSql(strQuery);

        }
        public void LoadOtherUsersOFFDay(int weeknumber, int UserID, int departmentid, int year, int day)
        {
            string strQuery = string.Empty;
            strQuery += " select  u.sFirstName + isnull(U.sLastName,'') as FullName ,uws.* , s.Abbrv ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " and uws.fkuserid <> " + UserID + " AND ud.fkdepartmentID = " + departmentid;
            strQuery += " AND uws.bisofday = 1  and uws.idayNumber = " + day;
            this.LoadFromRawSql(strQuery);

        }
        public DataTable LoadCurrentWorkshift(int weeknumber, int departmentid, int year)
        {
            string strQuery = string.Empty;
            //strQuery += " select  s.Abbrv + ' ' + uws.sStartTime + '-' + sEndTime + ' ' + u.sFirstName + isnull(U.sLastName,'') as FullName ,uws.* , s.Abbrv , spType.pkSpecialityTypeID";
            strQuery += " select   s.sSpeciality,u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName ,uws.* , s.Abbrv , spType.pkSpecialityTypeID";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid;
            strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  ";
            this.LoadFromRawSql(strQuery);
            return this.DataTable;
        }
        public void LoadOffdayUsers(int departmentid, int week, int year, int day)
        {

            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName , u.pkuserid ";

            strQuery += "  from tblUsers u  ";

            strQuery += "  left join dbo.tblUserDepartment ud on u.pkUserID = ud.fkUserID and u.bActiveByUser = 1 and u.bActiveByAdmin = 1";
            strQuery += " left join  tblUserAccessLevel ual on ual.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = usp.fkspecialitytypeid and sptype.special = 0 ";
            strQuery += "  where ud.fkdepartmentID = " + departmentid;

            strQuery += "  And u.pkuserid not in ";
            strQuery += "  (select tbluserworkshifts.fkuserid from tblUserWorkshifts ";
            strQuery += "  left join tblUserDepartment on tbluserworkshifts.fkUserID = tbluserdepartment.fkUserID ";
            strQuery += "  where iweeknumber = " + week + " and iyear = " + year + " and idaynumber = " + day + " and tbluserdepartment.fkdepartmentid = " + departmentid + ") ";
            strQuery += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid>1)AND ual.fkAccessLevelID <> 6 ";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadOffdayManagers(int departmentid, int week, int year, int day)
        {

            string strQuery = string.Empty;
            strQuery += "  select  u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName , u.pkuserid ";

            strQuery += "  from tblUsers u  ";

            strQuery += "  left join dbo.tblUserDepartment ud on u.pkUserID = ud.fkUserID and u.bActiveByUser = 1 and u.bActiveByAdmin = 1";
            strQuery += " left join  tblUserAccessLevel ual on ual.fkuserid = u.pkuserid ";
            strQuery += "  where ud.fkdepartmentID = " + departmentid;

            strQuery += "  And u.pkuserid not in ";
            strQuery += "  (select tbluserworkshifts.fkuserid from tblUserWorkshifts ";
            strQuery += "  left join tblUserDepartment on tbluserworkshifts.fkUserID = tbluserdepartment.fkUserID ";
            strQuery += "  where iweeknumber = " + week + " and iyear = " + year + " and idaynumber = " + day + " and tbluserdepartment.fkdepartmentid = " + departmentid + ") ";
            strQuery += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid<4 and fkaccesslevelid >4)AND ual.fkAccessLevelID <> 6 ";
            this.LoadFromRawSql(strQuery);
        }
        public void PreviousWeekOffDays(int week, int departmentid, int year)
        {
            string strQuery = string.Empty;
            strQuery += "  select   case when iyear is null then 0 else count(u.pkuserid) end as offdays,ud.fkdepartmentid , uws.iyear ,u.pkuserid,";
            strQuery += "  uws.iweeknumber ,(u.sfirstname + ' ' + isnull(u.slastname , '')) as FullName  from tblusers u  ";
            strQuery += "  left join tbluserdepartment ud on u.pkuserid = ud.fkuserid  ";
            strQuery += "  left join tbluserworkshifts uws on uws.fkuserid = u.pkuserid and uws.iweekNumber =  " + week + "  and uws.iyear = " + year + " and  uws.bontime = 1 ";
            strQuery += "  left join  tblUserAccessLevel ual on ual.fkuserid = u.pkuserid ";
            strQuery += "  inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += "  inner join dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = usp.fkspecialitytypeid and sptype.special = 0 ";
            strQuery += "  where ud.fkdepartmentid =  " + departmentid + " and ual.fkuserid  not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid>1) AND ual.fkAccessLevelID <> 6 ";
            strQuery += "  group by  u.pkuserid,uws.iweeknumber , u.sfirstname + ' ' + isnull(u.slastname , '') , ud.fkdepartmentid , uws.iyear ";
            this.LoadFromRawSql(strQuery);
        }
        public void PreviousWeekOffDaysByUSer(int week, int departmentid, int year, int UserID)
        {

            string strQuery = string.Empty;
            strQuery += "  select   case when iyear is null then 0 else count(u.pkuserid) end as offdays,ud.fkdepartmentid , uws.iyear ,u.pkuserid,";
            strQuery += "  uws.iweeknumber ,(u.sfirstname + ' ' + isnull(u.slastname , '')) as FullName  from tblusers u  ";
            strQuery += "  left join tbluserdepartment ud on u.pkuserid = ud.fkuserid  ";
            strQuery += "  left join tbluserworkshifts uws on uws.fkuserid = u.pkuserid and uws.iweekNumber =  " + week + "  and uws.iyear = " + year;
            strQuery += " left join  tblUserAccessLevel ual on ual.fkuserid = u.pkuserid ";
            strQuery += "  where ud.fkdepartmentid =  " + departmentid + " and ual.fkuserid  not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid>1) AND ual.fkAccessLevelID <> 6 and u.pkuserid = " + UserID + " and uws.bOnTime = 1 ";
            strQuery += "  group by  u.pkuserid,uws.iweeknumber , u.sfirstname + ' ' + isnull(u.slastname , '') , ud.fkdepartmentid , uws.iyear ";
            this.LoadFromRawSql(strQuery);
        }
        public void PreviousWeekOnHoursByUSer(int week, int year, int UserID)
        {
            string strQuery = string.Empty;
            strQuery = "  select * from tbluserworkshifts where fkuserid =" + UserID + " and iweeknumber = " + week + " and iyear = " + year + " and bontime = 1";
            //strQuery += " select sum(DATEDIFF(hour, CONVERT(datetime,'2011-01-01 '+s.sStartTime+':00'), ";
            //strQuery +=" CONVERT(datetime,'2011-01-02 '+s.sEndTime+':00'))) as hours ";
            //strQuery +=" from tbluserworkshifts s where fkuserid = 8 and iweeknumber = 47 and iyear = 2011 and bontime = 1 "; 
            this.LoadFromRawSql(strQuery);
        }

        public DataTable LoadCurrentWorkshiftForAccountManager(int daynumber, int weeknumber, int departmentid, int year)
        {
            string strQuery = string.Empty;
            //strQuery += " select  s.Abbrv + ' ' + uws.sStartTime + '-' + sEndTime + ' ' + u.sFirstName + isnull(U.sLastName,'') as FullName ,uws.* , s.Abbrv , spType.pkSpecialityTypeID";
            strQuery += " select    u.pkUserID,s.sSpeciality,u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName ,uws.* ,  spType.pkSpecialityTypeID ";
            strQuery += " ,uc.LowSeasonSalary,uc.HighSeasonSalary, uc.StandardSalary, uc.MinimumPerday,uc.PercentageOver,uc.fSalaryPercentage, uc.fkSalaryTypeID   ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " left join tblUserContract uc on uc.fkuserid = u.pkuserid ";
            strQuery += " where sptype.Special = 0 and  uws.iDayNumber = " + daynumber + " and  uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid;
            strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1)  ";
            this.LoadFromRawSql(strQuery);
            return this.DataTable;
        }
        public void LoadCurrentIncomeDetailForAccountManager(int daynumber, int weeknumber, int departmentid, int year)
        {
            string strQuery = string.Empty;
            strQuery += " select u.pkUserID,s.Abbrv,sSpeciality,s.orderid,s.bIsIncomeSpecific,u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName,i.pkIncomID ,i.fIncome,i.netIncome,i.userTip,uadv.pkUserAdvanceID, uadv.uAdvance ,uws.* ,  spType.pkSpecialityTypeID ";
            strQuery += " ,uc.LowSeasonSalary,uc.HighSeasonSalary, uc.StandardSalary, uc.MinimumPerday,uc.PercentageOver,uc.fSalaryPercentage, uc.fkSalaryTypeID   ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " left join tblUserContract uc on uc.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " where uws.iDayNumber = " + daynumber + " and  uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid;
            strQuery += " and s.bIsIncomeSpecific =1 and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) order by s.OrderID ";
            this.LoadFromRawSql(strQuery);
        }

        public void LoadCurrentIncomeDetailForECUser(int daynumber, int weeknumber, int departmentid, int year, int ecuserid, DateTime assignedDate)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.pkUserID,s.Abbrv,sSpeciality,s.orderid,s.bIsIncomeSpecific,u.sFirstName + ' ' + isnull(U.sLastName,'') as FullName,i.pkIncomID ,i.fIncome,i.netIncome,i.userTip,uadv.pkUserAdvanceID, uadv.uAdvance ,uws.* ,  spType.pkSpecialityTypeID ";
            strQuery += " ,uc.LowSeasonSalary,uc.HighSeasonSalary, uc.StandardSalary, uc.MinimumPerday,uc.PercentageOver,uc.fSalaryPercentage, uc.fkSalaryTypeID   ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " inner join dbo.tblECUserAssignments ecua on ecua.fkSpecialtyID = s.pkspecialityID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " left join tblUserContract uc on uc.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " where uws.iDayNumber = " + daynumber + " and  uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid + " and ecua.ECUserID = " + ecuserid + " and ecua.dmodifieddate='" + assignedDate + "' ";
            strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) order by s.OrderID ";
            this.LoadFromRawSql(strQuery);
        }

        public void LoadCurrentIncomeTotalForAccountManager(int daynumber, int weeknumber, int departmentid, int year)
        {
            string strQuery = string.Empty;
            strQuery += " select u.pkUserID,s.sSpeciality,s.orderid,i.pkIncomID ,i.fIncome,uadv.uAdvance ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " left join tblUserContract uc on uc.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " where uws.iDayNumber = " + daynumber + " and  uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid + " and s.bIsIncomeSpecific = 1 ";
            strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) order by s.OrderID ";
            this.LoadFromRawSql(strQuery);
        }

        public void LoadCurrentIncomeTotalForECUser(int daynumber, int weeknumber, int departmentid, int year, int ecuserid, DateTime assignedDate)
        {
            string strQuery = string.Empty;
            strQuery += " select u.pkUserID,s.sSpeciality,s.orderid,i.pkIncomID ,i.fIncome,uadv.uAdvance ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " inner join dbo.tblECUserAssignments ecua on ecua.fkSpecialtyID = s.pkspecialityID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " left join tblUserContract uc on uc.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblIncome i on i.fkuserid = u.pkuserid and i.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " left join dbo.tblUserAdvances uadv on uadv.fkuserid = u.pkuserid and uadv.fkUserWorkshiftID = uws.pkUserWorkshiftID ";
            strQuery += " where uws.iDayNumber = " + daynumber + " and  uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid + " and s.bIsIncomeSpecific = 1 " + " and ecua.ECUserID = " + ecuserid + " and ecua.dmodifieddate='" + assignedDate + "' ";
            strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) order by s.OrderID ";
            this.LoadFromRawSql(strQuery);
        }


        public DataTable LoadCurrentUserSalary(int weeknumber, DateTime startDate, int departmentid, int year, int userid)
        {
            string strQuery = string.Empty;
            //strQuery += " select  s.Abbrv + ' ' + uws.sStartTime + '-' + sEndTime + ' ' + u.sFirstName + isnull(U.sLastName,'') as FullName ,uws.* , s.Abbrv , spType.pkSpecialityTypeID";
            strQuery += " select uc.fkSalaryTypeID ,uc.LowSeasonSalary , uc.HighSeasonSalary, uc.StandardSalary, ";
            strQuery += " uc.MinimumPerday, uc.fSalaryPercentage, uc.PercentageOver, uc.bContractAgreed, ic.userTip,uad.uAdvance, uws.*  ";
            strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
            strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
            strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
            strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
            strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            strQuery += " left join dbo.tblIncome ic on ic.fkUserID = u.pkuserid  ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkuserid = u.pkuserid  ";
            strQuery += " left join dbo.tblUserAdvances uad on uad.fkuserid = u.pkuserid and uad.AdvanceDate = uws.dCreateDate ";
            strQuery += " where uws.dWeekStartDate = '" + startDate + "' and  uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid + " and u.pkuserid = " + userid;
            strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 5)  ";
            this.LoadFromRawSql(strQuery);
            return this.DataTable;
        }


        public void LoadWorkShiftByStartDate(DateTime dwStart)
        {
            this.Where.DWeekStartDate.Value = dwStart;
            this.Query.Load();
        }
        //public void GetWeeklySalary(int departmentid, DateTime weekStartDate)
        //{
        //    string strQuery = string.Empty;
        //    //strQuery += " select  s.Abbrv + ' ' + uws.sStartTime + '-' + sEndTime + ' ' + u.sFirstName + isnull(U.sLastName,'') as FullName ,uws.* , s.Abbrv , spType.pkSpecialityTypeID";
        //    strQuery += " select uc.fkSalaryTypeID ,uc.LowSeasonSalary , uc.HighSeasonSalary, uc.StandardSalary, ";
        //    strQuery += " uc.MinimumPerday, uc.fSalaryPercentage, uc.PercentageOver, uc.bContractAgreed, ic.userTip,uad.uAdvance, uws.*  ";
        //    strQuery += " from tblUserWorkshifts uws left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID ";
        //    strQuery += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID ";
        //    strQuery += " left join tblUsers u on u.pkUserID = uws.fkUserID ";
        //    strQuery += " left join tblUserAccessLevel ual on uws.fkuserid = ual.fkuserid ";
        //    strQuery += " left join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
        //    strQuery += " left join dbo.tblIncome ic on ic.fkUserID = u.pkuserid  ";
        //    strQuery += " left join dbo.tblUserContract uc on uc.fkuserid = u.pkuserid  ";
        //    strQuery += " left join dbo.tblUserAdvances uad on uad.fkuserid = u.pkuserid and uad.AdvanceDate = uws.dCreateDate ";
        //    strQuery += " where uws.iWeekNumber = " + weeknumber + " and uws.iyear = " + year + " AND ud.fkdepartmentID = " + departmentid + " and u.pkuserid = " + userid;
        //    strQuery += " and uws.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid > 5)  ";
        //    this.LoadFromRawSql(strQuery);

        //}
        public void GetWeekendWorkshift(int day, int week, int year)
        {
            this.Where.IDayNumber.Value = day;
            this.Where.IWeekNumber.Value = week;
            this.Where.IYear.Value = year;
            this.Query.Load();
        }
        public void getLastDayWorking(int day, int week, int year, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select uc.PercentageOver,uc.fSalaryPercentage, uws.* from tblUserWorkshifts uws ";
            strQuery += " inner join dbo.tblUserContract uc on uc.fkUserID = uws.fkUserID ";
            strQuery += " where uws.fkuserid = " + userid + " and uws.idaynumber = " + day + " and iweeknumber = " + week + " and iYear = " + year;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadManagersWorkshift(int DepartmentID, int weeknumber, int year, int day)
        {
            string query = string.Empty;
            query += " select u.pkuserid,u.sFirstName + ' ' + isnull(slastname ,'') as FullName,";
            query += " [dbo].[getUserWorkshit](u.pkuserid, " + weeknumber + "," + year + "," + day + ") as workshiftid from tblusers u inner join ";
            query += " dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid = " + DepartmentID + " inner join ";
            query += " dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and (ual.fkAccessLevelID = 2 or ual.fkAccessLevelID = 4) and ual.fkAccessLevelID != 1 and ual.fkAccessLevelID != 3 and ual.fkAccessLevelID != 5 and ual.fkAccessLevelID != 6";
            query += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            this.LoadFromRawSql(query);
        }
        public void LoadECUserWordkshift(int DepartmentID, int weeknumber, int year, int day)
        {
            string query = string.Empty;
            query += " select u.pkuserid,u.sFirstName + ' ' + isnull(slastname ,'') as FullName, ";
            query += " [dbo].[getUserWorkshit](u.pkuserid, " + weeknumber + "," + year + "," + day + ") as workshiftid  from tblusers u inner join ";
            query += " dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid = " + DepartmentID + " inner join ";
            query += " dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and ual.fkAccessLevelID = 3 ";
            query += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            query += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid>3 and fkaccesslevelid<3) ";
            this.LoadFromRawSql(query);
        }
        public void LoadStaffWordkshift(int DepartmentID, int weeknumber, int year, int day)
        {
            string query = string.Empty;
            query += " select u.pkuserid,u.sFirstName + ' ' + isnull(slastname ,'') as FullName, ";
            query += " [dbo].[getUserWorkshit](u.pkuserid, " + weeknumber + "," + year + "," + day + ") as workshiftid  from tblusers u inner join ";
            query += " dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid = " + DepartmentID + " inner join ";
            query += " dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and ual.fkAccessLevelID = 1 ";
            query += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            query += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid>1) ";
            this.LoadFromRawSql(query);
        }
        public void getSpecialUserWorkshift(int DepartmentID, int weeknumber, int year, int day)
        {
            string query = string.Empty;
            query += " select u.sFirstName + ' '+isnull(u.sLastName,'') as FullName,sptype.sSpecialityName, u.*, [dbo].[getSpecialPeopleWorkshit](u.pkUserID, " + weeknumber + "," + year + "," + day + ") as workshiftid ";
            query += " from dbo.tblusers u  ";
            query += " INNER join tbluserdepartment ud on u.pkuserid = ud.fkuserid ";
            query += " inner join tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            query += " inner join dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = usp.fkspecialitytypeid and sptype.special = 1 ";
            query += " where usp.bisprimary = 1 and ud.fkdepartmentid = " + DepartmentID;
            this.LoadFromRawSql(query);

            
        }
        public void getECUserForDay(int weeknumber, int year, int day, int userid)
        {
            string query = string.Empty;
            query += " select uws.* from dbo.tblUserWorkshifts uws ";
            query += " where  uws.iweeknumber = " + weeknumber + " and uws.iyear = " + year + " and uws.idaynumber = " + day + " and uws.fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }
        public void getECUserForDay(DateTime date, int userid)
        {
            string query = string.Empty;
            query += " select wua.* from dbo.tblECUserAssignments wua ";
            query += " where  wua.dModifiedDate = '" + date + "' and wua.ECUserID = " + userid;
            this.LoadFromRawSql(query);
        }
        public void GetUserWorkshit(int userid, int weeknumber, int daynumber, DateTime weekStart, DateTime weekEnd)
        {
            string query = string.Empty;
            query += " select uws.* from  dbo.tblUserWorkshifts uws  ";
            query += " where uws.dWeekStartDate >= '" + weekStart + "' ";
            query += " AND uws.dWeekEndDate <= '" + weekEnd + "' ";
            query += " And uws.iweeknumber = " + weeknumber;
            query += " And uws.fkuserid = " + userid;
            query += " And uws.idaynumber = " + daynumber;
            this.LoadFromRawSql(query);
        }

        public void getOneWeekUsers(int weeknumber, int departmentid)
        {
            string query = string.Empty;
            query += " select distinct uws.fkuserid, uws.iweeknumber,uws.dweekstartDate, uws.dweekEndDate from tblUserWorkshifts uws ";
            query += " inner join dbo.tblUserDepartment ud on uws.fkUserID = ud.fkUserID ";
            query += " where uws.iweeknumber = " + weeknumber + " and ud.fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void getWeeks(int year, int weeknumbr, int DropDownOption)
        {
            string query = string.Empty;
            query += " select distinct uws.iweeknumber, uws.dweekstartdate, uws.dweekenddate  from dbo.tblUserWorkshifts uws ";

            if (DropDownOption == 1)
            {
                query += " where uws.iweeknumber = " + weeknumbr;
            }
            else if (DropDownOption == 2)
            {
                query += " where uws.iyear = " + year + " and (uws.iweeknumber = " + (weeknumbr - 1).ToString() + " or uws.iweeknumber = " + weeknumbr + " or uws.iweeknumber = " + (weeknumbr + 1) + ") ";
            }
            else if (DropDownOption == 3)
            {
                query += " where uws.iyear = " + year + " and ";
                query += "(uws.iweeknumber = " + (weeknumbr - 2).ToString();
                query += " or uws.iweeknumber = " + (weeknumbr - 1);
                query += " or uws.iweeknumber = " + (weeknumbr);
                query += " or uws.iweeknumber = " + (weeknumbr + 1);
                query += " or uws.iweeknumber = " + (weeknumbr + 2) + ") ";
            }
            else if (DropDownOption == 4)
            {
                query += " where uws.iyear = " + year + " and ";
                query += "(uws.iweeknumber = " + (weeknumbr - 4).ToString();
                query += " or uws.iweeknumber = " + (weeknumbr - 3);
                query += " or uws.iweeknumber = " + (weeknumbr - 2);
                query += " or uws.iweeknumber = " + (weeknumbr - 1);
                query += " or uws.iweeknumber = " + (weeknumbr);
                query += " or uws.iweeknumber = " + (weeknumbr + 1);
                query += " or uws.iweeknumber = " + (weeknumbr + 2);
                query += " or uws.iweeknumber = " + (weeknumbr + 3);
                query += " or uws.iweeknumber = " + (weeknumbr + 4) + ") ";
            }
            else if (DropDownOption == 5)
            {
                query += " where uws.iyear = " + year + " ";
            }

            this.LoadFromRawSql(query);
        }


        public void GetWeekStats(DateTime weekstart, DateTime weekend, int weeknumber, int departmentid)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@weeknumber", weeknumber);
            Params.Add("@department", departmentid);
            this.LoadFromSql("spGetIncomeForStats", Params);
        }
        public void getWorshiftYears()
        {
            string query = string.Empty;
            query += " select distinct uws.iyear from dbo.tblUserWorkshifts uws ";
            this.LoadFromRawSql(query);
        }
        public void GetSalaryWeekStats(DateTime weekstart, DateTime weekend, int weeknumber, int departmentid)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@weeknumber", weeknumber);
            Params.Add("@department", departmentid);
            this.LoadFromSql("spSalariesForStats", Params);
        }
        public void getECUserWorkshift(int daynum, int weeknum, int year, int departmentid)
        {
            string query = string.Empty;
            query += " select  u.pkuserid,u.sFirstName + ' ' + isnull(slastname ,'') as FullName ";
            query += " ,uws.pkuserworkshiftid, uws.iweeknumber, uws.idaynumber,uws.iyear, spType.pkSpecialityTypeID  ";
            query += " ,uc.fkSalaryTypeID,[dbo].[GetSalaryByDay](u.pkuserid,uws.idaynumber,uws.iweeknumber,uc.fkSalaryTypeID, uws.dWeekStartDate, uws.dWeekEndDate ) as salary  ";
            query += " from tblusers u  ";
            query += " inner join dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid = " + departmentid;
            query += " inner join dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and ual.fkAccessLevelID = 3   ";
            query += " inner join  tblUserWorkshifts uws  on u.pkuserid = uws.fkuserid  ";
            query += " left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID  ";
            query += " left join tblSpecialityType spType on spType.pkSpecialityTypeID = s.fkSpecialityTypeID   ";
            query += " left join tblUserContract uc on uc.fkuserid = u.pkuserid   ";
            query += " where  uws.iDayNumber = " + daynum + " and  uws.iWeekNumber = " + weeknum + " and uws.iyear = " + year + " and  ";
            query += " u.bActiveByUser = 1 and u.bActiveByAdmin = 1  and u.pkuserid not in  ";
            query += " (select fkuserid from tbluseraccesslevel where fkaccesslevelid>3 and fkaccesslevelid<3)";
            this.LoadFromRawSql(query);
        }
        public void getManagerWorkshift(DateTime weekstartDate, DateTime selectedDate, int daynum, int year, int departmentid)
        {
            string query = string.Empty;
            query += " select u.pkuserid,u.sFirstName + ' ' + isnull(slastname ,'') as FullName, ";
            query += " uc.fkSalaryTypeID,[dbo].[GetUserSalary] ";
            query += " (u.pkuserid,uc.fkSalaryTypeID," + daynum + ",'" + weekstartDate + "')  ";
            query += " as salary from tblusers u  ";
            query += " inner join  dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid =  " + departmentid;
            query += " inner join  dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and (ual.fkAccessLevelID = 2  ";
            query += " or ual.fkAccessLevelID = 4) and ual.fkAccessLevelID != 1 and ual.fkAccessLevelID != 3 and  ";
            query += " ual.fkAccessLevelID != 5 and ual.fkAccessLevelID != 6  ";
            query += " left join tblUserContract uc on uc.fkuserid = u.pkuserid  ";
            query += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 and u.pkuserid not in ";
            query += " (select mdof.fkuserid from dbo.tblManagerDayOff  mdof where mdof.msingledate = '" + selectedDate + "')";
            this.LoadFromRawSql(query);
        }

        public void getSpecialUserWorkshiftForAttendence(int daynum, int weeknum, int year, int departmentid)
        {
            string query = string.Empty;
            query += " select distinct u.sFirstName + ' '+isnull(u.sLastName,'') as FullName,sptype.sSpecialityName, ";
            query += " uws.pkuserworkshiftid, uws.iweeknumber, uws.idaynumber,uws.iyear, spType.pkSpecialityTypeID   , ";
            query += " uc.fkSalaryTypeID,[dbo].[GetSalaryByDay](u.pkuserid,uws.idaynumber,uws.iweeknumber,uc.fkSalaryTypeID, ";
            query += " uws.dWeekStartDate, uws.dWeekEndDate ) as salary     ";
            query += " from dbo.tblusers u    ";
            query += " INNER join tbluserdepartment ud on u.pkuserid = ud.fkuserid  ";
            query += " inner join dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and ual.fkAccessLevelID = 1     ";
            query += " inner join  tblUserWorkshifts uws  on u.pkuserid = uws.fkuserid    ";
            query += " left join tblSpeciality s on uws.fkSpecialityID = s.pkspecialityID    ";
            query += " left join tblUserContract uc on uc.fkuserid = u.pkuserid   ";
            query += " inner join tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            query += " inner join dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = usp.fkspecialitytypeid and ";
            query += " sptype.special = 1   ";
            query += " where uws.iDayNumber = " + daynum + " and  uws.iWeekNumber = " + weeknum + " and uws.iyear =  " + year;
            query += " and  u.bActiveByUser = 1 and u.bActiveByAdmin = 1 and  usp.bisprimary=1 and ud.fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void getSpeciaUserForDay(int weeknumber, int year, int day, int userid)
        {
            string query = string.Empty;
            query += " select uws.* from dbo.tblUserWorkshifts uws ";
            query += " where  uws.iWeeknumber = " + weeknumber + " and uws.iYear = " + year + " and uws.iDaynumber = " + day + " and uws.fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }
        public void getAssignmentsForDay(int weeknumber, int year, int day, int DeptID)
        {
            string query = string.Empty;
            query += " select us.*,uws.* from dbo.tblSpeciality us inner join dbo.tblUserWorkshifts uws on us.pkSpecialityID = uws.fkSpecialityID  inner join dbo.tblUserAccessLevel UAL on uws.fkuserid=UAL.fkuserID where UAL.fkAccessLevelID <>3 and us.bIsIncomeSpecific =1";
            query += " and uws.iWeeknumber = " + weeknumber + " and uws.iYear = " + year + " and uws.iDaynumber = " + day + " and us.fkdepartmentid=" + DeptID + "";
            this.LoadFromRawSql(query);
        }
        public void getAssignmentsForDayWithSeperator(int weeknumber, int year, int day, int DeptID)
        {
            string query = string.Empty;
            query += " select us.*,uws.* from dbo.tblSpeciality us inner join dbo.tblUserWorkshifts uws on us.fkspecialitytypeid = uws.fkSpecialityID  inner join dbo.tblUserAccessLevel UAL on uws.fkuserid=UAL.fkuserID where UAL.fkAccessLevelID <>3 ";
            query += " and uws.iWeeknumber = " + weeknumber + " and uws.iYear = " + year + " and uws.iDaynumber = " + day + " and us.fkdepartmentid=" + DeptID + " order by orderid";
            this.LoadFromRawSql(query);
        }
        public void getSalaryandBonus(int UserId)
        {
            string query = string.Empty;
            query+="select * from tblUserWorkshifts where pkUserWorkshiftID= (SELECT MAX(pkUserWorkshiftID) AS Expr1 ";
            query+=" FROM  tblUserWorkshifts ";
            query += " WHERE (fkUserID = '" + UserId + "') AND (Bonus > 0))and bOnTime = '1' and bLate='0'";
            this.LoadFromRawSql(query);
        }
        public void LoadUserAlreadyExsit(int fkSpecialityID, int iWeekNumber, int iYear, int iDayNumber)
        {
            this.FlushData();
            string strQuery = string.Empty;
            strQuery += "select pkUserWorkshiftID from dbo.tblUserWorkshifts ";
            strQuery += "  where fkSpecialityID =" + fkSpecialityID + " and iWeekNumber = " + iWeekNumber + " and iYear = " + iYear + " and iDayNumber =" + iDayNumber;
            this.LoadFromRawSql(strQuery);
        }
    }
}
