using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblCompanyAddresses : LC.Model.BMS.DAL._tblCompanyAddresses
    {
        public void GetCompanyAddresses(int companyid)
        {
            string query = string.Empty;
            query += " select ca.*,c.sCountry from dbo.tblCompanyAddresses ca inner join  ";
            query += " dbo.tblCountries c on c.pkCountryID = ca.fkAddressCountry ";
            query += " where ca.fkCompanyId =  " + companyid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst()
        {
            string query = string.Empty;
            query += "select Top(1) ca.* from dbo.tblCompanyAddresses ca ";
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int CompanyAddressID)
        {
            string query = string.Empty;
            query += "select Top(1) ca.* from dbo.tblCompanyAddresses ca where ca.pkCompanyAddressID != " + CompanyAddressID;
            this.LoadFromRawSql(query);
        }
    }
}
