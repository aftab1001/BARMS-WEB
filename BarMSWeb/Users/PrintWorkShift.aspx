<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="PrintWorkShift.aspx.cs"
    Inherits="Users_PrintWorkShift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>--:: WEST ::--</title>
    
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:none;">
    <form id="form1" runat="server">
    <div>
        <div class="clear_10">
        </div>
        <div class="clear_10">
        </div>
        <div class="clear_10">
        </div>
        <div style="float: left; width: 100%; text-align: center;" class="bold_label">
            <asp:Label ID="lblName" runat="server" style="float:left;margin-left:27px;color:Gray;"></asp:Label> <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
        </div>
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
                    <div class="graybox_big">
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
                                        <table border="0" cellspacing="1" cellpadding="0" class="table_border">
                                            <tr class="headerstyle">
                                                <td class="align_center">
                                                    Sunday
                                                </td>
                                                <td class="align_center">
                                                    Monday
                                                </td>
                                                <td class="align_center">
                                                    Tuesday
                                                </td>
                                                <td class="align_center">
                                                    Wednesday
                                                </td>
                                                <td class="align_center">
                                                    Thursday
                                                </td>
                                                <td class="align_center">
                                                    Friday
                                                </td>
                                                <td class="align_center">
                                                    Saturday
                                                </td>
                                            </tr>
                                            <tr class="alternate_row_userprint">
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserSunday" runat="server" Text="Day OFF"></asp:Label>
                                                </td>
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserMonday" runat="server" Text="Day OFF"></asp:Label>
                                                </td>
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserTuesday" runat="server" Text="Day OFF"></asp:Label>
                                                </td>
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserWednesday" runat="server" Text="Day OFF"></asp:Label>
                                                </td>
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserThursday" runat="server" Text="Day OFF"></asp:Label>
                                                </td>
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserFriday" runat="server" Text="Day OFF"></asp:Label>
                                                </td>
                                                <td class="align_center">
                                                    <asp:Label ID="lblUserSaturday" runat="server" Text="Day OFF"></asp:Label>
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
                        <div class="clear_10">
                        </div>
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
        <div class="clear_10">
        </div>
    </div>
    </form>
</body>
</html>
