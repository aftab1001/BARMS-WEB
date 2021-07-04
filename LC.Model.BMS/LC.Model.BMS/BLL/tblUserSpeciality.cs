using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
  public  class tblUserSpeciality:LC.Model.BMS.DAL._tblUserSpeciality
    {
      public void CheckSpeciality(int SpecialityID, int UserID)
      {
          this.FlushData();
          this.Where.FkUserID.Value = UserID;
          this.Where.FkSpecialityTypeID.Value = SpecialityID;
          this.Query.Load();
      }
      public void LoadUserSpeciality(int userid)
      {
          this.FlushData();
          string strQuery = string.Empty;
          strQuery += " select usp.*,spType.sSpecialityName from tblUserSpeciality usp inner join tblSpecialityType spType ";
          strQuery += " on usp.fkSpecialityTypeID = spType.pkSpecialityTypeID where usp.fkUserID = " + userid;
          
          this.LoadFromRawSql(strQuery);
      }
      public void LoadUserSpecialityActive(int userid)
      {
          this.FlushData();
          string strQuery = string.Empty;
          strQuery += " select usp.*,spType.sSpecialityName from tblUserSpeciality usp inner join tblSpecialityType spType ";
          strQuery += " on usp.fkSpecialityTypeID = spType.pkSpecialityTypeID where usp.bIsPrimary = 1 and usp.fkUserID = " + userid;

          this.LoadFromRawSql(strQuery);
      }
       public void LoadUserSpecialityActive_New(int userid)
      {
          this.FlushData();
          string strQuery = string.Empty;
          strQuery += "SELECT  tblSpeciality.pkSpecialityID,tblSpeciality.sSpeciality ";
          strQuery += "   FROM  tblUserSpeciality INNER JOIN ";
          strQuery += "  tblSpecialityType ON tblUserSpeciality.fkSpecialityTypeID = tblSpecialityType.pkSpecialityTypeID INNER JOIN ";
          strQuery += " tblSpeciality ON tblSpecialityType.pkSpecialityTypeID = tblSpeciality.fkSpecialityTypeID  ";   
           strQuery += "  where bIsPrimary = 1 and  fkUserID = " + userid;

          this.LoadFromRawSql(strQuery);
      }
     
      


     

    }
}
