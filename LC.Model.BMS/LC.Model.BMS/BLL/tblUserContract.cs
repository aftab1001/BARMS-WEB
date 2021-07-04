using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblUserContract : LC.Model.BMS.DAL._tblUserContract
    {
        public void GetUserContract(int userid)
        {
            this.Where.FkUserID.Value = userid;
            this.Query.Load();
        }
        public void GetUserContractDetail(int userid)
        {
            string query = string.Empty;
            query = " select distinct u.sLastName,u.sImagePath,u.bActiveBonusDoc, ut.sSpecialityName, ut.pkSpecialityTypeID, ";
            query += " um.sMobilePhone, ue.sEmail, uc.*  from tblusercontract uc inner join ";
            query += " tblusers u on u.pkuserid = uc.fkuserid inner join  ";
            query += " dbo.tblUserEmails ue on ue.fkuserid = u.pkuserid inner join  ";
            query += " dbo.tblUserMobile um on um.fkuserid = u.pkuserid inner join  ";
            query += " dbo.tblUserSpeciality us on us.fkuserid = u.pkuserid inner join  ";
            query += " dbo.tblSpecialityType ut on ut.pkSpecialityTypeID = us.fkSpecialityTypeID  ";
            query += " where u.pkuserid = "+userid;

            this.LoadFromRawSql(query);

        }
        public void GetUserDetail(int userid)
        {
            string query = string.Empty;
            query = " select u.pkuserid,u.slastname,u.sImagePath,ut.pkSpecialityTypeID,ut.sSpecialityName , ";
            query += " um.sMobilePhone, ue.sEmail from dbo.tblusers u inner join ";
            query += " dbo.tblUserEmails ue on ue.fkuserid = u.pkuserid inner join ";
            query += " dbo.tblUserMobile um on um.fkuserid = u.pkuserid inner join ";
            query += " dbo.tblUserSpeciality us on us.fkuserid = u.pkuserid inner join ";
            query += " dbo.tblSpecialityType ut on ut.pkSpecialityTypeID = us.fkSpecialityTypeID ";
            query += " where u.pkuserid =" + userid + " and ue.bIsPrimary = 1 and um.bIsPrimary = 1 and us.bIsPrimary = 1";
            this.LoadFromRawSql(query);

        }
        public double GetUserSeasonSalary(DateTime date, int userid, int departmentid)
        {
            double salary = 0.0;
            date = Convert.ToDateTime(date.Month +"/"+date.Day+"/"+DateTime.Now.Year.ToString());
            tblDepartments departments = new tblDepartments();
            departments.LoadByPrimaryKey(departmentid);
            //if (departments.RowCount > 0)
            //{
                string[] low1 = departments.LowSeason1.Split('-');

                string low_season1_from_date = (low1[0].Split('/'))[0].ToString();
                string low_season1_from_month = (low1[0].Split('/'))[1].ToString();

                string low_season1_till_date = (low1[1].Split('/'))[0].ToString();
                string low_season1_till_month = (low1[1].Split('/'))[1].ToString();

                string[] low2 = departments.LowSeason2.Split('-');

                string low_season2_from_date = (low2[0].Split('/'))[0].ToString();
                string low_season2_from_month = (low2[0].Split('/'))[1].ToString();

                string low_season2_till_date = (low2[1].Split('/'))[0].ToString();
                string low_season2_till_month = (low2[1].Split('/'))[1].ToString();

                string[] high = departments.HighSeason.Split('-');

                string high_season_from_date = (high[0].Split('/'))[0].ToString();
                string high_season_from_month = (high[0].Split('/'))[1].ToString();

                string high_season_till_date = (high[1].Split('/'))[0].ToString();
                string high_season_till_month = (high[1].Split('/'))[1].ToString(); 
            //}
            

            
            DateTime dLowSeason_1_Start = new DateTime();
            DateTime dLowSeason_1_End = new DateTime();
            
            DateTime dLowSeason_2_Start = new DateTime();
            DateTime dLowSeason_2_End = new DateTime();
            
            DateTime dHighSeason_Start = new DateTime();
            DateTime dHighSeason_End = new DateTime();

            this.Where.FkUserID.Value = userid;
            this.Query.Load();

            dLowSeason_1_Start = Convert.ToDateTime(low_season1_from_month + "/" + low_season1_from_date+"/" + DateTime.Now.Year.ToString());
            dLowSeason_1_End = Convert.ToDateTime(low_season1_till_month + "/" + low_season1_till_date + "/" + DateTime.Now.Year.ToString());

            dLowSeason_2_Start = Convert.ToDateTime(low_season2_from_month + "/" + low_season2_from_date+"/" + DateTime.Now.Year.ToString());
            dLowSeason_2_End = Convert.ToDateTime(low_season2_till_month + "/" + low_season2_till_date + "/" + DateTime.Now.Year.ToString());

            dHighSeason_Start = Convert.ToDateTime(high_season_from_month + "/" + high_season_from_date + "/" + DateTime.Now.Year.ToString());
            dHighSeason_End = Convert.ToDateTime(high_season_till_month + "/" + high_season_till_date + "/" + DateTime.Now.Year.ToString());



            //dLowSeason_1_Start = Convert.ToDateTime("04/15/" + DateTime.Now.Year.ToString());
            //dLowSeason_1_End = Convert.ToDateTime("06/15/" + DateTime.Now.Year.ToString());

            //dLowSeason_2_Start = Convert.ToDateTime("08/15/" + DateTime.Now.Year.ToString());
            //dLowSeason_2_End = Convert.ToDateTime("10/15/" + DateTime.Now.Year.ToString());

            //dHighSeason_Start = Convert.ToDateTime("06/16/" + DateTime.Now.Year.ToString());
            //dHighSeason_End = Convert.ToDateTime("08/14/" + DateTime.Now.Year.ToString());

            if (date >= dLowSeason_1_Start && date <= dLowSeason_1_End)
            {
                if (this.RowCount > 0)
                    salary = this.LowSeasonSalary;
            }
            else if (date >= dLowSeason_2_Start && date <= dLowSeason_2_End)
            {
                if (this.RowCount > 0)
                    salary = this.LowSeasonSalary;
            }
            else if (date >= dHighSeason_Start && date <= dHighSeason_End)
            {
                if (this.RowCount > 0)
                    salary = this.HighSeasonSalary;
            }
            if (salary == 0.0)
                salary = this.LowSeasonSalary;
            return salary;
 
        }
        public void GetAgreedContract(int userid)
        {
            string strQuery = string.Empty;
            strQuery = " select * from dbo.tblUserContract where fkUserID = " + userid;
            this.LoadFromRawSql(strQuery);
        }
        
    }
}
