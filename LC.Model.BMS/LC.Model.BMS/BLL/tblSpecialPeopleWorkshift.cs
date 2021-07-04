using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSpecialPeopleWorkshift : LC.Model.BMS.DAL._tblSpecialPeopleWorkshift
    {
        public void getSpeciaUserForDay(int weeknumber, int year, int day, int userid)
        {
            string query = string.Empty;
            query += " select uws.* from dbo.tblUserWorkshifts uws ";
            query += " where  uws.iWeeknumber = " + weeknumber + " and uws.iYear = " + year + " and uws.iDaynumber = " + day + " and uws.fkuserid = " + userid;
            this.LoadFromRawSql(query);
        }
    }
}
