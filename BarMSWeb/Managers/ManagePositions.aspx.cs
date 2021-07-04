using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
public partial class Managers_ManagePositions : System.Web.UI.Page
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
            if (user.AccessLevel != 2)
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

        tblDepartments depart = new tblDepartments();
        depart.LoadByPrimaryKey(DepartmentID);
        lblHeading.Text = "Manage Positions for " + depart.s_SDepartmentName;

        if (!Page.IsPostBack)
        {
            BindDepartmentGrid();
            tblSpecialityType spType = new tblSpecialityType();
            spType.GetSpecialityTypesWithoutSeparator();
            commonMethods.FillDropDownList(ddlSpecialities, spType.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
        }

    }

    private void BindDepartmentGrid()
    {
        tblSpeciality speciality = new tblSpeciality();
        speciality.LoadAllSpecialities(DepartmentID);
        if (speciality.RowCount > 0)
        {
            int orderNumber = 0;
            orderNumber = speciality.OrderID;
            for (int i = 0; i < speciality.RowCount; i++)
            {
                if (orderNumber != speciality.OrderID)
                {
                    tblSpeciality specialityUpdate = new tblSpeciality();
                    specialityUpdate.LoadByPrimaryKey(speciality.PkSpecialityID);
                    specialityUpdate.OrderID = orderNumber;
                    specialityUpdate.DModifiedDate = DateTime.Now;
                    specialityUpdate.Save();
                }
                orderNumber++;
                speciality.MoveNext();
            }
        }
        speciality.FlushData();
        speciality.LoadAllSpecialities(DepartmentID);

        grdDepartment.DataSource = speciality.DefaultView;
        grdDepartment.DataBind();

    }
    protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
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
                //HtmlImage imgActive = (HtmlImage)e.Row.FindControl("imgActive");
                ImageButton imgbtnActive = e.Row.FindControl("imgbtnActive") as ImageButton;
                if (drv["bIsActive"].ToString() == "False")
                {
                    //imgActive.Src = "../images/close.png";
                    //imgActive.Alt = "Activate";

                    imgbtnActive.ImageUrl = "../images/close.png";
                    imgbtnActive.ToolTip = "Activate";
                }
                else
                {
                    //imgActive.Src = "../images/activate_icon.gif";
                    //imgActive.Alt = "De-Activate";

                    imgbtnActive.ImageUrl = "../images/activate_icon.gif";
                    imgbtnActive.ToolTip = "De-Activate";
                }
                ImageButton imgBtnIncome = e.Row.FindControl("imgBtnIncome") as ImageButton;
                string no_Income_Message = "No Income, click to change.";
                string Income_Message = "Brings Income, click to change.";
                if (!Convert.ToBoolean(drv["bIsIncomeSpecific"]))
                {
                    imgBtnIncome.ImageUrl = "../Images/Dollar_grey.png";
                    imgBtnIncome.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + no_Income_Message + "')");
                    imgBtnIncome.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                else
                {
                    imgBtnIncome.ImageUrl = "../Images/Dollar.png";
                    imgBtnIncome.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + Income_Message + "')");
                    imgBtnIncome.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int SpecialityID;
        switch (e.CommandName)
        {


            case "Del":
                SpecialityID = Convert.ToInt32(e.CommandArgument);
                tblUserWorkshifts uws = new tblUserWorkshifts();
                uws.CheckSpecialityInUse(SpecialityID);
                if (uws.RowCount > 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "Position is in use";
                }
                else
                {


                    tblSpeciality speciality = new tblSpeciality();
                    speciality.LoadByPrimaryKey(SpecialityID);
                    speciality.MarkAsDeleted();
                    speciality.Save();
                    BindDepartmentGrid();
                }
                break;

            //case "Change":
            //    SpecialityID = Convert.ToInt32(e.CommandArgument);
            //    tblSpeciality Editspeciality = new tblSpeciality();
            //    Editspeciality.LoadByPrimaryKey(SpecialityID);
            //    txtPosition.Text = Editspeciality.s_SSpeciality;
            //    txtAbbrv.Text = Editspeciality.s_Abbrv;
            //    chkActive.Checked = Editspeciality.BIsActive;
            //    ddlSpecialities.SelectedValue = Editspeciality.s_FkSpecialityTypeID;
            //    txtOrder.Text = Editspeciality.s_OrderID;
            //    btnSave.Visible = false;
            //    btnEdit.Visible = true;
            //    lbluser.Text = "Edit Position Name";
            //    hdnID.Value = SpecialityID.ToString();

            //    break;
            case "edt":
                SpecialityID = Convert.ToInt32(e.CommandArgument);
                tblSpeciality Editspeciality = new tblSpeciality();
                Editspeciality.LoadByPrimaryKey(SpecialityID);
                txtPosition.Text = Editspeciality.s_SSpeciality;
                txtAbbrv.Text = Editspeciality.s_Abbrv;
                chkActive.Checked = Editspeciality.BIsActive;
                if (Editspeciality.s_SSpeciality != "Separator")
                    ddlSpecialities.SelectedValue = Editspeciality.s_FkSpecialityTypeID;
                txtOrder.Text = Editspeciality.s_OrderID;
                btnSave.Visible = false;
                btnEdit.Visible = true;
                lbluser.Text = "Edit Position Name";
                hdnID.Value = SpecialityID.ToString();
                divAddPosition.Visible = true;
                imgBtnAdd.Visible = false;
                btnEdit.Visible = true;
                break;
            case "income":
                try
                {
                    SpecialityID = Convert.ToInt32(e.CommandArgument);
                    tblSpeciality specialityIncome = new tblSpeciality();
                    specialityIncome.LoadByPrimaryKey(SpecialityID);
                    if (specialityIncome.RowCount > 0)
                    {
                        if (specialityIncome.BIsIncomeSpecific == null || !specialityIncome.BIsIncomeSpecific)
                            specialityIncome.BIsIncomeSpecific = true;
                        else
                            specialityIncome.BIsIncomeSpecific = false;
                        specialityIncome.DModifiedDate = DateTime.Now;
                        specialityIncome.Save();
                        BindDepartmentGrid();
                    }
                }
                catch (Exception ex)
                { }

                break;
            case "Active":
                SpecialityID = Convert.ToInt32(e.CommandArgument);
                tblSpeciality Editspeciality2 = new tblSpeciality();
                Editspeciality2.LoadByPrimaryKey(SpecialityID);
                if (Editspeciality2.BIsActive)
                    Editspeciality2.BIsActive = false;
                else if (!Editspeciality2.BIsActive)
                    Editspeciality2.BIsActive = true;
                Editspeciality2.DModifiedDate = DateTime.Now;
                Editspeciality2.Save();

                tblSpecialityType spType = new tblSpecialityType();
                spType.LoadAll();
                commonMethods.FillDropDownList(ddlSpecialities, spType.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
                txtPosition.Text = "";
                txtAbbrv.Text = "";
                txtOrder.Text = "";
                chkActive.Checked = false;


                BindDepartmentGrid();
                break;

        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
       
        if (txtPosition.Text != "" && txtAbbrv.Text != "")
        {
            tblSpeciality speciality = new tblSpeciality();
            speciality.CheckSpecialityName(txtPosition.Text);
            if (speciality.RowCount > 0)
            {

                lblError.Visible = true;
                lblError.Text = "Position Name already Exist";

            }
            else
            {
                speciality.FlushData();
                speciality.AddNew();
                speciality.s_SSpeciality = txtPosition.Text;
                speciality.s_Abbrv = txtAbbrv.Text;
                speciality.BIsActive = chkActive.Checked;
                speciality.FkDepartmentID = DepartmentID;
                speciality.BIsIncomeSpecific = false;
                speciality.s_FkSpecialityTypeID = ddlSpecialities.SelectedValue;
                int ordernum = 0;
                tblSpeciality allSpecialty = new tblSpeciality();
                allSpecialty.GetSpecialtyMaxOrderID();
                if (allSpecialty.RowCount > 0)
                {
                    ordernum = Convert.ToInt32(allSpecialty.GetColumn("orderid").ToString());
                }
                ordernum += 1;
                speciality.OrderID = ordernum;
                speciality.DCreateDate = DateTime.Now;
                speciality.DModifiedDate = DateTime.Now;
                speciality.Save();                
                BindDepartmentGrid();
            }
            divAddPosition.Visible = false;
            imgBtnAdd.Visible = true;
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Delete", "alert('Please add Position Name & Position Abbr.');", true);

    }
    private void MakeSortSpecialty(tblSpeciality speciality)
    {
        TextBox txtSort = txtOrder;
        
        tblSpeciality specialities = new tblSpeciality();
        tblSpeciality singleSpecialty = new tblSpeciality();
        singleSpecialty.LoadByPrimaryKey(speciality.PkSpecialityID);

        if (txtSort.Text != "" && txtSort.Text != "0")
        {
            if (Convert.ToInt32(txtSort.Text) != Convert.ToInt32(hidSortNo.Value))
            {
                if (Convert.ToInt32(txtSort.Text) < Convert.ToInt32(hidSortNo.Value))
                {
                    specialities.FlushData();
                    specialities.GetSpecialtiesForSortLesser(Convert.ToInt32(txtSort.Text), Convert.ToInt32(hidSortNo.Value));
                    if (specialities.RowCount > 0)
                    {
                        for (int i = 0; i < specialities.RowCount; i++)
                        {
                            if (specialities.OrderID != Convert.ToInt32(hidSortNo.Value))
                            {
                                specialities.OrderID = specialities.OrderID + 1;
                                specialities.DModifiedDate = DateTime.Now;
                                specialities.Save();
                            }
                            specialities.MoveNext();
                        }
                    }
                }
                else if (Convert.ToInt32(txtSort.Text) > Convert.ToInt32(hidSortNo.Value))
                {
                    specialities.FlushData();
                    specialities.GetSpecialtiesForSortGreater(Convert.ToInt32(hidSortNo.Value), Convert.ToInt32(txtSort.Text));
                    if (specialities.RowCount > 0)
                    {
                        for (int i = 0; i < specialities.RowCount; i++)
                        {
                            if (specialities.OrderID != Convert.ToInt32(hidSortNo.Value))
                            {
                                specialities.OrderID = specialities.OrderID - 1;
                                specialities.DModifiedDate = DateTime.Now;
                                specialities.Save();
                            }
                            specialities.MoveNext();
                        }
                    }
                }
                if (singleSpecialty.RowCount > 0)
                {
                    singleSpecialty.OrderID = Convert.ToInt32(txtSort.Text);
                    singleSpecialty.DModifiedDate = DateTime.Now;
                    singleSpecialty.Save();
                }
            }
        }
        
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int ordernum = 0;
        tblSpeciality speciality = new tblSpeciality();
        if (txtPosition.Text != "" && txtAbbrv.Text != "")
        {
            speciality.FlushData();
            speciality.LoadByPrimaryKey(Convert.ToInt32(hdnID.Value));
            speciality.s_SSpeciality = txtPosition.Text;
            speciality.s_Abbrv = txtAbbrv.Text;
            speciality.BIsActive = chkActive.Checked;
            speciality.FkDepartmentID = DepartmentID;
            if (speciality.s_SSpeciality != "Separator") 
                speciality.s_FkSpecialityTypeID = ddlSpecialities.SelectedValue;
            hidSortNo.Value = speciality.OrderID.ToString();
            speciality.DModifiedDate = DateTime.Now;
            speciality.Save();


            tblSpeciality allSpecialty = new tblSpeciality();
            allSpecialty.GetSpecialtyMaxOrderID();
            if (allSpecialty.RowCount > 0)
                ordernum = Convert.ToInt32(allSpecialty.GetColumn("orderid").ToString());
            if (txtOrder.Text != "")
            {
                if (ordernum <= Convert.ToInt32(txtOrder.Text))
                {
                    txtOrder.Text = ordernum.ToString();
                }
                MakeSortSpecialty(speciality);
            }

            txtPosition.Text = "";
            txtAbbrv.Text = "";
            txtOrder.Text = "";
            chkActive.Checked = false;
            btnSave.Visible = true;
            btnEdit.Visible = false;
            hdnID.Value = "";
            lbluser.Text = "Add Position Name";
            BindDepartmentGrid();

            divAddPosition.Visible = false;
            imgBtnAdd.Visible = true;
            
        }
        else
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Delete", "alert('Please add Position Name & Position Abbr.');", true);
    }


   
    protected void grdDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDepartment.PageIndex = e.NewPageIndex;
        BindDepartmentGrid();
    }
    protected void imgBtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        
        imgBtnAdd.Visible = false;
        divAddPosition.Visible = true;
    }
    protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        divAddPosition.Visible = false;
        imgBtnAdd.Visible = true;
    }
    protected void imgBtnSeperator_Click(object sender, ImageClickEventArgs e)
    {
        tblSpeciality speciality = new tblSpeciality();
        speciality.AddNew();
        int ordernum = 0;
        tblSpeciality allSpecialty = new tblSpeciality();
        allSpecialty.GetSpecialtyMaxOrderID();
        if (allSpecialty.RowCount > 0)
        {
            ordernum = Convert.ToInt32(allSpecialty.GetColumn("orderid").ToString());
        }
        ordernum += 1;
        speciality.OrderID = ordernum;
        speciality.s_SSpeciality = "Separator";
        speciality.s_Abbrv = "Sep";
        speciality.BIsActive = true;
        speciality.FkDepartmentID = DepartmentID;
        speciality.BIsIncomeSpecific = false;
        tblSpecialityType sptype = new tblSpecialityType();
        sptype.GetSeparator();
        if (sptype.RowCount > 0)
        {
            speciality.FkSpecialityTypeID = sptype.PkSpecialityTypeID;
        }
        speciality.DCreateDate = DateTime.Now;
        speciality.DModifiedDate = DateTime.Now;
        speciality.Save();
        BindDepartmentGrid();
    }
}
