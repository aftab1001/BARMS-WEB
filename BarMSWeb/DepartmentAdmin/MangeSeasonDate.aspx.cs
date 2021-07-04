using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_MangeSeasonDate : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
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
            LoadDates();
        }
    }
    private void LoadDates()
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadByPrimaryKey(DepartmentID);
        if (departments.RowCount > 0)
        {
            string[] low1 = departments.LowSeason1.Split('-');
            
            txtFromDate1.Text = (low1[0].Split('/'))[0].ToString();
            ddlFromMonth1.SelectedValue = (low1[0].Split('/'))[1].ToString();

            txtTillDate1.Text = (low1[1].Split('/'))[0].ToString();
            ddlTillMonth1.SelectedValue = (low1[1].Split('/'))[1].ToString();

            string[] low2 = departments.LowSeason2.Split('-');

            txtFromDate2.Text = (low2[0].Split('/'))[0].ToString();
            ddlFromMonth2.SelectedValue = (low2[0].Split('/'))[1].ToString();

            txtTillDate2.Text = (low2[1].Split('/'))[0].ToString();
            ddlTillMonth2.SelectedValue = (low2[1].Split('/'))[1].ToString();

            string[] high = departments.HighSeason.Split('-');

            txtFromHighDate.Text = (high[0].Split('/'))[0].ToString();
            ddlFromHighMonth.SelectedValue = (high[0].Split('/'))[1].ToString();

            txtTillHighDate.Text = (high[1].Split('/'))[0].ToString();
            ddlTillHighMonth.SelectedValue = (high[1].Split('/'))[1].ToString();
        }
    }
    protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadByPrimaryKey(DepartmentID);
        if (departments.RowCount > 0)
        {

            string low_season1_from = txtFromDate1.Text + "/" + ddlFromMonth1.SelectedValue;
            string low_season1_till = txtTillDate1.Text + "/" + ddlTillMonth1.SelectedValue;

            string low_season2_from = txtFromDate2.Text + "/" + ddlFromMonth2.SelectedValue;
            string low_season2_till = txtTillDate2.Text + "/" + ddlTillMonth2.SelectedValue;

            string low_season_def = "From " + low_season1_from + " till " + low_season1_till + " AND " + low_season2_from + " till " + low_season2_till;


            string high_season_from = txtFromHighDate.Text + "/" + ddlFromHighMonth.SelectedValue;
            string high_season_till = txtTillHighDate.Text + "/" + ddlTillHighMonth.SelectedValue;
            string hig_season_def = "From " + high_season_from + " till " + high_season_till;

            departments.LowSeason1 = low_season1_from + "-" + low_season1_till;
            departments.LowSeason2 = low_season2_from + "-" + low_season2_till;
            departments.HighSeason = high_season_from + "-" + high_season_till;

            departments.LowSeasonDef = low_season_def;
            departments.HighSeasonDef = hig_season_def;

            departments.DModifiedDate = DateTime.Now;
            departments.Save();

            lblMessage.Visible = true;
        }
    }
}
