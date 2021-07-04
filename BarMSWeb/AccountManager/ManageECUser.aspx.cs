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

public partial class AccountManager_ManageECUser : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    static int BonusUserID = 0;
    static int ToUserID = 0;
    static string filterMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
            if (user.AccessLevel != 4)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }
            //UserID = user.UserID;
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }

        if (!Page.IsPostBack)
        {
            BindDropDownFilter();
            BindDropDownSearch();
            BindGridUsers();
        }
    }

    private void BindDropDownFilter()
    {
        imgBtnClearFilter.Visible = false;
        ddlCountry.Items.Clear();
        ddlSpeciality.Items.Clear();
        ddlYears.Items.Clear();

        tblCountries country = new tblCountries();
        country.GetAllCountriesAlphabetically();
        commonMethods.FillDropDownList(ddlCountry, country.DefaultView, "sCountry", "pkCountryID");
        ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));


        tblSpecialityType position = new tblSpecialityType();
        position.GetSpecialityTypesWithoutSeparator();
        commonMethods.FillDropDownList(ddlSpeciality, position.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
        ddlSpeciality.Items.Insert(0, new ListItem("--Select Speciality--", "0"));

        for (int i = 0; i <= 74; i++)
        {
            ddlYears.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
        }
        ddlYears.Items.Insert(0, new ListItem("--Select Year--", "0"));




    }
    private void BindDropDownSearch()
    {
        ddlSearch.Items.Clear();
        ddlSearch.Items.Add(new ListItem("--Select Search Item--", "0"));
        ddlSearch.Items.Add(new ListItem("First Name", "1"));
        ddlSearch.Items.Add(new ListItem("Last Name", "2"));
        ddlSearch.Items.Add(new ListItem("Country", "3"));
        
        ddlSearch.Items.Add(new ListItem("Gender", "5"));
        txtSearch.Text = "";
    }
    private void BindGridUsers()
    {
        tblUsers user = new tblUsers();
        user.LoadUsersActivatedByHim(DepartmentID, UserID,3);
        grdUsers.DataSource = user.DefaultView;
        grdUsers.DataBind();
        upnlRefreshUsers.Update();
        if (user.RowCount > 0)
        {
            if (rdoInActive.Checked)
            {
                filterMessage = "Now showing only <span style='text-decoration:underline;font-weight:bold;'>Inactive</span> users";
            }
            else if (rdoActive.Checked)
            {
                filterMessage = "Now showing only <span style='text-decoration:underline;font-weight:bold;'>Active</span> users";
            }
            else
            {
                filterMessage = "Now showing all users";
            }
            divFilter.InnerHtml = filterMessage;
            //lblFilterMessage.Text = "Now showing all users";
        }
        else
        {
            divFilter.InnerHtml = "";
        }
    }
    private void BindGridActiveUsers()
    {
        tblUsers user = new tblUsers();
        //user.LoadUsers(active , DepartmentID, UserID);
        user.LoadUsersActivatedByManager(DepartmentID, UserID,3);
        grdUsers.DataSource = user.DefaultView;
        grdUsers.DataBind();
        if (user.RowCount > 0)
        {
            if (rdoInActive.Checked)
            {
                filterMessage = "Now showing only <span style='text-decoration:underline;font-weight:bold;'>Inactive</span> users";
            }
            else if (rdoActive.Checked)
            {
                filterMessage = "Now showing only <span style='text-decoration:underline;font-weight:bold;'>Active</span> users";
            }
            else
            {
                filterMessage = "Now showing all users";
            }
            divFilter.InnerHtml = filterMessage;
            //lblFilterMessage.Text = "Now showing all users";
        }
        else
        {
            divFilter.InnerHtml = "";
        }

    }
    private void BindGridInActiveUsers()
    {
        tblUsers user = new tblUsers();
        //user.LoadUsers(active , DepartmentID, UserID);
        user.LoadUsersDeActivatedByManager(DepartmentID, UserID,3);
        grdUsers.DataSource = user.DefaultView;
        grdUsers.DataBind();
        if (user.RowCount > 0)
        {
            if (rdoInActive.Checked)
            {
                filterMessage = "Now showing only <span style='text-decoration:underline;font-weight:bold;'>Inactive</span> users";
            }
            else if (rdoActive.Checked)
            {
                filterMessage = "Now showing only <span style='text-decoration:underline;font-weight:bold;'>Active</span> users";
            }
            else
            {
                filterMessage = "Now showing all users";
            }
            divFilter.InnerHtml = filterMessage;
            //lblFilterMessage.Text = "Now showing all users";
        }
        else
        {
            divFilter.InnerHtml = "";
        }

    }
    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                //ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                //lblemail.Text = drv["sEmail"].ToString();

                HtmlAnchor ankMsn = e.Row.FindControl("ankMsn") as HtmlAnchor;
                HtmlAnchor ankSkype = e.Row.FindControl("ankSkype") as HtmlAnchor;
                ImageButton imgBtnBonus = e.Row.FindControl("imgBtnBonus") as ImageButton;
                ImageButton imgBtnSalary = e.Row.FindControl("imgBtnSalary") as ImageButton;
                ImageButton imgBtnContract = e.Row.FindControl("imgBtnContract") as ImageButton;


                tblUserContract contract = new tblUserContract();
                contract.GetAgreedContract(Convert.ToInt32(drv["pkUserID"].ToString()));
                if (contract.RowCount > 0)
                {
                    string contractMessage = string.Empty;
                    if (contract.BContractAgreed)
                    {
                        contractMessage = "Contract signed";
                        imgBtnContract.ImageUrl = "~/Images/icon_c_green.png";
                        imgBtnContract.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + contractMessage + "')");
                        imgBtnContract.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                    }
                    else if (!contract.BContractAgreed)
                    {
                        contractMessage = "Contract NOT signed";
                        imgBtnContract.ImageUrl = "~/Images/icon_c_red.png";
                        imgBtnContract.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + contractMessage + "')");
                        imgBtnContract.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                    }
                }
                else
                {
                    string contractMessage = string.Empty;
                    contractMessage = "Contract NOT signed";
                    imgBtnContract.ImageUrl = "~/Images/icon_c_red.png";
                    imgBtnContract.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + contractMessage + "')");
                    imgBtnContract.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                string bonusMessage = "Suggest Bonus";

                string salaryMessage = "Edit salary";
                imgBtnSalary.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + salaryMessage + "')");
                imgBtnSalary.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");

                if (!Convert.ToBoolean(drv["bActiveBonusDoc"]) && !Convert.ToBoolean(drv["bBonusApprovedByDepartment"]))
                {
                    imgBtnBonus.ImageUrl = "~/Images/icon_b_greypng.png";
                    imgBtnBonus.Enabled = true;

                    imgBtnBonus.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + bonusMessage + "')");
                    imgBtnBonus.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                else if (Convert.ToBoolean(drv["bActiveBonusDoc"]) && !Convert.ToBoolean(drv["bBonusApprovedByDepartment"]))
                {
                    imgBtnBonus.ImageUrl = "~/Images/icon_b_green.png";
                    imgBtnBonus.Enabled = true;

                    imgBtnBonus.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + bonusMessage + "')");
                    imgBtnBonus.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                else if (Convert.ToBoolean(drv["bActiveBonusDoc"]) && Convert.ToBoolean(drv["bBonusApprovedByDepartment"]))
                {
                    imgBtnBonus.ImageUrl = "~/Images/icon_b_golden.png";
                    imgBtnBonus.Enabled = false;
                }

                if (drv["sMessengerProfile"].ToString() != "")
                {
                    ankMsn.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + drv["sMessengerProfile"].ToString() + "')");
                    ankMsn.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                if (drv["sSkypeProfile"].ToString() != "")
                {
                    ankSkype.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + drv["sSkypeProfile"].ToString() + "')");
                    ankSkype.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                if (drv["sFaceBookProfile"].ToString() != "")
                {
                    HtmlAnchor ankFacebook = e.Row.FindControl("ankFacebook") as HtmlAnchor;
                    ankFacebook.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + drv["sFaceBookProfile"].ToString() + "')");
                    ankFacebook.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                if (drv["sTwitterProfile"].ToString() != "")
                {
                    HtmlAnchor ankTwitter = e.Row.FindControl("ankTwitter") as HtmlAnchor;
                    ankTwitter.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + drv["sTwitterProfile"].ToString() + "')");
                    ankTwitter.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../AccountManager/EditUser.aspx?id=" + drv["pkUserID"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 28)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 28) + "...";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                ImageButton imgActive = (ImageButton)e.Row.FindControl("imgActive");
                if (drv["bActiveByAdmin"].ToString() == "False")
                {
                    imgActive.ImageUrl = "../images/close.png";
                    imgActive.ToolTip = "Activate";
                }
                else
                {
                    imgActive.ImageUrl = "../images/activate_icon.gif";
                    imgActive.ToolTip = "De-Activate";
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {


            case "Active":
                int userid = Convert.ToInt32(e.CommandArgument);
                tblUsers user = new tblUsers();
                user.LoadByPrimaryKey(userid);
                if (user.BActiveByAdmin == false)
                {
                    user.BActiveByAdmin = true;
                    user.FkActivatedByAdminID = UserID;
                }
                else
                {
                    user.BActiveByAdmin = false;
                    user.FkActivatedByAdminID = UserID;
                }
                user.DModifiedDate = DateTime.Now;
                user.Save();
                if (rdoInActive.Checked)
                    BindGridInActiveUsers();
                else if (rdoActive.Checked)
                    BindGridActiveUsers();
                else
                    BindGridUsers();

                break;
            case "Message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();
                break;
            case "Salary":
                Response.Redirect("ManageSalary.aspx?uid=" + Convert.ToInt32(e.CommandArgument).ToString());
                break;
            case "Bonus":
                int uidBonus = Convert.ToInt32(e.CommandArgument);
                BonusUserID = uidBonus;
                tblUsers userBonus = new tblUsers();
                userBonus.LoadByPrimaryKey(uidBonus);
                txtBonusAmount.Text = "";
                txtNoteBonus.Text = "";
                tblUserContract contract = new tblUserContract();
                contract.GetUserContract(uidBonus);
                tblUserWorkshifts Workshifts = new tblUserWorkshifts();
                Workshifts.getSalaryandBonus(uidBonus);
                if (Workshifts.RowCount > 0)
                {

                    lblLastBouns.Text = commonMethods.ChangetToUK(Workshifts.GetColumn("Bonus").ToString());
                }
                else
                {

                    lblLastBouns.Text = "00.00";
                }
                if (userBonus.RowCount > 0)
                {
                    if (contract.RowCount > 0)
                    {
                        trSalaryType.Visible = true;
                        trSalary.Visible = true;
                        lblMessagepopup.Visible = false;
                        loadContract(contract);

                    }
                    else
                    {
                        lblMessagepopup.Visible = true;
                        lblMessagepopup.Text = "Not Salary ";
                        trSalaryType.Visible = false;
                        trSalary.Visible = false;
                        // divScaled.Style.Add("display", "block");
                        //divStandard.Style.Add("display", "none");
                        //divPercentageSalary.Style.Add("display", "none");
                        EmptyControls();
                    }

                    if (userBonus.BActiveBonusDoc)
                    {
                        chkBonus.Checked = true;
                        if (userBonus.GetColumn("FBonusAmount").ToString() != null && userBonus.GetColumn("FBonusAmount").ToString() != "")
                        {
                            txtBonusAmount.Text = commonMethods.ChangetToUK(userBonus.FBonusAmount.ToString()).ToString();
                        }
                        else
                        {
                            txtBonusAmount.Text = "";
                        }

                        txtNoteBonus.Text = userBonus.SBonusNote;
                    }
                    else
                    {
                        chkBonus.Checked = false;
                    }


                    if (userBonus.BActiveBonusDoc && userBonus.BBonusApprovedByDepartment)
                    {
                        return;
                    }
                    else
                        ModalPopupExtender2.Show();
                }
                break;
        }

    }
    private void EmptyControls()
    {
        //  txtStartDate.Text = "";
        // txtEndDate.Text = "";
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
    private void Readonly()
    {
        //rdScaled.Checked = true;
        // rdStandardSalary.Checked = false;
        // rdPerSalary.Checked = false;
        txtLowSeason.ReadOnly = true;
        txtHighSeason.ReadOnly = true;
        txtStandardSalary.ReadOnly = true;
        txtPercentage.ReadOnly = true;
        txtMinimumPerDay.ReadOnly = true;
        txtPercentageOver.ReadOnly = true;
    }
    private void loadContract(tblUserContract contract)
    {
        trSalaryType.Visible = false;
        Readonly();
        // txtStartDate.Text = contract.DEmploymentFromDate.ToString("dd/MM/yyyy");
        // txtEndDate.Text = contract.DEmploymentEndDate.ToString("dd/MM/yyyy");
        if (contract.FkSalaryTypeID == 1)
        {
            rdScaled.Checked = true;
            rdStandardSalary.Visible = false;
            divScaled.Style.Add("display", "block");
            divStandard.Style.Add("display", "none");
            divPercentageSalary.Style.Add("display", "none");
        }
        else if (contract.FkSalaryTypeID == 2)
        {
            rdStandardSalary.Checked = true;
            rdPerSalary.Visible = false;
            rdScaled.Visible = false;
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
    protected void rdoActive_CheckedChanged(object sender, EventArgs e)
    {
        imgBtnClearFilter.Visible = false;
        BindGridActiveUsers();
        BindDropDownFilter();
        BindDropDownSearch();
    }
    protected void rdoInActive_CheckedChanged(object sender, EventArgs e)
    {
        imgBtnClearFilter.Visible = false;
        BindGridInActiveUsers();
        BindDropDownFilter();
        BindDropDownSearch();
    }
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        imgBtnClearFilter.Visible = false;
        BindGridUsers();
        BindDropDownFilter();
        BindDropDownSearch();
    }
    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblUserInBox userIn = new tblUserInBox();
            userIn.AddNew();
            userIn.FkFromUserID = UserID;
            userIn.FkToUserID = ToUserID;
            userIn.SSubject = txtSubject.Text;
            userIn.SMessage = txtMessage.Text;
            userIn.DReceivedDate = DateTime.Now;
            userIn.BIsread = false;
            userIn.Save();

            tblUserSentBox userOut = new tblUserSentBox();
            userOut.AddNew();
            userOut.FkFromUserID = UserID;
            userOut.FkToUserID = ToUserID;
            userOut.SSubject = txtSubject.Text;
            userOut.SMessage = txtMessage.Text;
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            ModalPopupExtender1.Hide();
            if (rdoInActive.Checked)
                BindGridInActiveUsers();
            else if (rdoActive.Checked)
                BindGridActiveUsers();
            else
                BindGridUsers();
        }
        catch (Exception ex)
        {

        }


    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblUsers user = new tblUsers();
        if (ddlYears.SelectedValue != "0")
        {
            user.FlushData();
            user.LoadUsersByStartYear(DepartmentID, UserID, ddlYears.SelectedItem.Text);
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
            lnkViewAll.Visible = true;
        }
        else
        {
            user.FlushData();
            user.LoadUsers(3, DepartmentID, UserID);
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
        }
    }
    protected void ddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblUsers user = new tblUsers();
        if (ddlSpeciality.SelectedValue != "0")
        {
            user.FlushData();
            user.LoadUsersBySpeciality(DepartmentID, UserID, ddlSpeciality.SelectedItem.Text);
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
            lnkViewAll.Visible = true;
        }
        else
        {
            user.FlushData();
            user.LoadUsers(3, DepartmentID, UserID);
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
        }

    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblUsers user = new tblUsers();
        if (ddlCountry.SelectedValue != "0")
        {
            user.FlushData();
            user.LoadUsersByCountry(DepartmentID, UserID, Convert.ToInt32(ddlCountry.SelectedValue));
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
            lnkViewAll.Visible = true;
        }
        else
        {
            user.FlushData();
            user.LoadUsers(3, DepartmentID, UserID);
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
        }

    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindDropDownFilter();
        tblUsers user = new tblUsers();
        if (ddlSearch.SelectedValue == "4")
        {
            user.FlushData();
            user.LoadUsersBySearchField(DepartmentID, UserID, txtSearch.Text);
            //grdUsers.DataSource = user.DefaultView;
            //grdUsers.DataBind();
            //if (user.RowCount > 0)
            //    divFilter.InnerHtml = "Now showing search item(s)";
            lnkViewAll.Visible = true;
        }
        else if (ddlSearch.SelectedValue == "3")
        {
            user.FlushData();

            tblCountries countries = new tblCountries();
            countries.LoadByName(txtSearch.Text);
            if (countries.RowCount > 0)
            {
                user.FlushData();
                user.LoadUsersByCountry(DepartmentID, UserID, countries.PkCountryID);

            }
            //grdUsers.DataSource = user.DefaultView;
            //grdUsers.DataBind();
            //if (user.RowCount > 0)
            //    divFilter.InnerHtml = "Now showing search item(s)";
            lnkViewAll.Visible = true;
        }
        else if (ddlSearch.SelectedValue == "2")
        {
            user.FlushData();
            string condition = "and u.sLastName= '" + txtSearch.Text + "' ";
            user.LoadUsersByGeneralSearchField(DepartmentID, UserID, condition);
            //grdUsers.DataSource = user.DefaultView;
            //grdUsers.DataBind();
            //if (user.RowCount > 0)
            //    divFilter.InnerHtml = "Now showing search item(s)";
            lnkViewAll.Visible = true;
        }
        else if (ddlSearch.SelectedValue == "1")
        {
            user.FlushData();
            string condition = "and u.sFirstName= '" + txtSearch.Text + "' ";
            user.LoadUsersByGeneralSearchField(DepartmentID, UserID, condition);
            //grdUsers.DataSource = user.DefaultView;
            //grdUsers.DataBind();
            //if (user.RowCount > 0)
            //    divFilter.InnerHtml = "Now showing search item(s)";
            lnkViewAll.Visible = true;
        }
        else if (ddlSearch.SelectedValue == "5")
        {
            user.FlushData();
            int gender = 0;
            if (txtSearch.Text.ToLower() == "man" || txtSearch.Text.ToLower() == "male" || txtSearch.Text.ToLower() == "men")
                gender = 1;
            else if (txtSearch.Text.ToLower() == "woman" || txtSearch.Text.ToLower() == "female" || txtSearch.Text.ToLower() == "women")
                gender = 2;
            if (gender != 0)
            {
                string condition = "and u.iGender = " + gender + " ";
                user.LoadUsersByGeneralSearchField(DepartmentID, UserID, condition,3);
            }
            //grdUsers.DataSource = user.DefaultView;
            //grdUsers.DataBind();

            lnkViewAll.Visible = true;
        }
        grdUsers.DataSource = user.DefaultView;
        grdUsers.DataBind();
        if (user.RowCount > 0)
            divFilter.InnerHtml = "Now showing search item(s)";
        else
            divFilter.InnerHtml = "";
    }

    protected void lnkViewAll_Click(object sender, EventArgs e)
    {
        lnkViewAll.Visible = false;
        Response.Redirect("Manageusers.aspx");
    }

    protected void imgBtnFilterSpecialty_Click(object sender, ImageClickEventArgs e)
    {
        BindDropDownSearch();
        tblUsers user = new tblUsers();
        string filterYear = string.Empty;
        string filterSpecialty = string.Empty;
        string filterCountry = string.Empty;



        bool check = false;

        if (ddlYears.SelectedValue != "0")
        {
            filterYear = "(Year(uc.dEmploymentFromDate) = " + ddlYears.SelectedItem.Text + " or Year(uc.dEmploymentEndDate) = " + ddlYears.SelectedItem.Text + ") and";
            //lblFilterMessage.Text = "Now showing only users whose employement date starts or ends in " + ddlYears.SelectedItem;
            check = true;
            imgBtnClearFilter.Visible = true;
        }
        if (ddlSpeciality.SelectedValue != "0")
        {
            filterSpecialty = "(us.fkSpecialityTypeID = " + ddlSpeciality.SelectedValue + " ) and";
            check = true;
            imgBtnClearFilter.Visible = true;
        }
        if (ddlCountry.SelectedValue != "0")
        {
            filterCountry = "(u.fkNationalityCountry = " + ddlCountry.SelectedValue + ") and";
            check = true;
            imgBtnClearFilter.Visible = true;
        }

        if (check)
        {
            string query = string.Empty;
            string queryStart = " and (";
            string queryEnd = ")";
            string active_nonactive = string.Empty;
            if (rdoActive.Checked)
            {
                active_nonactive = "and u.bactivebyuser = 1 and u.bactivebyadmin= 1";
                query = active_nonactive;
            }
            else if (rdoInActive.Checked)
            {
                active_nonactive = "and u.bactivebyuser = 1 and u.bactivebyadmin= 0";
                query = active_nonactive;
            }
            else
            {
                query = "";
            }
            query += queryStart;

            if (ddlYears.SelectedValue != "0")
            {
                query += filterYear;

                filterMessage = "";
            }
            if (ddlSpeciality.SelectedValue != "0")
            {
                query += filterSpecialty;
                //filterMessage = "Now Showing Users with <span style='text-decoration:underline;font-weight:bold;'>Specialty " + ddlSpeciality.SelectedItem.Text + "</span>";
            }
            if (ddlCountry.SelectedValue != "0")
            {
                query += filterCountry;
            }


            CreateFilterMessage();
            divFilter.InnerHtml = filterMessage;
            query = query.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            query += queryEnd;
            user.FlushData();
            user.LoadFilteredUsers(DepartmentID, UserID, query,3);
            grdUsers.DataSource = user.DefaultView;
            grdUsers.DataBind();
            if (user.RowCount <= 0)
            {
                divFilter.InnerHtml = "";
            }
        }
        else
        {
            if (rdoInActive.Checked)
                BindGridInActiveUsers();
            else if (rdoActive.Checked)
                BindGridActiveUsers();
            else
                BindGridUsers();
            //user.FlushData();
            //user.LoadUsers(3, DepartmentID, UserID);
            //grdUsers.DataSource = user.DefaultView;
            //grdUsers.DataBind();

        }
    }
    private void CreateFilterMessage()
    {

        if (rdoInActive.Checked)
        {
            filterMessage = "Now showing <span style='text-decoration:underline;font-weight:bold;'>Inactive</span> users";
        }
        else if (rdoActive.Checked)
        {
            filterMessage = "Now showing <span style='text-decoration:underline;font-weight:bold;'>Active</span> users";
        }
        else
        {
            filterMessage = "Now showing all users";
        }

        if (ddlYears.SelectedValue != "0" && ddlSpeciality.SelectedValue == "0" && ddlCountry.SelectedValue == "0")
        {
            filterMessage += " of <span style='text-decoration:underline;font-weight:bold;'>" + ddlYears.SelectedItem.Text + "</span>";
        }
        else if (ddlYears.SelectedValue == "0" && ddlSpeciality.SelectedValue != "0" && ddlCountry.SelectedValue == "0")
        {
            filterMessage += " with <span style='text-decoration:underline;font-weight:bold;'> specialty " + ddlSpeciality.SelectedItem.Text + "</span>";
        }
        else if (ddlYears.SelectedValue == "0" && ddlSpeciality.SelectedValue == "0" && ddlCountry.SelectedValue != "0")
        {
            filterMessage += " from <span style='text-decoration:underline;font-weight:bold;'> " + ddlCountry.SelectedItem.Text + "</span>";
        }
        else if (ddlYears.SelectedValue != "0" && ddlSpeciality.SelectedValue != "0" && ddlCountry.SelectedValue == "0")
        {
            filterMessage += " with <span style='text-decoration:underline;font-weight:bold;'> specialty " + ddlSpeciality.SelectedItem.Text + "</span>,";
            filterMessage += " in <span style='text-decoration:underline;font-weight:bold;'>" + ddlYears.SelectedItem.Text + "</span>";
        }
        else if (ddlYears.SelectedValue != "0" && ddlCountry.SelectedValue != "0" && ddlSpeciality.SelectedValue == "0")
        {
            filterMessage += " from <span style='text-decoration:underline;font-weight:bold;'> " + ddlCountry.SelectedItem.Text + "</span>";
            filterMessage += " in <span style='text-decoration:underline;font-weight:bold;'>" + ddlYears.SelectedItem.Text + "</span>";
        }
        else if (ddlSpeciality.SelectedValue != "0" && ddlCountry.SelectedValue != "0" && ddlYears.SelectedValue == "0")
        {
            filterMessage += " from <span style='text-decoration:underline;font-weight:bold;'> " + ddlCountry.SelectedItem.Text + "</span>";
            filterMessage += " with <span style='text-decoration:underline;font-weight:bold;'> specialty " + ddlSpeciality.SelectedItem.Text + "</span>";
        }
        else if (ddlYears.SelectedValue != "0" && ddlSpeciality.SelectedValue != "0" && ddlCountry.SelectedValue != "0")
        {
            filterMessage += " from <span style='text-decoration:underline;font-weight:bold;'> " + ddlCountry.SelectedItem.Text + "</span>";
            filterMessage += " with <span style='text-decoration:underline;font-weight:bold;'> specialty " + ddlSpeciality.SelectedItem.Text + "</span>,";
            filterMessage += " in <span style='text-decoration:underline;font-weight:bold;'>" + ddlYears.SelectedItem.Text + "</span>";
        }
    }
    protected void imgBtnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnClearFilter.Visible = false;
        Response.Redirect("ManageECUser.aspx");
    }
    protected void imgBtnActiveBonus_Click(object sender, ImageClickEventArgs e)
    {
        divActiveDeactiveBonusMessage.Visible = false;
        tblUsers users = new tblUsers();
        users.LoadByPrimaryKey(BonusUserID);
        if (users.RowCount > 0)
        {
            if ((!users.BActiveBonusDoc && chkBonus.Checked)
                || (users.BActiveBonusDoc && !chkBonus.Checked))
            {
                users.SBonusNote = txtNoteBonus.Text;
                users.FBonusAmount = commonMethods.ChangeToUS(txtBonusAmount.Text);
                users.BActiveBonusDoc = chkBonus.Checked;
                users.DModifiedDate = DateTime.Now;
                users.Save();
                string name = users.SFirstName + " " + users.SLastName;

                users.FlushData();
                users.GetDepartmentAdminID(DepartmentID);
                if (users.RowCount > 0)
                {
                    try
                    {
                        #region Sending Bonus Activation Message to Department Admin

                        #region Receiver Inbox
                        tblUserInBox userIn = new tblUserInBox();
                        userIn.AddNew();
                        userIn.FkFromUserID = UserID;
                        userIn.FkToUserID = Convert.ToInt32(users.GetColumn("fkuserid"));
                        if (chkBonus.Checked)
                        {
                            userIn.SSubject = "Bonus Activation";
                            userIn.SMessage = name + " bonus  is activated and waiting to be approved by department admin.";
                        }
                        else if (!chkBonus.Checked)
                        {
                            userIn.SSubject = "Bonus De-Activation";
                            userIn.SMessage = name + " bonus is deactivated.";
                        }
                        userIn.DReceivedDate = DateTime.Now;
                        userIn.BIsread = false;
                        userIn.Save();
                        #endregion

                        #region Sender SentBox
                        tblUserSentBox userOut = new tblUserSentBox();
                        userOut.AddNew();
                        userOut.FkFromUserID = UserID;
                        userOut.FkToUserID = Convert.ToInt32(users.GetColumn("fkuserid"));
                        if (chkBonus.Checked)
                        {
                            userOut.SSubject = "Bonus Activation";
                            userOut.SMessage = name + " bonus  is activated and waiting to be approved by department admin.";
                        }
                        else if (!chkBonus.Checked)
                        {
                            userOut.SSubject = "Bonus De-Activation";
                            userOut.SMessage = name + " bonus is deactivated.";
                        }
                        userOut.DSentDate = DateTime.Now;
                        userOut.Save();
                        #endregion

                        #endregion

                        ModalPopupExtender1.Hide();
                        if (rdoInActive.Checked)
                            BindGridInActiveUsers();
                        else if (rdoActive.Checked)
                            BindGridActiveUsers();
                        else
                            BindGridUsers();
                        ModalPopupExtender2.Hide();
                    }
                    catch (Exception ex)
                    {

                    }

                }
                else
                {
                    divActiveDeactiveBonusMessage.Visible = true;
                    divActiveDeactiveBonusMessage.InnerHtml = "<span style='color:Green;'>Bonus Successfully Updated</span> <br/><span style='color:Red;'> Message transfer fails to Department Admin</span>";
                }


                if (rdoInActive.Checked)
                    BindGridInActiveUsers();
                else if (rdoActive.Checked)
                    BindGridActiveUsers();
                else
                    BindGridUsers();
            }
            else
            {
                ModalPopupExtender2.Hide();
            }
        }

    }
}
