using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserInBox : LC.Model.BMS.DAL._tblUserInBox
    {
        public void GetAllReveivedMessages(int UserID)
        {
            string query = string.Empty;
            query = "Select ui.pkInBoxID, ui.fkFromUserID,ui.fkToUserID,ui.bIsread,u2.sFirstName+' '+u2.sLastName as UserFrom, ";
            query += "u.sFirstName+' '+u.sLastName as name,  ";
            query += "ui.sSubject, ui.sMessage, ui.dReceivedDate  from dbo.tblUserInBox ui inner join ";
            query += "dbo.tblUsers u on u.pkuserid = ui.fkToUserID inner join ";
            query += "dbo.tblUsers u2 on u2.pkuserid  = ui.fkFromUserID ";
            query += "where ui.fkToUserID = " + UserID + " ";
            query += "order by ui.bIsread , ui.dReceivedDate desc";
            this.LoadFromRawSql(query);
        }

        public void GetAlleMessagebyReply(int userid)
        {
            string query = string.Empty;
            query = " select  m.fkinboxid as pkinboxid,m.fksentboxid as pksentboxid,m.dmessagedate as dReceivedDate,ur.sFirstName +' '+ur.sLastName as userfrom , 1 as bisread,uf.sFirstName +' '+uf.sLastName as name , ";
            query +=" r.fkreplytouserid as fkToUserID,r.fkreplyfromuserid as fkFromUserID, ";
            query +=" r.replysubject as sSubject,r.replymessage as sMessage from tblmessages m inner join ";
            query +=" tblreply r on r.fkmessageid = m.pkmessageid inner join ";
            query += " tblusers ur on ur.pkuserid = r.fkreplyfromuserid  inner join tblusers uf on uf.pkuserid = r.fkReplyToUserID  where fksentboxid in ";
            query +=" (Select us.pkSentBoxID from dbo.tblUserSentBox us inner join ";
            query +=" dbo.tblUsers u on u.pkuserid = us.fkToUserID inner join  ";
            query += " dbo.tblUsers u2 on u2.pkuserid  = us.fkFromUserID where fkFromUserID = 10 )";
            this.LoadFromRawSql(query);
        }
    }
}
