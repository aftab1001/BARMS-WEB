using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LC.Model.BMS.BLL;

/// <summary>
/// Summary description for GetProducts
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class GetProducts : System.Web.Services.WebService
{

    public GetProducts()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetProductNames(string prefixText, int count)
    {
        List<string> str = new List<string>();
        tblProducts p = new tblProducts();
        string filter = " and p.sproductname like '%" + prefixText + "%'";
        p.GetProducts_Autocomplete(1, filter);
        if (p.RowCount > 0)
        {
            for (int i = 0; i < p.RowCount; i++)
            {
                str.Add(p.GetColumn("sproductname").ToString());
                p.MoveNext();
            }
        }
        //string[] productarray = "";
        return str.ToArray();
    }

}

