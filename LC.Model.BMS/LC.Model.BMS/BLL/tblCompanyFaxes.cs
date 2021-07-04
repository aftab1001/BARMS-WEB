using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblCompanyFaxes:LC.Model.BMS.DAL._tblCompanyFaxes
    {
        public void GetCompanyFaxes(int companyid)
        {
            string query = string.Empty;
            query += "select cf.* from dbo.tblCompanyFaxes cf where cf.fkCompanyID =  " + companyid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst()
        {
            string query = string.Empty;
            query += "select Top(1) cf.* from dbo.tblCompanyFaxes cf";
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int CompanyFaxID)
        {
            string query = string.Empty;
            query += "select cf.* from dbo.tblCompanyFaxes cf where cf.pkCompanyFaxID != " + CompanyFaxID;
            this.LoadFromRawSql(query);
        }
        public void CheckCompanyFax(string fax)
        {
            string query = string.Empty;
            query += "select cf.* from dbo.tblCompanyFaxes cf  where cf.cFax = '" + fax + "'";
            this.LoadFromRawSql(query);
        }
    }
}
