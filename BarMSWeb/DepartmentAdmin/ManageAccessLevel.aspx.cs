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
public partial class DepartmentAdmin_ManageAccessLevel : System.Web.UI.Page
{
    int DepartmentID;
    int UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            DepartmentID = user.DepartmentID;
            UserID = user.UserID;
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

            LoadDropDowns();
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
            tblDepartments departments = new tblDepartments();
            departments.LoadByPrimaryKey(DepartmentID);
            if (departments.RowCount > 0)
                lblDepartmentName.Text = "Department :" + departments.SDepartmentName;
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
        user.LoadDepartmentUsersForDepartmentAdmin(DepartmentID);
        commonMethods.FillDropDownList(ddlUsers, user.DefaultView, "FullName", "pkUserID");
        AccessLevel.LoadAll();
        commonMethods.FillDropDownList(ddlAccessLevel, AccessLevel.DefaultView, "sAccessLevel", "pkAccessLevelID");
        ddlAccessLevel.Items.RemoveAt(ddlAccessLevel.Items.Count - 1);
        ddlAccessLevel.Items.RemoveAt(ddlAccessLevel.Items.Count - 1);

    }
    protected void grdAccessLevels_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
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
                    if (Accesss.FkAccessLevelID == 4)
                    {
                        tblDepartments departments = new tblDepartments();
                        departments.LoadByPrimaryKey(DepartmentID);
                        if (departments.RowCount > 0)
                        {
                            departments.FkAccountManagerUserID = 0;
                            departments.DModifiedDate = DateTime.Now;
                            departments.Save();
                        }
                    }
                    tblUserAccessLevel AccessLevel = new tblUserAccessLevel();
                    int levelNo = Accesss.FkAccessLevelID;
                    AccessLevel.CheckAccessAlreadyExist(levelNo - 1, Accesss.FkUserID);
                    if (AccessLevel.RowCount > 0)
                    {
                        Accesss.MarkAsDeleted();
                        Accesss.Save();
                    }
                    else
                    {
                        if (levelNo == 3)
                        {
                            tblUserAccessLevel checkAnyManagerExists = new tblUserAccessLevel();
                            checkAnyManagerExists.IsManagerExist(DepartmentID);
                            if (checkAnyManagerExists.RowCount > 0)
                            {
                                Accesss.MarkAsDeleted();
                                Accesss.Save();
                            }
                        }
                        else
                        {

                            Accesss.FkAccessLevelID = levelNo - 1;
                            Accesss.DModifiedDate = DateTime.Now;
                            Accesss.Save();
                        }
                    }

                }
                BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
                break;

        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblUserAccessLevel UAccess = new tblUserAccessLevel();
        UAccess.CheckAccessAlreadyExist(Convert.ToInt32(ddlAccessLevel.SelectedValue), Convert.ToInt32(ddlUsers.SelectedValue));
        if (UAccess.RowCount > 0)
        {
            lblError.Visible = true;
            lblError.Text = " Access Level already Exist";
            return;
        }
        else
        {
            UAccess.FlushData();
            UAccess.AddNew();
            UAccess.FkUserID = Convert.ToInt32(ddlUsers.SelectedValue);
            UAccess.FkAccessLevelID = Convert.ToInt32(ddlAccessLevel.SelectedValue);
            UAccess.DCreateDate = DateTime.Now;
            UAccess.DModifiedDate = DateTime.Now;
            UAccess.Save();
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
        }
        

        /*
        tblDepartments department = new tblDepartments();
        if (ddlAccessLevel.SelectedValue == "2")
        {
            department.CheckDepartmentHasAdmin(DepartmentID);
            if (department.RowCount > 0)
            {
                lblError.Visible = true;
                lblError.Text = " Department already has Manager";
                return;
            }
        }
        else if (ddlAccessLevel.SelectedValue == "4")
        {
            department.CheckDepartmentHasAccountManager(DepartmentID);
            if (department.RowCount > 0)
            {
                lblError.Visible = true;
                lblError.Text = " Department already has Account Manager";
                return;
            }
        }
        tblUserAccessLevel UAccess = new tblUserAccessLevel();
        UAccess.CheckAccessAlreadyExist(Convert.ToInt32(ddlAccessLevel.SelectedValue), Convert.ToInt32(ddlUsers.SelectedValue));
        if (UAccess.RowCount > 0)
        {
            lblError.Visible = true;
            lblError.Text = " Access Level already Exist";
            return;
        }
        else
        {

            if (ddlAccessLevel.SelectedValue == "3")
            {
                
            }
            tblUserAccessLevel checkAnyManagerExists = new tblUserAccessLevel();
            checkAnyManagerExists.IsAccountManagerExist(DepartmentID);
            if (checkAnyManagerExists.RowCount > 0)
            {
                lblError.Visible = true;
                lblError.Text = " Department already has Account Manager";
                return;
            }
            UAccess.AddNew();
            UAccess.FkUserID = Convert.ToInt32(ddlUsers.SelectedValue);
            UAccess.FkAccessLevelID = Convert.ToInt32(ddlAccessLevel.SelectedValue);
            UAccess.DCreateDate = DateTime.Now;
            UAccess.DModifiedDate = DateTime.Now;
            UAccess.Save();
            if (Convert.ToInt32(ddlAccessLevel.SelectedValue) == 4)
            {
                department.FlushData();
                department.LoadByPrimaryKey(DepartmentID);
                if (department.RowCount > 0)
                {
                    department.FkAccountManagerUserID = Convert.ToInt32(ddlUsers.SelectedValue);
                    department.DModifiedDate = DateTime.Now;
                    department.Save();
                }
            }
            BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
        }
      */
    }
    protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(Convert.ToInt32(ddlUsers.SelectedValue));
    }

}
