using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;
using System.Data;
using System.Drawing;
using MyGeneration.dOOdads;

public partial class AccountManager_OrderReceived : System.Web.UI.Page
{
    static int call = 0;
    int UserID;
    static string textboxid = string.Empty;
    int DepartmentID;
    static int basCat = 0;
    static int subCat = 0;
    static int pid = 0;
    static DataView dv = null;
    static double est_subtotal = 0.0;
    static double fin_subtotal = 0.0;
    static double fin_difference = 0.0;
    static double amount_paid = 0.0;
    static string orderDate = "";

    static double est_grand_total = 0.0;
    static double est_grand_final = 0.0;
    static double est_grand_difference = 0.0;

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
        try
        {
            if (Request.QueryString.Count > 0)
            {
                string orderid = Request.QueryString["orderid"].ToString();
                int uid = Convert.ToInt32(Request.QueryString["userid"].ToString());
                tblBaseOrder b = new tblBaseOrder();
                b.getBaseOrder(orderid);
                if (b.RowCount > 0)
                {
                    if (b.OrderReceivedByUser == uid)
                    {

                        ViewState["OrderUpdate"] = orderid;
                        Session["orderid"] = orderid;
                        tblBaseOrder border = new tblBaseOrder();
                        border.getBaseOrder(orderid);
                        txtCash.Text = commonMethods.ChangetToUK(border.CashAmount.ToString("N"));
                        SessionUser user = new SessionUser();
                        user = (SessionUser)Session["UserLogin"];

                        UserID = user.UserID;
                        DepartmentID = user.DepartmentID;
                        if (!IsPostBack)
                        {
                            call = 0;
                        }

                        if (call == 0)
                        {
                            call = 1;
                            NewOrder();
                        }


                    }
                    else
                    {
                        Session["UserLogin"] = null;
                        Response.Redirect("../West_login.aspx");
                    }
                }
                else
                {
                    Session["UserLogin"] = null;
                    Response.Redirect("../West_login.aspx");
                }

            }
            else if (Session["UserLogin"] != null)
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
                //LoadDropDowns();
            }
          SetTotalAmount();
        }
        catch (Exception ex)
        { }
    }
    private void SetTotalAmount()
    {
        double dFinalAmount = commonMethods.ChangeToUS(txtGrandEstimated.Text) - commonMethods.ChangeToUS(txtGrandFinal.Text);
        string sign = string.Empty;
        if (est_grand_difference > 0)
        {
            sign = "+";
            divGrandDifference.Style.Add("background", "url(../images/textbox_red.png) no-repeat;");
            divGrandDifference.Style.Remove("class");
            divGrandDifference.Style.Add("class", "textbox115_white");
            txtGrandDifference.Style.Add("color", "white");
            lblSpanGrandDifference.Style.Add("color", "white");
        }
        else if (est_grand_difference < 0)
        {
            sign = "-";
            divGrandDifference.Style.Add("background", "url(../images/textbox_light_green.png) no-repeat;");
            divGrandDifference.Style.Remove("class");
            divGrandDifference.Style.Add("class", "textbox115_white");
            txtGrandDifference.Style.Add("color", "white");
            lblSpanGrandDifference.Style.Add("color", "white");
        }
        else
        {
            sign = "-";
            divGrandDifference.Style.Add("background", "url(../images/textbox_115.png) no-repeat;");
            divGrandDifference.Style.Remove("class");
            divGrandDifference.Style.Add("class", "textbox115");
            txtGrandDifference.Style.Add("color", "Gray");
            lblSpanGrandDifference.Style.Add("color", "Gray");
        }
        txtGrandDifference.Text = sign + " " + commonMethods.ChangetToUK(est_grand_difference.ToString("N"));
    }
    //private void LoadDropDowns()
    //{
    //    ddlYear.Items.Clear();
    //    for (int i = 0; i <= 74; i++)
    //    {
    //        ddlYear.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
    //    }
    //    ddlYear.Items.Insert(0, new ListItem("Select Year", "0"));

    //    ddlSuppliers.Items.Clear();
    //    tblSupplier suppliers = new tblSupplier();
    //    suppliers.LoadAll();
    //    commonMethods.FillDropDownList(ddlSuppliers, suppliers.DefaultView, "SBrandName", "PkSupplierID");
    //    ddlSuppliers.Items.Insert(0, new ListItem("Select Supplier", "0"));

    //    rdWithInvoice.Checked = true;
    //    txtInvoiceNumber.Text = "";
    //    txtFrom.Text = "";
    //    txtTill.Text = "";
    //    txtNote.Text = "";

    //}
    private void GetAllOrders()
    {
        try
        {
            tblBaseOrder border = new tblBaseOrder();
            border.LoadAll();
            if (border.RowCount > 0)
            {
                for (int i = 0; i < border.RowCount; i++)
                {
                    double grand = 0.0;
                    tblOrders order = new tblOrders();

                    order.getOrdersByBaseOrder(border.PkBaseOrderID);
                    if (order.RowCount > 0)
                    {

                        for (int j = 0; j < order.RowCount; j++)
                        {
                            try
                            {
                                if (order.FinalSubtotal != 0 && order.FinalSubtotal != null && order.FinalSubtotal != 0.0)
                                {
                                    grand += order.FinalSubtotal;
                                }
                                else
                                {
                                    grand += order.OrderSubtotal;
                                }
                            }
                            catch (InvalidCastException ex)
                            {
                                grand += order.OrderSubtotal;
                            }
                            order.MoveNext();
                        }
                    }

                    border.GrandSubtotal = grand;
                    border.Save();
                    border.MoveNext();
                }
            }



            tblOrders o = new tblOrders();
            o.GetAllOrdersOnly();
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
                //Label lblOrderStatus = e.Row.FindControl("lblOrderStatus") as Label;
                Label lblSubtotal = e.Row.FindControl("lblSubtotal") as Label;
                HiddenField hidBaseid = e.Row.FindControl("hidBaseid") as HiddenField;


                TextBox txtCash = e.Row.FindControl("txtCash") as TextBox;
                HiddenField hidReceiverID = e.Row.FindControl("hidReceiverID") as HiddenField;
                DropDownList ddlUsers = e.Row.FindControl("ddlUsers") as DropDownList;
                if (txtCash.Text == "0")
                    txtCash.Text = "";

                tblUsers u = new tblUsers();
                u.LoadStaff(DepartmentID, UserID);
                if (u.RowCount > 0)
                {
                    ddlUsers.Items.Clear();
                    commonMethods.FillDropDownList(ddlUsers, u.DefaultView, "FullName", "PkUserID");
                    ddlUsers.Items.Insert(0, new ListItem("Select User", "0"));
                }
                ddlUsers.SelectedValue = hidReceiverID.Value;
                LinkButton lnkOrderIDUpdate = e.Row.FindControl("lnkOrderIDUpdate") as LinkButton;


                tblBaseOrder b = new tblBaseOrder();
                b.LoadByPrimaryKey(Convert.ToInt32(hidBaseid.Value));
                if (b.RowCount > 0)
                {
                    try
                    {
                        if (!b.AllowEdit)
                            lnkOrderIDUpdate.Enabled = false;
                        else
                            lnkOrderIDUpdate.Enabled = true;
                    }
                    catch (InvalidCastException ex)
                    {
                        lnkOrderIDUpdate.Enabled = true;
                    }
                }
                //Label lblPaid = e.Row.FindControl("lblPaid") as Label;
                //Label lblDue = e.Row.FindControl("lblDue") as Label;

                //if (lblPaid.Text == "" || lblPaid.Text == "0")
                //    lblPaid.Text = "-";
                //else
                //    lblPaid.Text = commonMethods.ChangetToUK(lblPaid.Text) + " €";
                //if (lblDue.Text == "" || lblDue.Text == "0")
                //    lblDue.Text = "-";
                //else
                //    lblDue.Text = commonMethods.ChangetToUK(lblDue.Text) + " €";


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

                //if (lblOrderStatus.Text == "1")
                //    lblOrderStatus.Text = "SENT";
                //else
                //{
                //    //lnkOrderIDUpdate.Enabled = false;
                //    lblOrderStatus.Text = "DELIVERED";
                //}
                DateTime dd = Convert.ToDateTime(lblOrderDate.Text);
                lblOrderDate.Text = dd.Day + "/" + dd.Month + "/" + dd.Year;


                lblSubtotal.Text = commonMethods.ChangetToUK(lblSubtotal.Text) + " €";
            }
        }
        catch (Exception)
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

                //lblOrderStatus_For_Final_Orders.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;


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
    }

    #region New Order

    protected void imgBtnNewOrder_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Session["orderid"] = null;
            ViewState["OrderUpdate"] = null;
            NewOrder();
        }
        catch (Exception ex)
        { }
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

            FinalOrder();
            mvMain.SetActiveView(vFinalOrders);
        }
        catch (Exception ex)
        {

        }

    }

    private void LoadBaseCategories()
    {
        try
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
        catch (Exception ex)
        { }
    }
    protected void ddlSubCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubCategories.SelectedValue != "0")
            {
                tblProducts prod = new tblProducts();
                prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
                grdProducts.DataSource = prod.DefaultView;
                grdProducts.DataBind();
            }
        }
        catch (Exception ex)
        { }
    }
    protected void ddlBaseCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
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
        catch (Exception ex)
        { }
    }

    #endregion

    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdProducts.PageIndex = e.NewPageIndex;
            tblProducts prod = new tblProducts();
            prod.GetProducts(Convert.ToInt32(ddlSubCategories.SelectedValue));
            grdProducts.DataSource = prod.DefaultView;
            grdProducts.DataBind();
        }
        catch (Exception ex)
        { }
    }
    protected void grdOrdersHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdOrdersHistory.PageIndex = e.NewPageIndex;
            GetAllOrders();
        }
        catch (Exception ex)
        { }
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
                Label Price = e.Row.FindControl("LabelPrice") as Label;

                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblAfterVat = e.Row.FindControl("lblAfterVat") as Label;
                Label lblSubtotals = e.Row.FindControl("lblSubtotals") as Label;
                double afterVat = 0.0;
                double subtotal = 0.0;


                if (ddlSuppliers != null)
                {

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
                        BindSupplier_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlSuppliers, ref Price, ref ddlPacking);
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
                        if (Price.Text == "")
                        {
                            DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price", "pkSupplierID").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "' ");
                            if (dr2[0][2].ToString() != "")
                                Price.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
                        }

                        if (Price.Text != "")
                        {
                            double prc = commonMethods.ChangeToUS(Price.Text);
                            afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                            lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";

                        }
                        //if (lblPrice.Text == "")
                        //    lblPrice.Text = "00.00";
                        //lblPrice.Text = commonMethods.ChangetToUK(lblPrice.Text);

                    }
                    if (lblSubtotals != null)
                    {
                        if (Price.Text != "")
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
                }
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
            //bool check = false;
            //for (int i = 0; i < SupplierIDs.Count(); i++)
            //{
            //    if (SupplierIDs[i].ToString() == ddlSuppliers.SelectedValue)
            //    {
            //        check = true;
            //        break;
            //    }
            //}
            //if (!check)
            //{
            //    SupplierIDs.Add(ddlSuppliers.SelectedValue);
            //}

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
    }
    private void BindSupplier_ForOrder(int productid, ref DropDownList ddlSuppliers, ref Label lblPrice, ref DropDownList ddlPacking)
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
            Label Price = ((DropDownList)sender).Parent.FindControl("LabelPrice") as Label;

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
                Price.Text = commonMethods.ChangetToUK(dr[0][2].ToString());
            else
                Price.Text = "";
            if (Price.Text != "")
            {
                if (lblAfterVat != null)
                {
                    double prc = commonMethods.ChangeToUS(Price.Text);
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
            Label Price = ((DropDownList)sender).Parent.FindControl("LabelPrice") as Label;
            Label lblPriceDifference = ((DropDownList)sender).Parent.FindControl("lblPriceDifference") as Label;
            HtmlImage img = ((DropDownList)sender).Parent.FindControl("img") as HtmlImage;

            Label lblVat = ((DropDownList)sender).Parent.FindControl("lblVat") as Label;
            Label lblAfterVat = ((DropDownList)sender).Parent.FindControl("lblAfterVat") as Label;
            Label lblSubtotals = ((DropDownList)sender).Parent.FindControl("lblSubtotals") as Label;
            Label lblFinalSubtotal = ((DropDownList)sender).Parent.FindControl("lblFinalSubtotal") as Label;
            Label lblFinalDifference = ((DropDownList)sender).Parent.FindControl("lblFinalDifference") as Label;

            HtmlContainerControl divDifference = ((DropDownList)sender).Parent.FindControl("divDifference") as HtmlContainerControl;

            TextBox txtQty = ((DropDownList)sender).Parent.FindControl("txtQty") as TextBox;
            double afterVat = 0.0;
            double subtotal = 0.0;
            if (dv == null)
                setDataView();

            lblPrice.Text = "00,00";
            lblPriceDifference.Text = "00,00";
            lblFinalDifference.Style.Add("color", "Gray");
            divDifference.Style.Add("background", "url(../images/textbox_small.png) no-repeat;");

            img.Src = "";
            DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price", "pkSupplierID").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "' and  pkProductPackageID = '" + ddlPacking.SelectedValue + "'");
            if (dr2[0][2].ToString() != "")
                Price.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
            if (Price.Text != "")
            {
                if (lblAfterVat != null)
                {
                    double prc = commonMethods.ChangeToUS(Price.Text);
                    afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                    lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N"));
                }
                if (lblSubtotals != null)
                {
                    subtotal = afterVat * commonMethods.ChangeToUS(txtQty.Text);
                    lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                }

                est_subtotal = 0.0;
                fin_subtotal = 0.0;
                fin_difference = 0.0;
                lblFinalSubtotal.Text = "00,00";
                lblFinalDifference.Text = "00,00";
                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        Label lblSubtotal = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                        Label lblFinalSubtotalAggregate = grdOrders_FinalOrder.Rows[j].FindControl("lblFinalSubtotal") as Label;
                        Label lblFinalDifferenceAggregate = grdOrders_FinalOrder.Rows[j].FindControl("lblFinalDifference") as Label;



                        est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotal.Text), 2);
                        fin_subtotal += Math.Round(commonMethods.ChangeToUS(lblFinalSubtotalAggregate.Text), 2);
                        fin_difference += Math.Round(commonMethods.ChangeToUS(lblFinalDifferenceAggregate.Text.Replace("-", "").Replace("+", "")), 2);
                    }

                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;

                    Label lblAggEstimated = grdSuppliers.Rows[i].FindControl("lblAggEstimated") as Label;
                    Label lblAggFinal = grdSuppliers.Rows[i].FindControl("lblAggFinal") as Label;
                    Label lblAggDifference = grdSuppliers.Rows[i].FindControl("lblAggDifference") as Label;

                    lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));

                    lblAggEstimated.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                    lblAggFinal.Text = commonMethods.ChangetToUK(fin_subtotal.ToString("N"));
                    lblAggDifference.Text = commonMethods.ChangetToUK(fin_difference.ToString("N"));


                    HiddenField hidSupplierID = grdSuppliers.Rows[i].FindControl("hidSupplierID") as HiddenField;
                    Label lblOrderTotal = grdSuppliers.Rows[i].FindControl("lblOrderTotal") as Label;
                    Label lblPreviousBalance = grdSuppliers.Rows[i].FindControl("lblPreviousBalance") as Label;
                    Label lblOrderBalance = grdSuppliers.Rows[i].FindControl("lblOrderBalance") as Label;
                    Label lblNewBalance = grdSuppliers.Rows[i].FindControl("lblNewBalance") as Label;
                    Label lblAmountPaid = grdSuppliers.Rows[i].FindControl("lblAmountPaid") as Label;
                    TextBox txtAmountPaid = grdSuppliers.Rows[i].FindControl("txtAmountPaid") as TextBox;


                    if (lblAggFinal.Text != "" && lblAggFinal.Text != "00,00")
                    {
                        lblOrderTotal.Text = lblAggFinal.Text;
                    }
                    else
                    {
                        lblOrderTotal.Text = lblAggEstimated.Text;
                    }

                    tblSupplier s = new tblSupplier();
                    s.GetOweAmount(Convert.ToInt32(hidSupplierID.Value), DepartmentID);
                    if (s.RowCount > 0)
                    {
                        if (s.GetColumn("amount").ToString().Trim() != "")
                            lblPreviousBalance.Text = commonMethods.ChangetToUK(Convert.ToDouble(s.GetColumn("amount").ToString()).ToString("N"));
                        else
                            lblPreviousBalance.Text = "00,00";
                    }
                    else
                        lblPreviousBalance.Text = "00,00";

                    if (lblPreviousBalance.Text == "")
                    {
                        lblPreviousBalance.Text = "00,00";
                    }
                    lblOrderBalance.Text = commonMethods.ChangetToUK((commonMethods.ChangeToUS(lblPreviousBalance.Text) + commonMethods.ChangeToUS(lblOrderTotal.Text)).ToString("N"));

                    txtAmountPaid.Text = commonMethods.ChangetToUK((commonMethods.ChangeToUS(lblOrderTotal.Text) + commonMethods.ChangeToUS(lblPreviousBalance.Text)).ToString("N"));

                    //lblNewBalance.Text = (commonMethods.ChangeToUS(lblOrderBalance.Text.Replace("-", "")) - commonMethods.ChangeToUS(txtAmountPaid.Text)).ToString("N");

                    est_grand_total += est_subtotal;
                    est_grand_final += fin_subtotal;
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
    }

    #region Final Orders

    protected void grdOrders_FinalOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
            else
            {

                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;

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

                Label Price = e.Row.FindControl("LabelPrice") as Label;
                Label lblPriceDifference = e.Row.FindControl("lblPriceDifference") as Label;
                Label lblFinalSubtotal = e.Row.FindControl("lblFinalSubtotal") as Label;
                Label lblFinalDifference = e.Row.FindControl("lblFinalDifference") as Label;

                HtmlContainerControl divDifference = e.Row.FindControl("divDifference") as HtmlContainerControl;

                Label lblVat = e.Row.FindControl("lblVat") as Label;
                Label lblAfterVat = e.Row.FindControl("lblAfterVat") as Label;
                Label lblSubtotals = e.Row.FindControl("lblSubtotals") as Label;

                if (hidPSupplierid != null)
                {
                    tblOrders order = new tblOrders();
                    order.GetOrder(Convert.ToInt32(hidPSupplierid.Value), ViewState["OrderUpdate"].ToString());

                    tblOrderDetail orderDetail = new tblOrderDetail();
                    orderDetail.GetOrderDetailForUpdate(ViewState["OrderUpdate"].ToString(), order.PkOrderID, Convert.ToInt32(hidProductID.Value));

                    if (orderDetail.RowCount > 0)
                    {
                        try
                        {
                            lblPrice.Text = commonMethods.ChangetToUK(orderDetail.NewPrice.ToString("N"));
                        }
                        catch (InvalidCastException ex)
                        {
                            lblPrice.Text = "00,00";
                        }
                        try
                        {
                            lblFinalSubtotal.Text = commonMethods.ChangetToUK(orderDetail.FinalSubtotal.ToString("N"));
                            lblFinalDifference.Text = commonMethods.ChangetToUK(orderDetail.SubtotalDifference.ToString("N"));
                        }
                        catch (InvalidCastException ex)
                        {

                        }
                    }
                }

                HtmlImage img = e.Row.FindControl("img") as HtmlImage;


                double difference = 0.0;
                double finalSubtal = 0.0;




                double afterVat = 0.0;
                double subtotal = 0.0;

                if (ddlPacking != null)
                {
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
                        BindSupplier_ForOrder(Convert.ToInt32(hidProductID.Value), ref ddlSuppliers, ref Price, ref ddlPacking);
                        ddlSuppliers.SelectedValue = hidPSupplierid.Value;
                        // Get_SupplierIDs_For_Order(ref ddlSuppliers);
                    }

                    #region Get Difference b/w new price- old price
                    if (lblPrice != null)
                    {
                        if (lblPrice.Text == "" || lblPrice.Text == "00,00")
                        {
                            lblPriceDifference.Text = "00,00";
                            img.Src = "";
                        }
                        else
                        {
                            double p_difference = 0.0;
                            p_difference = commonMethods.ChangeToUS(lblPrice.Text) - commonMethods.ChangeToUS(Price.Text);
                            string sign = string.Empty;
                            string imageRatio = string.Empty;
                            if (p_difference > 0)
                            {
                                sign = "+ ";
                                imageRatio = "../images/up_arrow.png";
                                lblPriceDifference.ForeColor = Color.Red;
                            }
                            else if (p_difference < 0)
                            {
                                sign = "- ";
                                imageRatio = "../images/down_arrow.png";
                                lblPriceDifference.ForeColor = Color.Green;
                            }
                            else
                            {
                                sign = "";
                                imageRatio = "";
                            }
                            lblPriceDifference.Text = sign + commonMethods.ChangetToUK(p_difference.ToString("N")).Replace("-", "");
                            img.Src = imageRatio;
                        }
                    }
                    #endregion

                    if (lblVat != null)
                        GetVAT_ForOrders(Convert.ToInt32(hidProductID.Value), ref lblVat);

                    if (lblAfterVat != null)
                    {
                        if (Price.Text == "")
                        {
                            DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price").Select("pkproductid = '" + hidProductID.Value + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "'");
                            if (dr2[0][2].ToString() != "")
                                Price.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
                        }
                        if (Price.Text != "")
                        {
                            double prc = commonMethods.ChangeToUS(Price.Text);
                            afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                            lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                        }
                        //foreach (var item in dvpForPrice)
                        //{
                        //    if (item.Key == hidProductID.Value)
                        //    {
                        //        //Price.Text = item.Value;
                        //        double prc = commonMethods.ChangeToUS(lblPrice.Text);
                        //        afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                        //        lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                        //        break;
                        //    }
                        //}

                    }
                    if (lblSubtotals != null)
                    {
                        if (Price.Text != "")
                        {
                            subtotal = afterVat * commonMethods.ChangeToUS(txtQty.Text);
                            lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                            if (lblPrice.Text == "00,00" || lblPrice.Text == "0" || lblPrice.Text == "")
                            {
                                lblFinalSubtotal.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                            }
                        }
                        else
                        {
                            lblSubtotals.Text = "00,00";
                        }

                    }
                    if (lblFinalDifference != null)
                    {
                        difference = commonMethods.ChangeToUS(lblFinalSubtotal.Text) - commonMethods.ChangeToUS(lblSubtotals.Text);
                        if (lblFinalSubtotal.Text == "00,00")
                            difference = 0;
                        string sign = string.Empty;

                        if (difference > 0)
                        {
                            sign = "+";
                            divDifference.Style.Add("background", "url(../images/textbox_small_red.png) no-repeat;");
                            divDifference.Style.Add("color", "white");
                            divDifference.Attributes.Remove("class");
                            divDifference.Attributes.Add("class", "textbox_small_white");
                            lblFinalDifference.Style.Add("color", "white");
                        }
                        else if (difference < 0)
                        {
                            sign = "-";
                            divDifference.Style.Add("background", "url(../images/textbox_small_green.png) no-repeat;");
                            divDifference.Attributes.Remove("class");
                            divDifference.Attributes.Add("class", "textbox_small_white");
                            lblFinalDifference.Style.Add("color", "white");
                        }
                        else
                        {
                            divDifference.Style.Add("background", "url(../images/textbox_small.png) no-repeat;");
                            divDifference.Attributes.Remove("class");
                            divDifference.Attributes.Add("class", "textbox_small");
                            lblFinalDifference.Style.Add("color", "Gray");
                        }

                        lblFinalDifference.Text = sign + " " + commonMethods.ChangetToUK(difference.ToString("N").Replace("-", ""));
                    }
                    if (ddlSuppliers != null)
                        lnkProductName.PostBackUrl = "ManageSupplier.aspx?id=" + ddlSuppliers.SelectedValue.ToString();
                }
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
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                HiddenField hidSupplierID = e.Row.FindControl("hidSupplierID") as HiddenField;
                HiddenField hidFavorite = e.Row.FindControl("hidFavorite") as HiddenField;

                TextBox txtDeliveryTime = e.Row.FindControl("txtDeliveryTime") as TextBox;
                TextBox txtDeliveryTimeEnd = e.Row.FindControl("txtDeliveryTimeEnd") as TextBox;
                TextBox txtComment = e.Row.FindControl("txtComment") as TextBox;
                TextBox txtNewComment = e.Row.FindControl("txtNewComment") as TextBox;
                TextBox txtInvoiceNumber = e.Row.FindControl("txtInvoiceNumber") as TextBox;
                TextBox txtInvoiceAmount = e.Row.FindControl("txtInvoiceAmount") as TextBox;
                TextBox txtNonInvoiceAmount = e.Row.FindControl("txtNonInvoiceAmount") as TextBox;
                ImageButton imgBtnSaveReceivedOrder = e.Row.FindControl("imgBtnSaveReceivedOrder") as ImageButton;


                Label lblOrderTotal = e.Row.FindControl("lblOrderTotal") as Label;
                Label lblPreviousBalance = e.Row.FindControl("lblPreviousBalance") as Label;
                Label lblOrderBalance = e.Row.FindControl("lblOrderBalance") as Label;
                Label lblAmountPaid = e.Row.FindControl("lblAmountPaid") as Label;
                TextBox txtAmountPaid = e.Row.FindControl("txtAmountPaid") as TextBox;

                Label lblNewBalance = e.Row.FindControl("lblNewBalance") as Label;

                Label lblAggEstimated = e.Row.FindControl("lblAggEstimated") as Label;
                Label lblAggFinal = e.Row.FindControl("lblAggFinal") as Label;






                GridView grdOrders_FinalOrder = e.Row.FindControl("grdOrders_FinalOrder") as GridView;

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
                        txtNewComment.Text = p.GetColumn("OrderNoteByReceiver").ToString();
                        txtInvoiceNumber.Text = p.GetColumn("InvoiceNumber").ToString();
                        txtInvoiceAmount.Text = p.GetColumn("InvoiceAmount").ToString();
                    }

                }

                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                //*******************

                tblOrders order = new tblOrders();
                order.GetOrder(Convert.ToInt32(hidSupplierID.Value), ViewState["OrderUpdate"].ToString());
                if (order.RowCount > 0)
                {
                    if (!string.IsNullOrEmpty(order.s_PaidAmount))
                    {
                        txtAmountPaid.ReadOnly = true;
                        imgBtnSaveReceivedOrder.Enabled = false;
                        imgSaveAllTop.Enabled = false;
                        imgBtnSaveAllBottom.Enabled = false;
                        lblOrderStatus.Text = "Order status is not editable!";
                        LockTextBoxes(this, true);
                    }
                }

                //******************
            }
        }
        catch (Exception ex)
        { }
    }

    public static void LockTextBoxes(Control parent, bool Action)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.GetType() == typeof(TextBox))
            {
                if ((((TextBox)(c)).ID != "txtTotalKM") && (((TextBox)(c)).ID != "txtTotalExpanse"))
                {
                    ((TextBox)(c)).ReadOnly = Action;
                }
                // tb.Focus();
            }
            //else if (c.GetType == typeof(DropDownList))
            //{
            //    ((DropDownList)(c)).SelectedIndex = 0;
            //}
            if (c.HasControls())
            {
                LockTextBoxes(c, Action);
            }
        }

    }

    protected void imgSaveAllTop_Click(object sender, EventArgs e)
    {
        TransactionMgr tx = TransactionMgr.ThreadTransactionMgr();
        try
        {
            tx.BeginTransaction();


            for (int j = 0; j < grdSuppliers.Rows.Count; j++)
            {
                Label lblAggEstimated = grdSuppliers.Rows[j].FindControl("lblAggEstimated") as Label;
                Label lblAggFinal = grdSuppliers.Rows[j].FindControl("lblAggFinal") as Label;
                Label lblAggDifference = grdSuppliers.Rows[j].FindControl("lblAggDifference") as Label;
                Label lblSubtotals = grdSuppliers.Rows[j].FindControl("lblSubtotals") as Label;

                TextBox txtDeliveryTime = grdSuppliers.Rows[j].FindControl("txtDeliveryTime") as TextBox;
                TextBox txtDeliveryTimeEnd = grdSuppliers.Rows[j].FindControl("txtDeliveryTimeEnd") as TextBox;

                TextBox txtNewComment = grdSuppliers.Rows[j].FindControl("txtNewComment") as TextBox;

                CheckBox chkInvoice = grdSuppliers.Rows[j].FindControl("chkInvoice") as CheckBox;
                TextBox txtInvoiceNumber = grdSuppliers.Rows[j].FindControl("txtInvoiceNumber") as TextBox;
                TextBox txtInvoiceAmount = grdSuppliers.Rows[j].FindControl("txtInvoiceAmount") as TextBox;
                TextBox txtNonInvoiceAmount = grdSuppliers.Rows[j].FindControl("txtNonInvoiceAmount") as TextBox;

                Label lblAmountPaid = grdSuppliers.Rows[j].FindControl("lblAmountPaid") as Label;

                Label lblOrderBalance = grdSuppliers.Rows[j].FindControl("lblOrderBalance") as Label;
                Label lblPreviousBalance = grdSuppliers.Rows[j].FindControl("lblPreviousBalance") as Label;

                TextBox txtAmountPaid = grdSuppliers.Rows[j].FindControl("txtAmountPaid") as TextBox;

                Label lblNewBalance = grdSuppliers.Rows[j].FindControl("lblNewBalance") as Label;

                HiddenField hidSupplierID = grdSuppliers.Rows[j].FindControl("hidSupplierID") as HiddenField;

                GridView grdOrders_FinalOrder = grdSuppliers.Rows[j].FindControl("grdOrders_FinalOrder") as GridView;


                tblOrders order = new tblOrders();
                order.GetOrder(Convert.ToInt32(hidSupplierID.Value), ViewState["OrderUpdate"].ToString());
                if (order.RowCount > 0)
                {
                    ViewState["OrderID"] = order.PkOrderID;
                    order.FkOrderStatusID = 2;
                    order.OrderNoteByReceiver = txtNewComment.Text;
                    order.OrderSubtotal = commonMethods.ChangeToUS(lblAggEstimated.Text);
                    order.FinalSubtotal = commonMethods.ChangeToUS(lblAggFinal.Text);
                    order.SubtotalDifference = commonMethods.ChangeToUS(lblAggDifference.Text.Replace("+", "").Replace("-", ""));
                    order.IsInvoice = chkInvoice.Checked;
                    order.DeliveryTime = txtDeliveryTime.Text + "-" + txtDeliveryTimeEnd.Text;
                    order.PaidAmount = commonMethods.ChangeToUS(txtAmountPaid.Text);

                    if (lblPreviousBalance.Text != "00,00" || lblPreviousBalance.Text != "0")
                    {
                        if (lblOrderBalance.Text != txtAmountPaid.Text)
                        {
                            //order.Balance = commonMethods.ChangeToUS(lblNewBalance.Text);
                            order.Balance = commonMethods.ChangeToUS(lblOrderBalance.Text) - commonMethods.ChangeToUS(txtAmountPaid.Text);
                        }
                    }

                    order.DModifiedDate = DateTime.Now;
                    order.Save();

                    if (chkInvoice.Checked)
                    {
                        tblOrderInvoices invoices = new tblOrderInvoices();
                        if ((txtInvoiceNumber.Text != "") &&
                            (txtInvoiceAmount.Text != "" && txtInvoiceAmount.Text != "00,00" && txtInvoiceAmount.Text != "0"))
                        {
                            invoices.FlushData();
                            invoices.GetInvoiceByOrder(Convert.ToInt32(ViewState["OrderID"]));
                            if (invoices.RowCount > 0)
                            {
                                invoices.InvoiceNumber = txtInvoiceNumber.Text;
                                invoices.InvoiceAmount = commonMethods.ChangeToUS(txtInvoiceAmount.Text);
                                invoices.NonInvoiceAmount = commonMethods.ChangeToUS(txtNonInvoiceAmount.Text);
                                invoices.Save();
                            }
                            else
                            {
                                invoices.FlushData();
                                invoices.AddNew();
                                invoices.FkOrderID = Convert.ToInt32(ViewState["OrderID"]);
                                invoices.InvoiceNumber = txtInvoiceNumber.Text;
                                invoices.InvoiceAmount = commonMethods.ChangeToUS(txtInvoiceAmount.Text);
                                invoices.NonInvoiceAmount = commonMethods.ChangeToUS(txtNonInvoiceAmount.Text);
                                invoices.Save();
                            }
                        }
                    }
                }

                for (int i = 0; i < grdOrders_FinalOrder.Rows.Count; i++)
                {
                    Label lblSubtotal = grdOrders_FinalOrder.Rows[i].FindControl("lblSubtotals") as Label;
                    Label lblFinalSubtotal = grdOrders_FinalOrder.Rows[i].FindControl("lblFinalSubtotal") as Label;
                    Label lblFinalDifference = grdOrders_FinalOrder.Rows[i].FindControl("lblFinalDifference") as Label;
                    Label LabelPrice = grdOrders_FinalOrder.Rows[i].FindControl("LabelPrice") as Label;

                    HiddenField hidProductID = grdOrders_FinalOrder.Rows[i].FindControl("hidProductID") as HiddenField;
                    DropDownList ddlPacking = grdOrders_FinalOrder.Rows[i].FindControl("ddlPacking") as DropDownList;

                    Label lblVat = grdOrders_FinalOrder.Rows[i].FindControl("lblVat") as Label;
                    Label lblAfterVat = grdOrders_FinalOrder.Rows[i].FindControl("lblAfterVat") as Label;
                    TextBox txtQt = grdOrders_FinalOrder.Rows[i].FindControl("txtQty") as TextBox;

                    TextBox lblPrice = grdOrders_FinalOrder.Rows[i].FindControl("lblPrice") as TextBox;

                    tblOrderDetail orderDetail = new tblOrderDetail();
                    orderDetail.GetOrderDetailForUpdate(ViewState["OrderUpdate"].ToString(), Convert.ToInt32(ViewState["OrderID"]), Convert.ToInt32(hidProductID.Value));
                    if (orderDetail.RowCount > 0)
                    {
                        orderDetail.FkProductPackageID = Convert.ToInt32(ddlPacking.SelectedValue);
                        if (lblPrice.Text != "" && lblPrice.Text != "00,00" && lblPrice.Text != "0")
                            orderDetail.NewPrice = commonMethods.ChangeToUS(lblPrice.Text);
                        else
                            orderDetail.ProudctPrice = commonMethods.ChangeToUS(LabelPrice.Text);

                        orderDetail.Vat = Convert.ToInt32(lblVat.Text.Replace("%", ""));
                        orderDetail.AfterVat = commonMethods.ChangeToUS(lblAfterVat.Text.Replace("€", ""));
                        orderDetail.Quantity = Convert.ToInt32(txtQt.Text);
                        orderDetail.Subtotal = commonMethods.ChangeToUS(lblSubtotal.Text);
                        orderDetail.FinalSubtotal = commonMethods.ChangeToUS(lblFinalSubtotal.Text);
                        orderDetail.SubtotalDifference = commonMethods.ChangeToUS(lblFinalDifference.Text.Replace("+", "").Replace("-", "").Replace(" ", ""));
                        orderDetail.DModifiedDate = DateTime.Now;
                        orderDetail.Save();
                        tblBaseOrder b_Allow = new tblBaseOrder();
                        b_Allow.getBaseOrder(ViewState["OrderUpdate"].ToString());
                        if (b_Allow.RowCount > 0)
                        {
                            b_Allow.AllowEdit = false;
                            b_Allow.Save();
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "savingMessage", "$(function(){RecordSaved();});", true);

                    }

                    if (lblPrice.Text != "" && lblPrice.Text != "00,00" && lblPrice.Text != "0")
                    {
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
                                    else
                                    {
                                        tblSupplierProductPrices sp = new tblSupplierProductPrices();
                                        sp.AddNew();
                                        sp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                                        sp.FkProductPackingQuantityRelID = rel.PkProductPackingQuantityRelID;
                                        sp.Price = commonMethods.ChangeToUS(lblPrice.Text);
                                        sp.DModifiedDate = DateTime.Now;
                                        sp.DCreatedDate = DateTime.Now;
                                        sp.Save();
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
                    }
                }
            }


            tx.CommitTransaction();
            lblMessageTop.Visible = true;


            upnlSuppliers.Update();


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
            if (ViewState["OrderUpdate"] != null)
            {
                tblOrders o = new tblOrders();
                o.getOrdersBySessionOrderID(ViewState["OrderUpdate"].ToString());
                if (o.RowCount > 0)
                {
                    for (int i = 0; i < o.RowCount; i++)
                    {
                        SupplierIDs.Add(o.GetColumn("fksupplierid").ToString());
                        o.MoveNext();
                    }
                }
            }
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
                dv = null;
                if (dv == null)
                    setDataView();

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
                //lblOrderStatus_For_Final_Orders.Text = "Order of " + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;


                est_subtotal = 0.0;
                est_grand_total = 0.0;
                double difference = 0.0;
                double paidamount = 0.0;
                for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                {
                    GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                    for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                    {
                        Label lblSubtotals = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;

                        Label lblFinalSubtotal = grdOrders_FinalOrder.Rows[j].FindControl("lblFinalSubtotal") as Label;
                        Label lblFinalDifference = grdOrders_FinalOrder.Rows[j].FindControl("lblFinalDifference") as Label;

                        est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotals.Text), 2);
                        fin_subtotal += Math.Round(commonMethods.ChangeToUS(lblFinalSubtotal.Text), 2);

                        if (lblFinalDifference.Text.IndexOf('+') != -1)
                        {
                            difference = Math.Round(commonMethods.ChangeToUS(lblFinalDifference.Text.Replace("+", "")), 2);
                            fin_difference += difference;
                        }
                        else if (lblFinalDifference.Text.IndexOf('-') != -1)
                        {
                            difference = Math.Round(commonMethods.ChangeToUS(lblFinalDifference.Text.Replace("-", "")), 2);
                            fin_difference -= difference;
                        }
                        else
                        {
                            difference = 0.0;
                        }


                    }
                    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;
                    Label lblAggEstimated = grdSuppliers.Rows[i].FindControl("lblAggEstimated") as Label;
                    Label lblAggFinal = grdSuppliers.Rows[i].FindControl("lblAggFinal") as Label;
                    Label lblAggDifference = grdSuppliers.Rows[i].FindControl("lblAggDifference") as Label;
                    HtmlContainerControl divAggDifference = grdSuppliers.Rows[i].FindControl("divAggDifference") as HtmlContainerControl;

                    Label lblOrderTotal = grdSuppliers.Rows[i].FindControl("lblOrderTotal") as Label;
                    Label lblPreviousBalance = grdSuppliers.Rows[i].FindControl("lblPreviousBalance") as Label;
                    Label lblOrderBalance = grdSuppliers.Rows[i].FindControl("lblOrderBalance") as Label;
                    Label lblNewBalance = grdSuppliers.Rows[i].FindControl("lblNewBalance") as Label;
                    Label lblAmountPaid = grdSuppliers.Rows[i].FindControl("lblAmountPaid") as Label;
                    TextBox txtAmountPaid = grdSuppliers.Rows[i].FindControl("txtAmountPaid") as TextBox;

                    HiddenField hidSupplierID = grdSuppliers.Rows[i].FindControl("hidSupplierID") as HiddenField;

                    lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));

                    lblAggEstimated.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                    lblAggFinal.Text = commonMethods.ChangetToUK(fin_subtotal.ToString("N"));

                    if (fin_difference > 0)
                    {

                        divAggDifference.Style.Add("background", "url(../images/textbox_small_red.png) no-repeat;");
                        //divAggDifference.Style.Add("color", "white");
                        divAggDifference.Attributes.Remove("class");
                        divAggDifference.Attributes.Add("class", "textbox_small_white");
                        lblAggDifference.Style.Add("color", "white");
                        lblAggDifference.Text = "+ " + commonMethods.ChangetToUK(fin_difference.ToString("N"));
                    }
                    else if (fin_difference < 0)
                    {

                        divAggDifference.Style.Add("background", "url(../images/textbox_small_green.png) no-repeat;");
                        //divAggDifference.Style.Add("color", "white");
                        divAggDifference.Attributes.Remove("class");
                        divAggDifference.Attributes.Add("class", "textbox_small_white");
                        lblAggDifference.Style.Add("color", "white");
                        lblAggDifference.Text = "- " + commonMethods.ChangetToUK(fin_difference.ToString("N")).Replace("-", "");
                    }
                    else
                    {
                        divAggDifference.Style.Add("background", "url(../images/textbox_small.png) no-repeat;");
                        divAggDifference.Attributes.Remove("class");
                        divAggDifference.Attributes.Add("class", "textbox_small");
                        lblAggDifference.Text = "00,00";
                    }
                    est_grand_total += est_subtotal;
                    est_grand_final += fin_subtotal;
                    est_grand_difference += fin_difference;

                    est_subtotal = 0.0;
                    fin_subtotal = 0.0;
                    fin_difference = 0.0;

                    lblOrderTotal.Text = "00,00";
                    if (lblAggFinal != null)
                    {
                        if (lblAggFinal.Text == "" || lblAggFinal.Text == "00,00" || lblAggFinal.Text == "0")
                        {
                            lblOrderTotal.Text = lblAggEstimated.Text;
                        }
                        else
                        {
                            lblOrderTotal.Text = lblAggFinal.Text;
                        }
                    }

                    tblSupplier s = new tblSupplier();
                    s.GetOweAmount(Convert.ToInt32(hidSupplierID.Value), DepartmentID);
                    if (s.RowCount > 0)
                    {
                        if (s.GetColumn("amount").ToString().Trim() != "")
                            lblPreviousBalance.Text = commonMethods.ChangetToUK(Convert.ToDouble(s.GetColumn("amount").ToString()).ToString("N"));
                        else
                            lblPreviousBalance.Text = "00,00";
                    }
                    else
                        lblPreviousBalance.Text = "00,00";

                    if (lblPreviousBalance.Text == "")
                    {
                        lblPreviousBalance.Text = "00,00";
                    }
                    lblOrderBalance.Text = commonMethods.ChangetToUK((commonMethods.ChangeToUS(lblPreviousBalance.Text) + commonMethods.ChangeToUS(lblOrderTotal.Text)).ToString("N"));

                    txtAmountPaid.Text = commonMethods.ChangetToUK(commonMethods.ChangeToUS(lblOrderTotal.Text).ToString()); //- commonMethods.ChangeToUS(lblPreviousBalance.Text)).ToString("N"));

                    if (txtAmountPaid.Text != "00,00" && txtAmountPaid.Text != "0" && txtAmountPaid.Text != "")
                        paidamount += commonMethods.ChangeToUS(txtAmountPaid.Text);
                    //lblNewBalance.Text = (commonMethods.ChangeToUS(lblOrderBalance.Text.Replace("-", "")) - commonMethods.ChangeToUS(txtAmountPaid.Text)).ToString("N");
                }
                txtPaid.Text = commonMethods.ChangetToUK(paidamount.ToString("N"));
                double paidDifference = 0.0;
                paidDifference = (paidamount - commonMethods.ChangeToUS(txtCash.Text));

                string sign = string.Empty;

                if (paidDifference > 0)
                {
                    sign = "-";
                    divLeft.Style.Add("background", "url(../images/textbox_red.png) no-repeat;");
                    divLeft.Style.Add("color", "white");
                    txtLeft.Style.Add("color", "white");
                }
                else if (paidDifference < 0)
                {
                    sign = "+";
                    divLeft.Style.Add("background", "url(../images/textbox_light_green.png) no-repeat;");
                    divLeft.Style.Add("color", "white");
                    txtLeft.Style.Add("color", "white");
                }
                txtLeft.Text = sign + " " + commonMethods.ChangetToUK(paidDifference.ToString("N").Replace("-", ""));
                lblGrandFinalTotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                txtGrandEstimated.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                txtGrandFinal.Text = commonMethods.ChangetToUK(est_grand_final.ToString("N"));
                //double dFinalAmount = commonMethods.ChangeToUS(txtGrandEstimated.Text) - commonMethods.ChangeToUS(txtGrandFinal.Text);
                if (est_grand_difference > 0)
                {
                    sign = "+";
                    divGrandDifference.Style.Add("background", "url(../images/textbox_red.png) no-repeat;");
                    divGrandDifference.Style.Remove("class");
                    divGrandDifference.Style.Add("class", "textbox115_white");
                    txtGrandDifference.Style.Add("color", "white");
                    lblSpanGrandDifference.Style.Add("color", "white");
                }
                else if (est_grand_difference < 0)
                {
                    sign = "-";
                    divGrandDifference.Style.Add("background", "url(../images/textbox_light_green.png) no-repeat;");
                    divGrandDifference.Style.Remove("class");
                    divGrandDifference.Style.Add("class", "textbox115_white");
                    txtGrandDifference.Style.Add("color", "white");
                    lblSpanGrandDifference.Style.Add("color", "white");
                }
                else
                {
                    sign = "-";
                    divGrandDifference.Style.Add("background", "url(../images/textbox_115.png) no-repeat;");
                    divGrandDifference.Style.Remove("class");
                    divGrandDifference.Style.Add("class", "textbox115");
                    txtGrandDifference.Style.Add("color", "Gray");
                    lblSpanGrandDifference.Style.Add("color", "Gray");
                }
                txtGrandDifference.Text = sign + " " + commonMethods.ChangetToUK(est_grand_difference.ToString("N"));
                

                est_grand_total = 0.0;
                est_grand_final = 0.0;
                est_grand_difference = 0.0;

                mvMain.SetActiveView(vFinalOrders);
                SetTotalAmount();
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


    }
    #endregion

    protected void lblPrice_TextChanged(object sender, EventArgs e)
    {
        fin_subtotal = 0.0;
        fin_difference = 0.0;
        est_subtotal = 0.0;
        try
        {
            DropDownList ddlSuppliers = ((TextBox)sender).Parent.FindControl("ddlSuppliers") as DropDownList;
            TextBox txtQty = ((TextBox)sender).Parent.FindControl("txtQty") as TextBox;
            HiddenField hidProductID = ((TextBox)sender).Parent.FindControl("hidProductID") as HiddenField;
            //Label lblPrice = ((TextBox)sender).Parent.FindControl("lblPrice") as Label;
            TextBox lblPrice = ((TextBox)sender).Parent.FindControl("lblPrice") as TextBox;

            Label Price = ((TextBox)sender).Parent.FindControl("LabelPrice") as Label;
            Label lblPriceDifference = ((TextBox)sender).Parent.FindControl("lblPriceDifference") as Label;
            HtmlImage img = ((TextBox)sender).Parent.FindControl("img") as HtmlImage;


            DropDownList ddlPacking = ((TextBox)sender).Parent.FindControl("ddlPacking") as DropDownList;
            DropDownList ddlQuantity = ((TextBox)sender).Parent.FindControl("ddlQuantity") as DropDownList;

            Label lblVat = ((TextBox)sender).Parent.FindControl("lblVat") as Label;
            Label lblAfterVat = ((TextBox)sender).Parent.FindControl("lblAfterVat") as Label;
            Label lblSubtotals = ((TextBox)sender).Parent.FindControl("lblSubtotals") as Label;
            Label lblFinalSubtotal = ((TextBox)sender).Parent.FindControl("lblFinalSubtotal") as Label;
            Label lblFinalDifference = ((TextBox)sender).Parent.FindControl("lblFinalDifference") as Label;
            HtmlContainerControl divDifference = ((TextBox)sender).Parent.FindControl("divDifference") as HtmlContainerControl;



            double afterVat = 0.0;
            double subtotal = 0.0;
            double finalSubtal = 0.0;
            double difference = 0.0;
            if (dv == null)
                setDataView();
            // DataRow[] dr = dv.ToTable("Suppliers", true, "pkSupplierID", "sBrandName", "UnitPrice", "Qty", "pkproductid").Select("pkproductid = '" + hidProductID.Value + "' and pkSupplierID = '" + ddlSuppliers.SelectedValue + "'", "UnitPrice");

            //lblPrice.Text = dr[0][2].ToString();
            bool newPrice = false;
            if (lblPrice.Text == "" || lblPrice.Text == "0" || lblPrice.Text == "00,00")
            {
                newPrice = true;
                lblPrice.Text = Price.Text;
                lblPriceDifference.Text = "00,00";
                lblPriceDifference.ForeColor = Color.DarkGray;
                divDifference.Style.Add("background", "url(../images/textbox_small.png) no-repeat;");
                divDifference.Style.Add("color", "Gray");
                lblFinalDifference.Style.Add("color", "#727272");
                img.Src = "";
            }
            else
            {
                double p_difference = 0.0;
                p_difference = commonMethods.ChangeToUS(lblPrice.Text) - commonMethods.ChangeToUS(Price.Text);
                string sign = string.Empty;
                string imageRatio = string.Empty;
                if (p_difference > 0)
                {
                    sign = "+ ";
                    imageRatio = "../images/up_arrow.png";
                    lblPriceDifference.ForeColor = Color.Red;
                }
                else if (p_difference < 0)
                {
                    sign = "- ";
                    imageRatio = "../images/down_arrow.png";
                    lblPriceDifference.ForeColor = Color.Green;
                }
                else
                {
                    sign = "";
                    imageRatio = "";
                }
                lblPriceDifference.Text = sign + commonMethods.ChangetToUK(p_difference.ToString("N")).Replace("-", "");
                img.Src = imageRatio;
            }


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
                    lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                }
                if (lblFinalSubtotal != null)
                {
                    //subtotal = commonMethods.ChangeToUS(lblAfterVat.Text.Replace("€", "")) * Convert.ToDouble(txtQty.Text);
                    finalSubtal = commonMethods.ChangeToUS(lblAfterVat.Text.Replace("€", "")) * Convert.ToDouble(txtQty.Text);
                    //lblSubtotals.Text = commonMethods.ChangetToUK(subtotal.ToString("N"));
                    lblFinalSubtotal.Text = commonMethods.ChangetToUK(finalSubtal.ToString("N"));

                }
                if (lblFinalDifference != null)
                {
                    difference = finalSubtal - commonMethods.ChangeToUS(lblSubtotals.Text);
                    string sign = string.Empty;

                    if (difference > 0)
                    {
                        sign = "+";
                        divDifference.Style.Add("background", "url(../images/textbox_small_red.png) no-repeat;");
                        divDifference.Attributes.Remove("class");
                        divDifference.Attributes.Add("class", "textbox_small_white");

                        lblFinalDifference.Style.Add("color", "white");
                    }
                    else if (difference < 0)
                    {
                        sign = "-";
                        divDifference.Style.Add("background", "url(../images/textbox_small_green.png) no-repeat;");
                        divDifference.Attributes.Remove("class");
                        divDifference.Attributes.Add("class", "textbox_small_white");
                        lblFinalDifference.Style.Add("color", "white");
                    }
                    else
                    {
                        divDifference.Style.Add("background", "url(../images/textbox_small.png) no-repeat;");
                        divDifference.Attributes.Remove("class");
                        divDifference.Attributes.Add("class", "textbox_small");
                        lblFinalDifference.Style.Add("color", "Gray");
                    }

                    lblFinalDifference.Text = sign + " " + commonMethods.ChangetToUK(difference.ToString("N").Replace("-", ""));
                }


                //if (txtQty != null)
                //  txtQty.Attributes.Add("onchange", "javascript:addSubtotals();");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sub", "$(function(){  addSubtotals();});", true);
                //GridView grdSuppliers = ((TextBox)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridView;
                string id = ((TextBox)sender).Parent.Parent.Parent.Parent.ID;

                if (id == "grdOrders_FinalOrder")
                {
                    if (newPrice)
                    {
                        lblFinalSubtotal.Text = "00,00";
                    }
                    est_subtotal = 0.0;
                    for (int i = 0; i < grdSuppliers.Rows.Count; i++)
                    {
                        GridView grdOrders_FinalOrder = grdSuppliers.Rows[i].FindControl("grdOrders_FinalOrder") as GridView;
                        for (int j = 0; j < grdOrders_FinalOrder.Rows.Count; j++)
                        {
                            Label lblSubtotal = grdOrders_FinalOrder.Rows[j].FindControl("lblSubtotals") as Label;
                            Label lblFinalSubtotalAggregate = grdOrders_FinalOrder.Rows[j].FindControl("lblFinalSubtotal") as Label;
                            Label lblFinalDifferenceAggregate = grdOrders_FinalOrder.Rows[j].FindControl("lblFinalDifference") as Label;
                            est_subtotal += Math.Round(commonMethods.ChangeToUS(lblSubtotal.Text), 2);
                            fin_subtotal += Math.Round(commonMethods.ChangeToUS(lblFinalSubtotalAggregate.Text), 2);
                            if (lblFinalDifferenceAggregate.Text.IndexOf('+') != -1)
                            {
                                difference = Math.Round(commonMethods.ChangeToUS(lblFinalDifferenceAggregate.Text.Replace("+", "")), 2);
                                fin_difference += difference;
                            }
                            else if (lblFinalDifferenceAggregate.Text.IndexOf('-') != -1)
                            {
                                difference = Math.Round(commonMethods.ChangeToUS(lblFinalDifferenceAggregate.Text.Replace("-", "")), 2);
                                fin_difference -= difference;
                            }
                            else
                            {
                                difference = 0.0;
                            }



                        }

                        Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;

                        Label lblAggEstimated = grdSuppliers.Rows[i].FindControl("lblAggEstimated") as Label;
                        Label lblAggFinal = grdSuppliers.Rows[i].FindControl("lblAggFinal") as Label;
                        Label lblAggDifference = grdSuppliers.Rows[i].FindControl("lblAggDifference") as Label;
                        HtmlContainerControl divAggDifference = grdSuppliers.Rows[i].FindControl("divAggDifference") as HtmlContainerControl;
                        HiddenField hidSupplierID = grdSuppliers.Rows[i].FindControl("hidSupplierID") as HiddenField;
                        Label lblOrderTotal = grdSuppliers.Rows[i].FindControl("lblOrderTotal") as Label;
                        Label lblPreviousBalance = grdSuppliers.Rows[i].FindControl("lblPreviousBalance") as Label;
                        Label lblOrderBalance = grdSuppliers.Rows[i].FindControl("lblOrderBalance") as Label;
                        Label lblNewBalance = grdSuppliers.Rows[i].FindControl("lblNewBalance") as Label;
                        Label lblAmountPaid = grdSuppliers.Rows[i].FindControl("lblAmountPaid") as Label;
                        TextBox txtAmountPaid = grdSuppliers.Rows[i].FindControl("txtAmountPaid") as TextBox;

                        lblSupplierTotal.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));

                        lblAggEstimated.Text = commonMethods.ChangetToUK(est_subtotal.ToString("N"));
                        lblAggFinal.Text = commonMethods.ChangetToUK(fin_subtotal.ToString("N"));

                        if (fin_difference > 0)
                        {
                            lblAggDifference.Text = "+ " + commonMethods.ChangetToUK(fin_difference.ToString("N"));
                            divAggDifference.Style.Add("background", "url(../images/textbox_small_red.png) no-repeat;");
                            divAggDifference.Attributes.Remove("class");
                            divAggDifference.Attributes.Add("class", "textbox_small_white");
                            lblAggDifference.Style.Add("color", "white");
                        }
                        else if (fin_difference < 0)
                        {
                            lblAggDifference.Text = "- " + commonMethods.ChangetToUK(fin_difference.ToString("N")).Replace("-", "");
                            divAggDifference.Style.Add("background", "url(../images/textbox_small_green.png) no-repeat;");
                            divAggDifference.Attributes.Remove("class");
                            divAggDifference.Attributes.Add("class", "textbox_small_white");
                            lblAggDifference.Style.Add("color", "white");
                        }
                        else
                        {
                            lblAggDifference.Text = "00,00";
                            lblAggDifference.Style.Add("color", "#727272");
                            divAggDifference.Style.Add("background", "url(../images/textbox_small.png) no-repeat;");
                            divAggDifference.Attributes.Remove("class");
                            divAggDifference.Attributes.Add("class", "textbox_small");
                        }

                        if (lblAggFinal.Text != "" && lblAggFinal.Text != "00,00" && lblAggFinal.Text != "0")
                        {
                            lblOrderTotal.Text = lblAggFinal.Text;
                        }
                        else
                        {
                            lblOrderTotal.Text = lblAggEstimated.Text;
                        }
                        tblSupplier s = new tblSupplier();
                        s.GetOweAmount(Convert.ToInt32(hidSupplierID.Value), DepartmentID);
                        if (s.RowCount > 0)
                        {
                            if (s.GetColumn("amount").ToString().Trim() != "")
                                lblPreviousBalance.Text = commonMethods.ChangetToUK(Convert.ToDouble(s.GetColumn("amount").ToString()).ToString("N"));
                            else
                                lblPreviousBalance.Text = "00,00";
                        }
                        else
                            lblPreviousBalance.Text = "00,00";

                        if (lblPreviousBalance.Text == "")
                        {
                            lblPreviousBalance.Text = "00,00";
                        }
                        lblOrderBalance.Text = commonMethods.ChangetToUK((commonMethods.ChangeToUS(lblPreviousBalance.Text) + commonMethods.ChangeToUS(lblOrderTotal.Text)).ToString("N"));
                        txtAmountPaid.Text = commonMethods.ChangetToUK((commonMethods.ChangeToUS(lblOrderTotal.Text) - commonMethods.ChangeToUS(lblPreviousBalance.Text)).ToString("N"));

                        //lblNewBalance.Text = (commonMethods.ChangeToUS(lblOrderBalance.Text.Replace("-", "")) - commonMethods.ChangeToUS(txtAmountPaid.Text)).ToString("N");

                        est_grand_total += est_subtotal;
                        est_grand_final += fin_subtotal;
                        est_grand_difference += fin_difference;

                        est_subtotal = 0.0;
                    }
                    lblGrandFinalTotal.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                    txtGrandEstimated.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
                    txtGrandFinal.Text = commonMethods.ChangetToUK(est_grand_final.ToString("N"));

                    string grandDiff_sign = string.Empty;
                    if (est_grand_difference > 0)
                    {
                        divGrandDifference.Style.Add("background", "url(../images/textbox_red.png) no-repeat;");

                        divGrandDifference.Style.Remove("class");
                        divGrandDifference.Style.Add("class", "textbox115_white");
                        txtGrandDifference.Style.Add("color", "white");
                        lblSpanGrandDifference.Style.Add("color", "white");
                        grandDiff_sign = "+ ";
                    }
                    else if (est_grand_difference < 0)
                    {
                        divGrandDifference.Style.Add("background", "url(../images/textbox_light_green.png) no-repeat;");
                        divGrandDifference.Style.Remove("class");
                        divGrandDifference.Style.Add("class", "textbox115_white");
                        txtGrandDifference.Style.Add("color", "white");
                        lblSpanGrandDifference.Style.Add("color", "white");
                        grandDiff_sign = "- ";
                    }
                    else
                    {
                        divGrandDifference.Style.Add("background", "url(../images/textbox_115.png) no-repeat;");
                        divGrandDifference.Style.Remove("class");
                        divGrandDifference.Style.Add("class", "textbox115");
                        txtGrandDifference.Style.Add("color", "#727272");
                        lblSpanGrandDifference.Style.Add("color", "#727272");
                        grandDiff_sign = "";
                    }
                    txtGrandDifference.Text = grandDiff_sign + commonMethods.ChangetToUK(est_grand_difference.ToString("N").Replace("-", ""));

                    est_grand_total = 0.0;
                    est_grand_final = 0.0;
                    est_grand_difference = 0.0;

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
                }
                change = true;
            }
            else
            {
                txtQty.Text = "0";
            }

            if (newPrice)
            {
                lblPrice.Text = "00,00";
                lblFinalSubtotal.Text = "00,00";

            }
            //for (int i = 0; i < grdSuppliers.Rows.Count; i++)
            //{
            //    Label lblSupplierTotal = grdSuppliers.Rows[i].FindControl("lblSupplierTotal") as Label;

            //}
            //Label lblSupplierTotal = this.Master.FindControl("ContentPlaceHolder1").FindControl("grdSuppliers").FindControl("lblSupplierTotal") as Label;

            SetTotalAmount();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "data", " $(function(){WaterMark();});", true);
        }
        catch (Exception ex)
        { }
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
            Label Price = ((TextBox)sender).Parent.FindControl("LabelPrice") as Label;
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

            if (lblPrice.Text != "" && lblPrice.Text != "00,00" && lblPrice.Text != "0")
                Price.Text = lblPrice.Text;

            if (Price.Text == "")
            {
                DataRow[] dr2 = dv.ToTable("Price", true, "pkProductPackageID", "pkproductid", "price").Select("pkproductid = '" + hidProductID.Value + "' and pkProductPackageID = '" + ddlPacking.SelectedValue + "' ");
                if (dr2[0][2].ToString() != "")
                    Price.Text = commonMethods.ChangetToUK(dr2[0][2].ToString());
            }


            if (Price.Text != "")
            {
                if (lblAfterVat != null)
                {
                    double prc = commonMethods.ChangeToUS(Price.Text);
                    afterVat = prc + (Convert.ToDouble(lblVat.Text.Replace("%", "")) * prc) / 100;
                    lblAfterVat.Text = commonMethods.ChangetToUK(afterVat.ToString("N")) + "€";
                }
                if (lblSubtotals != null)
                {
                    subtotal = commonMethods.ChangeToUS(lblAfterVat.Text.Replace("€", "")) * Convert.ToDouble(txtQty.Text);
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
                    txtGrandEstimated.Text = commonMethods.ChangetToUK(est_grand_total.ToString("N"));
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
    }
    protected void imgBtnSaveReceivedOrder_Click(object sender, EventArgs e)
    {
        try
        {
            Label lblAggEstimated = ((ImageButton)sender).Parent.FindControl("lblAggEstimated") as Label;
            Label lblAggFinal = ((ImageButton)sender).Parent.FindControl("lblAggFinal") as Label;
            Label lblAggDifference = ((ImageButton)sender).Parent.FindControl("lblAggDifference") as Label;
            Label lblSubtotals = ((ImageButton)sender).Parent.FindControl("lblSubtotals") as Label;

            TextBox txtDeliveryTime = ((ImageButton)sender).Parent.FindControl("txtDeliveryTime") as TextBox;
            TextBox txtDeliveryTimeEnd = ((ImageButton)sender).Parent.FindControl("txtDeliveryTimeEnd") as TextBox;

            TextBox txtNewComment = ((ImageButton)sender).Parent.FindControl("txtNewComment") as TextBox;

            CheckBox chkInvoice = ((ImageButton)sender).Parent.FindControl("chkInvoice") as CheckBox;
            TextBox txtInvoiceNumber = ((ImageButton)sender).Parent.FindControl("txtInvoiceNumber") as TextBox;
            TextBox txtInvoiceAmount = ((ImageButton)sender).Parent.FindControl("txtInvoiceAmount") as TextBox;
            TextBox txtNonInvoiceAmount = ((ImageButton)sender).Parent.FindControl("txtNonInvoiceAmount") as TextBox;
            Label lblOrderBalance = ((ImageButton)sender).Parent.FindControl("lblOrderBalance") as Label;
            Label lblPreviousBalance = ((ImageButton)sender).Parent.FindControl("lblPreviousBalance") as Label;
            Label lblAmountPaid = ((ImageButton)sender).Parent.FindControl("lblAmountPaid") as Label;
            TextBox txtAmountPaid = ((ImageButton)sender).Parent.FindControl("txtAmountPaid") as TextBox;

            Label lblNewBalance = ((ImageButton)sender).Parent.FindControl("lblNewBalance") as Label;

            HiddenField hidSupplierID = ((ImageButton)sender).Parent.FindControl("hidSupplierID") as HiddenField;


            tblOrders order = new tblOrders();
            order.GetOrder(Convert.ToInt32(hidSupplierID.Value), ViewState["OrderUpdate"].ToString());
            if (order.RowCount > 0)
            {
                ViewState["OrderID"] = order.PkOrderID;
                order.FkOrderStatusID = 2;
                order.OrderNoteByReceiver = txtNewComment.Text;
                order.OrderSubtotal = commonMethods.ChangeToUS(lblAggEstimated.Text);
                order.FinalSubtotal = commonMethods.ChangeToUS(lblAggFinal.Text);
                order.SubtotalDifference = commonMethods.ChangeToUS(lblAggDifference.Text.Replace("+", "").Replace("-", ""));
                order.IsInvoice = chkInvoice.Checked;
                order.DeliveryTime = txtDeliveryTime.Text + "-" + txtDeliveryTimeEnd.Text;
                order.PaidAmount = commonMethods.ChangeToUS(txtAmountPaid.Text);



                if (lblPreviousBalance.Text != "00,00" || lblPreviousBalance.Text != "0")
                {
                    if (lblOrderBalance.Text != txtAmountPaid.Text)
                    {
                        //order.Balance = commonMethods.ChangeToUS(lblNewBalance.Text);
                        order.Balance = commonMethods.ChangeToUS(lblOrderBalance.Text) - commonMethods.ChangeToUS(txtAmountPaid.Text);
                    }
                }
                order.DModifiedDate = DateTime.Now;
                order.Save();

                if (chkInvoice.Checked)
                {
                    tblOrderInvoices invoices = new tblOrderInvoices();
                    if ((txtInvoiceNumber.Text != "") &&
                        (txtInvoiceAmount.Text != "" && txtInvoiceAmount.Text != "00,00" && txtInvoiceAmount.Text != "0"))
                    {
                        invoices.FlushData();
                        invoices.GetInvoiceByOrder(Convert.ToInt32(ViewState["OrderID"]));
                        if (invoices.RowCount > 0)
                        {
                            invoices.InvoiceNumber = txtInvoiceNumber.Text;
                            invoices.InvoiceAmount = commonMethods.ChangeToUS(txtInvoiceAmount.Text);
                            invoices.NonInvoiceAmount = commonMethods.ChangeToUS(txtNonInvoiceAmount.Text);
                            invoices.Save();
                        }
                        else
                        {
                            invoices.FlushData();
                            invoices.AddNew();
                            invoices.FkOrderID = Convert.ToInt32(ViewState["OrderID"]);
                            invoices.InvoiceNumber = txtInvoiceNumber.Text;
                            invoices.InvoiceAmount = commonMethods.ChangeToUS(txtInvoiceAmount.Text);
                            invoices.NonInvoiceAmount = commonMethods.ChangeToUS(txtNonInvoiceAmount.Text);
                            invoices.Save();
                        }
                    }
                }
            }

            GridView grdOrders_FinalOrder = ((ImageButton)sender).Parent.FindControl("grdOrders_FinalOrder") as GridView;


            if (grdOrders_FinalOrder.Rows.Count > 0)
            {
                for (int i = 0; i < grdOrders_FinalOrder.Rows.Count; i++)
                {
                    Label lblSubtotal = grdOrders_FinalOrder.Rows[i].FindControl("lblSubtotals") as Label;
                    Label lblFinalSubtotal = grdOrders_FinalOrder.Rows[i].FindControl("lblFinalSubtotal") as Label;
                    Label lblFinalDifference = grdOrders_FinalOrder.Rows[i].FindControl("lblFinalDifference") as Label;
                    Label LabelPrice = grdOrders_FinalOrder.Rows[i].FindControl("LabelPrice") as Label;

                    HiddenField hidProductID = grdOrders_FinalOrder.Rows[i].FindControl("hidProductID") as HiddenField;
                    DropDownList ddlPacking = grdOrders_FinalOrder.Rows[i].FindControl("ddlPacking") as DropDownList;

                    Label lblVat = grdOrders_FinalOrder.Rows[i].FindControl("lblVat") as Label;
                    Label lblAfterVat = grdOrders_FinalOrder.Rows[i].FindControl("lblAfterVat") as Label;
                    TextBox txtQt = grdOrders_FinalOrder.Rows[i].FindControl("txtQty") as TextBox;

                    TextBox lblPrice = grdOrders_FinalOrder.Rows[i].FindControl("lblPrice") as TextBox;

                    tblOrderDetail orderDetail = new tblOrderDetail();
                    orderDetail.GetOrderDetailForUpdate(ViewState["OrderUpdate"].ToString(), Convert.ToInt32(ViewState["OrderID"]), Convert.ToInt32(hidProductID.Value));
                    if (orderDetail.RowCount > 0)
                    {
                        orderDetail.FkProductPackageID = Convert.ToInt32(ddlPacking.SelectedValue);
                        if (lblPrice.Text != "" && lblPrice.Text != "00,00" && lblPrice.Text != "0")
                            orderDetail.NewPrice = commonMethods.ChangeToUS(lblPrice.Text);
                        else
                            orderDetail.ProudctPrice = commonMethods.ChangeToUS(LabelPrice.Text);

                        orderDetail.Vat = Convert.ToInt32(lblVat.Text.Replace("%", ""));
                        orderDetail.AfterVat = commonMethods.ChangeToUS((lblAfterVat.Text.Replace("€", "")));
                        orderDetail.Quantity = Convert.ToInt32(txtQt.Text);
                        orderDetail.Subtotal = commonMethods.ChangeToUS(lblSubtotal.Text);
                        orderDetail.FinalSubtotal = commonMethods.ChangeToUS(lblFinalSubtotal.Text);
                        orderDetail.SubtotalDifference = commonMethods.ChangeToUS(lblFinalDifference.Text.Replace("+", "").Replace("-", "").Replace(" ", ""));
                        orderDetail.DModifiedDate = DateTime.Now;
                        orderDetail.Save();
                        tblBaseOrder b_Allow = new tblBaseOrder();
                        b_Allow.getBaseOrder(ViewState["OrderUpdate"].ToString());
                        if (b_Allow.RowCount > 0)
                        {
                            b_Allow.AllowEdit = false;
                            b_Allow.Save();
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "savingMessage", "$(function(){RecordSaved();});", true);
                    }

                    if (lblPrice.Text != "" && lblPrice.Text != "00,00" && lblPrice.Text != "0")
                    {
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
                                    else
                                    {
                                        tblSupplierProductPrices sp = new tblSupplierProductPrices();
                                        sp.AddNew();
                                        sp.FkSupplierID = Convert.ToInt32(hidSupplierID.Value);
                                        sp.FkProductPackingQuantityRelID = rel.PkProductPackingQuantityRelID;
                                        sp.Price = commonMethods.ChangeToUS(lblPrice.Text);
                                        sp.DModifiedDate = DateTime.Now;
                                        sp.DCreatedDate = DateTime.Now;
                                        sp.Save();
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
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    #region Update Order
    protected void grdOrdersHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument != null)
            {
                string orderid = e.CommandArgument.ToString();
                switch (e.CommandName.ToLower())
                {
                    case "order":
                        ViewState["OrderUpdate"] = orderid;
                        Session["orderid"] = orderid;
                        LinkButton btn = (LinkButton)e.CommandSource as LinkButton;
                        lblOrderStatus_For_Final_Orders.Text = "Order of " + ((Label)btn.FindControl("lblOrderDate")).Text;
                        tblOrders o = new tblOrders();
                        o.getOrderCash(orderid);
                        if (o.RowCount > 0)
                        {
                            if (o.GetColumn("cashamount").ToString() != "")
                                txtCash.Text = commonMethods.ChangetToUK(o.GetColumn("cashamount").ToString());
                            else
                                txtCash.Text = "00,00";
                        }
                        if (txtCash.Text == "")
                            txtCash.Text = "00,00";

                        NewOrder();
                        //FinalOrder();
                        break;


                    case "cash":
                        ImageButton button = (ImageButton)e.CommandSource;
                        TextBox txtcash = button.FindControl("txtCash") as TextBox;
                        DropDownList ddlusers = button.FindControl("ddlUsers") as DropDownList;
                        LinkButton lnkOrderIDUpdate = button.FindControl("lnkOrderIDUpdate") as LinkButton;
                        tblBaseOrder b = new tblBaseOrder();
                        b.LoadByPrimaryKey(Convert.ToInt32(orderid));
                        if (b.RowCount > 0)
                        {
                            if (txtcash.Text != "00,00" && txtcash.Text != "0" && txtcash.Text != "")
                            {
                                int rid = UserID;
                                b.CashAmount = commonMethods.ChangeToUS(txtcash.Text);
                                if (ddlusers.SelectedValue == "0")
                                    b.OrderReceivedByUser = UserID;
                                else
                                {
                                    b.OrderReceivedByUser = Convert.ToInt32(ddlusers.SelectedValue);
                                    rid = Convert.ToInt32(ddlusers.SelectedValue);
                                }
                                b.Save();

                                #region Internal Message
                                string url = string.Empty;
                                tblUserInBox userIn = new tblUserInBox();
                                // For Current Manger
                                userIn.AddNew();
                                userIn.FkFromUserID = UserID;
                                userIn.FkToUserID = rid;
                                userIn.SSubject = "Order Receiving";
                                if (ddlusers.SelectedValue != "0")
                                {
                                    tblUserAccessLevel uaccess = new tblUserAccessLevel();
                                    uaccess.LoadAccessLevel(Convert.ToInt32(ddlusers.SelectedValue));
                                    if (uaccess.RowCount > 0)
                                    {
                                        if (Convert.ToInt32(uaccess.GetColumn("AccessLevel").ToString()) == 1)
                                        {
                                            url = " <a href=\'http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/bms/Users/orderreceived.aspx?orderid=" + lnkOrderIDUpdate.CommandArgument + "&userid=" + ddlusers.SelectedValue + "\'>Click Here to go to order receiving page.</a>";
                                        }
                                        else if (Convert.ToInt32(uaccess.GetColumn("AccessLevel").ToString()) == 3)
                                        {
                                            url = " <a href=\'http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/bms/ECUser/orderreceived.aspx?orderid=" + lnkOrderIDUpdate.CommandArgument + "&userid=" + ddlusers.SelectedValue + "\'>Click Here to go to order receiving page.</a>";
                                        }
                                    }
                                }

                                userIn.SMessage = url;
                                userIn.DReceivedDate = DateTime.Now;
                                userIn.BIsread = false;
                                userIn.Save();
                                userIn.FlushData();


                                tblUserSentBox userOut = new tblUserSentBox();

                                //For Current Manager
                                userOut.AddNew();
                                userOut.FkFromUserID = UserID;
                                userOut.FkToUserID = rid;
                                userOut.SSubject = "Order Receiving";
                                if (ddlusers.SelectedValue != "0")
                                {
                                    tblUserAccessLevel uaccess = new tblUserAccessLevel();
                                    uaccess.LoadAccessLevel(Convert.ToInt32(ddlusers.SelectedValue));
                                    if (uaccess.RowCount > 0)
                                    {
                                        if (Convert.ToInt32(uaccess.GetColumn("AccessLevel").ToString()) == 1)
                                        {
                                            url = " <a href=\'http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/bms/Users/orderreceived.aspx?orderid=" + lnkOrderIDUpdate.CommandArgument + "&userid=" + ddlusers.SelectedValue + "\'>Click Here to go to order receiving page.</a>";
                                        }
                                        else if (Convert.ToInt32(uaccess.GetColumn("AccessLevel").ToString()) == 3)
                                        {
                                            url = " <a href=\'http://" + Request.ServerVariables["HTTP_HOST"].ToString() + "/bms/ECUser/orderreceived.aspx?orderid=" + lnkOrderIDUpdate.CommandArgument + "&userid=" + ddlusers.SelectedValue + "\'>Click Here to go to order receiving page.</a>";
                                        }
                                    }
                                }
                                userOut.SMessage = url;
                                userOut.DSentDate = DateTime.Now;
                                userOut.Save();
                                userOut.FlushData();

                                #endregion
                                lblReceivingOrder.Text = "Record Save Successfully.";
                                lblReceivingOrder.Style.Add("color", "Green");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ReceivedRecordSaved();});", true);
                            }
                            else
                            {
                                try
                                {
                                    if (b.CashAmount != null && b.CashAmount != 0)
                                    {
                                        lblReceivingOrder.Text = "Cash amount is removed.";
                                        lblReceivingOrder.Style.Add("color", "Green");
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ReceivedRecordSaved();});", true);
                                        b.CashAmount = 0;
                                        b.OrderReceivedByUser = UserID;
                                        b.Save();
                                    }
                                }
                                catch (InvalidCastException ex)
                                {
                                    lblReceivingOrder.Text = "Cash amount is required.";
                                    lblReceivingOrder.Style.Add("color", "Red");
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "this", "$(function(){ReceivedRecordSaved();});", true);
                                    b.CashAmount = 0;
                                    b.OrderReceivedByUser = UserID;
                                    b.Save();
                                }
                            }
                            GetAllOrders();
                        }

                        break;

                }
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

    protected void txtAmountPaid_TextChanged(object sender, EventArgs e)
    {
        TextBox txtAmountPaid = ((TextBox)sender).FindControl("txtAmountPaid") as TextBox;
        Label lblOrderBalance = ((TextBox)sender).FindControl("lblOrderBalance") as Label;
        Label lblNewBalance = ((TextBox)sender).FindControl("lblNewBalance") as Label;
        HtmlContainerControl divNewBalance = ((TextBox)sender).FindControl("divNewBalance") as HtmlContainerControl;
        if (txtAmountPaid.Text != "")
        {
            double orderBalance = commonMethods.ChangeToUS(lblOrderBalance.Text) - commonMethods.ChangeToUS(txtAmountPaid.Text);


            if (orderBalance > 0)
            {
                divNewBalance.Style.Add("background", "url(../images/textbox_small_red.png) no-repeat;");
                divNewBalance.Style.Add("color", "white");
                lblNewBalance.Style.Add("color", "white");
            }
            else if (orderBalance < 0)
            {
                divNewBalance.Style.Add("background", "url(../images/textbox_small_green.png) no-repeat;");
                divNewBalance.Style.Add("color", "white");
                lblNewBalance.Style.Add("color", "white");
            }
            else
            {

            }

            lblNewBalance.Text = commonMethods.ChangetToUK(orderBalance.ToString("N"));
            double paid = 0.0;
            double estimated = 0.0;
            double diff_inter = 0.0;
            for (int i = 0; i < grdSuppliers.Rows.Count; i++)
            {
                TextBox txtAmountPaid_Internal = grdSuppliers.Rows[i].FindControl("txtAmountPaid") as TextBox;
                paid += commonMethods.ChangeToUS(txtAmountPaid_Internal.Text);
            }
            estimated = commonMethods.ChangeToUS(txtGrandEstimated.Text);

            diff_inter = paid - estimated;


            //txtGrandFinal.Text = commonMethods.ChangetToUK(paid.ToString("N"));
            txtPaid.Text = commonMethods.ChangetToUK(paid.ToString("N"));

            txtLeft.Text = commonMethods.ChangetToUK((commonMethods.ChangeToUS(txtCash.Text) - commonMethods.ChangeToUS(txtPaid.Text)).ToString("N"));

            //txtGrandDifference.Text = commonMethods.ChangetToUK(diff_inter.ToString("N"));  // Commented By Rehan
            SetTotalAmount();
        }
        upnlSuppliers.Update();
        upnlGrand.Update();
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
    protected void dlDeliveryTime_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblTime = e.Item.FindControl("lblTime") as Label;
            lblTime.Text = DataBinder.GetPropertyValue(e.Item.DataItem, "dTime").ToString();
            lblTime.Attributes.Add("onclick", "javascript:setTime('" + textboxid + "','" + lblTime.Text + "');");

        }
    }
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

}
