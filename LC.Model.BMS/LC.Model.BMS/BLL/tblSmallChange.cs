using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSmallChange : LC.Model.BMS.DAL._tblSmallChange
    {
        public void GetSmallChanges(DateTime start, DateTime end)
        {
            string query = string.Empty;
            query += " select sm.* from tblsmallChange sm  where sm.dmodifieddate  >= '" + start + "' and sm.dmodifieddate  <= '" + end + "' ";
            this.LoadFromRawSql(query);
        }
    }
}
