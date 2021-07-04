
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using LC.Model.BMS.BLL;


public partial class DepartmentAdmin_ManageAdminExpenses : System.Web.UI.Page
{
    int year;
    int selectedWeek;
    int day;
    int DepartmentID;
    int UserID;
    static int O_ExpenseID = 0;
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
        if (!IsPostBack)
        {
            BindExpenses();
            imgBtnEditExpense.Visible = false;
           
        }
       
    }
   
    protected void grdOtherIncomes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void grdOtherIncomes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void ddlIncomeTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

       

    }

    #region Adding, Updating and Canceling Income Amount
    protected void imgAddSpecialIncome_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgBtnSaveOtherIncome_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void ImgBtnEdit_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        
    }


    #endregion

    #region Add New Income Type
    protected void imgBtnAddTempIncome_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    #endregion

   

    #region Expense Types

  
    protected void grdExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            ClearTextBox();
            int expenseid = Convert.ToInt32(e.CommandArgument);
            tblAdminExpenses ex = new tblAdminExpenses();
            switch (e.CommandName.ToLower())
            {
                case "expense":
                    trMessageExpense.Visible = false;
                    imgBtnEditExpense.Visible = true;
                    imgBtnSaveExpense.Visible = false;
                    ex.FlushData();
                    ex.LoadByPrimaryKey(expenseid);
                    O_ExpenseID = expenseid;
                    if (ex.RowCount > 0)
                    {

                        txtEnpenseAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("Amount").ToString());
                        txtNoteExpense.Text = ex.GetColumn("Note").ToString();
                        txtExpenseName.Text = ex.GetColumn("ExpenseName").ToString();

                    }
                    // upnlOtherIncome.Update();
                    break;
                case "expensetype":
                    trMessageExpense.Visible = false;
                    imgBtnEditExpense.Visible = true;
                    imgBtnSaveExpense.Visible = false;
                    ex.FlushData();
                    ex.LoadByPrimaryKey(expenseid);
                    O_ExpenseID = expenseid;
                    if (ex.RowCount > 0)
                    {

                        txtEnpenseAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("Amount").ToString());
                        txtNoteExpense.Text = ex.GetColumn("Note").ToString();
                        txtExpenseName.Text = ex.GetColumn("ExpenseName").ToString();
                       
                    }
                    // upnlOtherIncome.Update();
                    break;
                case "delexpense":
                    trMessageExpense.Visible = false;
                   // trddlExpense.Visible = false;
                    ex.FlushData();
                    ex.LoadByPrimaryKey(expenseid);
                    if (ex.RowCount > 0)
                    {
                        ex.MarkAsDeleted();
                        ex.Save();

                        
                        BindExpenses();
                        
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
                    }
                    break;
            }
        }
    }
    protected void grdExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkExpense = e.Row.FindControl("lnkExpense") as LinkButton;
            lnkExpense.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('Click to change amount.')");
            lnkExpense.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            lnkExpense.Text = commonMethods.ChangetToUK(lnkExpense.Text) + " €";


            LinkButton lnkExpenseType = e.Row.FindControl("lnkExpense") as LinkButton;
            lnkExpenseType.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('Click to change amount.')");
            lnkExpenseType.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
        }
    }
    protected void ddlExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
     
    }
   
    #region Adding,Editing and Canceling Expenses
    protected void imgBtnAddExpense_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void imgBtnSaveExpense_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEnpenseAmount.Text != "")
      {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        tblAdminExpenses obj = new tblAdminExpenses();
        obj.AddNew();
        obj.BDateCreate = mydate;
        obj.ExpenseName = txtExpenseName.Text;
        obj.Amount = commonMethods.ChangeToUS(txtEnpenseAmount.Text);
        obj.CreatedDate = DateTime.Now.Date;
        obj.ModifyDate=DateTime.Now.Date;
        obj.Note = txtNoteExpense.Text;
        obj.IsPaid = false;
        obj.Save();
        trMessageExpense.Visible = true;
        lblMessageExpense.Text = "Successfully Saved Expense Amount!";
        lblMessageExpense.ForeColor = Color.Green;
        BindExpenses();
        ClearTextBox();
      }
        else
      {
          trMessageExpense.Visible = true;
            lblMessageExpense.Text = "Plese enter Expense Amount!";
            lblMessageExpense.ForeColor = Color.Red;
      }
             
    }
    private void ClearTextBox()
    {
        txtExpenseName.Text = "";
        txtNoteExpense.Text = "";
        txtEnpenseAmount.Text = "";
    
    }
    private void BindExpenses()
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        tblAdminExpenses ex = new tblAdminExpenses();
        ex.GetExpenses(mydate.Year, mydate.Month, mydate.Day);
        if (ex.RowCount > 0)
        {
      grdExpenses.DataSource = ex.DefaultView;
        grdExpenses.DataBind();
  
        }
        else
        {
        grdExpenses.DataSource = null;
        grdExpenses.DataBind();

        }
        
        if (ex.RowCount > 0)
        {
            tdExpenseTotal.Visible = true;
            double ExpenseAmount = 0.0;

            for (int i = 0; i < ex.RowCount; i++)
            {
                ExpenseAmount += Convert.ToDouble(ex.GetColumn("Amount").ToString());
                ex.MoveNext();
            }
            lblExpenseSubtotal.Text = commonMethods.ChangetToUK(ExpenseAmount.ToString("N")) + " €";
        }
        else
        {
            tdExpenseTotal.Visible = false;
        }
       // upnlExpenses.Update();
    }
    protected void imgBtnEditExpense_Click(object sender, ImageClickEventArgs e)
    {
        //trddlExpense.Visible = false;
        if (txtEnpenseAmount.Text != "")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }
            tblAdminExpenses ex = new tblAdminExpenses();
            ex.LoadByPrimaryKey(O_ExpenseID);
            if (ex.RowCount > 0)
            {
                
                ex.Amount = commonMethods.ChangeToUS(txtEnpenseAmount.Text);
                ex.ExpenseName = txtExpenseName.Text;
                ex.Note = txtNoteExpense.Text;
                ex.ModifyDate = DateTime.Now.Date;
                ex.Save();
                trMessageExpense.Visible = true;
                lblMessageExpense.Text = "Successfully Updated Expense Amount!";
                lblMessageExpense.ForeColor = Color.Green;
               
                BindExpenses();
                ClearTextBox();
               
            }
            O_ExpenseID = 0;
            imgBtnEditExpense.Visible = false;
            imgBtnSaveExpense.Visible = true;
            
        }
        else
        {
            trMessageExpense.Visible = true;
            lblMessageExpense.Text = "Plese enter Expense Amount!";
            lblMessageExpense.ForeColor = Color.Red;
           // pnlExpenses.DefaultButton = "imgBtnEditExpense";
        }
       // upnlExpenses.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
    }
    protected void imgBtnCancelExpense_Click(object sender, ImageClickEventArgs e)
    {
       
    }

    #endregion

    #region Adding New Expense Type

    protected void imgBtnAddNewExpenseType_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    #endregion

    #endregion

    protected void btnGO_Click(object sender, EventArgs e)
    {
       
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;
       
     
        txtCheck.Text = "2";
       
        txtDay.Text = days.ToString();
       lblWeek.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();
        year = mydate.Year;
        selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        day = commonMethods.getDay(mydate);
        trMessageExpense.Visible = false;
        BindExpenses();

    }

    #region SubTotal for Tab 1

    
    protected void grdIncome_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    private void CreatingGridIncomeControls_For_Subtotal(GridViewRowEventArgs e)
    {
       
    }

    #endregion

    #region Daily Income Users for Tab 2

    
    protected void GrdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void GrdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    { }
    private void CreatingGridUserControls_For_Subtotal(GridViewRowEventArgs e)
    { }

    #region Saving Daily Income
    protected void imgBtnSaveTop_Click(object sender, ImageClickEventArgs e)
    { }
    #endregion

    #region Cancel Save Changing on Daily Income
    protected void imgCancelChanges_Click(object sender, ImageClickEventArgs e)
    {
      
    }
    #endregion

    #endregion

    #region Sending New Income and Expense Messages,Email to Department Admim

    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    { }

    #endregion

    #region Day Comment


    protected void imgBtnSaveDayComment_Click(object sender, ImageClickEventArgs e)
    { }

    #endregion

    #region Extra Code

    protected void upnlGo_Load(object sender, EventArgs e)
    { 
    }
  

    protected void pnlUsers_DataBinding(object sender, EventArgs e)
    {

    }
    #endregion

    #region Registers


    protected void imgBtnSaveRegisterValueTop_Click(object sender, ImageClickEventArgs e)
    { }
    protected void grdRegisters_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdRegisters_RowDataBound(object sender, GridViewRowEventArgs e)
    { }
    #endregion
}
