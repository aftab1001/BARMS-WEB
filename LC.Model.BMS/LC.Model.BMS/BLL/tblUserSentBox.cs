using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserSentBox : LC.Model.BMS.DAL._tblUserSentBox
    {
        public void GetAllSentMessages(int UserID)
        {
            string query = string.Empty;
            query = "Select us.pkSentBoxID, us.fkFromUserID,us.fkToUserID,u2.sFirstName+' '+u2.sLastName as UserFrom, u.sFirstName+' '+u.sLastName as name, ";
            query += "us.sSubject, us.sMessage, us.dSentDate  from dbo.tblUserSentBox us inner join ";
            query += "dbo.tblUsers u on u.pkuserid = us.fkToUserID inner join ";
            query += "dbo.tblUsers u2 on u2.pkuserid  = us.fkFromUserID ";
            query += "where fkFromUserID = " + UserID +" ";
            query += "order by dSentDate desc";
            this.LoadFromRawSql(query);
        }        
    }
}
