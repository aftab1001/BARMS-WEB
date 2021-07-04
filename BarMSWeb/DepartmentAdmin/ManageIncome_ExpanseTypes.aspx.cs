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
public partial class Admin_ManageIncome_ExpanseTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            if (user.AccessLevel != 5)
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
            BindIncomeGrid();
            BindExpansGrid();
        }
    }
    private void BindIncomeGrid()
    {
        tblIncomTypes incomType = new tblIncomTypes();
        incomType.LoadAll();
        grdIncomeTypes.DataSource = incomType.DefaultView;
        grdIncomeTypes.DataBind();
    }
    protected void grdIncomeTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgBtnActive = (ImageButton)e.Row.FindControl("imgBtnActive");
                if (Convert.ToBoolean(drv["bIsActive"]))
                {
                    imgBtnActive.ImageUrl = "~/Images/activate_icon.gif";
                    imgBtnActive.ToolTip = "Deactivate";
                }
                else
                {
                    imgBtnActive.ImageUrl = "~/Images/close.png";
                    imgBtnActive.ToolTip = "Activate";
                }

                HtmlGenericControl divDescription = e.Row.FindControl("divDescription") as HtmlGenericControl;
                divDescription.InnerHtml = drv["Description"].ToString();
                //ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                //Label lblAccess = (Label)e.Row.FindControl("lblAccess");
                //lblAccess.Text = drv["sAccessLevel"].ToString();
                //Label lblDate = (Label)e.Row.FindControl("lblDate");
                //lblDate.Text = drv["dCreateDate"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdIncomeTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int incomID;
        tblIncomTypes incomType = new tblIncomTypes();
        switch (e.CommandName)
        {


            //case "Del":
            //    incomID = Convert.ToInt32(e.CommandArgument);

            //    incomType.LoadByPrimaryKey(incomID);

            //    tblIncome incom = new tblIncome();
            //    incom.CheckIncomTypeInUse(incomType.PkIncomeTypeID);
            //    if (incom.RowCount > 0)
            //    {
            //        lblError.Visible = true;
            //        lblError.Text = "Income Type is in use";
            //    }
            //    else
            //    {
            //        incomType.MarkAsDeleted();
            //        incomType.Save();
            //        BindIncomeGrid();
            //    }
            //    break;

            case "Change":
                incomID = Convert.ToInt32(e.CommandArgument);

                incomType.LoadByPrimaryKey(incomID);
                txtIncomeType.Text = incomType.SIncomType;
                txtIncomeDescription.Text = incomType.Description;
                btnSave.Visible = false;
                btnEdit.Visible = true;
                lbluser.Text = "Edit Income Type";
                hdnID.Value = incomID.ToString();
                btnAdd.Visible = false;
                divIncome.Visible = true;
                lblError.Text = "";
                lblError.Visible = false;

                break;
            case "active":
                incomID = Convert.ToInt32(e.CommandArgument);
                incomType.FlushData();
                incomType.LoadByPrimaryKey(incomID);
                if (incomType.RowCount > 0)
                {
                    if (incomType.BIsActive)
                        incomType.BIsActive = false;
                    else if (!incomType.BIsActive)
                        incomType.BIsActive = true;
                    incomType.DModifiedDate = DateTime.Now;
                    incomType.Save();
                }
                BindIncomeGrid();
                break;

        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblIncomTypes incomType = new tblIncomTypes();
        incomType.CheckIncomeType(txtIncomeType.Text);
        if (incomType.RowCount > 0)
        {

            lblError.Visible = true;
            lblError.Text = "Income Type already Exist";

        }
        else
        {
            incomType.FlushData();
            incomType.AddNew();
            incomType.s_SIncomType = txtIncomeType.Text;
            incomType.BIsActive = true;
            incomType.Description = txtIncomeDescription.Text;
            incomType.DCreateDate = DateTime.Now;
            incomType.DModifiedDate = DateTime.Now;
            incomType.Save();

            btnAdd.Visible = true;
            divIncome.Visible = false;
            BindIncomeGrid();
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        tblIncomTypes incomType = new tblIncomTypes();
        incomType.CheckIncomeType(txtIncomeType.Text);
        if (incomType.RowCount > 0)
        {

            lblError.Visible = true;
            lblError.Text = "Income Type already Exist";
            incomType.FlushData();
            incomType.LoadByPrimaryKey(Convert.ToInt32(hdnID.Value));
            incomType.Description = txtIncomeDescription.Text;
            incomType.DModifiedDate = DateTime.Now;
            incomType.Save();
            txtIncomeType.Text = "";

            btnAdd.Visible = true;
            divIncome.Visible = false;

        }
        else
        {
            incomType.FlushData();
            incomType.LoadByPrimaryKey(Convert.ToInt32(hdnID.Value));
            incomType.s_SIncomType = txtIncomeType.Text;
            incomType.Description = txtIncomeDescription.Text;
            incomType.DModifiedDate = DateTime.Now;
            incomType.Save();
            txtIncomeType.Text = "";

            btnAdd.Visible = true;
            divIncome.Visible = false;
            btnSave.Visible = true;
            btnEdit.Visible = false;
            hdnID.Value = "";
            lbluser.Text = "Add Income Type";

        }
        BindIncomeGrid();

    }
    //grdExpansTypes
    private void BindExpansGrid()
    {
        tblExpanseCategory ExpansCat = new tblExpanseCategory();
        ExpansCat.LoadAll();
        grdExpansTypes.DataSource = ExpansCat.DefaultView;
        grdExpansTypes.DataBind();
    }
    protected void grdExpansTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                //ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                ImageButton imgBtnActive = (ImageButton)e.Row.FindControl("imgBtnActive");
                if (Convert.ToBoolean(drv["bIsActive"]))
                {
                    imgBtnActive.ImageUrl = "~/Images/activate_icon.gif";
                    imgBtnActive.ToolTip = "Deactivate";
                }
                else
                {
                    imgBtnActive.ImageUrl = "~/Images/close.png";
                    imgBtnActive.ToolTip = "Activate";
                }

                HtmlGenericControl divDescription = e.Row.FindControl("divDescription") as HtmlGenericControl;
                divDescription.InnerHtml = drv["Description"].ToString();
                //Label lblAccess = (Label)e.Row.FindControl("lblAccess");
                //lblAccess.Text = drv["sAccessLevel"].ToString();
                //Label lblDate = (Label)e.Row.FindControl("lblDate");
                //lblDate.Text = drv["dCreateDate"].ToString();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdExpansTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int ExpansCatID;
        tblExpanseCategory ExpansCat = new tblExpanseCategory();
        switch (e.CommandName)
        {


            //case "Del":
            //    ExpansCatID = Convert.ToInt32(e.CommandArgument);

            //    ExpansCat.LoadByPrimaryKey(ExpansCatID);

            //    tblExpanses Expans = new tblExpanses();
            //    Expans.CheckExpansCatInUse(ExpansCat.PkExpanseCategoryID);
            //    if (Expans.RowCount > 0)
            //    {
            //        lblError1.Visible = true;
            //        lblError1.Text = "Expanse Type is in use";
            //    }
            //    else
            //    {



            //        ExpansCat.MarkAsDeleted();
            //        ExpansCat.Save();
            //        BindExpansGrid();
            //    }
            //    break;

            case "Change":
                ExpansCatID = Convert.ToInt32(e.CommandArgument);

                ExpansCat.LoadByPrimaryKey(ExpansCatID);
                txtExpansType.Text = ExpansCat.SExpanseCategory;
                txtExpenseDescription.Text = ExpansCat.Description;
                btnSave1.Visible = false;
                btnEdit1.Visible = true;
                lblExpanse.Text = "Edit Expanse Type";
                hdnID1.Value = ExpansCatID.ToString();
                btnAddExpense.Visible = false;
                btnEdit1.Visible = true;
                divExpense.Visible = true;
                lblError1.Text = "";
                lblError1.Visible = false;

                break;
            case "active":
                ExpansCatID = Convert.ToInt32(e.CommandArgument);
                ExpansCat.LoadByPrimaryKey(ExpansCatID);
                if (ExpansCat.RowCount > 0)
                {
                    if (ExpansCat.BIsActive)
                        ExpansCat.BIsActive = false;
                    else if (!ExpansCat.BIsActive)
                        ExpansCat.BIsActive = true;
                    ExpansCat.DModifiedDate = DateTime.Now;
                    ExpansCat.Save();
                    BindExpansGrid();
                }
                break;

        }
    }
    protected void btnSave1_Click(object sender, ImageClickEventArgs e)
    {
        tblExpanseCategory ExpansCat = new tblExpanseCategory();
        ExpansCat.CheckExpanseCategory(txtExpansType.Text);
        if (ExpansCat.RowCount > 0)
        {

            lblError1.Visible = true;
            lblError1.Text = "Expanse Type already Exist";

        }
        else
        {
            ExpansCat.FlushData();
            ExpansCat.AddNew();
            ExpansCat.s_SExpanseCategory = txtExpansType.Text;
            ExpansCat.BIsActive = true;
            ExpansCat.Description = txtExpenseDescription.Text;
            ExpansCat.DCreateDate = DateTime.Now;
            ExpansCat.DModifiedDate = DateTime.Now;
            ExpansCat.Save();
            btnAddExpense.Visible = true;
            divExpense.Visible = false;
        }
        BindExpansGrid();
    }
    protected void btnEdit1_Click(object sender, ImageClickEventArgs e)
    {
        tblExpanseCategory ExpansCat = new tblExpanseCategory();
        ExpansCat.CheckExpanseCategory(txtExpansType.Text);
        if (ExpansCat.RowCount > 0)
        {

            lblError1.Visible = true;
            lblError1.Text = "Expanse Type already Exist";

            ExpansCat.FlushData();
            ExpansCat.LoadByPrimaryKey(Convert.ToInt32(hdnID1.Value));
            ExpansCat.Description = txtExpenseDescription.Text;
            ExpansCat.DModifiedDate = DateTime.Now;
            ExpansCat.Save();
            txtExpansType.Text = "";
            txtExpenseDescription.Text = "";

        }
        else
        {
            ExpansCat.FlushData();
            ExpansCat.LoadByPrimaryKey(Convert.ToInt32(hdnID1.Value));
            ExpansCat.s_SExpanseCategory = txtExpansType.Text;
            ExpansCat.Description = txtExpenseDescription.Text;
            ExpansCat.DModifiedDate = DateTime.Now;
            ExpansCat.Save();
            txtExpansType.Text = "";
            txtExpenseDescription.Text = "";

            btnSave1.Visible = true;
            btnEdit1.Visible = false;
            hdnID1.Value = "";
            lblExpanse.Text = "Add Expanse Type";

        }
        btnAddExpense.Visible = true;
        divExpense.Visible = false;
        BindExpansGrid();

    }



    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        btnAdd.Visible = false;
        divIncome.Visible = true;
        btnSave.Visible = true;
        btnEdit.Visible = false;
        lbluser.Text = "Add Income Type";
        lblError.Text = "";
        lblError.Visible = false;
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        btnAdd.Visible = true;
        divIncome.Visible = false;
    }
    protected void btnAddExpense_Click(object sender, ImageClickEventArgs e)
    {
        btnAddExpense.Visible = false;
        divExpense.Visible = true;
        btnSave1.Visible = true;
        btnEdit1.Visible = false;
        lblExpanse.Text = "Add Expanse Type";
        lblError1.Text = "";
        lblError1.Visible = false;
    }
    protected void btnCancelExpense_Click(object sender, ImageClickEventArgs e)
    {
        btnAddExpense.Visible = true;
        divExpense.Visible = false;
    }
}
