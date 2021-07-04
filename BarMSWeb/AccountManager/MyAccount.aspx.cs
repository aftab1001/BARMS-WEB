using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using LC.Model.BMS.BLL;
using System.Data;

public partial class AccountManager_MyAccount : System.Web.UI.Page
{
    int UserID;
    int ManagerUserID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["id"] != "")
        {
            string id = Request.QueryString["id"].ToString();
            UserID = Convert.ToInt32(id);
        }

        lblRequest.Visible = false;
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            if (user.AccessLevel != 4)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }
            ManagerUserID = user.UserID;
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        //UserID = 8;
        if (!Page.IsPostBack)
        {
            BindDropDowns();
            LoadValues();
            BindEmailGrid();
            BindMobileGrid();
            BindSpecialityGrid();
            BindAddressGrid();
        }
    }
    private void BindDropDowns()
    {
        tblDepartments department = new tblDepartments();
        tblSpecialityType position = new tblSpecialityType();
        tblCountries country = new tblCountries();
        department.LoadAll();
        position.GetSpecialityTypesWithoutSeparator();
        country.GetAllCountriesAlphabetically();
        //commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");
        commonMethods.FillDropDownList(ddlSpeciality, position.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
        commonMethods.FillDropDownList(ddlCountry, country.DefaultView, "sCountry", "pkCountryID");
        commonMethods.FillDropDownList(ddlNationality, country.DefaultView, "sCountry", "pkCountryID");
        commonMethods.FillDropDownList(ddlNationality, country.DefaultView, "sCountry", "pkCountryID");
        for (int i = 0; i <= 74; i++)
        {
            ddlYear.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
        }

        commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");


    }
    private void BindAddressGrid()
    {
        tblUserAddresses address = new tblUserAddresses();
        address.LoadUserAddress(UserID);
        grdAddress.DataSource = address.DefaultView;
        grdAddress.DataBind();
    }
    private void BindEmailGrid()
    {
        tblUserEmails emails = new tblUserEmails();
        emails.LoadUserEmails(UserID);
        if (emails.RowCount > 0)
        {
            grdEmails.DataSource = emails.DefaultView;
            grdEmails.DataBind();
        }

    }
    private void BindMobileGrid()
    {
        tblUserMobile mobile = new tblUserMobile();
        mobile.LoadUserMobiles(UserID);
        if (mobile.RowCount > 0)
        {
            grdMobile.DataSource = mobile.DefaultView;
            grdMobile.DataBind();
        }

    }

    private void BindSpecialityGrid()
    {
        tblUserSpeciality speciality = new tblUserSpeciality();
        speciality.LoadUserSpeciality(UserID);
        if (speciality.RowCount > 0)
        {
            grdSpeciality.DataSource = speciality.DefaultView;
            grdSpeciality.DataBind();
        }

    }
    private void LoadValues()
    {
        tblUsers users = new tblUsers();
        users.LoadByPrimaryKey(UserID);
        if (users.s_SUsername.Length <= 17)
        {
            lblUsername.Text = users.s_SUsername;
        }
        else
        {
            lblUsername.Text = users.s_SUsername.Substring(0, 16);
            lblUsername.ToolTip = users.s_SUsername;
        }
        txtFirstName.Text = users.s_SFirstName;
        txtLastName.Text = users.s_SLastName;
        ddlGender.SelectedValue = users.s_IGender;
        if (File.Exists(Server.MapPath(users.s_SImagePath)))
        {
            if(users.s_SImagePath != null && users.s_SImagePath != "")
                userImage.Src = users.s_SImagePath;
        }

        if (users.s_SFaceBookProfile != "")
            txtFacebook.Text = users.s_SFaceBookProfile.Substring(25);
        txtSkype.Text = users.s_SSkypeProfile;
        if (users.s_STwitterProfile != "")
            txtTwitter.Text = users.s_STwitterProfile.Substring(23);
        txtMessenger.Text = users.s_SMessengerProfile;
        ddlNationality.SelectedValue = users.FkNationalityCountry.ToString();

        string day = string.Empty;
        string month = string.Empty;
        string year = string.Empty;

        day = users.DOB.Day.ToString();
        month = users.DOB.Month.ToString();
        year = users.DOB.Year.ToString();

        txtDate.Text = day;
        ddlMonth.SelectedValue = month;
        ddlYear.SelectedValue = year;

        tblUserDepartment Udepartment = new tblUserDepartment();
        Udepartment.LoadUserDepartment(UserID);
        ddlDepartments.SelectedValue = Udepartment.FkDepartmentID.ToString();

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
                emailMain.LoadUserEmails(UserID);
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
                mobileMain.LoadUserMobiles(UserID);
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
                SpecialityMain.LoadUserSpeciality(UserID);
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
        if (txtEmail.Text != "")
        {
            if (CheckEmailExist(txtEmail.Text))
            {
                tblUserEmails userEmail = new tblUserEmails();
                userEmail.AddNew();
                userEmail.FkUserID = UserID;
                userEmail.s_SEmail = txtEmail.Text;
                userEmail.BIsPrimary = false;
                userEmail.DCreateDate = DateTime.Now;
                userEmail.DModifiedDate = DateTime.Now;
                userEmail.Save();
                BindEmailGrid();
            }
        }
    }
    private bool CheckEmailExist(string email)
    {
        tblUserEmails userEmail = new tblUserEmails();
        userEmail.CheckEmail(email, UserID);
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
        special.CheckSpeciality(SpecialityID, UserID);
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
            mobile.FkUserID = UserID;
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
            special.FkUserID = UserID;
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
        user.LoadByPrimaryKey(UserID);

        user.s_SPassword = txtNewPassword.Text;
        user.s_SFirstName = txtFirstName.Text;
        user.s_SLastName = txtLastName.Text;
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

        //user.s_DOB = datepicker.Text;
        user.s_FkNationalityCountry = ddlNationality.SelectedValue;

        if (txtFacebook.Text != "")
        {
            txtFacebook.Text =commonMethods.filterFacebookURL(txtFacebook.Text);
            user.s_SFaceBookProfile = "https://www.facebook.com/" + txtFacebook.Text;
        }

        if (txtTwitter.Text != "")
        {
            txtTwitter.Text =commonMethods.filterTwitterURL(txtTwitter.Text);
            user.s_STwitterProfile = "https://twitter.com/#!/" + txtTwitter.Text;
        }
        user.s_SSkypeProfile = txtSkype.Text;
        
        user.s_SMessengerProfile = txtMessenger.Text;
        user.DOB = d;
        if (fpUploadPic.PostedFile.FileName != "")
        {
            string strLocation = string.Empty;
            strLocation = Server.MapPath("../UserImages/" + user.PkUserID + "/");
            if (System.IO.Directory.Exists(strLocation) == false)
            { System.IO.Directory.CreateDirectory(strLocation); }
            //FileInfo file = new FileInfo(Server.MapPath(dataRow["FilePath"].ToString() + dataRow["FileName"].ToString()));
            string strFilename = fpUploadPic.FileName;//.PostedFile.FileName;
            string strExtension = string.Empty;
            // bool flag = true;
            strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));



            if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
            {

                //   flag = true;
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
        //user.BActiveByUser = false;
        // user.BActiveByAdmin = false;
        //user.DCreateDate = DateTime.Now;
        user.DModifiedDate = DateTime.Now;
        user.Save();
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
                UAddressMain.LoadUserAddress(UserID);
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
        if (txtAddress.Text != "")
        {
            tblUserAddresses UAddress = new tblUserAddresses();
            UAddress.AddNew();
            UAddress.FkUserID = UserID;
            UAddress.s_SAddressStreet = txtAddress.Text;
            UAddress.s_SAddressTown = txtTown.Text;
            UAddress.SAddressRegion = txtRegion.Text;
            UAddress.SAddressPostCode = txtPostCode.Text;
            UAddress.s_FkAddressCountry = ddlCountry.SelectedValue;
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
        if (txtMobile.Text != "")
        {
            mobile.AddNew();
            mobile.FkUserID = UserID;
            mobile.s_SMobilePhone = txtMobile.Text;
            mobile.BIsPrimary = false;
            mobile.DCreatedDate = DateTime.Now;
            mobile.DModifiedDate = DateTime.Now;
            mobile.Save();
            BindMobileGrid();
        }
        txtMobile.Text = "";
    }
}
