using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserMobile : LC.Model.BMS.DAL._tblUserMobile
    {
        //LoadUserMobiles
        public void LoadUserMobiles(int UserID)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.FkUserID.Value = UserID;
            this.Query.Load();

        }
        public void LoadUserMobilesActive(int userid)
        {
            this.FlushData();
            this.Where.BIsPrimary.Value = true;
            this.Where.FkUserID.Value = userid;
            this.Query.Load();
        }
    }
}
