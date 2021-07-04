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
public partial class Admin_ManageAccessLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            if (user.AccessLevel != 6)
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

            LoadDropDowns();
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
            lblDepartmentName.Text = "Department :" + ddlDepartments.SelectedItem.Text;
        }
    }
    private void BindGrid(int userID)
    {
        tblUserAccessLevel UAccess = new tblUserAccessLevel();
        UAccess.LoadUserAccessLevels(userID);
        grdAccessLevels.DataSource = UAccess.DefaultView;
        grdAccessLevels.DataBind();
    }

    private void LoadDropDowns()
    {
        tblUsers user = new tblUsers();
        tblAcessLevel AccessLevel = new tblAcessLevel();
        tblDepartments department = new tblDepartments();
        department.LoadAll();
        commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");

        user.LoadDepartmentUsers(Convert.ToInt32(ddlDepartments.SelectedValue));
        //if (user.RowCount > 0)
        //{
        commonMethods.FillDropDownList(ddlUsers, user.DefaultView, "FullName", "pkUserID");
        //}

        AccessLevel.LoadAll();
        commonMethods.FillDropDownList(ddlAccessLevel, AccessLevel.DefaultView, "sAccessLevel", "pkAccessLevelID");
        department.LoadAll();
        commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");

    }
    protected void grdAccessLevels_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

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
    protected void grdAccessLevels_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UserAccessLevel;
        switch (e.CommandName)
        {


            case "Del":
                UserAccessLevel = Convert.ToInt32(e.CommandArgument);
                tblUserAccessLevel Accesss = new tblUserAccessLevel();
                Accesss.LoadByPrimaryKey(UserAccessLevel);
                if (Accesss.FkAccessLevelID == 1)
                {
                    lblError.Visible = true;
                    lblError.Text = "User is Already at Minimum Access";
                }
                else
                {
                    tblUserAccessLevel AccessLevel = new tblUserAccessLevel();
                    int levelNo = Accesss.FkAccessLevelID - 1;
                    AccessLevel.CheckAccessAlreadyExist(levelNo, Accesss.FkUserID);
                    if (AccessLevel.RowCount > 0)
                    {
                        Accesss.MarkAsDeleted();
                        Accesss.Save();
                    }
                    else
                    {
                        Accesss.FkAccessLevelID = levelNo;
                        Accesss.DModifiedDate = DateTime.Now;
                        Accesss.Save();
                    }

                }
                BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
                break;

        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblDepartments department = new tblDepartments();
        
        tblUserAccessLevel UAccess = new tblUserAccessLevel();
        UAccess.IsDepartmentManagerExist(Convert.ToInt32(ddlDepartments.SelectedValue));
        if (UAccess.RowCount > 0)
        {
            tblUserAccessLevel access = new tblUserAccessLevel();
            access.LoadByPrimaryKey(Convert.ToInt32(UAccess.GetColumn("pkuseraccesslevel").ToString()));
            access.MarkAsDeleted();
            access.Save();

        }
        UAccess.FlushData();
        UAccess.AddNew();
        UAccess.FkUserID = Convert.ToInt32(ddlUsers.SelectedValue);
        UAccess.FkAccessLevelID = Convert.ToInt32(5);
        UAccess.DCreateDate = DateTime.Now;
        UAccess.DModifiedDate = DateTime.Now;
        UAccess.Save();
        BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));

    }
    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkAccess.Checked = false;
        BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
    }
    protected void ddlDepartments_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblUsers user = new tblUsers();
        user.LoadDepartmentUsers(Convert.ToInt32(ddlDepartments.SelectedValue));
        //if (user.RowCount > 0)
        //{
        commonMethods.FillDropDownList(ddlUsers, user.DefaultView, "FullName", "pkUserID");
        //}
        if (ddlUsers.SelectedValue != "")
        {
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
        }
        else
        {
            BindGrid(0);
        }
        lblDepartmentName.Text = "Department :" + ddlDepartments.SelectedItem.Text;
    }
}
