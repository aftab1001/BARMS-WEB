using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblDepartments : LC.Model.BMS.DAL._tblDepartments
    {
        public void CheckDepartmentName(string sName)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SDepartmentName.Value = sName;
            this.Query.Load();
        }
        public void CheckDepartmentHasAdmin(int DepartmentID)
        {

            string strQuery = string.Empty;
            strQuery += " Select * from tblDepartments where pkDepartmentID = " + DepartmentID;
            strQuery += " AND fkManagerUserID > 0 ";
            this.LoadFromRawSql(strQuery);
        }
        public void CheckDepartmentHasAccountManager(int DepartmentID)
        {

            string strQuery = string.Empty;
            strQuery += " Select * from tblDepartments where pkDepartmentID = " + DepartmentID;
            strQuery += " AND fkAccountManagerUserID > 0 ";
            this.LoadFromRawSql(strQuery);
        }
        public void LoadDepartmentByMenagerID(int userid)
        {
            this.Where.FkManagerUserID.Value = userid;
            this.Query.Load();
        }
        public void GetDepartmentsWithDepartmentAdminName()
        {
            string strQuery = string.Empty;
            strQuery += " select  d.*, isnull([dbo].[GetDepartmentAdminName](pkdepartmentid),'') as dname, ";
            strQuery += " isnull([dbo].[GetDepartmentAdminID](pkdepartmentid),'') as id from dbo.tblDepartments d ";
            this.LoadFromRawSql(strQuery);
        }
    }
}
