using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_StartupCapital : System.Web.UI.Page
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
            GetStartupCapital();
        }
    }

    private void GetStartupCapital()
    {
        tblStartupCapital stCapital = new tblStartupCapital();
        stCapital.getAllStartupCapital();
        grdStartupCapital.DataSource = stCapital.DefaultView;
        grdStartupCapital.DataBind();
    }


    #region Adding, Updating and Canceling Income Amount
    protected void imgAddSpecialIncome_Click(object sender, ImageClickEventArgs e)
    {
        imgAddSpecialIncome.Visible = false;
        trOtherIncome.Visible = true;
        trLineBefore.Visible = false;
        trLineAfter.Visible = true;
        imgBtnSaveOtherIncome.Visible = true;
        txtSCAmount.Text = "";
        txtNote.Text = "";
    }
    protected void imgBtnSaveOtherIncome_Click(object sender, ImageClickEventArgs e)
    {
        if (txtSCAmount.Text != "")
        {
            string[] start_D = txtDate.Text.Split('/'); // start Date
            string date = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            tblStartupCapital sc = new tblStartupCapital();
            sc.AddNew();
            sc.Amount = commonMethods.ChangeToUS(txtSCAmount.Text);
            sc.Note = txtNote.Text;
            sc.FkDepartmentAdminID = UserID;
            sc.DModifiedDate = Convert.ToDateTime(date);
            sc.DCreatedDate = DateTime.Now;
            sc.Save();

            txtSCAmount.Text = "";
            txtNote.Text = "";
            txtDate.Text = "";

            imgAddSpecialIncome.Visible = true;
            trOtherIncome.Visible = false;
            trLineBefore.Visible = true;
            trLineAfter.Visible = false;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", "$(function(){RecordSaved();});", true);

            GetStartupCapital();
        }
    }
    protected void ImgBtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["id"] != null)
            {
                tblStartupCapital sc = new tblStartupCapital();
                sc.LoadByPrimaryKey(Convert.ToInt32(ViewState["id"]));
                if (sc.RowCount > 0)
                {
                    sc.Amount = commonMethods.ChangeToUS(txtSCAmount.Text);
                    sc.Note = txtNote.Text;
                    string[] start_D = txtDate.Text.Split('/'); // start Date
                    string date = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                    sc.DModifiedDate = Convert.ToDateTime(date);
                    sc.Save();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", "$(function(){RecordSaved();});", true);

                    trLineAfter.Visible = true;
                    trLineBefore.Visible = false;
                    trOtherIncome.Visible = false;
                    ImgBtnEdit.Visible = false;
                    imgBtnSaveOtherIncome.Visible = true;
                    imgAddSpecialIncome.Visible = false;

                    GetStartupCapital();
                }

            }
        }
        catch (Exception ex)
        { }
    }
    protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ImgBtnEdit.Visible = false;
        imgBtnSaveOtherIncome.Visible = true;

        imgAddSpecialIncome.Visible = true;
        trOtherIncome.Visible = false;
        trLineBefore.Visible = true;
        trLineAfter.Visible = false;


    }


    #endregion



    protected void grdStartupCapital_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                Label lblNotes = e.Row.FindControl("lblNotes") as Label;
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;
                
                if (dr["note"].ToString() != "")
                {
                    if (dr["note"].ToString().Length > 30)
                    {
                        string note = dr["note"].ToString().Substring(0, 30);
                        lblNotes.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + note + "');");
                        lblNotes.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow();");
                    }
                }

                DateTime dt = Convert.ToDateTime(dr["dmodifieddate"].ToString());
                lblDate.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
                lblAmount.Text = lblAmount.Text + " €";

            }
            catch (Exception ex)
            { }
        }
    }
    protected void grdStartupCapital_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "edt":

                    trLineAfter.Visible = false;
                    trLineBefore.Visible = true;
                    trOtherIncome.Visible = true;
                    imgAddSpecialIncome.Visible = false;
                    ImgBtnEdit.Visible = true;
                    imgBtnSaveOtherIncome.Visible = false;

                    tblStartupCapital sc = new tblStartupCapital();
                    sc.LoadByPrimaryKey(id);
                    if (sc.RowCount > 0)
                    {
                        txtSCAmount.Text = commonMethods.ChangetToUK(sc.Amount.ToString());
                        txtNote.Text = sc.Note;

                        string date = sc.DModifiedDate.Day + "/" + sc.DModifiedDate.Month + "/" + sc.DModifiedDate.Year;
                        txtDate.Text = date;
                        ViewState["id"] = sc.PkStartupCapitalID;
                    }

                    break;
            }
        }
    }


}
