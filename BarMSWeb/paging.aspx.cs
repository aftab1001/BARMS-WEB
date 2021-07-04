using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class paging : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadUsers();

        }
    }
    private void loadUsers()
    {
        tblUsers u = new tblUsers();
        u.LoadAll();
        grdData.DataSource = u.DefaultView;
        grdData.DataBind();
    }
}
