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
using MyGeneration.dOOdads;
public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            LoadDropdowns();
        }
    }
    private void LoadDropdowns()
    {
        tblDepartments department = new tblDepartments();
        tblSpecialityType position = new tblSpecialityType();
        tblCountries country = new tblCountries();
        department.LoadAll();
        position.GetNormalSpecialtyTypes();
        country.GetAllCountriesAlphabetically();
        commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");
        commonMethods.FillDropDownList(ddlSpeciality, position.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
        commonMethods.FillDropDownList(ddlCountry, country.DefaultView, "sCountry", "pkCountryID");
        commonMethods.FillDropDownList(ddlNationality, country.DefaultView, "sCountry", "pkCountryID");
        for (int i = 0; i <= 74; i++)
        {
            ddlYear.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
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
    protected void btnOK_Click(object sender, ImageClickEventArgs e)
    {
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
                else if(month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
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
                //user.s_SFaceBookProfile = "https://www.facebook.com/" + txtFacebook.Text;
                user.s_SSkypeProfile = txtSkype.Text;
                //user.s_STwitterProfile = "https://twitter.com/#!/" + txtTwitter.Text;

                if (txtFacebook.Text != "")
                {
                    txtFacebook.Text = commonMethods.filterFacebookURL(txtFacebook.Text);
                    user.s_SFaceBookProfile = "https://www.facebook.com/" + txtFacebook.Text;
                }

                if (txtTwitter.Text != "")
                {
                    txtTwitter.Text = commonMethods.filterTwitterURL(txtTwitter.Text);
                    user.s_STwitterProfile = "https://twitter.com/#!/" + txtTwitter.Text;
                }
                user.s_SMessengerProfile = txtMessenger.Text;
                user.s_SActivationCode =  Guid.NewGuid().ToString();
                user.BActiveByUser = false;
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
                    strLocation = Server.MapPath("UserImages/" + user.PkUserID + "/");
                    if (System.IO.Directory.Exists(strLocation) == false)
                    { System.IO.Directory.CreateDirectory(strLocation); }
                    //FileInfo file = new FileInfo(Server.MapPath(dataRow["FilePath"].ToString() + dataRow["FileName"].ToString()));
                    string strFilename = fileupload.FileName;//.PostedFile.FileName;
                    string strExtension = string.Empty;
                    // bool flag = true;
                    strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));



                    if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
                    {

                        //   flag = true;
                        commonMethods.GiveAccessRights(strLocation);
                        fileupload.PostedFile.SaveAs(strLocation + strFilename);


                        user.s_SImagePath = "../UserImages/" + user.PkUserID + "/" + strFilename;

                        user.Save();

                    }


                }

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

                tblUserDepartment department = new tblUserDepartment();
                department.AddNew();
                department.s_FkDepartmentID = ddlDepartments.SelectedValue;
                department.FkUserID = user.PkUserID;
                department.DCreateDate = DateTime.Now;
                department.DModifiedDate = DateTime.Now;
                department.Save();

                //Add Address

                if (txtAddress.Text != "")
                {
                    tblUserAddresses userAddress = new tblUserAddresses();
                    userAddress.AddNew();
                    userAddress.FkUserID = user.PkUserID;
                    userAddress.BIsPrimary = true;
                    userAddress.s_SAddressStreet = txtAddress.Text;
                    userAddress.s_SAddressTown = txtTown.Text;
                    userAddress.SAddressPostCode = txtPostcode.Text;
                    userAddress.SAddressRegion = txtRegion.Text;
                    userAddress.s_FkAddressCountry = ddlCountry.SelectedValue;
                    userAddress.DCreateDate = DateTime.Now;
                    userAddress.DModifiedDate = DateTime.Now;
                    userAddress.Save();

                }
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

                Emailing email = new Emailing();
                email.P_ToAddress = txtEmail.Text;
                email.P_FromAddress = ConfigurationManager.AppSettings["EmailUserName"].ToString();//"noreply@West.com";
                email.P_Email_Subject = "Activate your Account @ West Bar";
                string strMessage = string.Empty;
                string strCode = new Guid().ToString();
                strMessage += "Dear " + txtFirstName.Text + " " + txtLastName.Text + "<br/>";
                //http://www.westbar.com/ActivateAccount.aspx
                strMessage += "Your account has been successfully created at West Bar. Please visit " + ConfigurationManager.AppSettings["ActivateAccount"].ToString() + " and enter following code.<br/>";
                strMessage += " Activattion code : " + user.s_SActivationCode + "<br/> ";
                strMessage += "Thank you";
                email.P_Message_Body = strMessage;
                email.Send_Email_New();

                Response.Redirect("Activation.aspx");

            }
            catch (Exception ex)
            {
                tx.RollbackTransaction();
                TransactionMgr.ThreadTransactionMgrReset();
                
            }

        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Username already exist";
        }
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
}
