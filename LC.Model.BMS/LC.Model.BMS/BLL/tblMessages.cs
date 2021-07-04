using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblMessages:LC.Model.BMS.DAL._tblMessages
    {
        public void GetMessageRowByInboxID(int InboxID)
        {
            this.Where.FkInboxID.Value = InboxID;
            this.Query.Load();
 
        }
        public void GetMessageRowBySentboxID(int SentboxID)
        {
            this.Where.FkSentBoxID.Value = SentboxID;
            this.Query.Load();

        }
    }
}
