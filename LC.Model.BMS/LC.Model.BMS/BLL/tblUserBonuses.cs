using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserBonuses : LC.Model.BMS.DAL._tblUserBonuses
    {
        public void GetUserBonus(int userid)
        {
            string strQuery = string.Empty;
            strQuery = "select ub.* from dbo.tblUserBonuses ub inner join  ";
            strQuery += " tblusers u on u.pkuserid = ub.fkuserid where ub.fkuserid = " + userid + "  and u.bActiveByUser = 1 and u.bActiveByAdmin = 1  order by ub.dCreatedDate desc";
            this.LoadFromRawSql(strQuery);
        }
        public void GetFilterdUserBonuses(int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select distinct top 1 ub.*, u.sFirstName + ' ' + isnull(slastname ,'') as FullName , u.*, ub.bonus, ub.dModifiedDate ";
            strQuery += " from dbo.tblUserBonuses ub inner join tblusers u on u.pkuserid = ub.fkuserid   where ub.fkuserid = " + userid + " ";
            strQuery += " and u.bActiveByUser = 1 and u.bActiveByAdmin = 1  order by ub.dModifiedDate desc ";
            this.LoadFromRawSql(strQuery);
        }
    }
}
