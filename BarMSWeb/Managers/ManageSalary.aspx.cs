using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class Managers_ManageSalary : System.Web.UI.Page
{
    int pkUserID;
    int SalariedUserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            pkUserID = user.UserID;
            if (user.AccessLevel != 2)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }

            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["uid"] != null)
                {
                    SalariedUserID = Convert.ToInt32(Request.QueryString["uid"]);
                }

            }
            else
            {
                Response.Redirect("ManageUsers.aspx");
            }

            if (!IsPostBack)
            {
                LoadUserInfoandBonus();
                tblUserContract contract = new tblUserContract();
                contract.GetUserContract(SalariedUserID);
                if (contract.RowCount > 0)
                {

                    loadContract(contract);
                    ViewState["contractid"] = contract.PkUserContratctID;
                }
                else
                {
                    divScaled.Style.Add("display", "block");
                    divStandard.Style.Add("display", "none");
                    divPercentageSalary.Style.Add("display", "none");
                    lblMessage.Visible = false;
                    EmptyControls();
                    ViewState["contractid"] = null;
                }
            }
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
    }
    private void LoadUserInfoandBonus()
    {
        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(SalariedUserID);
        if (u.RowCount > 0)
        {
            lblUserName.Text = u.SFirstName + " " + u.SLastName;
            chkBonus.Checked = u.BActiveBonusDoc;
        }
    }
    private void loadContract(tblUserContract contract)
    {
        txtStartDate.Text = contract.DEmploymentFromDate.ToString("dd/MM/yyyy");
        txtEndDate.Text = contract.DEmploymentEndDate.ToString("dd/MM/yyyy");
        if (contract.FkSalaryTypeID == 1)
        {
            rdScaled.Checked = true;
            divScaled.Style.Add("display", "block");
            divStandard.Style.Add("display", "none");
            divPercentageSalary.Style.Add("display", "none");
        }
        else if (contract.FkSalaryTypeID == 2)
        {
            rdStandardSalary.Checked = true;
            divStandard.Style.Add("display", "block");
            divScaled.Style.Add("display", "none");
            divPercentageSalary.Style.Add("display", "none");
        }
        else if (contract.FkSalaryTypeID == 3)
        {
            rdPerSalary.Checked = true;
            divPercentageSalary.Style.Add("display", "block");
            divScaled.Style.Add("display", "none");
            divStandard.Style.Add("display", "none");
        }

        if (contract.LowSeasonSalary != 0)
            txtLowSeason.Text = commonMethods.ChangetToUK(contract.LowSeasonSalary.ToString("N"));
        if (contract.HighSeasonSalary != 0)
            txtHighSeason.Text = commonMethods.ChangetToUK(contract.HighSeasonSalary.ToString("N"));
        if (contract.StandardSalary != 0)
            txtStandardSalary.Text = commonMethods.ChangetToUK(contract.StandardSalary.ToString("N"));
        if (contract.FSalaryPercentage != 0)
            txtPercentage.Text = commonMethods.ChangetToUK(contract.FSalaryPercentage.ToString("N"));
        if (contract.MinimumPerday != 0)
            txtMinimumPerDay.Text = commonMethods.ChangetToUK(contract.MinimumPerday.ToString("N"));
        if (contract.PercentageOver != 0)
            txtPercentageOver.Text = commonMethods.ChangetToUK(contract.PercentageOver.ToString("N"));


    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        tblUserContract contract = new tblUserContract();
        if (ViewState["contractid"] != null)
        {
            contract.LoadByPrimaryKey(Convert.ToInt32(ViewState["contractid"]));
            lblMessage.Visible = true;
            lblMessage.Text = "Successfully Updated!";
        }
        else
        {
            contract.AddNew();
            contract.DCreateDate = DateTime.Now;
            lblMessage.Visible = true;
            lblMessage.Text = "Successfully Added!";
        }

        contract.FkUserID = SalariedUserID;

        IFormatProvider providerStart;
        if (Convert.ToInt32(txtStartDate.Text.Trim().Substring(0, 2)) > 12)
            providerStart = new System.Globalization.CultureInfo("en-CA", true);
        else
            providerStart = new System.Globalization.CultureInfo("en-US", true);

        IFormatProvider providerEnd;
        if (Convert.ToInt32(txtEndDate.Text.Trim().Substring(0, 2)) > 12)
            providerEnd = new System.Globalization.CultureInfo("en-CA", true);
        else
            providerEnd = new System.Globalization.CultureInfo("en-US", true);

        string datetimeStart = txtStartDate.Text.Trim();
        String datetimeEnd = txtEndDate.Text.Trim();

        DateTime dtStart = DateTime.Parse(datetimeStart, providerStart, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        DateTime dtEnd = DateTime.Parse(datetimeEnd, providerEnd, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

        contract.DEmploymentFromDate = dtStart;//Convert.ToDateTime(Convert.ToDateTime(txtStartDate.Text).ToString("MM/dd/yy"));
        contract.DEmploymentEndDate = dtEnd;// Convert.ToDateTime(Convert.ToDateTime(txtEndDate.Text).ToString("MM/dd/yy"));
        if (rdScaled.Checked)
            contract.FkSalaryTypeID = 1;
        else if (rdStandardSalary.Checked)
            contract.FkSalaryTypeID = 2;
        else if (rdPerSalary.Checked)
            contract.FkSalaryTypeID = 3;
        if (txtLowSeason.Text != "")
            contract.LowSeasonSalary = Convert.ToDouble(commonMethods.ChangeToUS(txtLowSeason.Text.Replace(" €", "")).ToString("N"));
        else
            contract.LowSeasonSalary = 0;
        if (txtHighSeason.Text != "")
            contract.HighSeasonSalary = Convert.ToDouble(commonMethods.ChangeToUS(txtHighSeason.Text.Replace(" €", "")).ToString("N"));
        else
            contract.HighSeasonSalary = 0;
        if (txtStandardSalary.Text != "")
            contract.StandardSalary = Convert.ToDouble(commonMethods.ChangeToUS(txtStandardSalary.Text.Replace(" €", "")).ToString("N"));
        else
            contract.StandardSalary = 0;

        if (txtPercentage.Text != "")
            contract.FSalaryPercentage = Convert.ToDouble(commonMethods.ChangeToUS(txtPercentage.Text.Replace(" %", "")).ToString("N"));
        else
            contract.FSalaryPercentage = 0;
        if (txtMinimumPerDay.Text != "")
            contract.MinimumPerday = Convert.ToDouble(commonMethods.ChangeToUS(txtMinimumPerDay.Text.Replace(" €", "")).ToString("N"));
        else
            contract.MinimumPerday = 0;
        if (txtPercentageOver.Text != "")
            contract.PercentageOver = Convert.ToDouble(commonMethods.ChangeToUS(txtPercentageOver.Text.Replace(" €", "")).ToString("N"));
        else
            contract.PercentageOver = 0;
        contract.BContractAgreed = false;

        contract.DModifiedDate = DateTime.Now;
        contract.Save();
        ViewState["contractid"] = contract.PkUserContratctID;
        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(SalariedUserID);
        if (u.RowCount > 0)
        {
            u.BActiveBonusDoc = chkBonus.Checked;
            u.DModifiedDate = DateTime.Now;
            u.Save();
        }
        loadContract(contract);
    }
    private void EmptyControls()
    {
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        rdScaled.Checked = true;
        rdStandardSalary.Checked = false;
        rdPerSalary.Checked = false;
        txtLowSeason.Text = "";
        txtHighSeason.Text = "";
        txtStandardSalary.Text = "";
        txtPercentage.Text = "";
        txtMinimumPerDay.Text = "";
        txtPercentageOver.Text = "";
    }

    //protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    tblUserContract contract = new tblUserContract();
    //    contract.GetUserContract(Convert.ToInt32(ddlUsers.SelectedValue));
    //    if (contract.RowCount > 0)
    //    {
    //        loadContract(contract);
    //        ViewState["contractid"] = contract.PkUserContratctID;
    //    }
    //    else
    //    {
    //        divScaled.Style.Add("display", "block");
    //        divStandard.Style.Add("display", "none");
    //        divPercentageSalary.Style.Add("display", "none");
    //        lblMessage.Visible = false;
    //        chkBonus.Checked = false;
    //        EmptyControls();
    //        ViewState["contractid"] = null;
    //    }
    //}
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageUsers.aspx");
    }
}
