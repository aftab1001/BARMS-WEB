using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblBaseCategories : LC.Model.BMS.DAL._tblBaseCategories
    {
        public void GetDepartmentBaseCategories(int departmentid)
        {
            string query = string.Empty;
            //query += "select b.* from dbo.tblBaseCategories b where b.fkDepartmentID = " + departmentid;
            query += " select b.*,v.* from dbo.tblBaseCategories b inner join ";
            query += " dbo.tblVAT v on v.pkvatid = b.fkvatid where b.fkdepartmentid= " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void GetActiveBaseCate(int basecat, int departmentid)
        {
            string query = string.Empty;
            query += " select b.* from dbo.tblBaseCategories b where b.isActive = 1 and b.pkbasecategoryid != " + basecat + "  and b.fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void GetAllBase(int departmentid)
        {
            this.Where.FkDepartmentID.Value = departmentid;
            this.Where.IsActive.Value = true;
            this.Query.Load();
        }
    }
}
