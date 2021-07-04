using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_transaction : System.Web.UI.Page
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
            GetAllTrasactions();
        }
    }
    private void GetAllTrasactions()
    {
        tblTransactions tr = new tblTransactions();
        tr.getAllTransactions_without_condition();
        grdTransactions.DataSource = tr.DefaultView;
        grdTransactions.DataBind();
    }
    protected void grdTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgActivate = e.Row.FindControl("imgActivate") as ImageButton;
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;

            DataRowView drv = (DataRowView)e.Row.DataItem;
            if (Convert.ToBoolean(drv["Received"].ToString()))
                imgActivate.ImageUrl = "../Images/activate_icon.gif";
            else
                imgActivate.ImageUrl = "../Images/close.png";
            lblDate.Text = Convert.ToDateTime(lblDate.Text).ToString("dd") + "/" + Convert.ToDateTime(lblDate.Text).ToString("MM") + "/" + Convert.ToDateTime(lblDate.Text).Year;
            lblAmount.Text = commonMethods.ChangetToUK(Convert.ToDouble(lblAmount.Text).ToString("N")) + " €";


        }
    }
    protected void grdTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "edt":
                    tblTransactions tr = new tblTransactions();
                    tr.LoadByPrimaryKey(id);
                    if (tr.RowCount > 0)
                    {
                        if (tr.Received)
                            tr.Received = false;
                        else if (!tr.Received)
                            tr.Received = true;
                        tr.Save();
                        GetAllTrasactions();

                    }

                    break;
            }
        }
    }
}
