<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintWorkShift.aspx.cs" Inherits="Managers_PrintWorkShift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>--:: WEST ::--</title>
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script type="text/javascript">
    function OpenFeedbackWindow(FeedBack) {
            var feedbackText;
            feedbackText = FeedBack;
            document.getElementById("divBalloon").innerHTML = feedbackText;
            ShowContent("divFeedbackWindow");
        }
        function CloseFeedBackWindow() {

            HideContent("divFeedbackWindow");
        }
        function postback() {
            __doPostBack('btn', '');
        }


    </script>

</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div id="divFeedbackWindow" style="display: none; position: absolute;">
        <%--<asp:Label ID="lblFeedBackWindow" runat="server"></asp:Label>--%>
        <div class="popup_left_bg">
        </div>
        <div class="popup_middle_bg">
            <div id="divBalloon">
            </div>
        </div>
        <div class="popup_right_bg">
        </div>
    </div>
    <div class="main" style="width: 100%;">
        <div class="admin_content" style="width: 100%;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="graybox_topleftcorner">
                    </td>
                    <td class="graybox_top_bg">
                    </td>
                    <td class="graybox_toprightcorner">
                    </td>
                </tr>
                <tr>
                    <td class="graybox_centerleftbg">
                    </td>
                    <td class="graybox_centermiddlebg">
                        <div id="ShowWorkshift" runat="server" style="display: none; margin: 0 auto; text-align: center;">
                            <div class="rounded_box_big">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="whitebox_topleftcorner">
                                        </td>
                                        <td class="whitebox_top_bg">
                                        </td>
                                        <td class="whitebox_toprightcorner">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="whitebox_centerleftbg">
                                        </td>
                                        <td class="whitebox_centermiddlebg">
                                            <div class="rounded_box_big">
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <table border="0" cellspacing="2" cellpadding="2" style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="8" align="center" valign="top">
                                                                        <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="headerstylenew">
                                                                    <td style="width: 12%;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Sunday
                                                                        <asp:Label ID="lblSundayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Monday
                                                                        <asp:Label ID="lblMondayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Tuesday
                                                                        <asp:Label ID="lblTuesdayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Wednesday
                                                                        <asp:Label ID="lblWednesdayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Thursday
                                                                        <asp:Label ID="lblThursdayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Friday
                                                                        <asp:Label ID="lblFridayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td style="width: 12%;">
                                                                        Saturday
                                                                        <asp:Label ID="lblSaturdayDate" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <table id="tblWork" runat="server" style="width: 100%;">
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td class="whitebox_centerrightbg">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="whitebox_bottomleftcorner">
                                        </td>
                                        <td class="whitebox_bottom_bg">
                                        </td>
                                        <td class="whitebox_bottomrightcorner">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--Previous week off days--%>
                            <div class="clear_10">
                            </div>
                            <div style="margin: 0 auto; width: 430px; text-align: center; display: none;">
                                <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Staff Off days for previous week"></asp:Label>
                            </div>
                            <div class="clear_10">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="display: none;">
                                <tr>
                                    <td class="whitebox_topleftcorner">
                                    </td>
                                    <td class="whitebox_top_bg">
                                    </td>
                                    <td class="whitebox_toprightcorner">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="whitebox_centerleftbg">
                                    </td>
                                    <td class="whitebox_centermiddlebg">
                                        <asp:DataList ID="DLPreviousOffdays" runat="server" RepeatDirection="Horizontal"
                                            RepeatColumns="7" OnItemDataBound="DLPreviousOffdays_ItemDataBound" Width="100%">
                                            <ItemTemplate>
                                                <table style="background: #d6efff; width: 100%;" cellpadding="1" cellspacing="1"
                                                    border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblName" runat="server" Text="" CssClass="alternate_row_new"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td class="whitebox_centerrightbg">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="whitebox_bottomleftcorner">
                                    </td>
                                    <td class="whitebox_bottom_bg">
                                    </td>
                                    <td class="whitebox_bottomrightcorner">
                                    </td>
                                </tr>
                            </table>
                            <%--OffDay Grids--%>
                            <div class="clear_10">
                            </div>
                            <div style="margin: 0 auto; width: 430px;">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="DAY OFF"></asp:Label>
                            </div>
                            <div class="clear_10">
                            </div>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="whitebox_topleftcorner">
                                    </td>
                                    <td class="whitebox_top_bg">
                                    </td>
                                    <td class="whitebox_toprightcorner">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="whitebox_centerleftbg">
                                    </td>
                                    <td class="whitebox_centermiddlebg">
                                        <div class="rounded_box_big">
                                            <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 1px solid #CACACA">
                                                <tr class="headerstylenew">
                                                    <%--<td style="background: #fff; border-right: solid 1px #d6efff;">
                                    &nbsp;
                                </td>--%>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Sunday
                                                        <asp:Label ID="lblSundayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Monday
                                                        <asp:Label ID="lblMondayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Tuesday
                                                        <asp:Label ID="lblTuesdayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Wednesday
                                                        <asp:Label ID="lblWednesdayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Thursday
                                                        <asp:Label ID="lblThursdayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Friday
                                                        <asp:Label ID="lblFridayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="border: 2px solid #FFFFFF">
                                                        Saturday
                                                        <asp:Label ID="lblSaturdayDate1" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td style="border-right: solid 1px #d6efff;">
                                    &nbsp;
                                </td>--%>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdSunday" AllowSorting="true" AllowPaging="True" ShowHeader="false"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdSunday_RowDataBound">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdMonday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" OnRowDataBound="grdMonday_RowDataBound">
                                                            <Columns>
                                                                <%-- <asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdTuesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" OnRowDataBound="grdTuesday_RowDataBound">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdWednesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" OnRowDataBound="grdWednesday_RowDataBound">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdThursday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" OnRowDataBound="grdThursday_RowDataBound">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdFriday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" OnRowDataBound="grdFriday_RowDataBound">
                                                            <Columns>
                                                                <%-- <asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="align_left" valign="top">
                                                        <asp:GridView ID="grdSaturday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                            runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                            AlternatingRowStyle-CssClass="alternate_row_print" RowStyle-CssClass="rowstyle_print"
                                                            Width="100%" CellPadding="3" CellSpacing="2" BorderStyle="None" BorderWidth="0"
                                                            GridLines="None" OnRowDataBound="grdSaturday_RowDataBound">
                                                            <Columns>
                                                                <%--<asp:BoundField DataField="FullName" />--%>
                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label>
                                                                        <div id="mydiv" runat="server" style="float: right;">
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="3%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                    <td class="whitebox_centerrightbg">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="whitebox_bottomleftcorner">
                                    </td>
                                    <td class="whitebox_bottom_bg">
                                    </td>
                                    <td class="whitebox_bottomrightcorner">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td class="graybox_centerrightbg">
                    </td>
                </tr>
                <tr>
                    <td class="graybox_bottomleftcorner">
                    </td>
                    <td class="graybox_bottom_bg">
                    </td>
                    <td class="graybox_bottomrightcorner">
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btn" runat="server" OnClick="btn_Click" UseSubmitBehavior="false" style="display:none;" />
    </div>
    </form>
</body>
</html>
