using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblCompanyEmails:LC.Model.BMS.DAL._tblCompanyEmails
    {
        public void GetCompanyEmails(int companyid)
        {
            string query = string.Empty;
            query +=" select ce.*  from dbo.tblCompanyEmails ce where  ce.fkCompanyID =  " + companyid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst()
        {
            string query = string.Empty;
            query += "select Top(1) ce.*  from dbo.tblCompanyEmails ce";
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int CompanyEmailID)
        {
            string query = string.Empty;
            query += "select ce.*  from dbo.tblCompanyEmails ce where ce.pkCompanyEmailID != " + CompanyEmailID;
            this.LoadFromRawSql(query);
        }
        public void CheckCompanyEmail(string email)
        {
            string query = string.Empty;
            query += " select ce.*  from dbo.tblCompanyEmails ce where ce.cEmails = '" + email + "'";
            this.LoadFromRawSql(query);
        }
    }
}
