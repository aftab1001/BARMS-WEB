using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblCountries : LC.Model.BMS.DAL._tblCountries
    {
        public void LoadByName(string name)
        {
            this.Where.SCountry.Value = name;
            this.Query.Load();
        }
        public void GetAllCountriesAlphabetically()
        {
            string strQuery = " select * from dbo.tblCountries order by sCountry ";
            this.LoadFromRawSql(strQuery);
        }
    }
}
