using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblReply:LC.Model.BMS.DAL._tblReply
    {
        public void GetAllInBoxReplyMessages(int fkMessageID, int fromUserid, int ToUserid)
        {
            this.Where.FkMessageID.Value = fkMessageID;
            this.Where.FkReplyFromUserID.Value = fromUserid;
            this.Where.FkReplyToUserID.Value = ToUserid;
            this.Query.Load();
        }
        public void GetAllReplies(int fkMessageID)
        {
            string query = string.Empty;
            query = " select r.*,uFrom.sFirstName+' '+uFrom.sLastName as SenderName, uTo.sFirstName+' '+uTo.sLastName as ReceiverName from dbo.tblReply r inner join ";
            query += " dbo.tblUsers uFrom on r.fkReplyFromUserID = uFrom.pkUserid inner join ";
            query += " dbo.tblUsers uTo on r.fkReplyToUserID = uTo.pkUserid ";
            query += " where r.FkMessageID = " + fkMessageID+" ";
            query += " order by r.dReplyDate desc ";
            this.LoadFromRawSql(query);

        }

    }
}
