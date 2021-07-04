<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintStats.aspx.cs" Inherits="AccountManager_PrintStats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>
    <script type="text/javascript">
        function postback() {
            __doPostBack('btn', '');
        }
    </script>
      
    
</head>

<body style="background: transparent;">
    <form id="form1" runat="server">
    <div style="width: 100%;">
        <div id="content" runat="server" style="float: left;">
        </div>
    </div>
    <table>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:Button ID="btn" runat="server" OnClick="btn_Click" UseSubmitBehavior="false" style="display:none;" />
    </form>
</body>
</html>
