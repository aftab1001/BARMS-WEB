using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserEmails : LC.Model.BMS.DAL._tblUserEmails
    {
        public void LoadUserEmails(int UserID)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.FkUserID.Value = UserID;
            this.Query.Load();

        }
        public void CheckEmail(string email, int UserID)
        {
            this.FlushData();
            this.Where.FkUserID.Value = UserID;
            this.Where.SEmail.Value = email;
            this.Query.Load();
        }
        public void LoadUserEmailsActive(int userid)
        {
            this.FlushData();
            this.Where.BIsPrimary.Value = true;
            this.Where.FkUserID.Value = userid;
            this.Query.Load();
        }
    }
}
