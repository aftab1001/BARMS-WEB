using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class Admin_ContractDoc : System.Web.UI.Page
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
            tblSettings setting = new tblSettings();
            setting.LoadAll();
            if (setting.RowCount > 0)
            {
                LoadSetting(setting);
                ViewState["ID"] = setting.PkSettingID;
            }
            else
            {
                ViewState["ID"] = null;
                lblMessage.Visible = false;
            }
        }
    }
    protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        tblSettings setting = new tblSettings();
        if (ViewState["ID"] != null)
        {
            setting.LoadByPrimaryKey(Convert.ToInt32(ViewState["ID"]));
            lblMessage.Visible = true;
            lblMessage.Text = "Successfully Updated!";
        }
        else
        {
            setting.AddNew();
            setting.SCreatedDate = DateTime.Now;
            lblMessage.Visible = true;
            lblMessage.Text = "Successfully Added!";
        }
        //setting.SBonus = FreeTextBox1.Text;
        setting.SContract = FreeTextBox2.Text;
        setting.SModifiedDate = DateTime.Now;
        setting.Save();
    }
    private void LoadSetting(tblSettings setting)
    {
        //FreeTextBox1.Text = setting.SBonus;
        FreeTextBox2.Text = setting.SContract;
    }
}
