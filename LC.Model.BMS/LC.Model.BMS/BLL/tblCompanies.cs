using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblCompanies:LC.Model.BMS.DAL._tblCompanies
    {
        public void GetCompanies(int supplierid)
        {
            this.Where.FkSuplierID.Value = supplierid;
            this.Query.Load();
        }
    }
}
