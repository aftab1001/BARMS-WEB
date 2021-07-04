using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
using System.Data;

public partial class DepartmentAdmin_BonusDoc : System.Web.UI.Page
{
    int UserID;
    int Managerid;
    int AccountManagerid;
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
            //BindStaff();
            //BindManagers();
            BindAllStaff_Managers();
            BindYears();
            BindDropDownFilter();
            BindDropDownSearch();
           // BindddlUsers();
        }

    }
    private void BindAllStaff_Managers()
    {
        tblUsers users = new tblUsers();

        users.AllManagers_Bonus(DepartmentID);
        grdManagers.DataSource = users.DefaultView;
        grdManagers.DataBind();

        users.FlushData();
        users.LoadStaff(DepartmentID,UserID);
        grdStaff.DataSource = users.DefaultView;
        grdStaff.DataBind();
    }
    private void BindYears()
    {
        ddlYears.Items.Clear();
        for (int i = 0; i <= 74; i++)
        {
            ddlYears.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
        }
        ddlYears.Items.Insert(0, new ListItem("--Select Year--", "0"));
    }
    private void BindManagers()
    {
        tblUsers u = new tblUsers();
        u.LoadManager_and_AccountManager(DepartmentID);
        if (u.RowCount > 0)
        {
            for (int i = 0; i < u.RowCount; i++)
            {
                ddlusers.Items.Add(new ListItem(u.GetColumn("FullName").ToString(), u.PkUserID.ToString()));
                u.MoveNext();
            }
        }
        grdManagers.DataSource = u.DefaultView;
        grdManagers.DataBind();
    }
    private void BindStaff()
    {
        tblUsers u = new tblUsers();
        u.LoadStaff(DepartmentID, UserID);
        if (u.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlusers, u.DefaultView, "FullName", "pkUserID");   
        }
        grdStaff.DataSource = u.DefaultView;
        grdStaff.DataBind();
    }

    private void BindddlUsers()
    {
        tblUsers u = new tblUsers();
        u.LoadManager_and_AccountManager(DepartmentID);
        if (u.RowCount > 0)
            commonMethods.FillDropDownList(ddlusers, u.DefaultView, "FullName", "pkUserID");   
        u.FlushData();
        u.LoadStaff(DepartmentID, UserID);
        if (u.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlusers, u.DefaultView, "FullName", "pkUserID");   
        }
    }

    protected void grdStaff_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "active":
                    tblUsers u = new tblUsers();
                    u.LoadByPrimaryKey(Convert.ToInt32(id));
                    ImageButton btn = (ImageButton)e.CommandSource;

                    if (u.RowCount > 0)
                    {
                        if (!u.BBonusApprovedByDepartment)
                            u.BBonusApprovedByDepartment = true;
                        else
                            u.BBonusApprovedByDepartment = false;

                        if (u.BActiveBonusDoc)
                        {
                            u.DModifiedDate = DateTime.Now;
                            u.Save();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Bonus", "alert('First Activate Bonus By Manager!')", true);
                        }

                        if (!u.BBonusApprovedByDepartment)
                            btn.ImageUrl = "~/images/close.png";
                    }
                    BindStaff();
                    break;
                case "activeuser":
                    tblUsers uActive = new tblUsers();
                    uActive.LoadByPrimaryKey(Convert.ToInt32(id));
                     ImageButton btnUser = (ImageButton)e.CommandSource;
                    if (uActive.RowCount > 0)
                    {
                        if(!uActive.BActiveByAdmin)
                            uActive.BActiveByAdmin = true;
                        else if(uActive.BActiveByAdmin)
                            uActive.BActiveByAdmin = false;

                        uActive.DModifiedDate = DateTime.Now;
                        uActive.Save();
                        if (uActive.BActiveByAdmin)
                            btnUser.ImageUrl = "~/images/activate_icon.gif";
                        else if (!uActive.BActiveByAdmin)
                            btnUser.ImageUrl = "~/images/close.png";

                    }
                    break;

                case "name":
                    //tblUsers userName = new tblUsers();
                    //userName.LoadByPrimaryKey(Convert.ToInt32(id));
                    tblUserBonuses bonuses = new tblUserBonuses();
                    bonuses.GetUserBonus(Convert.ToInt32(id));
                    if (bonuses.RowCount > 0)
                        FreeTextBox1.Text = bonuses.Bonus;
                    else
                        FreeTextBox1.Text = "";
                    ViewState["staffID"] = id;

                    break;
            }
        }
    }
    protected void grdStaff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drv = ((DataRowView)e.Row.DataItem).Row;

            ImageButton btn = e.Row.FindControl("imgBtnActive") as ImageButton;
            Image imgActive = e.Row.FindControl("imgActive") as Image;
            ImageButton imgActiveUser = e.Row.FindControl("imgActiveUser") as ImageButton;

            if (!Convert.ToBoolean(drv["BActiveBonusDoc"]))
                imgActive.ImageUrl = "~/images/close.png";
            else if (Convert.ToBoolean(drv["BActiveBonusDoc"]))
                imgActive.ImageUrl = "~/images/activate_icon.gif";

            if (!Convert.ToBoolean(drv["BBonusApprovedByDepartment"]))
                btn.ImageUrl = "~/images/close.png";
        }
    }
    protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["staffID"] != null)
        {
            tblUserBonuses bonuses = new tblUserBonuses();
            bonuses.GetFilterdUserBonuses(Convert.ToInt32(ViewState["staffID"]));
            if (bonuses.RowCount > 0)
            {
                FreeTextBox1.Text = bonuses.Bonus.ToString();
            }
            else
            {
                bonuses.FlushData();
                bonuses.AddNew();
                bonuses.FkUserID = Convert.ToInt32(ViewState["staffID"]);
                bonuses.Bonus = FreeTextBox1.Text;
                bonuses.DCreatedDate = DateTime.Now;
                bonuses.DModifiedDate = DateTime.Now;
                bonuses.Save();
            }
        }
    }
    protected void grdManagers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "active":
                    tblUsers u = new tblUsers();
                    u.LoadByPrimaryKey(Convert.ToInt32(id));
                    ImageButton btn = (ImageButton)e.CommandSource;

                    if (u.RowCount > 0)
                    {
                        if (!u.BActiveBonusDoc)
                            u.BActiveBonusDoc = true;
                        else
                            u.BActiveBonusDoc = false;
                        u.DModifiedDate = DateTime.Now;
                        u.Save();

                        //if (!u.BActiveBonusDoc)
                        //    btn.ImageUrl = "~/images/close.png";
                        //else
                        //    btn.ImageUrl = "~/Images/activate_icon.gif";
                    }
                    BindManagers();
                    break;

                case "name":
                    //tblUsers userName = new tblUsers();
                    //userName.LoadByPrimaryKey(Convert.ToInt32(id));
                    tblUserBonuses bonuses = new tblUserBonuses();
                    bonuses.GetUserBonus(Convert.ToInt32(id));
                    if (bonuses.RowCount > 0)
                        FreeTextBox1.Text = bonuses.Bonus;
                    else
                        FreeTextBox1.Text = "";
                    ViewState["staffID"] = id;

                    break;
            }
        }
    }
    protected void grdManagers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drv = ((DataRowView)e.Row.DataItem).Row;

            ImageButton btn = e.Row.FindControl("imgBtnActive") as ImageButton;


            if (!Convert.ToBoolean(drv["BActiveBonusDoc"]))
                btn.ImageUrl = "~/images/close.png";
        }
    }
    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        
            //tblUserBonuses bonuses = new tblUserBonuses();
            //if (ddlYears.SelectedValue != "0")
            //{
            //    bonuses.FlushData();
            //    bonuses.GetFilterdUserBonuses(Convert.ToInt32(ddlusers.SelectedValue), ddlYears.SelectedItem.Text);
            //    if (bonuses.RowCount > 0)
            //    {
            //        grdBonus.DataSource = bonuses.DefaultView;
            //        grdBonus.DataBind();
            //        ModalPopupExtender2.Show();
                   
            //    }
            //    else
            //    {
            //        FreeTextBox1.Text = "";
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Bonus", "alert('There is no any bonus assigned to it.!')", true);
            //    }
            //}
            //else
            //{
            //    FreeTextBox1.Text = "";
       
    }
    protected void grdBonus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            int day;
            string mnonthName;

            day = Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dModifiedDate")).Day;
            mnonthName = Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dModifiedDate")).ToString("MMMM");

            lblDate.Text = day + " " + mnonthName + " " + Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dModifiedDate")).Year;
        }
    }
    protected void imgBtnFilterSpecialty_Click(object sender, ImageClickEventArgs e)
    {
        BindDropDownSearch();
        tblUsers user = new tblUsers();
        string filterYear = string.Empty;
        string filterYearPopup = string.Empty;
        string filterSpecialty = string.Empty;
        string filterCountry = string.Empty;



        bool check = false;

        if (ddlYears.SelectedValue != "0")
        {
            filterYear = "(Year(uc.dEmploymentFromDate) = " + ddlYears.SelectedItem.Text + " or Year(uc.dEmploymentEndDate) = " + ddlYears.SelectedItem.Text + ") and";
            filterYearPopup = "(Year(ub.dModifiedDate) = " + ddlYears.SelectedItem.Text + " ) and";
            check = true;
            imgBtnClearFilter.Visible = true;
        }
        if (ddlSpeciality.SelectedValue != "0")
        {
            filterSpecialty = "(us.fkSpecialityTypeID = " + ddlSpeciality.SelectedValue + " ) and";
            check = true;
            imgBtnClearFilter.Visible = true;
        }
        if (ddlCountry.SelectedValue != "0")
        {
            filterCountry = "(u.fkNationalityCountry = " + ddlCountry.SelectedValue + ") and";
            check = true;
            imgBtnClearFilter.Visible = true;
        }

        if (check)
        {
            string query = string.Empty;
            string queryPopup = string.Empty;
            string queryStart = " and (";
            string queryEnd = ")";
            string active_nonactive = string.Empty;
            if (rdoActive.Checked)
            {
                active_nonactive = "and u.bactivebyuser = 1 and u.bactivebyadmin= 1";
                query = active_nonactive;
                queryPopup = active_nonactive;
            }
            else if (rdoInActive.Checked)
            {
                active_nonactive = "and u.bactivebyuser = 1 and u.bactivebyadmin= 0";
                query = active_nonactive;
                queryPopup = active_nonactive;
            }
            else
            {
                query = "";
                queryPopup = "";
            }
            query += queryStart;
            queryPopup += queryStart;

            if (ddlYears.SelectedValue != "0")
            {
                query += filterYear;
                queryPopup += filterYearPopup;

                //filterMessage = "";
            }
            if (ddlSpeciality.SelectedValue != "0")
            {
                query += filterSpecialty;
                queryPopup += filterSpecialty;
            }
            if (ddlCountry.SelectedValue != "0")
            {
                query += filterCountry;
                queryPopup += filterCountry;
            }


            //CreateFilterMessage();
            //divFilter.InnerHtml = filterMessage;
            query = query.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            queryPopup = queryPopup.TrimEnd('d').TrimEnd('n').TrimEnd('a');

            query += queryEnd;
            queryPopup += queryEnd;

            #region Grid on Manage Bonus Page
            user.FlushData();
            //user.filterUsers_Bonus(DepartmentID, UserID, query);
            user.filterUsers_BonusPopUp(DepartmentID, UserID, queryPopup);
            grdStaff.DataSource = user.DefaultView;
            grdStaff.DataBind();

            user.FlushData();
            //user.filterManagers_Bonus(DepartmentID, query);
            user.filterManagers_BonusPopUp(DepartmentID, queryPopup);
            grdManagers.DataSource = user.DefaultView;
            grdManagers.DataBind();
            #endregion

            #region Grid on PopUp Bonus
            user.FlushData();
            user.filterUsers_BonusPopUp(DepartmentID, UserID, queryPopup);
            grdBonusStaff.DataSource = user.DefaultView;
            grdBonusStaff.DataBind();

            user.FlushData();
            user.filterManagers_BonusPopUp(DepartmentID, queryPopup);
            grdBonusManagers.DataSource = user.DefaultView;
            grdBonusManagers.DataBind();

            ModalPopupExtender2.Show();
            #endregion

            //if (user.RowCount <= 0)
            //{
            //    divFilter.InnerHtml = "";
            //}
        }
        else
        {
            if (rdoInActive.Checked)
                BindGridInActive();
            else if (rdoActive.Checked)
                BindGridActive();
            else
                BindAllStaff_Managers();
        }
    }
    private void BindGridInActive()
    {
        tblUsers users = new tblUsers();

        users.InActiceManagers_Bonus(DepartmentID);
        grdManagers.DataSource = users.DefaultView;
        grdManagers.DataBind();

        users.FlushData();
        users.InActiveUsers_Bonus(UserID, DepartmentID);
        grdStaff.DataSource = users.DefaultView;
        grdStaff.DataBind();
        FreeTextBox1.Text = "";
    }
    private void BindGridActive()
    {
        tblUsers users = new tblUsers();

        users.ActiveManagers_Bonus(DepartmentID);
        grdManagers.DataSource = users.DefaultView;
        grdManagers.DataBind();

        users.FlushData();
        users.ActiveUsers_Bonus(UserID, DepartmentID);
        grdStaff.DataSource = users.DefaultView;
        grdStaff.DataBind();

        FreeTextBox1.Text = "";
    }
    protected void imgBtnClearFilter_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnClearFilter.Visible = false;
        Response.Redirect("BonusDoc.aspx");
    }
    protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindDropDownFilter();
        tblUsers user = new tblUsers();
        if (ddlSearch.SelectedValue == "4")
        {
            user.FlushData();
            tblSpecialityType spType = new tblSpecialityType();
            spType.SpecialityTypeName(txtSearch.Text);
            if (spType.RowCount > 0)
            {
                user.FlushData();
                string condition = "and us.fkSpecialityTypeID = " + spType.PkSpecialityTypeID;
                BindSearchResult(condition);
                //user.LoadSearchUsers_Bonus(DepartmentID, UserID, condition);                
                //grdStaff.DataSource = user.DefaultView;
                //grdStaff.DataBind();

                //user.FlushData();
                //user.LoadSearchUsers_BonusPopUp(DepartmentID, UserID, condition);
                //grdBonusStaff.DataSource = user.DefaultView;
                //grdBonusStaff.DataBind();

                //user.FlushData();
                //user.LoadSearchManagers_Bonus(DepartmentID, condition);                
                //grdManagers.DataSource = user.DefaultView;
                //grdManagers.DataBind();

                //user.FlushData();
                //user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
                //grdBonusManagers.DataSource = user.DefaultView;
                //grdBonusManagers.DataBind();

            }
        }
        else if (ddlSearch.SelectedValue == "3")
        {
            user.FlushData();
            tblCountries countries = new tblCountries();
            countries.LoadByName(txtSearch.Text);
            if (countries.RowCount > 0)
            {
                user.FlushData();
                string condition = "and u.fkNationalityCountry= '" + countries.PkCountryID.ToString() + "' ";
                BindSearchResult(condition);
                //user.LoadSearchUsers_Bonus(DepartmentID, UserID, condition);
                //grdStaff.DataSource = user.DefaultView;
                //grdStaff.DataBind();

                //user.FlushData();
                //user.LoadSearchUsers_BonusPopUp(DepartmentID, UserID, condition);
                //grdBonusStaff.DataSource = user.DefaultView;
                //grdBonusStaff.DataBind();

                //user.FlushData();
                //user.LoadSearchManagers_Bonus(DepartmentID, condition);
                //grdManagers.DataSource = user.DefaultView;
                //grdManagers.DataBind();

                //user.FlushData();
                //user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
                //grdBonusManagers.DataSource = user.DefaultView;
                //grdBonusManagers.DataBind();
            }
        }
        else if (ddlSearch.SelectedValue == "2")
        {
            user.FlushData();
            string condition = "and u.sLastName= '" + txtSearch.Text + "' ";
            BindSearchResult(condition);
            //user.LoadSearchUsers_Bonus(DepartmentID, UserID, condition);
            //grdStaff.DataSource = user.DefaultView;
            //grdStaff.DataBind();

            //user.FlushData();
            //user.LoadSearchUsers_BonusPopUp(DepartmentID, UserID, condition);
            //grdBonusStaff.DataSource = user.DefaultView;
            //grdBonusStaff.DataBind();

            //user.FlushData();
            //user.LoadSearchManagers_Bonus(DepartmentID, condition);
            //grdManagers.DataSource = user.DefaultView;
            //grdManagers.DataBind();

            //user.FlushData();
            //user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
            //grdBonusManagers.DataSource = user.DefaultView;
            //grdBonusManagers.DataBind();
        }
        else if (ddlSearch.SelectedValue == "1")
        {
            user.FlushData();
            string condition = "and u.sFirstName= '" + txtSearch.Text + "' ";
            BindSearchResult(condition);
            //user.LoadSearchUsers_Bonus(DepartmentID, UserID, condition);
            //grdStaff.DataSource = user.DefaultView;
            //grdStaff.DataBind();

            //user.FlushData();
            //user.LoadSearchUsers_BonusPopUp(DepartmentID, UserID, condition);
            //grdBonusStaff.DataSource = user.DefaultView;
            //grdBonusStaff.DataBind();

            //user.FlushData();
            //user.LoadSearchManagers_Bonus(DepartmentID, condition);
            //grdManagers.DataSource = user.DefaultView;
            //grdManagers.DataBind();

            //user.FlushData();
            //user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
            //grdBonusManagers.DataSource = user.DefaultView;
            //grdBonusManagers.DataBind();
        }
        else if (ddlSearch.SelectedValue == "5")
        {
            user.FlushData();
            int gender = 0;
            if (txtSearch.Text.ToLower() == "man" || txtSearch.Text.ToLower() == "male" || txtSearch.Text.ToLower() == "men")
                gender = 1;
            else if (txtSearch.Text.ToLower() == "woman" || txtSearch.Text.ToLower() == "female" || txtSearch.Text.ToLower() == "women")
                gender = 2;
            if (gender != 0)
            {
                string condition = "and u.iGender = " + gender + " ";
                BindSearchResult(condition);
                //user.LoadSearchUsers_Bonus(DepartmentID, UserID, condition);
                //grdStaff.DataSource = user.DefaultView;
                //grdStaff.DataBind();

                //user.FlushData();
                //user.LoadSearchUsers_BonusPopUp(DepartmentID, UserID, condition);
                //grdBonusStaff.DataSource = user.DefaultView;
                //grdBonusStaff.DataBind();

                //user.FlushData();
                //user.LoadSearchManagers_Bonus(DepartmentID, condition);
                //grdManagers.DataSource = user.DefaultView;
                //grdManagers.DataBind();

                //user.FlushData();
                //user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
                //grdBonusManagers.DataSource = user.DefaultView;
                //grdBonusManagers.DataBind();
            }
        }

    }
    private void BindSearchResult(string condition)
    {
        tblUsers user = new tblUsers();
        user.FlushData();
        //user.LoadSearchUsers_Bonus(UserID,DepartmentID, condition);
        user.LoadSearchUsers_BonusPopUp(UserID, DepartmentID, condition);
        grdStaff.DataSource = user.DefaultView;
        grdStaff.DataBind();

        user.FlushData();
        user.LoadSearchUsers_BonusPopUp(UserID, DepartmentID, condition);
        grdBonusStaff.DataSource = user.DefaultView;
        grdBonusStaff.DataBind();

        user.FlushData();
        //user.LoadSearchManagers_Bonus(DepartmentID, condition);
        user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
        grdManagers.DataSource = user.DefaultView;
        grdManagers.DataBind();

        user.FlushData();
        user.LoadSearchManagers_BonusPopUp(DepartmentID, condition);
        grdBonusManagers.DataSource = user.DefaultView;
        grdBonusManagers.DataBind();

        ModalPopupExtender2.Show();
    }
    private void BindDropDownFilter()
    {
        imgBtnClearFilter.Visible = false;
        ddlCountry.Items.Clear();
        ddlSpeciality.Items.Clear();
        ddlYears.Items.Clear();

        tblCountries country = new tblCountries();
        country.GetAllCountriesAlphabetically();
        commonMethods.FillDropDownList(ddlCountry, country.DefaultView, "sCountry", "pkCountryID");
        ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));


        tblSpecialityType position = new tblSpecialityType();
        position.GetSpecialityTypesWithoutSeparator();
        commonMethods.FillDropDownList(ddlSpeciality, position.DefaultView, "sSpecialityName", "pkSpecialityTypeID");
        ddlSpeciality.Items.Insert(0, new ListItem("--Select Speciality--", "0"));

        for (int i = 0; i <= 74; i++)
        {
            ddlYears.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
        }
        ddlYears.Items.Insert(0, new ListItem("--Select Year--", "0"));

    }
    private void BindDropDownSearch()
    {
        ddlSearch.Items.Clear();
        ddlSearch.Items.Add(new ListItem("--Select Search Item--", "0"));
        ddlSearch.Items.Add(new ListItem("First Name", "1"));
        ddlSearch.Items.Add(new ListItem("Last Name", "2"));
        ddlSearch.Items.Add(new ListItem("Country", "3"));
        ddlSearch.Items.Add(new ListItem("Speciality", "4"));
        ddlSearch.Items.Add(new ListItem("Gender", "5"));
        txtSearch.Text = "";
    }
    protected void rdoActive_CheckedChanged(object sender, EventArgs e)
    {
        BindGridActive();
    }
    protected void rdoInActive_CheckedChanged(object sender, EventArgs e)
    {
        BindGridInActive();
    }
    protected void rdoAll_CheckedChanged(object sender, EventArgs e)
    {
        BindAllStaff_Managers();
    }
    protected void grdBonusManagers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int day;
            string mnonthName;

            tblUserBonuses bonuses = new tblUserBonuses();
            bonuses.GetFilterdUserBonuses(Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "pkUserID")));
            if (bonuses.RowCount > 0)
            {
                day = bonuses.DModifiedDate.Day;
                mnonthName = bonuses.DModifiedDate.ToString("MMMM");
                
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                lblDate.Text = day + " " + mnonthName + " " + bonuses.DModifiedDate.Year;

                Label lblBonus = e.Row.FindControl("lblBonus") as Label;
                lblBonus.Text = bonuses.Bonus;
            }
        }
    }
    protected void grdBonusStaff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int day;
            string mnonthName;

            tblUserBonuses bonuses = new tblUserBonuses();
            bonuses.GetFilterdUserBonuses(Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "pkUserID")));
            if (bonuses.RowCount > 0)
            {
                day = bonuses.DModifiedDate.Day;
                mnonthName = bonuses.DModifiedDate.ToString("MMMM");

                Label lblDate = e.Row.FindControl("lblDate") as Label;
                lblDate.Text = day + " " + mnonthName + " " + bonuses.DModifiedDate.Year;

                Label lblBonus = e.Row.FindControl("lblBonus") as Label;
                lblBonus.Text = bonuses.Bonus;
            }
        }
    }
}
