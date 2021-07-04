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

public partial class MasterPages_UserMaster : System.Web.UI.MasterPage
{
    int UserID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            UserID = user.UserID;
        }
        if (!IsPostBack)
        {
            BindInBox();
            //ankMessages.Style.Add(HtmlTextWriterStyle.Color, "black");
            //HtmlGenericControl span = new HtmlGenericControl("span");
            //span.Style.Add("color", "Green");
            //span.Style.Add("font-size", "20px;");
            //ankMessages.Controls.Add(span);
            //span.Attributes.Add("onmouseover", "");

        }
       

    }
    private void BindInBox()
    {
        int count = 0;
        tblUserInBox userIn = new tblUserInBox();
        userIn.GetAllReveivedMessages(UserID);
        if (userIn.RowCount > 0)
        {
            for (int i = 0; i < userIn.RowCount; i++)
            {
                if (!Convert.ToBoolean(userIn.GetColumn("bIsRead")))
                {
                    count++;
                    ankMessages.InnerHtml = "<span style='float:left;'> My Messages </span><span class='span'>(" + count.ToString() + " New)</span>";

                }
                userIn.MoveNext();
            }
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("../West_Login.aspx");
    }
}
