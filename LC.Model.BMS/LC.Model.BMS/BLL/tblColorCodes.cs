using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblColorCodes:LC.Model.BMS.DAL._tblColorCodes
    {
        public void GetColor(int iManagerID)
        {
            this.Where.Fkuserid.Value = iManagerID;
            this.Query.Load();
        }
    }
}
