using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserAddresses : LC.Model.BMS.DAL._tblUserAddresses
    {
        public void LoadUserAddress(int UserID)
        {
            string strQuery = string.Empty;
            strQuery += " Select UAD.* , C.sCountry ";
            strQuery += " From dbo.tblUserAddresses UAD inner join tblCountries C on UAD.fkAddressCountry = C.pkCountryID ";
            strQuery += " where UAD.fkUserID =  " + UserID;
            this.LoadFromRawSql(strQuery);
        }
        public void LoadUserAddressActive(int userid)
        {
            this.FlushData();
            this.Where.BIsPrimary.Value = true;
            this.Where.FkUserID.Value = userid;
            this.Query.Load();
        }
    }
}
