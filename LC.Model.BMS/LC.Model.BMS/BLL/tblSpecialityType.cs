using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSpecialityType : LC.Model.BMS.DAL._tblSpecialityType
    {
        public void GetNormalSpecialtyTypes()
        {
            string strQuery = string.Empty;
            strQuery = " select spType.* from tblSpecialityType spType where spType.sSpecialityName != 'Separator' and Special = 0 ";
            this.LoadFromRawSql(strQuery);
        }

        public void checkSpecialUser(int userid)
        {
            string strQuery = string.Empty;
            strQuery += " select u.pkuserid from tblusers u inner join ";
            strQuery += " dbo.tblUserSpeciality us on us.fkuserid = u.pkuserid inner join ";
            strQuery += " dbo.tblSpecialityType sptype on sptype.pkspecialitytypeid = us.fkSpecialityTypeID ";
            strQuery += " where sptype.Special = 1 and sptype.sSpecialityName != 'Separator' and u.pkuserid = " + userid;
            this.LoadFromRawSql(strQuery);
        }
        public void GetSpecialityTypesWithoutSeparator()
        {
            string strQuery = string.Empty;
            strQuery = " select spType.* from tblSpecialityType spType where spType.sSpecialityName != 'Separator' ";
            this.LoadFromRawSql(strQuery);
        }
        public void SpecialityTypeName(string SpecialityTypeName)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SSpecialityName.Value = SpecialityTypeName;
            this.Query.Load();

        }
        public void GetSeparator()
        {
            string query = string.Empty;
            query += " select t.* from dbo.tblSpecialityType t where t.sspecialityname = 'Separator' ";
            this.LoadFromRawSql(query);
        }
        public void getSpecialSpecialtyies()
        {
            string query = string.Empty;
            query += "select sptype.* from dbo.tblSpecialityType sptype where sptype.special = 1 and sptype.bisactive = 1 and spType.sSpecialityName != 'Separator'";
            this.LoadFromRawSql(query);
        }
    }
}
