using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LC.Model.BMS.BLL
{
    public class tblUserAccessLevel : LC.Model.BMS.DAL._tblUserAccessLevel
    {
        public void LoadAccessLevel(int userID)
        {
            this.FlushData();
            string strQuery = string.Empty;
            strQuery += " Select max(fkAccessLevelID) AccessLevel from tblUserAccessLevel where fkUserID = " + userID;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUserAccessLevels(int UserID)
        {
            this.FlushData();
            string strQuery = string.Empty;
            strQuery += " Select u.sfirstname + isnull(u.slastname,'') as FullName ,UAL.pkUserAccessLevel , AL.sAccessLevel , UAL.dCreateDate ";

            strQuery += " From tblusers u left join  tblUserAccessLevel UAL on u.pkuserid = ual.fkuserid inner join tblAcessLevel AL on UAL.fkAccessLevelID = AL.pkAccessLevelID ";
            strQuery += " Where UAL.fkUserID =  "  + UserID;
            this.LoadFromRawSql(strQuery);
        }
        public void CheckAccessAlreadyExist(int AccessID, int UserID)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.FkUserID.Value = UserID;
            this.Where.FkAccessLevelID.Value = AccessID;
            this.Query.Load();
        }
        public void IsManagerExist(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct ual.fkaccesslevelid from tblAcessLevel al ";
            strQuery += " inner join dbo.tblUserAccessLevel ual on ual.fkAccessLevelID = al.pkAccessLevelID where ual.fkaccesslevelid= 2 and ual.fkuserid in ";
            strQuery += "(select distinct u.pkuserid from tblusers u left join tblUserDepartment ud on  u.pkUserid = ud.fkUserid    ";
            strQuery += " left join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid  ";
            strQuery += " left join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID where u.bactivebyuser = 1 ";
            strQuery += " and u.bactivebyadmin= 1  and (ual.fkAccessLevelID <> 6 or ual.fkAccessLevelID is null) AND ud.fkDepartmentID = "+DepartmentID+")";

            this.LoadFromRawSql(strQuery);
        }

        public void IsAccountManagerExist(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct ual.fkaccesslevelid from tblAcessLevel al ";
            strQuery += " inner join dbo.tblUserAccessLevel ual on ual.fkAccessLevelID = al.pkAccessLevelID where ual.fkaccesslevelid= 4 and ual.fkuserid in ";
            strQuery += "(select distinct u.pkuserid from tblusers u left join tblUserDepartment ud on  u.pkUserid = ud.fkUserid    ";
            strQuery += " left join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid  ";
            strQuery += " left join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID where u.bactivebyuser = 1 ";
            strQuery += " and u.bactivebyadmin= 1  and (ual.fkAccessLevelID <> 6 or ual.fkAccessLevelID is null) AND ud.fkDepartmentID = " + DepartmentID + ")";

            this.LoadFromRawSql(strQuery);
        }
        public void IsDepartmentManagerExist(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct ual.fkaccesslevelid , ual.pkuseraccesslevel from tblAcessLevel al ";
            strQuery += " inner join dbo.tblUserAccessLevel ual on ual.fkAccessLevelID = al.pkAccessLevelID where ual.fkaccesslevelid= 5 and ual.fkuserid in ";
            strQuery += "(select distinct u.pkuserid from tblusers u left join tblUserDepartment ud on  u.pkUserid = ud.fkUserid    ";
            strQuery += " left join tblDepartments d on ud.fkdepartmentid = d.pkdepartmentid  ";
            strQuery += " left join tblUserAccessLevel  ual on ual.fkUserID = u.pkUserID where u.bactivebyuser = 1 ";
            strQuery += " and u.bactivebyadmin= 1  and (ual.fkAccessLevelID <> 6 or ual.fkAccessLevelID is null) AND ud.fkDepartmentID = " + DepartmentID + ")";

            this.LoadFromRawSql(strQuery);
        }
        public DataTable LoadAllUserId(int AccessLevelID, int departmentid)
        {
            string strQuery = string.Empty;
            strQuery += " SELECT tblUserAccessLevel.fkUserID, tblUserAccessLevel.fkAccessLevelID,tblUserDepartment.fkDepartmentID  ";
            strQuery += " FROM tblUserAccessLevel INNER JOIN tblUserDepartment ON tblUserAccessLevel.fkUserID = tblUserDepartment.fkUserID ";
            strQuery += " where tblUserAccessLevel.fkAccessLevelID = " + AccessLevelID + "  and tblUserDepartment.fkDepartmentID = " + departmentid;
            this.LoadFromRawSql(strQuery);
            return this.DataTable;
        }
    }
}
