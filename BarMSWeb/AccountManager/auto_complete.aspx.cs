using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AccountManager_auto_complete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["q"])) // remember, 'q' is the query the autosuggest will pass
        {
            int q = 0;
            if (Int32.TryParse(Request.QueryString["q"], out q))
            {
                for (int i = q; i < 1001; i++)
                    if (i.ToString().Contains(q.ToString())) Response.Write(i + Environment.NewLine);
            }
            else
                Response.Write("Not a number fool!");
        }
    }
}
