using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
using MyGeneration.dOOdads;
public partial class Managers_Register : System.Web.UI.Page
{
    static int UserID;
    static int DepartmentID = 0;
    static int SpecialUserID;
    static int BonusUserID = 0;
    static int ToUserID = 0;
    static string filterMessage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["UserLogin"] != null)
            {
                SessionUser user = new SessionUser();
                user = (SessionUser)Session["UserLogin"];

                if (user.AccessLevel != 2)
                {
                    Session["UserLogin"] = null;
                    Response.Redirect("../West_login.aspx");
                }

                UserID = user.UserID;
                DepartmentID = user.DepartmentID;
            }
            else
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }

        }
        if (!IsPostBack)
        {
            LoadDropdowns();
            GetSpecialUsers();
        }
    }
    private void LoadDropdowns()
    {
        try
        {
            tblDepartments department = new tblDepartments();
            tblSpecialityType sptype = new tblSpecialityType();
            tblCountries country = new tblCountries();
            department.LoadAll();
            sptype.getSpecialSpecialtyies();
            country.GetAllCountriesAlphabetically();
            commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");
            commonMethods.FillDropDownList(ddlSpeciality, sptype.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
            commonMethods.FillDropDownList(ddlCountry, country.DefaultView, "sCountry", "pkCountryID");
            commonMethods.FillDropDownList(ddlNationality, country.DefaultView, "sCountry", "pkCountryID");
            for (int i = 0; i <= 74; i++)
            {
                ddlYear.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
            }
        }
        catch (Exception ex)
        { }


    }
    //#region Removing base address of facebook and twitter
    //private string filterFacebookURL(string facebookURL)
    //{
    //    if (facebookURL.ToLower().IndexOf("https") != -1)
    //        facebookURL = facebookURL.Replace("https", "").Replace("HTTPS", "");
    //    if (facebookURL.ToLower().IndexOf("http") != -1)
    //        facebookURL = facebookURL.Replace("http", "").Replace("HTTP", "");
    //    if (facebookURL.IndexOf(":") != -1)
    //        facebookURL = facebookURL.Replace(":", "");
    //    if (facebookURL.IndexOf("//") != -1)
    //        facebookURL = facebookURL.Replace("//", "");
    //    if (facebookURL.ToLower().IndexOf("www") != -1)
    //        facebookURL = facebookURL.Replace("www", "").Replace("WWW", "");
    //    if (facebookURL.IndexOf(".") != -1)
    //        facebookURL = facebookURL.Replace(".", "");
    //    if (facebookURL.ToLower().IndexOf("facebook") != -1)
    //        facebookURL = facebookURL.Replace("facebook", "").Replace("FACEBOOK", "");
    //    if (facebookURL.IndexOf(".") != -1)
    //        facebookURL = facebookURL.Replace(".", "");
    //    if (facebookURL.ToLower().IndexOf("com") != -1)
    //        facebookURL = facebookURL.Replace("com", "").Replace("COM", "");
    //    if (facebookURL.IndexOf("/") != -1)
    //        facebookURL = facebookURL.Replace("/", "");
    //    return facebookURL;
    //}
    //private string filterTwitterURL(string twitterURL)
    //{
    //    if (twitterURL.ToLower().IndexOf("https") != -1)
    //        twitterURL = twitterURL.Replace("https", "").Replace("HTTPS", "");
    //    if (twitterURL.ToLower().IndexOf("http") != -1)
    //        twitterURL = twitterURL.Replace("http", "").Replace("HTTP", "");
    //    if (twitterURL.IndexOf(":") != -1)
    //        twitterURL = twitterURL.Replace(":", "");
    //    if (twitterURL.IndexOf("//") != -1)
    //        twitterURL = twitterURL.Replace("//", "");
    //    if (twitterURL.ToLower().IndexOf("www") != -1)
    //        twitterURL = twitterURL.Replace("www", "").Replace("WWW", "");
    //    if (twitterURL.IndexOf(".") != -1)
    //        twitterURL = twitterURL.Replace(".", "");
    //    if (twitterURL.ToLower().IndexOf("twitter") != -1)
    //        twitterURL = twitterURL.Replace("twitter", "").Replace("TWITTER", "");
    //    if (twitterURL.IndexOf(".") != -1)
    //        twitterURL = twitterURL.Replace(".", "");
    //    if (twitterURL.ToLower().IndexOf("com") != -1)
    //        twitterURL = twitterURL.Replace("com", "").Replace("COM", "");
    //    if (twitterURL.IndexOf("/") != -1)
    //        twitterURL = twitterURL.Replace("/", "");
    //    if (twitterURL.IndexOf("#") != -1)
    //        twitterURL = twitterURL.Replace("#", "");
    //    if (twitterURL.IndexOf("!") != -1)
    //        twitterURL = twitterURL.Replace("!", "");
    //    if (twitterURL.IndexOf("/") != -1)
    //        twitterURL = twitterURL.Replace("/", "");
    //    return twitterURL;
    //}
    //#endregion

    #region adding new Special User
    protected void imgBtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        mvUser.SetActiveView(vNewUser);
    }
    #endregion

    #region Special Users

    private void GetSpecialUsers()
    {
        tblUsers u = new tblUsers();
        u.GetSpecialUsers(DepartmentID);
        grdUsers.DataSource = u.DefaultView;
        grdUsers.DataBind();
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

                        GetSpecialUsers();
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



                GetSpecialUsers();
            }
            else
            {
                ModalPopupExtender2.Hide();
            }
        }

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
            GetSpecialUsers();
        }
        catch (Exception ex)
        {

        }


    }
    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton imgBtnActive = e.Row.FindControl("imgBtnActive") as ImageButton;
                Label lblName = e.Row.FindControl("lblName") as Label;


                DataRowView drv = e.Row.DataItem as DataRowView;


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

                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                if (Convert.ToBoolean(drv["BActiveByAdmin"]))
                {
                    imgBtnActive.ImageUrl = "../Images/activate_icon.gif";
                }
                else
                {
                    imgBtnActive.ImageUrl = "../Images/close.png";
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandArgument != null)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                SpecialUserID = id;
                switch (e.CommandName.ToLower())
                {
                    case "active":

                        tblUsers u = new tblUsers();
                        u.LoadByPrimaryKey(id);
                        if (u.RowCount > 0)
                        {
                            if (u.BActiveByAdmin)
                                u.BActiveByAdmin = false;
                            else if (!u.BActiveByAdmin)
                                u.BActiveByAdmin = true;
                            u.FkActivatedByAdminID = UserID;
                            u.DModifiedDate = DateTime.Now;
                            u.Save();
                            GetSpecialUsers();
                        }
                        break;
                    case "edt":
                        ViewState["spid"] = id;
                        BindDropDowns();
                        LoadValues();
                        BindEmailGrid();
                        BindMobileGrid();
                        BindSpecialityGrid();
                        BindAddressGrid();
                        mvUser.SetActiveView(vUpdate);
                        break;
                    case "message":
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
                    case "salary":
                        Response.Redirect("ManageSalary.aspx?uid=" + Convert.ToInt32(e.CommandArgument).ToString());
                        break;
                    case "bonus":
                        int uidBonus = Convert.ToInt32(e.CommandArgument);
                        BonusUserID = uidBonus;
                        txtBonusAmount.Text = "";
                        txtNoteBonus.Text = "";
                        tblUsers userBonus = new tblUsers();
                        userBonus.LoadByPrimaryKey(uidBonus);
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
                                lblMessage.Visible = false;
                                loadContract(contract);

                            }
                            else
                            {
                                lblMessage.Visible = true;
                                lblMessage.Text = "Not Salary ";
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
                            //if (userBonus.BActiveBonusDoc)
                            //    chkBonus.Checked = true;
                            //else
                            //    chkBonus.Checked = false;

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
        }
        catch (Exception ex)
        { }
    }
    #endregion
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
    protected void btnOK_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            #region adding Special User

            if (CheckUserExist(txtUsername.Text))
            {
                TransactionMgr tx = TransactionMgr.ThreadTransactionMgr();
                try
                {
                    tx.BeginTransaction();
                    tblUsers user = new tblUsers();
                    user.AddNew();
                    user.s_SUsername = txtUsername.Text;
                    user.s_SPassword = txtPassword.Text;
                    user.s_SFirstName = txtFirstName.Text;
                    user.s_SLastName = txtLastName.Text;
                    /*
                    user.s_IGender = ddlGender.SelectedValue;
                    int day = Convert.ToInt32(txtDate.Text);
                    int month = Convert.ToInt32(ddlMonth.SelectedValue);
                    if (month == 2)
                    {
                        if (day > 28)
                        {
                            day = 28;
                        }
                    }
                    else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    {
                        if (day > 31)
                        {
                            day = 31;
                        }
                    }
                    else
                    {
                        if (day > 30)
                        {
                            day = 30;
                        }
                    }
                    DateTime d = Convert.ToDateTime(ddlMonth.SelectedValue + "/" + day.ToString() + "/" + ddlYear.SelectedValue);

                    user.s_DOB = d.ToString();
                    //return;
                    user.s_FkNationalityCountry = ddlNationality.SelectedValue;

                    user.s_SActivationCode = Guid.NewGuid().ToString();
                     */
                    user.BActiveByUser = true;
                    user.BActiveByAdmin = false;
                    user.BActiveBonusDoc = false;
                    user.BBonusApprovedByDepartment = false;
                    user.DCreateDate = DateTime.Now;
                    user.DModifiedDate = DateTime.Now;
                    user.Save();
                    //if (fileupload.HasFile)
                    if (fileupload.PostedFile.FileName != "")
                    {
                        string strLocation = string.Empty;
                        strLocation = Server.MapPath("../UserImages/" + user.PkUserID + "/");
                        if (System.IO.Directory.Exists(strLocation) == false)
                        { System.IO.Directory.CreateDirectory(strLocation); }
                        //FileInfo file = new FileInfo(Server.MapPath(dataRow["FilePath"].ToString() + dataRow["FileName"].ToString()));
                        string strFilename = fileupload.FileName;//.PostedFile.FileName;
                        string strExtension = string.Empty;
                        // bool flag = true;
                        strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));



                        if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
                        {
                            commonMethods.GiveAccessRights(strLocation);
                            fileupload.PostedFile.SaveAs(strLocation + strFilename);
                            user.s_SImagePath = "../UserImages/" + user.PkUserID + "/" + strFilename;
                            user.Save();

                        }
                    }
                    /*
                    tblUserEmails userEmails = new tblUserEmails();
                    if (txtEmail.Text != "")
                    {
                        userEmails.AddNew();
                        userEmails.FkUserID = user.PkUserID;
                        userEmails.s_SEmail = txtEmail.Text;
                        userEmails.BIsPrimary = true;
                        userEmails.DCreateDate = DateTime.Now;
                        userEmails.DModifiedDate = DateTime.Now;
                        userEmails.Save();
                    }

                    //Add Mobile
                    tblUserMobile mobile = new tblUserMobile();
                    if (txtMobile.Text != "")
                    {
                        mobile.AddNew();
                        mobile.FkUserID = user.PkUserID;
                        mobile.BIsPrimary = true;
                        mobile.s_SMobilePhone = txtMobile.Text;
                        mobile.DModifiedDate = DateTime.Now;
                        mobile.DCreatedDate = DateTime.Now;
                        mobile.Save();
                    }

                    //Add Department
                    */
                    tblUserDepartment department = new tblUserDepartment();
                    department.AddNew();
                    department.s_FkDepartmentID = ddlDepartments.SelectedValue;
                    department.FkUserID = user.PkUserID;
                    department.DCreateDate = DateTime.Now;
                    department.DModifiedDate = DateTime.Now;
                    department.Save();
                    /*
                    //Add Address

                    if (txtAddress.Text != "")
                    {
                        tblUserAddresses userAddress = new tblUserAddresses();
                        userAddress.AddNew();
                        userAddress.FkUserID = user.PkUserID;
                        userAddress.BIsPrimary = true;
                        userAddress.s_SAddressStreet = txtAddress.Text;
                        userAddress.s_SAddressTown = txtTown.Text;
                        //userAddress.SAddressPostCode = txtPostcode.Text;
                        userAddress.SAddressRegion = txtRegion.Text;
                        userAddress.s_FkAddressCountry = ddlCountry.SelectedValue;
                        userAddress.DCreateDate = DateTime.Now;
                        userAddress.DModifiedDate = DateTime.Now;
                        userAddress.Save();

                    }
                    */
                    tblUserSpeciality special = new tblUserSpeciality();
                    special.AddNew();
                    special.s_FkSpecialityTypeID = ddlSpeciality.SelectedValue;
                    special.BIsPrimary = true;
                    special.FkUserID = user.PkUserID;
                    special.DCreateDate = DateTime.Now;
                    special.DModifiedDate = DateTime.Now;
                    special.Save();
                    tblUserAccessLevel userAcces = new tblUserAccessLevel();
                    userAcces.AddNew();
                    userAcces.FkUserID = user.PkUserID;
                    userAcces.FkAccessLevelID = 1;
                    userAcces.DCreateDate = DateTime.Now;
                    userAcces.DModifiedDate = DateTime.Now;
                    userAcces.Save();
                    //tx.RollbackTransaction();
                    //TransactionMgr.ThreadTransactionMgrReset();
                    tx.CommitTransaction();

                    //Sending Activation Email to user
                    /*
                    Emailing email = new Emailing();
                    email.P_ToAddress = txtEmail.Text;
                    email.P_FromAddress = "noreply@West.com";
                    email.P_Email_Subject = "Activate your Account @ West Bar";
                    string strMessage = string.Empty;
                    string strCode = new Guid().ToString();
                    strMessage += "Dear " + txtFirstName.Text + " " + txtLastName.Text + "<br/>";
                    //http://www.westbar.com/ActivateAccount.aspx
                    strMessage += "Your account has been successfully created at West Bar. Please visit http://75.150.196.117/BMS/ActivateAccount.aspx and enter following code.<br/>";
                    strMessage += " Activattion code : " + user.s_SActivationCode + "<br/> ";
                    strMessage += "Thank you";
                    email.P_Message_Body = strMessage;
                    email.Send_Email();
                    */


                }
                catch (Exception ex)
                {
                    tx.RollbackTransaction();
                    TransactionMgr.ThreadTransactionMgrReset();

                }

            }
            else
            {
                // lblError.Visible = true;
                // lblError.Text = "Username already exist";
            }



            GetSpecialUsers();
            mvUser.SetActiveView(vSpecialUsers);

            #endregion
        }
        catch (Exception ex)
        { }
    }
    private bool CheckUserExist(string username)
    {
        tblUsers user = new tblUsers();
        user.UserExist(username);
        if (user.RowCount > 0)
        {

            return false;
        }
        else
        {
            return true;
        }

    }
    private void BindDropDowns()
    {
        tblDepartments department = new tblDepartments();
        tblSpecialityType position = new tblSpecialityType();
        tblCountries country = new tblCountries();
        position.checkSpecialUser(SpecialUserID);
        if (position.RowCount > 0)
        {
            position.FlushData();
            position.getSpecialSpecialtyies();
        }
        else
        {
            position.FlushData();
            position.GetNormalSpecialtyTypes();
        }
        department.LoadAll();

        country.GetAllCountriesAlphabetically();
        //commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");
        commonMethods.FillDropDownList(ddlSpecialtyUpdate, position.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
        commonMethods.FillDropDownList(ddlCountryUpdate, country.DefaultView, "sCountry", "pkCountryID");
        commonMethods.FillDropDownList(ddlNationalityUpadate, country.DefaultView, "sCountry", "pkCountryID");

        for (int i = 0; i <= 74; i++)
        {
            ddlYearUpdate.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
        }

        commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");


    }
    private void BindAddressGrid()
    {
        tblUserAddresses address = new tblUserAddresses();
        address.LoadUserAddress(SpecialUserID);
        grdAddress.DataSource = address.DefaultView;
        grdAddress.DataBind();
        upnlAddress.Update();
    }
    private void BindEmailGrid()
    {
        tblUserEmails emails = new tblUserEmails();
        emails.LoadUserEmails(SpecialUserID);
        if (emails.RowCount > 0)
        {
            grdEmails.DataSource = emails.DefaultView;
            grdEmails.DataBind();
        }
        upnlEmail.Update();

    }
    private void BindMobileGrid()
    {
        tblUserMobile mobile = new tblUserMobile();
        mobile.LoadUserMobiles(SpecialUserID);
        if (mobile.RowCount > 0)
        {
            grdMobile.DataSource = mobile.DefaultView;
            grdMobile.DataBind();
        }
        upnlMobile.Update();

    }

    private void BindSpecialityGrid()
    {
        tblUserSpeciality speciality = new tblUserSpeciality();
        speciality.LoadUserSpeciality(SpecialUserID);
        if (speciality.RowCount > 0)
        {
            grdSpeciality.DataSource = speciality.DefaultView;
            grdSpeciality.DataBind();
        }
        upnlSpecialty.Update();
    }
    private void LoadValues()
    {
        tblUsers users = new tblUsers();
        users.LoadByPrimaryKey(SpecialUserID);
        if (users.s_SUsername.Length <= 17)
        {
            lblUsername.Text = users.s_SUsername;
        }
        else
        {
            lblUsername.Text = users.s_SUsername.Substring(0, 16);
            lblUsername.ToolTip = users.s_SUsername;
        }
        txtFirstNameUpdate.Text = users.s_SFirstName;
        txtLastNameUpdate.Text = users.s_SLastName;
        try
        {
            ddlGenderUpdate.SelectedValue = users.s_IGender;
        }
        catch (ArgumentOutOfRangeException ex)
        { }
        if (File.Exists(Server.MapPath(users.s_SImagePath)))
        {
            if (users.s_SImagePath != null && users.s_SImagePath != "")
                userImage.Src = users.s_SImagePath;
        }

        //if (users.s_SFaceBookProfile != "")
        //    txtFacebook.Text = users.s_SFaceBookProfile.Substring(25);
        //txtSkype.Text = users.s_SSkypeProfile;
        //if (users.s_STwitterProfile != "")
        //    txtTwitter.Text = users.s_STwitterProfile.Substring(23);
        //txtMessenger.Text = users.s_SMessengerProfile;
        try
        {
            ddlNationalityUpadate.SelectedValue = users.FkNationalityCountry.ToString();

            string day = string.Empty;
            string month = string.Empty;
            string year = string.Empty;

            day = users.DOB.Day.ToString();
            month = users.DOB.Month.ToString();
            year = users.DOB.Year.ToString();

            txtBirthDateUpdate.Text = day;
            ddlMonthUpdate.SelectedValue = month;
            ddlYearUpdate.SelectedValue = year;

            tblUserDepartment Udepartment = new tblUserDepartment();
            Udepartment.LoadUserDepartment(SpecialUserID);
            ddlDepartments.SelectedValue = Udepartment.FkDepartmentID.ToString();
        }
        catch (Exception ex)
        { }

    }
    protected void grdEmails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int emailID;
        switch (e.CommandName)
        {


            case "Del":
                emailID = Convert.ToInt32(e.CommandArgument);
                tblUserEmails email = new tblUserEmails();
                email.LoadByPrimaryKey(emailID);
                email.MarkAsDeleted();
                email.Save();
                BindEmailGrid();
                break;
            case "main":
                emailID = Convert.ToInt32(e.CommandArgument);
                tblUserEmails emailMain = new tblUserEmails();
                emailMain.LoadUserEmails(SpecialUserID);
                if (emailMain.RowCount > 0)
                {
                    for (int i = 0; i < emailMain.RowCount; i++)
                    {
                        if (emailMain.PkEmailID == emailID)
                            emailMain.BIsPrimary = true;
                        else
                            emailMain.BIsPrimary = false;
                        emailMain.Save();
                        emailMain.DModifiedDate = DateTime.Now;
                        emailMain.MoveNext();
                    }
                }



                BindEmailGrid();
                break;
        }
    }
    protected void grdEmails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton imgbtnSetActiveEmail = (ImageButton)e.Row.FindControl("imgbtnSetActiveEmail");
                if (Convert.ToBoolean(drv["bIsPrimary"]))
                {
                    imgbtnSetActiveEmail.ImageUrl = "~/Images/Star_Black.png";
                    imgbtnSetActiveEmail.Width = 16;
                    imgbtnSetActiveEmail.ToolTip = "Active";
                }

                imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                Label lblemail = (Label)e.Row.FindControl("lblemail");
                lblemail.Text = drv["sEmail"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdMobile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int MobileID;
        switch (e.CommandName)
        {


            case "Del":
                MobileID = Convert.ToInt32(e.CommandArgument);
                tblUserMobile mobile = new tblUserMobile();
                mobile.LoadByPrimaryKey(MobileID);
                mobile.MarkAsDeleted();
                mobile.Save();
                BindMobileGrid();
                break;
            case "main":
                MobileID = Convert.ToInt32(e.CommandArgument);
                tblUserMobile mobileMain = new tblUserMobile();
                mobileMain.LoadUserMobiles(SpecialUserID);
                if (mobileMain.RowCount > 0)
                {
                    for (int i = 0; i < mobileMain.RowCount; i++)
                    {
                        if (mobileMain.PkPhineID == MobileID)
                            mobileMain.BIsPrimary = true;
                        else
                            mobileMain.BIsPrimary = false;
                        mobileMain.Save();
                        mobileMain.DModifiedDate = DateTime.Now;
                        mobileMain.MoveNext();
                    }
                    BindMobileGrid();
                }
                break;

        }
    }
    protected void grdMobile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton imgBtnActiveMobile = (ImageButton)e.Row.FindControl("imgBtnActiveMobile");
                if (Convert.ToBoolean(drv["bIsPrimary"]))
                {
                    imgBtnActiveMobile.ImageUrl = "~/Images/Star_Black.png";
                    imgBtnActiveMobile.Width = 16;
                    imgBtnActiveMobile.ToolTip = "Active";
                }
                imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                Label lblMobile = (Label)e.Row.FindControl("lblMobile");
                lblMobile.Text = drv["sMobilePhone"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdSpeciality_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int SpecialityID;
        switch (e.CommandName)
        {


            case "Del":
                SpecialityID = Convert.ToInt32(e.CommandArgument);
                tblUserSpeciality Speciality = new tblUserSpeciality();
                Speciality.LoadByPrimaryKey(SpecialityID);
                Speciality.MarkAsDeleted();
                Speciality.Save();
                BindSpecialityGrid();
                break;
            case "main":
                SpecialityID = Convert.ToInt32(e.CommandArgument);
                tblUserSpeciality SpecialityMain = new tblUserSpeciality();
                SpecialityMain.LoadUserSpeciality(SpecialUserID);
                if (SpecialityMain.RowCount > 0)
                {
                    for (int i = 0; i < SpecialityMain.RowCount; i++)
                    {
                        if (SpecialityMain.PkUserSpecialityID == SpecialityID)
                            SpecialityMain.BIsPrimary = true;
                        else
                            SpecialityMain.BIsPrimary = false;
                        SpecialityMain.Save();
                        SpecialityMain.DModifiedDate = DateTime.Now;
                        SpecialityMain.MoveNext();
                    }
                    BindSpecialityGrid();
                }
                break;

        }
    }
    protected void grdSpeciality_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton imgBtnActiveSpeciality = (ImageButton)e.Row.FindControl("imgBtnActiveSpeciality");
                if (Convert.ToBoolean(drv["bIsPrimary"]))
                {
                    imgBtnActiveSpeciality.ImageUrl = "~/Images/Star_Black.png";
                    imgBtnActiveSpeciality.Width = 16;
                    imgBtnActiveSpeciality.ToolTip = "Active";
                }
                imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                Label lblSpeciality = (Label)e.Row.FindControl("lblSpeciality");
                lblSpeciality.Text = drv["sSpecialityName"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnEmail_Click(object sender, ImageClickEventArgs e)
    {
        if (TextBox1.Text != "")
        {
            if (CheckEmailExist(TextBox1.Text))
            {
                tblUserEmails userEmail = new tblUserEmails();
                userEmail.AddNew();
                userEmail.FkUserID = SpecialUserID;
                userEmail.s_SEmail = TextBox1.Text;
                userEmail.BIsPrimary = false;
                userEmail.DCreateDate = DateTime.Now;
                userEmail.DModifiedDate = DateTime.Now;
                userEmail.Save();
                TextBox1.Text = "";
                BindEmailGrid();
            }
        }
    }
    private bool CheckEmailExist(string email)
    {
        tblUserEmails userEmail = new tblUserEmails();
        userEmail.CheckEmail(email, SpecialUserID);
        if (userEmail.RowCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool CheckSpecialityExist(int SpecialityID)
    {
        tblUserSpeciality special = new tblUserSpeciality();
        special.CheckSpeciality(SpecialityID, SpecialUserID);
        if (special.RowCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void btnMobile_Click(object sender, ImageClickEventArgs e)
    {
        tblUserMobile mobile = new tblUserMobile();
        if (txtMobile.Text != "")
        {
            mobile.AddNew();
            mobile.FkUserID = SpecialUserID;
            mobile.s_SMobilePhone = txtMobile.Text;
            mobile.Save();
            BindMobileGrid();
        }
    }
    protected void btnSpeciality_Click(object sender, ImageClickEventArgs e)
    {
        if (CheckSpecialityExist(Convert.ToInt32(ddlSpeciality.SelectedValue)))
        {
            tblUserSpeciality special = new tblUserSpeciality();
            special.AddNew();
            special.s_FkSpecialityTypeID = ddlSpeciality.SelectedValue;
            special.BIsPrimary = false;
            special.FkUserID = SpecialUserID;
            special.DCreateDate = DateTime.Now;
            special.DModifiedDate = DateTime.Now;
            special.Save();
            BindSpecialityGrid();
            BindDropDowns();
        }
    }
    //#region Removing base address of facebook and twitter
    //private string filterFacebookURL(string facebookURL)
    //{
    //    if (facebookURL.ToLower().IndexOf("https") != -1)
    //        facebookURL = facebookURL.Replace("https", "").Replace("HTTPS", "");
    //    if (facebookURL.ToLower().IndexOf("http") != -1)
    //        facebookURL = facebookURL.Replace("http", "").Replace("HTTP", "");
    //    if (facebookURL.IndexOf(":") != -1)
    //        facebookURL = facebookURL.Replace(":", "");
    //    if (facebookURL.IndexOf("//") != -1)
    //        facebookURL = facebookURL.Replace("//", "");
    //    if (facebookURL.ToLower().IndexOf("www") != -1)
    //        facebookURL = facebookURL.Replace("www", "").Replace("WWW", "");
    //    if (facebookURL.IndexOf(".") != -1)
    //        facebookURL = facebookURL.Replace(".", "");
    //    if (facebookURL.ToLower().IndexOf("facebook") != -1)
    //        facebookURL = facebookURL.Replace("facebook", "").Replace("FACEBOOK", "");
    //    if (facebookURL.IndexOf(".") != -1)
    //        facebookURL = facebookURL.Replace(".", "");
    //    if (facebookURL.ToLower().IndexOf("com") != -1)
    //        facebookURL = facebookURL.Replace("com", "").Replace("COM", "");
    //    if (facebookURL.IndexOf("/") != -1)
    //        facebookURL = facebookURL.Replace("/", "");
    //    return facebookURL;
    //}
    //private string filterTwitterURL(string twitterURL)
    //{
    //    if (twitterURL.ToLower().IndexOf("https") != -1)
    //        twitterURL = twitterURL.Replace("https", "").Replace("HTTPS", "");
    //    if (twitterURL.ToLower().IndexOf("http") != -1)
    //        twitterURL = twitterURL.Replace("http", "").Replace("HTTP", "");
    //    if (twitterURL.IndexOf(":") != -1)
    //        twitterURL = twitterURL.Replace(":", "");
    //    if (twitterURL.IndexOf("//") != -1)
    //        twitterURL = twitterURL.Replace("//", "");
    //    if (twitterURL.ToLower().IndexOf("www") != -1)
    //        twitterURL = twitterURL.Replace("www", "").Replace("WWW", "");
    //    if (twitterURL.IndexOf(".") != -1)
    //        twitterURL = twitterURL.Replace(".", "");
    //    if (twitterURL.ToLower().IndexOf("twitter") != -1)
    //        twitterURL = twitterURL.Replace("twitter", "").Replace("TWITTER", "");
    //    if (twitterURL.IndexOf(".") != -1)
    //        twitterURL = twitterURL.Replace(".", "");
    //    if (twitterURL.ToLower().IndexOf("com") != -1)
    //        twitterURL = twitterURL.Replace("com", "").Replace("COM", "");
    //    if (twitterURL.IndexOf("/") != -1)
    //        twitterURL = twitterURL.Replace("/", "");
    //    if (twitterURL.IndexOf("#") != -1)
    //        twitterURL = twitterURL.Replace("#", "");
    //    if (twitterURL.IndexOf("!") != -1)
    //        twitterURL = twitterURL.Replace("!", "");
    //    if (twitterURL.IndexOf("/") != -1)
    //        twitterURL = twitterURL.Replace("/", "");
    //    return twitterURL;
    //}
    //#endregion
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        tblUsers user = new tblUsers();
        user.LoadByPrimaryKey(SpecialUserID);

        // user.s_SPassword = txtPassword.Text;
        user.s_SFirstName = txtFirstNameUpdate.Text;
        user.s_SLastName = txtLastNameUpdate.Text;
        user.s_IGender = ddlGenderUpdate.SelectedValue;
        int day;
        try
        {
            day = Convert.ToInt32(txtBirthDateUpdate.Text);
            int month = Convert.ToInt32(ddlMonthUpdate.SelectedValue);
            if (month == 2)
            {
                if (day > 28)
                {
                    day = 28;
                }
            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day > 31)
                {
                    day = 31;
                }
            }
            else
            {
                if (day > 30)
                {
                    day = 30;
                }
            }
            DateTime d = Convert.ToDateTime(ddlMonthUpdate.SelectedValue + "/" + day.ToString() + "/" + ddlYearUpdate.SelectedValue);
            user.DOB = d;
        }
        catch (FormatException ex)
        { }

        //user.s_DOB = datepicker.Text;
        user.s_FkNationalityCountry = ddlNationalityUpadate.SelectedValue;


        
        if (fpUploadPic.PostedFile.FileName != "")
        {
            string strLocation = string.Empty;
            strLocation = Server.MapPath("../UserImages/" + user.PkUserID + "/");
            if (System.IO.Directory.Exists(strLocation) == false)
            { System.IO.Directory.CreateDirectory(strLocation); }
            //FileInfo file = new FileInfo(Server.MapPath(dataRow["FilePath"].ToString() + dataRow["FileName"].ToString()));
            string strFilename = fpUploadPic.FileName;//.PostedFile.FileName;
            string strExtension = string.Empty;
            strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));
            if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
            {
                commonMethods.GiveAccessRights(strLocation);

                if (File.Exists(Server.MapPath("../UserImages/" + user.PkUserID + "/" + strFilename)))
                {
                    File.Delete(Server.MapPath("../UserImages/" + user.PkUserID + "/" + strFilename));
                }
                fpUploadPic.PostedFile.SaveAs(strLocation + strFilename);
                user.s_SImagePath = "../UserImages/" + user.PkUserID + "/" + strFilename;
                user.Save();
            }
        }

        user.DModifiedDate = DateTime.Now;
        user.Save();

        if (txtOldPassword.Text != "")
        {
            tblUsers u = new tblUsers();
            u.CheckPassword(txtOldPassword.Text);
            if (u.RowCount > 0)
            {
                if (txtNewPassword.Text != "" && txtReNewPassword.Text != "")
                {
                    if (txtNewPassword.Text == txtReNewPassword.Text)
                    {
                        user.SPassword = txtNewPassword.Text;
                        user.DModifiedDate = DateTime.Now;
                        user.Save();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){alert('confirm password is different from new password !');});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){alert('Please enter new password and confirm password to save your new password !');});", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){alert('Please enter correct old password!');});", true);
            }
        }
        lblMessage.Visible = true;
        LoadValues();
    }
    protected void grdAddress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton imgBtnActiveAddress = (ImageButton)e.Row.FindControl("imgBtnActiveAddress");
                if (Convert.ToBoolean(drv["bIsPrimary"]))
                {
                    imgBtnActiveAddress.ImageUrl = "~/Images/Star_Black.png";
                    imgBtnActiveAddress.Width = 16;
                    imgBtnActiveAddress.ToolTip = "Active";
                }
                imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                Label lblSpeciality = (Label)e.Row.FindControl("lblSpeciality");
                lblSpeciality.Text = drv["sSpeciality"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdAddress_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int AddressID;
        switch (e.CommandName)
        {


            case "Del":
                AddressID = Convert.ToInt32(e.CommandArgument);
                tblUserAddresses UAddress = new tblUserAddresses();
                UAddress.LoadByPrimaryKey(AddressID);
                UAddress.MarkAsDeleted();
                UAddress.Save();
                BindAddressGrid();
                break;
            case "main":
                AddressID = Convert.ToInt32(e.CommandArgument);
                tblUserAddresses UAddressMain = new tblUserAddresses();
                UAddressMain.LoadUserAddress(SpecialUserID);
                if (UAddressMain.RowCount > 0)
                {
                    for (int i = 0; i < UAddressMain.RowCount; i++)
                    {
                        if (UAddressMain.PkAddressID == AddressID)
                            UAddressMain.BIsPrimary = true;
                        else
                            UAddressMain.BIsPrimary = false;
                        UAddressMain.Save();
                        UAddressMain.DModifiedDate = DateTime.Now;
                        UAddressMain.MoveNext();
                    }
                    BindAddressGrid();
                }
                break;

        }
    }
    protected void btnAddress_Click(object sender, ImageClickEventArgs e)
    {
        if (txtAddressUpdate.Text != "")
        {
            tblUserAddresses UAddress = new tblUserAddresses();
            UAddress.AddNew();
            UAddress.FkUserID = Convert.ToInt32(ViewState["spid"]);
            UAddress.s_SAddressStreet = txtAddressUpdate.Text;
            UAddress.s_SAddressTown = txtTownUpdate.Text;
            UAddress.SAddressRegion = txtRegionUpdate.Text;
            UAddress.SAddressPostCode = txtPostCodeUpdate.Text;
            UAddress.s_FkAddressCountry = ddlCountryUpdate.SelectedValue;
            UAddress.Save();
            BindAddressGrid();
        }
    }
    protected void btnSendRequest_Click(object sender, ImageClickEventArgs e)
    {
        lblRequest.Visible = true;
    }
    //protected void imgBtnImage_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (fpUploadPic.HasFile)
    //    {

    //    }
    //}
    protected void btnMobile_Click1(object sender, ImageClickEventArgs e)
    {
        tblUserMobile mobile = new tblUserMobile();
        if (TextBox2.Text != "")
        {
            mobile.AddNew();
            mobile.FkUserID = SpecialUserID;
            mobile.s_SMobilePhone = TextBox2.Text;
            mobile.BIsPrimary = false;
            mobile.DCreatedDate = DateTime.Now;
            mobile.DModifiedDate = DateTime.Now;
            mobile.Save();
            TextBox2.Text = "";
            BindMobileGrid();
        }
        txtMobile.Text = "";
    }
}

