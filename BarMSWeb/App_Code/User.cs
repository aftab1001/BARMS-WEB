using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for User
/// //In SessionUser Class MemberType 1 for Employer
//In SessionUser Class MemberType 2 for Exceutive Member
//In SessionUser Class MemberType 3 for Student
/// </summary>
public class SessionUser
{
    
    private int userID;
    private int accessLevel;
    private int departmentID;


    public int UserID
    {
        get
        {
            return userID;
        }
        set
        {
            userID = value;
        }
    }
    public int AccessLevel
    {
        get
        {
            return accessLevel;
        }
        set
        {
            accessLevel = value;
        }
    }
    public int DepartmentID
    {
        get
        {
            return departmentID;
        }
        set
        {
            departmentID = value;
        }
    }

    public SessionUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
