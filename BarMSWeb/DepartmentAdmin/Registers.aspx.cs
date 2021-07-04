using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_Registers : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    static string regName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
            if (user.AccessLevel != 5)
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

        if (!Page.IsPostBack)
        {
            GetRegisters();
            GetAllVat();
        }
    }

    #region Registers
    private void GetRegisters()
    {
        tblRegisters regs = new tblRegisters();
        regs.GetAllRegisters();
        grdRegisters.DataSource = regs.DefaultView;
        grdRegisters.DataBind();
    }
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        btnAdd.Visible = false;
        divIncome.Visible = true;
        btnSave.Visible = true;
        btnEdit.Visible = false;

        txtRegisterName.Text = "";
        txtRegisterDescription.Text = "";

        lblMessage.Text = "";
        lblMessage.Visible = false;
        GetAllVat();
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblRegisters regs = new tblRegisters();

        regs.CheckRegisterNameInSaveMode(txtRegisterName.Text);
        if (regs.RowCount > 0)
        {
            string message = "Register already Exists!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
        }
        else
        {
            regs.FlushData();
            regs.AddNew();
            regs.RName = txtRegisterName.Text;
            regs.IsActive = true;
            regs.RDescription = txtRegisterDescription.Text;
            //regs.FkVatID = Convert.ToInt32(ddlVat.SelectedValue);
            regs.DCreatedDate = DateTime.Now;
            regs.DModifiedDate = DateTime.Now;
            regs.Save();

            btnAdd.Visible = true;
            divIncome.Visible = false;
            lblMessage.Text = "Successfully Added!";
            txtRegisterName.Text = "";
            txtRegisterDescription.Text = "";
            lblMessage.Visible = true;
            GetRegisters();
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int rid = Convert.ToInt32(hdnID.Value);
            tblRegisters regs = new tblRegisters();
            regs.CheckRegisterNameInEditMode(rid, txtRegisterName.Text);
            if (regs.RowCount > 0)
            {
                string message = "Register already Exists!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "$(function(){ alert('" + message + "'); });", true);
            }
            else
            {
                regs.FlushData();
                regs.LoadByPrimaryKey(rid);
                regs.RName = txtRegisterName.Text;
                regs.RDescription = txtRegisterDescription.Text;
                //regs.FkVatID = Convert.ToInt32(ddlVat.SelectedValue);
                regs.DModifiedDate = DateTime.Now;
                regs.Save();
                lblMessage.Text = "Successfully Updated!";
                lblMessage.Visible = true;
                txtRegisterName.Text = "";
                txtRegisterDescription.Text = "";
                btnAdd.Visible = true;
                divIncome.Visible = false;
                btnSave.Visible = true;
                btnEdit.Visible = false;
                hdnID.Value = "";
                GetRegisters();
            }
        }
        catch (Exception ex)
        { }


    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        btnAdd.Visible = true;
        divIncome.Visible = false;
    }
    protected void grdRegisters_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument != null)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                tblRegisters regs = new tblRegisters();
                switch (e.CommandName.ToLower())
                {
                    case "change":
                        regs.LoadByPrimaryKey(id);
                        if (regs.RowCount > 0)
                        {
                            txtRegisterName.Text = regs.RName;
                            regName = regs.RName;
                            txtRegisterDescription.Text = regs.RDescription;
                            //ddlVat.SelectedValue = regs.FkVatID.ToString();
                            hdnID.Value = "0";
                            hdnID.Value = regs.PkRegisterID.ToString();
                        }

                        btnSave.Visible = false;
                        btnAdd.Visible = false;

                        btnEdit.Visible = true;
                        divIncome.Visible = true;



                        break;
                    case "active":

                        regs.FlushData();
                        regs.LoadByPrimaryKey(id);
                        if (regs.RowCount > 0)
                        {
                            if (regs.IsActive)
                                regs.IsActive = false;
                            else if (!regs.IsActive)
                                regs.IsActive = true;
                            regs.DModifiedDate = DateTime.Now;
                            regs.Save();
                        }
                        GetRegisters();
                        break;
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void grdRegisters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgBtnActive = (ImageButton)e.Row.FindControl("imgBtnActive");
                if (Convert.ToBoolean(drv["isActive"]))
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
                divDescription.InnerHtml = drv["rDescription"].ToString();
                if (drv["rDescription"].ToString().Length > 65)
                {
                    divDescription.InnerHtml = drv["rDescription"].ToString().Substring(0, 65) + "...";
                    divDescription.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + drv["rDescription"].ToString() + "')");
                    divDescription.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdRegisters_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRegisters.PageIndex = e.NewPageIndex;
        GetRegisters();
    }

    #endregion

    #region Vat
    private void GetAllVat()
    {
        tblVAT vat = new tblVAT();
        vat.LoadAll();
        ddlVat.Items.Clear();
        if (vat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlVat, vat.DefaultView, "Vat", "PkVatID");
        }
        ddlVat.Items.Insert(0, new ListItem("Select Vat", "0"));
        grdVat.DataSource = vat.DefaultView;
        grdVat.DataBind();
    }

    protected void lnkSalesVat_Click(object sender, EventArgs e)
    {
        GetAllVat();
        mvMain.SetActiveView(vSalesVat);
    }
    protected void imgBtnSaveVat_Click(object sender, ImageClickEventArgs e)
    {
        if (txtVatAmount.Text != "")
        {
            tblVAT vat = new tblVAT();
            vat.AddNew();
            vat.Vat = txtVatAmount.Text.Replace("%","") + "%";
            vat.DModifiedDate = DateTime.Now;
            vat.DCreatedDate = DateTime.Now;
            vat.Save();
            GetAllVat();
            txtVatAmount.Text = "";

            trMessageVat.Visible = true;
            lblMessageVat.Text = "Successfully Added Vat Amount!";
            lblMessageVat.ForeColor = Color.Green;
            trVat.Visible = false;
        }
        else
        {
            trMessageVat.Visible = true;
            lblMessageVat.Text = "Plese Enter Vat Amount!";
            lblMessageVat.ForeColor = Color.Red;
        }
    }
    protected void imgBtnEditVat_Click(object sender, ImageClickEventArgs e)
    {
        if (txtVatAmount.Text != "")
        {
            int vid = Convert.ToInt32(hdnID.Value);

            tblVAT vat = new tblVAT();
            vat.LoadByPrimaryKey(vid);
            if (vat.RowCount > 0)
            {
                vat.Vat = txtVatAmount.Text.Replace("%", "") + "%";
                vat.DModifiedDate = DateTime.Now;
                vat.Save();
                trMessageVat.Visible = true;
                txtVatAmount.Text = "";
                lblMessageVat.Text = "Successfully Updated Vat Amount!";
                trMessageVat.Visible = true;

                lblMessageVat.ForeColor = Color.Green;
                
                hdnID.Value = "0";
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
            trMessageVat.Visible = true;
            lblMessageVat.Text = "Plese enter Vat Amount!";
            lblMessageVat.ForeColor = Color.Red;
        }

    }
    protected void imgBtnCancelVat_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnEditVat.Visible = false;
        imgBtnSaveVat.Visible = true;
        trMessageVat.Visible = false;
        trVat.Visible = false;
        trLineBeforeVat.Visible = true;
        trLineAfterVat.Visible = false;
    }
    protected void imgBtnAddVat_Click(object sender, ImageClickEventArgs e)
    {
        trMessageVat.Visible = false;
        trLineBeforeVat.Visible = false;
        trLineAfterVat.Visible = true;
        imgBtnSaveVat.Visible = true;
        trMessageVat.Visible = false;
        txtVatAmount.Text = "";
        trVat.Visible = true;
        imgBtnEditVat.Visible = false;
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
                        txtVatAmount.Text = vat.Vat.ToString().Replace("%","");
                        hdnID.Value = vat.PkVatID.ToString();

                        trMessageVat.Visible = false;
                        trLineBeforeVat.Visible = true;
                        trLineAfterVat.Visible = false;
                        imgBtnSaveVat.Visible = false;
                        imgBtnEditVat.Visible = true;
                        trMessageVat.Visible = false;
                        trVat.Visible = true;
                    }
                    break;
            }
        }
    }
    protected void grdVat_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    #endregion
    protected void lnkBackToRegisters_Click(object sender, EventArgs e)
    {
        mvMain.SetActiveView(vRegister);
    }
}
