using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class Managers_ManageSpecialties : System.Web.UI.Page
{
    int pkUserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            pkUserID = user.UserID;
            
            if (user.AccessLevel != 2)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }

            if (!IsPostBack)
            {
                BindSpecialities();
                
            }
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
    }
    private void BindSpecialities()
    {
        tblSpecialityType specialityTypes = new tblSpecialityType();
        specialityTypes.GetSpecialityTypesWithoutSeparator();
        grdSpecialities.DataSource = specialityTypes.DefaultView;
        grdSpecialities.DataBind();
    }
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblSpecialityType spType = new tblSpecialityType();
        if (ViewState["id"] != null)
        {

            spType.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"]));
            if (spType.RowCount > 0)
            {
                if (txtSpeciality.Text != "Separator")
                {
                    spType.SSpecialityName = txtSpeciality.Text;
                    spType.DModifiedDate = DateTime.Now;
                    spType.Save();
                    BindSpecialities();
                    lblMessage.Text = "Successfully Updated!";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Already Added!";
                    lblMessage.Visible = true;
                }
            }
        }
        else
        {
            if (txtSpeciality.Text != "Separator")
            {
                spType = new tblSpecialityType();
                spType.AddNew();
                spType.SSpecialityName = txtSpeciality.Text;
                spType.DModifiedDate = DateTime.Now;
                spType.DCreateDate = DateTime.Now;
                spType.BIsActive = true;
                spType.Special = false;
                spType.Save();
                BindSpecialities();
                lblMessage.Text = "Successfully Added!";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = "Already Added!";
                lblMessage.Visible = true;
            }

        }
        txtSpeciality.Text = "";
        imgBtnAddSpecialty.Visible = true;
        divSpecialty.Visible = false;
    }
    protected void grdSpecialities_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            tblSpecialityType spType = new tblSpecialityType();
            switch (e.CommandName)
            {
                case "edt":
                    lblMessage.Visible = false;
                    spType.FlushData();
                    spType.LoadByPrimaryKey(id);
                    if (spType.RowCount > 0)
                    {
                        txtSpeciality.Text = spType.SSpecialityName.ToString();
                        ViewState["id"] = spType.PkSpecialityTypeID;
                        imgBtnAddSpecialty.Visible = false;
                        divSpecialty.Visible = true;
                    }
                    break;
                case "active":
                    spType.FlushData();
                    spType.LoadByPrimaryKey(id);
                    if (spType.RowCount > 0)
                    {
                        if (spType.BIsActive)
                            spType.BIsActive = false;
                        else if (!spType.BIsActive)
                            spType.BIsActive = true;
                        spType.DModifiedDate = DateTime.Now;
                        spType.Save();
                        BindSpecialities();
                    }
                    break;
                case "special":
                    spType.FlushData();
                    spType.LoadByPrimaryKey(id);
                    if (spType.RowCount > 0)
                    {
                        if (spType.Special)
                            spType.Special = false;
                        else if (!spType.Special)
                            spType.Special = true;
                        spType.DModifiedDate = DateTime.Now;
                        spType.Save();
                        BindSpecialities();
                    }
                    break;
            }
        }
    }
    protected void grdSpecialities_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgBtnActive = e.Row.FindControl("imgBtnActive") as ImageButton;
            ImageButton imgBtnSpecial = e.Row.FindControl("imgBtnSpecial") as ImageButton;


            #region Showing ToolTip Activate/Deactivate any Specialty
            if (Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "bIsActive")))
            {
                imgBtnActive.ImageUrl = "../Images/activate_icon.gif";
                imgBtnActive.ToolTip = "Deactivate";
            }
            else
            {
                imgBtnActive.ImageUrl = "../Images/close.png";
                imgBtnActive.ToolTip = "Activate";
            }
            #endregion

            #region Showing ToolTip Activate/Deactivate Special Specialty
            if (Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "Special")))
            {
                imgBtnSpecial.ImageUrl = "../Images/activate_icon.gif";
                imgBtnSpecial.ToolTip = "Deactivate";
            }
            else
            {
                imgBtnSpecial.ImageUrl = "../Images/close.png";
                imgBtnSpecial.ToolTip = "Activate";
            }
            #endregion

        }
    }
    protected void imgBtnAddSpecialty_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["id"] = null;
        imgBtnAddSpecialty.Visible = false;
        divSpecialty.Visible = true;
        lblMessage.Visible = false;
    }
    protected void imgCancel_Click(object sender, ImageClickEventArgs e)
    {
        divSpecialty.Visible = false;
        imgBtnAddSpecialty.Visible = true;
    }
}
