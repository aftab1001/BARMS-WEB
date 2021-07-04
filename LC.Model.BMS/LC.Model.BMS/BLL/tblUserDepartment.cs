using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserDepartment : LC.Model.BMS.DAL._tblUserDepartment
    {
         public void CheckDepartmentInUse(int ID)
        {
            this.Where.WhereClauseReset();
            this.Where.FkDepartmentID.Value = ID;
            this.Query.Load();
        }
        public void LoadUserDepartment(int UserID)
        {
            this.Where.WhereClauseReset();
            this.Where.FkUserID.Value = UserID;
            this.Query.Load();
        }
    }
}
