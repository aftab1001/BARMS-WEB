using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Drawing;
using ExpertPdf.HtmlToPdf;
using LC.Model.BMS.BLL;
using MyGeneration.dOOdads;

public partial class AccountManager_ManageOrders : System.Web.UI.Page
{
    int UserID;
    static string textboxid = string.Empty;
    int DepartmentID;
    static int basCat = 0;
    static int subCat = 0;
    static int pid = 0;
    static DataView dv = null;
    static double est_subtotal = 0.0;
    static double est_grand_total = 0.0;
    static List<string> SupplierIDs = new List<string>();
    static bool orderCheck = false;
    static List<int> Pids = new List<int>();
    static bool change = false;
    bool checkForOrder = false;
    static Dictionary<int, int> dic_supplier_packing = new Dictionary<int, int>();
    DublicateValuePair dvp = new DublicateValuePair();
    DublicateValuePair dvpForQuantity = new DublicateValuePair();
    DublicateValuePair dvpForPackingID = new DublicateValuePair();
    DublicateValuePair dvpForPrice = new DublicateValuePair();

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

            Session["orderid"] = null;
            Pids.Clear();
            LoadBaseCategories();
            GetAllOrders();
            LoadDropDowns();
        }
    }
    private void LoadDropDowns()
    {
        ddlYear.Items.Clear();
        for (int i = 0; i <= 74; i++)
        {
            ddlYear.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
        }
        ddlYear.Items.Insert(0, new ListItem("Select Year", "0"));

        ddlSuppliers.Items.Clear();
        tblSupplier suppliers = new tblSupplier();
        suppliers.LoadAll();
        commonMethods.FillDropDownList(ddlSuppliers, suppliers.DefaultView, "SBrandName", "PkSupplierID");
        ddlSuppliers.Items.Insert(0, new ListItem("Select Supplier", "0"));

        rdWithInvoice.Checked = true;
        txtInvoiceNumber.Text = "";
        txtFrom.Text = "";
        txtTill.Text = "";
        txtNote.Text = "";

    }
    private void GetAllOrders()
    {
        try
        {
            tblOrders o = new tblOrders();
            o.GetOrderHistory();
            grdOrdersHistory.DataSource = o.DefaultView;
            grdOrdersHistory.DataBind();
        }
        catch (Exception)
        { }
    }
    protected void grdOrdersHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOrderDate = e.Row.FindControl("lblOrderDate") as Label;
                Label lblOrderStatus = e.Row.FindControl("lblOrderStatus") as Label;
                Label lblSubtotal = e.Row.FindControl("lblSubtotal") as Label;


                LinkButton lnkOrderIDUpdate = e.Row.FindControl("lnkOrderIDUpdate") as LinkButton;

                Label lblPaid = e.Row.FindControl("lblPaid") as Label;
                Label lblDue = e.Row.FindControl("lblDue") as Label;
                double dPayedAmt = 0.0;
                dPayedAmt = Convert.ToDouble(lblPaid.Text);
                if (lblPaid.Text == "" || lblPaid.Text == "0")
                    lblPaid.Text = "-";
                else
                    lblPaid.Text = commonMethods.ChangetToUK(lblPaid.Text) + " €";
                double dSubTotal = Convert.ToDouble(lblSubtotal.Text);
                double dPayed = Convert.ToDouble(dPayedAmt.ToString());
                string sDueAmt = (dSubTotal - dPayed).ToString();
                if (lblDue.Text == "" || lblDue.Text == "0")
                    lblDue.Text = "-";
                else
                    lblDue.Text = commonMethods.ChangetToUK(lblDue.Text) + " €";


                lnkOrderIDUpdate.Text = lnkOrderIDUpdate.Text.Replace("-", "").Substring(0, 9);

                //if (lblNote != null)
                //{
                //    string note = lblNote.Text;
                //    if (note.Length > 15)
                //    {
                //        lblNote.Text = lblNote.Text.Substring(0, 15) + "...";
                //        lblNote.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + note + "')");
                //        lblNote.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                //    }
                //}

                if (lblOrderStatus.Text == "1")
                    lblOrderStatus.Text = "SENT";
                else
                {
                    lnkOrderIDUpdate.Enabled = false;
                    lblOrderStatus.Text = "DELIVERED";
                }
                DateTime dd = Convert.ToDateTime(lblOrderDate.Text);
                lblOrderDate.Text = dd.Day + "/" + dd.Month + "/" + dd.Year;


                lblSubtotal.Text = commonMethods.ChangetToUK(lblSubtotal.Text) + " €";
            }
        }
        catch (Exception ex)
        { }
    }
    protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void imgBtnSelect_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (grdProducts.Rows.Count > 0)
            {
                for (int i = 0; i < grdProducts.Rows.Count; i++)
                {
                    CheckBox chk = grdProducts.Rows[i].FindControl("chkProduct") as CheckBox;
                    if (chk.Checked)
                    {
                        HiddenField hidProductID = grdProducts.Rows[i].FindControl("hidProductID") as HiddenField;

                        bool check = false;
                        for (int j = 0; j < Pids.Count(); j++)
                        {
                            if (Pids[j].ToString() == hidProductID.Value)
                            {
                                check = true;
                                break;
                            }
                        }
                        if (!check)
                        {
                            Pids.Add(Convert.ToInt32(hidProductID.Value));
                        }

                    }
                }
                imgBtnNextToViewOrder.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void imgBtnNextToViewOrder_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            /*
            tblProducts pd = new tblProducts();
            pd.LoadAll();
            for (int i = 0; i < pd.RowCount; i++)
            {
                productid_array += pd.PkProductID.ToString() + ",";
                pd.MoveNext();
            }
            */
            //if (productid_array.Length > 0)
            //{
            //productid_array = productid_array.Substring(0, productid_array.LastIndexOf(','));
            //tblProducts p = new tblProducts();
            //p.GetProductsForOrders(productid_array);

            //if (p.RowCount > 0)
            //{

            //dv = p.DefaultView;
            if (dv == null)
                setDataView();

            if (dv != null)
            {

                lblOrderStatus_For_Final_Orders.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;


                setDataView();
                DataRow[] dr = dv.ToTable("SupplierOrder", true, "pkSupplierID", "sBrandName").Select();
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("pkSupplierID");
                dtTemp.Columns.Add("sBrandName");

                for (int i = 0; i < dr.Count(); i++)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp[0] = dr[i][0].ToString();
                    drTemp[1] = dr[i][1].ToString();
                    dtTemp.Rows.Add(drTemp);
                }

                grdSuppliers.DataSource = dtTemp;
                grdSuppliers.DataBind();

                est_subtotal = 0.0;
                est_grand_total = 0.0;

                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        Label lblSubtotals = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                        est_subtotal += Math.Round(Convert.ToDouble(lblSubtotals.Text), 2);
                    }
                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                    lblSupplierTotal.Text = est_subtotal.ToString("N");
                    est_grand_total += est_subtotal;
                    est_subtotal = 0.0;
                }
                lblGrandFinalTotal.Text = est_grand_total.ToString("N");
                mvMain.SetActiveView(vFinalOrders);


                //DataRow[] dr = dv.ToTable("Suppliers", true, "pkSupplierID").Select();

                //for (int i = 0; i < dr.Count(); i++)
                //{
                //    SupplierIDs.Add(dr[i].ToString());
                //}


                /*
                grdBase.DataSource = dv.ToTable("BaseCategory", true, "pkbasecategoryid", "BaseCat");
                grdBase.DataBind();
                lblOrder.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

                est_subtotal = 0.0;
                est_grand_total = 0.0;

                for (int i = 0; i < grdBase.Rows.Count; i++)
                {
                    GridView grdS = grdBase.Rows[i].FindControl("grdSub") as GridView;
                    for (int j = 0; j < grdS.Rows.Count; j++)
                    {
                        GridView grdO = grdS.Rows[j].FindControl("grdOrders") as GridView;
                        for (int k = 0; k < grdO.Rows.Count; k++)
                        {
                            Label lblSubtotals = grdO.Rows[k].FindControl("lblSubtotals") as Label;
                            est_subtotal += Math.Round(Convert.ToDouble(lblSubtotals.Text), 2);
                        }
                    }
                    Label lblEstimatedSubtotal = grdBase.Rows[i].FindControl("lblEstimatedSubtotal") as Label;
                    lblEstimatedSubtotal.Text = est_subtotal.ToString("N");
                    est_grand_total += est_subtotal;
                    est_subtotal = 0.0;
                }
                lblGrandSubtotal.Text = est_grand_total.ToString("N");
            
                mvMain.SetActiveView(vNewOrders);
                 */
            }
            else
            {
                mvMain.SetActiveView(vSelectProducts);
            }
            //}
            //else
            //{
            //    mvMain.SetActiveView(vSelectProducts);
            //}

        }
        catch (Exception ex)
        { }
    }
    private void setDataView()
    {
        try
        {
            string productid_array = string.Empty;
            tblProducts pd = new tblProducts();
            if (ViewState["OrderUpdate"] != null)
            {
                pd.getOrderProductsIDs(ViewState["OrderUpdate"].ToString());
            }
            else
                pd.LoadAll();
            for (int i = 0; i < pd.RowCount; i++)
            {
                productid_array += pd.GetColumn("PkProductID").ToString() + ",";
                pd.MoveNext();
            }

            if (productid_array.Length > 0)
            {
                productid_array = productid_array.Substring(0, productid_array.LastIndexOf(','));
                tblProducts p = new tblProducts();
                p.GetProductsForOrders(productid_array);
                if (p.RowCount > 0)
                {
                    dv = p.DefaultView;
                }
                else
                {
                    mvMain.SetActiveView(vSelectProducts);
                }
            }
            //if (Pids.Count > 0)
            //{
            //    for (int i = 0; i < Pids.Count(); i++) { productid_array += Pids[i].ToString() + ","; }
            //    if (productid_array.Length > 0)
            //    {
            //        productid_array = productid_array.Substring(0, productid_array.LastIndexOf(','));
            //        tblProducts p = new tblProducts();
            //        p.GetProductsForOrders(productid_array);

            //        if (p.RowCount > 0)
            //        {
            //            dv = p.DefaultView;

            //        }
            //    }
            //}
        }
        catch (Exception ex)
        { }


    }
    protected void imgBtnNextToFinal_Click(object sender, ImageClickEventArgs e)
    {
        FinalOrder();
        upnlBase.Update();
        upnlGrand.Update();
        upnlSuppliers.Update();
    }

    #region New Order

    protected void imgBtnNewOrder_Click(object sender, ImageClickEventArgs e)
    {
        Session["orderid"] = null;
        ViewState["OrderUpdate"] = null;
        NewOrder();
    }
    private void NewOrder()
    {
        try
        {
            dv = null;
            if (dv == null)
                setDataView();

            grdBase.DataSource = dv.ToTable("BaseCategory", true, "pkbasecategoryid", "BaseCat");
            grdBase.DataBind();
            lblOrder.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

            est_subtotal = 0.0;
            est_grand_total = 0.0;

            for (int i = 0; i < grdBase.Rows.Count; i++)
            {
                GridView grdS = grdBase.Rows[i].FindControl("grdSub") as GridView;
                for (int j = 0; j < grdS.Rows.Count; j++)
                {
                    GridView grdO = grdS.Rows[j].FindControl("grdOrders") as GridView;
                    for (int k = 0; k < grdO.Rows.Count; k++)
                    {
                        Label lblSubtotals = grdO.Rows[k].FindControl("lblSubtotals") as Label;
                        est_subtotal += Math.Round(Convert.ToDouble(lblSubtotals.Text), 2);
                    }
                }
                Label lblEstimatedSubtotal = grdBase.Rows[i].FindControl("lblEstimatedSubtotal") as Label;
                if (est_subtotal == 0.0)
                    lblEstimatedSubtotal.Text = "00,00";
                else
                    lblEstimatedSubtotal.Text = est_subtotal.ToString("N");
                est_grand_total += est_subtotal;
                est_subtotal = 0.0;
            }
            if (est_grand_total == 0.0)
                lblGrandSubtotal.Text = "00,00";
            else
                lblGrandSubtotal.Text = est_grand_total.ToString("N");
            upnlGrandSubtotal.Update();
            mvMain.SetActiveView(vNewOrders);
        }
        catch (Exception ex)
        {

        }

    }

    private void LoadBaseCategories()
    {
        tblBaseCategories bCat = new tblBaseCategories();
        bCat.GetDepartmentBaseCategories(DepartmentID);
        if (bCat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlBaseCategories, bCat.DefaultView, "CatagoryName", "PkBaseCategoryID");
            ddlBaseCategories.Items.Insert(0, new ListItem("Select Base Category", "0"));
        }
        else
        {
            ddlBaseCategories.Items.Clear();
            ddlBaseCategories.Items.Insert(0, new ListItem("Select Base Category", "0"));
        }
    }
    protected void ddlSubCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubCategories.SelectedValue != "0")
        {
            tblProducts prod = new tblProducts();
            prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
            grdProducts.DataSource = prod.DefaultView;
            grdProducts.DataBind();
        }
    }
    protected void ddlBaseCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBaseCategories.SelectedValue != "0")
        {
            tblSubCategories sub = new tblSubCategories();
            sub.GetSubCategories(Convert.ToInt32(ddlBaseCategories.SelectedValue));
            if (sub.RowCount > 0)
            {
                ddlSubCategories.Items.Clear();
                commonMethods.FillDropDownList(ddlSubCategories, sub.DefaultView, "CSubCategoryName", "PkSubCategoryID");
                ddlSubCategories.Items.Insert(0, new ListItem("Select Sub Category", "0"));
            }
        }
        else
        {
            ddlSubCategories.Items.Clear();
            ddlSubCategories.Items.Insert(0, new ListItem("Select Sub Category", "0"));
        }
        grdProducts.DataSource = null;
        grdProducts.DataBind();
    }

    #endregion

    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProducts.PageIndex = e.NewPageIndex;
        tblProducts prod = new tblProducts();
        prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
        grdProducts.DataSource = prod.DefaultView;
        grdProducts.DataBind();
    }
    protected void grdOrdersHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOrdersHistory.PageIndex = e.NewPageIndex;
        GetAllOrders();
    }
    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdBase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grdSub = e.Row.FindControl("grdSub") as GridView;
                HiddenField hidBase = e.Row.FindControl("hidBase") as HiddenField;

                DataRow[] dr = dv.ToTable("SubCategory", true, "pkbasecategoryid", "pksubcategoryid", "SubCat").Select("pkbasecategoryid = '" + hidBase.Value + "'");
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("pkbasecategoryid");
                dtTemp.Columns.Add("pksubcategoryid");
                dtTemp.Columns.Add("SubCat");
                for (int i = 0; i < dr.Count(); i++)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp[0] = dr[i][0].ToString();
                    drTemp[1] = dr[i][1].ToString();
                    drTemp[2] = dr[i][2].ToString();
                    dtTemp.Rows.Add(drTemp);
                }
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                grdSub.DataSource = dtTemp;

                //grdSub.DataSource = (new System.Collections.Generic.Mscorlib_CollectionDebugView<System.Data.DataRow>(dv.ToTable("SubCategory", true, "pkbasecategoryid", "pksubcategoryid", "SubCat").Select("pkbasecategoryid = '" + hidBase.Value + "'").ToList())).Items[0].Table;
                grdSub.DataBind();
            }
        }
        catch (Exception)
        { }
    }
    protected void grdBase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdSub_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hidBase_grdSub = e.Row.FindControl("hidBase_grdSub") as HiddenField;
                HiddenField hidSub = e.Row.FindControl("hidSub") as HiddenField;
                GridView grdOrders = e.Row.FindControl("grdOrders") as GridView;
                if (dv == null)
                    setDataView();
                DataRow[] dr = dv.ToTable("Products", true, "pkbasecategoryid", "pksubcategoryid", "pkproductid", "Product").Select("pkbasecategoryid = '" + hidBase_grdSub.Value + "' and pksubcategoryid = '" + hidSub.Value + "'");
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("pkbasecategoryid");
                dtTemp.Columns.Add("pksubcategoryid");
                dtTemp.Columns.Add("pkproductid");
                dtTemp.Columns.Add("Product");
                for (int i = 0; i < dr.Count(); i++)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp[0] = dr[i][0].ToString();
                    drTemp[1] = dr[i][1].ToString();
                    drTemp[2] = dr[i][2].ToString();
                    drTemp[3] = dr[i][3].ToString();
                    dtTemp.Rows.Add(drTemp);
                }
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;

                grdOrders.DataSource = dtTemp;
                grdOrders.DataBind();
            }
        }
        catch (Exception)
        { }
    }
    protected void grdSub_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
            }
            else
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;

                LinkButton lnkProductName = (LinkButton)e.Row.FindControl("lnkProductName");
                DropDownList ddlPacking = e.Row.FindControl("ddlPacking") as DropDownList;
                DropDownList ddlQuantity = e.Row.FindControl("ddlQuantity") as DropDownList;
                DropDownList ddlSuppliers = e.Row.FindControl("ddlSuppliers") as DropDownList;
                DropDownList ddlQty = e.Row.FindControl("ddlQty") as DropDownList;

                HiddenField hidProductID = e.Row.FindControl("hidProductID") as HiddenField;
                TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;
                //Label lblPrice = e.Row.FindControl("lblPrice") as Label;
                TextBox lblPrice = e.Row.FindControl("lblPrice") as TextBox;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblAfterVat = e.Row.FindControl("lblAfterVat") as Label;
                Label lblSubtotals = e.Row.FindControl("lblSubtotals") as Label;
                double afterVat = 0.0;
                double subtotal = 0.0;
                if (lnkProductName != null)
                {
                    string name = lnkProductName.Text;
                    if (name.Length > 15)
                    {
                        lnkProductName.Text = lnkProductName.Text.Substring(0, 15) + "...";
                        lnkProductName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lnkProductName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                    }
                }
                if (ddlPacking != null)
                    BindPacking_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlPacking);
                //if (ddlQuantity != null)
                //    BindQuantity_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlPacking, ref ddlQuantity);
                if (ddlSuppliers != null)
                {
                    BindSupplier_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlSuppliers, ref lblPrice, ref ddlPacking);
                }

                if (ViewState["OrderUpdate"] != null)
                {
                    tblProducts p = new tblProducts();
                    p.getOrderPackingSupplier(ViewState["OrderUpdate"].ToString(), Convert.ToInt32(hidProductID.Value));
                    if (p.RowCount > 0)
                    {
                        ddlPacking.SelectedValue = p.GetColumn("fkproductpackageid").ToString();
                        ddlSuppliers.SelectedValue = p.GetColumn("fksupplierid").ToString();
                        txtQty.Text = p.GetColumn("quantity").ToString();
                    }

                }

                if (lblVat != null)
                    GetVAT_ForOrders(Convert.ToInt32(hidProductID.Value), ref lblVat);

                if (lblAfterVat != null)
                {
                    if (lblPrice.Text == "")
                    {
                        DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price", "pkSupplierID").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "' ");
                        if (dr2[0][2].ToString() != "")
                            lblPrice.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
                    }

                    if (lblPrice.Text != "")
                    {
                        double prc = commonMethods.ChangeToUS(lblPrice.Text);
                        afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                        lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";

                    }
                    //if (lblPrice.Text == "")
                    //    lblPrice.Text = "00.00";
                    //lblPrice.Text = commonMethods.ChangetToUK(lblPrice.Text);

                }
                if (lblSubtotals != null)
                {
                    if (lblPrice.Text != "")
                    {
                        subtotal = afterVat * Convert.ToDouble(txtQty.Text);
                        if (subtotal == 0.0)
                            lblSubtotals.Text = "00,00";
                        else
                            lblSubtotals.Text = subtotal.ToString("N");
                    }
                    else
                    {
                        lblSubtotals.Text = "00,00";
                    }
                }

                if (ddlSuppliers != null)
                    lnkProductName.PostBackUrl = "ManageSupplier.aspx?id=" + ddlSuppliers.SelectedValue.ToString();
                #region comment
                //Label lblEstimatedSubtotal = ((GridView)sender).Parent.FindControl("grdsub").Parent.FindControl("grdbase").FindControl("lblEstimatedSubtotal") as Label;
                //string id = ((GridView)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ID;

                /*
                Label lblEstimatedSubtotal = this.Master.FindControl("ContentPlaceHolder1").FindControl("grdBase").FindControl("lblEstimatedSubtotal") as Label;
                if (lblEstimatedSubtotal != null)
                {
                    double est_subtotal = 0.0;
                    est_subtotal = Convert.ToDouble(lblEstimatedSubtotal.Text);
                    est_subtotal += subtotal;
                    lblEstimatedSubtotal.Text = est_subtotal.ToString("N");
                }
                 */
                #endregion
            }
        }
        catch (Exception ex)
        { }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "data", " $(function(){WaterMark();});", true);
    }
    private void Get_SupplierIDs_For_Order(ref DropDownList ddlSuppliers, ref DropDownList ddlPacking, ref HiddenField hidProductID)
    {
        try
        {
            bool check = false;
            for (int i = 0; i < SupplierIDs.Count(); i++)
            {
                if (SupplierIDs[i].ToString() == ddlSuppliers.SelectedValue)
                {
                    check = true;
                    break;
                }
            }
            if (!check)
            {
                SupplierIDs.Add(ddlSuppliers.SelectedValue);
            }

            tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
            rel.GetID(Convert.ToInt32(hidProductID.Value), Convert.ToInt32(ddlPacking.SelectedValue));
            if (rel.RowCount > 0)
            {
                dvp.Add(ddlSuppliers.SelectedValue, rel.PkProductPackingQuantityRelID.ToString());
            }

        }
        catch (Exception)
        { }
    }
    private void BindPacking_ForOrder(int productid, ref DropDownList ddlPacking)
    {
        try
        {
            if (dv == null)
                setDataView();
            DataRow[] dr = dv.ToTable("Packing", true, "pkProductPackageID", "pName", "qName", "pkproductid").Select("pkproductid = '" + productid + "'");
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("pkProductPackageID");
            dtTemp.Columns.Add("pName");
            dtTemp.Columns.Add("qName");

            for (int i = 0; i < dr.Count(); i++)
            {
                DataRow drTemp = dtTemp.NewRow();
                drTemp[0] = dr[i][0].ToString();
                drTemp[1] = dr[i][1].ToString();
                drTemp[2] = dr[i][2].ToString();

                dtTemp.Rows.Add(drTemp);
            }

            ddlPacking.Items.Clear();
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                ddlPacking.Items.Add(new ListItem(dtTemp.Rows[i]["pName"].ToString() + " " + dtTemp.Rows[i]["qName"].ToString(), dtTemp.Rows[i]["pkProductPackageID"].ToString()));
            }

            // commonMethods.FillDropDownList(ddlPacking, dtTemp.DefaultView, "pName", "pkProductPackageID");
        }
        catch (Exception)
        { }
        upnlBase.Update();
    }
    private void BindQuantity_ForOrder(int productid, ref DropDownList ddlPacking, ref DropDownList ddlQuantity)
    {
        try
        {
            if (dv == null)
                setDataView();
            DataRow[] dr = dv.ToTable("Quantity", true, "pkProductPackageID", "pkProductQuantityID", "qName", "pkproductid").Select("pkproductid = '" + productid + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "'");
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("pkProductQuantityID");
            dtTemp.Columns.Add("qName");

            for (int i = 0; i < dr.Count(); i++)
            {
                DataRow drTemp = dtTemp.NewRow();
                drTemp[0] = dr[i][1].ToString();
                drTemp[1] = dr[i][2].ToString();

                dtTemp.Rows.Add(drTemp);
            }
            commonMethods.FillDropDownList(ddlQuantity, dtTemp.DefaultView, "qName", "pkProductQuantityID");
        }
        catch (Exception)
        { }
        upnlBase.Update();
    }
    private void BindSupplier_ForOrder(int productid, ref DropDownList ddlSuppliers, ref TextBox lblPrice, ref DropDownList ddlPacking)
    {
        try
        {
            if (dv == null)
                setDataView();
            DataRow[] dr = dv.ToTable("Suppliers", true, "pkSupplierID", "sBrandName", "Price", "Qty", "pkProductPackageID", "pkproductid").Select("pkproductid = '" + productid + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "'", "Price DESC");
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("pkSupplierID");
            dtTemp.Columns.Add("sBrandName");
            dtTemp.Columns.Add("Price");
            dtTemp.Columns.Add("Qty");
            for (int i = 0; i < dr.Count(); i++)
            {
                DataRow drTemp = dtTemp.NewRow();
                drTemp[0] = dr[i][0].ToString();
                drTemp[1] = dr[i][1].ToString();
                drTemp[2] = dr[i][2].ToString();
                drTemp[3] = dr[i][3].ToString();
                dtTemp.Rows.Add(drTemp);
            }
            DataView dataView = new DataView(dtTemp);
            dataView.Sort = "Price asc";


            DataTable dtTempSort = dataView.ToTable();

            int cout = dtTempSort.Rows.Count;

            List<DataRow> drSort = new List<DataRow>();
            List<DataRow> drSortDeleted = new List<DataRow>();

            DataTable dtDeleted = new DataTable();
            dtDeleted.Columns.Add("pkSupplierID");
            dtDeleted.Columns.Add("sBrandName");
            dtDeleted.Columns.Add("Price");
            dtDeleted.Columns.Add("Qty");

            int row = 0;

            while (row < cout)
            {
                if (dtTempSort.Rows[row][2].ToString() == "")
                {
                    DataRow drDel = dtDeleted.NewRow();
                    drDel[0] = dtTempSort.Rows[row][0];
                    drDel[1] = dtTempSort.Rows[row][1];
                    drDel[2] = dtTempSort.Rows[row][2];
                    drDel[3] = dtTempSort.Rows[row][3];
                    dtDeleted.Rows.Add(drDel);

                    dtTempSort.Rows.Remove(dtTempSort.Rows[row]);
                    dtTempSort.AcceptChanges();
                    //drSort.Add(dtTempSort.Rows[i]);
                    //row++;

                    cout = dtTempSort.Rows.Count;
                    row = 0;
                }
                else
                {
                    row++;
                }
            }


            if (dtDeleted.Rows.Count > 0)
            {
                for (int i = 0; i < dtDeleted.Rows.Count; i++)
                {
                    DataRow drTemp = dtTempSort.NewRow();
                    drTemp[0] = dtDeleted.Rows[i][0];
                    drTemp[1] = dtDeleted.Rows[i][1];
                    drTemp[2] = dtDeleted.Rows[i][2];
                    drTemp[3] = dtDeleted.Rows[i][3];
                    dtTempSort.Rows.Add(drTemp);
                }
            }

            commonMethods.FillDropDownList(ddlSuppliers, dtTempSort.DefaultView, "sBrandName", "pkSupplierID");
            if (dtTempSort.Rows[0][2].ToString() != "")
                lblPrice.Text = commonMethods.ChangetToUK(dtTempSort.Rows[0][2].ToString());

        }
        catch (Exception ex)
        { }
        upnlBase.Update();
    }
    private void GetVAT_ForOrders(int productid, ref Label lblVat)
    {
        try
        {
            if (dv == null)
                setDataView();
            DataRow[] dr = dv.ToTable("vat", true, "pkproductid", "vat").Select("pkproductid = '" + productid + "'");
            lblVat.Text = dr[0][1].ToString();
        }
        catch (Exception)
        { }
        upnlBase.Update();
    }
    protected void grdOrders_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlPacking = ((DropDownList)sender).Parent.FindControl("ddlPacking") as DropDownList;
            DropDownList ddlSuppliers = ((DropDownList)sender).Parent.FindControl("ddlSuppliers") as DropDownList;
            DropDownList ddlQty = ((DropDownList)sender).Parent.FindControl("ddlQty") as DropDownList;

            HiddenField hidProductID = ((DropDownList)sender).Parent.FindControl("hidProductID") as HiddenField;
            //Label lblPrice = ((DropDownList)sender).Parent.FindControl("lblPrice") as Label;
            TextBox lblPrice = ((DropDownList)sender).Parent.FindControl("lblPrice") as TextBox;

            Label lblVat = ((DropDownList)sender).Parent.FindControl("lblVat") as Label;
            Label lblAfterVat = ((DropDownList)sender).Parent.FindControl("lblAfterVat") as Label;
            Label lblSubtotals = ((DropDownList)sender).Parent.FindControl("lblSubtotals") as Label;
            TextBox txtQty = ((DropDownList)sender).Parent.FindControl("txtQty") as TextBox;

            double afterVat = 0.0;
            double subtotal = 0.0;
            if (dv == null)
                setDataView();
            DataRow[] dr = dv.ToTable("Suppliers", true, "pkSupplierID", "pkProductPackageID", "Price", "pkproductid").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "' and  pkProductPackageID = '" + ddlPacking.SelectedValue + "'", "Price");

            if (dr[0][2].ToString() != "")
                lblPrice.Text = commonMethods.ChangetToUK(dr[0][2].ToString());
            else
                lblPrice.Text = "";
            if (lblPrice.Text != "")
            {
                if (lblAfterVat != null)
                {
                    double prc = commonMethods.ChangeToUS(lblPrice.Text);
                    afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                    lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                }
                if (lblSubtotals != null)
                {
                    subtotal = afterVat * commonMethods.ChangeToUS(txtQty.Text);
                    lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                }
                change = true;

                est_subtotal = 0.0;
                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        Label lblSubtotal = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                        est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotal.Text), 2);
                    }
                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                    lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                    est_grand_total += est_subtotal;
                    est_subtotal = 0.0;
                }
                lblGrandFinalTotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                est_grand_total = 0.0;
                upnlSuppliers.Update();
                upnlGrand.Update();
                //  Get_SupplierIDs_For_Order(ref ddlSuppliers);
            }
            else
            {
                txtQty.Text = "0";
                lblSubtotals.Text = "00,00";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "time", "$(function(){TimeRange();});", true);
        }
        catch (Exception)
        { }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "data", " $(function(){WaterMark();});", true);
        upnlBase.Update();
    }
    protected void ddlQty_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlSuppliers = ((DropDownList)sender).Parent.FindControl("ddlSuppliers") as DropDownList;
            DropDownList ddlQty = ((DropDownList)sender).Parent.FindControl("ddlQty") as DropDownList;
            HiddenField hidProductID = ((DropDownList)sender).Parent.FindControl("hidProductID") as HiddenField;
            Label lblPrice = ((DropDownList)sender).Parent.FindControl("lblPrice") as Label;

            Label lblVat = ((DropDownList)sender).Parent.FindControl("lblVat") as Label;
            Label lblAfterVat = ((DropDownList)sender).Parent.FindControl("lblAfterVat") as Label;
            Label lblSubtotals = ((DropDownList)sender).Parent.FindControl("lblSubtotals") as Label;
            double afterVat = 0.0;
            double subtotal = 0.0;

            DataRow[] dr = dv.ToTable("Suppliers", true, "pkSupplierID", "sBrandName", "UnitPrice", "Qty", "pkproductid").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "'", "UnitPrice");

            lblPrice.Text = dr[0][2].ToString();


            if (lblAfterVat != null)
            {
                afterVat = Convert.ToDouble(lblPrice.Text) + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * Convert.ToDouble(lblPrice.Text)) / 100;
                lblAfterVat.Text = afterVat.ToString("N") + "€";
            }
            if (lblSubtotals != null)
            {
                subtotal = Convert.ToDouble(lblAfterVat.Text.Replace("€", "")) * Convert.ToDouble(ddlQty.SelectedItem.Text);
                lblSubtotals.Text = subtotal.ToString("N");
            }
            change = true;
        }
        catch (Exception)
        { }
        upnlBase.Update();
    }
    protected void ddlPacking_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlPacking = ((DropDownList)sender).Parent.FindControl("ddlPacking") as DropDownList;
            DropDownList ddlSuppliers = ((DropDownList)sender).Parent.FindControl("ddlSuppliers") as DropDownList;
            DropDownList ddlQuantity = ((DropDownList)sender).Parent.FindControl("ddlQuantity") as DropDownList;
            HiddenField hidProductID = ((DropDownList)sender).Parent.FindControl("hidProductID") as HiddenField;
            //Label lblPrice = ((DropDownList)sender).Parent.FindControl("lblPrice") as Label;
            TextBox lblPrice = ((DropDownList)sender).Parent.FindControl("lblPrice") as TextBox;
            Label lblVat = ((DropDownList)sender).Parent.FindControl("lblVat") as Label;
            Label lblAfterVat = ((DropDownList)sender).Parent.FindControl("lblAfterVat") as Label;
            Label lblSubtotals = ((DropDownList)sender).Parent.FindControl("lblSubtotals") as Label;
            TextBox txtQty = ((DropDownList)sender).Parent.FindControl("txtQty") as TextBox;
            double afterVat = 0.0;
            double subtotal = 0.0;
            if (dv == null)
                setDataView();

            DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price", "pkSupplierID").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "' and  pkProductPackageID = '" + ddlPacking.SelectedValue + "'");
            if (dr2[0][2].ToString() != "")
                lblPrice.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
            if (lblPrice.Text != "")
            {
                if (lblAfterVat != null)
                {
                    double prc = commonMethods.ChangeToUS(lblPrice.Text);
                    afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                    lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N"));
                }
                if (lblSubtotals != null)
                {
                    subtotal = afterVat * commonMethods.ChangeToUS(txtQty.Text);
                    lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                }

                est_subtotal = 0.0;
                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        Label lblSubtotal = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                        est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotal.Text), 2);
                    }
                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                    lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                    est_grand_total += est_subtotal;
                    est_subtotal = 0.0;
                }
                lblGrandFinalTotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                est_grand_total = 0.0;

                change = true;
                upnlSuppliers.Update();
            }
            else
            {
                txtQty.Text = "0";
                lblSubtotals.Text = "00,00";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "time", "$(function(){TimeRange();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "data", " $(function(){WaterMark();});", true);
        }
        catch (Exception ex)
        { }
        upnlBase.Update();
    }
    protected void ddlQuantity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //DropDownList ddlPacking = ((DropDownList)sender).Parent.FindControl("ddlPacking") as DropDownList;
            //DropDownList ddlQuantity = ((DropDownList)sender).Parent.FindControl("ddlQuantity") as DropDownList;
            //HiddenField hidProductID = ((DropDownList)sender).Parent.FindControl("hidProductID") as HiddenField;
            //Label lblPrice = ((DropDownList)sender).Parent.FindControl("lblPrice") as Label;

            //if (dv == null)
            //    setDataView();
            //DataRow[] dr = dv.ToTable("Quantity", true, "pkProductPackageID", "pkProductQuantityID", "pName", "pkproductid").Select("pkproductid = '" + hidProductID.Value + "' and pkProductQuantityID = '" + ddlQuantity.SelectedValue + "'");
            //DataTable dtTemp = new DataTable();
            //dtTemp.Columns.Add("pkProductPackageID");
            //dtTemp.Columns.Add("pName");

            //for (int i = 0; i < dr.Count(); i++)
            //{
            //    DataRow drTemp = dtTemp.NewRow();
            //    drTemp[0] = dr[i][1].ToString();
            //    drTemp[1] = dr[i][2].ToString();

            //    dtTemp.Rows.Add(drTemp);
            //}
            //commonMethods.FillDropDownList(ddlPacking, dtTemp.DefaultView, "pName", "pkProductPackageID");
            //DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkProductQuantityID", "qName", "pkproductid", "price").Select("pkproductid = '" + hidProductID.Value + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "' and pkProductQuantityID = '" + ddlQuantity.SelectedValue + "'");
            //lblPrice.Text = commonMethods.ChangetToUK(dr2[0][4].ToString()) + " €";
            //change = true;
            //upnlSuppliers.Update();

        }
        catch (Exception ex)
        { }
        upnlBase.Update();

    }

    #region Final Orders

    protected void grdOrders_FinalOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
            }
            else
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;

                LinkButton lnkProductName = (LinkButton)e.Row.FindControl("lnkProductName");
                DropDownList ddlPacking = e.Row.FindControl("ddlPacking") as DropDownList;
                DropDownList ddlQuantity = e.Row.FindControl("ddlQuantity") as DropDownList;
                DropDownList ddlSuppliers = e.Row.FindControl("ddlSuppliers") as DropDownList;
                DropDownList ddlQty = e.Row.FindControl("ddlQty") as DropDownList;
                TextBox txtQty = e.Row.FindControl("txtQty") as TextBox;


                HiddenField hidProductID = e.Row.FindControl("hidProductID") as HiddenField;
                HiddenField hidPSupplierid = e.Row.FindControl("hidPSupplierid") as HiddenField;


                //Label lblPrice = e.Row.FindControl("lblPrice") as Label;
                TextBox lblPrice = e.Row.FindControl("lblPrice") as TextBox;
                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblAfterVat = e.Row.FindControl("lblAfterVat") as Label;
                Label lblSubtotals = e.Row.FindControl("lblSubtotals") as Label;
                double afterVat = 0.0;
                double subtotal = 0.0;
                if (lnkProductName != null)
                {
                    string name = lnkProductName.Text;
                    if (name.Length > 15)
                    {
                        lnkProductName.Text = lnkProductName.Text.Substring(0, 15) + "...";
                        lnkProductName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lnkProductName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                    }
                }
                int qty = 0;
                foreach (var item in dvpForQuantity)
                {
                    if (item.Key == hidProductID.Value)
                    {
                        qty = Convert.ToInt32(item.Value);
                        break;
                    }
                }
                if (qty != 0)
                    txtQty.Text = qty.ToString();



                if (ddlPacking != null)
                    BindPacking_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlPacking);

                foreach (var item in dvpForPackingID)
                {
                    if (item.Key == hidProductID.Value)
                    {
                        ddlPacking.SelectedValue = item.Value;
                        break;
                    }
                }




                if (ddlSuppliers != null)
                {
                    BindSupplier_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlSuppliers, ref lblPrice, ref ddlPacking);
                    ddlSuppliers.SelectedValue = hidPSupplierid.Value;
                    // Get_SupplierIDs_For_Order(ref ddlSuppliers);
                }
                if (lblVat != null)
                    GetVAT_ForOrders(Convert.ToInt32(hidProductID.Value), ref lblVat);

                if (lblAfterVat != null)
                {
                    if (lblPrice.Text == "")
                    {
                        DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price").Select("pkproductid = '" + hidProductID.Value + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "'");
                        if (dr2[0][2].ToString() != "")
                            lblPrice.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
                    }
                    if (lblPrice.Text != "")
                    {
                        double prc = commonMethods.ChangeToUS(lblPrice.Text);
                        afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                        lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                    }
                    foreach (var item in dvpForPrice)
                    {
                        if (item.Key == hidProductID.Value)
                        {
                            lblPrice.Text = item.Value;
                            double prc = commonMethods.ChangeToUS(lblPrice.Text);
                            afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                            lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                            break;
                        }
                    }

                }
                if (lblSubtotals != null)
                {
                    if (lblPrice.Text != "")
                    {
                        subtotal = afterVat * commonMethods.ChangeToUS(txtQty.Text);
                        lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                    }
                    else
                    {
                        lblSubtotals.Text = "00,00";
                    }

                }
                if (ddlSuppliers != null)
                    lnkProductName.PostBackUrl = "ManageSupplier.aspx?id=" + ddlSuppliers.SelectedValue.ToString();
                //Label lblEstimatedSubtotal = ((GridView)sender).Parent.FindControl("grdsub").Parent.FindControl("grdbase").FindControl("lblEstimatedSubtotal") as Label;
                //string id = ((GridView)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.ID;

                /*
                Label lblEstimatedSubtotal = this.Master.FindControl("ContentPlaceHolder1").FindControl("grdBase").FindControl("lblEstimatedSubtotal") as Label;
                if (lblEstimatedSubtotal != null)
                {
                    double est_subtotal = 0.0;
                    est_subtotal = Convert.ToDouble(lblEstimatedSubtotal.Text);
                    est_subtotal += subtotal;
                    lblEstimatedSubtotal.Text = est_subtotal.ToString("N");
                }
                 */

            }
        }
        catch (Exception ex)
        { }
    }
    protected void grdOrders_FinalOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdSuppliers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hidSupplierID = e.Row.FindControl("hidSupplierID") as HiddenField;
                HiddenField hidFavorite = e.Row.FindControl("hidFavorite") as HiddenField;

                TextBox txtDeliveryTime = e.Row.FindControl("txtDeliveryTime") as TextBox;
                TextBox txtDeliveryTimeEnd = e.Row.FindControl("txtDeliveryTimeEnd") as TextBox;

                //ImageButton btnDeliveryTime = e.Row.FindControl("btnDeliveryTime") as ImageButton;
                //ImageButton btnDeliveryTimeEnd = e.Row.FindControl("btnDeliveryTimeEnd") as ImageButton;
                //btnDeliveryTime.Attributes.Add("onclick", "javascript:setTime('" + btnDeliveryTime.ClientID + "');");
                //btnDeliveryTimeEnd.Attributes.Add("onclick", "javascript:setTime('" + btnDeliveryTimeEnd.ClientID + "');");

                TextBox txtComment = e.Row.FindControl("txtComment") as TextBox;

                GridView grdOrders_FinalOrder = e.Row.FindControl("grdOrders_FinalOrder") as GridView;

                Label lblSupplierName = e.Row.FindControl("lblSupplierName") as Label;
                Label lblSTotal = e.Row.FindControl("lblSTotal") as Label;

                DropDownList ddlFavoriteMethod = e.Row.FindControl("ddlFavoriteMethod") as DropDownList;
                DataRow[] drFavorite = dv.ToTable("FavoriteMethod", true, "pkSupplierID", "sContactMethod_Email", "sContactMethod_Fax", "sContactMethod_Phone").Select("pkSupplierID = '" + hidSupplierID.Value + "'");
                if (drFavorite.Count() > 0)
                {
                    if (Convert.ToBoolean(drFavorite[0][1]))
                        hidFavorite.Value = "Email";
                    else if (Convert.ToBoolean(drFavorite[0][2]))
                        hidFavorite.Value = "Fax";
                    else if (Convert.ToBoolean(drFavorite[0][3]))
                        hidFavorite.Value = "Phone";

                    ddlFavoriteMethod.Items.Add(new ListItem("Email", "1"));
                    ddlFavoriteMethod.Items.Add(new ListItem("Fax", "2"));
                    ddlFavoriteMethod.Items.Add(new ListItem("Phone", "3"));
                    ddlFavoriteMethod.Items.Insert(0, new ListItem("Favorite Other Method", "0"));
                }

                string queryExpression = "";
                for (int l = 0; l < Pids.Count; l++)
                {
                    queryExpression += "pkProductid = " + Pids[l].ToString() + " or ";
                }
                queryExpression = queryExpression.Substring(0, queryExpression.Length - 4);

                string queryExpressionForPacking = " and (";
                foreach (var item in dvp)
                {
                    if (item.Key == hidSupplierID.Value)
                        queryExpressionForPacking += "pkProductPackingQuantityRelID = " + item.Value + " or ";
                }
                queryExpressionForPacking = queryExpressionForPacking.Substring(0, queryExpressionForPacking.Length - 4) + ")";

                if (dv == null)
                    setDataView();
                DataRow[] dr = dv.ToTable("Products_Final", true, "pkbasecategoryid", "pksubcategoryid", "pkproductid", "Product", "pkSupplierID", "pkProductPackingQuantityRelID").Select("pkSupplierID = '" + hidSupplierID.Value + "' and (" + queryExpression + ")" + queryExpressionForPacking);
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("pkbasecategoryid");
                dtTemp.Columns.Add("pksubcategoryid");
                dtTemp.Columns.Add("pkproductid");
                dtTemp.Columns.Add("Product");
                dtTemp.Columns.Add("pkSupplierID");
                for (int i = 0; i < dr.Count(); i++)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp[0] = dr[i][0].ToString();
                    drTemp[1] = dr[i][1].ToString();
                    drTemp[2] = dr[i][2].ToString();
                    drTemp[3] = dr[i][3].ToString();
                    drTemp[4] = dr[i][4].ToString();
                    dtTemp.Rows.Add(drTemp);
                }



                grdOrders_FinalOrder.DataSource = dtTemp;
                grdOrders_FinalOrder.DataBind();

                if (ViewState["OrderUpdate"] != null)
                {
                    tblProducts p = new tblProducts();
                    p.getDeliveryTime(ViewState["OrderUpdate"].ToString(), Convert.ToInt32(hidSupplierID.Value));
                    if (p.RowCount > 0)
                    {
                        string[] split = p.GetColumn("deliverytime").ToString().Split('-');
                        txtDeliveryTime.Text = split[0].ToString();
                        txtDeliveryTimeEnd.Text = split[1].ToString();
                        txtComment.Text = p.GetColumn("ordernote").ToString();
                    }

                }

                lblSTotal.Text = lblSupplierName.Text + " Total:";
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }


    protected void btnDeliveryTime_Click(object sender, EventArgs e)
    {
        ImageButton btnDeliveryTime = ((ImageButton)sender).Parent.FindControl("btnDeliveryTime") as ImageButton;
        TextBox txtDeliveryTime = ((ImageButton)sender).Parent.FindControl("txtDeliveryTime") as TextBox;
        textboxid = txtDeliveryTime.ClientID;
        setTime();
        MPEDate.Show();

    }
    protected void btnDeliveryTimeEnd_Click(object sender, EventArgs e)
    {
        ImageButton btnDeliveryTimeEnd = ((ImageButton)sender).Parent.FindControl("btnDeliveryTimeEnd") as ImageButton;
        TextBox txtDeliveryTimeEnd = ((ImageButton)sender).Parent.FindControl("txtDeliveryTimeEnd") as TextBox;
        textboxid = txtDeliveryTimeEnd.ClientID;
        setTime();
        MPEDate.Show();
    }

    protected void lnkOrder_Click(object sender, EventArgs e)
    {
        try
        {
            if (orderCheck && !change)
            {
                change = false;
                LinkButton lnkOrder = ((LinkButton)sender).Parent.FindControl("lnkOrder") as LinkButton;
                HiddenField hidFavorite = ((LinkButton)sender).Parent.FindControl("hidFavorite") as HiddenField;
                DropDownList ddlFavoriteMethod = ((LinkButton)sender).Parent.FindControl("ddlFavoriteMethod") as DropDownList;
                Label lblOrderMessage = ((LinkButton)sender).Parent.FindControl("lblOrderMessage") as Label;
                CheckBox chkReady = ((LinkButton)sender).Parent.FindControl("chkReady") as CheckBox;
                GridView grdOrderEmail = ((LinkButton)sender).Parent.FindControl("grdOrders_FinalOrder") as GridView;
                TextBox txtComment = ((LinkButton)sender).Parent.FindControl("txtComment") as TextBox;
                TextBox txtDeliveryTime = ((LinkButton)sender).Parent.FindControl("txtDeliveryTime") as TextBox;

                string pName = string.Empty;
                string size = string.Empty;

                string qty = string.Empty;
                string supplierN = string.Empty;

                string supplierAddress = string.Empty;
                bool Order = false;
                for (int i = 0; i < grdOrderEmail.Rows.Count; i++)
                {
                    Label lblSubtotals = grdOrderEmail.Rows[i].FindControl("lblSubtotals") as Label;
                    LinkButton lnkProductName = grdOrderEmail.Rows[i].FindControl("lnkProductName") as LinkButton;
                    DropDownList ddlPacking = grdOrderEmail.Rows[i].FindControl("ddlPacking") as DropDownList;
                    DropDownList ddlSuppliers = grdOrderEmail.Rows[i].FindControl("ddlSuppliers") as DropDownList;

                    TextBox txtQty = grdOrderEmail.Rows[i].FindControl("txtQty") as TextBox;
                    if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                    {
                        Order = true;
                        pName += lnkProductName.Text + ",";
                        size += ddlPacking.SelectedItem.Text + ",";
                        qty += txtQty.Text + ",";
                        if (supplierN == "")
                        {
                            supplierN = ddlSuppliers.SelectedItem.Text;
                            tblSupplierEmails email = new tblSupplierEmails();
                            email.GetActiveEmail(Convert.ToInt32(ddlSuppliers.SelectedValue));
                            if (email.RowCount > 0)
                            {
                                supplierAddress = email.s_SEmail;
                            }
                        }
                    }
                }
                if (Order)
                {
                    string orderStatus = "Order of " + lblOrderStatus_For_Final_Orders.Text;
                    pdfhtml.SetValues(pName, size, qty, supplierN, txtComment.Text, txtDeliveryTime.Text, orderStatus);

                    //string html = GenerateHTMLForOrder(supplierN, lblOrderStatus_For_Final_Orders.Text, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);


                    if (ddlFavoriteMethod.SelectedValue != "0")
                    {
                        if (ddlFavoriteMethod.SelectedValue == "1")
                        {
                            Emailing email = new Emailing();
                            email.P_FromAddress = "noreply@west.com";
                            email.P_ToAddress = supplierAddress;
                            email.P_Email_Subject = "Order";
                            for (int i = 0; i < pName.Substring(0, pName.Length - 1).Split(',').Length; i++)
                            {
                                email.P_Message_Body += (pName.Substring(0, pName.Length - 1).Split(','))[i].ToString() + " ------- " + (qty.Substring(0, qty.Length - 1).Split(','))[i].ToString() + " x " + (size.Substring(0, size.Length - 1).Split(','))[i].ToString() + "</br>"; ;
                            }

                            email.P_Message_Body += "NOTES :" + txtComment.Text + "</br>";
                            email.P_Message_Body += "Delivery Time: " + txtDeliveryTime.Text + "</br>";
                            email.Send_Email();


                            lblOrderMessage.Text = "ORDERED (by email). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                            chkReady.Checked = true;
                        }
                        else if (ddlFavoriteMethod.SelectedValue == "2")
                        {
                            lblOrderMessage.Text = "ORDERED (by fax). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                            chkReady.Checked = true;
                        }
                        else if (ddlFavoriteMethod.SelectedValue == "3")
                        {
                            lblOrderMessage.Text = "Not Ordered yet! (by Phone) please print, then select ready!";
                        }
                    }
                    else
                    {
                        if (hidFavorite.Value == "Email")
                        {
                            Emailing email = new Emailing();
                            email.P_FromAddress = "noreply@west.com";
                            email.P_ToAddress = supplierAddress;
                            email.P_Email_Subject = "Order";
                            for (int i = 0; i < pName.Substring(0, pName.Length - 1).Split(',').Length; i++)
                            {
                                email.P_Message_Body += pName[i].ToString() + " ------- " + qty[i] + " x " + size[i] + "</br>"; ;
                            }

                            email.P_Message_Body += "NOTES :" + txtComment.Text + "</br>";
                            email.P_Message_Body += "Delivery Time: " + txtDeliveryTime.Text + "</br>";
                            email.Send_Email();
                            lblOrderMessage.Text = "ORDERED (by email). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                            chkReady.Checked = true;
                        }
                        else if (hidFavorite.Value == "Fax")
                        {
                            lblOrderMessage.Text = "ORDERED (by fax). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                            chkReady.Checked = true;
                        }
                        else if (hidFavorite.Value == "Phone")
                        {
                            lblOrderMessage.Text = "Not Ordered yet! (by Phone) please print, then select ready!";
                            lblOrderMessage.ForeColor = Color.Red;
                            lblOrderMessage.Style.Add("font-size", "12px;");
                        }
                    }
                    upnlSuppliers.Update();
                }
                else
                {
                    string message = "Sorry Not Ordered for selected supplier. Please enter quantity greater than 0";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "order", " $(function(){alert('" + message + "');});", true);
                }

            }
            else if (!orderCheck)
            {
                string message = "Please save your order first.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "order", " $(function(){alert('" + message + "');});", true);
            }
        }
        catch (Exception ex)
        { }
    }

    protected void lnkOrderAllByFavoriteMethod_Click(object sender, EventArgs e)
    {
        try
        {
            if (orderCheck && !change)
            {
                change = false;
                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    HiddenField hidFavorite = grdSuppliers.Rows[i].FindControl("hidFavorite") as HiddenField;
                    DropDownList ddlFavoriteMethod = grdSuppliers.Rows[i].FindControl("ddlFavoriteMethod") as DropDownList;
                    Label lblOrderMessage = grdSuppliers.Rows[i].FindControl("lblOrderMessage") as Label;
                    CheckBox chkReady = grdSuppliers.Rows[i].FindControl("chkReady") as CheckBox;
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    TextBox txtComment = grdSuppliers.Rows[i].FindControl("txtComment") as TextBox;
                    TextBox txtDeliveryTime = grdSuppliers.Rows[i].FindControl("txtDeliveryTime") as TextBox;


                    string pName = string.Empty;
                    string size = string.Empty;

                    string qty = string.Empty;
                    string supplierN = string.Empty;
                    bool Order = false;
                    string supplierAddress = string.Empty;
                    if (grdOrders_FinalOrder != null)
                    {
                        for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                        {
                            Label lblSubtotals = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                            LinkButton lnkProductName = grdOrders_FinalOrder.Rows[j].FindControl("lnkProductName") as LinkButton;
                            DropDownList ddlPacking = grdOrders_FinalOrder.Rows[j].FindControl("ddlPacking") as DropDownList;

                            DropDownList ddlSuppliers = grdOrders_FinalOrder.Rows[j].FindControl("ddlSuppliers") as DropDownList;

                            TextBox txtQty = grdOrders_FinalOrder.Rows[j].FindControl("txtQty") as TextBox;
                            if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                            {
                                Order = true;
                                pName += lnkProductName.Text + ",";
                                size += ddlPacking.SelectedItem.Text + ",";
                                qty += txtQty.Text + ",";
                                if (supplierN == "")
                                {
                                    supplierN = ddlSuppliers.SelectedItem.Text;
                                    tblSupplierEmails email = new tblSupplierEmails();
                                    email.GetActiveEmail(Convert.ToInt32(ddlSuppliers.SelectedValue));
                                    if (email.RowCount > 0)
                                    {
                                        supplierAddress = email.s_SEmail;
                                    }
                                }
                            }

                        }
                    }

                    // string html = GenerateHTMLForOrder(supplierN, lblOrderStatus_For_Final_Orders.Text, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
                    if (Order)
                    {
                        if (ddlFavoriteMethod.SelectedValue != "0")
                        {
                            if (ddlFavoriteMethod.SelectedValue == "1")
                            {
                                Emailing email = new Emailing();
                                email.P_FromAddress = "noreply@west.com";
                                email.P_ToAddress = supplierAddress;
                                email.P_Email_Subject = "Order";
                                for (int k = 0; k < pName.Substring(0, pName.Length - 1).Split(',').Length; k++)
                                {
                                    email.P_Message_Body += (pName.Substring(0, pName.Length - 1).Split(','))[k].ToString() + " ------- " + (qty.Substring(0, qty.Length - 1).Split(','))[k].ToString() + " x " + (size.Substring(0, size.Length - 1).Split(','))[k].ToString() + "</br>"; ;
                                }

                                email.P_Message_Body += "NOTES :" + txtComment.Text + "</br>";
                                email.P_Message_Body += "Delivery Time: " + txtDeliveryTime.Text + "</br>";
                                email.Send_Email();
                                lblOrderMessage.Text = "ORDERED (by email). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                chkReady.Checked = true;
                            }
                            else if (ddlFavoriteMethod.SelectedValue == "2")
                            {
                                lblOrderMessage.Text = "ORDERED (by fax). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                chkReady.Checked = true;
                            }
                            else if (ddlFavoriteMethod.SelectedValue == "3")
                            {
                                lblOrderMessage.Text = "Not Ordered yet! (by Phone) please print, then select ready!";
                                lblOrderMessage.ForeColor = Color.Red;
                                lblOrderMessage.Style.Add("font-size", "12px;");
                            }
                        }
                        else
                        {
                            if (hidFavorite.Value == "Email")
                            {
                                Emailing email = new Emailing();
                                email.P_FromAddress = "noreply@west.com";
                                email.P_ToAddress = supplierAddress;
                                email.P_Email_Subject = "Order";
                                for (int k = 0; k < pName.Substring(0, pName.Length - 1).Split(',').Length; k++)
                                {
                                    email.P_Message_Body += (pName.Substring(0, pName.Length - 1).Split(','))[k].ToString() + " ------- " + (qty.Substring(0, qty.Length - 1).Split(','))[k].ToString() + " x " + (size.Substring(0, size.Length - 1).Split(','))[k].ToString() + "</br>"; ;
                                }

                                email.P_Message_Body += "NOTES :" + txtComment.Text + "</br>";
                                email.P_Message_Body += "Delivery Time: " + txtDeliveryTime.Text + "</br>";
                                email.Send_Email();
                                lblOrderMessage.Text = "ORDERED (by email). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                chkReady.Checked = true;
                                lblOrderMessage.Text = "ORDERED (by email). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                chkReady.Checked = true;
                            }
                            else if (hidFavorite.Value == "Fax")
                            {
                                lblOrderMessage.Text = "ORDERED (by fax). " + DateTime.Now.TimeOfDay.Hours + ":" + DateTime.Now.TimeOfDay.Minutes + ", " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                chkReady.Checked = true;
                            }
                            else if (hidFavorite.Value == "Phone")
                            {
                                lblOrderMessage.Text = "Not Ordered yet! (by Phone) please print, then select ready!";
                                lblOrderMessage.ForeColor = Color.Red;
                                lblOrderMessage.Style.Add("font-size", "12px;");
                            }
                        }
                        upnlSuppliers.Update();
                    }
                    else
                    {
                        string message = "Order Quantity should be greater than zero!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "checkOrders", "$(function(){alert('" + message + "');});", true);
                    }
                }

            }
            else if (!orderCheck)
            {
                string message = "Please save your order first.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "order", " $(function(){alert('" + message + "');});", true);
            }
        }
        catch (Exception ex)
        { }
    }
    protected void imgSaveAllTop_Click(object sender, EventArgs e)
    {
        TransactionMgr tx = TransactionMgr.ThreadTransactionMgr();
        try
        {
            tx.BeginTransaction();
            bool baseCheck = false;
            for (int i = 0; i < grdSuppliers.Rows.Count; i++)
            {
                GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;

                for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                {
                    TextBox txtQty = grdOrders_FinalOrder.Rows[j].FindControl("txtQty") as TextBox;
                    if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                    {
                        baseCheck = true;
                        break;
                    }
                    if (baseCheck)
                        break;
                }
            }

            if (baseCheck)
            {

                tblBaseOrder baseOrder = new tblBaseOrder();

                if (Session["orderid"] != null)
                {
                    baseOrder.FlushData();
                    baseOrder.getBaseOrder(Session["orderid"].ToString());
                }
                else
                    CreatedOrderSession();

                try
                {
                    if (baseOrder.RowCount == 0)
                    {
                        baseOrder.AddNew();
                        baseOrder.DCreatedDate = DateTime.Now;
                    }
                }
                catch (NullReferenceException ex)
                {
                    baseOrder.FlushData();
                    baseOrder.AddNew();
                    baseOrder.DCreatedDate = DateTime.Now;
                }

                baseOrder.DModifiedDate = DateTime.Now;
                baseOrder.SessionOrderID = Session["orderid"].ToString();
                baseOrder.GrandSubtotal = commonMethods.ChangeToUS(lblGrandFinalTotal.Text);
                baseOrder.Save();
                //if (Session["orderid"] != null)
                //{
                //if (baseOrder.RowCount > 0)
                //{
                tblOrders o = new tblOrders();
                o.getOrdersByBaseOrder(baseOrder.PkBaseOrderID);
                if (o.RowCount > 0)
                {
                    for (int n = 0; n < o.RowCount; n++)
                    {
                        tblOrderDetail odetail = new tblOrderDetail();
                        odetail.GetOrderDetail(o.PkOrderID);
                        if (odetail.RowCount > 0)
                        {
                            for (int m = 0; m < odetail.RowCount; m++)
                            {
                                odetail.MarkAsDeleted();
                                odetail.Save();
                                odetail.MoveNext();
                            }
                        }
                        o.MarkAsDeleted();
                        o.Save();
                        o.MoveNext();
                    }
                }
                //}
                //}


                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    HiddenField hidSupplierID = grdSuppliers.Rows[i].FindControl("hidSupplierID") as HiddenField;
                    TextBox txtComment = grdSuppliers.Rows[i].FindControl("txtComment") as TextBox;
                    TextBox txtDeliveryTime = grdSuppliers.Rows[i].FindControl("txtDeliveryTime") as TextBox;
                    TextBox txtDeliveryTimeEnd = grdSuppliers.Rows[i].FindControl("txtDeliveryTimeEnd") as TextBox;
                    CheckBox chkReady = grdSuppliers.Rows[i].FindControl("chkReady") as CheckBox;
                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;


                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;

                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        TextBox txtQty = grdOrders_FinalOrder.Rows[j].FindControl("txtQty") as TextBox;
                        if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                        {
                            if (!checkForOrder)
                            {
                                if (hidSupplierID.Value != "")
                                {
                                    tblOrders order = new tblOrders();
                                    order.AddNew();
                                    order.DCreateDate = DateTime.Now;
                                    order.FkBaseOrderID = baseOrder.PkBaseOrderID;
                                    order.FkOrderStatusID = 1;
                                    order.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                                    order.OrderNote = txtComment.Text;
                                    order.OrderSubtotal = commonMethods.ChangeToUS(lblSupplierTotal.Text);
                                    order.FinalSubtotal = commonMethods.ChangeToUS(lblSupplierTotal.Text);
                                    order.OrderReady = chkReady.Checked;
                                    order.DeliveryTime = txtDeliveryTime.Text + "-" + txtDeliveryTimeEnd.Text;
                                    order.DModifiedDate = DateTime.Now;
                                    order.Save();
                                    ViewState["OrderID"] = order.PkOrderID;

                                }

                            }
                            if (ViewState["OrderID"] != null)
                            {
                                HiddenField hidProductID = grdOrders_FinalOrder.Rows[j].FindControl("hidProductID") as HiddenField;
                                DropDownList ddlPacking = grdOrders_FinalOrder.Rows[j].FindControl("ddlPacking") as DropDownList;
                                DropDownList ddlQuantity = grdOrders_FinalOrder.Rows[j].FindControl("ddlQuantity") as DropDownList;

                                DropDownList ddlQty = grdOrders_FinalOrder.Rows[j].FindControl("ddlQty") as DropDownList;


                                //Label lblPrice = grdOrders_FinalOrder.Rows[j].FindControl("lblPrice") as Label;
                                TextBox lblPrice = grdOrders_FinalOrder.Rows[j].FindControl("lblPrice") as TextBox;

                                Label lblVat = grdOrders_FinalOrder.Rows[j].FindControl("lblVat") as Label;
                                Label lblAfterVat = grdOrders_FinalOrder.Rows[j].FindControl("lblAfterVat") as Label;
                                Label lblSubtotals = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                                TextBox txtQt = grdOrders_FinalOrder.Rows[j].FindControl("txtQty") as TextBox;

                                tblOrderDetail orderDetail = new tblOrderDetail();
                                orderDetail.FlushData();
                                orderDetail.AddNew();
                                orderDetail.FkOrderID = Convert.ToInt32(ViewState["OrderID"]);
                                orderDetail.FkProductID = Convert.ToInt32(hidProductID.Value);
                                orderDetail.FkProductPackageID = Convert.ToInt32(ddlPacking.SelectedValue);
                                orderDetail.ProudctPrice = commonMethods.ChangeToUS(lblPrice.Text);
                                orderDetail.Vat = Convert.ToInt32(lblVat.Text.Replace("%", ""));
                                orderDetail.AfterVat = commonMethods.ChangeToUS(lblAfterVat.Text.Replace("€", ""));
                                orderDetail.Quantity = Convert.ToInt32(txtQt.Text);
                                orderDetail.Subtotal = commonMethods.ChangeToUS(lblSubtotals.Text);
                                orderDetail.DCreatedDate = DateTime.Now;
                                orderDetail.DModifiedDate = DateTime.Now;
                                orderDetail.Save();



                                tblProductPackingQuantityRel rel = new tblProductPackingQuantityRel();
                                rel.GetID(Convert.ToInt32(hidProductID.Value), Convert.ToInt32(ddlPacking.SelectedValue));
                                if (rel.RowCount > 0)
                                {
                                    tblSupplierProductPrices spp = new tblSupplierProductPrices();
                                    spp.GETRecord(Convert.ToInt32(hidSupplierID.Value), rel.PkProductPackingQuantityRelID);
                                    if (spp.RowCount > 0)
                                    {
                                        try
                                        {
                                            if (spp.Price == 0.0 || spp.Price == 0 || spp.Price == 0.00)
                                            {
                                                spp.Price = commonMethods.ChangeToUS(lblPrice.Text);
                                                spp.DModifiedDate = DateTime.Now;
                                                spp.Save();
                                            }
                                        }
                                        catch (InvalidCastException ex)
                                        {
                                            spp.Price = commonMethods.ChangeToUS(lblPrice.Text);
                                            spp.DModifiedDate = DateTime.Now;
                                            spp.Save();
                                        }
                                    }
                                }
                                ViewState["OrderID"] = null;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "savingMessage", "$(function(){RecordSaved();});", true);
                            }
                            else
                            {
                                tx.RollbackTransaction();
                                TransactionMgr.ThreadTransactionMgrReset();
                                ViewState["OrderDetailID"] = null;
                                ViewState["OrderID"] = null;

                                string message = "Error on saving order. Please click save all button again to save order.";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "checkOrdersError", "$(function(){alert('" + message + "');});", true);
                            }
                        }
                    }
                }
                checkForOrder = true;
                tx.CommitTransaction();
                lblMessageTop.Visible = true;
                orderCheck = true;
                change = false;
                upnlSuppliers.Update();

            }
            else
            {
                string message = "Plese change order quantity 0 to any number to successfully save your order!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "checkOrders", "$(function(){alert('" + message + "');});", true);

            }
        }
        catch (Exception ex)
        {
            tx.RollbackTransaction();
            TransactionMgr.ThreadTransactionMgrReset();
            ViewState["OrderDetailID"] = null;
            ViewState["OrderID"] = null;

            string message = "Error on saving order. Please click save all button again to save order.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "checkOrdersError", "$(function(){alert('" + message + "');});", true);
        }
        upnlSuppliers.Update();

    }

    private void CreatedOrderSession()
    {
        if (Session["orderid"] == null)
        {
            Session["orderid"] = Guid.NewGuid().ToString();
        }
    }

    private void FinalOrder()
    {
        try
        {
            SupplierIDs.Clear();
            Pids.Clear();
            for (int i = 0; i < grdBase.Rows.Count; i++)
            {
                GridView grdS = grdBase.Rows[i].FindControl("grdSub") as GridView;
                for (int j = 0; j < grdS.Rows.Count; j++)
                {
                    GridView grdO = grdS.Rows[j].FindControl("grdOrders") as GridView;
                    for (int k = 0; k < grdO.Rows.Count; k++)
                    {
                        DropDownList ddlSuppliers = grdO.Rows[k].FindControl("ddlSuppliers") as DropDownList;
                        DropDownList ddlPacking = grdO.Rows[k].FindControl("ddlPacking") as DropDownList;
                        TextBox txtQty = grdO.Rows[k].FindControl("txtQty") as TextBox;
                        //Label lblPrice = grdO.Rows[k].FindControl("lblPrice") as Label;
                        TextBox lblPrice = grdO.Rows[k].FindControl("lblPrice") as TextBox;
                        HiddenField hidProductID = grdO.Rows[k].FindControl("hidProductID") as HiddenField;
                        if (txtQty.Text != "" && txtQty.Text != "0" && txtQty.Text != "00,00" && (lblPrice.Text != "" || lblPrice.Text != "0"))
                        {
                            Get_SupplierIDs_For_Order(ref ddlSuppliers, ref ddlPacking, ref hidProductID);
                            Pids.Add(Convert.ToInt32(hidProductID.Value));
                            dvpForQuantity.Add(hidProductID.Value, txtQty.Text);

                            dvpForPackingID.Add(hidProductID.Value, ddlPacking.SelectedValue);
                            dvpForPrice.Add(hidProductID.Value, lblPrice.Text);
                        }
                    }
                }
            }
            if (SupplierIDs.Count > 0)
            {

                string queryExpression = "";
                for (int l = 0; l < SupplierIDs.Count; l++)
                {
                    queryExpression += "pkSupplierID = " + SupplierIDs[l].ToString() + " or ";
                }
                queryExpression = queryExpression.Substring(0, queryExpression.Length - 4);

                /*
               string queryExpression = "";
               for (int l = 0; l < Pids.Count; l++)
               {
                   queryExpression += "pkProductid = " + Pids[l].ToString() + " or ";
               }
               queryExpression = queryExpression.Substring(0, queryExpression.Length - 4);

               string[] ids = SupplierIDs.ToArray();
               lblOrderStatus_For_Final_Orders.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

               if (dv == null)
                   setDataView();

               DataRow[] dr = dv.ToTable("SupplierOrder", true, "pkSupplierID", "sBrandName","pkproductid").Select(queryExpression);
               DataTable dtTemp = new DataTable();
               dtTemp.Columns.Add("pkSupplierID");
               dtTemp.Columns.Add("sBrandName");

               for (int i = 0; i < dr.Count(); i++)
               {
                   DataRow drTemp = dtTemp.NewRow();
                   drTemp[0] = dr[i][0].ToString();
                   drTemp[1] = dr[i][1].ToString();
                   dtTemp.Rows.Add(drTemp);
               }
               DataView dv2 = new DataView();
               dtTemp.Namespace = "newtable";
               dv2.Table = dtTemp;

               grdSuppliers.DataSource = dv2.ToTable("SupplierOrder2", true, "pkSupplierID", "sBrandName");
               grdSuppliers.DataBind();

               */


                DataRow[] dr = dv.ToTable("SupplierOrder", true, "pkSupplierID", "sBrandName").Select(queryExpression);
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("pkSupplierID");
                dtTemp.Columns.Add("sBrandName");

                for (int i = 0; i < dr.Count(); i++)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp[0] = dr[i][0].ToString();
                    drTemp[1] = dr[i][1].ToString();
                    dtTemp.Rows.Add(drTemp);
                }

                grdSuppliers.DataSource = dtTemp;
                grdSuppliers.DataBind();
                lblOrderStatus_For_Final_Orders.Text = "Order of " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;


                est_subtotal = 0.0;
                est_grand_total = 0.0;

                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        Label lblSubtotals = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                        est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotals.Text), 2);
                    }
                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                    lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                    est_grand_total += est_subtotal;
                    est_subtotal = 0.0;
                }
                lblGrandFinalTotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                mvMain.SetActiveView(vFinalOrders);
            }
            else
            {
                string message = "Plese change order quantity 0 to any number to successfully save your order!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "checkOrders", "$(function(){alert('" + message + "');});", true);
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "time", "$(function(){TimeRange();});", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "data", " $(function(){WaterMark();});", true);
        }

        catch (Exception ex)
        { }

        #region Comment Final Order
        /*
        try
        {
            setDataView();

            if (dv != null)
            {

                lblOrderStatus_For_Final_Orders.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

                DataRow[] dr = dv.ToTable("SupplierOrder", true, "pkSupplierID", "sBrandName").Select();
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("pkSupplierID");
                dtTemp.Columns.Add("sBrandName");

                for (int i = 0; i < dr.Count(); i++)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp[0] = dr[i][0].ToString();
                    drTemp[1] = dr[i][1].ToString();
                    dtTemp.Rows.Add(drTemp);
                }

                grdSuppliers.DataSource = dtTemp;
                grdSuppliers.DataBind();


                est_subtotal = 0.0;
                est_grand_total = 0.0;

                //for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                //{
                //    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                //    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                //    {
                //        Label lblSubtotals = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                //        est_subtotal += Math.Round(Convert.ToDouble(lblSubtotals.Text), 2);
                //    }
                //    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                //    lblSupplierTotal.Text = est_subtotal.ToString("N");
                //    est_grand_total += est_subtotal;
                //    est_subtotal = 0.0;
                //}
                //lblGrandFinalTotal.Text = est_grand_total.ToString("N");
                imgBtnNewOrder.Visible = false;
                mvMain.SetActiveView(vFinalOrders);
            }
            else
            {
                mvMain.SetActiveView(vOrderHistory);
            }
        }
        catch (Exception)
        { }
         */
        #endregion
    }
    #endregion

    #region Filter Orders
    protected void imgBtnClrFilters_Click(object sender, ImageClickEventArgs e)
    {
        LoadDropDowns();
        GetAllOrders();
    }
    protected void imgBtnFilters_Click(object sender, ImageClickEventArgs e)
    {
        string start = string.Empty;
        int supplieridFilter = 0;
        if (txtFrom.Text != "")
        {
            string[] start_D = txtFrom.Text.Split('/'); // start Date
            start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
            start = Convert.ToDateTime(start).AddDays(-1).ToShortDateString();
        }
        string end = string.Empty;
        if (txtTill.Text != "")
        {
            string[] end_D = txtTill.Text.Split('/'); // end date
            end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];
            end = Convert.ToDateTime(end).AddDays(1).ToShortDateString();
        }

        try
        {
            string query = string.Empty;
            if (ddlYear.SelectedValue != "0")
            {
                query += "year(b.dCreatedDate) = '" + ddlYear.SelectedItem.Text + "' and";
            }
            if (ddlSuppliers.SelectedValue != "0")
            {
                query += " s.sBrandName = '" + ddlSuppliers.SelectedItem.Text + "' and";
                supplieridFilter = Convert.ToInt32(ddlSuppliers.SelectedValue);
            }
            if (txtFrom.Text != "")
            {
                query += " b.dCreatedDate >= '" + start + "' and";
                if (txtTill.Text != "")
                {
                    query += " b.dCreatedDate <= '" + end + "' and";
                }
            }

            if (rdWithInvoice.Checked)
            {
                if (txtInvoiceNumber.Text != "")
                    query += " i.InvoiceNumber = '" + txtInvoiceNumber.Text + "' and";
            }
            //if (txtNote.Text != "")
            //{
            //    query += " o.OrderNote = '" + txtNote.Text + "' and";
            //}
            if (query != "")
                query = "where " + query.Substring(0, query.Trim().Length - 3);

            if (query == "")
            {
                grdOrdersHistory.DataSource = null;
                grdOrdersHistory.DataBind();
            }
            else
            {
                tblOrders o = new tblOrders();
                o.GetOrderHistoryFilter(query, supplieridFilter);
                grdOrdersHistory.DataSource = o.DefaultView;
                grdOrdersHistory.DataBind();
            }
        }
        catch (Exception ex)
        { }
    }

    #endregion

    /*
    private string GenerateHTMLForOrder(string supplierName, string orderDate, string[] productNames, string[] size, string[] qty, string note, string deliveryTime)
    {
        string html = string.Empty;
        //HtmlTable table = new HtmlTable();
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();

        cell.InnerHtml = supplierName + " " + orderDate;
        row.Cells.Add(cell);
        tblSampleText.Rows.Add(row);
        for (int i = 0; i < productNames.Length; i++)
        {
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            td.InnerHtml = productNames[i].ToString();
            HtmlTableCell td2 = new HtmlTableCell();
            td2.InnerHtml = "---------";
            HtmlTableCell td3 = new HtmlTableCell();
            td3.InnerHtml = qty[i] + " x " + size[i];
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            tr.Cells.Add(td3);
            tblSampleText.Rows.Add(tr);
        }

        //divEmail.Controls.Add(table);
        //return divEmail.InnerHtml;
        return "";

    }
     */

    protected void lnkPDF_Click(object sender, EventArgs e)
    {
        try
        {

            LinkButton lnkOrder = ((LinkButton)sender).Parent.FindControl("lnkOrder") as LinkButton;
            HiddenField hidFavorite = ((LinkButton)sender).Parent.FindControl("hidFavorite") as HiddenField;
            DropDownList ddlFavoriteMethod = ((LinkButton)sender).Parent.FindControl("ddlFavoriteMethod") as DropDownList;
            Label lblOrderMessage = ((LinkButton)sender).Parent.FindControl("lblOrderMessage") as Label;
            CheckBox chkReady = ((LinkButton)sender).Parent.FindControl("chkReady") as CheckBox;
            GridView grdOrderEmail = ((LinkButton)sender).Parent.FindControl("grdOrders_FinalOrder") as GridView;
            TextBox txtComment = ((LinkButton)sender).Parent.FindControl("txtComment") as TextBox;
            TextBox txtDeliveryTime = ((LinkButton)sender).Parent.FindControl("txtDeliveryTime") as TextBox;

            string pName = string.Empty;
            string size = string.Empty;

            string qty = string.Empty;
            string supplierN = string.Empty;
            bool order = false;
            string supplierAddress = string.Empty;
            for (int i = 0; i < grdOrderEmail.Rows.Count; i++)
            {
                Label lblSubtotals = grdOrderEmail.Rows[i].FindControl("lblSubtotals") as Label;
                LinkButton lnkProductName = grdOrderEmail.Rows[i].FindControl("lnkProductName") as LinkButton;
                DropDownList ddlPacking = grdOrderEmail.Rows[i].FindControl("ddlPacking") as DropDownList;

                DropDownList ddlSuppliers = grdOrderEmail.Rows[i].FindControl("ddlSuppliers") as DropDownList;

                TextBox txtQty = grdOrderEmail.Rows[i].FindControl("txtQty") as TextBox;
                if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                {
                    order = true;
                    pName += lnkProductName.Text + ",";
                    size += ddlPacking.SelectedItem.Text + ":";
                    qty += txtQty.Text + ",";
                    if (supplierN == "")
                    {
                        supplierN = ddlSuppliers.SelectedItem.Text;
                        tblSupplierEmails email = new tblSupplierEmails();
                        email.GetActiveEmail(Convert.ToInt32(ddlSuppliers.SelectedValue));
                        if (email.RowCount > 0)
                        {
                            supplierAddress = email.s_SEmail;
                        }
                    }
                }

            }

            if (order)
            {
                string orderStatus = lblOrderStatus_For_Final_Orders.Text;
                pdfhtml.SetValues(pName, size, qty, supplierN, txtComment.Text, txtDeliveryTime.Text, orderStatus);
                //GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
                GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Print", "$(function(){PdfSingle();});", true);
            }

            upnlSuppliers.Update();
        }
        catch (Exception ex)
        { }

    }
    private void GenerateHTMLForOrder(string supplierName, string orderDate, string[] productNames, string[] size, string[] qty, string note, string deliveryTime)
    {

        try
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            cell.InnerHtml = supplierName + " " + orderDate;
            row.Cells.Add(cell);
            tblSampleText.Rows.Add(row);
            for (int i = 0; i < productNames.Length; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlTableCell td = new HtmlTableCell();
                td.InnerHtml = productNames[i].ToString();
                HtmlTableCell td2 = new HtmlTableCell();
                td2.InnerHtml = "---------";
                HtmlTableCell td3 = new HtmlTableCell();
                td3.InnerHtml = qty[i] + " x " + size[i];
                tr.Cells.Add(td);
                tr.Cells.Add(td2);
                tr.Cells.Add(td3);
                tblSampleText.Rows.Add(tr);
            }




        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOrder = ((LinkButton)sender).Parent.FindControl("lnkOrder") as LinkButton;
            HiddenField hidFavorite = ((LinkButton)sender).Parent.FindControl("hidFavorite") as HiddenField;
            DropDownList ddlFavoriteMethod = ((LinkButton)sender).Parent.FindControl("ddlFavoriteMethod") as DropDownList;
            Label lblOrderMessage = ((LinkButton)sender).Parent.FindControl("lblOrderMessage") as Label;
            CheckBox chkReady = ((LinkButton)sender).Parent.FindControl("chkReady") as CheckBox;
            GridView grdOrderEmail = ((LinkButton)sender).Parent.FindControl("grdOrders_FinalOrder") as GridView;
            TextBox txtComment = ((LinkButton)sender).Parent.FindControl("txtComment") as TextBox;
            TextBox txtDeliveryTime = ((LinkButton)sender).Parent.FindControl("txtDeliveryTime") as TextBox;

            string pName = string.Empty;
            string size = string.Empty;

            string qty = string.Empty;
            string supplierN = string.Empty;
            bool order = false;
            string supplierAddress = string.Empty;
            for (int i = 0; i < grdOrderEmail.Rows.Count; i++)
            {
                Label lblSubtotals = grdOrderEmail.Rows[i].FindControl("lblSubtotals") as Label;
                LinkButton lnkProductName = grdOrderEmail.Rows[i].FindControl("lnkProductName") as LinkButton;
                DropDownList ddlPacking = grdOrderEmail.Rows[i].FindControl("ddlPacking") as DropDownList;

                DropDownList ddlSuppliers = grdOrderEmail.Rows[i].FindControl("ddlSuppliers") as DropDownList;

                TextBox txtQty = grdOrderEmail.Rows[i].FindControl("txtQty") as TextBox;
                if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                {
                    order = true;
                    pName += lnkProductName.Text + ",";
                    size += ddlPacking.SelectedItem.Text + ":";
                    qty += txtQty.Text + ",";
                    if (supplierN == "")
                    {
                        supplierN = ddlSuppliers.SelectedItem.Text;
                        tblSupplierEmails email = new tblSupplierEmails();
                        email.GetActiveEmail(Convert.ToInt32(ddlSuppliers.SelectedValue));
                        if (email.RowCount > 0)
                        {
                            supplierAddress = email.s_SEmail;
                        }
                    }
                }
            }

            if (order)
            {
                string orderStatus = lblOrderStatus_For_Final_Orders.Text;
                pdfhtml.SetValues(pName, size, qty, supplierN, txtComment.Text, txtDeliveryTime.Text, orderStatus);
                //GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
                GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Print", "$(function(){PrintSingle();});", true);
            }

            upnlSuppliers.Update();
        }
        catch (Exception ex)
        { }

    }
    protected void lnkPrintAll_Click(object sender, EventArgs e)
    {
        string seperator_With_SupplierName = string.Empty;
        string pName = string.Empty;
        string size = string.Empty;
        string comment = string.Empty;
        string deliveryTime = string.Empty;
        string qty = string.Empty;
        string supplierN = string.Empty;
        bool order = false;
        string supplierAddress = string.Empty;
        for (int i = 0; i < grdSuppliers.Rows.Count; i++)
        {
            HiddenField hidSupplierID = grdSuppliers.Rows[i].FindControl("hidSupplierID") as HiddenField;
            TextBox txtComment = grdSuppliers.Rows[i].FindControl("txtComment") as TextBox;
            TextBox txtDeliveryTime = grdSuppliers.Rows[i].FindControl("txtDeliveryTime") as TextBox;
            TextBox txtDeliveryTimeEnd = grdSuppliers.Rows[i].FindControl("txtDeliveryTimeEnd") as TextBox;
            CheckBox chkReady = grdSuppliers.Rows[i].FindControl("chkReady") as CheckBox;
            Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
            Label lblSupplierName = grdSuppliers.Rows[i].FindControl("lblSupplierName") as Label;

            GridView grdOrderEmail = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;

            for (int j = 0; j < grdOrderEmail.Rows.Count; j++)
            {
                Label lblSubtotals = grdOrderEmail.Rows[j].FindControl("lblSubtotals") as Label;
                LinkButton lnkProductName = grdOrderEmail.Rows[j].FindControl("lnkProductName") as LinkButton;
                DropDownList ddlPacking = grdOrderEmail.Rows[j].FindControl("ddlPacking") as DropDownList;
                DropDownList ddlSuppliers = grdOrderEmail.Rows[j].FindControl("ddlSuppliers") as DropDownList;
                TextBox txtQty = grdOrderEmail.Rows[j].FindControl("txtQty") as TextBox;
                if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                {
                    order = true;
                    pName += lnkProductName.Text + ",";
                    size += ddlPacking.SelectedItem.Text + ":";
                    qty += txtQty.Text + ",";
                    seperator_With_SupplierName += lblSupplierName.Text + ",";
                }
            }
            supplierN += lblSupplierName.Text + ",";
            comment += txtComment.Text + ",";
            deliveryTime += txtDeliveryTime.Text + ",";
            pName += "";
        }
        if (order)
        {
            string orderStatus = lblOrderStatus_For_Final_Orders.Text;
            pdfhtml.SetValuesAll(pName, size, qty, supplierN, comment, deliveryTime, orderStatus, seperator_With_SupplierName);
            //GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
            //GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), comment, txtDeliveryTime.Text);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Print", "$(function(){PrintAll();});", true);
        }

    }
    protected void lnkPDFAll_Click(object sender, EventArgs e)
    {
        string seperator_With_SupplierName = string.Empty;
        string pName = string.Empty;
        string size = string.Empty;
        string comment = string.Empty;
        string deliveryTime = string.Empty;
        string qty = string.Empty;
        string supplierN = string.Empty;
        bool order = false;
        string supplierAddress = string.Empty;
        for (int i = 0; i < grdSuppliers.Rows.Count; i++)
        {
            HiddenField hidSupplierID = grdSuppliers.Rows[i].FindControl("hidSupplierID") as HiddenField;
            TextBox txtComment = grdSuppliers.Rows[i].FindControl("txtComment") as TextBox;
            TextBox txtDeliveryTime = grdSuppliers.Rows[i].FindControl("txtDeliveryTime") as TextBox;
            TextBox txtDeliveryTimeEnd = grdSuppliers.Rows[i].FindControl("txtDeliveryTimeEnd") as TextBox;
            CheckBox chkReady = grdSuppliers.Rows[i].FindControl("chkReady") as CheckBox;
            Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
            Label lblSupplierName = grdSuppliers.Rows[i].FindControl("lblSupplierName") as Label;

            GridView grdOrderEmail = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;

            for (int j = 0; j < grdOrderEmail.Rows.Count; j++)
            {
                Label lblSubtotals = grdOrderEmail.Rows[j].FindControl("lblSubtotals") as Label;
                LinkButton lnkProductName = grdOrderEmail.Rows[j].FindControl("lnkProductName") as LinkButton;
                DropDownList ddlPacking = grdOrderEmail.Rows[j].FindControl("ddlPacking") as DropDownList;
                DropDownList ddlSuppliers = grdOrderEmail.Rows[j].FindControl("ddlSuppliers") as DropDownList;
                TextBox txtQty = grdOrderEmail.Rows[j].FindControl("txtQty") as TextBox;
                if (txtQty.Text != "" && txtQty.Text != "00,00" && txtQty.Text != "0")
                {
                    order = true;
                    pName += lnkProductName.Text + ",";
                    size += ddlPacking.SelectedItem.Text + ":";
                    qty += txtQty.Text + ",";
                    seperator_With_SupplierName += lblSupplierName.Text + ",";
                }
            }
            supplierN += lblSupplierName.Text + ",";
            comment += txtComment.Text + ",";
            deliveryTime += txtDeliveryTime.Text + ",";
            pName += "";
        }
        if (order)
        {
            string orderStatus = lblOrderStatus_For_Final_Orders.Text;
            pdfhtml.SetValuesAll(pName, size, qty, supplierN, comment, deliveryTime, orderStatus, seperator_With_SupplierName);
            //GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), txtComment.Text, txtDeliveryTime.Text);
            //GenerateHTMLForOrder(supplierN, orderStatus, pName.Substring(0, pName.Length - 1).Split(','), size.Substring(0, size.Length - 1).Split(','), qty.Substring(0, qty.Length - 1).Split(','), comment, txtDeliveryTime.Text);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Print", "$(function(){PdfAll();});", true);
        }
    }
    protected void txtQty__TextChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlSuppliers = ((TextBox)sender).Parent.FindControl("ddlSuppliers") as DropDownList;
            TextBox txtQty = ((TextBox)sender).Parent.FindControl("txtQty") as TextBox;
            HiddenField hidProductID = ((TextBox)sender).Parent.FindControl("hidProductID") as HiddenField;
            //Label lblPrice = ((TextBox)sender).Parent.FindControl("lblPrice") as Label;
            TextBox lblPrice = ((TextBox)sender).Parent.FindControl("lblPrice") as TextBox;
            DropDownList ddlPacking = ((TextBox)sender).Parent.FindControl("ddlPacking") as DropDownList;
            DropDownList ddlQuantity = ((TextBox)sender).Parent.FindControl("ddlQuantity") as DropDownList;

            Label lblVat = ((TextBox)sender).Parent.FindControl("lblVat") as Label;
            Label lblAfterVat = ((TextBox)sender).Parent.FindControl("lblAfterVat") as Label;
            Label lblSubtotals = ((TextBox)sender).Parent.FindControl("lblSubtotals") as Label;
            double afterVat = 0.0;
            double subtotal = 0.0;
            if (dv == null)
                setDataView();
            // DataRow[] dr = dv.ToTable("Suppliers", true, "pkSupplierID", "sBrandName", "UnitPrice", "Qty", "pkproductid").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "'", "UnitPrice");

            //lblPrice.Text = dr[0][2].ToString();
            if (lblPrice.Text == "")
            {
                DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price").Select("pkproductid = '" + hidProductID.Value + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "' ");
                if (dr2[0][2].ToString() != "")
                    lblPrice.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
            }


            if (lblPrice.Text != "")
            {
                if (lblAfterVat != null)
                {
                    double prc = commonMethods.ChangeToUS(lblPrice.Text);
                    afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                    lblAfterVat.Text = afterVat.ToString("N") + "€";
                }
                if (lblSubtotals != null)
                {
                    subtotal = Convert.ToDouble(lblAfterVat.Text.Replace("€", "")) * Convert.ToDouble(txtQty.Text);
                    lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                }

                //if (txtQty != null)
                //  txtQty.Attributes.Add("onchange", "javascript:addSubtotals();");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sub", "$(function(){  addSubtotals();});", true);
                //GridView grdSuppliers = ((TextBox)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridView;
                string id = ((TextBox)sender).Parent.Parent.Parent.Parent.ID;

                if (id == "grdOrders_FinalOrder")
                {
                    est_subtotal = 0.0;
                    for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                    {
                        GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                        for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                        {
                            Label lblSubtotal = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                            est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotal.Text), 2);
                        }
                        Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                        lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                        est_grand_total += est_subtotal;
                        est_subtotal = 0.0;
                    }
                    lblGrandFinalTotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                    est_grand_total = 0.0;
                    upnlSuppliers.Update();
                    upnlGrand.Update();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "time", "$(function(){TimeRange();});", true);
                }
                else if (id == "grdOrders")
                {
                    est_subtotal = 0.0;
                    for (int i = 0; i < grdBase.Rows.Count; i++)
                    {
                        GridView grdSub = grdBase.Rows[i].FindControl("grdSub") as GridView;
                        for (int j = 0; j < grdSub.Rows.Count; j++)
                        {
                            GridView grdOrders = grdSub.Rows[j].FindControl("grdOrders") as GridView;
                            for (int k = 0; k < grdOrders.Rows.Count; k++)
                            {
                                Label lblSubtotal = grdOrders.Rows[k].FindControl("lblSubtotals") as Label;
                                est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotal.Text), 2);
                            }
                        }
                        Label lblEstimatedSubtotal = grdBase.Rows[i].FindControl("lblEstimatedSubtotal") as Label;
                        lblEstimatedSubtotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                        est_grand_total += est_subtotal;
                        est_subtotal = 0.0;
                    }
                    lblGrandSubtotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                    est_grand_total = 0.0;
                    upnlSuppliers.Update();
                    upnlGrand.Update();
                    upnlGrandSubtotal.Update();
                }
                change = true;
            }
            else
            {
                txtQty.Text = "0";
            }
            //for (int i = 0; i < grdSuppliers.Rows.Count; i++)
            //{
            //    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;

            //}
            //Label lblSupplierTotal = this.Master.FindControl("ContentPlaceHolder1").FindControl("grdSuppliers").FindControl("lblSupplierTotal") as Label;


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "data", " $(function(){WaterMark();});", true);
        }
        catch (Exception ex)
        { }
        upnlBase.Update();
    }

    #region Update Order
    protected void grdOrdersHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            string orderid = e.CommandArgument.ToString();
            switch (e.CommandName.ToLower())
            {
                case "order":
                    ViewState["OrderUpdate"] = orderid;
                    Session["orderid"] = orderid;
                    NewOrder();
                    break;
            }
        }
    }



    #endregion

    #region TimePicker

    public void setTime()
    {
        try
        {
            DataTable temptblOffDays = new DataTable();
            temptblOffDays.Columns.Add("dTime");

            int tempMinuts = 0;
            int minutstoadd = 15;
            for (int i = 28; i < 85; i++)
            {
                tempMinuts = minutstoadd * i;
                DateTime dStartTime = new DateTime();
                string dTime = Convert.ToDateTime(dStartTime).ToString("HH:mm");
                string tempDate = Convert.ToDateTime(dTime).AddMinutes(tempMinuts).ToString("HH:mm");
                DataRow tDR = temptblOffDays.NewRow();
                tDR[0] = tempDate;
                temptblOffDays.Rows.Add(tDR);
            }
            dlDeliveryTime.DataSource = temptblOffDays;
            dlDeliveryTime.DataBind();

            upnlSuppliers.Update();

        }
        catch (Exception ex)
        {

        }

    }

    protected void dlDeliveryTime_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblTime = e.Item.FindControl("lblTime") as Label;
            lblTime.Text = DataBinder.GetPropertyValue(e.Item.DataItem, "dTime").ToString();
            lblTime.Attributes.Add("onclick", "javascript:setTime('" + textboxid + "','" + lblTime.Text + "');");

        }
    }
    public DateTime FirstDayOfMonth(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public DateTime LastDayOfMonth(DateTime dateTime)
    {
        DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
        return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
    }
    #endregion

}
