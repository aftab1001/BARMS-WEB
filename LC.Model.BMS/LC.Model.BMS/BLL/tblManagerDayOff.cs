using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblManagerDayOff : LC.Model.BMS.DAL._tblManagerDayOff
    {
        public void getSingleDayOff(int userid, DateTime dayoff)
        {
            string query = string.Empty;
            query += "select mdo.* from tblmanagerdayoff mdo where mdo.msingledate = '" + dayoff + "' and fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }
        public void getRangeDayOff(int userid, DateTime rangedate)
        {
            string query = string.Empty;
            query += "select mdo.* from tblmanagerdayoff mdo where '" + rangedate + "' >= mdo.mstartdate and '" + rangedate + "' <= mdo.menddate and fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }
        public void getAllSingleDayOff(DateTime rangedateStart, DateTime rangedateEnd)
        {
            string query = string.Empty;
            query += "select mdo.* from tblmanagerdayoff mdo where  mdo.msingledate <='" + rangedateEnd + "' and mdo.msingledate>= '" + rangedateStart + "'";
            this.LoadFromRawSql(query);
        }
        public void getAllRangeDayOff(DateTime rangedateStart, DateTime rangedateEnd)
        {
            string query = string.Empty;
            query += "select mdo.* from tblmanagerdayoff mdo where  mdo.mstartdate>='" + rangedateStart + "' and  mdo.menddate <='" + rangedateEnd + "'";
            this.LoadFromRawSql(query);
        }
        public void getDateAvailable(DateTime d)
        {
            string query = string.Empty;
            query += " select m.* from dbo.tblManagerDayOff m where m.msingledate = '" + d + "'";
            this.LoadFromRawSql(query);
        }
        public void getALLoffDays()
        {
            string query = string.Empty;
            query += " select m.* from dbo.tblManagerDayOff m";
            this.LoadFromRawSql(query);
        }
        public void getSingleDayOffforSalery(int userid, DateTime rangedateStart, DateTime rangedateEnd)
        {
            string query = string.Empty;
            query += "select mdo.* from tblmanagerdayoff mdo where mdo.msingledate > '" + rangedateStart + "' and mdo.msingledate < '" + rangedateEnd + "' and fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }
    }
}
