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

public partial class Admin_ManageDepartments : System.Web.UI.Page
{
    static int ToUserID = 0;
    int Userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            Userid = user.UserID;
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
            BindDepartmentGrid();
        }
    }
    private void BindDepartmentGrid()
    {
        tblDepartments department = new tblDepartments();
        department.GetDepartmentsWithDepartmentAdminName();
        grdDepartment.DataSource = department.DefaultView;
        grdDepartment.DataBind();
    }
    protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    imgBtnActive.ImageUrl = "~/Images/activate_icon.gif";
                else
                    imgBtnActive.ImageUrl = "~/Images/close.png";

                LinkButton lnkDepartmentAdmin = e.Row.FindControl("lnkName") as LinkButton;
                lnkDepartmentAdmin.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lnkDepartmentAdmin.Text.Length > 18)
                {
                    lnkDepartmentAdmin.Text = lnkDepartmentAdmin.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lnkDepartmentAdmin.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lnkDepartmentAdmin.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int DepartmentID;
        switch (e.CommandName)
        {


            case "Del":
                DepartmentID = Convert.ToInt32(e.CommandArgument);
                tblUserDepartment UDepart = new tblUserDepartment();
                UDepart.CheckDepartmentInUse(DepartmentID);
                if (UDepart.RowCount > 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Dapartment is in use";
                }
                else
                {


                    tblDepartments department = new tblDepartments();
                    department.LoadByPrimaryKey(DepartmentID);
                    department.MarkAsDeleted();
                    department.Save();
                    BindDepartmentGrid();
                }
                break;

            case "Change":
                DepartmentID = Convert.ToInt32(e.CommandArgument);
                tblDepartments depart = new tblDepartments();
                depart.LoadByPrimaryKey(DepartmentID);
                txtDepartment.Text = depart.SDepartmentName;
                txtPhone.Text = depart.SPhone;
                txtAddress.Text = depart.SAddress;
                btnSave.Visible = false;
                btnEdit.Visible = true;
                lbluser.Text = "Edit Department";
                hdnID.Value = DepartmentID.ToString();
                btnAddDepartment.Visible = false;
                divDepartment.Visible = true;

                break;
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(Userid);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();
                break;
            case "active":
                int didActive = Convert.ToInt32(e.CommandArgument);
                tblDepartments departAcive = new tblDepartments();
                departAcive.LoadByPrimaryKey(didActive);
                if (departAcive.RowCount > 0)
                {
                    if (departAcive.BIsActive)
                        departAcive.BIsActive = false;
                    else if (!departAcive.BIsActive)
                        departAcive.BIsActive = true;
                    departAcive.DModifiedDate = DateTime.Now;
                    departAcive.Save();

                    if (!departAcive.BIsActive)
                    {
                        tblUsers usersActive = new tblUsers();
                        usersActive.LoadUsersActivatedByHim(didActive, Userid);
                        if (usersActive.RowCount > 0)
                        {
                            for (int i = 0; i < usersActive.RowCount; i++)
                            {
                                usersActive.BActiveByAdmin = false;
                                usersActive.DModifiedDate = DateTime.Now;
                                usersActive.Save();
                                usersActive.MoveNext();
                            }

                        }
                    }
                    BindDepartmentGrid();
                }
                break;

        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblDepartments department = new tblDepartments();
        department.CheckDepartmentName(txtDepartment.Text);
        if (department.RowCount > 0)
        {

            lblError.Visible = true;
            lblError.Text = "Dapartment already Exist";

        }
        else
        {
            if (txtDepartment.Text != "")
            {
                department.FlushData();
                department.AddNew();
                department.s_SDepartmentName = txtDepartment.Text;
                department.DCreateDate = DateTime.Now;
                department.DModifiedDate = DateTime.Now;
                department.SPhone = txtPhone.Text;
                department.SAddress = txtAddress.Text;
                department.WeekStartTime = "20:30";
                department.WeekEndTime = "04:30";
                department.BIsActive = true;
                department.WeekendStartTime = "20:00";
                department.WeekendEndTime = "04:00";
                btnAddDepartment.Visible = true;
                divDepartment.Visible = false;
                department.Save();
                BindDepartmentGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Bonus", "alert('Please Enter Department Name!')", true);
            }
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        tblDepartments department = new tblDepartments();
        department.CheckDepartmentName(txtDepartment.Text);
        if (department.RowCount > 0)
        {

            lblError.Visible = true;
            lblError.Text = "Same Dapartment name already Exist";
            department.FlushData();
            department.LoadByPrimaryKey(Convert.ToInt32(hdnID.Value));
            department.s_SPhone = txtPhone.Text;
            department.s_SAddress = txtAddress.Text;
            //department.DCreateDate = DateTime.Now;
            department.DModifiedDate = DateTime.Now;
            department.Save();
            txtDepartment.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            btnSave.Visible = true;
            btnEdit.Visible = false;
            hdnID.Value = "";
            lbluser.Text = "Add Department";
            btnAddDepartment.Visible = true;
            divDepartment.Visible = false;

            BindDepartmentGrid();

        }
        else
        {
            if (txtDepartment.Text != "")
            {
                department.FlushData();
                department.LoadByPrimaryKey(Convert.ToInt32(hdnID.Value));
                department.s_SDepartmentName = txtDepartment.Text;
                department.s_SPhone = txtPhone.Text;
                department.s_SAddress = txtAddress.Text;
                //department.DCreateDate = DateTime.Now;
                department.DModifiedDate = DateTime.Now;
                department.Save();
                txtDepartment.Text = "";
                txtPhone.Text = "";
                txtAddress.Text = "";
                btnSave.Visible = true;
                btnEdit.Visible = false;
                hdnID.Value = "";
                lbluser.Text = "Add Department";
                btnAddDepartment.Visible = true;
                divDepartment.Visible = false;

                BindDepartmentGrid();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Bonus", "alert('Please Enter Department Name!')", true);
            }
        }

    }
    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblUserInBox userIn = new tblUserInBox();
            userIn.AddNew();
            userIn.FkFromUserID = Userid;
            userIn.FkToUserID = ToUserID;
            userIn.SSubject = txtSubject.Text;
            userIn.SMessage = txtMessage.Text;
            userIn.DReceivedDate = DateTime.Now;
            userIn.BIsread = false;
            userIn.Save();

            tblUserSentBox userOut = new tblUserSentBox();
            userOut.AddNew();
            userOut.FkFromUserID = Userid;
            userOut.FkToUserID = ToUserID;
            userOut.SSubject = txtSubject.Text;
            userOut.SMessage = txtMessage.Text;
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            ModalPopupExtender1.Hide();
            BindDepartmentGrid();
        }
        catch (Exception ex)
        {

        }
    }
   
    protected void btnAddDepartment_Click(object sender, ImageClickEventArgs e)
    {
        btnAddDepartment.Visible = false;
        divDepartment.Visible = true;
        btnEdit.Visible = false;
        btnSave.Visible = true;
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        btnAddDepartment.Visible = true;
        divDepartment.Visible = false;
    }
}
