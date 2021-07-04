<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pdfGenerator.aspx.cs" Inherits="pdfGenerator" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="Ajaxified" Assembly="Ajaxified" Namespace="Ajaxified" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <link href="JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.min.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    

    <script type="text/javascript">
        //        $(function() {
        //            $('#clickme').click(function() {
        //                $('#divImage').animate({
        //                    height: 'toggle'
        //                }, 1000, function() {
        //                    // Animation complete.
        //                });
        //            });

        //            $.jqplot('chartdiv', [[[1, 2], [3, 5.12], [5, 13.1], [7, 33.6], [9, 85.9], [11, 219.9]]]);
        //        });
        $(function() { });
        function postback() {
            __doPostBack('btn', '');
        }
        function printReport() {
            var a = window.open("../pdfGenerator.aspx?r=ps", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" UseSubmitBehavior="false"
        Style="display: none;" />
    <table id="tblSampleText" runat="server" align="center">
    </table>
    
    <asp:Button ID="btn" runat="server" OnClick="btn_Click" UseSubmitBehavior="false"
        Style="display: none;" />
    
    </form>
</body>
</html>
