<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="BonusDoc.aspx.cs" Inherits="DepartmentAdmin_BonusDoc"
    ValidateRequest="false" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function ChangeColor(element) {
            element.style.background = '#e8e8e8';
        }
        function DefaultColor(element) {
            element.style.background = 'transparent';
        }
        function FixColor(element) {
            element.style.background = '#e8e8e8';
        }
        
    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table cellpadding="2" cellspacing="2" border="0" align="center" style="font-weight: bold;">
            <tr>
                <td>
                    <asp:RadioButton ID="rdoActive" runat="server" GroupName="Active" AutoPostBack="true"
                        Text="Active" OnCheckedChanged="rdoActive_CheckedChanged" />
                </td>
                <td>
                    <asp:RadioButton ID="rdoInActive" runat="server" GroupName="Active" AutoPostBack="true"
                        Text="InActive" OnCheckedChanged="rdoInActive_CheckedChanged" />
                </td>
                <td>
                    <asp:RadioButton ID="rdoAll" runat="server" Checked="true" GroupName="Active" AutoPostBack="true"
                        Text="All" OnCheckedChanged="rdoAll_CheckedChanged" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                <div style="position:absolute;margin-top:-31px;">
                    <a href="ManageUsers.aspx" style="color: Blue; font-size: 15px;">Manage Users</a>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Filter Bonus:
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    Users:
                </td>
                <td>
                    <div class="textbox204">
                        <asp:DropDownList ID="ddlusers" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Year:
                </td>
                <td>
                    <div class="textbox204">
                        <asp:DropDownList ID="ddlYears" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Specialties:
                </td>
                <td>
                    <div class="textbox204">
                        <asp:DropDownList ID="ddlSpeciality" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Country:
                </td>
                <td style="width: 230px; float: right;">
                    <div class="textbox204">
                        <asp:DropDownList ID="ddlCountry" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:ImageButton ID="imgBtnFilterSpecialty" runat="server" ImageUrl="~/Images/btn_filter.png"
                        OnClick="imgBtnFilterSpecialty_Click" />
                    <asp:ImageButton ID="imgBtnClearFilter" runat="server" Visible="false" ImageUrl="~/Images/btn_clearfilter.png"
                        OnClick="imgBtnClearFilter_Click" />
                </td>
            </tr>
            <tr style="height: 15px;">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Search By Item:
                </td>
                <td style="width: 230px;">
                    <div class="textbox204" style="float: left;">
                        <asp:DropDownList ID="ddlSearch" runat="server">
                        </asp:DropDownList>
                    </div>
                    <span style="float: left; margin-top: 10px; margin-left: 5px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlSearch"
                            ErrorMessage="*" ValidationGroup="na" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                    </span>
                </td>
                <td>
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="imgBtnSearch">
                        <div class="textbox204" style="float: left;">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        </div>
                        <span style="float: left; margin-top: 10px; margin-left: 5px; width: 10px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearch"
                                ErrorMessage="*" ValidationGroup="na" Display="Dynamic"></asp:RequiredFieldValidator>
                        </span><span style="float: left; margin-left: 5px; margin-top: 5px;">
                            <asp:ImageButton ID="imgBtnSearch" runat="server" ImageUrl="~/Images/search_icon.png"
                                ValidationGroup="na" OnClick="imgBtnSearch_Click" />
                        </span>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
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
                                    Managers:<br />
                                    <asp:GridView ID="grdManagers" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                        BorderWidth="0" OnRowCommand="grdManagers_RowCommand" EmptyDataText="Sorry! No Record Found"
                                        OnRowDataBound="grdManagers_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkName" runat="server" Text="<%#Bind('FullName') %>" CommandName="name"
                                                        CommandArgument="<%#Bind('PkUserID') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" ImageUrl="~/Images/activate_icon.gif"
                                                        CommandName="active" CommandArgument="<%#Bind('pkuserid') %>" />
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
                </td>
            </tr>
            <tr>
                <td>
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
                                    Staff:<br />
                                    <asp:GridView ID="grdStaff" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                        BorderWidth="0" OnRowCommand="grdStaff_RowCommand" EmptyDataText="Sorry! No Record Found"
                                        OnRowDataBound="grdStaff_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkName" runat="server" Text="<%#Bind('FullName') %>" CommandName="name"
                                                        CommandArgument="<%#Bind('PkUserID') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bonus Activation">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgActive" runat="server" ImageUrl="~/Images/activate_icon.gif" />
                                                    <asp:ImageButton ID="imgActiveUser" runat="server" ImageUrl="~/Images/activate_icon.gif" style="display:none;"
                                                        CommandName="activeUser" CommandArgument="<%#Bind('pkuserid') %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgBtnActive" runat="server" ImageUrl="~/Images/activate_icon.gif"
                                                        CommandName="active" CommandArgument="<%#Bind('pkuserid') %>" />
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
                </td>
            </tr>
            <tr>
                <td>
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
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                Bonus:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <FTB:FreeTextBox ID="FreeTextBox1" Focus="true" SupportFolder="FreeTextBox/" JavaScriptLocation="ExternalFile"
                                                    ButtonImagesLocation="ExternalFile" ToolbarImagesLocation="ExternalFile" ToolbarStyleConfiguration="OfficeXP"
                                                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,                                   

FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,

Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,

JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;

CreateLink,Unlink,InsertImageFromGallery|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,

StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;

InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,

DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,

InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,

InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell" runat="Server" GutterBackColor="red" DesignModeCss="designmode.css"
                                                    ButtonSet="Office2000" ImageGalleryPath="~/images/" />
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
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="imgbtnSubmit" runat="server" ImageUrl="~/Images/btn_save.png"
                        OnClick="imgbtnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnExtent2"
        PopupControlID="pnlBonus" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent2" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlBonus" runat="server" Style="width: 990px;">
        <asp:UpdatePanel ID="upnlBonus" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 990px !important;">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;overflow:hidden;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%" align="center">
                        <tr>
                            <td align="left" style="font-weight: bold;">
                                Managers:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:GridView ID="grdBonusManagers" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="header_row"
                                    AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Width="100%"
                                    EmptyDataText="Sorry No Record Found!" CellPadding="0" BorderStyle="None" BorderWidth="0"
                                    GridLines="None" OnRowDataBound="grdBonusManagers_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="FullName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200" />
                                        <%--<asp:BoundField DataField="Bonus" HeaderText="Bonus" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="600" HeaderStyle-Width="655" />--%>
                                        <asp:TemplateField HeaderText="Bonus" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="600"
                                            HeaderStyle-Width="655">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBonus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="dModifiedDate"  ItemStyle-HorizontalAlign="Left" />--%>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-weight: bold;">
                                Staff:
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:GridView ID="grdBonusStaff" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="header_row"
                                    AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Width="100%"
                                    EmptyDataText="Sorry No Record Found!" CellPadding="0" BorderStyle="None" BorderWidth="0"
                                    GridLines="None" OnRowDataBound="grdBonusStaff_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="FullName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200" />
                                        <%-- <asp:BoundField DataField="Bonus" HeaderText="Bonus" ItemStyle-HorizontalAlign="Left"
                                            ItemStyle-Width="600" HeaderStyle-Width="655" />--%>
                                        <asp:TemplateField HeaderText="Bonus" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="600"
                                            HeaderStyle-Width="655">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBonus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="dModifiedDate"  ItemStyle-HorizontalAlign="Left" />--%>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
