using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data;
using LC.Model.BMS.BLL;
using MyGeneration.dOOdads;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Drawing.Imaging;

public partial class AccountManager_ManageSupplier : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    static int SupplierID = 0;
    static int CompanyID = 0;
    static int CompanySupplierID = 0;
    static int ContactPeopleID = 0;
    static int SupplierProductID = 0;
    static string senderEmail = "";
    static string receiverEmail = "";
    static double invoiceSum = 0.0;
    static double NonInvoiceSum = 0.0;

    HiddenField hidIsInvoice = new HiddenField();
    HiddenField hidOrderStatus = new HiddenField();

    static string sort = "Descending";

    static int graph_supplierid = 0;
    static int graph_relid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];

            if (user.AccessLevel != 4)
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

        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                tblSupplier supplierUpdate = new tblSupplier();
                supplierUpdate.LoadByPrimaryKey(Convert.ToInt32(Request.QueryString[0].ToString()));
                if (supplierUpdate.RowCount > 0)
                {
                    ViewState["Supplierid"] = supplierUpdate.PkSupplierID;
                    txtSupplierBrand.Text = supplierUpdate.SBrandName;
                    supplierImage.Src = supplierUpdate.SLogo;
                    rdContactEmail.Checked = supplierUpdate.SContactMethod_Email;
                    rdTelephone.Checked = supplierUpdate.SContactMethod_Phone;
                    rdFax.Checked = supplierUpdate.SContactMethod_Fax;

                    LoadSuppliersEmails();
                    LoadSuppliersPhones();
                    LoadSuppliersFaxes();
                    LoadSuppliersAddresses();
                    LoadProductSuppliers();
                    //calllingThreeFunctions();
                    LoadDefault();
                    loadSupplierProducts(Convert.ToInt32(Request.QueryString[0].ToString()));
                    LoadEditProductSuppliers();
                    LoadAllContactPeople(Convert.ToInt32(Request.QueryString[0].ToString()));
                    GetAllOrder();


                    tblCompanies company = new tblCompanies();
                    company.GetCompanies(SupplierID);
                    if (company.RowCount > 0)
                    {
                        CompanyID = company.PkCompanyID;
                        txtCompanyBrand.Text = company.CBrandName;
                        imgCompanyLogo.Src = company.Logo;
                        rdCompanyEmail.Checked = company.CContactMethod_Email;
                        rdCompanyTelephone.Checked = company.CContactMethod_Phone;
                        rdCompanyFax.Checked = company.CContactMethod_Fax;
                        ddlSuppliers.SelectedValue = company.FkSuplierID.ToString();

                        // LoadCompanyEmails();
                        // LoadCompanyPhones();
                        // LoadCompanyFaxes();
                        // LoadCompanyAddresses();
                        // LoadDefaultCompany();

                        ShowTRc();
                    }
                    else
                    {
                        CompanyID = 0;
                        txtCompanyBrand.Text = "";
                        rdCompanyTelephone.Checked = true;
                        imgCompanyLogo.Src = "../images/no_image.gif";
                    }




                    mvTab1.SetActiveView(vEditSupplier);
                    imgBtnAddSupplier.Visible = false;
                    lnkBackToSpplier.Visible = true;
                }
            }
            else
            {
                LoadAllSuppliers();
                LoadAllCompanies();
                LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));
                LoadCountries();
                LoadCountriesCompany();
                LoadEditProductSuppliers();
                LoadProductSuppliers();
                GetAllOrder();
                //calllingThreeFunctions();
            }
        }
    }

    #region Supplier

    private void LoadAllSuppliers()
    {
        tblSupplier supplier = new tblSupplier();
        supplier.GetDepartmentSuppliers(DepartmentID);

        grdSuppliers.DataSource = supplier.DefaultView;
        grdSuppliers.DataBind();
    }
    private void LoadSuppliersEmails()
    {
        tblSupplierEmails supplierEmails = new tblSupplierEmails();
        supplierEmails.GetSupplierEmails(Convert.ToInt32(ViewState["Supplierid"]));
        grdEmails.DataSource = supplierEmails.DefaultView;
        grdEmails.DataBind();
    }
    private void LoadSuppliersPhones()
    {
        tblSupplierPhone supplierPhones = new tblSupplierPhone();
        supplierPhones.GetSupplierPhones(Convert.ToInt32(ViewState["Supplierid"]));
        grdMobile.DataSource = supplierPhones.DefaultView;
        grdMobile.DataBind();
    }
    private void LoadDefault()
    {
        txtSupplierEmail.Text = "";

        txtSupplierFax.Text = "";
        txtSupplierMobile.Text = "";
        txtAddress.Text = "";
        txtTown.Text = "";
        txtPostCode.Text = "";
        txtRegion.Text = "";

        LoadCountries();
    }
    private void LoadSuppliersFaxes()
    {
        tblSupplierFaxes supplierFaxes = new tblSupplierFaxes();
        supplierFaxes.GetSupplierFaxes(Convert.ToInt32(ViewState["Supplierid"]));
        grdFax.DataSource = supplierFaxes.DefaultView;
        grdFax.DataBind();
    }
    private void LoadSuppliersAddresses()
    {
        tblSupplierAddresses supplierAddresses = new tblSupplierAddresses();
        supplierAddresses.GetSupplierAddresses(Convert.ToInt32(ViewState["Supplierid"]));
        grdAddress.DataSource = supplierAddresses.DefaultView;
        grdAddress.DataBind();
    }
    protected void imgBtnAddSupplier_Click(object sender, ImageClickEventArgs e)
    {

        SupplierID = 0;
        imgBtnAddSupplier.Visible = false;
        lnkBackToSpplier.Visible = true;
        imgBtnAddSupplierProducts.Visible = false;
        mvTab1.SetActiveView(vAddSupplier);

        trAddContactPeople.Visible = false;

        txtSupplierBrand.Text = "";
        rdTelephone.Checked = true;
        rdContactEmail.Checked = false;
        rdFax.Checked = false;
        LoadDefault();
    }
    protected void imgBtnSaveSupplier_Click(object sender, ImageClickEventArgs e)
    {
        TransactionMgr tx = TransactionMgr.ThreadTransactionMgr();
        try
        {
            tx.BeginTransaction();

            tblSupplierEmails supplierEmail = new tblSupplierEmails();
            tblSupplierPhone supplierPhone = new tblSupplierPhone();
            supplierPhone.FlushData();
            supplierPhone.CheckSupplierPhone(txtAddMobileSupplier.Text);


            supplierEmail.FlushData();
            supplierEmail.CheckSupplierEmail(txtAddEmailSupplier.Text);

            tblSupplierFaxes supplierFax = new tblSupplierFaxes();
            supplierFax.FlushData();
            supplierFax.CheckSupplierFax(txtAddFaxSupplier.Text);

            if (supplierEmail.RowCount > 0)
            {
                string message = "Email Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);

            }
            else if (supplierPhone.RowCount > 0)
            {
                string message = "Phone Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else if (supplierFax.RowCount > 0)
            {
                string message = "Fax Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                tblSupplier supplier = new tblSupplier();
                supplier.AddNew();
                supplier.SBrandName = txtAddSupplierName.Text;
                supplier.SContactMethod_Email = rdAddEmail.Checked;
                supplier.SContactMethod_Fax = rdAddFax.Checked;
                supplier.SContactMethod_Phone = rdAddPhone.Checked;
                supplier.Website = txtWebSite.Text;
                supplier.SocialProfile1 = txtSocialProfile1.Text;
                supplier.SocialProfile2 = txtSocialProfile2.Text;
                supplier.DModifiedDate = DateTime.Now;
                supplier.DCreatedDate = DateTime.Now;
                supplier.Save();


                #region Adding Supplier Email
                if (txtAddEmailSupplier.Text != "")
                {
                    bool active = false;

                    supplierEmail.FlushData();
                    if (!supplierEmail.GetFirst(supplier.PkSupplierID))
                        active = true;

                    supplierEmail.FlushData();
                    supplierEmail.AddNew();
                    supplierEmail.SEmail = txtAddEmailSupplier.Text;
                    supplierEmail.FkSupplierID = supplier.PkSupplierID;
                    supplierEmail.BIsPrimary = active;
                    supplierEmail.DModifiedDate = DateTime.Now;
                    supplierEmail.DCreatedDate = DateTime.Now;
                    supplierEmail.Save();
                }

                #endregion

                #region Adding Supplier Department
                tblDepartmentSuppliers dSuppliers = new tblDepartmentSuppliers();
                dSuppliers.AddNew();
                dSuppliers.FkSupplierID = supplier.PkSupplierID;
                dSuppliers.FkDepartmentID = DepartmentID;
                dSuppliers.DModifiedDate = DateTime.Now;
                dSuppliers.DCreatedDate = DateTime.Now;
                dSuppliers.Save();
                #endregion

                #region Adding Supplier Phones
                if (txtAddMobileSupplier.Text != "")
                {
                    bool active2 = false;
                    supplierPhone.FlushData();
                    if (!supplierPhone.GetFirst(supplier.PkSupplierID))
                        active2 = true;

                    supplierPhone.FlushData();

                    supplierPhone.FlushData();
                    supplierPhone.AddNew();
                    supplierPhone.Phone = txtAddMobileSupplier.Text;
                    supplierPhone.FkSupplierID = supplier.PkSupplierID;
                    supplierPhone.BIsPrimary = active2;
                    supplierPhone.DModifiedDate = DateTime.Now;
                    supplierPhone.DCreatedDate = DateTime.Now;
                    supplierPhone.Save();
                }

                #endregion

                #region Adding Supplier Faxes
                if (txtAddFaxSupplier.Text != "")
                {
                    bool active3 = false;
                    if (!supplierFax.GetFirst(supplier.PkSupplierID))
                        active3 = true;

                    supplierFax.FlushData();
                    supplierFax.AddNew();
                    supplierFax.SFax = txtAddFaxSupplier.Text;
                    supplierFax.FkSupplierID = supplier.PkSupplierID;
                    supplierFax.BIsPrimary = active3;
                    supplierFax.DModifiedDate = DateTime.Now;
                    supplierFax.DCreatedDate = DateTime.Now;
                    supplierFax.Save();
                }
                #endregion

                #region Adding Supplier Address
                if (txtAddAddressSuppplier.Text != "" ||
                    txtAddTownSupplier.Text != "" ||
                    txtAddPostCodeSupplier.Text != "" ||
                    txtAddRegionSupplier.Text != ""
                    )
                {


                    bool active4 = false;
                    tblSupplierAddresses supAddress = new tblSupplierAddresses();
                    if (!supAddress.GetFirst(supplier.PkSupplierID))
                        active4 = true;
                    supAddress.FlushData();
                    supAddress.AddNew();
                    supAddress.SAddressStreet = txtAddAddressSuppplier.Text;
                    supAddress.SAddressTown = txtAddTownSupplier.Text;
                    supAddress.SAddressPostCode = txtAddPostCodeSupplier.Text;
                    supAddress.SAddressRegion = txtAddRegionSupplier.Text;
                    supAddress.FkAddressCountry = Convert.ToInt32(ddlAddCountries.SelectedValue);
                    supAddress.BIsPrimary = active4;
                    supAddress.FkSupplierID = supplier.PkSupplierID;
                    supAddress.DModifiedDate = DateTime.Now;
                    supAddress.DCreatedDatae = DateTime.Now;
                    supAddress.Save();
                }
                #endregion

                if (fpNewFileUploadSupplier.PostedFile.FileName != "")
                {
                    string strLocation = string.Empty;
                    strLocation = Server.MapPath("../SupplierImages/" + supplier.PkSupplierID + "/");
                    if (System.IO.Directory.Exists(strLocation) == false)
                    { System.IO.Directory.CreateDirectory(strLocation); }

                    string strFilename = fpNewFileUploadSupplier.FileName;//.PostedFile.FileName;
                    string strExtension = string.Empty;

                    strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));

                    if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
                    {
                        commonMethods.GiveAccessRights(strLocation);
                        if (File.Exists(Server.MapPath("../SupplierImages/" + supplier.PkSupplierID + "/" + strFilename)))
                        {
                            File.Delete(Server.MapPath("../SupplierImages/" + supplier.PkSupplierID + "/" + strFilename));
                        }
                        fpNewFileUploadSupplier.PostedFile.SaveAs(strLocation + strFilename);

                        supplier.SLogo = "../SupplierImages/" + supplier.PkSupplierID + "/" + strFilename;
                        supplier.DModifiedDate = DateTime.Now;
                        supplier.Save();
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saveMessage", "$(function(){ SupplierSaved();});", true);
                imgBtnAddSupplier.Visible = true;
                lnkBackToSpplier.Visible = false;
                LoadAllSuppliers();
                mvTab1.SetActiveView(vGrdSuppliers);
            }
            tx.CommitTransaction();
            LoadProductSuppliers();
            LoadEditProductSuppliers();
        }
        catch (Exception ex)
        {
            tx.RollbackTransaction();
            TransactionMgr.ThreadTransactionMgrReset();
        }
    }
    protected void imgBtnImageUploadTop_Click(object sender, ImageClickEventArgs e)
    {
        SavePhoto();
    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (txtSupplierBrand.Text != "")
        {
            if (Convert.ToInt32(ViewState["Supplierid"]) == 0)
            {
                //tblSupplier supp = new tblSupplier();
                //supp.AddNew();
                //supp.SBrandName = txtSupplierBrand.Text;
                //supp.SContactMethod_Email = rdContactEmail.Checked;
                //supp.SContactMethod_Phone = rdTelephone.Checked;
                //supp.SContactMethod_Fax = rdFax.Checked;
                //supp.DModifiedDate = DateTime.Now;
                //supp.DCreatedDate = DateTime.Now;
                //supp.Save();

                //tblDepartmentSuppliers dSuppliers = new tblDepartmentSuppliers();
                //dSuppliers.AddNew();
                //dSuppliers.FkSupplierID = supp.PkSupplierID;
                //dSuppliers.FkDepartmentID = DepartmentID;
                //dSuppliers.DModifiedDate = DateTime.Now;
                //dSuppliers.DCreatedDate = DateTime.Now;
                //dSuppliers.Save();


                //SupplierID = supp.PkSupplierID;
                //SavePhoto();

                //LoadSuppliersEmails();
                //LoadSuppliersPhones();
                //LoadSuppliersFaxes();
                //LoadSuppliersAddresses();
                //LoadAllSuppliers();



                // ApplyFloatingJquery();



            }
            else
            {
                tblSupplier supp = new tblSupplier();
                supp.LoadByPrimaryKey(Convert.ToInt32(ViewState["Supplierid"]));
                if (supp.RowCount > 0)
                {
                    supp.SBrandName = txtSupplierBrand.Text;
                    supp.SContactMethod_Email = rdContactEmail.Checked;
                    supp.SContactMethod_Phone = rdTelephone.Checked;
                    supp.SContactMethod_Fax = rdFax.Checked;
                    supp.Website = txtSupplierWebsite.Text;
                    supp.SocialProfile1 = txtSupplierSocialProfile1.Text;
                    supp.SocialProfile2 = txtSupplierSocialProfile2.Text;
                    supp.DModifiedDate = DateTime.Now;
                    supp.Save();

                    LoadAllSuppliers();

                    lblAddSupplierMessage.Text = "Successfully Updated!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saveMessage", "$(function(){ SupplierSaved(); });", true);
                    LoadProductSuppliers();
                    LoadEditProductSuppliers();

                }
            }
        }
    }
    private void ApplyFloatingJquery()
    {
        scrollingDiv.Style.Add("position", "absolute");
        scrollingDiv.Style.Add("top", "74%");
        scrollingDiv.Style.Add("-moz-box-shadow", "inset 0 0 10px #000000");
        scrollingDiv.Style.Add("-webkit-box-shadow", "inset 0 0 10px #000000");
        scrollingDiv.Style.Add("box-shadow", "inset 0 0 10px #000000");

        string scriptBlock = " $(function() {";
        scriptBlock += " var $scrollingDiv = $('#ctl00_ContentPlaceHolder1_scrollingDiv');";
        scriptBlock += " var scrollBottom = $scrollingDiv.height() - 300; ";
        scriptBlock += "$(window).scroll(function() {";
        scriptBlock += "$scrollingDiv";
        scriptBlock += ".stop()";
        scriptBlock += ".animate({ 'marginTop': ($(window).scrollTop() - 70) + 'px' }, 'slow');";
        scriptBlock += "}); });";
        //parseInt($scrollingDiv.height())
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jq", scriptBlock, true);
    }
    private void stopFloatingJquery()
    {
        scrollingDiv.Style.Add("position", "relative");
        scrollingDiv.Style.Add("top", "");
        scrollingDiv.Style.Add("-moz-box-shadow", "");
        scrollingDiv.Style.Add("-webkit-box-shadow", "");
        scrollingDiv.Style.Add("box-shadow", "");

        string scriptBlock = " $(function() {";
        scriptBlock += " var $scrollingDiv = $('#ctl00_ContentPlaceHolder1_scrollingDiv');";
        scriptBlock += " var scrollBottom = $scrollingDiv.height() - 300; ";
        scriptBlock += "$(window).scroll(function() {";
        scriptBlock += "$scrollingDiv";
        scriptBlock += ".stop()";
        // scriptBlock += ".animate({ 'marginTop': ($(window).scrollTop() - 150) + 'px' }, 'slow');";
        scriptBlock += "}); });";
        //parseInt($scrollingDiv.height())
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jq", scriptBlock, true);
    }
    private void LoadCountries()
    {
        tblCountries country = new tblCountries();
        country.GetAllCountriesAlphabetically();
        commonMethods.FillDropDownList(ddlCountry, country.DefaultView, "sCountry", "pkCountryID");
        commonMethods.FillDropDownList(ddlAddCountries, country.DefaultView, "sCountry", "pkCountryID");
    }
    private void SavePhoto()
    {
        try
        {
            tblSupplier s = new tblSupplier();
            s.LoadByPrimaryKey(Convert.ToInt32(ViewState["Supplierid"]));
            if (fpUploadPic.PostedFile.FileName != "")
            {
                string strLocation = string.Empty;
                strLocation = Server.MapPath("../SupplierImages/" + s.PkSupplierID + "/");
                if (System.IO.Directory.Exists(strLocation) == false)
                { System.IO.Directory.CreateDirectory(strLocation); }

                string strFilename = fpUploadPic.FileName;//.PostedFile.FileName;
                string strExtension = string.Empty;

                strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));

                if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
                {
                    commonMethods.GiveAccessRights(strLocation);
                    if (File.Exists(Server.MapPath("../SupplierImages/" + s.PkSupplierID + "/" + strFilename)))
                    {
                        File.Delete(Server.MapPath("../SupplierImages/" + s.PkSupplierID + "/" + strFilename));
                    }
                    fpUploadPic.PostedFile.SaveAs(strLocation + strFilename);

                    s.SLogo = "../SupplierImages/" + s.PkSupplierID + "/" + strFilename;
                    s.DModifiedDate = DateTime.Now;
                    s.Save();

                    supplierImage.Src = s.SLogo;
                    lblAddSupplierMessage.Text = "Successfully Updated";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saveMessage", "$(function(){ SupplierSaved(); });", true);

                }
            }

        }
        catch (Exception ex)
        {
        }

    }

    protected void lnkBackToSpplier_Click1(object sender, EventArgs e)
    {
        mvTab1.SetActiveView(vGrdSuppliers);
        lnkBackToSpplier.Visible = false;
        imgBtnAddSupplier.Visible = true;
        lblAddSupplierMessage.Visible = false;
        imgBtnAddSupplierProducts.Visible = true;
        Response.Redirect("ManageSupplier.aspx");
    }

    protected void btnSupplierEmail_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(ViewState["Supplierid"]) != 0)
        {
            bool active = false;
            tblSupplierEmails supplierEmail = new tblSupplierEmails();
            if (!supplierEmail.GetFirst(Convert.ToInt32(ViewState["Supplierid"])))
                active = true;

            supplierEmail.FlushData();
            supplierEmail.CheckSupplierEmail(txtSupplierEmail.Text);
            if (supplierEmail.RowCount > 0)
            {
                string message = "Email Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                if (ViewState["supplierEmail"] != null)
                {
                    supplierEmail.FlushData();
                    supplierEmail.LoadByPrimaryKey(Convert.ToInt32(ViewState["supplierEmail"]));
                    if (supplierEmail.RowCount > 0)
                    {
                        supplierEmail.SEmail = txtSupplierEmail.Text;
                        supplierEmail.DModifiedDate = DateTime.Now;
                        supplierEmail.Save();
                        txtSupplierEmail.Text = "";
                        ViewState["supplierEmail"] = null;
                        LoadSuppliersEmails();
                    }
                }
                else
                {
                    supplierEmail.FlushData();
                    supplierEmail.AddNew();
                    supplierEmail.SEmail = txtSupplierEmail.Text;
                    supplierEmail.FkSupplierID = Convert.ToInt32(ViewState["Supplierid"]);
                    supplierEmail.BIsPrimary = active;
                    supplierEmail.DModifiedDate = DateTime.Now;
                    supplierEmail.DCreatedDate = DateTime.Now;
                    supplierEmail.Save();
                    txtSupplierEmail.Text = "";
                    LoadSuppliersEmails();
                }
                btnSupplierEmail.ImageUrl = "../images/btn_addanotheremail.gif";
            }

        }
    }
    protected void btnSupplierMobile_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(ViewState["Supplierid"]) != 0)
        {
            bool active = false;
            tblSupplierPhone supplierPhone = new tblSupplierPhone();
            if (!supplierPhone.GetFirst(Convert.ToInt32(ViewState["Supplierid"])))
                active = true;

            supplierPhone.FlushData();
            supplierPhone.CheckSupplierPhone(txtSupplierMobile.Text);
            if (supplierPhone.RowCount > 0)
            {
                string message = "Phone Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                if (ViewState["supplierPhone"] != null)
                {
                    supplierPhone.FlushData();
                    supplierPhone.LoadByPrimaryKey(Convert.ToInt32(ViewState["supplierPhone"]));
                    if (supplierPhone.RowCount > 0)
                    {
                        supplierPhone.Phone = txtSupplierMobile.Text;
                        supplierPhone.DModifiedDate = DateTime.Now;
                        supplierPhone.Save();
                        txtSupplierMobile.Text = "";
                        LoadSuppliersPhones();
                        ViewState["supplierPhone"] = null;
                    }
                }
                else
                {

                    supplierPhone.FlushData();
                    supplierPhone.AddNew();
                    supplierPhone.Phone = txtSupplierMobile.Text;
                    supplierPhone.FkSupplierID = Convert.ToInt32(ViewState["Supplierid"]);
                    supplierPhone.BIsPrimary = active;
                    supplierPhone.DModifiedDate = DateTime.Now;
                    supplierPhone.DCreatedDate = DateTime.Now;
                    supplierPhone.Save();
                    txtSupplierMobile.Text = "";
                    LoadSuppliersPhones();

                }
                btnSupplierMobile.ImageUrl = "../images/btn_anotherphone.gif";
            }
        }
    }
    protected void btnSupplierfax_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(ViewState["Supplierid"]) != 0)
        {
            bool active = false;
            tblSupplierFaxes supplierFax = new tblSupplierFaxes();
            if (!supplierFax.GetFirst(Convert.ToInt32(ViewState["Supplierid"])))
                active = true;

            supplierFax.FlushData();
            supplierFax.CheckSupplierFax(txtSupplierFax.Text);
            if (supplierFax.RowCount > 0)
            {
                string message = "Phone Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                if (ViewState["FaxID"] != null)
                {
                    supplierFax.FlushData();
                    supplierFax.LoadByPrimaryKey(Convert.ToInt32(ViewState["FaxID"]));
                    if (supplierFax.RowCount > 0)
                    {
                        supplierFax.SFax = txtSupplierFax.Text;
                        supplierFax.FaxNote = txtSupplierFaxNote.Text;
                        supplierFax.DModifiedDate = DateTime.Now;
                        supplierFax.Save();
                        ViewState["FaxID"] = null;
                        txtSupplierFax.Text = "";
                        txtSupplierFaxNote.Text = "";
                        LoadSuppliersFaxes();
                    }

                }
                else
                {
                    supplierFax.FlushData();
                    supplierFax.AddNew();
                    supplierFax.SFax = txtSupplierFax.Text;
                    supplierFax.FkSupplierID = Convert.ToInt32(ViewState["Supplierid"]);
                    supplierFax.BIsPrimary = active;
                    supplierFax.FaxNote = txtSupplierFaxNote.Text;
                    supplierFax.DModifiedDate = DateTime.Now;
                    supplierFax.DCreatedDate = DateTime.Now;
                    supplierFax.Save();
                    txtSupplierFax.Text = "";
                    txtSupplierFaxNote.Text = "";
                    LoadSuppliersFaxes();
                }
                btnSupplierfax.ImageUrl = "../images/btn_addanother.png";
            }
        }
    }
    protected void btnAddress_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(ViewState["Supplierid"]) != 0)
        {
            bool active = false;
            tblSupplierAddresses supAddress = new tblSupplierAddresses();
            if (!supAddress.GetFirst(Convert.ToInt32(ViewState["Supplierid"])))
                active = true;

            if (ViewState["supplierAddress"] != null)
            {
                supAddress.FlushData();
                supAddress.LoadByPrimaryKey(Convert.ToInt32(ViewState["supplierAddress"]));
                if (supAddress.RowCount > 0)
                {
                    supAddress.SAddressStreet = txtAddress.Text;
                    supAddress.SAddressTown = txtTown.Text;
                    supAddress.SAddressPostCode = txtPostCode.Text;
                    supAddress.SAddressRegion = txtRegion.Text;
                    supAddress.FkAddressCountry = Convert.ToInt32(ddlCountry.SelectedValue);
                    supAddress.DModifiedDate = DateTime.Now;
                    supAddress.Save();
                }
            }
            else
            {
                supAddress.FlushData();
                supAddress.AddNew();
                supAddress.SAddressStreet = txtAddress.Text;
                supAddress.SAddressTown = txtTown.Text;
                supAddress.SAddressPostCode = txtPostCode.Text;
                supAddress.SAddressRegion = txtRegion.Text;
                supAddress.FkAddressCountry = Convert.ToInt32(ddlCountry.SelectedValue);
                supAddress.BIsPrimary = active;
                supAddress.FkSupplierID = Convert.ToInt32(ViewState["Supplierid"]);
                supAddress.DModifiedDate = DateTime.Now;
                supAddress.DCreatedDatae = DateTime.Now;
                supAddress.Save();
            }
            txtAddress.Text = "";
            txtTown.Text = "";
            txtPostCode.Text = "";
            txtRegion.Text = "";
            rdTelephone.Checked = true;
            btnAddress.ImageUrl = "../images/btn_addanotheraddress.gif";
            LoadSuppliersAddresses();
            LoadCountries();
        }

    }
    protected void grdSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = 0;
            imgBtnAddSupplierProducts.Visible = false;
            try
            {
                id = Convert.ToInt32(e.CommandArgument);
            }
            catch (FormatException ex)
            { }
            tblSupplier supplierUpdate = new tblSupplier();
            switch (e.CommandName.ToLower())
            {
                case "name":
                    supplierUpdate.FlushData();
                    supplierUpdate.LoadByPrimaryKey(id);
                    if (supplierUpdate.RowCount > 0)
                    {
                        ViewState["Supplierid"] = supplierUpdate.PkSupplierID;

                        //SupplierID = supplierUpdate.PkSupplierID;
                        txtSupplierBrand.Text = supplierUpdate.SBrandName;
                        supplierImage.Src = supplierUpdate.SLogo;
                        rdContactEmail.Checked = supplierUpdate.SContactMethod_Email;
                        rdTelephone.Checked = supplierUpdate.SContactMethod_Phone;
                        rdFax.Checked = supplierUpdate.SContactMethod_Fax;
                        txtSupplierWebsite.Text = supplierUpdate.Website;
                        txtSupplierSocialProfile1.Text = supplierUpdate.SocialProfile1;
                        txtSupplierSocialProfile2.Text = supplierUpdate.SocialProfile2;
                        LoadSuppliersEmails();
                        LoadSuppliersPhones();
                        LoadSuppliersFaxes();
                        LoadSuppliersAddresses();
                        LoadDefault();


                        tblCompanies company = new tblCompanies();
                        company.GetCompanies(Convert.ToInt32(ViewState["Supplierid"]));
                        if (company.RowCount > 0)
                        {
                            CompanyID = company.PkCompanyID;
                            txtCompanyBrand.Text = company.CBrandName;
                            imgCompanyLogo.Src = company.Logo;
                            rdCompanyEmail.Checked = company.CContactMethod_Email;
                            rdCompanyTelephone.Checked = company.CContactMethod_Phone;
                            rdCompanyFax.Checked = company.CContactMethod_Fax;
                            ddlSuppliers.SelectedValue = company.FkSuplierID.ToString();

                            // LoadCompanyEmails();
                            // LoadCompanyPhones();
                            // LoadCompanyFaxes();
                            //  LoadCompanyAddresses();
                            // LoadDefaultCompany();

                            ShowTRc();
                        }
                        else
                        {
                            CompanyID = 0;
                            txtCompanyBrand.Text = "";
                            rdCompanyTelephone.Checked = true;
                            imgCompanyLogo.Src = "../images/no_image.gif";
                        }


                        loadSupplierProducts(Convert.ToInt32(ViewState["Supplierid"]));
                        LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));

                        mvTab1.SetActiveView(vEditSupplier);
                        imgBtnAddSupplier.Visible = false;
                        lnkBackToSpplier.Visible = true;

                    }
                    break;

                case "email":
                    supplierUpdate.FlushData();
                    supplierUpdate.LoadByPrimaryKey(id);
                    {
                        tblSupplierEmails emails = new tblSupplierEmails();
                        emails.GetActiveEmail(supplierUpdate.PkSupplierID);
                        if (emails.RowCount > 0)
                        {
                            receiverEmail = emails.SEmail;
                        }

                        lblToAddress.Text = supplierUpdate.SBrandName;
                        tblUsers u = new tblUsers();
                        u.LoadByPrimaryKey(UserID);
                        if (u.RowCount > 0)
                        {
                            lblFromAddress.Text = u.SFirstName + " " + u.SLastName;
                            tblUserEmails ue = new tblUserEmails();
                            ue.LoadUserEmailsActive(u.PkUserID);
                            if (ue.RowCount > 0)
                            {
                                senderEmail = ue.SEmail;
                            }
                            ModalPopupExtender1.Show();
                        }

                    }
                    break;

                case "active":
                    supplierUpdate.FlushData();
                    supplierUpdate.LoadByPrimaryKey(id);
                    if (supplierUpdate.RowCount > 0)
                    {
                        try
                        {
                            if (supplierUpdate.IsActive.ToString() != "")
                            {
                                if (supplierUpdate.IsActive)
                                    supplierUpdate.IsActive = false;
                                else if (!supplierUpdate.IsActive)
                                    supplierUpdate.IsActive = true;
                            }
                            else
                            {
                                supplierUpdate.IsActive = false;
                            }
                        }
                        catch (InvalidCastException ex)
                        {
                            supplierUpdate.IsActive = false;
                        }
                        supplierUpdate.DModifiedDate = DateTime.Now;
                        supplierUpdate.Save();
                        LoadAllSuppliers();
                    }

                    break;

                case "sort":
                    tblSupplier supplier = new tblSupplier();
                    if (sort == SortDirection.Ascending.ToString())
                    {

                        supplier.GetDepartmentSuppliersByOrder(DepartmentID, "asc");
                        sort = SortDirection.Descending.ToString();
                    }
                    else if (sort == SortDirection.Descending.ToString())
                    {
                        supplier.GetDepartmentSuppliersByOrder(DepartmentID, "desc");
                        sort = SortDirection.Ascending.ToString();
                    }


                    grdSuppliers.DataSource = supplier.DefaultView;
                    grdSuppliers.DataBind();

                    break;
            }
        }
    }
    protected void grdSuppliers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;


                HtmlContainerControl divAmount = e.Row.FindControl("divAmount") as HtmlContainerControl;
                LinkButton lnkAmount = e.Row.FindControl("lnkAmount") as LinkButton;
                try
                {
                    if (Convert.ToDouble(dr["amount"].ToString()) > 0)
                    {
                        divAmount.Style.Add("background", "url(../images/textbox_pink.png) no-repeat;");
                        lnkAmount.Style.Add("color", "#cf2116");
                        lnkAmount.Style.Add("line-height", "29px;");
                        divAmount.Style.Add("font-weight", "bold");
                        lnkAmount.Text = commonMethods.ChangetToUK(lnkAmount.Text) + " €";

                    }
                    else if (Convert.ToDouble(dr["amount"].ToString()) < 0)
                    {
                        lnkAmount.Text = lnkAmount.Text.Replace("-", "");
                        divAmount.Style.Add("background", "url(../images/textbox_light_green.png) no-repeat;");
                        lnkAmount.Style.Add("color", "#38761d");
                        lnkAmount.Style.Add("line-height", "29px;");
                        divAmount.Style.Add("font-weight", "bold");
                        lnkAmount.Text = commonMethods.ChangetToUK(lnkAmount.Text) + " €";
                    }
                    else
                    {
                        divAmount.Visible = false;
                    }
                }
                catch (FormatException ex)
                {
                    divAmount.Visible = false;
                }

                ImageButton imgBtnActive = e.Row.FindControl("imgBtnActive") as ImageButton;
                if (dr["isActive"].ToString() != "")
                {
                    if (Convert.ToBoolean(dr["isActive"].ToString()))
                        imgBtnActive.ImageUrl = "../Images/activate_icon.gif";
                    else if (!Convert.ToBoolean(dr["isActive"].ToString()))
                        imgBtnActive.ImageUrl = "../Images/close.png";
                }
                else
                {
                    imgBtnActive.ImageUrl = "../Images/close.png";
                }

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void grdSuppliers_Sorting(object sender, GridViewSortEventArgs e)
    {
        tblSupplier supplier = new tblSupplier();
        if (sort == SortDirection.Ascending.ToString())
        {

            supplier.GetDepartmentSuppliersByOrder(DepartmentID, "asc");
            sort = SortDirection.Descending.ToString();
        }
        else if (sort == SortDirection.Descending.ToString())
        {
            supplier.GetDepartmentSuppliersByOrder(DepartmentID, "desc");
            sort = SortDirection.Ascending.ToString();
        }


        grdSuppliers.DataSource = supplier.DefaultView;
        grdSuppliers.DataBind();

    }
    protected void grdSuppliers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSuppliers.PageIndex = e.NewPageIndex;
        tblSupplier supplier = new tblSupplier();
        if (sort == SortDirection.Ascending.ToString())
        {
            supplier.GetDepartmentSuppliersByOrder(DepartmentID, "desc");

        }
        else if (sort == SortDirection.Descending.ToString())
        {
            supplier.GetDepartmentSuppliersByOrder(DepartmentID, "asc");

        }
        grdSuppliers.DataSource = supplier.DefaultView;
        grdSuppliers.DataBind();
    }

    protected void grdEmails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblSupplierEmails supplierEmails = new tblSupplierEmails();

            switch (e.CommandName.ToLower())
            {
                case "active":

                    supplierEmails.GetAllExceptOne(id);
                    if (supplierEmails.RowCount > 0)
                    {
                        for (int i = 0; i < supplierEmails.RowCount; i++)
                        {
                            supplierEmails.BIsPrimary = false;
                            supplierEmails.Save();
                            supplierEmails.MoveNext();
                        }
                    }
                    supplierEmails.FlushData();
                    supplierEmails.LoadByPrimaryKey(id);
                    if (supplierEmails.RowCount > 0)
                    {
                        supplierEmails.BIsPrimary = true;
                        supplierEmails.DModifiedDate = DateTime.Now;
                        supplierEmails.Save();
                        LoadSuppliersEmails();
                    }
                    break;

                case "del":
                    supplierEmails.FlushData();
                    supplierEmails.LoadByPrimaryKey(id);
                    if (supplierEmails.RowCount > 0)
                    {
                        supplierEmails.MarkAsDeleted();
                        supplierEmails.Save();
                    }
                    LoadSuppliersEmails();
                    break;
                case "edt":
                    supplierEmails.FlushData();
                    supplierEmails.LoadByPrimaryKey(id);
                    if (supplierEmails.RowCount > 0)
                    {
                        txtSupplierEmail.Text = supplierEmails.SEmail;
                        ViewState["supplierEmail"] = supplierEmails.PkSupplierEmails;
                        btnSupplierEmail.ImageUrl = "../Images/btn_edit_admin.png";
                    }
                    LoadSuppliersEmails();
                    break;
            }
        }
    }
    protected void grdEmails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgbtnSetActiveEmail = e.Row.FindControl("imgbtnSetActiveEmail") as ImageButton;

            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgbtnSetActiveEmail.ImageUrl = "~/Images/Star_Black.png";
                imgbtnSetActiveEmail.Width = 16;
                imgbtnSetActiveEmail.ToolTip = "Mark as favorite";
                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;
            }
        }
    }



    protected void grdMobile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgBtnActiveMobile = e.Row.FindControl("imgBtnActiveMobile") as ImageButton;

            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgBtnActiveMobile.ImageUrl = "~/Images/Star_Black.png";
                imgBtnActiveMobile.Width = 16;
                imgBtnActiveMobile.ToolTip = "Mark as favorite";
                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;
            }
        }
    }
    protected void grdMobile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblSupplierPhone supplierPhone = new tblSupplierPhone();

            switch (e.CommandName.ToLower())
            {
                case "active":

                    supplierPhone.GetAllExceptOne(id);
                    if (supplierPhone.RowCount > 0)
                    {
                        for (int i = 0; i < supplierPhone.RowCount; i++)
                        {
                            supplierPhone.BIsPrimary = false;
                            supplierPhone.Save();
                            supplierPhone.MoveNext();
                        }
                    }

                    supplierPhone.FlushData();
                    supplierPhone.LoadByPrimaryKey(id);
                    if (supplierPhone.RowCount > 0)
                    {
                        supplierPhone.BIsPrimary = true;
                        supplierPhone.DModifiedDate = DateTime.Now;
                        supplierPhone.Save();
                        LoadSuppliersPhones();
                    }
                    break;

                case "del":
                    supplierPhone.FlushData();
                    supplierPhone.LoadByPrimaryKey(id);
                    if (supplierPhone.RowCount > 0)
                    {
                        supplierPhone.MarkAsDeleted();
                        supplierPhone.Save();
                    }
                    LoadSuppliersPhones();
                    break;

                case "edt":
                    supplierPhone.FlushData();
                    supplierPhone.LoadByPrimaryKey(id);
                    if (supplierPhone.RowCount > 0)
                    {
                        txtSupplierMobile.Text = supplierPhone.Phone;
                        ViewState["supplierPhone"] = supplierPhone.PkSupplierPhoneID;
                        btnSupplierMobile.ImageUrl = "../Images/btn_edit_admin.png";
                    }
                    break;
            }
        }
    }
    protected void grdFax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgBtnActiveFaxes = e.Row.FindControl("imgBtnActiveFaxes") as ImageButton;

            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgBtnActiveFaxes.ImageUrl = "~/Images/Star_Black.png";
                imgBtnActiveFaxes.Width = 16;
                imgBtnActiveFaxes.ToolTip = "Mark as favorite";

                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;

            }
        }
    }
    protected void grdFax_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblSupplierFaxes supplierFaxes = new tblSupplierFaxes();

            switch (e.CommandName.ToLower())
            {
                case "active":

                    supplierFaxes.GetAllExceptOne(id);
                    if (supplierFaxes.RowCount > 0)
                    {
                        for (int i = 0; i < supplierFaxes.RowCount; i++)
                        {
                            supplierFaxes.BIsPrimary = false;
                            supplierFaxes.Save();
                            supplierFaxes.MoveNext();
                        }
                    }

                    supplierFaxes.FlushData();
                    supplierFaxes.LoadByPrimaryKey(id);
                    if (supplierFaxes.RowCount > 0)
                    {
                        supplierFaxes.BIsPrimary = true;
                        supplierFaxes.DModifiedDate = DateTime.Now;
                        supplierFaxes.Save();
                        LoadSuppliersFaxes();
                    }
                    break;
                case "del":
                    supplierFaxes.FlushData();
                    supplierFaxes.LoadByPrimaryKey(id);
                    if (supplierFaxes.RowCount > 0)
                    {
                        supplierFaxes.MarkAsDeleted();
                        supplierFaxes.Save();
                    }
                    LoadSuppliersFaxes();
                    break;
                case "edt":
                    supplierFaxes.FlushData();
                    supplierFaxes.LoadByPrimaryKey(id);
                    if (supplierFaxes.RowCount > 0)
                    {
                        txtSupplierFax.Text = supplierFaxes.SFax;
                        txtSupplierFaxNote.Text = supplierFaxes.FaxNote;
                        ViewState["FaxID"] = supplierFaxes.PkSupplierFaxID;
                        btnSupplierfax.ImageUrl = "../images/btn_edit_admin.png";
                    }
                    break;

            }
        }
    }
    protected void grdAddress_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblSupplierAddresses supplierAddress = new tblSupplierAddresses();

            switch (e.CommandName.ToLower())
            {
                case "active":

                    supplierAddress.GetAllExceptOne(id);
                    if (supplierAddress.RowCount > 0)
                    {
                        for (int i = 0; i < supplierAddress.RowCount; i++)
                        {
                            supplierAddress.BIsPrimary = false;
                            supplierAddress.Save();
                            supplierAddress.MoveNext();
                        }
                    }

                    supplierAddress.FlushData();
                    supplierAddress.LoadByPrimaryKey(id);
                    if (supplierAddress.RowCount > 0)
                    {
                        supplierAddress.BIsPrimary = true;
                        supplierAddress.DModifiedDate = DateTime.Now;
                        supplierAddress.Save();
                        LoadSuppliersAddresses();
                    }
                    break;
                case "del":
                    supplierAddress.FlushData();
                    supplierAddress.LoadByPrimaryKey(id);
                    if (supplierAddress.RowCount > 0)
                    {
                        supplierAddress.MarkAsDeleted();
                        supplierAddress.Save();
                    }
                    LoadSuppliersAddresses();
                    break;

                case "edt":
                    supplierAddress.FlushData();
                    supplierAddress.LoadByPrimaryKey(id);
                    if (supplierAddress.RowCount > 0)
                    {
                        txtAddress.Text = supplierAddress.SAddressStreet;
                        txtTown.Text = supplierAddress.SAddressTown;
                        txtPostCode.Text = supplierAddress.SAddressPostCode;
                        txtRegion.Text = supplierAddress.SAddressRegion;
                        txtRegion.Text = supplierAddress.SAddressRegion;
                        ddlCountry.SelectedValue = supplierAddress.FkAddressCountry.ToString();
                        ViewState["supplierAddress"] = supplierAddress.PkSupplierAddressID;

                        btnAddress.ImageUrl = "../images/btn_edit_admin.png";
                    }
                    break;
            }
        }
    }
    protected void grdAddress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            ImageButton imgBtnActiveAddress = e.Row.FindControl("imgBtnActiveAddress") as ImageButton;
            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgBtnActiveAddress.ImageUrl = "~/Images/Star_Black.png";
                imgBtnActiveAddress.Width = 16;
                imgBtnActiveAddress.ToolTip = "Mark as favorite";

                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;
            }
        }
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {

        stopFloatingJquery();
    }

    #endregion

    #region Company

    private void LoadAllCompanies()
    {
        tblCompanies companies = new tblCompanies();
        //companies.GetCompanies(Convert.ToInt32(ViewState["Supplierid"]));
        companies.LoadAll();
        grdCompanies.DataSource = companies.DefaultView;
        grdCompanies.DataBind();
    }
    private void LoadCompanyEmails()
    {
        tblCompanyEmails companyEmails = new tblCompanyEmails();
        companyEmails.GetCompanyEmails(CompanyID);
        grdEmailCompany.DataSource = companyEmails.DefaultView;
        grdEmailCompany.DataBind();
    }
    private void LoadCompanyPhones()
    {
        tblCompanyPhones companyPhones = new tblCompanyPhones();
        companyPhones.GetCompanyPhones(CompanyID);
        grdMobileCompany.DataSource = companyPhones.DefaultView;
        grdMobileCompany.DataBind();
    }
    private void LoadDefaultCompany()
    {
        txtCompanyEmail.Text = "";

        txtCompanyFax.Text = "";
        txtCompanyMobile.Text = "";
        txtAddressCompany.Text = "";
        txtTownCompany.Text = "";
        txtPostCodeCompany.Text = "";
        txtRegionCompany.Text = "";

        LoadCountriesCompany();
    }
    private void LoadCompanyFaxes()
    {
        tblCompanyFaxes companyFaxes = new tblCompanyFaxes();
        companyFaxes.GetCompanyFaxes(CompanyID);
        grdFaxCompany.DataSource = companyFaxes.DefaultView;
        grdFaxCompany.DataBind();
    }
    private void LoadCompanyAddresses()
    {
        tblCompanyAddresses companyAddresses = new tblCompanyAddresses();
        companyAddresses.GetCompanyAddresses(CompanyID);
        grdAddressCompany.DataSource = companyAddresses.DefaultView;
        grdAddressCompany.DataBind();
    }
    private void LoadCountriesCompany()
    {
        tblCountries country = new tblCountries();
        country.GetAllCountriesAlphabetically();
        commonMethods.FillDropDownList(ddlCountryCompany, country.DefaultView, "sCountry", "pkCountryID");
    }
    private void SavePhotoCompany()
    {
        try
        {

            tblCompanies c = new tblCompanies();
            c.LoadByPrimaryKey(CompanyID);

            if (fpCompanyPic.PostedFile.FileName != "")
            {
                string strLocation = string.Empty;
                strLocation = Server.MapPath("../CompanyImages/" + c.PkCompanyID + "/");
                if (System.IO.Directory.Exists(strLocation) == false)
                { System.IO.Directory.CreateDirectory(strLocation); }

                string strFilename = fpCompanyPic.FileName;//.PostedFile.FileName;
                string strExtension = string.Empty;

                strExtension = strFilename.Substring(strFilename.IndexOf("."), strFilename.Length - strFilename.IndexOf("."));

                if ((strExtension == ".jpg") || (strExtension == ".jpeg") || (strExtension == ".gif") || (strExtension == ".tif") || (strExtension == ".tiff") || (strExtension == ".png") || (strExtension == ".bmp"))
                {
                    commonMethods.GiveAccessRights(strLocation);
                    if (File.Exists(Server.MapPath("../CompanyImages/" + c.PkCompanyID + "/" + strFilename)))
                    {
                        File.Delete(Server.MapPath("../CompanyImages/" + c.PkCompanyID + "/" + strFilename));
                    }
                    fpCompanyPic.PostedFile.SaveAs(strLocation + strFilename);

                    c.Logo = "../CompanyImages/" + c.PkCompanyID + "/" + strFilename;
                    c.DModifiedDate = DateTime.Now;
                    c.Save();

                    imgCompanyLogo.Src = c.Logo;

                }
            }
        }
        catch (Exception ex)
        {
        }

    }
    private void ShowTRc()
    {
        trcEmail.Visible = true;
        trcEmailSplit.Visible = true;
        trcFax.Visible = true;
        trcFaxSplit.Visible = true;
        trcAddress.Visible = true;
    }
    private void HideTRc()
    {
        trcEmail.Visible = false;
        trcEmailSplit.Visible = false;
        trcFax.Visible = false;
        trcFaxSplit.Visible = false;
        trcAddress.Visible = false;
    }
    private void BindSuppliers()
    {
        tblSupplier suppliers = new tblSupplier();
        suppliers.GetDepartmentSuppliers(DepartmentID);
        if (suppliers.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlSuppliers, suppliers.DefaultView, "SBrandName", "PkSupplierID");
        }
    }
    protected void imgBtnAddCompany_Click(object sender, ImageClickEventArgs e)
    {

        CompanySupplierID = 0;
        CompanyID = 0;

        trAddContactPeople.Visible = false;


        txtCompanyBrand.Text = "";
        rdCompanyTelephone.Checked = true;
        rdCompanyEmail.Checked = false;
        rdCompanyFax.Checked = false;
        LoadDefaultCompany();
        BindSuppliers();
    }
    protected void imgBtnSaveCompany_Click(object sender, ImageClickEventArgs e)
    {
        if (txtCompanyBrand.Text != "")
        {
            if (CompanyID == 0)
            {
                tblCompanies comp = new tblCompanies();
                comp.AddNew();
                comp.CBrandName = txtCompanyBrand.Text;
                comp.CContactMethod_Email = rdContactEmail.Checked;
                comp.CContactMethod_Fax = rdCompanyFax.Checked;
                comp.CContactMethod_Phone = rdCompanyTelephone.Checked;
                comp.FkSuplierID = Convert.ToInt32(ViewState["Supplierid"]);
                comp.DModifiedDate = DateTime.Now;
                comp.DCreatedDate = DateTime.Now;
                comp.Save();


                CompanyID = comp.PkCompanyID;
                SavePhotoCompany();

                // LoadCompanyEmails();
                // LoadCompanyPhones();
                //  LoadCompanyFaxes();
                //  LoadCompanyAddresses();
                //  LoadAllCompanies();

                // ApplyFloatingJquery();
                ShowTRc();
            }
            else
            {
                tblCompanies comp = new tblCompanies();
                comp.LoadByPrimaryKey(CompanyID);
                if (comp.RowCount > 0)
                {
                    comp.CBrandName = txtCompanyBrand.Text;
                    comp.CContactMethod_Email = rdCompanyEmail.Checked;
                    comp.CContactMethod_Fax = rdCompanyFax.Checked;
                    comp.CContactMethod_Phone = rdCompanyTelephone.Checked;
                    comp.DModifiedDate = DateTime.Now;
                    comp.Save();
                    SavePhotoCompany();
                    // LoadAllCompanies();
                }
            }
        }
    }
    protected void btnCompanyEmail_Click(object sender, ImageClickEventArgs e)
    {
        if (CompanyID != 0)
        {
            bool active = false;
            tblCompanyEmails companyEmail = new tblCompanyEmails();
            if (!companyEmail.GetFirst())
                active = true;



            companyEmail.FlushData();
            companyEmail.CheckCompanyEmail(txtCompanyEmail.Text);
            if (companyEmail.RowCount > 0)
            {
                string message = "Email Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                companyEmail.FlushData();
                companyEmail.AddNew();
                companyEmail.CEmails = txtCompanyEmail.Text;
                companyEmail.FkCompanyID = CompanyID;
                companyEmail.BIsPrimary = active;
                companyEmail.DModifiedDate = DateTime.Now;
                companyEmail.DCreateedDate = DateTime.Now;
                companyEmail.Save();
                txtCompanyEmail.Text = "";
                LoadCompanyEmails();
            }

        }
    }
    protected void btnCompanyMobile_Click(object sender, ImageClickEventArgs e)
    {
        if (CompanyID != 0)
        {
            bool active = false;
            tblCompanyPhones companyPhones = new tblCompanyPhones();
            if (!companyPhones.GetFirst())
                active = true;

            companyPhones.FlushData();
            companyPhones.CheckCompanyPhone(txtCompanyMobile.Text);
            if (companyPhones.RowCount > 0)
            {
                string message = "Phone Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                companyPhones.FlushData();
                companyPhones.AddNew();
                companyPhones.CPhones = txtSupplierMobile.Text;
                companyPhones.FkCompanyID = CompanyID;
                companyPhones.BIsPrimary = active;
                companyPhones.DModifiedDate = DateTime.Now;
                companyPhones.DCreatedDate = DateTime.Now;
                companyPhones.Save();
                txtCompanyMobile.Text = "";
                LoadCompanyPhones();
            }
        }
    }
    protected void btnCompanyFax_Click(object sender, ImageClickEventArgs e)
    {
        if (CompanyID != 0)
        {
            bool active = false;
            tblCompanyFaxes companyFax = new tblCompanyFaxes();
            if (!companyFax.GetFirst())
                active = true;

            companyFax.FlushData();
            companyFax.CheckCompanyFax(txtCompanyFax.Text);
            if (companyFax.RowCount > 0)
            {
                string message = "Phone Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                companyFax.FlushData();
                companyFax.AddNew();
                companyFax.CFax = txtCompanyFax.Text;
                companyFax.FkCompanyID = CompanyID;
                companyFax.BIsPrimary = active;
                companyFax.DModifiedDate = DateTime.Now;
                companyFax.DCreatedDate = DateTime.Now;
                companyFax.Save();
                txtCompanyFax.Text = "";
                LoadCompanyFaxes();
            }
        }
    }
    protected void btnAddressCompany_Click(object sender, ImageClickEventArgs e)
    {
        if (CompanyID != 0)
        {
            bool active = false;
            tblCompanyAddresses compAddress = new tblCompanyAddresses();
            if (!compAddress.GetFirst())
                active = true;

            compAddress.FlushData();
            compAddress.AddNew();
            compAddress.SAddressStreet = txtAddress.Text;
            compAddress.SAddressTown = txtTown.Text;
            compAddress.SAddressPostCode = txtPostCode.Text;
            compAddress.SAddressRegion = txtRegion.Text;
            compAddress.FkAddressCountry = Convert.ToInt32(ddlCountryCompany.SelectedValue);
            compAddress.BIsPrimary = active;
            compAddress.FkCompanyId = CompanyID;
            compAddress.DModifiedDate = DateTime.Now;
            compAddress.DCreateDate = DateTime.Now;
            compAddress.Save();

            txtAddressCompany.Text = "";
            txtTownCompany.Text = "";
            txtPostCodeCompany.Text = "";
            txtRegionCompany.Text = "";
            rdCompanyTelephone.Checked = true;
            LoadCompanyAddresses();
        }
    }
    protected void grdEmailCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblCompanyEmails companyEmails = new tblCompanyEmails();


            switch (e.CommandName.ToLower())
            {
                case "active":

                    companyEmails.GetAllExceptOne(id);
                    if (companyEmails.RowCount > 0)
                    {
                        for (int i = 0; i < companyEmails.RowCount; i++)
                        {
                            companyEmails.BIsPrimary = false;
                            companyEmails.Save();
                            companyEmails.MoveNext();
                        }
                    }
                    companyEmails.FlushData();
                    companyEmails.LoadByPrimaryKey(id);
                    if (companyEmails.RowCount > 0)
                    {
                        companyEmails.BIsPrimary = true;
                        companyEmails.DModifiedDate = DateTime.Now;
                        companyEmails.Save();
                        LoadCompanyEmails();
                    }
                    break;

                case "del":
                    companyEmails.FlushData();
                    companyEmails.LoadByPrimaryKey(id);
                    if (companyEmails.RowCount > 0)
                    {
                        companyEmails.MarkAsDeleted();
                        companyEmails.Save();
                    }
                    LoadCompanyEmails();
                    break;
            }
        }
    }
    protected void grdEmailCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgbtnSetActiveEmail = e.Row.FindControl("imgbtnSetActiveEmail") as ImageButton;

            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgbtnSetActiveEmail.ImageUrl = "~/Images/Star_Black.png";
                imgbtnSetActiveEmail.Width = 16;
                imgbtnSetActiveEmail.ToolTip = "Active";
                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;
            }
        }
    }
    protected void grdMobileCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblCompanyPhones companyPhones = new tblCompanyPhones();


            switch (e.CommandName.ToLower())
            {
                case "active":

                    companyPhones.GetAllExceptOne(id);
                    if (companyPhones.RowCount > 0)
                    {
                        for (int i = 0; i < companyPhones.RowCount; i++)
                        {
                            companyPhones.BIsPrimary = false;
                            companyPhones.Save();
                            companyPhones.MoveNext();
                        }
                    }

                    companyPhones.FlushData();
                    companyPhones.LoadByPrimaryKey(id);
                    if (companyPhones.RowCount > 0)
                    {
                        companyPhones.BIsPrimary = true;
                        companyPhones.DModifiedDate = DateTime.Now;
                        companyPhones.Save();
                        LoadCompanyPhones();
                    }
                    break;

                case "del":
                    companyPhones.FlushData();
                    companyPhones.LoadByPrimaryKey(id);
                    if (companyPhones.RowCount > 0)
                    {
                        companyPhones.MarkAsDeleted();
                        companyPhones.Save();
                    }
                    LoadCompanyPhones();
                    break;
            }
        }
    }
    protected void grdMobileCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgBtnActiveMobile = e.Row.FindControl("imgBtnActiveMobile") as ImageButton;

            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgBtnActiveMobile.ImageUrl = "~/Images/Star_Black.png";
                imgBtnActiveMobile.Width = 16;
                imgBtnActiveMobile.ToolTip = "Active";
                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;
            }
        }
    }
    protected void grdFaxCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblCompanyFaxes companyFaxes = new tblCompanyFaxes();
            switch (e.CommandName.ToLower())
            {
                case "active":

                    companyFaxes.GetAllExceptOne(id);
                    if (companyFaxes.RowCount > 0)
                    {
                        for (int i = 0; i < companyFaxes.RowCount; i++)
                        {
                            companyFaxes.BIsPrimary = false;
                            companyFaxes.Save();
                            companyFaxes.MoveNext();
                        }
                    }

                    companyFaxes.FlushData();
                    companyFaxes.LoadByPrimaryKey(id);
                    if (companyFaxes.RowCount > 0)
                    {
                        companyFaxes.BIsPrimary = true;
                        companyFaxes.DModifiedDate = DateTime.Now;
                        companyFaxes.Save();
                        LoadCompanyFaxes();
                    }
                    break;
                case "del":
                    companyFaxes.FlushData();
                    companyFaxes.LoadByPrimaryKey(id);
                    if (companyFaxes.RowCount > 0)
                    {
                        companyFaxes.MarkAsDeleted();
                        companyFaxes.Save();
                    }
                    LoadCompanyFaxes();
                    break;
            }
        }
    }
    protected void grdFaxCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgBtnActiveFaxes = e.Row.FindControl("imgBtnActiveFaxes") as ImageButton;

            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgBtnActiveFaxes.ImageUrl = "~/Images/Star_Black.png";
                imgBtnActiveFaxes.Width = 16;
                imgBtnActiveFaxes.ToolTip = "Active";

                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;

            }
        }
    }
    protected void grdAddressCompany_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            tblCompanyAddresses companyAddress = new tblCompanyAddresses();

            switch (e.CommandName.ToLower())
            {
                case "active":

                    companyAddress.GetAllExceptOne(id);
                    if (companyAddress.RowCount > 0)
                    {
                        for (int i = 0; i < companyAddress.RowCount; i++)
                        {
                            companyAddress.BIsPrimary = false;
                            companyAddress.Save();
                            companyAddress.MoveNext();
                        }
                    }

                    companyAddress.FlushData();
                    companyAddress.LoadByPrimaryKey(id);
                    if (companyAddress.RowCount > 0)
                    {
                        companyAddress.BIsPrimary = true;
                        companyAddress.DModifiedDate = DateTime.Now;
                        companyAddress.Save();
                        LoadCompanyAddresses();
                    }
                    break;
                case "del":
                    companyAddress.FlushData();
                    companyAddress.LoadByPrimaryKey(id);
                    if (companyAddress.RowCount > 0)
                    {
                        companyAddress.MarkAsDeleted();
                        companyAddress.Save();
                    }
                    LoadCompanyAddresses();
                    break;
            }
        }
    }
    protected void grdAddressCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            ImageButton imgBtnActiveAddress = e.Row.FindControl("imgBtnActiveAddress") as ImageButton;
            if (Convert.ToBoolean(drv["bIsPrimary"]))
            {
                imgBtnActiveAddress.ImageUrl = "~/Images/Star_Black.png";
                imgBtnActiveAddress.Width = 16;
                imgBtnActiveAddress.ToolTip = "Active";

                ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
                imgDelete.Visible = false;
            }
        }
    }
    protected void grdCompanies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblCompanies company = new tblCompanies();
            tblSupplier supplierUpdate = new tblSupplier();
            switch (e.CommandName.ToLower())
            {
                case "name":
                    company.FlushData();
                    company.LoadByPrimaryKey(id);
                    if (company.RowCount > 0)
                    {
                        BindSuppliers();

                        CompanyID = company.PkCompanyID;
                        txtCompanyBrand.Text = company.CBrandName;
                        imgCompanyLogo.Src = company.Logo;
                        rdCompanyEmail.Checked = company.CContactMethod_Email;
                        rdCompanyTelephone.Checked = company.CContactMethod_Phone;
                        rdCompanyFax.Checked = company.CContactMethod_Fax;
                        ddlSuppliers.SelectedValue = company.FkSuplierID.ToString();

                        LoadCompanyEmails();
                        LoadCompanyPhones();
                        LoadCompanyFaxes();
                        LoadCompanyAddresses();
                        LoadDefaultCompany();
                        //ApplyFloatingJquery();
                        ShowTRc();

                    }
                    break;
            }
        }
    }
    protected void grdCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnHideCompany_Click(object sender, EventArgs e)
    {

        //stopFloatingJquery();
    }

    #endregion

    #region Contact People

    private void LoadContactSuppliers()
    {
        tblSupplier suppliers = new tblSupplier();
        suppliers.GetDepartmentSuppliers(DepartmentID);
        if (suppliers.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlContactSupplier, suppliers.DefaultView, "SBrandName", "PkSupplierID");
        }
    }
    private void LoadAllContactPeople(int supid)
    {
        tblContactPeople contactPeople = new tblContactPeople();
        contactPeople.GetContactsBySupplier(supid);
        grdContactPeople.DataSource = contactPeople.DefaultView;
        grdContactPeople.DataBind();
    }
    private void DefaultContactControls()
    {
        txtContactName.Text = "";
        txtContactEmail1.Text = "";
        txtContactEmail2.Text = "";
        txtContactPhone1.Text = "";
        txtContactPhone2.Text = "";
        txtContactTitle.Text = "";
        txtContactFax.Text = "";
        txtContactAddress.Text = "";
        txtContactNote.Text = "";
        //LoadContactSuppliers();
    }
    protected void imgBtnAddContactPeople_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            tblContactPeople contactPeople = new tblContactPeople();
            if (ViewState["ContactID"] != null)
            {
                contactPeople.FlushData();
                contactPeople.LoadByPrimaryKey(Convert.ToInt32(ViewState["ContactID"]));
                if (contactPeople.RowCount > 0)
                {
                    contactPeople.Title = txtContactTitle.Text;
                    contactPeople.PName = txtContactName.Text;
                    contactPeople.PEmail1 = txtContactEmail1.Text;
                    contactPeople.Phone1 = txtContactPhone1.Text;
                    contactPeople.PEmail2 = txtContactEmail2.Text;
                    contactPeople.Phone2 = txtContactPhone2.Text;
                    contactPeople.Fax = txtContactFax.Text;
                    contactPeople.PAddress = txtContactAddress.Text;
                    contactPeople.ContactNote = txtContactNote.Text;
                    contactPeople.DModifiedDate = DateTime.Now;
                    contactPeople.Save();
                    ViewState["ContactID"] = null;
                }
            }
            else
            {
                bool active = false;
                contactPeople.FlushData();
                if (!contactPeople.GetFirst(Convert.ToInt32(ViewState["Supplierid"])))
                    active = true;

                contactPeople.FlushData();
                contactPeople.AddNew();
                contactPeople.Title = txtContactTitle.Text;
                contactPeople.PName = txtContactName.Text;
                contactPeople.PEmail1 = txtContactEmail1.Text;
                contactPeople.Phone1 = txtContactPhone1.Text;
                contactPeople.PEmail2 = txtContactEmail2.Text;
                contactPeople.Phone2 = txtContactPhone2.Text;
                contactPeople.Fax = txtContactFax.Text;
                contactPeople.PAddress = txtContactAddress.Text;
                contactPeople.ContactNote = txtContactNote.Text;
                contactPeople.FkSuplierID = Convert.ToInt32(ViewState["Supplierid"]);
                contactPeople.DModifiedDate = DateTime.Now;
                contactPeople.DCreatedDate = DateTime.Now;
                contactPeople.BIsPrimary = active;
                contactPeople.Save();
            }
            imgBtnAddContactPeople.ImageUrl = "~/Images/btn_addcontact.png";

            DefaultContactControls();
            LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));
            //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
        }
        catch (Exception ex)
        {
        }
    }
    protected void imgBtnSaveContact_Click(object sender, ImageClickEventArgs e)
    {
        tblContactPeople contactPeople = new tblContactPeople();
        if (ContactPeopleID == 0)
        {
            contactPeople.FlushData();
            contactPeople.AddNew();
            contactPeople.Title = txtContactTitle.Text;
            contactPeople.PName = txtContactName.Text;
            contactPeople.PEmail1 = txtContactEmail1.Text;
            contactPeople.Phone1 = txtContactPhone1.Text;
            contactPeople.PEmail2 = txtContactEmail2.Text;
            contactPeople.Phone2 = txtContactPhone2.Text;
            contactPeople.Fax = txtContactFax.Text;
            contactPeople.PAddress = txtContactAddress.Text;
            contactPeople.FkSuplierID = Convert.ToInt32(ViewState["Supplierid"]);
            contactPeople.DModifiedDate = DateTime.Now;
            contactPeople.DCreatedDate = DateTime.Now;
            contactPeople.Save();
            LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));
        }
        else
        {
            contactPeople.FlushData();
            contactPeople.LoadByPrimaryKey(ContactPeopleID);
            if (contactPeople.RowCount > 0)
            {
                contactPeople.Title = txtContactTitle.Text;
                contactPeople.PName = txtContactName.Text;
                contactPeople.PEmail1 = txtContactEmail1.Text;
                contactPeople.Phone1 = txtContactPhone1.Text;
                contactPeople.PEmail2 = txtContactEmail2.Text;
                contactPeople.Phone2 = txtContactPhone2.Text;
                contactPeople.Fax = txtContactFax.Text;
                contactPeople.PAddress = txtContactAddress.Text;
                contactPeople.DModifiedDate = DateTime.Now;
                contactPeople.Save();
                LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));
            }
        }
        //tblGrdContactPeople.Visible = true;
        //trAddContactPeople.Visible = false;
        //imgBtnAddContactPeople.Visible = true;
    }
    protected void btnHideContact_Click(object sender, EventArgs e)
    {
        trAddContactPeople.Visible = false;
    }
    protected void grdContactPeople_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                ImageButton imgbtnSetActiveEmail = e.Row.FindControl("imgBtnActiveAddress") as ImageButton;

                if (Convert.ToBoolean(drv["bIsPrimary"]))
                {
                    imgbtnSetActiveEmail.ImageUrl = "~/Images/Star_Black.png";
                    imgbtnSetActiveEmail.Width = 16;
                    imgbtnSetActiveEmail.ToolTip = "Mark as favorite";

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdContactPeople_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdContactPeople.PageIndex = e.NewPageIndex;
        LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));
    }
    protected void grdContactPeople_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblContactPeople contactPeople = new tblContactPeople();
            switch (e.CommandName.ToLower())
            {
                case "name":
                    contactPeople.FlushData();
                    contactPeople.LoadByPrimaryKey(id);
                    if (contactPeople.RowCount > 0)
                    {
                        LoadContactSuppliers();
                        txtContactTitle.Text = contactPeople.Title;
                        txtContactName.Text = contactPeople.PName;
                        txtContactEmail1.Text = contactPeople.PEmail1;
                        txtContactPhone1.Text = contactPeople.Phone1;
                        txtContactEmail2.Text = contactPeople.PEmail2;
                        txtContactPhone2.Text = contactPeople.Phone2;
                        txtContactFax.Text = contactPeople.Fax;
                        txtContactAddress.Text = contactPeople.PAddress;

                        ViewState["ContactID"] = contactPeople.PkContactPeopleID;

                        //imgBtnAddContactPeople.Visible = false;
                        //tblGrdContactPeople.Visible = false;
                        //trAddContactPeople.Visible = true;
                        //imgBtnSaveContact.ImageUrl = "../Images/btn_update.png";
                    }
                    break;
                case "edt":
                    contactPeople.FlushData();
                    contactPeople.LoadByPrimaryKey(id);
                    if (contactPeople.RowCount > 0)
                    {
                        txtContactTitle.Text = contactPeople.Title;
                        txtContactName.Text = contactPeople.PName;
                        txtContactEmail1.Text = contactPeople.PEmail1;
                        txtContactPhone1.Text = contactPeople.Phone1;
                        txtContactEmail2.Text = contactPeople.PEmail2;
                        txtContactPhone2.Text = contactPeople.Phone2;
                        txtContactFax.Text = contactPeople.Fax;
                        txtContactNote.Text = contactPeople.ContactNote;

                        ViewState["ContactID"] = contactPeople.PkContactPeopleID;

                        //imgBtnAddContactPeople.Visible = false;
                        //tblGrdContactPeople.Visible = false;
                        //trAddContactPeople.Visible = true;
                        imgBtnAddContactPeople.ImageUrl = "../Images/btn_edit_admin.png";
                        //imgBtnSaveContact.ImageUrl = "../Images/btn_update.png";
                    }
                    break;
                case "active":
                    contactPeople.FlushData();
                    contactPeople.GetAllExceptOne(id);
                    if (contactPeople.RowCount > 0)
                    {
                        for (int i = 0; i < contactPeople.RowCount; i++)
                        {
                            contactPeople.BIsPrimary = false;
                            contactPeople.Save();
                            contactPeople.MoveNext();
                        }
                    }
                    contactPeople.FlushData();
                    contactPeople.LoadByPrimaryKey(id);
                    if (contactPeople.RowCount > 0)
                    {
                        contactPeople.BIsPrimary = true;
                        contactPeople.DModifiedDate = DateTime.Now;
                        contactPeople.Save();
                        LoadAllContactPeople(Convert.ToInt32(ViewState["Supplierid"]));
                    }
                    break;
            }
        }
    }
    #endregion

    #region Product Linking

    private void calllingThreeFunctions()
    {
        LoadAllSupplierNamesForBase();
        LoadAllSupplierNamesForSub();
        LoadAllSupplierNamesForProducts();
    }

    private void LoadAllSupplierNamesForBase()
    {
        //tblSupplierBaseCats sb = new tblSupplierBaseCats();
        //sb.GetSupplierNamesForBase();
        //grdSuppliersNamesForBaseCategory.DataSource = sb.DefaultView;
        //grdSuppliersNamesForBaseCategory.DataBind();
    }
    private void LoadAllSupplierNamesForSub()
    {
        //tblSupplierSubCats sc = new tblSupplierSubCats();
        //sc.GetSupplierNamesForSub();
        //grdSupplierNamesForSubcategory.DataSource = sc.DefaultView;
        //grdSupplierNamesForSubcategory.DataBind();
    }
    private void LoadAllSupplierNamesForProducts()
    {
        tblSupplier s = new tblSupplier();
        s.LoadAll();
        dtlSupplierNamesForSubcategory.DataSource = s.DefaultView;
        dtlSupplierNamesForSubcategory.DataBind();
    }

    protected void ddlBaseCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBaseCategories.SelectedValue != "0")
        {
            tblSupplierBaseCats sb = new tblSupplierBaseCats();
            sb.GetSupplierNamesForBase(Convert.ToInt32(ddlBaseCategories.SelectedValue));
            dtlSuppliersNamesForBaseCategory.DataSource = sb.DefaultView;
            dtlSuppliersNamesForBaseCategory.DataBind();

            tblSubCategories sub = new tblSubCategories();
            sub.GetSubCategories(Convert.ToInt32(ddlBaseCategories.SelectedValue));
            if (sub.RowCount > 0)
            {
                ddlSubCategories.Items.Clear();
                commonMethods.FillDropDownList(ddlSubCategories, sub.DefaultView, "CSubCategoryName", "PkSubCategoryID");
                //ddlSubCategories.Items.Insert(0, new ListItem("Select Sub Category", "0"));

                tblSupplierSubCats ssCat = new tblSupplierSubCats();
                ssCat.GetSupplierNamesForSub(Convert.ToInt32(ddlSubCategories.SelectedValue));
                dtlSupplierNamesForSubcategory.DataSource = ssCat.DefaultView;
                dtlSupplierNamesForSubcategory.DataBind();

                tblProducts prod = new tblProducts();
                prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
                if (prod.RowCount > 0)
                {
                    commonMethods.FillDropDownList(ddlProductsIndividual, prod.DefaultView, "sProductName", "PkProductID");
                    ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
                }
                else
                {
                    ddlProductsIndividual.Items.Clear();
                    ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
                }

            }
            else
            {
                ddlSubCategories.Items.Clear();
                dtlSupplierNamesForSubcategory.DataSource = null;
                dtlSupplierNamesForSubcategory.DataBind();

                ddlProductsIndividual.Items.Clear();
                ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
            }

        }
        else
        {
            ddlSubCategories.Items.Clear();
            ddlSubCategories.Items.Insert(0, new ListItem("Select Sub Category", "0"));
        }
        dtlSupplierNamesForProducts.DataSource = null;
        dtlSupplierNamesForProducts.DataBind();
        upnlProductLinking.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);

    }
    protected void ddlSubCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubCategories.SelectedValue != "0")
        {
            tblProducts prod = new tblProducts();
            prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
            if (prod.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlProductsIndividual, prod.DefaultView, "sProductName", "PkProductID");
                ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
            }
            else
            {
                ddlProductsIndividual.Items.Clear();
                ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
            }
            tblSupplierSubCats sc = new tblSupplierSubCats();
            sc.GetSupplierNamesForSub(Convert.ToInt32(ddlSubCategories.SelectedValue));
            dtlSupplierNamesForSubcategory.DataSource = sc.DefaultView;
            dtlSupplierNamesForSubcategory.DataBind();

        }
        else
        {
            ddlProductsIndividual.Items.Clear();
            ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
        }

        dtlSupplierNamesForProducts.DataSource = null;
        dtlSupplierNamesForProducts.DataBind();
        upnlProductLinking.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
    }

    protected void ddlBaseCategoryIndividual_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBaseCategoryIndividual.SelectedValue != "0")
        {
            tblSubCategories sub = new tblSubCategories();
            sub.GetSubCategories(Convert.ToInt32(ddlBaseCategoryIndividual.SelectedValue));
            if (sub.RowCount > 0)
            {
                ddlSubCategoryIndividual.Items.Clear();
                commonMethods.FillDropDownList(ddlSubCategoryIndividual, sub.DefaultView, "CSubCategoryName", "PkSubCategoryID");
                ddlSubCategoryIndividual.Items.Insert(0, new ListItem("Select Sub Category", "0"));
            }
        }
        else
        {
            ddlSubCategoryIndividual.Items.Clear();
            ddlSubCategoryIndividual.Items.Insert(0, new ListItem("Select Sub Category", "0"));
        }
        upnlProductLinking.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
    }
    protected void ddlSubCategoryIndividual_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubCategoryIndividual.SelectedValue != "0")
        {
            tblProducts prod = new tblProducts();
            prod.GetProducts(Convert.ToInt32(ddlSubCategoryIndividual.SelectedValue));
            if (prod.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlProductsIndividual, prod.DefaultView, "sProductName", "PkProductID");
                ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
            }
            else
            {
                ddlProductsIndividual.Items.Clear();
                ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
            }
        }
        upnlProductLinking.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
    }

    //protected void grdSuppliersNamesForBaseCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = (DataRowView)e.Row.DataItem;
    //        CheckBox chkActive = e.Row.FindControl("chkActive") as CheckBox;
    //        if ((drv["SupplierBaseID"]).ToString() != "")
    //        {
    //            chkActive.Checked = true;
    //        }
    //    }
    //}
    protected void dtlSuppliersNamesForBaseCategory_RowDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            CheckBox chkActive = e.Item.FindControl("chkActive") as CheckBox;
            if ((drv["SupplierBaseID"]).ToString() != "")
            {
                chkActive.Checked = true;
            }
            try
            {
                if (!Convert.ToBoolean((drv["isActive"]).ToString()))
                {
                    chkActive.Style.Add("color", "Gray");
                }
            }
            catch (Exception ex)
            {
                chkActive.Style.Add("color", "Gray");
            }

        }
    }

    //protected void grdSupplierNamesForSubcategory_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = (DataRowView)e.Row.DataItem;
    //        CheckBox chkActive = e.Row.FindControl("chkActive") as CheckBox;
    //        if ((drv["SupplierSubID"]).ToString() != "")
    //        {
    //            chkActive.Checked = true;
    //        }
    //    }
    //}
    protected void dtlSupplierNamesForSubcategory_RowDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            CheckBox chkActive = e.Item.FindControl("chkActive") as CheckBox;
            if ((drv["SupplierSubID"]).ToString() != "")
            {
                chkActive.Checked = true;
            }
            try
            {
                if (!Convert.ToBoolean((drv["isActive"]).ToString()))
                {
                    chkActive.Style.Add("color", "Gray");
                }
            }
            catch (Exception ex)
            {
                chkActive.Style.Add("color", "Gray");
            }
        }
    }
    //protected void grdSupplierNamesForProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView drv = (DataRowView)e.Row.DataItem;
    //        CheckBox chkActive = e.Row.FindControl("chkActive") as CheckBox;
    //        if ((drv["SupplierProID"]).ToString() != "")
    //        {
    //            chkActive.Checked = true;
    //        }
    //    }
    //}
    protected void dtlSupplierNamesForProducts_RowDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            CheckBox chkActive = e.Item.FindControl("chkActive") as CheckBox;
            if ((drv["SupplierProID"]).ToString() != "")
            {
                chkActive.Checked = true;
            }
            try
            {
                if (!Convert.ToBoolean((drv["isActive"]).ToString()))
                {
                    chkActive.Style.Add("color", "Gray");
                }
            }
            catch (Exception ex)
            {
                chkActive.Style.Add("color", "Gray");
            }
        }
    }

    protected void ddlProductsIndividual_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductsIndividual.SelectedValue != "0")
        {
            tblSupplierProducts sp = new tblSupplierProducts();
            sp.GetSupplierNamesForPro(Convert.ToInt32(ddlProductsIndividual.SelectedValue));
            dtlSupplierNamesForProducts.DataSource = sp.DefaultView;
            dtlSupplierNamesForProducts.DataBind();
        }
        else
        {
            dtlSupplierNamesForProducts.DataSource = null;
            dtlSupplierNamesForProducts.DataBind();
        }
        upnlProductLinking.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
    }

    protected void chkActiveBase_Clicked(Object sender, EventArgs e)
    {
        try
        {
            CheckBox chkActiveBase = ((CheckBox)sender).Parent.FindControl("chkActive") as CheckBox;
            HiddenField hidSupplierID = ((CheckBox)sender).Parent.FindControl("hidSupplierID") as HiddenField;

            HiddenField hidSupplierBase = ((CheckBox)sender).Parent.FindControl("hidSupplierBaseID") as HiddenField;
            tblSupplierBaseCats sbCat = new tblSupplierBaseCats();
            tblSupplierSubCats ssCat = new tblSupplierSubCats();
            if (chkActiveBase.Checked)
            {
                HiddenField hidSupplierBaseID = ((CheckBox)sender).Parent.FindControl("hidSupplierBaseID") as HiddenField;
                #region Adding Supplier Base Category Linking
                sbCat.FlushData();
                sbCat.AddNew();
                sbCat.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                sbCat.FkBaseCategoryID = Convert.ToInt32(ddlBaseCategories.SelectedValue);
                sbCat.DCreatedDate = DateTime.Now;
                sbCat.Save();
                #endregion

                tblSupplierBaseCats sbCat2 = new tblSupplierBaseCats();
                sbCat2.GetSupplierNamesForBase(Convert.ToInt32(ddlBaseCategories.SelectedValue));
                dtlSuppliersNamesForBaseCategory.DataSource = sbCat2.DefaultView;
                dtlSuppliersNamesForBaseCategory.DataBind();

                tblSubCategories sub = new tblSubCategories();
                sub.GetSubCategories(Convert.ToInt32(ddlBaseCategories.SelectedValue));
                if (sub.RowCount > 0)
                {
                    for (int i = 0; i < sub.RowCount; i++)
                    {
                        #region Adding Supplier Sub Category Linking
                        ssCat.FlushData();
                        ssCat.AddNew();
                        ssCat.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                        ssCat.FkSubCategoryID = sub.PkSubCategoryID;
                        ssCat.DCreatedDate = DateTime.Now;
                        ssCat.Save();
                        #endregion

                        #region Adding Supplier Product Linking

                        tblProducts prod = new tblProducts();
                        prod.GetProducts(sub.PkSubCategoryID);
                        if (prod.RowCount > 0)
                        {
                            tblSupplierProducts sp = new tblSupplierProducts();
                            for (int j = 0; j < prod.RowCount; j++)
                            {

                                sp.FlushData();
                                sp.GetSupplierProducts(prod.PkProductID, Convert.ToInt32(hidSupplierID.Value));
                                if (sp.RowCount == 0)
                                {
                                    sp.FlushData();
                                    sp.AddNew();
                                    sp.FkProductID = prod.PkProductID;
                                    sp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                                    sp.DCreatedDate = DateTime.Now;
                                    sp.DModifiedDate = DateTime.Now;
                                    sp.Save();
                                }
                                tblSupplierProductPrices ssp = new tblSupplierProductPrices();
                                tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
                                rel.GetPacking_Qunatity(prod.PkProductID);
                                if (rel.RowCount > 0)
                                {
                                    for (int r = 0; r < rel.RowCount; r++)
                                    {
                                        ssp.FlushData();
                                        ssp.GETRecord(Convert.ToInt32(hidSupplierID.Value), rel.PkProductPackingQuantityRelID);
                                        if (ssp.RowCount == 0)
                                        {
                                            ssp.FlushData();
                                            ssp.AddNew();
                                            ssp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                                            ssp.FkProductPackingQuantityRelID = rel.PkProductPackingQuantityRelID;
                                            ssp.DModifiedDate = DateTime.Now;
                                            ssp.DCreatedDate = DateTime.Now;
                                            ssp.Save();

                                        }
                                        rel.MoveNext();
                                    }
                                }






                                prod.MoveNext();
                            }
                        }

                        #endregion





                        sub.MoveNext();
                    }

                    tblSupplierSubCats sc = new tblSupplierSubCats();
                    sc.GetSupplierNamesForSub(Convert.ToInt32(ddlSubCategories.SelectedValue));
                    dtlSupplierNamesForSubcategory.DataSource = sc.DefaultView;
                    dtlSupplierNamesForSubcategory.DataBind();
                }
            }
            else if (!chkActiveBase.Checked)
            {
                HiddenField hidSupplierBaseID = ((CheckBox)sender).Parent.FindControl("hidSupplierBaseID") as HiddenField;
                sbCat.FlushData();
                sbCat.LoadByPrimaryKey(Convert.ToInt32(hidSupplierBaseID.Value));
                if (sbCat.RowCount > 0)
                {

                    tblSubCategories sub = new tblSubCategories();
                    sub.GetSubCategories(Convert.ToInt32(ddlBaseCategories.SelectedValue));
                    if (sub.RowCount > 0)
                    {
                        for (int i = 0; i < sub.RowCount; i++)
                        {
                            #region Removing Supplier Products Linking

                            tblProducts prod = new tblProducts();
                            prod.GetProducts(sub.PkSubCategoryID);
                            if (prod.RowCount > 0)
                            {
                                tblSupplierProducts sp = new tblSupplierProducts();
                                for (int j = 0; j < prod.RowCount; j++)
                                {
                                    sp.FlushData();
                                    sp.GetSupplierProducts(prod.PkProductID, Convert.ToInt32(hidSupplierID.Value));
                                    if (sp.RowCount > 0)
                                    {
                                        sp.MarkAsDeleted();
                                        sp.Save();
                                    }
                                    prod.MoveNext();
                                }
                            }

                            #endregion




                            ssCat.FlushData();
                            ssCat.GetSupplierSubRecord(Convert.ToInt32(hidSupplierID.Value), sub.PkSubCategoryID);
                            if (ssCat.RowCount > 0)
                            {
                                #region Removing Supplier Sub Cateogory Linking
                                ssCat.MarkAsDeleted();
                                ssCat.Save();

                                #endregion
                            }

                            sub.MoveNext();
                        }

                        tblSupplierSubCats sc = new tblSupplierSubCats();
                        sc.GetSupplierNamesForSub(Convert.ToInt32(ddlSubCategories.SelectedValue));
                        dtlSupplierNamesForSubcategory.DataSource = sc.DefaultView;
                        dtlSupplierNamesForSubcategory.DataBind();
                    }

                    #region Removing Supplier Base Category Linking
                    sbCat.MarkAsDeleted();
                    sbCat.Save();
                    #endregion
                }
            }
            ddlBaseCategoryIndividual.SelectedValue = "0";
            ddlSubCategoryIndividual.SelectedValue = "0";
            ddlProductsIndividual.SelectedValue = "0";
            dtlSupplierNamesForProducts.DataSource = null;
            dtlSupplierNamesForProducts.DataBind();
            upnlProductLinking.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
        }
        catch (Exception ex)
        { }
    }

    protected void chkActiveSub_Clicked(Object sender, EventArgs e)
    {
        try
        {
            CheckBox chkActiveSub = ((CheckBox)sender).Parent.FindControl("chkActive") as CheckBox;
            HiddenField hidSupplierID = ((CheckBox)sender).Parent.FindControl("hidSupplierID") as HiddenField;

            tblSupplierSubCats ssCat = new tblSupplierSubCats();

            if (chkActiveSub.Checked)
            {
                HiddenField hidSupplierSubID = ((CheckBox)sender).Parent.FindControl("hidSupplierSubID") as HiddenField;

                #region Adding Supplier Sub Category Linking
                ssCat.FlushData();
                ssCat.AddNew();
                ssCat.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                ssCat.FkSubCategoryID = Convert.ToInt32(ddlSubCategories.SelectedValue);
                ssCat.DCreatedDate = DateTime.Now;
                ssCat.Save();
                #endregion

                tblSupplierSubCats ssCat2 = new tblSupplierSubCats();
                ssCat2.GetSupplierNamesForSub(Convert.ToInt32(ddlSubCategories.SelectedValue));
                dtlSupplierNamesForSubcategory.DataSource = ssCat2.DefaultView;
                dtlSupplierNamesForSubcategory.DataBind();

                #region Adding Supplier Product Linking

                tblProducts prod = new tblProducts();
                prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
                if (prod.RowCount > 0)
                {
                    tblSupplierProducts sp = new tblSupplierProducts();
                    for (int j = 0; j < prod.RowCount; j++)
                    {
                        sp.FlushData();
                        sp.GetSupplierProducts(prod.PkProductID, Convert.ToInt32(hidSupplierID.Value));
                        if (sp.RowCount == 0)
                        {
                            sp.FlushData();
                            sp.AddNew();
                            sp.FkProductID = prod.PkProductID;
                            sp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                            sp.DCreatedDate = DateTime.Now;
                            sp.DModifiedDate = DateTime.Now;
                            sp.Save();
                        }

                        tblSupplierProductPrices ssp = new tblSupplierProductPrices();
                        tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
                        rel.GetPacking_Qunatity(prod.PkProductID);
                        if (rel.RowCount > 0)
                        {
                            for (int r = 0; r < rel.RowCount; r++)
                            {
                                ssp.FlushData();
                                ssp.GETRecord(Convert.ToInt32(hidSupplierID.Value), rel.PkProductPackingQuantityRelID);
                                if (ssp.RowCount == 0)
                                {
                                    ssp.FlushData();
                                    ssp.AddNew();
                                    ssp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                                    ssp.FkProductPackingQuantityRelID = rel.PkProductPackingQuantityRelID;
                                    ssp.DModifiedDate = DateTime.Now;
                                    ssp.DCreatedDate = DateTime.Now;
                                    ssp.Save();

                                }
                                rel.MoveNext();
                            }
                        }



                        prod.MoveNext();
                    }
                }

                #endregion

            }
            else if (!chkActiveSub.Checked)
            {
                HiddenField hidSupplierSubID = ((CheckBox)sender).Parent.FindControl("hidSupplierSubID") as HiddenField;

                #region Removing Supplier Products Linking

                tblProducts prod = new tblProducts();
                prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
                if (prod.RowCount > 0)
                {
                    tblSupplierProducts sp = new tblSupplierProducts();
                    for (int j = 0; j < prod.RowCount; j++)
                    {
                        sp.FlushData();
                        sp.GetSupplierProducts(prod.PkProductID, Convert.ToInt32(hidSupplierID.Value));
                        if (sp.RowCount > 0)
                        {
                            sp.MarkAsDeleted();
                            sp.Save();
                        }
                        prod.MoveNext();
                    }
                }

                #endregion

                #region Removing Supplier Sub Cateogory Linking
                ssCat.FlushData();
                ssCat.LoadByPrimaryKey(Convert.ToInt32(hidSupplierSubID.Value));
                if (ssCat.RowCount > 0)
                {
                    ssCat.MarkAsDeleted();
                    ssCat.Save();
                }
                #endregion
            }
            upnlProductLinking.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
        }
        catch (Exception ex)
        { }
    }

    protected void chkActivePro_Clicked(Object sender, EventArgs e)
    {
        try
        {
            CheckBox chkActivePro = ((CheckBox)sender).Parent.FindControl("chkActive") as CheckBox;
            HiddenField hidSupplierID = ((CheckBox)sender).Parent.FindControl("hidSupplierID") as HiddenField;

            if (chkActivePro.Checked)
            {
                HiddenField hidSupplierProID = ((CheckBox)sender).Parent.FindControl("hidSupplierProID") as HiddenField;
                #region Adding Supplier Product Linking

                tblSupplierProducts sp = new tblSupplierProducts();

                sp.AddNew();
                sp.FkProductID = Convert.ToInt32(ddlProductsIndividual.SelectedValue);
                sp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                sp.DCreatedDate = DateTime.Now;
                sp.DModifiedDate = DateTime.Now;
                sp.Save();

                tblSupplierProductPrices ssp = new tblSupplierProductPrices();
                tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
                rel.GetPacking_Qunatity(Convert.ToInt32(ddlProductsIndividual.SelectedValue));
                if (rel.RowCount > 0)
                {
                    for (int r = 0; r < rel.RowCount; r++)
                    {
                        ssp.FlushData();
                        ssp.GETRecord(Convert.ToInt32(hidSupplierID.Value), rel.PkProductPackingQuantityRelID);
                        if (ssp.RowCount == 0)
                        {
                            ssp.FlushData();
                            ssp.AddNew();
                            ssp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                            ssp.FkProductPackingQuantityRelID = rel.PkProductPackingQuantityRelID;
                            ssp.DModifiedDate = DateTime.Now;
                            ssp.DCreatedDate = DateTime.Now;
                            ssp.Save();
                        }
                        rel.MoveNext();
                    }
                }

                #endregion
            }
            else if (!chkActivePro.Checked)
            {
                HiddenField hidSupplierProID = ((CheckBox)sender).Parent.FindControl("hidSupplierProID") as HiddenField;

                #region Removing Supplier Product Linking
                tblSupplierProducts sp = new tblSupplierProducts();
                sp.LoadByPrimaryKey(Convert.ToInt32(hidSupplierProID.Value));
                if (sp.RowCount > 0)
                {
                    sp.MarkAsDeleted();
                    sp.Save();
                }
                #endregion
            }
            upnlProductLinking.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Jqu", "$(function(){ApplyJqueryForProductLinking();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JquSaved", "$(function(){RecordSaved();});", true);
        }
        catch (Exception ex)
        { }
    }

    #endregion

    #region Supplier Products
    protected void ddlEditSupplierProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEditSupplierProducts.SelectedValue != "0")
            {
                trFilter.Visible = true;
                tblSupplier s = new tblSupplier();
                s.GetSupplierInfoForProductHistory(Convert.ToInt32(ddlEditSupplierProducts.SelectedValue));
                if (s.RowCount > 0)
                {
                    if (s.GetColumn("sLogo").ToString() != "")
                        imgCompanyLogoP.Src = s.GetColumn("sLogo").ToString();
                    else
                        imgCompanyLogoP.Src = "../Images/no_image.gif";
                    lblCompanyBrand.Text = s.GetColumn("sBrandName").ToString();
                    lblCompanyAddress.Text = s.GetColumn("sAddressStreet").ToString() + " " + s.GetColumn("sAddressTown").ToString() + " " + s.GetColumn("sAddressRegion").ToString() + " " + s.GetColumn("sAddressPostCode") + " " + s.GetColumn("sCountry").ToString();
                    lblPhoneEmailFax.Text = "Fav. Phone: " + s.GetColumn("phone").ToString() + " " + "Fav. Email: " + s.GetColumn("sEmail").ToString() + " " + "Fav. FAX: " + s.GetColumn("sFax").ToString();
                    lblContact.Text = "Contact Person Name: " + s.GetColumn("pName").ToString() + " " + "Contact Person Phone and Email: " + s.GetColumn("cphone").ToString() + " " + s.GetColumn("cphone").ToString();
                }
                else
                {

                    imgCompanyLogoP.Src = "../Images/no_image.gif";
                    lblCompanyBrand.Text = "";
                    lblCompanyAddress.Text = "";
                    lblPhoneEmailFax.Text = "";
                    lblContact.Text = "";
                }

                GetSupplierProduct();
                imgBtnSaveAll.Visible = true;
            }
            else
            {
                trFilter.Visible = false;
                imgCompanyLogoP.Src = "../Images/no_image.gif";
                lblCompanyBrand.Text = "";
                lblCompanyAddress.Text = "";
                lblPhoneEmailFax.Text = "";
                lblContact.Text = "";
                grdProductPrices.DataSource = null;
                grdProductPrices.DataBind();
                imgBtnSaveAll.Visible = false;
            }
            txtSearch.Text = "";
            upnlSupplierProducts.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
        }
    }


    private void GetSupplierProduct()
    {
        tblSupplierProductPrices spp = new tblSupplierProductPrices();
        spp.getSupplierProductPrices(Convert.ToInt32(ddlEditSupplierProducts.SelectedValue));
        grdProductPrices.DataSource = spp.DefaultView;
        grdProductPrices.DataBind();
        if (spp.RowCount == 0)
        {
            trFilter.Visible = false;
            imgBtnSaveAll.Visible = false;
        }
    }

    protected void grdProductPrices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                TextBox txtNewPrice = e.Row.FindControl("txtNewPrice") as TextBox;
                TextBox txtDate = e.Row.FindControl("txtDate") as TextBox;
                HiddenField hidid = e.Row.FindControl("hidid") as HiddenField;
                LinkButton lnkOldPrice = e.Row.FindControl("lnkOldPrice") as LinkButton;
                LinkButton lnkProductName = e.Row.FindControl("lnkProductName") as LinkButton;
                HiddenField hidSupplierid = e.Row.FindControl("hidSupplierid") as HiddenField;
                HiddenField hidrelid = e.Row.FindControl("hidrelid") as HiddenField;

                lnkProductName.PostBackUrl = "Products.aspx?pid=" + drv["pkproductid"].ToString() + "&packid=" + drv["pkProductPackageID"].ToString() + "&relid=" + hidrelid.Value;

                Label lblDifferenceValue = e.Row.FindControl("lblDifferenceValue") as Label;
                HtmlImage imgAero = e.Row.FindControl("imgAero") as HtmlImage;

                HtmlContainerControl divDate = e.Row.FindControl("divDate") as HtmlContainerControl;

                tblSupplierProductPrices ssp = new tblSupplierProductPrices();
                ssp.GetOldNewPrice(Convert.ToInt32(hidSupplierid.Value), Convert.ToInt32(hidrelid.Value));
                if (ssp.RowCount > 1)
                {
                    double newPrice = 0.0;
                    double oldPrice = 0.0;
                    for (int i = 0; i < ssp.RowCount; i++)
                    {
                        if (i == 0)
                        {
                            newPrice = ssp.Price;
                            int day = ssp.DModifiedDate.Day;
                            int month = ssp.DModifiedDate.Month;
                            int year = ssp.DModifiedDate.Year;
                            lnkOldPrice.Text = commonMethods.ChangetToUK(newPrice.ToString("N"));
                            lnkOldPrice.Text += " € (" + day.ToString() + " / " + month.ToString() + " / " + year.ToString() + ")";
                        }
                        else if (i == 1)
                        {
                            oldPrice = ssp.Price;

                        }
                        ssp.MoveNext();
                    }
                    //txtNewPrice.Text = commonMethods.ChangetToUK(newPrice.ToString("N"));


                    double diff = newPrice - oldPrice;
                    if (diff > 0)
                    {
                        lblDifferenceValue.Text = "+ " + commonMethods.ChangetToUK(diff.ToString("N"));
                        lblDifferenceValue.Style.Add("color", "red");
                        imgAero.Src = "../Images/up_arrow.png";
                    }
                    else if (diff < 0)
                    {

                        lblDifferenceValue.Text = "- " + commonMethods.ChangetToUK(diff.ToString("N").Replace("-", ""));
                        lblDifferenceValue.Style.Add("color", "green");
                        imgAero.Src = "../Images/down_arrow.png";
                    }
                }
                else if (ssp.RowCount > 0)
                {
                    try
                    {
                        if (!DBNull.Equals(ssp.Price, null) && ssp.Price != 0.0 && ssp.Price != 0)
                        {
                            double oldPrice = 0.0;
                            oldPrice = ssp.Price;
                            int day = ssp.DModifiedDate.Day;
                            int month = ssp.DModifiedDate.Month;
                            int year = ssp.DModifiedDate.Year;
                            lnkOldPrice.Text = commonMethods.ChangetToUK(oldPrice.ToString("N"));
                            lnkOldPrice.Text += " € (" + day.ToString() + " / " + month.ToString() + " / " + year.ToString() + ")";
                        }
                    }
                    catch (InvalidCastException ex)
                    {

                    }
                }
                //txtDate.Attributes.Add("onclick", "javascript:GridDate(this);");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ GridDate('" + txtDate + "') });", true);
            }
            catch (Exception ex)
            { }
        }
    }
    protected void grdProductPrices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProductPrices.PageIndex = e.NewPageIndex;
        GetSupplierProduct();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
    }
    protected void grdProductPrices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            switch (e.CommandName.ToLower())
            {

                case "edt":
                    try
                    {
                        ImageButton btn = (ImageButton)e.CommandSource as ImageButton;

                        TextBox txtDate = btn.FindControl("txtDate") as TextBox;
                        if (txtDate.Text != "")
                        {
                            string[] start_D = txtDate.Text.Split('/'); // start Date
                            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                            start = Convert.ToDateTime(start).AddDays(-1).ToShortDateString();

                            TextBox txt = btn.FindControl("txtNewPrice") as TextBox;
                            HiddenField hidSupplierid = btn.FindControl("hidSupplierid") as HiddenField;
                            HiddenField hidrelid = btn.FindControl("hidrelid") as HiddenField;
                            HiddenField hidid = btn.FindControl("hidid") as HiddenField;


                            tblSupplierProductPrices ssp = new tblSupplierProductPrices();
                            ssp.GetOldNewPrice(Convert.ToInt32(hidSupplierid.Value), Convert.ToInt32(hidrelid.Value));
                            if (ssp.RowCount == 1)
                            {
                                try
                                {
                                    if ((ssp.Price != 0 || ssp.Price != 0.0) && ssp.Price.ToString() != commonMethods.ChangeToUS(txt.Text).ToString())
                                    {
                                        ssp.FlushData();
                                        ssp.AddNew();
                                        ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                        ssp.FkProductPackingQuantityRelID = Convert.ToInt32(hidrelid.Value);
                                        ssp.FkSupplierID = Convert.ToInt32(hidSupplierid.Value);
                                        ssp.DCreatedDate = Convert.ToDateTime(start);
                                        ssp.DModifiedDate = Convert.ToDateTime(start);
                                        ssp.Save();
                                    }
                                    else
                                    {
                                        ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                        ssp.DModifiedDate = DateTime.Now;
                                        ssp.Save();
                                    }
                                }
                                catch (InvalidCastException ex)
                                {
                                    ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                    ssp.DModifiedDate = DateTime.Now;
                                    ssp.Save();
                                }
                            }
                            else if (ssp.RowCount > 1)
                            {
                                bool checkPrice = false;
                                for (int j = 0; j < ssp.RowCount; j++)
                                {
                                    try
                                    {
                                        if ((ssp.Price != 0 || ssp.Price != 0.0) && ssp.Price.ToString() != commonMethods.ChangeToUS(txt.Text).ToString())
                                        { }
                                        else
                                            checkPrice = true;
                                    }
                                    catch (InvalidCastException ex)
                                    {
                                        checkPrice = true;
                                    }

                                    ssp.MoveNext();
                                }

                                if (!checkPrice)
                                {
                                    ssp.FlushData();
                                    ssp.AddNew();
                                    ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                    ssp.FkProductPackingQuantityRelID = Convert.ToInt32(hidrelid.Value);
                                    ssp.FkSupplierID = Convert.ToInt32(hidSupplierid.Value);
                                    ssp.DCreatedDate = Convert.ToDateTime(start);
                                    ssp.DModifiedDate = Convert.ToDateTime(start);
                                    ssp.Save();
                                }
                            }
                            else
                            {
                                ssp.FlushData();
                                ssp.AddNew();
                                ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                ssp.FkProductPackingQuantityRelID = Convert.ToInt32(hidrelid.Value);
                                ssp.FkSupplierID = Convert.ToInt32(hidSupplierid.Value);
                                ssp.DCreatedDate = Convert.ToDateTime(start);
                                ssp.DModifiedDate = Convert.ToDateTime(start);
                                ssp.Save();
                            }



                            GetSupplierProduct();
                            upnlSupplierProducts.Update();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "thisAlert", "$(function(){alert('please enter date in date field to save product price!');});", true);
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);


                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
                    }
                    break;

                case "graph":
                    LinkButton btn2 = (LinkButton)e.CommandSource as LinkButton;
                    Label lblProductName = btn2.FindControl("lblProductName") as Label;
                    Label lblPacking = btn2.FindControl("lblPacking") as Label;
                    Label lblQuantity = btn2.FindControl("lblQuantity") as Label;

                    HiddenField hidSupplierid2 = btn2.FindControl("hidSupplierid") as HiddenField;
                    HiddenField hidrelid2 = btn2.FindControl("hidrelid") as HiddenField;

                    graph_supplierid = Convert.ToInt32(hidSupplierid2.Value);
                    graph_relid = Convert.ToInt32(hidrelid2.Value);
                    tblSupplierProductPrices ssp2 = new tblSupplierProductPrices();
                    ssp2.GetOldNewPriceAll(Convert.ToInt32(hidSupplierid2.Value), Convert.ToInt32(hidrelid2.Value));
                    DataView dt = ssp2.DefaultView;
                    if (ssp2.RowCount > 0)
                    {
                        string xLast = string.Empty;
                        string xFirst = string.Empty;
                        int prevDay = 0;
                        int days = 0;
                        DateTime dtDate = ssp2.DModifiedDate;
                        prevDay = ssp2.DModifiedDate.Day;
                        double price = 0.0;
                        xLast = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                        xFirst = "1/1/" + (DateTime.Now.Year - 1).ToString();
                        txtFromDatePopup.Text = xFirst;
                        txtEndDatePopup.Text = xLast;
                        for (int i = 0; i < ssp2.RowCount; i++)
                        {
                            if (price < ssp2.Price)
                                price = ssp2.Price;

                            if (i == ssp2.RowCount - 1)
                            {
                                //xLast = ssp2.DModifiedDate.Day.ToString() + "/" + ssp2.DModifiedDate.Month.ToString() + "/" + ssp2.DModifiedDate.Year.ToString();
                                //xFirst = "1/1/" + (ssp2.DModifiedDate.Year - 1).ToString();
                                TimeSpan tr = ssp2.DModifiedDate.Subtract(dtDate);
                                days = Convert.ToInt32(tr.TotalDays);

                            }
                            ssp2.MoveNext();
                        }

                        LineChart c = new LineChart(900, 480, Page);
                        c.SetXAxisLabel(xFirst, xLast);
                        c.strTitle = "Price Range";

                        c.ftXorigin = 0;
                        c.ftScaleX = ssp2.RowCount;
                        c.ftXdivs = ssp2.RowCount;
                        int divs = 0;
                        if (price > 0 && price < 31)
                            divs = Convert.ToInt32(price) / 3;
                        else if (price > 30 && price < 51)
                            divs = Convert.ToInt32(price) / 5;
                        else if (price > 50 && price < 101)
                            divs = Convert.ToInt32(price) / 10;
                        else if (price > 100 && price < 151)
                            divs = Convert.ToInt32(price) / 15;
                        else if (price > 150 && price < 201)
                            divs = Convert.ToInt32(price) / 20;
                        else if (price > 200)
                            divs = Convert.ToInt32(price) / 30;


                        c.ftYorigin = 0;
                        c.ftScaleY = (float)price;
                        c.ftYdivs = divs + 1;

                        ssp2.FlushData();
                        ssp2.GetOldNewPriceAll(Convert.ToInt32(hidSupplierid2.Value), Convert.ToInt32(hidrelid2.Value));
                        for (int i = 1; i <= ssp2.RowCount; i++)
                        {
                            c.AddValue(i, Convert.ToInt32(ssp2.Price));
                            //prevDay = prevDay + ssp2.DModifiedDate.Day;
                            ssp2.MoveNext();
                        }
                        c.Draw();

                        // System.Drawing.Image image = System.Drawing.Image.FromStream(Serialization.gStream);
                        BinaryFormatter formatter = new BinaryFormatter();
                        Serialization.gStream.Position = 0;
                        Bitmap bt = (Bitmap)formatter.Deserialize(Serialization.gStream);
                        bt.SetResolution(1024, 768);
                        //System.Drawing.Image image =  System.Drawing.Image.FromStream(Serialization.gStream).GetPropertyItem; 
                        string tempGUI = Guid.NewGuid().ToString();
                        string sName = tempGUI.Substring(0, 5);
                        //string direcroryPath = Server.MapPath("../Chart/");
                        //if (Directory.Exists(direcroryPath))
                        //{
                        //    Directory.Delete(direcroryPath, true);
                        //    Directory.CreateDirectory(direcroryPath);
                        //}
                        bt.Save(Server.MapPath("../Chart/chart" + sName + ".jpg"), ImageFormat.Jpeg);
                        imgGraph.Src = "../Chart/chart" + sName + ".jpg";
                        UpdatePanel1.Update();

                    }
                    lblPriceDetail.Text = lblProductName.Text + ", " + lblPacking.Text + ", " + lblQuantity.Text + ", price changes";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
                    UpdatePanel1.Update();
                    ModalPopupExtender2.Show();

                    break;
            }
        }
    }
    protected void imgBtnSaveAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdProductPrices.Rows.Count > 0)
            {
                for (int i = 0; i < grdProductPrices.Rows.Count; i++)
                {
                    TextBox txt = grdProductPrices.Rows[i].FindControl("txtNewPrice") as TextBox;
                    HiddenField hidSupplierid = grdProductPrices.Rows[i].FindControl("hidSupplierid") as HiddenField;
                    HiddenField hidrelid = grdProductPrices.Rows[i].FindControl("hidrelid") as HiddenField;
                    HiddenField hidid = grdProductPrices.Rows[i].FindControl("hidid") as HiddenField;
                    TextBox txtDate = grdProductPrices.Rows[i].FindControl("txtDate") as TextBox;

                    if (txtDate.Text != "" && txt.Text != "")
                    {
                        string[] start_D = txtDate.Text.Split('/'); // start Date
                        string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                        start = Convert.ToDateTime(start).AddDays(-1).ToShortDateString();

                        tblSupplierProductPrices ssp = new tblSupplierProductPrices();
                        ssp.GetOldNewPrice(Convert.ToInt32(hidSupplierid.Value), Convert.ToInt32(hidrelid.Value));
                        if (ssp.RowCount == 1)
                        {
                            try
                            {
                                if ((ssp.Price != 0 || ssp.Price != 0.0) && ssp.Price.ToString() != commonMethods.ChangeToUS(txt.Text).ToString())
                                {
                                    ssp.FlushData();
                                    ssp.AddNew();
                                    ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                    ssp.FkProductPackingQuantityRelID = Convert.ToInt32(hidrelid.Value);
                                    ssp.FkSupplierID = Convert.ToInt32(hidSupplierid.Value);
                                    ssp.DCreatedDate = Convert.ToDateTime(start);
                                    ssp.DModifiedDate = Convert.ToDateTime(start);
                                    ssp.Save();
                                }
                                else
                                {
                                    ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                    ssp.DModifiedDate = DateTime.Now;
                                    ssp.Save();
                                }
                            }
                            catch (InvalidCastException ex)
                            {
                                ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                ssp.DModifiedDate = DateTime.Now;
                                ssp.Save();
                            }
                        }
                        else if (ssp.RowCount > 1)
                        {
                            bool checkPrice = false;
                            for (int j = 0; j < ssp.RowCount; j++)
                            {
                                try
                                {
                                    if ((ssp.Price != 0 || ssp.Price != 0.0) && ssp.Price.ToString() != commonMethods.ChangeToUS(txt.Text).ToString())
                                    { }
                                    else
                                        checkPrice = true;
                                }
                                catch (InvalidCastException ex)
                                {
                                    checkPrice = true;
                                }

                                ssp.MoveNext();
                            }

                            if (!checkPrice)
                            {
                                ssp.FlushData();
                                ssp.AddNew();
                                ssp.Price = commonMethods.ChangeToUS(txt.Text);
                                ssp.FkProductPackingQuantityRelID = Convert.ToInt32(hidrelid.Value);
                                ssp.FkSupplierID = Convert.ToInt32(hidSupplierid.Value);
                                ssp.DCreatedDate = Convert.ToDateTime(start);
                                ssp.DModifiedDate = Convert.ToDateTime(start);
                                ssp.Save();
                            }
                        }
                    }
                }
                GetSupplierProduct();
                upnlSupplierProducts.Update();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
        }
    }
    protected void imgBtnSearchProducts_Click(object sender, ImageClickEventArgs e)
    {
        tblSupplierProductPrices spp = new tblSupplierProductPrices();
        spp.getSupplierProductPrices_Filter(Convert.ToInt32(ddlEditSupplierProducts.SelectedValue), txtSearch.Text);
        grdProductPrices.DataSource = spp.DefaultView;
        grdProductPrices.DataBind();
        upnlSupplierProducts.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);

    }
    protected void imgBtnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        txtSearch.Text = "";
        tblSupplierProductPrices spp = new tblSupplierProductPrices();
        spp.getSupplierProductPrices(Convert.ToInt32(ddlEditSupplierProducts.SelectedValue));
        grdProductPrices.DataSource = spp.DefaultView;
        grdProductPrices.DataBind();
        if (spp.RowCount == 0)
            trFilter.Visible = false;
        upnlSupplierProducts.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
    }
    protected void imgBtnGraph_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string[] start_D = txtFromDatePopup.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
            start = Convert.ToDateTime(start).AddDays(-1).ToShortDateString();

            string[] end_D = txtEndDatePopup.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];
            end = Convert.ToDateTime(end).AddDays(1).ToShortDateString();

            tblSupplierProductPrices ssp2 = new tblSupplierProductPrices();
            ssp2.GetOldNewPriceAll_Filer(start, end, graph_supplierid, graph_relid);
            if (ssp2.RowCount > 0)
            {
                string xLast = string.Empty;
                string xFirst = string.Empty;
                int prevDay = 0;
                int days = 0;
                DateTime dtDate = ssp2.DModifiedDate;
                prevDay = ssp2.DModifiedDate.Day;
                double price = 0.0;

                for (int i = 0; i < ssp2.RowCount; i++)
                {
                    if (price < ssp2.Price)
                        price = ssp2.Price;

                    if (i == ssp2.RowCount - 1)
                    {
                        xLast = ssp2.DModifiedDate.Day.ToString() + "/" + ssp2.DModifiedDate.Month.ToString() + "/" + ssp2.DModifiedDate.Year.ToString();
                        xFirst = "1/1/" + (ssp2.DModifiedDate.Year - 1).ToString();
                        TimeSpan tr = ssp2.DModifiedDate.Subtract(dtDate);
                        days = Convert.ToInt32(tr.TotalDays);

                    }
                    ssp2.MoveNext();
                }

                LineChart c = new LineChart(900, 480, Page);
                c.SetXAxisLabel(xFirst, xLast);
                c.strTitle = "Price Range";

                c.ftXorigin = 0;
                c.ftScaleX = ssp2.RowCount;
                c.ftXdivs = ssp2.RowCount;
                int divs = 0;
                if (price > 0 && price < 31)
                    divs = Convert.ToInt32(price) / 3;
                else if (price > 30 && price < 51)
                    divs = Convert.ToInt32(price) / 5;
                else if (price > 50 && price < 101)
                    divs = Convert.ToInt32(price) / 10;
                else if (price > 100 && price < 151)
                    divs = Convert.ToInt32(price) / 15;
                else if (price > 150 && price < 201)
                    divs = Convert.ToInt32(price) / 20;
                else if (price > 200)
                    divs = Convert.ToInt32(price) / 30;


                c.ftYorigin = 0;
                c.ftScaleY = (float)price;
                c.ftYdivs = divs + 1;

                ssp2.FlushData();
                ssp2.GetOldNewPriceAll_Filer(start, end, graph_supplierid, graph_relid);
                if (ssp2.RowCount == 1)
                    c.AddValue(0, Convert.ToInt32(ssp2.Price));
                for (int i = 1; i <= ssp2.RowCount; i++)
                {
                    c.AddValue(i, Convert.ToInt32(ssp2.Price));
                    //prevDay = prevDay + ssp2.DModifiedDate.Day;
                    ssp2.MoveNext();
                }
                c.Draw();

                // System.Drawing.Image image = System.Drawing.Image.FromStream(Serialization.gStream);
                BinaryFormatter formatter = new BinaryFormatter();
                Serialization.gStream.Position = 0;
                Bitmap bt = (Bitmap)formatter.Deserialize(Serialization.gStream);
                bt.SetResolution(1024, 768);
                //System.Drawing.Image image =  System.Drawing.Image.FromStream(Serialization.gStream).GetPropertyItem; 
                string tempGUI = Guid.NewGuid().ToString();
                string sName = tempGUI.Substring(0, 5);
                //string direcroryPath = Server.MapPath("../Chart/");
                //if (Directory.Exists(direcroryPath))
                //{
                //    Directory.Delete(direcroryPath, true);
                //    Directory.CreateDirectory(direcroryPath);
                //}
                bt.Save(Server.MapPath("../Chart/chart" + sName + ".jpg"), ImageFormat.Jpeg);
                imgGraph.Src = "../Chart/chart" + sName + ".jpg";
                UpdatePanel1.Update();
                //((UpdatePanel)pnlPriceGraph.FindControl("UpdatePanel1")).Update();
            }
            else
            {
                imgGraph.Src = "../images/emptyGraph.png";
                UpdatePanel1.Update();

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){filter();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "dateM", "$(function(){loadDate();});", true);
        }
    }

    #endregion


    #region Supplier Order Detail

    protected void ddlSupplierOrders_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSupplierOrders.SelectedValue != "0")
        {
            // By Rehan
            lblInvoiceSum.Text = "00,00 €";
            lblNoInvoiceSum.Text = "00,00 €";
            lblTotalOrders.Text = "00,00 €";
            lblPaid.Text = "00,00 €";
            lblDue.Text = "00,00 €";
            // End By Rehan
            tblSupplier s = new tblSupplier();
            s.GetSupplierInfoForProductHistory(Convert.ToInt32(ddlSupplierOrders.SelectedValue));
            if (s.RowCount > 0)
            {
                if (s.GetColumn("sLogo").ToString() != "")
                    imgCompanyLogoO.Src = s.GetColumn("sLogo").ToString();
                else
                    imgCompanyLogoO.Src = "../Images/no_image.gif";
                lblCompanyBrandOrder.Text = s.GetColumn("sBrandName").ToString();
                lblCompanyAddressOrder.Text = s.GetColumn("sAddressStreet").ToString() + " " + s.GetColumn("sAddressTown").ToString() + " " + s.GetColumn("sAddressRegion").ToString() + " " + s.GetColumn("sAddressPostCode") + " " + s.GetColumn("sCountry").ToString();
                lblCompayPhoneEmailOrder.Text = "Fav. Phone: " + s.GetColumn("phone").ToString() + " " + "Fav. Email: " + s.GetColumn("sEmail").ToString() + " " + "Fav. FAX: " + s.GetColumn("sFax").ToString();
                lblCompanyContactOrder.Text = "Contact Person Name: " + s.GetColumn("pName").ToString() + " " + "Contact Person Phone and Email: " + s.GetColumn("cphone").ToString() + " " + s.GetColumn("cphone").ToString();
            }
            else
            {
                imgCompanyLogoO.Src = "../Images/no_image.gif";
                lblCompanyBrandOrder.Text = "";
                lblCompanyAddressOrder.Text = "";
                lblCompayPhoneEmailOrder.Text = "";
                lblCompanyContactOrder.Text = "";

            }
            GetAllOrder();
        }
        else
        {
            imgCompanyLogoO.Src = "../Images/no_image.gif";
            lblCompanyBrandOrder.Text = "";
            lblCompanyAddressOrder.Text = "";
            lblCompayPhoneEmailOrder.Text = "";
            lblCompanyContactOrder.Text = "";

            grdOrderDetail.DataSource = null;
            grdOrderDetail.DataBind();

            lblInvoiceSum.Text = "00,00 €";
            lblNoInvoiceSum.Text = "00,00 €";
            lblTotalOrders.Text = "00,00 €";
            lblPaid.Text = "00,00 €";
            lblDue.Text = "00,00 €";
        }
        upnlSupplierOrders.Update();

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);

    }

    private void GetAllOrder()
    {
        try
        {
            NonInvoiceSum = 0.0;
            invoiceSum = 0.0;
            tblBaseOrder bo = new tblBaseOrder();
            bo.GetUniqueOrder(Convert.ToInt32(ddlSupplierOrders.SelectedValue));
            grdOrderDetail.DataSource = bo.DefaultView;
            grdOrderDetail.DataBind();

            if (bo.RowCount > 0)
            {

                double totalOrder = 0.0;
                double paid = 0.0;
                double due = 0.0;

                for (int i = 0; i < bo.RowCount; i++)
                {
                    //if (bo.GetColumn("InvoiceAmount").ToString() != "")
                    //    invoiceSum += Convert.ToDouble(bo.GetColumn("subtotal"));
                    if (bo.GetColumn("subtotal").ToString() != "")
                    {
                        totalOrder += Convert.ToDouble(bo.GetColumn("subtotal"));
                        //invoiceSum += Convert.ToDouble(bo.GetColumn("subtotal"));
                    }
                    if (bo.GetColumn("paid").ToString() != "")
                        paid += Convert.ToDouble(bo.GetColumn("paid"));
                    if (bo.GetColumn("due").ToString() != "")
                        due += Convert.ToDouble(bo.GetColumn("due"));
                    bo.MoveNext();
                }

                //lblInvoiceSum.Text = commonMethods.ChangetToUK(invoiceSum.ToString("N")) + " €";
                lblTotalOrders.Text = commonMethods.ChangetToUK(totalOrder.ToString("N")) + " €";
                lblPaid.Text = commonMethods.ChangetToUK(paid.ToString("N")) + " €";
                lblDue.Text = commonMethods.ChangetToUK(due.ToString("N").Replace("-", "")) + " €";
            }
            else
            {
                lblInvoiceSum.Text = "00,00 €";
                lblNoInvoiceSum.Text = "00,00 €";
                lblTotalOrders.Text = "00,00 €";
                lblPaid.Text = "00,00 €";
                lblDue.Text = "00,00 €";
            }

            if (NonInvoiceSum != 0.0 || NonInvoiceSum != 0 || NonInvoiceSum != 0.00)
            {
                lblNoInvoiceSum.Text = commonMethods.ChangetToUK(NonInvoiceSum.ToString("N")) + " €";
            }
            if (invoiceSum != 0.0 || invoiceSum != 0 || invoiceSum != 0.00)
            {
                lblInvoiceSum.Text = commonMethods.ChangetToUK(invoiceSum.ToString("N")) + " €";
            }

            LoadFromTillDates();
        }
        catch (Exception ex)
        { }



    }
    private void LoadFromTillDates()
    {
        try
        {
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            DateTime low1Start = new DateTime();
            DateTime low1End = new DateTime();

            DateTime low2Start = new DateTime();
            DateTime low2End = new DateTime();

            DateTime highStart = new DateTime();
            DateTime highEnd = new DateTime();

            tblDepartments d = new tblDepartments();
            d.LoadByPrimaryKey(DepartmentID);
            if (d.RowCount > 0)
            {

                // For Low Season 1
                string[] low1 = d.LowSeason1.ToString().Split('-');
                low1Start = Convert.ToDateTime((low1[0].Split('/'))[1].ToString() + "/" + (low1[0].Split('/'))[0].ToString() + "/" + DateTime.Now.Year);
                low1End = Convert.ToDateTime((low1[1].Split('/'))[1].ToString() + "/" + (low1[1].Split('/'))[0].ToString() + "/" + DateTime.Now.Year);

                low1 = null;

                // For Low Season 2
                low1 = d.LowSeason2.ToString().Split('-');
                low2Start = Convert.ToDateTime((low1[0].Split('/'))[1].ToString() + "/" + (low1[0].Split('/'))[0].ToString() + "/" + DateTime.Now.Year);
                low2End = Convert.ToDateTime((low1[1].Split('/'))[1].ToString() + "/" + (low1[1].Split('/'))[0].ToString() + "/" + DateTime.Now.Year);

                low1 = null;

                // For High Season
                low1 = d.HighSeason.ToString().Split('-');
                highStart = Convert.ToDateTime((low1[0].Split('/'))[1].ToString() + "/" + (low1[0].Split('/'))[0].ToString() + "/" + DateTime.Now.Year);
                highEnd = Convert.ToDateTime((low1[1].Split('/'))[1].ToString() + "/" + (low1[1].Split('/'))[0].ToString() + "/" + DateTime.Now.Year);
            }

            if (DateTime.Now.Date >= low1Start || DateTime.Now.Date <= low1End)
            {

                txtFromDate.Text = low1Start.Day + "/" + low1Start.Month + "/" + low1Start.Year;
            }
            else if (DateTime.Now.Date >= low2Start || DateTime.Now.Date <= low2End)
            {
                txtFromDate.Text = low2Start.Day + "/" + low2Start.Month + "/" + low2Start.Year;
            }
            else if (DateTime.Now.Date >= highStart || DateTime.Now.Date <= highEnd)
            {
                txtFromDate.Text = highStart.Day + "/" + highStart.Month + "/" + highStart.Year;
            }
            txtTillDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        }
        catch (Exception ex)
        { }

    }


    protected void grdInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblInvoiceNum = e.Row.FindControl("lblInvoiceNum") as Label;
            Label lblInvSum = e.Row.FindControl("lblInvSum") as Label;
            Label lblNonInvSum = e.Row.FindControl("lblNonInvSum") as Label;

            if (lblNonInvSum.Text == "")
                lblNonInvSum.Text = "-";
            else
            {
                lblNonInvSum.Text = commonMethods.ChangetToUK(lblNonInvSum.Text) + " €";
                //if (lblInvoiceSum.Text != "")
                //{
                //    double value = Convert.ToDouble(lblInvoiceSum.Text.Replace(" €", ""));
                //    double newVaL = Convert.ToDouble(lblNonInvSum.Text.Replace(" €", ""));
                //    lblNonInvSum.Text = commonMethods.ChangetToUK((value + newVaL).ToString("N"));
                //}
            }

            if (lblInvoiceNum.Text == "")
            {
                lblInvoiceNum.Text = "-";
                lblInvSum.Text = "-";
            }
            else
            {
                lblNonInvSum.Text = "-";
                lblInvSum.Text = commonMethods.ChangetToUK(lblInvSum.Text) + " €";
            }
            try
            {
                if (DataBinder.GetPropertyValue(e.Row.DataItem, "NonInvoiceAmount").ToString() != "")
                {
                    //if (lblInvoiceSum.Text != "")
                    //{
                    //    double value = Convert.ToDouble(lblInvoiceSum.Text.Replace(" €",""));
                    //    double newVaL = Convert.ToDouble(DataBinder.GetPropertyValue(e.Row.DataItem, "NonInvoiceAmount").ToString());
                    //    lblNonInvSum.Text =commonMethods.ChangetToUK((value + newVaL).ToString("N"));
                    //}

                    lblNonInvSum.Text = commonMethods.ChangetToUK(DataBinder.GetPropertyValue(e.Row.DataItem, "NonInvoiceAmount").ToString()) + " €";

                }
            }
            catch (Exception ex)
            { }

            if (lblNonInvSum.Text != "")
            {
                NonInvoiceSum += commonMethods.ChangeToUS(lblNonInvSum.Text.Replace(" €", ""));
            }
            if (lblInvSum.Text != "")
            {
                invoiceSum += commonMethods.ChangeToUS(lblInvSum.Text.Replace(" €", ""));
            }

        }
    }
    protected void grdOrderDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            LinkButton lnkOrder = e.Row.FindControl("lnkOrder") as LinkButton;
            LinkButton lnkDate = e.Row.FindControl("lnkDate") as LinkButton;

            Label lblSubTotal = e.Row.FindControl("lblSubTotal") as Label;
            Label lblPaidAmount = e.Row.FindControl("lblPaidAmount") as Label;
            Label lblDueAmount = e.Row.FindControl("lblDueAmount") as Label;
            GridView grdInvoices = e.Row.FindControl("grdInvoices") as GridView;
            HtmlTable tblInvoices = e.Row.FindControl("tblInvoices") as HtmlTable;

            tblBaseOrder b = new tblBaseOrder();

            b.GetUniqueOrderForInvoices(Convert.ToInt32(ddlSupplierOrders.SelectedValue), Convert.ToInt32(drv["pkBaseOrderID"]));
            grdInvoices.DataSource = b.DefaultView;
            grdInvoices.DataBind();
            if (b.RowCount > 0)
            {
                if (b.RowCount == 2)
                {
                    bool checkInvoice = false;
                    for (int i = 0; i < b.RowCount; i++)
                    {
                        if (b.GetColumn("InvoiceNumber").ToString() == "")
                        {
                            checkInvoice = true;
                            break;
                        }
                        b.MoveNext();
                    }

                    if (checkInvoice)
                    {
                        DataRow[] dr = b.DefaultView.ToTable("Invoice", true, "InvoiceNumber", "InvoiceAmount").Select();
                        DataTable dtTemp = new DataTable();
                        dtTemp.Columns.Add("InvoiceNumber");
                        dtTemp.Columns.Add("InvoiceAmount");
                        dtTemp.Columns.Add("NonInvoiceAmount");


                        string InVoiceNum = string.Empty;
                        string InvoiceAmount = string.Empty;
                        string NonInvoiceAmount = string.Empty;

                        if (dr[0][0].ToString() != "")
                        {
                            InVoiceNum = dr[0][0].ToString();
                            InvoiceAmount = dr[0][1].ToString();
                        }
                        else
                        {
                            NonInvoiceAmount = dr[0][1].ToString();
                        }
                        if (dr[1][0].ToString() != "")
                        {
                            InVoiceNum = dr[1][0].ToString();
                            InvoiceAmount = dr[1][1].ToString();
                        }
                        else
                        {
                            NonInvoiceAmount = dr[1][1].ToString();
                        }


                        DataRow drTemp = dtTemp.NewRow();
                        drTemp[0] = InVoiceNum;
                        drTemp[1] = InvoiceAmount;
                        drTemp[2] = NonInvoiceAmount;
                        dtTemp.Rows.Add(drTemp);

                        grdInvoices.DataSource = dtTemp;
                        grdInvoices.DataBind();

                    }
                }

            }
            else
            {
                grdInvoices.DataSource = null;
                grdInvoices.DataBind();

                tblInvoices.Visible = true;
            }



            if (drv["dCreatedDate"].ToString() != "")
            {
                int day = Convert.ToDateTime(drv["dCreatedDate"].ToString()).Day;
                int month = Convert.ToDateTime(drv["dCreatedDate"].ToString()).Month;
                int year = Convert.ToDateTime(drv["dCreatedDate"].ToString()).Year;
                //lblDate.Text = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
                lnkDate.Text = day.ToString() + "/" + month.ToString() + "/" + year.ToString();


            }
            //lnkOrder.Text = lnkOrder.Text.Replace("-", "").Substring(0, 9);


            if (lblSubTotal.Text == "" || lblSubTotal.Text == "0")
                lblSubTotal.Text = "-";
            else
                lblSubTotal.Text = commonMethods.ChangetToUK(lblSubTotal.Text) + " €";

            if (lblPaidAmount.Text == "" || lblPaidAmount.Text == "0")
                lblPaidAmount.Text = "-";
            else
                lblPaidAmount.Text = commonMethods.ChangetToUK(lblPaidAmount.Text) + " €";

            if (lblDueAmount.Text == "" || lblDueAmount.Text == "0")
            {
                lblDueAmount.Text = "-";
                e.Row.Style.Add("color", "gray");


                lnkDate.Style.Add("color", "gray");

            }
            else
            {
                lblDueAmount.Text = commonMethods.ChangetToUK(lblDueAmount.Text) + " €";
                e.Row.Style.Add("font-weight", "bold");
                lnkDate.Style.Add("font-weight", "bold");
            }


        }

    }
    protected void grdOrderDetailPop_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;


            Label lnkOldPrice = e.Row.FindControl("lnkOldPrice") as Label;
            Label lblAfterVat = e.Row.FindControl("lblAfterVat") as Label;
            Label lblSubtotalPop = e.Row.FindControl("lblSubtotalPop") as Label;

            if (lnkOldPrice != null)
            {
                lnkOldPrice.Text = commonMethods.ChangetToUK(lnkOldPrice.Text) + " €";
                lblAfterVat.Text = lblAfterVat.Text + " %";
                lblSubtotalPop.Text = commonMethods.ChangetToUK(lblSubtotalPop.Text) + " €";
            }
        }
    }
    protected void grdOrderDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdOrderDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            LinkButton btn = e.CommandSource as LinkButton;
            Label lblDueAmount = btn.FindControl("lblDueAmount") as Label;
            int id = Convert.ToInt32(e.CommandArgument);
            tblBaseOrder b = new tblBaseOrder();
            switch (e.CommandName.ToLower())
            {
                case "order":
                    b.FlushData();
                    b.LoadByPrimaryKey(id);
                    if (b.RowCount > 0)
                    {
                        b.GetOrderDetail(b.SessionOrderID, Convert.ToInt32(ddlSupplierOrders.SelectedValue));
                        grdOrderDetailPop.DataSource = b.DefaultView;
                        grdOrderDetailPop.DataBind();
                        double sum = 0.0;
                        if (b.RowCount > 0)
                        {
                            for (int i = 0; i < b.RowCount; i++)
                            {
                                if (b.GetColumn("subtotal").ToString() != "")
                                    sum += Convert.ToDouble(b.GetColumn("subtotal"));
                                b.MoveNext();
                            }
                            lblTotal.Text = commonMethods.ChangetToUK(sum.ToString()) + " €";
                            string orderStatus = string.Empty;
                            if (lblDueAmount.Text == "-")
                            {
                                orderStatus = "SENT";
                                hidOrderStatus.Value = "SENT";
                                chkInvoicePop.Visible = false;
                                lblInvoicePop.Visible = false;
                            }
                            else
                            {
                                orderStatus = "DELIVERED";
                                hidOrderStatus.Value = "DELIVERED";
                                chkInvoicePop.Visible = true;
                                lblInvoicePop.Visible = true;
                            }
                            lblOrderOF.Text = orderStatus + " " + "Order of " + Convert.ToDateTime(b.GetColumn("DCreatedDate")).Day.ToString() + "/" + Convert.ToDateTime(b.GetColumn("DCreatedDate")).Month.ToString() + "/" + Convert.ToDateTime(b.GetColumn("DCreatedDate")).Year.ToString();
                        }
                        if (ddlSupplierOrders.SelectedValue != "0")
                        {
                            tblSupplier s = new tblSupplier();
                            s.GetSupplierInfoForProductHistory(Convert.ToInt32(ddlSupplierOrders.SelectedValue));
                            if (s.RowCount > 0)
                            {
                                if (s.GetColumn("sLogo").ToString() != "")
                                    imgCompanyLogoPop.Src = s.GetColumn("sLogo").ToString();
                                lblCompanyBrandPop.Text = s.GetColumn("sBrandName").ToString();
                                lblCompanyAddressPOp.Text = s.GetColumn("sAddressStreet").ToString() + " " + s.GetColumn("sAddressTown").ToString() + " " + s.GetColumn("sAddressRegion").ToString() + " " + s.GetColumn("sAddressPostCode") + " " + s.GetColumn("sCountry").ToString();
                                lblCompanyPhoneEmailPop.Text = "Fav. Phone: " + s.GetColumn("phone").ToString() + " " + "Fav. Email: " + s.GetColumn("sEmail").ToString() + " " + "Fav. FAX: " + s.GetColumn("sFax").ToString();
                                lblCompanyContactPop.Text = "Contact Person Name: " + s.GetColumn("pName").ToString() + " " + "Contact Person Phone and Email: " + s.GetColumn("cphone").ToString() + " " + s.GetColumn("cphone").ToString();
                            }
                            // GetAllOrder();
                        }
                        else
                        {
                            imgCompanyLogoP.Src = "../Images/no_image.gif";
                            lblCompanyBrandPop.Text = "";
                            lblCompanyAddressPOp.Text = "";
                            lblCompanyPhoneEmailPop.Text = "";
                            lblCompanyContactPop.Text = "";



                        }
                        ModalPopupExtender3.Show();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);
                    }


                    break;
            }
        }

    }
    protected void imgBtnFilterOrder_Click(object sender, ImageClickEventArgs e)
    {

        string[] start_D = txtFromDate.Text.Split('/'); // start Date
        string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
        start = Convert.ToDateTime(start).AddDays(-1).ToShortDateString();

        string[] end_D = txtTillDate.Text.Split('/'); // end date
        string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];
        end = Convert.ToDateTime(end).AddDays(1).ToShortDateString();

        tblBaseOrder b = new tblBaseOrder();
        b.GetOrderByFiler(start, end, Convert.ToInt32(ddlSupplierOrders.SelectedValue));
        grdOrderDetail.DataSource = b.DefaultView;
        grdOrderDetail.DataBind();

        if (b.RowCount > 0)
        {
            double invoiceSum = 0.0;
            double totalOrder = 0.0;
            double paid = 0.0;
            double due = 0.0;

            for (int i = 0; i < b.RowCount; i++)
            {
                //if (bo.GetColumn("InvoiceAmount").ToString() != "")
                //    invoiceSum += Convert.ToDouble(bo.GetColumn("subtotal"));
                if (b.GetColumn("subtotal").ToString() != "")
                {
                    totalOrder += Convert.ToDouble(b.GetColumn("subtotal"));
                    invoiceSum += Convert.ToDouble(b.GetColumn("subtotal"));
                }
                if (b.GetColumn("paid").ToString() != "")
                    paid += Convert.ToDouble(b.GetColumn("paid"));
                if (b.GetColumn("due").ToString() != "")
                    due += Convert.ToDouble(b.GetColumn("due"));
                b.MoveNext();
            }

            lblInvoiceSum.Text = commonMethods.ChangetToUK(invoiceSum.ToString("N")) + " €";
            lblTotalOrders.Text = commonMethods.ChangetToUK(totalOrder.ToString("N")) + " €";
            lblPaid.Text = commonMethods.ChangetToUK(paid.ToString("N")) + " €";
            lblDue.Text = commonMethods.ChangetToUK(due.ToString("N").Replace("-", "")) + " €";
        }
        else
        {
            lblInvoiceSum.Text = "00,00 €";
            lblTotalOrders.Text = "00,00 €";
            lblPaid.Text = "00,00 €";
            lblDue.Text = "00,00 €";
        }

        upnlSupplierOrders.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ShowDataPicker();});", true);

    }

    protected void imgBtnPdf_Click(object sender, ImageClickEventArgs e)
    {
        string date = string.Empty;
        string orderno = string.Empty;
        string OSubtoal = string.Empty;
        string OPaidAmount = string.Empty;
        string ODueAmount = string.Empty;

        string productName = string.Empty;
        string packingName = string.Empty;
        string quantityName = string.Empty;
        string price = string.Empty;
        string vat = string.Empty;
        string Qty = string.Empty;
        string sSubtotal = string.Empty;
        string sep = string.Empty;

        if (grdOrderDetail.Rows.Count > 0)
        {
            tblBaseOrder b = new tblBaseOrder();
            for (int i = 0; i < grdOrderDetail.Rows.Count; i++)
            {
                LinkButton lnkDate = grdOrderDetail.Rows[i].FindControl("lnkDate") as LinkButton;
                LinkButton lnkOrder = grdOrderDetail.Rows[i].FindControl("lnkOrder") as LinkButton;
                GridView grdInvoices = grdOrderDetail.Rows[i].FindControl("grdInvoices") as GridView;
                Label lblSubTotal = grdOrderDetail.Rows[i].FindControl("lblSubTotal") as Label;
                Label lblPaidAmount = grdOrderDetail.Rows[i].FindControl("lblPaidAmount") as Label;
                Label lblDueAmount = grdOrderDetail.Rows[i].FindControl("lblDueAmount") as Label;

                date += lnkDate.Text.ToString() + ",";
                orderno += lnkOrder.Text.ToString().Replace("-", "").Substring(0, 10) + ",";
                OSubtoal += lblSubTotal.Text + ":";
                OPaidAmount += lblPaidAmount.Text + ":";
                ODueAmount += lblDueAmount.Text + ":";

                //if (grdInvoices.Rows.Count > 0)
                //{
                //    for (int k = 0; k < grdInvoices.Rows.Count; k++)
                //    {

                //    }
                //}
                b.FlushData();
                b.GetOrderDetail(lnkOrder.Text, Convert.ToInt32(ddlSupplierOrders.SelectedValue));
                if (b.RowCount > 0)
                {
                    for (int j = 0; j < b.RowCount; j++)
                    {
                        productName += b.GetColumn("sProductName").ToString() + ",";
                        packingName += b.GetColumn("pName").ToString() + ",";
                        quantityName += b.GetColumn("qName").ToString() + ",";
                        price += b.GetColumn("ProudctPrice").ToString() + ":";
                        vat += b.GetColumn("vat").ToString() + ":";
                        Qty += b.GetColumn("Quantity").ToString() + ",";
                        sSubtotal += b.GetColumn("subtotal").ToString() + ":";
                        sep += lnkOrder.Text.ToString().Replace("-", "").Substring(0, 10) + ",";
                        b.MoveNext();
                    }
                }

            }

            //pdfhtml.SetOrderDetail(invoiceSum.ToString("N"), NonInvoiceSum.ToString("N"), ddlSupplierOrders.SelectedItem.Text, lblTotalOrders.Text, lblPaid.Text, lblDue.Text,
           //     orderno, date, ",", ":", ":", OSubtoal, OPaidAmount, ODueAmount, productName, packingName, quantityName, price, vat, Qty, sSubtotal, sep);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Print", "$(function(){window.open('../pdfGenerator.aspx?r=pdfo', 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');});", true);
        }






    }

    protected void imgBtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string date = string.Empty;
        string orderno = string.Empty;
        string OSubtoal = string.Empty;
        string OPaidAmount = string.Empty;
        string ODueAmount = string.Empty;

        string productName = string.Empty;
        string packingName = string.Empty;
        string quantityName = string.Empty;
        string price = string.Empty;
        string vat = string.Empty;
        string Qty = string.Empty;
        string sSubtotal = string.Empty;
        string sep = string.Empty;

        if (grdOrderDetail.Rows.Count > 0)
        {
            tblBaseOrder b = new tblBaseOrder();
            for (int i = 0; i < grdOrderDetail.Rows.Count; i++)
            {
                LinkButton lnkDate = grdOrderDetail.Rows[i].FindControl("lnkDate") as LinkButton;
                LinkButton lnkOrder = grdOrderDetail.Rows[i].FindControl("lnkOrder") as LinkButton;
                GridView grdInvoices = grdOrderDetail.Rows[i].FindControl("grdInvoices") as GridView;
                Label lblSubTotal = grdOrderDetail.Rows[i].FindControl("lblSubTotal") as Label;
                Label lblPaidAmount = grdOrderDetail.Rows[i].FindControl("lblPaidAmount") as Label;
                Label lblDueAmount = grdOrderDetail.Rows[i].FindControl("lblDueAmount") as Label;

                date += lnkDate.Text.ToString() + ",";
                orderno += lnkOrder.Text.ToString().Replace("-", "").Substring(0, 10) + ",";
                OSubtoal += lblSubTotal.Text + ":";
                OPaidAmount += lblPaidAmount.Text + ":";
                ODueAmount += lblDueAmount.Text + ":";

                //if (grdInvoices.Rows.Count > 0)
                //{
                //    for (int k = 0; k < grdInvoices.Rows.Count; k++)
                //    {

                //    }
                //}
                b.FlushData();
                b.GetOrderDetail(lnkOrder.Text, Convert.ToInt32(ddlSupplierOrders.SelectedValue));
                if (b.RowCount > 0)
                {
                    for (int j = 0; j < b.RowCount; j++)
                    {
                        productName += b.GetColumn("sProductName").ToString() + ",";
                        packingName += b.GetColumn("pName").ToString() + ",";
                        quantityName += b.GetColumn("qName").ToString() + ",";
                        price += b.GetColumn("ProudctPrice").ToString() + ":";
                        vat += b.GetColumn("vat").ToString() + ":";
                        Qty += b.GetColumn("Quantity").ToString() + ",";
                        sSubtotal += b.GetColumn("subtotal").ToString() + ":";
                        sep += lnkOrder.Text.ToString().Replace("-", "").Substring(0, 10) + ",";
                        b.MoveNext();
                    }
                }

            }

            pdfhtml.SetOrderDetail(invoiceSum.ToString("N"), NonInvoiceSum.ToString("N"), ddlSupplierOrders.SelectedItem.Text,lblTotalOrders.Text, lblPaid.Text, lblDue.Text,
                orderno, date, ",", ":", ":", OSubtoal, OPaidAmount, ODueAmount, productName, packingName, quantityName, price, vat, Qty, sSubtotal, sep);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Print", "$(function(){window.open('../pdfGenerator.aspx?r=printo', 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');});", true);
        }


    }
    #endregion


    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        Emailing email = new Emailing();
        email.P_FromAddress = senderEmail;
        email.P_ToAddress = receiverEmail;
        email.P_Email_Subject = txtSubject.Text;
        email.P_Message_Body = txtMessage.Text;
        email.Send_Email();
        ModalPopupExtender1.Hide();
    }


    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgBtnCancel_Click1(object sender, ImageClickEventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
        imgBtnAddContactPeople.Visible = true;
        trAddContactPeople.Visible = false;
        tblGrdContactPeople.Visible = true;



    }

    protected void imgBtnAddSupplierProducts_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnAddSupplierProducts.Visible = false;
        imgBtnAddSupplier.Visible = false;
        txtProductPrice.Text = "";
        lnkBackToSpplier.Visible = true;
        mvTab1.SetActiveView(vAddSupplierProduct);

        LoadProductSuppliers();
    }
    protected void imgBtnSaveSupplierProducts_Click(object sender, ImageClickEventArgs e)
    {
        tblSupplierProducts sp = new tblSupplierProducts();
        sp.FlushData();
        sp.GetSupplierProducts(Convert.ToInt32(ddlProducts.SelectedValue), Convert.ToInt32(ddlSuppliersForProduct.SelectedValue));
        if (sp.RowCount > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", " alert('This product is already tied to selected supplier.');", true);
        }
        else
        {
            sp.AddNew();
            sp.FkProductID = Convert.ToInt32(ddlProducts.SelectedValue);
            sp.FkSupplierID = Convert.ToInt32(ddlSuppliersForProduct.SelectedValue);
            sp.UnitPrice = commonMethods.ChangeToUS(txtProductPrice.Text);
            sp.DModifiedDate = DateTime.Now;
            sp.DCreatedDate = DateTime.Now;
            sp.Save();
            imgBtnAddSupplierProducts.Visible = true;
            imgBtnAddSupplier.Visible = true;
            lnkBackToSpplier.Visible = false;
            mvTab1.SetActiveView(vGrdSuppliers);
        }

    }

    private void loadSupplierProducts(int spid)
    {
        tblSupplierProducts sp = new tblSupplierProducts();
        sp.GetSupplierProductsBySupplier(spid);
        grdSupplierProducts.DataSource = sp.DefaultView;
        grdSupplierProducts.DataBind();
        mvTab3.SetActiveView(vSupplierProducts);
        if (sp.RowCount > 0)
        {
            lblSupplierProductMessage.Visible = false;
        }
        else
        {
            lblSupplierProductMessage.Visible = true;

        }
    }
    private void LoadEditProductSuppliers()
    {
        tblSupplier sup = new tblSupplier();
        sup.GetDepartmentSuppliers(DepartmentID);
        if (sup.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlEditSupplierProducts, sup.DefaultView, "SBrandName", "PkSupplierID");
            ddlEditSupplierProducts.Items.Insert(0, new ListItem("Select suppliers", "0"));

            commonMethods.FillDropDownList(ddlSupplierOrders, sup.DefaultView, "SBrandName", "PkSupplierID");
            ddlSupplierOrders.Items.Insert(0, new ListItem("Select suppliers", "0"));
            upnlSupplierOrders.Update();
            upnlSupplierProducts.Update();
        }
    }
    private void LoadProductSuppliers()
    {
        //string bv = string.Empty;
        //char [] c = lblVertialBaseName.Text.ToCharArray();
        //for (int i = 0; i < c.Length; i++)
        //{
        //    bv += c[i].ToString() + "<br/>";
        //}
        //lblVertialBaseName.Text = bv;

        tblBaseCategories bCat = new tblBaseCategories();
        bCat.GetDepartmentBaseCategories(DepartmentID);
        if (bCat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlBaseCategories, bCat.DefaultView, "CatagoryName", "PkBaseCategoryID");
            commonMethods.FillDropDownList(ddlBaseCategoryIndividual, bCat.DefaultView, "CatagoryName", "PkBaseCategoryID");
            ddlBaseCategoryIndividual.Items.Insert(0, new ListItem("Select Base Category", "0"));

            tblSupplierBaseCats sbCat = new tblSupplierBaseCats();
            sbCat.GetSupplierNamesForBase(Convert.ToInt32(ddlBaseCategories.SelectedValue));
            dtlSuppliersNamesForBaseCategory.DataSource = sbCat.DefaultView;
            dtlSuppliersNamesForBaseCategory.DataBind();



            tblSubCategories sub = new tblSubCategories();
            sub.GetSubCategories(Convert.ToInt32(ddlBaseCategories.SelectedValue));
            if (sub.RowCount > 0)
            {
                ddlSubCategories.Items.Clear();
                commonMethods.FillDropDownList(ddlSubCategories, sub.DefaultView, "CSubCategoryName", "PkSubCategoryID");
                //ddlSubCategories.Items.Insert(0, new ListItem("Select Sub Category", "0"));

                tblSupplierSubCats ssCat = new tblSupplierSubCats();
                ssCat.GetSupplierNamesForSub(Convert.ToInt32(ddlSubCategories.SelectedValue));
                dtlSupplierNamesForSubcategory.DataSource = ssCat.DefaultView;
                dtlSupplierNamesForSubcategory.DataBind();
            }

        }
        else
        {
            ddlBaseCategories.Items.Clear();
            //ddlBaseCategories.Items.Insert(0, new ListItem("Select Base Category", "0"));
        }

        ddlSubCategories.Items.Clear();
        //ddlSubCategories.Items.Insert(0, new ListItem("Select Sub Category", "0"));

        ddlProductsIndividual.Items.Clear();
        ddlProductsIndividual.Items.Insert(0, new ListItem("Select Product", "0"));
        upnlProductLinking.Update();
    }

    //protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlProducts.SelectedValue != "0")
    //    {
    //        tblSupplierProducts p = new tblSupplierProducts();

    //    }
    //}

    protected void grdSupplierProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {

            int id = Convert.ToInt32(e.CommandArgument);
            tblSupplierProducts sp = new tblSupplierProducts();
            switch (e.CommandName.ToLower())
            {
                case "del":
                    sp.FlushData();
                    sp.LoadByPrimaryKey(id);
                    if (sp.RowCount > 0)
                    {
                        sp.MarkAsDeleted();
                        sp.Save();
                        if (ddlEditSupplierProducts.SelectedValue != "0")
                            loadSupplierProducts(Convert.ToInt32(ddlEditSupplierProducts.SelectedValue));
                        else
                            loadSupplierProducts(Convert.ToInt32(ViewState["Supplierid"]));
                        //upnlSupplierProducts.Update();
                    }
                    break;

                case "name":
                    sp.FlushData();
                    sp.LoadByPrimaryKey(id);
                    if (sp.RowCount > 0)
                    {

                        SupplierProductID = sp.PkSupplierProductID;
                        txtPrice.Text = sp.UnitPrice.ToString();

                        lblSupplierProductMessage.Visible = false;
                        mvTab3.SetActiveView(vEditSupplierProduct);
                    }

                    break;
            }
        }
    }
    protected void imgBtnSaveEditPrice_Click(object sender, ImageClickEventArgs e)
    {

        if (SupplierProductID != 0)
        {
            tblSupplierProducts sp = new tblSupplierProducts();
            sp.LoadByPrimaryKey(SupplierProductID);
            if (sp.RowCount > 0)
            {
                sp.UnitPrice = Convert.ToDouble(txtPrice.Text);
                sp.DModifiedDate = DateTime.Now;
                sp.Save();
                loadSupplierProducts(sp.FkSupplierID);
            }
        }

        mvTab3.SetActiveView(vSupplierProducts);

    }

}
