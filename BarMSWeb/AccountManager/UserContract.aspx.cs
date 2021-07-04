using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
using System.IO;


public partial class AccountManager_UserContract : System.Web.UI.Page
{
    int UserID;
    int pkContractID;
    int departmentid;
    static string userName = "";
    static string userLastName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            departmentid = user.DepartmentID;
            if (user.AccessLevel != 4)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }
            UserID = user.UserID;
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        tblUserContract contract = new tblUserContract();
        contract.GetUserContract(UserID);
        if (contract.RowCount > 0)
            pkContractID = contract.PkUserContratctID;


        if (!Page.IsPostBack)
        {
            tblDepartments departments = new tblDepartments();
            departments.LoadByPrimaryKey(departmentid);
            if (departments.RowCount > 0)
            {
                lblHighSeason.Text = departments.HighSeasonDef;
                lblLowSeason.Text = departments.LowSeasonDef;
            }
            
            loadContract();
        }

    }
    private void loadContract()
    {

        LoadUserProfileInfo();

        tblUserContract contract = new tblUserContract();
        contract.GetUserContractDetail(UserID);
        bool active = false;
        if (contract.RowCount > 0)
        {
            pkContractID = Convert.ToInt32(contract.GetColumn("pkUserContratctID").ToString());
            txtStartDate.Text = Convert.ToDateTime(contract.GetColumn("DEmploymentFromDate")).ToString("dd/MM/yyyy");
            txtEndDate.Text = Convert.ToDateTime(contract.GetColumn("DEmploymentEndDate")).ToString("dd/MM/yyyy");

            if (contract.GetColumn("FkSalaryTypeID").ToString() == "1")
            {
                trScaled.Visible = true;
                rdScaled.Checked = true;
                
                if (contract.GetColumn("LowSeasonSalary").ToString() != "0")
                    txtLowSeason.Text = contract.GetColumn("LowSeasonSalary").ToString() + " €";
                if (contract.GetColumn("HighSeasonSalary").ToString() != "0")
                    txtHighSeason.Text = contract.GetColumn("HighSeasonSalary").ToString() + " €";
            }
            else if (contract.GetColumn("FkSalaryTypeID").ToString() == "2")
            {
                trStandard.Visible = true;
                rdStandard.Checked = true;
                tblSalary.Width = "40%";
                if (contract.GetColumn("StandardSalary").ToString() != "0")
                    txtStandard.Text = contract.GetColumn("StandardSalary").ToString() + " €";
            }
            else if (contract.GetColumn("FkSalaryTypeID").ToString() == "3")
            {
                trPercentage.Visible = true;
                rdPercentage.Checked = true;
                if (contract.GetColumn("FSalaryPercentage").ToString() != "0")
                    txtPercentageSalary.Text = contract.GetColumn("FSalaryPercentage").ToString() + " %";
                if (contract.GetColumn("MinimumPerday").ToString() != "0")
                    txtMinimumPerDay.Text = contract.GetColumn("MinimumPerday").ToString() + " €";
                if (contract.GetColumn("PercentageOver").ToString() != "0")
                    txtPerOver.Text = contract.GetColumn("PercentageOver").ToString() + " €";
            }

            //imgUser.Src = contract.GetColumn("sImagePath").ToString();
            //lblName.Text = contract.GetColumn("SLastName").ToString();
            //lblSpeciality.Text = contract.GetColumn("sSpecialityName").ToString();
            //lblPhone.Text = contract.GetColumn("sMobilePhone").ToString();
            //lblEmail.Text = contract.GetColumn("sEmail").ToString();
            if (Convert.ToBoolean(contract.GetColumn("bActiveBonusDoc")))
            {
                active = true;
            }
            if (contract.GetColumn("bContractAgreed").ToString() == "True")
            {
                chkAgreement.Enabled = false;
                chkAgreement.Checked = true;
                lblCheckAgreement.Text = userName + " " + userLastName + ", digitally signed this contract at " + Convert.ToDateTime(contract.GetColumn("DModifiedDate")).DayOfWeek + ", " + Convert.ToDateTime(contract.GetColumn("DModifiedDate")).ToString("dd/MM/yyy HH:mm:ss tt");
            }
            else
            {
                chkAgreement.Enabled = true;
                lblCheckAgreement.Text = "I understand the above contract and I agree with all the referenced conditions.";
            }

        }
        else
        {
            chkAgreement.Enabled = false;
            //contract.FlushData();
            //contract.GetUserDetail(UserID);
            //if (contract.RowCount > 0)
            //{
            //    imgUser.Src = contract.GetColumn("sImagePath").ToString();
            //    lblName.Text = contract.GetColumn("SLastName").ToString();
            //    lblSpeciality.Text = contract.GetColumn("sSpecialityName").ToString();
            //    lblPhone.Text = contract.GetColumn("sMobilePhone").ToString();
            //    lblEmail.Text = contract.GetColumn("sEmail").ToString();
            //}
        }

        

       
        tblSettings setting = new tblSettings();
        setting.LoadAll();
        if (setting.RowCount > 0)
        {
            divContractDoc.InnerHtml = setting.SContract;
        }
    }
    private void LoadUserProfileInfo()
    {
        tblUserEmails useremails = new tblUserEmails();
        useremails.LoadUserEmailsActive(UserID);
        if (useremails.RowCount > 0)
        {
            lblEmail.Text = useremails.SEmail;
            userName = useremails.SEmail;
            string email = lblEmail.Text;
            if (lblEmail.Text.Length > 12)
            {
                lblEmail.Text = lblEmail.Text.Substring(0, 12) + "..";
                lblEmail.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + email + "')");
                lblEmail.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
        }

        tblUserMobile userMobiles = new tblUserMobile();
        userMobiles.LoadUserMobilesActive(UserID);
        if (userMobiles.RowCount > 0)
            lblPhone.Text = userMobiles.SMobilePhone.ToString();

        tblUserSpeciality userSpeciality = new tblUserSpeciality();
        userSpeciality.LoadUserSpecialityActive(UserID);
        if (userSpeciality.RowCount > 0)
        {
            lblSpeciality.Text = userSpeciality.GetColumn("sSpecialityName").ToString();
            string specialty = lblSpeciality.Text;
            if (lblSpeciality.Text.Length > 12)
            {
                lblSpeciality.Text = lblSpeciality.Text.Substring(0, 12) + "..";
                lblSpeciality.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + specialty + "')");
                lblSpeciality.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
        }

        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(UserID);
        if (u.RowCount > 0)
        {
            string name = u.SFirstName + " " + u.SLastName;
            lblName.Text = u.SFirstName + " " + u.SLastName;
            userLastName = name;
            if (lblName.Text.Length > 12)
            {
                lblName.Text = lblName.Text.Substring(0, 12) + "..";
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            imgUser.Src = u.SImagePath;

            if (u.BActiveBonusDoc && u.BBonusApprovedByDepartment)
            {
                tblUserBonuses bonuses = new tblUserBonuses();
                bonuses.GetUserBonus(u.PkUserID);
                if (bonuses.RowCount > 0)
                {
                    DataView dd = bonuses.DefaultView;
                    divBonusDoc.InnerHtml = " <h2>Bonus:</h2>" + bonuses.Bonus;
                }
            }
        }
    }
    protected void chkAgreement_CheckedChanged(object sender, EventArgs e)
    {
        chkAgreement.Enabled = false;
        tblUserContract contract = new tblUserContract();
        contract.LoadByPrimaryKey(pkContractID);
        if (contract.RowCount > 0)
        {
            contract.BContractAgreed = true;
            contract.DModifiedDate = DateTime.Now;
            contract.Save();
            lblCheckAgreement.Text = userName + " " + userLastName + ", digitally signed this contract at " + contract.DModifiedDate.DayOfWeek + ", " + contract.DModifiedDate.ToString("dd/MM/yyy HH:mm:ss tt");
        }
    }
}
