<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="transaction.aspx.cs" Inherits="DepartmentAdmin_transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
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
                
                    Transactions
                    <div class="rounded_box">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                        <div class="clear_10">
                        </div>
                        <asp:GridView ID="grdTransactions" runat="server" AutoGenerateColumns="false" Width="100%"
                            EmptyDataText="No Transaction added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                            OnRowDataBound="grdTransactions_RowDataBound" OnRowCommand="grdTransactions_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text="<%#Bind('dModifiedDate') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department Admin">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDepartmentAdmin" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransactionType" runat="server" Text="<%#Bind('TransactionType') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notes">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNotes" runat="server" Text="<%#Bind('Notes') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('Amount') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Received">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgActivate" runat="server" CommandName="edt" CommandArgument="<%#Bind('pkTransactionID') %>"
                                            ImageUrl="../Images/close.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
</asp:Content>
