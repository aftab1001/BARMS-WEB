<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReceipts.aspx.cs" Inherits="DepartmentAdmin_PrintReceipts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div>
        <table width="100%" runat="server" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td>
                    <asp:GridView ID="grdReceipts" runat="server" AutoGenerateColumns="false" GridLines="None"
                        OnRowDataBound="grdReceipts_RowDataBound" Style="width: 100%">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table style="width: 700px" cellpadding="0" cellspacing="0" border="0">
                                        <tr style="height: 50px;">
                                            <td align="left">
                                                <asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hidUser" runat="server" Value="<%#Bind('pkuserid') %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
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
                                                            <div style="margin: 0 auto; text-align: center;">
                                                                <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text=""></asp:Label></div>
                                                            
                                                            <asp:GridView ID="grdPaymentForSingleUser" runat="server" AutoGenerateColumns="false"
                                                                Width="100%" EmptyDataText="No Record Found!." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdPaymentForSingleUser_RowDataBound"
                                                                Style="width: 100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblType" runat="server" Text="<%#Bind('Type') %>"></asp:Label>
                                                                            <asp:HiddenField ID="hidWeekStart" runat="server" Value="<%#Bind('dWeekStartDate') %>" />
                                                                            <asp:HiddenField ID="hidWeekEnd" runat="server" Value="<%#Bind('dWeekEndDate') %>" />
                                                                            <asp:HiddenField ID="hidUserID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                            <asp:LinkButton ID="lnkName" runat="server" Text="<%#Bind('FullName') %>" Visible="false"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSundayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSundaySalary" runat="server" Text="<%#Bind('Sunday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblMondayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMondaySalary" runat="server" Text="<%#Bind('Monday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblTuesdayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTuesdaySalary" runat="server" Text="<%#Bind('Tuesday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblWednesdayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWednesdaySalary" runat="server" Text="<%#Bind('Wednesday') %>"
                                                                                Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblThursdayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblThursdaySalary" runat="server" Text="<%#Bind('Thursday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblFridayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFridaySalary" runat="server" Text="<%#Bind('Friday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSaturdayHeader" runat="server"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSaturdaySalary" runat="server" Text="<%#Bind('Saturday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblTotal" runat="server" Text="Total" Style="position: relative; right: -17px;
                                                                                text-align: right;"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWeekSubtotal" runat="server" Style="margin-right: 17px;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                Payment:
                                                <asp:Label ID="lblTotal" runat="server" Style="padding-right: 17px; padding-left: 20px;"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
