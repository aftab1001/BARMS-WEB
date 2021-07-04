using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MyGeneration.dOOdads;
using LC.Model.BMS.BLL;


public partial class DepartmentAdmin_Products : System.Web.UI.Page
{

    int UserID;
    int DepartmentID;
    static int BaseCatID = 0;
    static int SubCatID = 0;
    static int ProductID = 0;
    static HiddenField hidPQR; // for Packing Quantity Relationship id

    static int fkBasetCatID = 0;
    static int fkSubCatID = 0;
    static int PackingID = 0;
    static int QuantityID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserLogin"] != null)
            {
                SessionUser user = new SessionUser();
                user = (SessionUser)Session["UserLogin"];

                if (user.AccessLevel != 5)
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


                LoadBaseCategories();
                GetProducts();
                LoadPackingForProduct();
                LoadQuantityForProduct();
                LoadVatForProduct();
                LoadBaseCategoriesForProduct();


                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString[0].ToString() != "" && Request.QueryString[1].ToString() != "" && Request.QueryString[0].ToString() != "")
                    {
                        int pid = Convert.ToInt32(Request.QueryString[0]);
                        int packid = Convert.ToInt32(Request.QueryString[1]);
                        int relid = Convert.ToInt32(Request.QueryString[2]);
                        LoadQueryStringProduct(pid, packid, relid);

                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Products.aspx");
        }
    }
    private void LoadQueryStringProduct(int pid, int packid, int relid)
    {
        try
        {
            hidPQR = new HiddenField();
            hidPQR.Value = relid.ToString();
            imgBtnFilterProduct.Visible = true;
            LoadVatForProduct();

            tblProducts pro = new tblProducts();
            pro.LoadByPrimaryKey(pid);

            txtProduct.Text = pro.SProductName;

            tblSubCategories sub = new tblSubCategories();
            sub.LoadByPrimaryKey(pro.FkSubCategoryID);
            if (sub.RowCount > 0)
                ddlBaseCat_vAddProducts.SelectedValue = sub.FkBaseCategoryID.ToString();

            sub.FlushData();
            sub.GetActiveSubCat(Convert.ToInt32(ddlBaseCat_vAddProducts.SelectedValue));
            ddlSubCat_vAddProducts.Items.Clear();
            if (sub.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlSubCat_vAddProducts, sub.DefaultView, "CSubCategoryName", "PkSubCategoryID");

                ddlSubCat_vAddProducts.SelectedValue = pro.FkSubCategoryID.ToString();
            }
            ddlSubCat_vAddProducts.Items.Insert(0, new ListItem("Sub Category", "0"));

            //ddlVatPro.SelectedValue = pro.FkVatid.ToString();


            ProductID = pro.PkProductID;



            tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
            rel.LoadByPrimaryKey(relid);
            if (rel.RowCount > 0)
            {
                ddlVatPro.SelectedValue = rel.FkVatID.ToString();
            }
            try
            {
                ddlPacking_vAddProduct.SelectedValue = rel.FkProductPackageID.ToString();
            }
            catch (InvalidCastException ex)
            {

            }
            try
            {
                // ddlQuantity_vAddProduct.SelectedValue = rel.FkProductQuantityID.ToString();
            }
            catch (InvalidCastException ex)
            {
            }
            imgBtnFilter.Visible = false;
            trAddProduct.Visible = true;
            imgBtnSaveProduct.ImageUrl = "../Images/btn_edit.png";
            imgBtnCancelProduct.ImageUrl = "../Images/btn_cancel.png";
            imgBtnSaveProduct.Visible = true;
            trLineBeforeProduct.Visible = true;
            trLineAfterProduct.Visible = false;
        }
        catch (Exception ex)
        { }
    }

    #region Base Category

    private void LoadBaseCategories()
    {
        tblBaseCategories bCat = new tblBaseCategories();
        bCat.GetDepartmentBaseCategories(DepartmentID);
        grdBaseCategories.DataSource = bCat.DefaultView;
        grdBaseCategories.DataBind();
    }
    protected void imgBtnAddBaseCategory_Click(object sender, ImageClickEventArgs e)
    {
        trLineBeforeBase.Visible = false;
        trLineAfterBase.Visible = true;
        imgBtnSaveBase.ImageUrl = "../Images/btn_save_account.png";
        trAddBase.Visible = true;
        txtBaseCate.Text = "";

        BaseCatID = 0;
    }
    protected void imgBtnSaveBase_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblBaseCategories baseCat = new tblBaseCategories();

            if (BaseCatID == 0)
            {
                baseCat.AddNew();
                baseCat.DCreatedDate = DateTime.Now;
                baseCat.FkDepartmentID = DepartmentID;
                baseCat.IsActive = true;

                lblMessageProduct.Text = "Successfully Added!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
            else
            {
                baseCat.FlushData();
                baseCat.LoadByPrimaryKey(BaseCatID);

                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
            baseCat.CatagoryName = txtBaseCate.Text;
            baseCat.BDescription = txtBaseDescription.Text;
            baseCat.FkVatID = Convert.ToInt32(ddlVat.SelectedValue);
            baseCat.DModifiedDate = DateTime.Now;
            baseCat.Save();
            txtBaseCate.Text = "";
            LoadBaseCategories();
            trAddBase.Visible = false;
            imgBtnAddBaseCategory.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdBaseCategories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {

            int id = Convert.ToInt32(e.CommandArgument);
            tblBaseCategories baseCat = new tblBaseCategories();
            switch (e.CommandName.ToLower())
            {
                case "name":
                    baseCat.FlushData();
                    baseCat.LoadByPrimaryKey(id);
                    if (baseCat.RowCount > 0)
                    {
                        txtBaseCate.Text = baseCat.CatagoryName;
                        txtBaseDescription.Text = baseCat.BDescription;
                        BaseCatID = baseCat.PkBaseCategoryID;
                        ddlVat.SelectedValue = baseCat.FkVatID.ToString();
                        trAddBase.Visible = true;
                        imgBtnSaveBase.ImageUrl = "../Images/btn_edit_admin.png";
                        trLineBeforeBase.Visible = true;
                        trLineAfterBase.Visible = false;
                    }
                    break;
                case "addsub":
                    mvMain.SetActiveView(vAddSub);
                    baseCat.FlushData();
                    baseCat.LoadByPrimaryKey(id);
                    if (baseCat.RowCount > 0)
                    {

                        fkBasetCatID = baseCat.PkBaseCategoryID;
                        GetSubCategories();
                    }
                    break;
                case "del":
                    baseCat.FlushData();
                    baseCat.LoadByPrimaryKey(id);
                    if (baseCat.RowCount > 0)
                    {
                        tblSubCategories subcatDel = new tblSubCategories();
                        subcatDel.GetSubCategories(baseCat.PkBaseCategoryID);
                        if (subcatDel.RowCount > 0)
                        {
                            tblProducts productDel = new tblProducts();
                            for (int i = 0; i < subcatDel.RowCount; i++)
                            {
                                productDel.FlushData();
                                productDel.GetProducts(subcatDel.PkSubCategoryID);
                                if (productDel.RowCount > 0)
                                {
                                    for (int j = 0; j < productDel.RowCount; j++)
                                    {
                                        productDel.MarkAsDeleted();
                                        productDel.Save();
                                        productDel.MoveNext();
                                    }
                                }
                                subcatDel.MarkAsDeleted();
                                subcatDel.Save();
                                subcatDel.MoveNext();
                            }
                        }

                        baseCat.MarkAsDeleted();
                        baseCat.Save();
                        LoadBaseCategories();
                    }
                    break;
            }
        }
    }
    protected void grdBaseCategories_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            CheckBox chkActive = e.Row.FindControl("chkActive") as CheckBox;
            if (!Convert.ToBoolean(drv["isActive"].ToString()))
                chkActive.Checked = false;
            else
                chkActive.Checked = true;
        }
    }

    #endregion

    #region Sub Categories

    private void GetSubCategories()
    {
        tblSubCategories sub = new tblSubCategories();
        sub.GetSubCategories(fkBasetCatID);
        grdSubCategory.DataSource = sub.DefaultView;
        grdSubCategory.DataBind();
    }
    protected void imgBtnAddSub_Click(object sender, ImageClickEventArgs e)
    {
        BindBase();
        GetAllVat();
        trAddSub.Visible = true;

        trLineBeforeSub.Visible = false;
        trLineAfterSub.Visible = true;
        txtSubCat.Text = "";
        txtdescriptionSub.Text = "";
        imgBtnSaveSub.ImageUrl = "../Images/btn_save_account.png";
        SubCatID = 0;
    }
    protected void imgBtnSaveSub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblSubCategories sub = new tblSubCategories();
            if (SubCatID == 0)
            {
                sub.FlushData();
                sub.AddNew();
                sub.IsActive = true;
                sub.DCreatedDate = DateTime.Now;


                lblMessageProduct.Text = "Successfully Added!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
            else
            {
                sub.FlushData();
                sub.LoadByPrimaryKey(SubCatID);

                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
            sub.FkBaseCategoryID = fkBasetCatID;
            sub.CSubCategoryName = txtSubCat.Text;

            sub.FkBaseCategoryID = Convert.ToInt32(ddlBase_vAddSub.SelectedValue);

            sub.SDescription = txtdescriptionSub.Text;
            sub.FkVatID = Convert.ToInt32(ddlVatSub.SelectedValue);
            sub.DModifiedDate = DateTime.Now;
            sub.Save();
            GetSubCategories();
            imgBtnAddSub.Visible = true;
            trAddSub.Visible = false;
            BindBase();
            GetAllVat();
            GetAllSubCategories();
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdSubCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {

            int id = Convert.ToInt32(e.CommandArgument);
            tblSubCategories sub = new tblSubCategories();
            switch (e.CommandName.ToLower())
            {
                case "name":
                    sub.FlushData();
                    sub.LoadByPrimaryKey(id);
                    if (sub.RowCount > 0)
                    {
                        txtSubCat.Text = sub.CSubCategoryName;
                        SubCatID = sub.PkSubCategoryID;
                        ddlBase_vAddSub.SelectedValue = sub.FkBaseCategoryID.ToString();
                        if (sub.FkVatID != 0)
                            ddlVatSub.SelectedValue = sub.FkVatID.ToString();
                        txtdescriptionSub.Text = sub.SDescription;
                        trAddSub.Visible = true;
                        trLineBeforeSub.Visible = true;
                        trLineAfterSub.Visible = false;
                        imgBtnSaveSub.ImageUrl = "../Images/btn_edit_admin.png";
                    }
                    break;
                case "addproduct":
                    mvMain.SetActiveView(vAddProduct);
                    sub.FlushData();
                    sub.LoadByPrimaryKey(id);
                    if (sub.RowCount > 0)
                    {
                        //lblSubCat.Text = sub.CSubCategoryName;
                        fkSubCatID = sub.PkSubCategoryID;
                        GetProducts();
                    }
                    break;
                case "del":
                    sub.FlushData();
                    sub.LoadByPrimaryKey(id);
                    if (sub.RowCount > 0)
                    {
                        tblProducts productDel = new tblProducts();
                        productDel.GetProducts(sub.PkSubCategoryID);
                        if (productDel.RowCount > 0)
                        {
                            for (int j = 0; j < productDel.RowCount; j++)
                            {
                                productDel.MarkAsDeleted();
                                productDel.Save();
                                productDel.MoveNext();
                            }
                        }

                        sub.MarkAsDeleted();
                        sub.Save();
                        GetSubCategories();
                    }
                    break;
            }
        }

    }
    protected void grdSubCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            CheckBox chkActive = e.Row.FindControl("chkActiveSub") as CheckBox;
            HiddenField hidSub = e.Row.FindControl("hidSub") as HiddenField;
            Label lblVATSub = e.Row.FindControl("lblVATSub") as Label;


            tblSubCategories sub = new tblSubCategories();
            sub.LoadByPrimaryKey(Convert.ToInt32(hidSub.Value));
            if (sub.RowCount > 0)
            {
                if (sub.FkVatID != 0)
                {
                    tblVAT vat = new tblVAT();
                    vat.LoadByPrimaryKey(sub.FkVatID);
                    if (vat.RowCount > 0)
                    {
                        lblVATSub.Text = vat.Vat.ToString();
                    }
                }
            }



            if (!Convert.ToBoolean(drv["isActive"].ToString()))
                chkActive.Checked = false;
            else
                chkActive.Checked = true;


        }
    }
    private void BindBase()
    {
        tblBaseCategories b = new tblBaseCategories();
        b.GetAllBase(DepartmentID);
        ddlBase_vAddSub.Items.Clear();
        if (b.RowCount > 0)
            commonMethods.FillDropDownList(ddlBase_vAddSub, b.DefaultView, "CatagoryName", "pkBaseCategoryID");
        ddlBase_vAddSub.Items.Insert(0, new ListItem("Select Base Category", "0"));
    }
    protected void lnkResetSubcategoryVat_Click(object sender, EventArgs e)
    {
        tblSubCategories sub = new tblSubCategories();
        sub.GetAllSubCats(DepartmentID);
        if (sub.RowCount > 0)
        {
            for (int i = 0; i < sub.RowCount; i++)
            {
                sub.FkVatID = 0;
                sub.DModifiedDate = DateTime.Now;
                sub.Save();
                sub.MoveNext();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }

            GetAllSubCategories();
        }
    }
    #endregion

    #region Products

    private void LoadPackingForProduct()
    {
        try
        {
            tblProductPackages proPack = new tblProductPackages();
            proPack.GetPackingOptions_Active();
            ddlPacking_vAddProduct.Items.Clear();
            if (proPack.RowCount > 0)
            {
                ddlPacking_vAddProduct.Items.Clear();
                for (int i = 0; i < proPack.RowCount; i++)
                {
                    ddlPacking_vAddProduct.Items.Add(new ListItem(proPack.PName + " " + proPack.QName, proPack.PkProductPackageID.ToString()));

                    proPack.MoveNext();
                }
            }
            ddlPacking_vAddProduct.Items.Insert(0, new ListItem("Packing & Quantity", "0"));
        }
        catch (Exception ex)
        { }
    }
    private void LoadQuantityForProduct()
    {
        tblProductQuantities proQuantity = new tblProductQuantities();
        proQuantity.LoadAll();
        ddlQuantity_vAddProduct.Items.Clear();
        if (proQuantity.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlQuantity_vAddProduct, proQuantity.DefaultView, "qName", "pkProductQuantityID");
        }
        ddlQuantity_vAddProduct.Items.Insert(0, new ListItem("Quantity", "0"));
    }

    private void LoadVatForProduct()
    {
        tblVAT v = new tblVAT();
        v.LoadAll();
        ddlVatPro.Items.Clear();
        if (v.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlVatPro, v.DefaultView, "Vat", "PkVatID");
        }
        ddlVatPro.Items.Insert(0, new ListItem("Vat", "0"));
    }
    private void GetProducts()
    {
        tblProducts prod = new tblProducts();
        prod.GetProductsForGrid(DepartmentID);
        grdProducts.DataSource = prod.DefaultView;
        grdProducts.DataBind();
    }
    private void LoadBaseCategoriesForProduct()
    {
        tblBaseCategories b = new tblBaseCategories();
        b.GetAllBase(DepartmentID);
        ddlBaseCat_vAddProducts.Items.Clear();
        if (b.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlBaseCat_vAddProducts, b.DefaultView, "CatagoryName", "PkBaseCategoryID");
        }
        ddlBaseCat_vAddProducts.Items.Insert(0, new ListItem("Base Category", "0"));

        ddlSubCat_vAddProducts.Items.Clear();
        ddlSubCat_vAddProducts.Items.Insert(0, new ListItem("Sub Category", "0"));
    }
    protected void imgBtnAddProduct_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnFilter.Visible = false;
        imgBtnFilterProduct.Visible = true;
        trLineBeforeProduct.Visible = false;
        trLineAfterProduct.Visible = true;
        trAddProduct.Visible = true;

        imgBtnCancelProduct.ImageUrl = "../Images/btn_cancel.png";
        imgBtnSaveProduct.ImageUrl = "../Images/btn_save_account.png";
        imgBtnSaveProduct.Visible = true;
        txtProduct.Text = "";
        ProductID = 0;
        GetProducts();
        LoadBaseCategoriesForProduct();
        LoadPackingForProduct();
        LoadQuantityForProduct();
        LoadVatForProduct();
    }
    protected void imgBtnSaveProduct_Click(object sender, ImageClickEventArgs e)
    {

        TransactionMgr tx = TransactionMgr.ThreadTransactionMgr();
        try
        {
            tx.BeginTransaction();
            tblProducts pro = new tblProducts();
            if (ProductID == 0)
            {
                #region Adding Product

                tblProducts p = new tblProducts();
                p.checkProductExist(txtProduct.Text.Trim(), Convert.ToInt32(ddlSubCat_vAddProducts.SelectedValue));
                if (p.RowCount > 0)
                {
                    int pid = p.PkProductID;
                    p.FlushData();
                    p.checkProduct_Packing_Quantity(pid, Convert.ToInt32(ddlPacking_vAddProduct.SelectedValue));
                    if (p.RowCount > 0)
                    {
                        string message = "This product is already exists with the given packing and quantity.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
                        return;
                    }
                    else
                    {
                        tblProductPackingQuantityRel pqrel = new tblProductPackingQuantityRel();
                        pqrel.AddNew();
                        pqrel.FkProductID = pid;
                        pqrel.FkProductPackageID = Convert.ToInt32(ddlPacking_vAddProduct.SelectedValue);
                        pqrel.FkVatID = Convert.ToInt32(ddlVatPro.SelectedValue);
                        pqrel.IsActive = true;
                        pqrel.DModifiedDate = DateTime.Now;
                        pqrel.DCreatedDate = DateTime.Now;
                        pqrel.Save();

                        lblMessageProduct.Text = "Successfully Added!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
                    }
                }
                else
                {
                    pro.FlushData();
                    pro.AddNew();
                    pro.FkSubCategoryID = Convert.ToInt32(ddlSubCat_vAddProducts.SelectedValue);
                    pro.FkVatid = Convert.ToInt32(ddlVatPro.SelectedValue);
                    pro.SProductName = txtProduct.Text;
                    pro.DCreatedDate = DateTime.Now;
                    pro.DModifiedDate = DateTime.Now;
                    pro.Save();

                    tblProductPackingQuantityRel pqrel = new tblProductPackingQuantityRel();
                    pqrel.AddNew();
                    pqrel.FkProductID = pro.PkProductID;
                    pqrel.FkProductPackageID = Convert.ToInt32(ddlPacking_vAddProduct.SelectedValue);
                    pqrel.FkProductQuantityID = Convert.ToInt32(ddlQuantity_vAddProduct.SelectedValue);
                    pqrel.FkVatID = Convert.ToInt32(ddlVatPro.SelectedValue);
                    pqrel.IsActive = true;
                    pqrel.DModifiedDate = DateTime.Now;
                    pqrel.DCreatedDate = DateTime.Now;
                    pqrel.Save();


                    lblMessageProduct.Text = "Successfully Added!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
                }

                trAddProduct.Visible = false;
                trLineBeforeProduct.Visible = true;
                trLineAfterProduct.Visible = false;
                GetProducts();
                #endregion
            }
            else
            {
                #region Updating Product
                pro.FlushData();
                pro.LoadByPrimaryKey(ProductID);
                if (pro.RowCount > 0)
                {
                    tblProducts p = new tblProducts();
                    p.checkProductExist_for_Updating(pro.PkProductID, txtProduct.Text.Trim());
                    if (p.RowCount > 0)
                    {
                        string message = "Product with given already exists. Please enter different name.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
                        return;
                    }
                    else
                    {
                        p.FlushData();
                        p.checkProduct_Packing_Quantity_for_updating(Convert.ToInt32(hidPQR.Value), pro.PkProductID, Convert.ToInt32(ddlPacking_vAddProduct.SelectedValue));
                        if (p.RowCount > 0)
                        {
                            string message = "This product is already exists with the given packing and quantity.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
                            return;
                        }
                        else
                        {
                            pro.SProductName = txtProduct.Text;
                            pro.FkSubCategoryID = Convert.ToInt32(ddlSubCat_vAddProducts.SelectedValue);
                            pro.FkVatid = Convert.ToInt32(ddlVatPro.SelectedValue);
                            pro.DModifiedDate = DateTime.Now;
                            pro.Save();

                            tblProductPackingQuantityRel pqrel = new tblProductPackingQuantityRel();
                            pqrel.LoadByPrimaryKey(Convert.ToInt32(hidPQR.Value));
                            pqrel.FkProductID = pro.PkProductID;
                            pqrel.FkProductPackageID = Convert.ToInt32(ddlPacking_vAddProduct.SelectedValue);
                            pqrel.FkVatID = Convert.ToInt32(ddlVatPro.SelectedValue);
                            pqrel.IsActive = true;
                            pqrel.DModifiedDate = DateTime.Now;
                            pqrel.DCreatedDate = DateTime.Now;
                            pqrel.Save();


                            lblMessageProduct.Text = "Successfully Updated!";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);

                            trAddProduct.Visible = false;
                            trLineBeforeProduct.Visible = true;
                            trLineAfterProduct.Visible = false;
                            GetProducts();
                        }

                    }
                }
                #endregion
            }

            tx.CommitTransaction();
        }
        catch (Exception ex)
        {

            tx.RollbackTransaction();
            TransactionMgr.ThreadTransactionMgrReset();
        }
    }
    protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {

            int id = Convert.ToInt32(e.CommandArgument);
            tblProducts pro = new tblProducts();
            switch (e.CommandName.ToLower())
            {
                case "name":
                    pro.FlushData();
                    pro.LoadByPrimaryKey(id);
                    if (pro.RowCount > 0)
                    {
                        imgBtnFilterProduct.Visible = true;
                        LoadVatForProduct();
                        txtProduct.Text = pro.SProductName;

                        tblSubCategories sub = new tblSubCategories();
                        sub.LoadByPrimaryKey(pro.FkSubCategoryID);
                        if (sub.RowCount > 0)
                            ddlBaseCat_vAddProducts.SelectedValue = sub.FkBaseCategoryID.ToString();

                        sub.FlushData();
                        sub.GetActiveSubCat(Convert.ToInt32(ddlBaseCat_vAddProducts.SelectedValue));
                        ddlSubCat_vAddProducts.Items.Clear();
                        if (sub.RowCount > 0)
                        {
                            commonMethods.FillDropDownList(ddlSubCat_vAddProducts, sub.DefaultView, "CSubCategoryName", "PkSubCategoryID");

                            ddlSubCat_vAddProducts.SelectedValue = pro.FkSubCategoryID.ToString();
                        }
                        ddlSubCat_vAddProducts.Items.Insert(0, new ListItem("Sub Category", "0"));

                        //ddlVatPro.SelectedValue = pro.FkVatid.ToString();


                        ProductID = pro.PkProductID;


                        LinkButton btn = e.CommandSource as LinkButton;
                        hidPQR = btn.FindControl("hidPQR") as HiddenField;
                        tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
                        rel.LoadByPrimaryKey(Convert.ToInt32(hidPQR.Value));
                        if (rel.RowCount > 0)
                        {
                            ddlVatPro.SelectedValue = rel.FkVatID.ToString();
                        }
                        try
                        {
                            ddlPacking_vAddProduct.SelectedValue = rel.FkProductPackageID.ToString();
                        }
                        catch (InvalidCastException ex)
                        {

                        }
                        try
                        {
                            // ddlQuantity_vAddProduct.SelectedValue = rel.FkProductQuantityID.ToString();
                        }
                        catch (InvalidCastException ex)
                        {
                        }
                        imgBtnFilter.Visible = false;
                        trAddProduct.Visible = true;
                        imgBtnSaveProduct.ImageUrl = "../Images/btn_edit.png";
                        imgBtnCancelProduct.ImageUrl = "../Images/btn_cancel.png";
                        imgBtnSaveProduct.Visible = true;
                        trLineBeforeProduct.Visible = true;
                        trLineAfterProduct.Visible = false;
                    }
                    break;

                case "del":
                    pro.FlushData();
                    pro.LoadByPrimaryKey(id);
                    if (pro.RowCount > 0)
                    {
                        pro.MarkAsDeleted();
                        pro.Save();
                        GetProducts();
                    }
                    break;
            }
        }
    }
    protected void imgBtnUpdateProduct_Click(object sender, ImageClickEventArgs e)
    {
        if (ProductID != 0)
        {
            tblProducts p = new tblProducts();
            p.LoadByPrimaryKey(ProductID);
            if (p.RowCount > 0)
            {
                p.SProductName = txtEditProduct.Text;
                p.DModifiedDate = DateTime.Now;
                p.Save();



                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
                imgBtnAddProduct.Visible = true;
                trAddProduct.Visible = false;
                GetProducts();
                mvMain.SetActiveView(vAddProduct);
            }
        }
    }
    protected void imgBtnUpdateProductCancel_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnAddProduct.Visible = true;
        trAddProduct.Visible = false;
        txtProduct.Text = "";


        mvMain.SetActiveView(vAddProduct);
    }
    protected void chkActivePro_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)sender).Parent.FindControl("chkActivePro") as CheckBox;
        //tblProducts p = new tblProducts();
        tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
        if (!chk.Checked)
        {
            HiddenField hidPro = ((CheckBox)sender).Parent.FindControl("hidPro") as HiddenField;
            HiddenField hidPQR = ((CheckBox)sender).Parent.FindControl("hidPQR") as HiddenField;
            rel.LoadByPrimaryKey(Convert.ToInt32(hidPQR.Value));
            if (rel.RowCount > 0)
            {
                rel.IsActive = false;
                rel.DModifiedDate = DateTime.Now;
                rel.Save();

                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }



            //p.FlushData();
            //p.LoadByPrimaryKey(Convert.ToInt32(hidPro.Value));
            //if (p.RowCount > 0)
            //{
            //    p.IsActive = false;
            //    p.DModifiedDate = DateTime.Now;
            //    p.Save();
            //    lblMessageProduct.Visible = true;
            //    lblMessageProduct.Text = "Successfully Updated!";
            //}
            GetProducts();
        }
        else
        {
            //HiddenField hidPro = ((CheckBox)sender).Parent.FindControl("hidPro") as HiddenField;
            //p.FlushData();
            //p.LoadByPrimaryKey(Convert.ToInt32(hidPro.Value));
            //if (p.RowCount > 0)
            //{
            //    p.IsActive = true;
            //    p.DModifiedDate = DateTime.Now;
            //    p.Save();
            //    lblMessageProduct.Visible = true;
            //    lblMessageProduct.Text = "Successfully Updated!";

            //}
            HiddenField hidPQR = ((CheckBox)sender).Parent.FindControl("hidPQR") as HiddenField;
            rel.LoadByPrimaryKey(Convert.ToInt32(hidPQR.Value));
            if (rel.RowCount > 0)
            {
                rel.IsActive = true;
                rel.DModifiedDate = DateTime.Now;
                rel.Save();

                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
            GetProducts();
        }
    }
    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            CheckBox chkActive = e.Row.FindControl("chkActivePro") as CheckBox;
            Label lblVATPro = e.Row.FindControl("lblVATPro") as Label;

            HiddenField hidPro = e.Row.FindControl("hidPro") as HiddenField;
            HiddenField hidSubCat_pro = e.Row.FindControl("hidSubCat_pro") as HiddenField;
            HiddenField hidBaseCat_pro = e.Row.FindControl("hidBaseCat_pro") as HiddenField;


            if (!Convert.ToBoolean(drv["isActive"].ToString()))
                chkActive.Checked = false;
            else
                chkActive.Checked = true;

            bool check = false;
            string vat = string.Empty;

            tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
            rel.LoadByPrimaryKey(Convert.ToInt32(drv["pkProductPackingQuantityRelID"].ToString()));
            if (rel.RowCount > 0)
            {
                if (rel.FkVatID != 0)
                {
                    tblVAT v = new tblVAT();
                    v.LoadByPrimaryKey(rel.FkVatID);
                    if (v.RowCount > 0)
                    {
                        vat = v.Vat;
                    }
                    check = true;
                }
            }
            if (check)
            {
                lblVATPro.Text = vat;
            }
            else if (!check)
            {
                CheckProductVat_Product(Convert.ToInt32(hidSubCat_pro.Value), Convert.ToInt32(hidBaseCat_pro.Value), lblVATPro);
            }



            //tblProducts p = new tblProducts();
            //p.LoadByPrimaryKey(Convert.ToInt32(hidPro.Value));
            //if (p.RowCount > 0)
            //{
            //    if (p.FkVatid != 0)
            //    {
            //        tblVAT v = new tblVAT();
            //        v.LoadByPrimaryKey(p.FkVatid);
            //        if (v.RowCount > 0)
            //        {
            //            vat = v.Vat;
            //        }

            //        check = true;
            //    }
            //}
            //if (check)
            //{
            //    lblVATPro.Text = vat;
            //}
            //else if(!check)
            //{
            //    CheckProductVat_Product(Convert.ToInt32(hidSubCat_pro.Value), Convert.ToInt32(hidBaseCat_pro.Value), lblVATPro);
            //}

        }
    }
    private void CheckProductVat_Product(int subcatid, int bcatid, Label lblVat)
    {
        bool check = false;
        string vat = string.Empty;
        tblSubCategories sub = new tblSubCategories();
        sub.LoadByPrimaryKey(subcatid);
        if (sub.RowCount > 0)
        {
            if (sub.FkVatID != 0)
            {
                tblVAT v = new tblVAT();
                v.LoadByPrimaryKey(sub.FkVatID);
                if (v.RowCount > 0)
                {
                    vat = v.Vat;
                }
                check = true;
            }
        }

        if (check)
        {
            lblVat.Text = vat;
        }
    }
    protected void ddlBaseCat_vAddProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBaseCat_vAddProducts.SelectedValue != "0")
        {
            tblSubCategories sub = new tblSubCategories();
            sub.GetActiveSubCat(Convert.ToInt32(ddlBaseCat_vAddProducts.SelectedValue));
            ddlSubCat_vAddProducts.Items.Clear();
            if (sub.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlSubCat_vAddProducts, sub.DefaultView, "cSubCategoryName", "pkSubCategoryID");
            }
            ddlSubCat_vAddProducts.Items.Insert(0, new ListItem("Sub Category", "0"));
        }
    }
    protected void lnkResetVat_Click(object sender, EventArgs e)
    {
        tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
        rel.GetProductsForVat(DepartmentID);
        if (rel.RowCount > 0)
        {
            for (int i = 0; i < rel.RowCount; i++)
            {
                rel.FkVatID = 0;
                rel.DModifiedDate = DateTime.Now;
                rel.Save();
                rel.MoveNext();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
            GetProducts();

        }
    }

    #endregion

    protected void imgBtnCancelSub_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnAddSub.Visible = true;
        trAddSub.Visible = false;
        trLineBeforeSub.Visible = false;
        trLineAfterSub.Visible = true;

    }
    protected void imgBtnCancelBase_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnAddBaseCategory.Visible = true;
        trAddBase.Visible = false;
        trLineBeforeBase.Visible = false;
        trLineAfterBase.Visible = true;
    }
    protected void imgBtnCancelProduct_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnAddProduct.Visible = true;

        txtProduct.Text = "";
        ProductID = 0;
        string imageurl = imgBtnCancelProduct.ImageUrl;
        if (imageurl == "../Images/btn_clearfilter2.png")
        {
            GetProducts();
            LoadBaseCategoriesForProduct();
            LoadPackingForProduct();
            LoadQuantityForProduct();
            LoadVatForProduct();
        }
        else
        {
            trAddProduct.Visible = false;
            trLineBeforeProduct.Visible = false;
            trLineAfterProduct.Visible = true;
            imgBtnFilter.Visible = false;
        }

    }
    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProducts.PageIndex = e.NewPageIndex;
        GetProducts();
    }
    protected void grdBaseCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdBaseCategories.PageIndex = e.NewPageIndex;
        LoadBaseCategories();
    }
    protected void grdSubCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSubCategory.PageIndex = e.NewPageIndex;
        GetSubCategories();
    }

    protected void lnkBackToBase_Click1(object sender, EventArgs e)
    {
        imgBtnAddSub.Visible = true;
        trAddSub.Visible = false;
        imgBtnAddBaseCategory.Visible = true;

        trAddBase.Visible = false;
        mvMain.SetActiveView(vAddBase);
        lblMessageProduct.Text = "";
    }
    protected void lnkBacktoSub_Click1(object sender, EventArgs e)
    {
        imgBtnAddProduct.Visible = true;
        trAddProduct.Visible = false;

        imgBtnAddSub.Visible = true;
        trAddSub.Visible = false;
        lblMessageProduct.Text = "";
        mvMain.SetActiveView(vAddSub);
    }


    protected void btnUpdatePackage_Click(object sender, ImageClickEventArgs e)
    {
        if (ProductID != 0)
        {
            bool active = false;
            tblProductPackages pk = new tblProductPackages();
            if (!pk.GetFirst(ProductID))
                active = true;

            pk.FlushData();
            pk.CheckProductPackage(txtEditPackage.Text);
            if (pk.RowCount > 0)
            {
                string message = "Package Already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                pk.AddNew();
                pk.PName = txtEditPackage.Text;
                pk.FkProductID = ProductID;
                pk.IsActive = active;
                pk.DModifiedDate = DateTime.Now;
                pk.DCreatedDate = DateTime.Now;
                pk.Save();
                txtEditPackage.Text = "";
                LoadPackages();
            }
        }
    }
    protected void btnUpdateQuantity_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void lnkBackToProducts_Click1(object sender, EventArgs e)
    {
        imgBtnAddProduct.Visible = true;
        trAddProduct.Visible = false;
        lblMessageProduct.Text = "";
        mvMain.SetActiveView(vAddProduct);
    }
    protected void lnkBase_Click(object sender, EventArgs e)
    {
        LoadBaseCategories();
        GetAllVat();
        trAddBase.Visible = false;
        trLineBeforeBase.Visible = false;
        trLineAfterBase.Visible = true;
        mvMain.SetActiveView(vAddBase);
    }
    protected void lnkSub_Click(object sender, EventArgs e)
    {
        BindBase();
        GetAllVat();
        GetAllSubCategories();
        trAddSub.Visible = false;
        trLineBeforeSub.Visible = false;
        trLineAfterSub.Visible = true;
        mvMain.SetActiveView(vAddSub);
    }
    protected void lnkPro_Click(object sender, EventArgs e)
    {
        GetProducts();
        LoadBaseCategoriesForProduct();
        LoadPackingForProduct();
        LoadQuantityForProduct();
        imgBtnFilter.Visible = false;
        trAddProduct.Visible = false;
        trLineBeforeProduct.Visible = false;
        trLineAfterProduct.Visible = true;
        mvMain.SetActiveView(vAddProduct);
    }

    #region Supplies Vat
    private void GetAllVat()
    {
        tblVAT vat = new tblVAT();
        vat.GetSortedVatValues();
        ddlVat.Items.Clear();
        ddlVatSub.Items.Clear();
        if (vat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlVat, vat.DefaultView, "Vat", "PkVatID");
            commonMethods.FillDropDownList(ddlVatSub, vat.DefaultView, "Vat", "PkVatID");
        }
        ddlVat.Items.Insert(0, new ListItem("Select Vat", "0"));
        ddlVatSub.Items.Insert(0, new ListItem("Select Vat", "0"));
        grdVat.DataSource = vat.DefaultView;
        grdVat.DataBind();
    }
    protected void imgBtnAddVat_Click(object sender, ImageClickEventArgs e)
    {

        trLineBeforeVat.Visible = false;
        trLineAfterVat.Visible = true;
        imgBtnSaveVat.Visible = true;

        txtVatAmount.Text = "";
        trVat.Visible = true;
        imgBtnEditVat.Visible = false;
    }
    protected void imgBtnSaveVat_Click(object sender, ImageClickEventArgs e)
    {
        if (txtVatAmount.Text != "")
        {
            tblVAT vat = new tblVAT();
            vat.AddNew();
            vat.Vat = txtVatAmount.Text.Replace("%", "") + "%";
            vat.IsActive = true;
            vat.DModifiedDate = DateTime.Now;
            vat.DCreatedDate = DateTime.Now;
            vat.Save();
            GetAllVat();
            txtVatAmount.Text = "";


            lblMessageVat.Text = "Successfully Added Vat Amount!";
            lblMessageVat.ForeColor = Color.Green;
            trVat.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ VatSaved(); });", true);
        }
        else
        {

            lblMessageVat.Text = "Plese Enter Vat Amount!";
            lblMessageVat.ForeColor = Color.Red;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ VatSaved(); });", true);
        }
    }
    protected void imgBtnEditVat_Click(object sender, ImageClickEventArgs e)
    {
        if (txtVatAmount.Text != "")
        {
            int vid = Convert.ToInt32(ViewState["vatid"]);

            tblVAT vat = new tblVAT();
            vat.LoadByPrimaryKey(vid);
            if (vat.RowCount > 0)
            {
                vat.Vat = txtVatAmount.Text.Replace("%", "") + "%";
                vat.DModifiedDate = DateTime.Now;
                vat.Save();

                txtVatAmount.Text = "";
                lblMessageVat.Text = "Successfully Updated Vat Amount!";
                lblMessageVat.ForeColor = Color.Green;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ VatSaved(); });", true);

                GetAllVat();

                imgBtnEditVat.Visible = false;
                imgBtnSaveVat.Visible = true;
                trVat.Visible = false;
                trLineBeforeVat.Visible = true;
                trLineAfterVat.Visible = false;

            }
        }
        else
        {

            lblMessageVat.Text = "Plese enter Vat Amount!";
            lblMessageVat.ForeColor = Color.Red;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ VatSaved(); });", true);
        }
    }
    protected void imgBtnCancelVat_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnEditVat.Visible = false;
        imgBtnSaveVat.Visible = true;

        trVat.Visible = false;
        trLineBeforeVat.Visible = true;
        trLineAfterVat.Visible = false;
    }
    protected void grdVat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblVAT vat = new tblVAT();

            switch (e.CommandName.ToLower())
            {
                case "vat":
                    hdnID.Value = "0";
                    vat.FlushData();
                    vat.LoadByPrimaryKey(id);
                    if (vat.RowCount > 0)
                    {
                        txtVatAmount.Text = vat.Vat.ToString().Replace("%", "");
                        hdnID.Value = vat.PkVatID.ToString();
                        ViewState["vatid"] = vat.PkVatID;


                        trLineBeforeVat.Visible = true;
                        trLineAfterVat.Visible = false;
                        imgBtnSaveVat.Visible = false;
                        imgBtnEditVat.Visible = true;

                        trVat.Visible = true;
                    }
                    break;

                case "active":
                    try
                    {
                        vat.FlushData();
                        vat.LoadByPrimaryKey(id);
                        if (vat.RowCount > 0)
                        {

                            bool checkVat = false;
                            string message = string.Empty;
                            if (vat.IsActive)
                            {
                                tblVAT vCheck = new tblVAT();

                                vCheck.CheckVatForBase(vat.PkVatID);
                                if (vCheck.RowCount > 0)
                                {
                                    checkVat = true;
                                    message = "This vat already used in Base Category. So pls first change base category vat with any other value";
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "vatMessage1", "$(function(){alert('" + message + "');});", true);

                                }
                                else
                                {
                                    vCheck.FlushData();
                                    vCheck.CheckVatForSub(vat.PkVatID);
                                    if (vCheck.RowCount > 0)
                                    {
                                        checkVat = true;
                                        message = "This vat already used in Base Category. So pls first change base category vat with any other value";
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "vatMessage2", "$(function(){alert('" + message + "');});", true);
                                    }
                                    else
                                    {
                                        vCheck.FlushData();
                                        vCheck.CheckVatForPro(vat.PkVatID);
                                        if (vCheck.RowCount > 0)
                                        {
                                            checkVat = true;
                                            message = "This vat already used in Base Category. So pls first change base category vat with any other value";
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "vatMessage3", "$(function(){alert('" + message + "');});", true);
                                        }
                                    }
                                }
                                if (!checkVat)
                                {
                                    vat.IsActive = false;
                                    vat.DModifiedDate = DateTime.Now;
                                    vat.Save();
                                    GetAllVat();
                                }
                            }
                            else if (!vat.IsActive)
                            {
                                vat.IsActive = true;
                                vat.DModifiedDate = DateTime.Now;
                                vat.Save();
                            }
                        }
                    }
                    catch (Exception ex)
                    { }
                    break;
            }
        }
    }
    protected void grdVat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;

                ImageButton imgBtnActive = e.Row.FindControl("imgBtnActive") as ImageButton;

                if (Convert.ToBoolean(dr["isActive"].ToString()))
                    imgBtnActive.ImageUrl = "../Images/activate_icon.gif";
                else
                    imgBtnActive.ImageUrl = "../Images/close.png";
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

    protected void lnkSuppliesVat_Click(object sender, EventArgs e)
    {
        GetAllVat();
        ModalPopupExtender1.Show();
    }

    #region Activating/de-activating basecategories

    private void GetAllBaseCategories(int basecat)
    {
        tblBaseCategories bcat = new tblBaseCategories();
        bcat.GetActiveBaseCate(basecat, DepartmentID);
        ddlBaseCategories.Items.Clear();
        if (bcat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlBaseCategories, bcat.DefaultView, "CatagoryName", "PkBaseCategoryID");
        }
        ddlBaseCategories.Items.Insert(0, new ListItem("Chose Base Category", "0"));
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)sender).Parent.FindControl("chkActive") as CheckBox;
        if (!chk.Checked)
        {
            HiddenField hidbaseid = ((CheckBox)sender).Parent.FindControl("hidBase") as HiddenField;
            HiddenField hidBaseName = ((CheckBox)sender).Parent.FindControl("hidBaseName") as HiddenField;

            tblSubCategories objsub = new tblSubCategories();
            objsub.GetActiveSubCat(Convert.ToInt32(hidbaseid.Value));
            dtlBaseDropDowns.DataSource = objsub.DefaultView;
            dtlBaseDropDowns.DataBind();
            if (objsub.RowCount > 0)
            {
                lblBaseChange.Text = hidBaseName.Value;
                ModalPopupExtender2.Show();
                GetAllBaseCategories(Convert.ToInt32(hidbaseid.Value));

            }
            else
            {
                tblBaseCategories objBaseCat = new tblBaseCategories();
                objBaseCat.LoadByPrimaryKey(Convert.ToInt32(hidbaseid.Value));
                if (objBaseCat.RowCount > 0)
                {
                    objBaseCat.IsActive = false;
                    objBaseCat.DModifiedDate = DateTime.Now;
                    objBaseCat.Save();

                    lblMessageProduct.Text = "Successfully Updated!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
                }
            }
        }
        else
        {
            HiddenField hidbaseid = ((CheckBox)sender).Parent.FindControl("hidBase") as HiddenField;
            tblBaseCategories objBaseCat = new tblBaseCategories();
            objBaseCat.LoadByPrimaryKey(Convert.ToInt32(hidbaseid.Value));
            if (objBaseCat.RowCount > 0)
            {
                objBaseCat.IsActive = true;
                objBaseCat.DModifiedDate = DateTime.Now;
                objBaseCat.Save();

                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
        }
    }
    protected void imgBtnSaveForActivation_Click(object sender, ImageClickEventArgs e)
    {
        int bid = 0;
        if (ddlBaseCategories.SelectedValue != "0")
        {
            tblSubCategories sub = new tblSubCategories();
            for (int i = 0; i < dtlBaseDropDowns.Items.Count; i++)
            {
                DropDownList ddl = dtlBaseDropDowns.Items[i].FindControl("ddlBaseCategoriesDTL") as DropDownList;
                HiddenField hidSubForAct = dtlBaseDropDowns.Items[i].FindControl("hidSubForAct") as HiddenField;
                if (bid == 0)
                {
                    HiddenField hidBaseForAct = dtlBaseDropDowns.Items[i].FindControl("hidBaseForAct") as HiddenField;
                    bid = Convert.ToInt32(hidBaseForAct.Value);
                }


                sub.FlushData();
                sub.LoadByPrimaryKey(Convert.ToInt32(hidSubForAct.Value));
                if (sub.RowCount > 0)
                {
                    sub.FkBaseCategoryID = Convert.ToInt32(ddlBaseCategories.SelectedValue);
                    sub.DModifiedDate = DateTime.Now;
                    sub.Save();
                    lblPopBaseActMessage.Visible = true;

                }
            }
            tblBaseCategories baseCat = new tblBaseCategories();
            baseCat.LoadByPrimaryKey(bid);
            if (baseCat.RowCount > 0)
            {
                baseCat.IsActive = false;
                baseCat.DModifiedDate = DateTime.Now;
                baseCat.Save();
            }


        }
        else
        {
            bool check = false;
            tblSubCategories sub = new tblSubCategories();
            for (int i = 0; i < dtlBaseDropDowns.Items.Count; i++)
            {
                DropDownList ddl = dtlBaseDropDowns.Items[i].FindControl("ddlBaseCategoriesDTL") as DropDownList;
                HiddenField hidSubForAct = dtlBaseDropDowns.Items[i].FindControl("hidSubForAct") as HiddenField;
                if (bid == 0)
                {
                    HiddenField hidBaseForAct = dtlBaseDropDowns.Items[i].FindControl("hidBaseForAct") as HiddenField;
                    bid = Convert.ToInt32(hidBaseForAct.Value);
                }
                if (ddl.SelectedValue != "0")
                {
                    sub.FlushData();
                    sub.LoadByPrimaryKey(Convert.ToInt32(hidSubForAct.Value));
                    if (sub.RowCount > 0)
                    {
                        sub.FkBaseCategoryID = Convert.ToInt32(ddl.SelectedValue);
                        sub.DModifiedDate = DateTime.Now;
                        sub.Save();
                    }
                    check = true;
                }
            }
            if (!check)
            {
                string message = "Pls choose single or multiple base categories for affected sub categories!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                tblBaseCategories baseCat = new tblBaseCategories();
                baseCat.LoadByPrimaryKey(bid);
                if (baseCat.RowCount > 0)
                {
                    baseCat.IsActive = false;
                    baseCat.DModifiedDate = DateTime.Now;
                    baseCat.Save();
                }
            }
        }
    }
    protected void imgBtnCancelForActivation_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    }
    protected void dtlBaseDropDowns_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DropDownList ddlBaseCategoriesDTL = e.Item.FindControl("ddlBaseCategoriesDTL") as DropDownList;
            HiddenField hidBaseForAct = e.Item.FindControl("hidBaseForAct") as HiddenField;
            BindDTLDropDownList(Convert.ToInt32(hidBaseForAct.Value), ddlBaseCategoriesDTL);
        }
    }
    private void BindDTLDropDownList(int basecat, DropDownList ddlbase)
    {
        tblBaseCategories bcat = new tblBaseCategories();
        bcat.GetActiveBaseCate(basecat, DepartmentID);
        ddlbase.Items.Clear();
        if (bcat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlbase, bcat.DefaultView, "CatagoryName", "PkBaseCategoryID");
        }
        ddlbase.Items.Insert(0, new ListItem("Chose Base Category", "0"));
    }
    protected void dtlBaseDropDowns_ItemCommand(object source, DataListCommandEventArgs e)
    {

    }

    #endregion

    #region Activating/de-activating sub categories
    private void GetAllSubCategories()
    {
        tblSubCategories sub = new tblSubCategories();
        sub.GetAllSubCats(DepartmentID);
        grdSubCategory.DataSource = sub.DefaultView;
        grdSubCategory.DataBind();
    }
    protected void imgBtnSaveForActivationSub_Click(object sender, ImageClickEventArgs e)
    {
        int bid = 0;
        if (ddlSubCategories.SelectedValue != "0")
        {

            tblProducts p = new tblProducts();
            for (int i = 0; i < dtlSubDropDowns.Items.Count; i++)
            {
                DropDownList ddl = dtlSubDropDowns.Items[i].FindControl("ddlSubCategoriesDTL") as DropDownList;
                HiddenField hidProForAct = dtlSubDropDowns.Items[i].FindControl("hidProForAct") as HiddenField;
                if (bid == 0)
                {
                    HiddenField hidSubcatForAct = dtlSubDropDowns.Items[i].FindControl("hidSubcatForAct") as HiddenField;
                    bid = Convert.ToInt32(hidSubcatForAct.Value);
                }


                p.FlushData();
                p.LoadByPrimaryKey(Convert.ToInt32(hidProForAct.Value));
                if (p.RowCount > 0)
                {
                    p.FkSubCategoryID = Convert.ToInt32(ddlSubCategories.SelectedValue);
                    p.DModifiedDate = DateTime.Now;
                    p.Save();
                    lblPopSubActMessage.Visible = true;

                }
            }
            tblSubCategories sub = new tblSubCategories();
            sub.LoadByPrimaryKey(bid);
            if (sub.RowCount > 0)
            {
                sub.IsActive = false;
                sub.DModifiedDate = DateTime.Now;
                sub.Save();
            }


        }
        else
        {
            bool check = false;

            tblProducts p = new tblProducts();
            for (int i = 0; i < dtlSubDropDowns.Items.Count; i++)
            {
                DropDownList ddl = dtlSubDropDowns.Items[i].FindControl("ddlSubCategoriesDTL") as DropDownList;
                HiddenField hidProForAct = dtlSubDropDowns.Items[i].FindControl("hidProForAct") as HiddenField;
                if (bid == 0)
                {
                    HiddenField hidSubcatForAct = dtlSubDropDowns.Items[i].FindControl("hidSubcatForAct") as HiddenField;
                    bid = Convert.ToInt32(hidSubcatForAct.Value);
                }
                if (ddl.SelectedValue != "0")
                {
                    p.FlushData();
                    p.LoadByPrimaryKey(Convert.ToInt32(hidProForAct.Value));
                    if (p.RowCount > 0)
                    {
                        p.FkSubCategoryID = Convert.ToInt32(ddl.SelectedValue);
                        p.DModifiedDate = DateTime.Now;
                        p.Save();
                    }
                    check = true;
                }
            }
            if (!check)
            {
                string message = "Pls choose single or multiple Sub categories for affected Products!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                tblSubCategories sub = new tblSubCategories();
                sub.LoadByPrimaryKey(bid);
                if (sub.RowCount > 0)
                {
                    sub.IsActive = false;
                    sub.DModifiedDate = DateTime.Now;
                    sub.Save();
                }

            }
        }
    }
    protected void imgBtnCancelForActivationSub_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender3.Hide();
    }
    protected void dtlSubDropDowns_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            DropDownList ddlSubCategoriesDTL = e.Item.FindControl("ddlSubCategoriesDTL") as DropDownList;
            HiddenField hidSubcatForAct = e.Item.FindControl("hidSubcatForAct") as HiddenField;
            BindDTLDropDownListSub(Convert.ToInt32(hidSubcatForAct.Value), ddlSubCategoriesDTL);
        }
    }
    protected void chkActiveSub_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)sender).Parent.FindControl("chkActiveSub") as CheckBox;
        HiddenField hidSub = ((CheckBox)sender).Parent.FindControl("hidSub") as HiddenField;
        HiddenField hidSubName = ((CheckBox)sender).Parent.FindControl("hidSubName") as HiddenField;

        if (!chk.Checked)
        {
            HiddenField hidSubid = ((CheckBox)sender).Parent.FindControl("hidSub") as HiddenField;
            tblProducts p = new tblProducts();
            p.getActiveProduct(Convert.ToInt32(hidSubid.Value));
            dtlSubDropDowns.DataSource = p.DefaultView;
            dtlSubDropDowns.DataBind();
            if (p.RowCount > 0)
            {
                lblAffectedSubcat.Text = hidSubName.Value;
                ModalPopupExtender3.Show();
                tblSubCategories sub = new tblSubCategories();
                sub.GetActiveSubCate(Convert.ToInt32(hidSubid.Value));
                ddlSubCategories.Items.Clear();
                if (sub.RowCount > 0)
                {
                    commonMethods.FillDropDownList(ddlSubCategories, sub.DefaultView, "cSubCategoryName", "pkSubCategoryID");
                }
                ddlSubCategories.Items.Insert(0, new ListItem("Chose Sub Category", "0"));

            }
            else
            {
                tblSubCategories sub = new tblSubCategories();
                sub.LoadByPrimaryKey(Convert.ToInt32(hidSubid.Value));
                if (sub.RowCount > 0)
                {
                    sub.IsActive = false;
                    sub.DModifiedDate = DateTime.Now;
                    sub.Save();

                    lblMessageProduct.Text = "Successfully Updated!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
                }
            }
        }
        else
        {

            tblSubCategories sub = new tblSubCategories();
            sub.LoadByPrimaryKey(Convert.ToInt32(hidSub.Value));
            if (sub.RowCount > 0)
            {
                sub.IsActive = true;
                sub.DModifiedDate = DateTime.Now;
                sub.Save();

                lblMessageProduct.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ SupplierSaved(); });", true);
            }
        }
    }
    private void BindDTLDropDownListSub(int subcat, DropDownList ddlsub)
    {
        tblSubCategories sub = new tblSubCategories();
        sub.GetActiveSubCate(subcat);
        ddlsub.Items.Clear();
        if (sub.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlsub, sub.DefaultView, "cSubCategoryName", "pkSubCategoryID");
        }
        ddlsub.Items.Insert(0, new ListItem("Chose Sub Category", "0"));
    }
    #endregion

    #region Packing
    protected void lnkPackagingPopup_Click(object sender, EventArgs e)
    {
        LoadPackages();
        trPacking.Visible = false;
        trLineBeforePacking.Visible = false;
        trLineAfterPacking.Visible = true;
        ModalPopupExtender4.Show();
    }
    private void LoadPackages()
    {
        tblProductPackages proPack = new tblProductPackages();
        proPack.GetOrderedPacking();
        grdPackingPopup.DataSource = proPack.DefaultView;
        grdPackingPopup.DataBind();
    }
    protected void imgBtnAddPackingOptions_Click(object sender, ImageClickEventArgs e)
    {
        trLineBeforePacking.Visible = false;
        trLineAfterPacking.Visible = true;
        trPacking.Visible = true;
        imgBtnSavePacking.ImageUrl = "../Images/btn_save_account.png";
        txtOrderNumPacking.Text = "";
        txtPackingName.Text = "";
        txtPackingDescription.Text = "";
        PackingID = 0;
    }
    protected void grdPackingPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPackingPopup.PageIndex = e.NewPageIndex;
        LoadPackages();
    }
    protected void grdPackingPopup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblProductPackages packing = new tblProductPackages();

            switch (e.CommandName.ToLower())
            {
                case "name":
                    packing.FlushData();
                    packing.LoadByPrimaryKey(id);
                    if (packing.RowCount > 0)
                    {
                        PackingID = packing.PkProductPackageID;
                        txtPackingName.Text = packing.PName;
                        txtQuantityNameNew.Text = packing.QName;
                        txtOrderNumPacking.Text = packing.POrder.ToString();
                        txtPackingDescription.Text = packing.PDescription;
                        imgBtnSavePacking.ImageUrl = "../Images/btn_edit.png";
                        trLineBeforePacking.Visible = true;
                        trLineAfterPacking.Visible = false;
                        trPacking.Visible = true;
                    }
                    break;

                case "active":
                    packing.FlushData();
                    packing.LoadByPrimaryKey(id);
                    if (packing.RowCount > 0)
                    {
                        if (packing.IsActive)
                            packing.IsActive = false;
                        else if (!packing.IsActive)
                            packing.IsActive = true;
                        packing.DModifiedDate = DateTime.Now;
                        packing.Save();
                        LoadPackages();
                        lblPackingMessage.Text = "Successfully Updated!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ PackingSaved(); });", true);
                    }
                    break;
            }
        }
    }
    protected void grdPackingPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                ImageButton imgBtnActive = e.Row.FindControl("imgBtnActive") as ImageButton;
                if (Convert.ToBoolean(drv["isActive"].ToString()))
                    imgBtnActive.ImageUrl = "../Images/activate_icon.gif";
                else
                    imgBtnActive.ImageUrl = "../Images/close.png";

            }
        }
        catch (Exception ex)
        { }
    }
    protected void imgBtnSavePacking_Click(object sender, ImageClickEventArgs e)
    {
        tblProductPackages packing = new tblProductPackages();
        if (PackingID != 0)
        {
            #region Adding Packing Option
            packing.FlushData();
            packing.LoadByPrimaryKey(PackingID);
            if (packing.RowCount > 0)
            {
                packing.DModifiedDate = DateTime.Now;
                int ordernum = 0;
                tblProductPackages packingForOrder = new tblProductPackages();
                packingForOrder.GetPackingMaxOrderNum();
                if (packingForOrder.RowCount > 0)
                {
                    ordernum = Convert.ToInt32(packingForOrder.GetColumn("orderNum").ToString());
                }
                hidSortNo.Value = packing.POrder.ToString();
                if (txtOrderNumPacking.Text != "")
                {
                    if (ordernum <= Convert.ToInt32(txtOrderNumPacking.Text))
                    {
                        txtOrderNumPacking.Text = ordernum.ToString();
                    }
                    MakeSortForPacking(packing);
                }
                packing.POrder = Convert.ToInt32(txtOrderNumPacking.Text);


                lblPackingMessage.Text = "Successfully Updated!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ PackingSaved(); });", true);
            }
            #endregion
        }
        else
        {
            #region updating packing option
            packing.FlushData();
            packing.AddNew();
            int ordernum = 0;
            tblProductPackages packingForOrder = new tblProductPackages();
            packingForOrder.GetPackingMaxOrderNum();
            if (packingForOrder.RowCount > 0)
            {
                ordernum = Convert.ToInt32(packingForOrder.GetColumn("orderNum").ToString());
            }
            ordernum += 1;
            packing.POrder = ordernum;
            packing.IsActive = true;
            packing.DModifiedDate = DateTime.Now;
            packing.DCreatedDate = DateTime.Now;

            lblPackingMessage.Text = "Successfully Added!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "message", "$(function(){ PackingSaved(); });", true);
            #endregion
        }

        packing.PName = txtPackingName.Text;
        packing.QName = txtQuantityNameNew.Text;
        packing.PDescription = txtPackingDescription.Text;
        packing.Save();

        trPacking.Visible = false;
        trLineBeforePacking.Visible = false;
        trLineAfterPacking.Visible = true;
        LoadPackages();


    }
    protected void imgBtnCancelPacking_Click(object sender, ImageClickEventArgs e)
    {
        trPacking.Visible = false;
        trLineBeforePacking.Visible = false;
        trLineAfterPacking.Visible = true;
    }
    private void MakeSortForPacking(tblProductPackages packing)
    {
        TextBox txtSort = txtOrderNumPacking;

        tblProductPackages packings = new tblProductPackages();
        tblProductPackages singlePacking = new tblProductPackages();

        singlePacking.LoadByPrimaryKey(packing.PkProductPackageID);

        if (txtSort.Text != "" && txtSort.Text != "0")
        {
            if (Convert.ToInt32(txtSort.Text) != Convert.ToInt32(hidSortNo.Value))
            {
                if (Convert.ToInt32(txtSort.Text) < Convert.ToInt32(hidSortNo.Value))
                {
                    packings.FlushData();
                    packings.GetPackingForSortLesser(Convert.ToInt32(txtSort.Text), Convert.ToInt32(hidSortNo.Value));
                    if (packings.RowCount > 0)
                    {
                        for (int i = 0; i < packings.RowCount; i++)
                        {
                            if (packings.POrder != Convert.ToInt32(hidSortNo.Value))
                            {
                                packings.POrder = packings.POrder + 1;
                                packings.DModifiedDate = DateTime.Now;
                                packings.Save();
                            }
                            packings.MoveNext();
                        }
                    }
                }
                else if (Convert.ToInt32(txtSort.Text) > Convert.ToInt32(hidSortNo.Value))
                {
                    packings.FlushData();
                    packings.GetPackingForSortGreater(Convert.ToInt32(hidSortNo.Value), Convert.ToInt32(txtSort.Text));
                    if (packings.RowCount > 0)
                    {
                        for (int i = 0; i < packings.RowCount; i++)
                        {
                            if (packings.POrder != Convert.ToInt32(hidSortNo.Value))
                            {
                                packings.POrder = packings.POrder - 1;
                                packings.DModifiedDate = DateTime.Now;
                                packings.Save();
                            }
                            packings.MoveNext();
                        }
                    }
                }
                if (singlePacking.RowCount > 0)
                {
                    singlePacking.POrder = Convert.ToInt32(txtSort.Text);
                    singlePacking.DModifiedDate = DateTime.Now;
                    singlePacking.Save();
                }
            }
        }

    }

    protected void grdPacking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblProductPackages pk = new tblProductPackages();

            switch (e.CommandName.ToLower())
            {
                case "active":

                    pk.GetAllExceptOne(id);
                    if (pk.RowCount > 0)
                    {
                        for (int i = 0; i < pk.RowCount; i++)
                        {
                            pk.IsActive = false;
                            pk.Save();
                            pk.MoveNext();
                        }
                    }
                    pk.FlushData();
                    pk.LoadByPrimaryKey(id);
                    if (pk.RowCount > 0)
                    {
                        pk.IsActive = true;
                        pk.DModifiedDate = DateTime.Now;
                        pk.Save();
                        LoadPackages();
                    }
                    break;

                case "del":
                    pk.FlushData();
                    pk.LoadByPrimaryKey(id);
                    if (pk.RowCount > 0)
                    {
                        pk.MarkAsDeleted();
                        pk.Save();
                    }
                    LoadPackages();
                    break;
            }
        }
    }
    protected void grdPacking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            ImageButton imgbtnSetActivePackage = e.Row.FindControl("imgbtnSetActivePackage") as ImageButton;

            //if (Convert.ToBoolean(drv["bIsPrimary"]))
            //{
            //    imgbtnSetActivePackage.ImageUrl = "~/Images/Star_Black.png";
            //    imgbtnSetActivePackage.Width = 16;
            //    imgbtnSetActivePackage.ToolTip = "Active";
            //    ImageButton imgDelete = e.Row.FindControl("imgDelete") as ImageButton;
            //    imgDelete.Visible = false;
            //}
        }
    }

    #endregion

    #region Quantity
    protected void lnkQuantityPopup_Click(object sender, EventArgs e)
    {
        LoadQuantities();
        trQuantity.Visible = false;
        trLineBeforeQuantity.Visible = false;
        trLineAfterQuantity.Visible = true;
        ModalPopupExtender5.Show();
    }
    private void LoadQuantities()
    {
        tblProductQuantities quantity = new tblProductQuantities();
        quantity.LoadAll();
        grdQuantityPopup.DataSource = quantity.DefaultView;
        grdQuantityPopup.DataBind();
    }

    protected void imgBtnAddQuantityOptions_Click(object sender, ImageClickEventArgs e)
    {
        trLineBeforeQuantity.Visible = false;
        trLineAfterQuantity.Visible = true;
        trQuantity.Visible = true;
        imgBtnSaveQuantity.ImageUrl = "../Images/btn_save_account.png";
        txtOrderNumQuantity.Text = "";
        txtQuantityName.Text = "";
        txtQuantityDescription.Text = "";
        QuantityID = 0;
    }
    protected void imgBtnSaveQuantity_Click(object sender, ImageClickEventArgs e)
    {

        tblProductQuantities quantity = new tblProductQuantities();
        if (QuantityID != 0)
        {
            quantity.FlushData();
            quantity.LoadByPrimaryKey(QuantityID);
            if (quantity.RowCount > 0)
            {
                quantity.DModifiedDate = DateTime.Now;
                int ordernum = 0;

                tblProductQuantities quantityForOrder = new tblProductQuantities();


                quantityForOrder.GetQuantityMaxOrderNum();
                if (quantityForOrder.RowCount > 0)
                {
                    ordernum = Convert.ToInt32(quantityForOrder.GetColumn("orderNum").ToString());
                }
                hidSortQuantity.Value = quantity.QOrder.ToString();
                if (txtOrderNumQuantity.Text != "")
                {
                    if (ordernum <= Convert.ToInt32(txtOrderNumQuantity.Text))
                    {
                        txtOrderNumQuantity.Text = ordernum.ToString();
                    }
                    MakeSortForQuantity(quantity);
                }
                quantity.QOrder = Convert.ToInt32(txtOrderNumQuantity.Text);

                lblQuantityMessage.Visible = true;
                lblQuantityMessage.Text = "Successfully Updated!";
            }
        }
        else
        {
            quantity.FlushData();
            quantity.AddNew();
            int ordernum = 0;
            tblProductQuantities quantityForOrder = new tblProductQuantities();
            quantityForOrder.GetQuantityMaxOrderNum();
            if (quantityForOrder.RowCount > 0)
            {
                ordernum = Convert.ToInt32(quantityForOrder.GetColumn("orderNum").ToString());
            }
            ordernum += 1;
            quantity.QOrder = ordernum;
            quantity.IsActive = true;
            quantity.DModifiedDate = DateTime.Now;
            quantity.DCreatedDate = DateTime.Now;
            lblQuantityMessage.Visible = true;
            lblQuantityMessage.Text = "Successfully Added!";
        }

        quantity.QName = txtQuantityName.Text;
        quantity.QDescription = txtQuantityDescription.Text;
        quantity.Save();

        trQuantity.Visible = false;
        trLineBeforeQuantity.Visible = false;
        trLineAfterQuantity.Visible = true;
        LoadQuantities();
    }
    protected void imgBtnCancelQuantity_Click(object sender, ImageClickEventArgs e)
    {
        trQuantity.Visible = false;
        trLineBeforeQuantity.Visible = false;
        trLineAfterQuantity.Visible = true;
    }
    protected void grdQuantityPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdQuantityPopup.PageIndex = e.NewPageIndex;
        LoadQuantities();
    }
    protected void grdQuantityPopup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblProductPackages packing = new tblProductPackages();
            tblProductQuantities quantity = new tblProductQuantities();


            switch (e.CommandName.ToLower())
            {
                case "name":
                    quantity.FlushData();
                    quantity.LoadByPrimaryKey(id);
                    if (quantity.RowCount > 0)
                    {
                        QuantityID = quantity.PkProductQuantityID;
                        txtQuantityName.Text = quantity.QName;
                        txtOrderNumQuantity.Text = quantity.QOrder.ToString();
                        txtQuantityDescription.Text = quantity.QDescription;
                        imgBtnSaveQuantity.ImageUrl = "../Images/btn_edit.png";
                        trLineBeforeQuantity.Visible = true;
                        trLineAfterQuantity.Visible = false;
                        trQuantity.Visible = true;
                    }
                    break;
            }
        }
    }
    protected void grdQuantityPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    private void MakeSortForQuantity(tblProductQuantities quantity)
    {
        TextBox txtSort = txtOrderNumQuantity;

        tblProductQuantities quantities = new tblProductQuantities();
        tblProductQuantities singleQuantity = new tblProductQuantities();

        singleQuantity.LoadByPrimaryKey(quantity.PkProductQuantityID);

        if (txtSort.Text != "" && txtSort.Text != "0")
        {
            if (Convert.ToInt32(txtSort.Text) != Convert.ToInt32(hidSortQuantity.Value))
            {
                if (Convert.ToInt32(txtSort.Text) < Convert.ToInt32(hidSortQuantity.Value))
                {
                    quantities.FlushData();
                    quantities.GetQuantityForSortLesser(Convert.ToInt32(txtSort.Text), Convert.ToInt32(hidSortQuantity.Value));
                    if (quantities.RowCount > 0)
                    {
                        for (int i = 0; i < quantities.RowCount; i++)
                        {
                            if (quantities.QOrder != Convert.ToInt32(hidSortQuantity.Value))
                            {
                                quantities.QOrder = quantities.QOrder + 1;
                                quantities.DModifiedDate = DateTime.Now;
                                quantities.Save();
                            }
                            quantities.MoveNext();
                        }
                    }
                }
                else if (Convert.ToInt32(txtSort.Text) > Convert.ToInt32(hidSortQuantity.Value))
                {
                    quantities.FlushData();
                    quantities.GetQuantityForSortGreater(Convert.ToInt32(hidSortQuantity.Value), Convert.ToInt32(txtSort.Text));
                    if (quantities.RowCount > 0)
                    {
                        for (int i = 0; i < quantities.RowCount; i++)
                        {
                            if (quantities.QOrder != Convert.ToInt32(hidSortQuantity.Value))
                            {
                                quantities.QOrder = quantities.QOrder - 1;
                                quantities.DModifiedDate = DateTime.Now;
                                quantities.Save();
                            }
                            quantities.MoveNext();
                        }
                    }
                }
                if (singleQuantity.RowCount > 0)
                {
                    singleQuantity.QOrder = Convert.ToInt32(txtSort.Text);
                    singleQuantity.DModifiedDate = DateTime.Now;
                    singleQuantity.Save();
                }
            }
        }

    }

    protected void grdQuantity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        { }
    }
    protected void grdQuantity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { }
    }
    #endregion

    protected void imgBtnFilterProduct_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnFilterProduct.Visible = false;
        trAddProduct.Visible = true;
        trLineAfterProduct.Visible = true;
        trLineBeforeProduct.Visible = false;
        imgBtnCancelProduct.ImageUrl = "../Images/btn_clearfilter2.png";
        imgBtnSaveProduct.Visible = false;
        imgBtnFilter.Visible = true;
    }
    protected void vAddProduct_Activate(object sender, EventArgs e)
    {

    }
    protected void imgBtnFilter_Click(object sender, ImageClickEventArgs e)
    {
        string query = string.Empty;
        if (ddlBaseCat_vAddProducts.SelectedValue != "0")
        {
            query += "and bas.pkBaseCategoryID = " + ddlBaseCat_vAddProducts.SelectedValue;
        }
        if (ddlSubCat_vAddProducts.SelectedValue != "0")
        {
            query += " and sub.pksubcategoryid = " + ddlSubCat_vAddProducts.SelectedValue;
        }
        if (txtProduct.Text != "")
        {
            query += " and p.sProductName LIKE '%" + txtProduct.Text + "%'";
        }
        if (ddlPacking_vAddProduct.SelectedValue != "0")
        {
            query += " and rel.fkProductPackageID = " + ddlPacking_vAddProduct.SelectedValue;
        }

        if (ddlVatPro.SelectedValue != "0")
        {
            query += " and p.fkvatid = " + ddlVatPro.SelectedValue;
        }

        tblProducts p = new tblProducts();
        p.GetProductsForGrid_filter(DepartmentID, query);
        grdProducts.DataSource = p.DefaultView;
        grdProducts.DataBind();
    }
}
