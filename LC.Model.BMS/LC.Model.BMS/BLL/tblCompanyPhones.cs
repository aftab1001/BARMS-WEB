using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
   public class tblCompanyPhones:LC.Model.BMS.DAL._tblCompanyPhones
    {
        public void GetCompanyPhones(int companyid)
        {
            string query = string.Empty;
            query += " select cp.* from dbo.tblCompanyPhones cp where cp.fkCompanyID =   " + companyid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst()
        {
            string query = string.Empty;
            query += "select top(1) cp.* from dbo.tblCompanyPhones cp";
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int CompanyPhoneID)
        {
            string query = string.Empty;
            query += "select cp.* from dbo.tblCompanyPhones cp where cp.pkCompanyPhoneID != " + CompanyPhoneID;
            this.LoadFromRawSql(query);
        }
        public void CheckCompanyPhone(string phone)
        {
            string query = string.Empty;
            query += " select cp.* from dbo.tblCompanyPhones cp where cp.cPhones = '" + phone + "'";
            this.LoadFromRawSql(query);
        }
    }
}
