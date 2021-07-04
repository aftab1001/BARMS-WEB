<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master"
    AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="AccountManager_Products"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function() {
            $('input.filter').bind('keyup blur', function() {
                if (this.value.match(/[^1-9]/g)) {
                    this.value = this.value.replace(/[^1-9]/g, '');
                }
            });
        });
        function SupplierSaved() {
            $("#ctl00_ContentPlaceHolder1_lblMessageProduct").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblMessageProduct");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblMessageProduct").fadeOut('slow');
                }
            }, 4000);
        }

        function VatSaved() {
            $("#ctl00_ContentPlaceHolder1_lblMessageVat").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblMessageVat");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblMessageVat").fadeOut('slow');
                }
            }, 4000);
        }
        function PackingSaved() {
            $("#ctl00_ContentPlaceHolder1_lblPackingMessage").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblPackingMessage");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblPackingMessage").fadeOut('slow');
                }
            }, 4000);
        }

        
        
        
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnableScriptGlobalization="true">
        <Services>
            <asp:ServiceReference Path="~/AccountManager/GetProducts.asmx" />
        </Services>
    </asp:ScriptManager>
    <div>
        <asp:Label ID="lblMessageProduct" runat="server" Text="Successfully Added!" ForeColor="Green"
            Style="position: relative; left: 200px; font-size: 15px;display:none;"></asp:Label>
        <div style="color: #416da3; text-align: center; margin-left: 223px;">
            <table>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkBase" runat="server" Text="Base Categories" Style="text-decoration: underline;"
                            OnClick="lnkBase_Click"></asp:LinkButton>
                        |
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkSub" runat="server" Text="Sub Categories" Style="text-decoration: underline;"
                            OnClick="lnkSub_Click"></asp:LinkButton>
                        |
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkPro" runat="server" Text="Products" Style="text-decoration: underline;"
                            OnClick="lnkPro_Click"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
            <asp:View ID="vAddProduct" runat="server" OnActivate="vAddProduct_Activate">
                <fieldset>
                    <legend>Products</legend>
                    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                        <tr>
                            <td>
                                <asp:LinkButton ID="lnkBacktoSub" runat="server" Style="font-size: 12px; font-weight: normal;
                                    color: #619ae9" OnClick="lnkBacktoSub_Click1" Visible="false">
                                                                    <img src="../images/back_arrow.png" alt="" />Back to Sub Categories
                                </asp:LinkButton>
                            </td>
                            <td align="right">
                                <div style="float: right;">
                                    <asp:LinkButton ID="lnkResetVat" runat="server" Text="Reset VAT" OnClick="lnkResetVat_Click"
                                        Style="text-decoration: underline; color: #416da3;" OnClientClick="javascript:return confirm('Are you sure you want to reset vat on all products according to sub category!');"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgBtnAddProduct" runat="server" ImageUrl="~/Images/btn_addproduct.png"
                                    OnClick="imgBtnAddProduct_Click" />
                                <asp:ImageButton ID="imgBtnFilterProduct" runat="server" ImageUrl="~/Images/btn_filter_account.png"
                                    OnClick="imgBtnFilterProduct_Click" />
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="lnkPackagingPopup" runat="server" Text="Packing & Quantity Options"
                                    Style="text-decoration: underline; color: #416da3;" OnClick="lnkPackagingPopup_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkQuantityPopup" runat="server" Text="Quantity Options" Style="text-decoration: underline;
                                    color: #416da3; display: none;" OnClick="lnkQuantityPopup_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="trLineBeforeProduct" runat="server">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr id="trAddProduct" runat="server" visible="false">
                            <td colspan="2">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="imgBtnSaveProduct">
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                Name
                                            </td>
                                            <td>
                                            </td>
                                            <td style="display:none;">
                                            </td>
                                            <td>
                                                VAT
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="textbox115">
                                                    <asp:DropDownList ID="ddlBaseCat_vAddProducts" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBaseCat_vAddProducts_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="ddlBaseCat_vAddProducts" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="*" ValidationGroup="reqp"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox115">
                                                    <asp:DropDownList ID="ddlSubCat_vAddProducts" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="ddlSubCat_vAddProducts" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="*" ValidationGroup="reqp"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox115">
                                                    <asp:TextBox ID="txtProduct" runat="server"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="autoCompDesigner" runat="server" CompletionSetCount="20"
                                                        EnableCaching="true" MinimumPrefixLength="1" ServiceMethod="GetProductNames"
                                                        ServicePath="~/AccountManager/GetProducts.asmx" TargetControlID="txtProduct"
                                                        CompletionListCssClass="autocomplete_completionListElement">
                                                    </cc1:AutoCompleteExtender>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="txtProduct" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="reqp"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox115">
                                                    <asp:DropDownList ID="ddlPacking_vAddProduct" runat="server" >
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="ddlPacking_vAddProduct" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="*" ValidationGroup="reqp"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td style="display:none;">
                                                <div class="textbox_small">
                                                    <asp:DropDownList ID="ddlQuantity_vAddProduct" runat="server" Style="width: 65px;">
                                                    </asp:DropDownList>
                                                    
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:DropDownList ID="ddlVatPro" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td colspan="2" align="right">
                                                <asp:ImageButton ID="imgBtnSaveProduct" runat="server" ImageUrl="~/Images/btn_save.png"
                                                    ValidationGroup="reqp" OnClick="imgBtnSaveProduct_Click" />
                                                <asp:ImageButton ID="imgBtnFilter" runat="server" ImageUrl="~/Images/btn_filter.png" style="float:left;"
                                                    Visible="false" OnClick="imgBtnFilter_Click" />
                                                <asp:ImageButton ID="imgBtnCancelProduct" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                    OnClick="imgBtnCancelProduct_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="trLineAfterProduct" runat="server" visible="false" style="padding-top: 10px;
                            padding-bottom: 10px;">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="align_top">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                    <div class="rounded_box">
                                                                        Products
                                                                        <div class="clear_10">
                                                                        </div>
                                                                        <asp:GridView ID="grdProducts" AllowSorting="True" AllowPaging="True" runat="server"
                                                                            PageSize="30" AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                            EmptyDataText="Sorry! No Products." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                            OnRowCommand="grdProducts_RowCommand" OnPageIndexChanging="grdProducts_PageIndexChanging"
                                                                            OnRowDataBound="grdProducts_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="BaseCat" HeaderText="Base Category" />
                                                                                <asp:BoundField DataField="SubCat" HeaderText="Sub Category" />
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkProduct" runat="server" Text="<%#Bind('Product') %>" CommandName="name"
                                                                                            CommandArgument='<%# Bind("pkProductID") %>'></asp:LinkButton>
                                                                                        <asp:HiddenField ID="hidPQR" runat="server" Value="<%#Bind('pkProductPackingQuantityRelID') %>" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="pName" HeaderText="Packaging" />
                                                                                <asp:BoundField DataField="qName" HeaderText="Quantity" />
                                                                                <asp:TemplateField HeaderText="VAT">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVATPro" runat="server" Text="<%#Bind('vat') %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Activate">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkActivePro" runat="server" AutoPostBack="true" OnCheckedChanged="chkActivePro_CheckedChanged" />
                                                                                        <asp:HiddenField ID="hidPro" runat="server" Value="<%# Bind('pkProductID') %>" />
                                                                                        <asp:HiddenField ID="hidSubCat_pro" runat="server" Value="<%# Bind('pkSubCategoryID') %>" />
                                                                                        <asp:HiddenField ID="hidBaseCat_pro" runat="server" Value="<%# Bind('pkBaseCategoryID') %>" />
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
                                                    <td colspan="2" class="height10">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:View>
            <asp:View ID="vAddBase" runat="server">
                <fieldset>
                    <legend>Base Category</legend>
                    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgBtnAddBaseCategory" runat="server" ImageUrl="~/Images/btn_addbase.png"
                                    OnClick="imgBtnAddBaseCategory_Click" />
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkSuppliesVat" runat="server" Text="Supplies VAT" Style="text-decoration: underline;
                                    color: #416da3;" OnClick="lnkSuppliesVat_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="trLineBeforeBase" runat="server">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr id="trAddBase" runat="server" visible="false">
                            <td colspan="2">
                                <asp:Panel ID="pnlBase" runat="server" DefaultButton="imgBtnSaveBase">
                                    <table>
                                        <tr>
                                            <td>
                                                Name:
                                            </td>
                                            <td>
                                                Description:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtBaseCate" runat="server" CssClass="filterNum"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="txtBaseCate" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="rm"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtBaseDescription" runat="server" CssClass="filterNum"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:DropDownList ID="ddlVat" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="ddlVat" Display="Dynamic" InitialValue="0"
                                                        ErrorMessage="*" ValidationGroup="rm"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnSaveBase" runat="server" ImageUrl="~/Images/btn_save.png"
                                                    ValidationGroup="rm" OnClick="imgBtnSaveBase_Click" />
                                                <asp:ImageButton ID="imgBtnCancelBase" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                    OnClick="imgBtnCancelBase_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="trLineAfterBase" runat="server" visible="false" style="padding-top: 10px;
                            padding-bottom: 10px;">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="align_top">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                    <div class="rounded_box">
                                                                        Base Categories
                                                                        <div class="clear_10">
                                                                        </div>
                                                                        <asp:GridView ID="grdBaseCategories" AllowSorting="True" AllowPaging="True" runat="server"
                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                            EmptyDataText="Sorry! No Base Category." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                            OnRowCommand="grdBaseCategories_RowCommand" OnPageIndexChanging="grdBaseCategories_PageIndexChanging"
                                                                            OnRowDataBound="grdBaseCategories_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnlBaseCagegory" runat="server" Text="<%#Bind('CatagoryName') %>"
                                                                                            CommandName="name" CommandArgument='<%# Bind("pkBaseCategoryID") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="bDescription" HeaderText="Description" />
                                                                                <asp:BoundField DataField="vat" HeaderText="VAT" />
                                                                                <asp:TemplateField HeaderText="Activate">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true" OnCheckedChanged="chkActive_CheckedChanged" />
                                                                                        <asp:HiddenField ID="hidBase" runat="server" Value="<%# Bind('pkBaseCategoryID') %>" />
                                                                                        <asp:HiddenField ID="hidBaseName" runat="server" Value="<%# Bind('CatagoryName') %>" />
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
                                                    <td colspan="2" class="height10">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:View>
            <asp:View ID="vAddSub" runat="server">
                <fieldset>
                    <legend>Sub Category</legend>
                    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                        <tr>
                            <td style="display: none;">
                                <asp:LinkButton ID="lnkBackToBase" runat="server" Style="font-size: 12px; font-weight: normal;
                                    color: #619ae9" OnClick="lnkBackToBase_Click1">
                                                                    <img src="../images/back_arrow.png" alt="" />Back to Base Categories
                                </asp:LinkButton>
                            </td>
                            <td align="right">
                                <div style="float: right;">
                                    <asp:LinkButton ID="lnkResetSubcategoryVat" runat="server" Text="Reset VAT" OnClick="lnkResetSubcategoryVat_Click"
                                        Style="text-decoration: underline; color: #416da3;" OnClientClick="javascript:return confirm('Are you sure you want to reset vat on all subcategories according to base category!');"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgBtnAddSub" runat="server" ImageUrl="~/Images/btn_add_sub.png"
                                    OnClick="imgBtnAddSub_Click" />
                            </td>
                        </tr>
                        <tr id="trLineBeforeSub" runat="server">
                            <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr id="trAddSub" runat="server" visible="false">
                            <td>
                                <asp:Panel ID="pnlSub" runat="server" DefaultButton="imgBtnSaveSub">
                                    <table>
                                        <tr>
                                            <td>
                                                Base Category:
                                            </td>
                                            <td>
                                                Name:
                                            </td>
                                            <td>
                                                Description:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="textbox204">
                                                    <asp:DropDownList ID="ddlBase_vAddSub" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="ddlBase_vAddSub" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="*" ValidationGroup="reqc"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtSubCat" runat="server" CssClass="filterNum"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="txtSubCat" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="reqc"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtdescriptionSub" runat="server" CssClass="filterNum"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:DropDownList ID="ddlVatSub" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td colspan="2" align="right">
                                                <asp:ImageButton ID="imgBtnSaveSub" runat="server" ImageUrl="~/Images/btn_save.png"
                                                    ValidationGroup="reqc" OnClick="imgBtnSaveSub_Click" />
                                                <asp:ImageButton ID="imgBtnCancelSub" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                    OnClick="imgBtnCancelSub_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="trLineAfterSub" runat="server" visible="false" style="padding-top: 10px;
                            padding-bottom: 10px;">
                            <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="align_top">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                    <div class="rounded_box">
                                                                        Sub Categories
                                                                        <div class="clear_10">
                                                                        </div>
                                                                        <asp:GridView ID="grdSubCategory" AllowSorting="True" AllowPaging="True" runat="server"
                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                            EmptyDataText="Sorry! No Sub Category." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                            OnRowCommand="grdSubCategory_RowCommand" OnPageIndexChanging="grdSubCategory_PageIndexChanging"
                                                                            OnRowDataBound="grdSubCategory_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkSubCategory" runat="server" Text="<%#Bind('cSubCategoryName') %>"
                                                                                            CommandName="name" CommandArgument='<%# Bind("pkSubCategoryID") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="sDescription" HeaderText="Description" />
                                                                                <asp:TemplateField HeaderText="VAT">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVATSub" runat="server" Text="<%#Bind('vat') %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Activate">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkActiveSub" runat="server" AutoPostBack="true" OnCheckedChanged="chkActiveSub_CheckedChanged" />
                                                                                        <asp:HiddenField ID="hidSub" runat="server" Value="<%# Bind('pkSubCategoryID') %>" />
                                                                                        <asp:HiddenField ID="hidSubName" runat="server" Value="<%# Bind('cSubCategoryName') %>" />
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
                                                    <td colspan="2" class="height10">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:View>
            <asp:View ID="vEditProduct" runat="server">
                <asp:LinkButton ID="lnkBackToProducts" runat="server" Style="font-size: 12px; font-weight: normal;
                    color: #619ae9" OnClick="lnkBackToProducts_Click1">
                                                                    <img src="../images/back_arrow.png" alt="" />Back to Products
                </asp:LinkButton>
                <table>
                    <tr>
                        <td>
                            Product Name:
                        </td>
                        <td>
                            <div class="textbox204" style="margin-left: -590px;">
                                <asp:TextBox ID="txtEditProduct" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="float: right;
                                    position: relative; top: 6px;" ControlToValidate="txtEditProduct" Display="Dynamic"
                                    ErrorMessage="*" ValidationGroup="reu"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="height30">
                            <img src="../images/horizontal_line.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="align_top">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                <div class="rounded_box">
                                                                    Packaging
                                                                    <div class="clear_10">
                                                                    </div>
                                                                    <asp:GridView ID="grdPacking" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="header_row"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Width="100%"
                                                                        EmptyDataText="Sorry! No Package." CellPadding="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" Style="font-weight: normal;" OnRowCommand="grdPacking_RowCommand"
                                                                        OnRowDataBound="grdPacking_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                        <tr>
                                                                                            <td class="align_left">
                                                                                                <asp:Label ID="lblPacking" runat="server"></asp:Label>
                                                                                            </td>
                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                <asp:ImageButton ID="imgbtnSetActivePackage" runat="server" CommandName="active"
                                                                                                    CommandArgument='<%# Bind("pkProductPackageID") %>' ImageUrl="~/Images/activate_icon.gif"
                                                                                                    ToolTip="Non-Active" />
                                                                                            </td>
                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkProductPackageID") %>'
                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="height10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width45percent">
                                                    <div class="textbox204">
                                                        <asp:TextBox ID="txtEditPackage" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="float: right;
                                                            position: relative; top: 6px;" ControlToValidate="txtEditPackage" Display="Dynamic"
                                                            ErrorMessage="*" ValidationGroup="re"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                                <td class="width55percent">
                                                    <asp:ImageButton ID="btnUpdatePackage" runat="server" ImageUrl="../images/btn_addonher_pack.png"
                                                        OnClick="btnUpdatePackage_Click" ValidationGroup="re" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="width30">
                                    </td>
                                    <td class="align_top" style="display: none;">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                <div class="rounded_box">
                                                                    Quantity
                                                                    <div class="clear_10">
                                                                    </div>
                                                                    <asp:GridView ID="grdQuantity" AllowSorting="True" AllowPaging="false" runat="server"
                                                                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                        EmptyDataText="Sorry! No Phone." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                        OnRowCommand="grdQuantity_RowCommand" OnRowDataBound="grdQuantity_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                        <tr>
                                                                                            <td class="align_left">
                                                                                                <asp:Label ID="lblQuantity" runat="server" Text="<%#Bind('Qty') %>"></asp:Label>
                                                                                            </td>
                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                <asp:ImageButton ID="imgBtnActiveQuantity" runat="server" CommandName="active" CommandArgument='<%# Bind("pkProductQuantityID") %>'
                                                                                                    ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                            </td>
                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkProductQuantityID") %>'
                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="height10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="width45percent">
                                                    <div class="textbox204">
                                                        <asp:TextBox ID="txtEditQuantity" runat="server" CssClass="filterNum"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Style="float: right;
                                                            position: relative; top: 6px;" ControlToValidate="txtEditQuantity" Display="Dynamic"
                                                            ErrorMessage="*" ValidationGroup="rm"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                                <td class="width55percent">
                                                    <asp:ImageButton ID="btnUpdateQuantity" runat="server" ImageUrl="../images/btn_anotherphone.gif"
                                                        OnClick="btnUpdateQuantity_Click" ValidationGroup="rm" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="height30">
                            <img src="../images/horizontal_line.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div>
                                <asp:ImageButton ID="imgBtnUpdateProduct" runat="server" ImageUrl="~/Images/btn_update.png"
                                    ValidationGroup="reu" OnClick="imgBtnUpdateProduct_Click" />
                                <asp:ImageButton ID="imgBtnUpdateProductCancel" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                    OnClick="imgBtnUpdateProductCancel_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnExtent1"
        PopupControlID="pnlAddresses" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent1" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlAddresses" runat="server">
        <asp:UpdatePanel ID="upnlAddresses" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 753px !important;">
                    <span style="float: left; color: White; font-size: 15px; line-height: 39px; padding-left: 5px;">
                        VAT</span> <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table width="90%" cellpadding="0" cellspacing="10" border="0" align="center">
                        <tr>
                            <td colspan="3">
                                <h3>
                                    <asp:Label ID="lblMessageVat" runat="server" Style="display: none;"></asp:Label>
                                </h3>
                            </td>
                        </tr>
                        <tr id="trAddVat" runat="server">
                            <td style="width: 100px;" colspan="3">
                                <asp:ImageButton ID="imgBtnAddVat" runat="server" ImageUrl="~/Images/btn_addvat.png"
                                    OnClick="imgBtnAddVat_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 5px;">
                            </td>
                        </tr>
                        <tr id="trLineBeforeVat" runat="server">
                            <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr id="trVat" runat="server" visible="false">
                            <td valign="middle">
                                Vat:
                            </td>
                            <td valign="middle">
                                <div class="textbox_small">
                                    <asp:TextBox ID="txtVatAmount" runat="server" Style="text-align: center;" CssClass="filter"></asp:TextBox>
                                    <span style="position: relative; float: right; left: 11px; margin-top: -19px;">%</span>
                                </div>
                            </td>
                            <td valign="bottom">
                                <asp:ImageButton ID="imgBtnSaveVat" runat="server" ImageUrl="~/Images/btn_save.png"
                                    OnClick="imgBtnSaveVat_Click" />
                                <asp:ImageButton ID="imgBtnEditVat" runat="server" ImageUrl="~/Images/btn_edit_admin.png"
                                    OnClick="imgBtnEditVat_Click" />
                                <asp:ImageButton ID="imgBtnCancelVat" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                    OnClick="imgBtnCancelVat_Click" />
                            </td>
                        </tr>
                        <tr id="trLineAfterVat" runat="server" visible="false">
                            <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <div class="clear_10">
                                </div>
                                <table width="50%" border="0" cellspacing="0" cellpadding="0">
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
                                                <div class="clear_10">
                                                </div>
                                                <asp:GridView ID="grdVat" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    EmptyDataText="No Vat Exists." GridLines="None" HeaderStyle-CssClass="header_row"
                                                    AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                    OnRowCommand="grdVat_RowCommand" OnRowDataBound="grdVat_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Vat">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkVat" runat="server" CommandName="vat" CommandArgument="<%#Bind('pkvatid') %>"
                                                                    Text="<%#Bind('vat') %>"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Active">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgBtnActive" CommandName="active" CommandArgument="<%#Bind('pkvatid') %>"
                                                                    runat="server" ImageUrl="../Images/activate_icon.gif" />
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
                                <div class="clear_30">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnExtent2"
        PopupControlID="pnlChangeBase" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent2" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlChangeBase" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 753px !important;">
                    <span style="float: left; color: White; font-size: 15px; line-height: 39px; padding-left: 5px;">
                        Requester</span> <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="0" cellspacing="10" border="0" align="center" style="width: 733px;">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblPopBaseActMessage" runat="server" Visible="false" ForeColor="Green"
                                    Text="Successfully Updated!" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Before de-activating the category "<asp:Label ID="lblBaseChange" runat="server"></asp:Label>",
                                you will need to link the following subcategories to a different category! Please
                                chose the right parent category for the sub categories below.<br>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Link all affected Sub Categories to the new parent Category on the right:
                            </td>
                            <td>
                                <div class="textbox204">
                                    <asp:DropDownList ID="ddlBaseCategories" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                ..or chose individually to link a sub category to a base category below!
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <table style="width: 75%;" cellpadding="0" border="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:DataList ID="dtlBaseDropDowns" runat="server" RepeatColumns="2" OnItemDataBound="dtlBaseDropDowns_ItemDataBound">
                                                <ItemTemplate>
                                                    <table style="width: 100%;" cellspacing="5" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 60px;">
                                                                <asp:Label ID="lblSubCatName" runat="server" Text="<%#Bind('cSubCategoryName') %>"></asp:Label>
                                                                <asp:HiddenField ID="hidBaseForAct" runat="server" Value="<%#Bind('fkBaseCategoryID') %>" />
                                                                <asp:HiddenField ID="hidSubForAct" runat="server" Value="<%#Bind('pkSubCategoryID') %>" />
                                                            </td>
                                                            <td>
                                                                <div class="textbox204">
                                                                    <asp:DropDownList ID="ddlBaseCategoriesDTL" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:ImageButton ID="imgBtnSaveForActivation" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                    OnClick="imgBtnSaveForActivation_Click" />
                                <asp:ImageButton ID="imgBtnCancelForActivation" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                    OnClick="imgBtnCancelForActivation_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnExtent3"
        PopupControlID="pnlChangeSub" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent3" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlChangeSub" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 753px !important;">
                    <span style="float: left; color: White; font-size: 15px; line-height: 39px; padding-left: 5px;">
                        Requester</span> <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender3.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="0" cellspacing="10" border="0" align="center" style="width: 733px;">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lblPopSubActMessage" runat="server" Visible="false" ForeColor="Green"
                                    Text="Successfully Updated!" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Before de-activating the category "<asp:Label ID="lblAffectedSubcat" runat="server"></asp:Label>
                                " , you will need to link the following products to a different sub category! Please
                                chose the desired parent sub category for the products below.<br>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Link all affected Products to the desired parent Sub Categories on the right:
                            </td>
                            <td>
                                <div class="textbox204">
                                    <asp:DropDownList ID="ddlSubCategories" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                ..or chose individually to link a product to a sub category below!
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <table style="width: 75%;" cellpadding="0" border="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:DataList ID="dtlSubDropDowns" runat="server" RepeatColumns="2" OnItemDataBound="dtlSubDropDowns_ItemDataBound">
                                                <ItemTemplate>
                                                    <table style="width: 100%;" cellspacing="5" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 60px;">
                                                                <asp:Label ID="lblProductName" runat="server" Text="<%#Bind('sProductName') %>"></asp:Label>
                                                                <asp:HiddenField ID="hidSubcatForAct" runat="server" Value="<%#Bind('fkSubCategoryID') %>" />
                                                                <asp:HiddenField ID="hidProForAct" runat="server" Value="<%#Bind('pkProductID') %>" />
                                                            </td>
                                                            <td>
                                                                <div class="textbox204">
                                                                    <asp:DropDownList ID="ddlSubCategoriesDTL" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:ImageButton ID="imgBtnSaveForActivationSub" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                    OnClick="imgBtnSaveForActivationSub_Click" />
                                <asp:ImageButton ID="imgBtnCancelForActivationSub" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                    OnClick="imgBtnCancelForActivationSub_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnExtent4"
        PopupControlID="pnlChangePacking" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent4" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlChangePacking" runat="server">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 753px !important; color: ">
                    <span style="float: left; color: White; font-size: 15px; line-height: 39px; padding-left: 5px;">
                        Packing Options</span> <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender4.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblPackingMessage" runat="server" ForeColor="Green" Style="font-size: 15px;
                                    display: none;" Text="Successfully Added"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnAddPackingOptions" runat="server" ImageUrl="~/Images/btn_addpacking.png"
                                    OnClick="imgBtnAddPackingOptions_Click" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                        </tr>
                        <tr id="trLineBeforePacking" runat="server">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr id="trPacking" runat="server" visible="false">
                            <td colspan="2">
                                <asp:Panel ID="Panel2" runat="server" DefaultButton="imgBtnSavePacking">
                                    <table cellpaddin="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="left">
                                                #
                                            </td>
                                            <td align="left">
                                                Packing Name:
                                            </td>
                                            <td align="left">
                                                Quantity Name:
                                            </td>
                                            <td align="left">
                                                Description:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="90px;" valign="top">
                                                <div class="textbox_small">
                                                    <asp:TextBox ID="txtOrderNumPacking" runat="server" CssClass="filter"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Style="float: right;
                                                        position: relative; top: -20px;" ControlToValidate="txtOrderNumPacking" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="rmpo"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hidSortNo" runat="server" />
                                                </div>
                                            </td>
                                            <td align="left" style="134px;" valign="top">
                                                <div class="textbox115">
                                                    <asp:TextBox ID="txtPackingName" runat="server" CssClass="filterNum"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="float: right;
                                                        position: relative; top: -20px;" ControlToValidate="txtPackingName" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="rmpo"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td align="left" style="138px;" valign="top">
                                                <div class="textbox115">
                                                    <asp:TextBox ID="txtQuantityNameNew" runat="server" CssClass="filterNum"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="float: right;
                                                        position: relative; top: -20px;" ControlToValidate="txtQuantityNameNew" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="rmpo"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td valign="top" align="left">
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtPackingDescription" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td valign="top">
                                                <asp:ImageButton ID="imgBtnSavePacking" runat="server" ImageUrl="~/Images/btn_save.png"
                                                    ValidationGroup="rmpo" OnClick="imgBtnSavePacking_Click" />
                                                <asp:ImageButton ID="imgBtnCancelPacking" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                    OnClick="imgBtnCancelPacking_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="trLineAfterPacking" runat="server" visible="false" style="padding-top: 10px;
                            padding-bottom: 10px;">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="align_top">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                    <div class="rounded_box">
                                                                        Packing
                                                                        <div class="clear_10">
                                                                        </div>
                                                                        <asp:GridView ID="grdPackingPopup" AllowSorting="True" AllowPaging="True" runat="server"
                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                            EmptyDataText="Sorry! No Packing." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                            OnRowCommand="grdPackingPopup_RowCommand" OnPageIndexChanging="grdPackingPopup_PageIndexChanging"
                                                                            OnRowDataBound="grdPackingPopup_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="pOrder" HeaderText="#" ItemStyle-Width="50" />
                                                                                <asp:TemplateField HeaderText="Packing Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnlPackingName" runat="server" Text="<%#Bind('pName') %>" CommandName="name"
                                                                                            CommandArgument='<%# Bind("pkProductPackageID") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Quantity Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkQuantityName" runat="server" Text="<%#Bind('qName') %>" CommandName="name"
                                                                                            CommandArgument='<%# Bind("pkProductPackageID") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="pDescription" HeaderText="Description" />
                                                                                <asp:TemplateField HeaderText="Active">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnActive" runat="server" ImageUrl="../Images/activate_icon.gif"
                                                                                            CommandName="active" CommandArgument="<%#Bind('pkProductPackageID') %>" />
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
                                                    <td colspan="2" class="height10">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="btnExtent5"
        PopupControlID="pnlChangeQuantity" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent5" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlChangeQuantity" runat="server">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 753px !important; color: ">
                    <span style="float: left; color: White; font-size: 15px; line-height: 39px; padding-left: 5px;">
                        Quantity Options</span> <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender5.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblQuantityMessage" runat="server" ForeColor="Green" Style="font-size: 15px;"
                                    Visible="false" Text="Successfully Added"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnAddQuantityOptions" runat="server" ImageUrl="~/Images/btn_add_quantity.png"
                                    OnClick="imgBtnAddQuantityOptions_Click" />
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                            </td>
                        </tr>
                        <tr id="trLineBeforeQuantity" runat="server">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr id="trQuantity" runat="server" visible="false">
                            <td colspan="2">
                                <asp:Panel ID="Panel4" runat="server" DefaultButton="imgBtnSaveQuantity">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                #
                                            </td>
                                            <td align="left">
                                                Name:
                                            </td>
                                            <td align="left">
                                                Description:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <div class="textbox_small">
                                                    <asp:TextBox ID="txtOrderNumQuantity" runat="server" CssClass="filter"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="txtOrderNumQuantity" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="rmpq"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hidSortQuantity" runat="server" />
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtQuantityName" runat="server" CssClass="filterNum"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Style="float: right;
                                                        position: relative; top: 6px;" ControlToValidate="txtQuantityName" Display="Dynamic"
                                                        ErrorMessage="*" ValidationGroup="rmpq"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                            <td align="left">
                                                <div class="textbox204">
                                                    <asp:TextBox ID="txtQuantityDescription" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnSaveQuantity" runat="server" ImageUrl="~/Images/btn_save.png"
                                                    ValidationGroup="rmpq" OnClick="imgBtnSaveQuantity_Click" />
                                                <asp:ImageButton ID="imgBtnCancelQuantity" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                    OnClick="imgBtnCancelQuantity_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="trLineAfterQuantity" runat="server" visible="false" style="padding-top: 10px;
                            padding-bottom: 10px;">
                            <td colspan="2" style="background-color: #bfb8bb; height: 2px;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="align_top">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
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
                                                                    <div class="rounded_box">
                                                                        Quantity
                                                                        <div class="clear_10">
                                                                        </div>
                                                                        <asp:GridView ID="grdQuantityPopup" AllowSorting="True" AllowPaging="True" runat="server"
                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                            EmptyDataText="Sorry! No Quantity." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                            OnRowCommand="grdQuantityPopup_RowCommand" OnPageIndexChanging="grdQuantityPopup_PageIndexChanging"
                                                                            OnRowDataBound="grdQuantityPopup_RowDataBound">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="qOrder" HeaderText="#" />
                                                                                <asp:TemplateField HeaderText="Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnlQuantityName" runat="server" Text="<%#Bind('qName') %>" CommandName="name"
                                                                                            CommandArgument='<%# Bind("pkProductQuantityID") %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="qDescription" HeaderText="Description" />
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
                                                    <td colspan="2" class="height10">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
