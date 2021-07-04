using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_ContractDoc : System.Web.UI.Page
{
    int Userid;
    int Departmentid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            Departmentid = user.DepartmentID;
            Userid = user.UserID;
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
            LoadContract();
        }
    }
    private void LoadContract()
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadByPrimaryKey(Departmentid);
        if (departments.RowCount > 0)
        {
            if (departments.Contract != null && departments.Contract != "")
            {
                FreeTextBox2.Text = departments.Contract;
            }
            else
            {
                tblSettings setting = new tblSettings();
                setting.LoadAll();
                if (setting.RowCount > 0)
                {
                    FreeTextBox2.Text = setting.SContract;
                }
            }
        }
        else
        {
            tblSettings setting = new tblSettings();
            setting.LoadAll();
            if (setting.RowCount > 0)
            {
                FreeTextBox2.Text = setting.SContract;
            }
        }
    }
    protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadByPrimaryKey(Departmentid);
        if (departments.RowCount > 0)
        {
            departments.Contract = FreeTextBox2.Text;
            departments.DModifiedDate = DateTime.Now;
            departments.Save();
            lblMessage.Visible = true;
            lblMessage.Text = "Successfully Updated!";
        }
    }
    private void LoadSetting(tblSettings setting)
    {
        FreeTextBox2.Text = setting.SContract;
    }
    protected void btnRestore_Click(object sender, EventArgs e)
    {
       

    }
    protected void btnRestore_Click(object sender, ImageClickEventArgs e)
    {
        tblSettings setting = new tblSettings();
        setting.LoadAll();
        if (setting.RowCount > 0)
        {
            FreeTextBox2.Text = setting.SContract;
        }

    }
}
