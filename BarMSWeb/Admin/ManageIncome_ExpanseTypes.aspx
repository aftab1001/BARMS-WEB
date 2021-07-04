<%@ Page Language="C#" MasterPageFile="~/MasterPages/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageIncome_ExpanseTypes.aspx.cs" Inherits="Admin_ManageIncome_ExpanseTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/btnaddincome.png"
        OnClick="btnAdd_Click" />
    <asp:Label ID="lblError" runat="server" Visible="false" CssClass="Error" Text="Income Type already Exist"></asp:Label>
    <div id="divIncome" runat="server" visible="false">
        <table cellpadding="2" cellspacing="2" style="width: 100%;" border="0">
            <tr>
                <td width="150">
                    <asp:Label ID="lbluser" runat="server" Text="Add IncomeType"></asp:Label>
                </td>
                <td width="204">
                    <div class="textbox204">
                        <asp:TextBox ID="txtIncomeType" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="req" ControlToValidate="txtIncomeType" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td align="left">
                    <asp:HiddenField ID="hdnID" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="150">
                    Description:
                </td>
                <td colspan="2">
                    <div class="textboxmulti">
                        <asp:TextBox ID="txtIncomeDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="150">
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_save.png" BgColor="Transparent" ValidationGroup="req"
                        OnClick="btnSave_Click" />
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/btn_edit_admin.png" ValidationGroup="req"
                        BgColor="Transparent" OnClick="btnEdit_Click" Visible="false" />
                    <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/btn_cancel.png"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                </td>
            </tr>
        </table>
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
                <div class="rounded_box">
                    Income Types
                    <div class="clear_10">
                    </div>
                    <asp:GridView ID="grdIncomeTypes" AllowSorting="True" AllowPaging="True" runat="server"
                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                        BorderWidth="0" GridLines="None" OnRowCommand="grdIncomeTypes_RowCommand" OnRowDataBound="grdIncomeTypes_RowDataBound">
                        <Columns>
                            <%--<asp:BoundField DataField="sIncomType" HeaderText="Name" ItemStyle-Width="100px" />--%>
                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkIncomeType" runat="server" Text="<%#Bind('sIncomType') %>"
                                        CommandName="Change" CommandArgument='<%# Bind("pkIncomeTypeID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <div id="divDescription" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                        <tr>
                                            <%--<td class="align_left" style="width:170px;">
                                    <asp:Label ID="lblAccess" runat="server"></asp:Label></td>--%>
                                            <%-- <td class="align_right" style="padding-right:10px;">
                                     <asp:ImageButton ID="imgedit" runat="server" Visible="false" CommandName="Change" CommandArgument='<%# Bind("pkIncomeTypeID") %>'
                                        ImageUrl="../images/edit.png" ToolTip="Edit" /></td>--%>
                                            <%--<td class="align_right" style="width: 20px;">
                                    <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkIncomeTypeID") %>'
                                        ImageUrl="../images/close.png" ToolTip="Delete" />
                                            </td>--%>
                                            <td class="align_right" style="width: 25px;">
                                                <asp:ImageButton ID="imgBtnActive" runat="server" CommandName="active" CommandArgument='<%# Bind("pkIncomeTypeID") %>'
                                                    ImageUrl="~/Images/activate_icon.gif"  />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
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
    <div class="clear_30">
    </div>
    <div style="height: 1px; background: #ccc; width: 100%;">
    </div>
    <div class="clear_30">
    </div>
    <%-- <asp:Button ID="btnAddExpense" runat="server" Text="Add ExpenseType" OnClick="btnAddExpense_Click" />--%>
    <asp:ImageButton ID="btnAddExpense" runat="server" ImageUrl="~/Images/btn_addexponse.png"
        OnClick="btnAddExpense_Click" />
    <asp:Label ID="lblError1" runat="server" Visible="false" CssClass="Error" Text="Expense Type already Exist"></asp:Label>
    <div id="divExpense" runat="server" visible="false">
        <table cellpadding="2" cellspacing="2" style="width: 100%;" border="0">
            <tr>
                <td width="150">
                    <asp:Label ID="lblExpanse" runat="server" Text="Add Expanse Type"></asp:Label>
                </td>
                <td width="204">
                    <div class="textbox204">
                        <asp:TextBox ID="txtExpansType" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="req" ControlToValidate="txtExpansType" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td align="left">
                    <asp:HiddenField ID="hdnID1" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="150">
                    Description:
                </td>
                <td colspan="2">
                    <div class="textboxmulti">
                        <asp:TextBox ID="txtExpenseDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="150">
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:ImageButton ID="btnSave1" runat="server" ImageUrl="~/Images/btn_save.png" BgColor="Transparent" ValidationGroup="req"
                        OnClick="btnSave1_Click" />
                    <asp:ImageButton ID="btnEdit1" runat="server" ImageUrl="~/Images/btn_edit_admin.png" ValidationGroup="req"
                        BgColor="Transparent" OnClick="btnEdit1_Click" Visible="false" />
                    <%--<asp:Button ID="btnCancelExpense" runat="server" Text="Cancel" OnClick="btnCancelExpense_Click" />--%>
                    <asp:ImageButton ID="btnCancelExpense" runat="server" ImageUrl="~/Images/btn_cancel.png" 
                        OnClick="btnCancelExpense_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                </td>
            </tr>
        </table>
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
                <div class="rounded_box">
                    Expense Types
                    <div class="clear_10">
                    </div>
                    <asp:GridView ID="grdExpansTypes" AllowSorting="True" AllowPaging="True" runat="server"
                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                        BorderWidth="0" GridLines="None" OnRowCommand="grdExpansTypes_RowCommand" OnRowDataBound="grdExpansTypes_RowDataBound">
                        <Columns>
                            <%--<asp:BoundField DataField="sExpanseCategory" HeaderText="Name" ItemStyle-Width="100px" />--%>
                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExpense" runat="server" Text="<%#Bind('sExpanseCategory') %>"
                                        CommandArgument='<%# Bind("pkExpanseCategoryID") %>' CommandName="Change"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <div id="divDescription" runat="server">
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                        <tr>
                                            <%--<td class="align_left" style="width:170px;">
                                    <asp:Label ID="lblAccess" runat="server"></asp:Label></td>--%>
                                            <%--<td class="align_right" style="padding-right: 10px;">
                                                <asp:ImageButton ID="imgedit" runat="server" CommandName="Change" CommandArgument='<%# Bind("pkExpanseCategoryID") %>'
                                                    ImageUrl="../images/edit.png" ToolTip="Edit" />
                                            </td>--%>
                                            <%--<td class="align_right" style="width: 20px;">
                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkExpanseCategoryID") %>'
                                                    ImageUrl="../images/close.png" ToolTip="Delete" />
                                            </td>--%>
                                            <td class="align_right" style="width: 25px;">
                                                <asp:ImageButton ID="imgBtnActive" runat="server" CommandName="active" CommandArgument='<%# Bind("pkExpanseCategoryID") %>'
                                                    ImageUrl="~/Images/activate_icon.gif"  />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Width="3%" />
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
</asp:Content>
