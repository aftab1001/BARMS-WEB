using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GetHtml
/// </summary>
public static class  GetHtml
{
    

    public static string divHtml;

    public static void setHtml(string html)
    {
        divHtml = html;
    }
    public static string getDivContent()
    {
        return divHtml;
    }
}
