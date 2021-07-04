using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace LC.Model.BMS.BLL
{
    public class tblUsers : LC.Model.BMS.DAL._tblUsers
    {
        public void UserExist(string username)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SUsername.Value = username;
            this.Query.Load();

        }
        public void AuthenticateUser(string username, string password)
        {
            this.FlushData();
            this.Where.SUsername.Value = username;
            this.Where.SPassword.Value = password;
            this.Where.BActiveByAdmin.Value = 1;
            this.Where.BActiveByUser.Value = 1;
            this.Query.Load();
        }
        public void AuthenticateActivationCode(string strCode)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SActivationCode.Value = strCode;
            this.Query.Load();
        }

        public void LoadManager_and_AccountManager(int departmentid)
        {
            string strquery = string.Empty;
            strquery += " select  u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* from tblusers u ";
            strquery += " inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strquery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid and d.pkDepartmentID = " + departmentid;
            strquery += " inner join dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and (ual.fkaccesslevelid = 2 or ual.fkaccesslevelid = 4) ";
            strquery += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            this.LoadFromRawSql(strquery);

        }
        public void LoadECUser(int departmentid)
        {
            string strquery = string.Empty;
            strquery += " select  u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* from tblusers u ";
            strquery += " inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strquery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid and d.pkDepartmentID = " + departmentid;
            strquery += " inner join dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid and (ual.fkaccesslevelid = 3) ";
            strquery += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            this.LoadFromRawSql(strquery);

        }
        public void LoadUsers(int active, int departmentid, int userid)
        {
            string strQuery = string.Empty;
            if (active == 1)
            {
                strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* ";
                strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
                strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
                strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
                strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 ";
                strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
                strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            }
            if (active == 2)
            {
                strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* ";
                strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
                strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
                strQuery += " where u.bactivebyuser = 0 or u.bactivebyadmin= 0  and ud.fkDepartmentID = " + departmentid;
                strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";

            }
            if (active == 3)
            {
                strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
                strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
                strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
                strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
                //strQuery += " where u.bactivebyuser = 0 or u.bactivebyadmin= 0 ";
                strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
                strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            }
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersActivatedByHim(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " left join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1";
            strQuery += " where u.bactivebyuser = 1";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersActivatedByHim(int departmentid, int userid, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.*  ";
            strQuery += " from tblusers u  ";
            strQuery += " inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid   ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid  ";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " left join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid   ";
            strQuery += " left join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and  ";
            strQuery += " sptype.sSpecialityName !='Separator' and sptype.bisactive = 1  ";
            strQuery += " where u.bactivebyuser = 1 and ual.fkAccessLevelID <> 6 and  ";
            strQuery += " ud.fkDepartmentID = " + departmentid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel ";
            strQuery += " where fkaccesslevelid > " + AccessLevel + " ) and u.pkuserid != " + userid;

            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersActivatedByManager(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " left join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bactivebyuser = 1 and u.bActiveByAdmin = 1";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }


        public void LoadNormalUsersForECUser(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bactivebyuser = 1 and u.bActiveByAdmin = 1";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }

        public void LoadUsersActivatedByManager(int departmentid, int userid, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bactivebyuser = 1 and u.bActiveByAdmin = 1";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersDeActivatedByManager(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bactivebyuser = 1 and u.bActiveByAdmin = 0";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersDeActivatedByManager(int departmentid, int userid, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bactivebyuser = 1 and u.bActiveByAdmin = 0";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadFilteredUsers(int departmentid, int userid, string filterString)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID = us.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bActiveByUser = 1 and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid + " ";
            strQuery += filterString;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadFilteredUsers(int departmentid, int userid, string filterString, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID = us.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where u.bActiveByUser = 1 and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid + " ";
            strQuery += filterString;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersByStartYear(int departmentid, int userid, string year)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid + " ";
            strQuery += " and u.pkuserid in ( select c.fkuserid from dbo.tblUserContract c where Year(dEmploymentFromDate) = " + year + ")";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersByStartYear(int departmentid, int userid, string year, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID  and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid + " ";
            strQuery += " and u.pkuserid in ( select c.fkuserid from dbo.tblUserContract c where Year(dEmploymentFromDate) = " + year + ")";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersBySpeciality(int departmentid, int userid, string speciality)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid + " ";
            strQuery += " and u.pkuserid in (select distinct us.fkuserid from dbo.tblSpecialityType st inner join ";
            strQuery += " dbo.tblUserSpeciality us on us.fkSpecialityTypeID = st.pkSpecialityTypeID ";
            strQuery += " where st.sSpecialityName = '" + speciality + "') ";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersBySpeciality(int departmentid, int userid, string speciality, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid + " ";
            strQuery += " and u.pkuserid in (select distinct us.fkuserid from dbo.tblSpecialityType st inner join ";
            strQuery += " dbo.tblUserSpeciality us on us.fkSpecialityTypeID = st.pkSpecialityTypeID ";
            strQuery += " where st.sSpecialityName = '" + speciality + "') ";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersByCountry(int departmentid, int userid, int country)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid + " ";
            strQuery += " and u.fkNationalityCountry = " + country + " ";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersByCountry(int departmentid, int userid, int country, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid + " ";
            strQuery += " and u.fkNationalityCountry = " + country + " ";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersByGeneralSearchField(int departmentid, int userid, string search)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid + " ";
            strQuery += search;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersByGeneralSearchField(int departmentid, int userid, string search, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType sptype on sptype.pkSpecialityTypeID =  usp.fkSpecialityTypeID and sptype.special = 0 and sptype.sSpecialityName !='Separator' and sptype.bisactive = 1 ";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid + " ";
            strQuery += search;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersBySearchField(int departmentid, int userid, string search)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join dbo.tblUserSpeciality us on us.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType st on us.fkSpecialityTypeID = st.pkSpecialityTypeID";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid + " ";
            strQuery += " and st.sSpecialityName = '" + search + "'";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersBySearchField(int departmentid, int userid, string search, int AccessLevel)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID and ual.fkaccesslevelid = " + AccessLevel;
            strQuery += " inner join dbo.tblUserSpeciality us on us.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblSpecialityType st on us.fkSpecialityTypeID = st.pkSpecialityTypeID";
            strQuery += " where ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > " + AccessLevel + ") and u.pkuserid != " + userid + " ";
            strQuery += " and st.sSpecialityName = '" + search + "'";
            this.LoadFromRawSql(strQuery);
        }

        public void LoadSalariedUsers(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadStaff(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
           // strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) and u.pkuserid != " + userid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1 and fkaccesslevelid <> 3) and u.pkuserid != " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadManagers(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 and ual.fkAccessLevelID = 2 and ud.fkDepartmentID = " + departmentid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadManagersRequested(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.* from tblusers u inner join ";
            strQuery += " dbo.tblUserAccessLevel ual on ual.fkUserID = u.pkuserid inner join ";
            strQuery += " dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid ";
            strQuery += " where ual.fkAccessLevelID = 2 and ud.fkDepartmentID = " + departmentid;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadAccountManagers(int departmentid, int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 and ual.fkAccessLevelID = 4 and ud.fkDepartmentID = " + departmentid;
            this.LoadFromRawSql(strQuery);
        }


        public void LoadDepartmentUsers(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* ";
            strQuery += " from tblusers u left join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " left join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " left join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 ";
            strQuery += " and (ual.fkAccessLevelID <> 6 or ual.fkAccessLevelID is null) AND ud.fkDepartmentID = " + DepartmentID;

            this.LoadFromRawSql(strQuery);
        }
        public void LoadDepartmentUsersForDepartmentAdmin(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.*   ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on ud.fkuserid = u.pkuserid  ";
            strQuery += " inner join tblUserAccessLevel ual on ual.fkuserid = u.pkuserid ";
            strQuery += " where u.pkuserid not in ";
            strQuery += " (select distinct u.pkuserid from tblusers u inner join ";
            strQuery += " tblUserAccessLevel ual on ual.fkuserid = u.pkuserid ";
            strQuery += " where ual.fkaccesslevelid = 5 or ual.fkaccesslevelid = 6)  ";
            strQuery += " and u.bactivebyuser = 1 and u.bactivebyadmin= 1 and ud.fkDepartmentID = " + DepartmentID;

            this.LoadFromRawSql(strQuery);
        }
        public void LoadUsersForWorkShift(int DepartmentID, int spTypeID, int week, int year, int day)
        {
            string strQuery = string.Empty;
            strQuery += " select u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join tblUserSpeciality usp on usp.fkUserID = u.pkUserID  and usp.bIsPrimary = 1";
            strQuery += " inner join tblSpecialityType spType on spType.pkSpecialityTypeID =   usp.fkSpecialityTypeID ";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 and spType.pkSpecialityTypeID =  " + spTypeID;
            strQuery += " and ual.fkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid>1) AND ual.fkAccessLevelID <> 6 AND ud.fkDepartmentID = " + DepartmentID;
            strQuery += " And u.pkuserid not in (select fkuserid from tblUserWorkshifts where iweeknumber = " + week + " and iyear = " + year + " and idaynumber = " + day + ") ";

            this.LoadFromRawSql(strQuery);
        }
        public void GetAllUsersForMessages(int userid, int departmentid)
        {
            string strQuery = string.Empty;
            strQuery = " Select ut.pkUserID,ut.sUsername, ut.sPassword, ";
            strQuery += " ut.sFirstName +' '+ut.sLastName as FullName, ut.dModifiedDate from dbo.tblUsers ut  left join ";
            strQuery += " dbo.tblUserDepartment ud on ut.pkuserid = ud.fkuserid left join ";
            strQuery += " dbo.tblDepartments d on ud.fkDepartmentID  = d.pkDepartmentID ";
            strQuery += " where d.pkDepartmentid = " + departmentid + " and ut.bActiveByUser = 1 and ut.bActiveByAdmin = 1 and ut.pkuserid in ";
            strQuery += " (select distinct(ualT.fkUserID) from dbo.tblUserAccessLevel ualT ";
            strQuery += " where ualT.fkAccessLevelID in ";
            strQuery += " (select ual.fkAccessLevelID from dbo.tblUserAccessLevel ual ";
            strQuery += " where ual.fkuserid in ";
            strQuery += " (select u.pkuserid from dbo.tblUsers u ";
            strQuery += " where u.pkuserid = " + userid + ")))";
            this.LoadFromRawSql(strQuery);
        }
        public void GetUserDepartment(int userid)
        {
            string strQuery = string.Empty;
            strQuery = " select d.pkdepartmentid from tblusers u left join ";
            strQuery += " tbluserdepartment ud on u.pkuserid = ud.fkuserid left join ";
            strQuery += " tbldepartments d on ud.fkDepartmentID = d.pkDepartmentID ";
            strQuery += " where pkuserid = " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void GetUsersByName(string name)
        {
            this.Where.SFirstName.Value = name;
            this.Query.Load();
        }
        public void LoadUsers()
        {
            try
            {
                //ur.LoadFromSql("Sp_GetUsers");
                this.LoadFromRawSql("Sp_GetUsers");
            }
            catch (Exception ex)
            {

            }
        }
        public void GetAccountManager()
        {
            string strQuery = string.Empty;
            strQuery = " select u.sFirstName +' '+u.sLastName as FullName from tblusers u inner join ";
            strQuery += " tbluseraccesslevel a on a.fkuserid = u.pkuserid  ";
            strQuery += " where a.fkAccessLevelID = 4";
            this.LoadFromRawSql(strQuery);
        }
        public void GetDepartmentAdminID(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select ud.fkuserid from tblUserDepartment ud  ";
            strQuery += " where ud.fkUserID in ";
            strQuery += " (select distinct u.pkuserid from tblusers u inner join  ";
            strQuery += " tblUserAccessLevel ual on ual.fkuserid = pkuserid ";
            strQuery += " where ual.fkaccesslevelid  = 5) and fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(strQuery);
        }
        public void GetSalariedUsersIDs(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join tbluserworkshifts uws on uws.fkuserid = u.pkuserid ";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 ";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            this.LoadFromRawSql(strQuery);
        }
        public void GetSalariedUsers(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.*,uwp.* ";
            strQuery += " from tblusers u inner join tblUserDepartment ud on  u.pkUserid = ud.fkUserid ";
            strQuery += " inner join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid";
            strQuery += " inner join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID";
            strQuery += " inner join tbluserworkshifts uws on uws.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblUserWeeklyPayments uwp on uwp.fkuserid = u.pkuserid ";
            strQuery += " where u.bactivebyuser = 1 and u.bactivebyadmin= 1 and uwp.paidByAccountManager = 1 and uwp.receivedByUser = 0 ";
            strQuery += " and ual.fkAccessLevelID <> 6 and ud.fkDepartmentID = " + departmentid;
            strQuery += " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            this.LoadFromRawSql(strQuery);
        }
        public void GetUserProfile(int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,u.pkuserid, ";
            strQuery += " ue.sEmail, um.sMobilePhone, uad.* from tblusers u left join  ";
            strQuery += " dbo.tblUserEmails ue on ue.fkuserid = u.pkuserid and ue.bIsPrimary = 1 left join ";
            strQuery += " dbo.tblUserMobile um on um.fkuserid = u.pkuserid and um.bIsPrimary = 1 left join ";
            strQuery += " dbo.tblUserAddresses uad on uad.fkuserid = u.pkuserid and uad.bIsPrimary = 1";
            strQuery += " where u.pkuserid = " + userid + " and u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            this.LoadFromRawSql(strQuery);
        }
        public void InActiveUsers_Bonus(int userid, int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and u.bActiveByAdmin = 0 or u.bActiveByUser = 0 and ua.fkaccesslevelid = 1 ";
            strQuery += " and u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            this.LoadFromRawSql(strQuery);
        }
        public void ActiveUsers_Bonus(int userid, int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and u.bActiveByAdmin = 1 and u.bActiveByUser = 1 and ua.fkaccesslevelid = 1 ";
            strQuery += " and u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            this.LoadFromRawSql(strQuery);
        }
        public void AllUsers_Bonus(int userid, int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and ua.fkaccesslevelid = 1 and u.bActiveByUser = 1";
            strQuery += " and u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            this.LoadFromRawSql(strQuery);
        }
        public void InActiceManagers_Bonus(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and u.bActiveByAdmin = 0";
            this.LoadFromRawSql(strQuery);
        }
        public void ActiveManagers_Bonus(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and u.bActiveByAdmin = 1 and u.bActiveByUser = 1";
            this.LoadFromRawSql(strQuery);
        }
        public void AllManagers_Bonus(int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where u.bActiveByUser = 1 and ud.fkDepartmentID = " + departmentid;
            this.LoadFromRawSql(strQuery);
        }
        public void filterUsers_Bonus(int departmentid, int userid, string filterString)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* from dbo.tblUsers u  ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and ua.fkaccesslevelid = 1 and u.bActiveByUser = 1";
            strQuery += " and  u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            strQuery += filterString;
            this.LoadFromRawSql(strQuery);
        }
        public void filterUsers_BonusPopUp(int departmentid, int userid, string filterString)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.* from dbo.tblUsers u  ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " inner join dbo.tblUserBonuses ub on ub.fkuserid = u.pkuserid ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and ua.fkaccesslevelid = 1 ";
            strQuery += " and u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            strQuery += filterString;
            this.LoadFromRawSql(strQuery);
        }
        public void filterManagers_Bonus(int departmentid, string filterString)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where u.bActiveByUser = 1 and ud.fkDepartmentID = " + departmentid;
            strQuery += filterString;
            this.LoadFromRawSql(strQuery);
        }
        public void filterManagers_BonusPopUp(int departmentid, string filterString)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct top 1 u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1  and us.fkSpecialityTypeID < 1";
            strQuery += " inner join dbo.tblUserBonuses ub on ub.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where ud.fkDepartmentID = " + departmentid;
            strQuery += filterString;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadSearchUsers_Bonus(int userid, int departmentid, string search)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and ua.fkaccesslevelid = 1 and u.bActiveByUser = 1 ";
            strQuery += " and u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            strQuery += search;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadSearchUsers_BonusPopUp(int userid, int departmentid, string search)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 ";
            strQuery += " inner join dbo.tblUserBonuses ub on ub.fkuserid = u.pkuserid ";
            strQuery += " where ud.fkDepartmentID = " + departmentid + " and ua.fkaccesslevelid = 1 ";
            strQuery += " and u.pkuserid != " + userid + " and u.pkuserid not in ( select fkuserid from tbluseraccesslevel where fkaccesslevelid > 1) ";
            strQuery += search;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadSearchManagers_Bonus(int departmentid, string search)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1 and u.bActiveByUser = 1";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where ud.fkDepartmentID = " + departmentid;
            strQuery += search;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadSearchManagers_BonusPopUp(int departmentid, string search)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct  u.sFirstName + ' ' + isnull(slastname ,'') as FullName ,  u.* from dbo.tblUsers u ";
            strQuery += " inner join dbo.tblUserDepartment ud on ud.fkuserid = pkuserid ";
            strQuery += " left join dbo.tblUserContract uc on uc.fkUserID = u.pkuserid ";
            strQuery += " left join dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid and us.bIsPrimary = 1  and us.fkSpecialityTypeID < 1";
            strQuery += " inner join dbo.tblUserBonuses ub on ub.fkuserid = u.pkuserid ";
            strQuery += " inner join dbo.tblUserAccessLevel ua on ua.fkuserid = u.pkuserid and (ua.fkaccesslevelid = 4 or ua.fkaccesslevelid = 2)";
            strQuery += " where ud.fkDepartmentID = " + departmentid;
            strQuery += search;
            this.LoadFromRawSql(strQuery);
        }
        public void getDepartmentAdmin(int departmentid)
        {
            string query = string.Empty;
            query += " select  u.pkuserid, u.sfirstname + isnull(u.slastname,'') as FullName from tblusers u inner join  ";
            query += " tbluseraccesslevel ual on ual.fkuserid = u.pkuserid inner join ";
            query += " tbluserdepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid = 1 ";
            query += " where ual.fkAccessLevelID = 5";
            this.LoadFromRawSql(query);
        }
        public void getDepartmentManager(int Department)
        {
            string query = string.Empty;
            query += " select u.pkuserid,u.sFirstName + ' ' + isnull(slastname ,'') as FullName  from tblusers u inner join ";
            query += " dbo.tblUserDepartment ud on ud.fkuserid = u.pkuserid and ud.fkdepartmentid = " + Department + " inner join ";
            query += " dbo.tblUserAccessLevel ual on ual.fkuserid = u.pkuserid inner join ";
            query += " dbo.tblAcessLevel al on al.pkAccessLevelID =  ual.fkAccessLevelID and (al.pkAccessLevelID = 2 or  al.pkAccessLevelID = 4) and al.pkAccessLevelID != 1 and al.pkAccessLevelID != 3 and al.pkAccessLevelID != 5 and al.pkAccessLevelID != 6";
            query += " where u.bActiveByUser = 1 and u.bActiveByAdmin = 1 ";
            this.LoadFromRawSql(query);
        }
        public void GetAllSalaries(int weeknumber, int departmentid, DateTime weekstartdate, DateTime weekenddate)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekNumber", weeknumber);
            Params.Add("@departmentid", departmentid);
            Params.Add("@WeekStartDate", weekstartdate);
            Params.Add("@WeekEndDate", weekenddate);
            this.LoadFromSql("GetWeekSalary", Params);
        }
        public void GetPaymentForSingle(int userid, int weeknumber, int departmentid, DateTime weekstartdate, DateTime weekenddate)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@Userid", userid);
            Params.Add("@weekNumber", weeknumber);
            Params.Add("@departmentid", departmentid);
            Params.Add("@WeekStartDate", weekstartdate);
            Params.Add("@WeekEndDate", weekenddate);
            this.LoadFromSql("GetWeekSalaryForSingleUser", Params);
        }
        public void GetSpecialUsers(int departmentid)
        {
            string query = string.Empty;
            query += " select u.sFirstName + ' '+isnull(u.sLastName,'') as FullName,sptype.sSpecialityName, u.* ";
            query += " from dbo.tblusers u  ";
            query += " INNER join tbluserdepartment ud on u.pkuserid = ud.fkuserid ";
            query += " inner join tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            query += " inner join dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = usp.fkspecialitytypeid and sptype.special = 1 ";
            query += " where ud.fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void GetSpecialUsersBySpeciality(int departmentid)
        {
            string query = string.Empty;
            query += " select u.sFirstName + ' '+isnull(u.sLastName,'') as FullName,sptype.sSpecialityName, u.* ";
            query += " from dbo.tblusers u  ";
            query += " INNER join tbluserdepartment ud on u.pkuserid = ud.fkuserid ";
            query += " inner join tblUserSpeciality usp on usp.fkuserid = u.pkuserid ";
            query += " inner join dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = usp.fkspecialitytypeid and usp.bIsPrimary = 1 and sptype.special = 1 ";
            query += " where ud.fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void GetUser(int userid)
        {
            string query = string.Empty;
            query += " select u.sFirstName + ' '+isnull(u.sLastName,'') as FullName, u.pkuserid from tblusers u where u.pkuserid =" + userid;
            this.LoadFromRawSql(query);
        }
        public void CheckPassword(string password)
        {
            this.Where.SPassword.Value = password;
            this.Query.Load();
        }
        public void GetStaffForStats(DateTime weekstartdate, DateTime weekenddate, int weeknumber, int departmentid, int userid)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@WeekStartDate", weekstartdate);
            Params.Add("@WeekEndDate", weekenddate);
            Params.Add("@weeknumber", weeknumber);
            Params.Add("@departmentid", departmentid);
            Params.Add("@Userid", userid);
            this.LoadFromSql("spGetStaffForStats", Params);
        }
        public void getStaffNameForStatistics(int year, int departmentid, int specialityid)
        {
            string query = string.Empty;
            query += " select distinct u.pkuserid,u.sfirstname +' '+ isnull(u.slastname,'') as FullName from tblusers  u inner join ";
            query += " dbo.tblUserSpeciality us on us.fkUserID = u.pkuserid inner join ";
            query += " dbo.tblSpecialityType sptypes on sptypes.pkSpecialityTypeID = us.fkSpecialityTypeID ";
            query += " where sptypes.pkSpecialityTypeID in ";
            query += " (select distinct s.fkSpecialityTypeID from dbo.tblSpeciality s ";
            query += " where s.sSpeciality != 'Separator' and s.fkdepartmentid = " + departmentid + " and s.pkSpecialityID in ( ";
            query += " select distinct uws.fkSpecialityID from dbo.tblUserWorkshifts uws ";
            query += " where uws.iyear = " + year + ")) and fkSpecialityTypeID = " + specialityid;
            query += " and u.pkuserid not in (select fkuserid from tbluseraccesslevel where fkaccesslevelid=3 or fkaccesslevelid=4 or fkaccesslevelid=2 or fkaccesslevelid=5 or fkaccesslevelid=6)  ";
            this.LoadFromRawSql(query);
        }
        public void getuserInfoForStats(int userid)
        {
            string query = string.Empty;
            query += " select distinct u.sfirstname +'' +u.slastname as fullname,u.sImagePath,  ue.sEmail, um.sMobilePhone, ";
            query += " uad.sAddressStreet+''+ uad.sAddressTown +' '+uad.sAddressPostCode +' '+uad.sAddressRegion  ";
            query += " +' '+(select c.sCountry from dbo.tblCountries c where c.pkCountryID = uad.fkAddressCountry) as useraddress ";
            query += " from tblusers u left join ";
            query += " dbo.tblUserEmails ue on ue.fkuserid = u.pkuserid left join ";
            query += " dbo.tblUserMobile um on um.fkuserid = u.pkuserid left join ";
            query += " dbo.tblUserAddresses uad on uad.fkuserid = u.pkuserid ";
            query += " where u.pkuserid = " + userid + " and ue.bIsPrimary = 1 and um.bIsPrimary = 1 and uad.bIsPrimary = 1";
            this.LoadFromRawSql(query);
        }

    }
}
