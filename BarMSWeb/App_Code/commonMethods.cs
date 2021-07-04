using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
using System.Collections;
using MyGeneration.dOOdads;
using LC.Model.BMS.BLL;



/// <summary>
/// Summary description for commonMethods
/// </summary>
public class commonMethods : System.Web.UI.Page 
{
    public commonMethods()
    {

    }

   //public commonMethods()
   //{
   //    this.Init += new EventHandler(Page_Init);
   //}

   //private void Page_Init(System.Object sender, System.EventArgs e)
   //{
   //    TransactionMgr.ThreadTransactionMgrReset();
   //} 

    public static string GetFormatedString(double value)
    {
        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
        nfi.NumberGroupSeparator = " ";
        string strFormated = string.Empty;
        strFormated = value.ToString("N2", nfi);
        if (strFormated.Contains(".00"))
        {
            strFormated = strFormated.Substring(0, strFormated.IndexOf("."));
        }
        return strFormated;
    }

    public static string ReadHtmlSource(string Path)
    {
        StreamReader reader = new StreamReader(Path);
        return reader.ReadToEnd();
    }
    public static string Give_UniqueName_ForUpload()
    {

        return DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Today.Year + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond;

    }

    public static void FillListBox(ListBox listbox,
        DataView dataView, string displayMember, string valueMember)
    {
        listbox.DataTextField = displayMember;
        listbox.DataValueField = valueMember;
        listbox.DataSource = dataView;
        listbox.DataBind();
    }

    public static void FillDropDownList(DropDownList dropDownList,
        DataView dataView, string displayMember, string valueMember)
    {
        dropDownList.DataTextField = displayMember;
        dropDownList.DataValueField = valueMember;
        dropDownList.DataSource = dataView;
        dropDownList.DataBind();
    }

    ///  Fill DropDownList
   

    public static void FillDropDownList(ListBox listbox,
        DataView dataView, string displayMember, string valueMember)
    {
        listbox.DataTextField = displayMember;
        listbox.DataValueField = valueMember;
        listbox.DataSource = dataView;
        listbox.DataBind();
    }
    public static void FillCheckListBox(CheckBoxList listbox,
     DataView dataView, string displayMember, string valueMember)
    {
        listbox.DataTextField = displayMember;
        listbox.DataValueField = valueMember;
        listbox.DataSource = dataView;
        listbox.DataBind();
    }


    public static void FillRadioButtonList(RadioButtonList listbox, DataView dataView, string displayMember, string valueMember)
    {
        listbox.DataTextField = displayMember;
        listbox.DataValueField = valueMember;
        listbox.DataSource = dataView;
        listbox.DataBind();
    }

    public static string GetHTML(string URL)
    {
        // Create a request for the URL. 		
        //cell-mlsRequest request = WebRequest.Create (URL);
        //request.Credentials = CredentialCache.DefaultCredentials;
        //HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
        // Get the stream containing content returned by the server.
        //Stream dataStream = response.GetResponseStream ();       

        // Get HTML data
        WebClient client = new WebClient();
        client.Credentials = CredentialCache.DefaultCredentials;
        Stream data = client.OpenRead(URL);

        StreamReader reader = new StreamReader(data);
        string responseFromServer = reader.ReadToEnd();

        // Cleanup the streams and the response.

        reader.Close();
        data.Close();

        return responseFromServer;
    }
    public static string GetApplicationPath()
    {
        return System.Configuration.ConfigurationSettings.AppSettings["RootPath"].ToString();
    }

    public static string GetPageTitle()
    {
        return System.Configuration.ConfigurationSettings.AppSettings["PageTitle"].ToString();
    }

    public static string ResolveUrl(string urlFormat, params object[] args)
    {
        return ResolveUrl(String.Format(urlFormat, args));
    }

    public static string ResolveUrl(string url)
    {
        if (String.IsNullOrEmpty(url))
            return String.Empty;
        else
        {
            string[] urlParts = url.Split('?');
            string absoluteUrl = VirtualPathUtility.ToAbsolute(urlParts[0]);

            if (urlParts.Length == 2)
                return String.Concat(absoluteUrl, '?', urlParts[1]);
            else
                return absoluteUrl;
        }
    }

    public static void Set_CheckBoxListValues(ref CheckBoxList ChkBoxList, DataTable Dt)
    {
        int i;
        int j, CBoxVal;
        int DtVal;

        for (i = 0; i < ChkBoxList.Items.Count; i++)
        {
            CBoxVal = Convert.ToInt16(ChkBoxList.Items[i].Value);

            for (j = 0; j < Dt.Rows.Count; j++)
            {
                DtVal = Convert.ToInt16(Dt.Rows[j]["FeatureID"]);

                if (CBoxVal == DtVal)
                {
                    ChkBoxList.Items[i].Selected = true;
                }
            }

        }

    }


    public static void Export_DataTable_ToCSV(ref DataTable DT, string CSVFileName, System.Web.HttpResponse Response)
    {

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + CSVFileName + ".csv");
        Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.Charset = "windows-1252";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "text/plain";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

        ////// string fileName = @"c:\csv\MyEmailAddressbook.csv";
        //////StreamWriter writer = File.CreateText(fileName);

        foreach (DataRow dr in DT.Rows)
        {
            foreach (DataColumn dc in dr.Table.Columns)
            {
                Response.Write(dr[dc]);
                Response.Write(",");
            }

            Response.Write("\n");
        }

        Response.End();

        //MessageBox.Show("File Created");
        //writer.Close();


    }


    public static bool IsDate(string anyString)
    {
        if (anyString == null)
        {
            anyString = "";
        }
        
        if (anyString.Length > 0)
        {
            DateTime dummyDate;
           
            try
            {
                dummyDate = DateTime.Parse(anyString);
            }

            catch
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    public static void GiveAccessRights(string path)
    {
        String UserAccount = Environment.UserDomainName + "\\" + Environment.UserName;
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        System.Security.AccessControl.DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
        directorySecurity.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(UserAccount, System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));
        directoryInfo.SetAccessControl(directorySecurity);
    }
    public static int GetWeeknumber(DateTime Date)
    {
        CultureInfo ciCurr = CultureInfo.CurrentCulture;
        int weekNum = ciCurr.Calendar.GetWeekOfYear(Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        return weekNum;

    }

    public static DateTime GetWeekStartDate(int year, int week)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int day = (int)jan1.DayOfWeek - 1;
        int delta = (day < 4 ? -day : 7 - day) + 7 * (week - 1);

        return jan1.AddDays(delta);
    }
    public static int GetWeekNumber_New(DateTime dtPassed)
    {
        CultureInfo ciCurr = CultureInfo.CurrentCulture;
        int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        return weekNum;
    }
    #region Removing base address of facebook and twitter
    public static string filterFacebookURL(string facebookURL)
    {
        if (facebookURL.ToLower().IndexOf("https:") != -1)
            facebookURL = facebookURL.Replace("https:", "").Replace("HTTPS:", "");
        if (facebookURL.ToLower().IndexOf("http:") != -1)
            facebookURL = facebookURL.Replace("http:", "").Replace("HTTP:", "");
        if (facebookURL.IndexOf("//") != -1)
            facebookURL = facebookURL.Replace("//", "");
        if (facebookURL.ToLower().IndexOf("www.") != -1)
            facebookURL = facebookURL.Replace("www.", "").Replace("WWW.", "");
        if (facebookURL.ToLower().IndexOf("facebook.") != -1)
            facebookURL = facebookURL.Replace("facebook.", "").Replace("FACEBOOK.", "");
        if (facebookURL.ToLower().IndexOf("com/") != -1)
            facebookURL = facebookURL.Replace("com/", "").Replace("COM/", "");
        return facebookURL;
    }
    public static string filterTwitterURL(string twitterURL)
    {
        if (twitterURL.ToLower().IndexOf("https:") != -1)
            twitterURL = twitterURL.Replace("https:", "").Replace("HTTPS:", "");
        if (twitterURL.ToLower().IndexOf("http:") != -1)
            twitterURL = twitterURL.Replace("http:", "").Replace("HTTP:", "");
        if (twitterURL.IndexOf("//") != -1)
            twitterURL = twitterURL.Replace("//", "");
        if (twitterURL.ToLower().IndexOf("www.") != -1)
            twitterURL = twitterURL.Replace("www.", "").Replace("WWW.", "");
        if (twitterURL.ToLower().IndexOf("twitter.") != -1)
            twitterURL = twitterURL.Replace("twitter.", "").Replace("TWITTER.", "");
        if (twitterURL.ToLower().IndexOf("com/") != -1)
            twitterURL = twitterURL.Replace("com/", "").Replace("COM/", "");
        if (twitterURL.IndexOf("#!/") != -1)
            twitterURL = twitterURL.Replace("#!/", "");
        return twitterURL;
    }
    public static int getDay(DateTime date)
    {
        int day;

        switch (date.DayOfWeek.ToString().ToLower())
        {
            case "sunday":
                day = 1;
                break;
            case "monday":
                day = 2;
                break;
            case "tuesday":
                day = 3;
                break;
            case "wednesday":
                day = 4;
                break;
            case "thursday":
                day = 5;
                break;
            case "friday":
                day = 6;
                break;
            case "saturday":
                day = 7;
                break;
            default:
                day = 0;
                break;
        }
       
        return day;
    }
    #endregion

    public static double ChangeToUS(string value)
    {
        value = value.Trim();

        string retValue = string.Empty;

        string[] tempArray;
        string[] BeforeComma;
        string AfterComma = string.Empty;

        tempArray = value.Split(',');

        if (tempArray.Length > 1)
        {
            BeforeComma = tempArray[0].Split('.');
            AfterComma = tempArray[1].ToString();
        }
        else
        {
            BeforeComma = tempArray[0].Split('.');
        }
        if (BeforeComma.Length == 1)
        {
            if(BeforeComma[0].ToString() == "0")
                retValue += "00,";
            else
                retValue += BeforeComma[0] + ',';
        }
        else
        {
            for (int i = 0; i <= BeforeComma.Length - 1; i++)
                retValue += BeforeComma[i] + ',';
        }
        
        retValue = retValue.Substring(0, retValue.LastIndexOf(',')) + '.';
        
        if (tempArray.Length > 1)
            retValue += AfterComma;
        else
            retValue += "00";
        return Math.Round(Convert.ToDouble(retValue), 2);
    }
    public static string ChangetToUK(string value)
    {
        value = value.Trim();

        string retValue = string.Empty;

        string[] tempArray;
        string[] BeforeDot;
        string AfterDot = string.Empty;

        tempArray = value.Split('.');

        if (tempArray.Length > 1)
        {
            BeforeDot = tempArray[0].Split(',');
            AfterDot = tempArray[1].ToString();
        }
        else
            BeforeDot = tempArray[0].Split(',');

        if (BeforeDot.Length == 1)
        {
            if (BeforeDot[0].ToString() == "0")
                retValue += "00.";
            else
                retValue += BeforeDot[0] + '.';
        }
        else
        {
            for (int i = 0; i <= BeforeDot.Length - 1; i++)
                retValue += BeforeDot[i] + '.';
        }
        
        retValue = retValue.Substring(0, retValue.LastIndexOf('.')) + ',';
        
        if (tempArray.Length > 1)
            retValue += AfterDot;
        else
            retValue += "00";
        return retValue;        
    }

    
    

   
}