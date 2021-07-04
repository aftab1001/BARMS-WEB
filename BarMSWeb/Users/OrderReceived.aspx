<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserMaster.master"
    AutoEventWireup="true" CodeFile="OrderReceived.aspx.cs" Inherits="Users_OrderReceived" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="Ajaxified" Assembly="Ajaxified" Namespace="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />
    <link href="../Style/SiteStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.price_format.1.7.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function setTime(txtid, txt) {
            document.getElementById(txtid).value = txt;
            $find('<%=MPEDate.ClientID %>').hide();
            //alert(document.getElementById(txtid).value);
        }
        function clientShowing(sender) {

        }
        function clientShown(sender) {

        }
        function clientHiding(sender) {

        }
        function clientHidden(sender) {

        }
        function selectionChanged(sender) {
            //alert(sender._selectedTime);
        }
    </script>

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
        function CheckValidity() {
            var chk = document.getElementById("ctl00_ContentPlaceHolder1_chkBonus");
            if (!chk.checked) {
                return false;
            }
        }
        
    </script>

    <script type="text/javascript">
        $(function() {
            $("#ctl00_ContentPlaceHolder1_txtFrom").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy'
            });
            $("#ctl00_ContentPlaceHolder1_txtTill").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy'
            });

            $("#ctl00_ContentPlaceHolder1_rdWithInvoice").change(function() {
                $("#divInvoiceNum").show();

            });

            $("#ctl00_ContentPlaceHolder1_rdWithoutOInvoice").change(function() {
                $("#divInvoiceNum").hide();

            });
            $('input.filter').bind('keyup blur', function() {
                if (this.value.match(/[^-: 0-9]/g)) {
                    this.value = this.value.replace(/[^-: 0-9]/g, '');
                }
                else {
                    if (this.value.split('-').length > 2) {
                        this.value = this.value.substring(0, this.value.lastIndexOf('-'));
                    }
                }
            });


            $('input.filter_qty').bind('keyup blur', function() {
                if (this.value.match(/[^,.0-9]/g)) {
                    this.value = this.value.replace(/[^,.0-9]/g, '');
                }
                else {
                    if (this.value.split(',').length > 2) {
                        this.value = this.value.substring(0, this.value.lastIndexOf(','));
                    }
                }
            });


        });
        function RecordSaved() {
            //            $("#ctl00_ContentPlaceHolder1_lblRecordMessage").show().delay(100).fadeOut();
            $("#ctl00_ContentPlaceHolder1_lblMessageTop").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblMessageTop");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblMessageTop").fadeOut('slow');
                }
            }, 5000);
        }

        function ReceivedRecordSaved() {
            //            $("#ctl00_ContentPlaceHolder1_lblRecordMessage").show().delay(100).fadeOut();
            $("#ctl00_ContentPlaceHolder1_lblReceivingOrder").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblReceivingOrder");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblReceivingOrder").fadeOut('slow');
                }
            }, 5000);
        }


        function TimeRange() {


        }
        function PdfSingle() {
            var a = window.open("../pdfGenerator.aspx?r=s", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function PdfAll() {
            var a = window.open("../pdfGenerator.aspx?r=m", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function PrintSingle() {
            var a = window.open("../pdfGenerator.aspx?r=ps", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function PrintAll() {
            var a = window.open("../pdfGenerator.aspx?r=pm", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function WaterMark() {

            $('.filter_qty').watermark('00,00');
        }
        /*
        function addSubtotals() {

            var subTotal = 0.0;
        var supp_subTotal = 0.0;

            var commonid_supplier = "ctl00_ContentPlaceHolder1_grdSuppliers_ctl0";
        var commonid_orders = "grdOrders_FinalOrder_ctl0";

            var grd_supplier = document.getElementById("<%= grdSuppliers.ClientID %>");
        var grandTotal = document.getElementById("<%= lblGrandFinalTotal.ClientID %>");

            if (grd_supplier.rows.length > 0) {

                for (i = 2; i <= grd_supplier.rows.length; i++) {
        alert(i);

                    var grd_orders = document.getElementById(commonid_supplier + i + "_" + "grdOrders_FinalOrder");
        if (grd_orders != null) {
        if (grd_orders.rows.length > 0) {
        for (j = 2; j <= grd_orders.rows.length; j++) {
        alert("orders :" + j);
        var order_total = document.getElementById(commonid_supplier + i + "_" + commonid_orders + j + "_" + "lblSubtotals");
        alert("orders :" + commonid_supplier + i + "_" + commonid_orders + j + "_" + "lblSubtotals");
        if (order_total != null) {
        alert("find :" + order_total.innerHTML);
        subTotal += getNum(order_total.innerHTML);
        order_total.innerHTML = ChangeToUK(order_total.innerHTML);
        }


                            }

                        }
        }
        }
        if (subTotal != 0.0)
        grandTotal.innerHTML = ChangeToUK(supp_subTotal.toString());
        }
        }

        function getNum(value) {
        if (value == "00,00") {
        value = "0";
        }
        alert("getNum: " + value);
        value = ChangeToUS(value);
        value = parseFloat(value, 10);
        value = roundNumber(value, 2);

            //value = parseFloat(value.toFixed(2),10);
        return value;
        }
        function ChangeToUS(value) {

            var retValue = "";

            var tempArray = new Array();
        var BeforeComma = new Array();
        var AfterComma = "";

            tempArray = value.split(',');

            if (tempArray.length > 1) {
        BeforeComma = tempArray[0].split('.');
        AfterComma = tempArray[1].toString();
        }
        else {
        BeforeComma = tempArray[0].split('.');
        AfterComma = "00";
        }
        if (BeforeComma.length == 1) {
        if (BeforeComma[0].toString() == "0")
        retValue += "00,";
        else
        retValue += BeforeComma[0] + ',';
        }
        else {
        for (i = 0; i <= BeforeComma.length - 1; i++)
        retValue += BeforeComma[i] + ',';
        }

            retValue = retValue.substr(0, retValue.lastIndexOf(',')) + '.';
        retValue += AfterComma;
        retValue = parseFloat(retValue, 10);
        retValue = roundNumber(retValue, 2);
        alert("after change: " + retValue);
        return retValue;
        }
        function ChangeToUK(value) {


            var retValue = "";

            var tempArray = new Array();
        var BeforeComma = new Array();
        var AfterComma = "";

            tempArray = value.split('.');

            if (tempArray.length > 1) {
        BeforeComma = tempArray[0].split(',');
        AfterComma = tempArray[1].toString();
        }
        else {
        BeforeComma = tempArray[0].split(',');
        AfterComma = "00";
        }
        if (BeforeComma.length == 1) {
        if (BeforeComma[0].toString() == "0")
        retValue += "00.";
        else
        retValue += BeforeComma[0] + '.';
        }
        else {
        for (i = 0; i <= BeforeComma.length - 1; i++)
        retValue += BeforeComma[i] + '.';
        }

            retValue = retValue.substr(0, retValue.lastIndexOf('.')) + ',';
        retValue += AfterComma;

            return retValue;
        }

        function roundNumber(num, dec) {
        var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
        return result;
        }
       
        
        

        var grd_orders = document.getElementById(commonid_supplier + i + "_" + "grdOrders_FinalOrder");
        if (grd_orders != null) {
        if (grd_orders.rows.length > 0) {

                            for (j = 2; j <= grd_orders.rows.length; j++) {

                                var order_total = document.getElementById(commonid_supplier + i + "_" + commonid_orders + j + "_" + "lblSubtotals");

                                if (order_total != null) {
        subTotal += getNum(order_total.innerHTML);
        order_total.innerHTML = ChangeToUK(order_total.innerHTML);

                                }

                            }

                        }

                        var supplier_total = document.getElementById(commonid_supplier + i + "_" + "lblSupplierTotal");
        if (supplier_total != null) {
        supp_subTotal += getNum(subTotal.toString());
        supplier_total.innerHTML = ChangeToUK(subTotal.toString());
        }
        }
        
        
        
        */
        
    </script>

    <div id="divFeedbackWindow" style="display: none; position: absolute;">
        <%--<asp:Label ID="lblFeedBackWindow" runat="server"></asp:Label>--%>
        <div style="width: auto">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <img src="../images/pop_shape.png" class="pop_shape" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="6">
                                    <img src="../images/box_left_topbg.png" width="6" height="6" alt="" />
                                </td>
                                <td class="box_top_bg">
                                </td>
                                <td align="right">
                                    <img src="../images/box_right_topbg.png" width="6" height="6" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="box_left_bg">
                                </td>
                                <td>
                                    <div id="divBalloon" style="background-color: White; max-width: 300px;">
                                    </div>
                                </td>
                                <td align="right" class="box_right_bg">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="../images/box_left_bottombg.png" width="6" height="6" alt="" />
                                </td>
                                <td class="box_bottom_bg">
                                </td>
                                <td align="right">
                                    <img src="../images/box_right_bottombg.png" width="6" height="6" alt="" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="1">
            <asp:View ID="vSelectProducts" runat="server">
                <table>
                    <tr>
                        <td style="height: 37px">
                            Base Categories:
                        </td>
                        <td style="height: 37px">
                            <div class="textbox204">
                                <asp:DropDownList ID="ddlBaseCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBaseCategories_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Style="float: right;
                                    position: relative; top: 6px;" ControlToValidate="ddlBaseCategories" Display="Dynamic"
                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sub Categories:
                        </td>
                        <td>
                            <div class="textbox204">
                                <asp:DropDownList ID="ddlSubCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategories_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Style="float: right;
                                    position: relative; top: 6px;" ControlToValidate="ddlSubCategories" Display="Dynamic"
                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <div id="Div3" runat="server">
                                            <table width="500" border="0" cellspacing="0" cellpadding="0">
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
                                                            <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                AllowPaging="true" PageSize="15" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowCommand="grdProducts_RowCommand"
                                                                OnPageIndexChanging="grdProducts_PageIndexChanging" OnRowDataBound="grdProducts_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkProduct" runat="server" />
                                                                            <asp:HiddenField ID="hidProductID" runat="server" Value="<%#Bind('pkProductID') %>" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Product Name">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkProductName" runat="server" CommandName="name" CommandArgument="<%#Bind('pkProductID') %>"
                                                                                Text="<%#Bind('sProductName') %>"></asp:LinkButton>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:ImageButton ID="imgBtnSelect" runat="server" ImageUrl="../Images/btn_select.png"
                                            OnClick="imgBtnSelect_Click" />
                                        <asp:ImageButton ID="imgBtnNextToViewOrder" runat="server" ImageUrl="../Images/btn_next.png"
                                            Visible="false" OnClick="imgBtnNextToViewOrder_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vOrderHistory" runat="server">
                <asp:Label ID="lblReceivingOrder" runat="server" Style="color: Green; font-size: 15px;
                    margin-left: 286px; display: none;" Text="Record Save Successfully"></asp:Label>
                <asp:ImageButton ID="imgBtnNewOrder" runat="server" ImageUrl="../Images/btn_order.png"
                    OnClick="imgBtnNewOrder_Click" Visible="false" />
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdOrdersHistory" runat="server" GridLines="None" HeaderStyle-CssClass="header_row"
                                AllowPaging="true" PageSize="20" RowStyle-CssClass="rowstyle" AlternatingRowStyle-CssClass="alternate_row"
                                OnRowDataBound="grdOrdersHistory_RowDataBound" AutoGenerateColumns="false" Style="width: 100%;"
                                EmptyDataText="Sorry No Result Found." OnPageIndexChanging="grdOrdersHistory_PageIndexChanging"
                                OnRowCommand="grdOrdersHistory_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDate" runat="server" Text="<%#Bind('dCreatedDate') %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="sBrandName" HeaderText="Supplier" ItemStyle-Width="100" />--%>
                                    <asp:TemplateField HeaderText="OrderID" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderID" runat="server" Text="<%#Bind('SessionOrderID') %>" Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnkOrderIDUpdate" runat="server" Text="<%#Bind('SessionOrderID') %>"
                                                CommandName="order" CommandArgument="<%#Bind('SessionOrderID') %>"></asp:LinkButton>
                                            <asp:HiddenField ID="hidBaseid" runat="server" Value="<%#Bind('pkBaseOrderID') %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SubTotal" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubtotal" runat="server" Text="<%#Bind('GrandSubtotal') %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Order Status" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderStatus" runat="server" Text="<%#Bind('fkOrderStatusID') %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Cash">
                                        <ItemTemplate>
                                            <div class="textbox115">
                                                <asp:TextBox ID="txtCash" runat="server" Text="<%#Bind('CashAmount') %>"></asp:TextBox>
                                                <asp:HiddenField ID="hidReceiverID" runat="server" Value="<%#Bind('OrderReceivedByUser') %>" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Assign to">
                                        <ItemTemplate>
                                            <div class="textbox115">
                                                <asp:DropDownList ID="ddlUsers" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnsave" runat="server" CommandName="cash" CommandArgument="<%#Bind('pkBaseOrderID') %>"
                                                ImageUrl="~/Images/btn_save.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vNewOrders" runat="server">
                <fieldset style="background-color: #fff;">
                    <legend id="lgOrder" runat="server">Order of
                        <asp:Label ID="lblOrder" runat="server"></asp:Label>
                    </legend>
                    <asp:GridView ID="grdBase" runat="server" GridLines="None" ShowHeader="false" OnRowCommand="grdBase_RowCommand"
                        OnRowDataBound="grdBase_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <fieldset id="fldBase" runat="server" style="background-color: #e8e8e8;">
                                        <legend id="lgBase" runat="server">
                                            <asp:Label ID="lblBaseName" runat="server" Text="<%#Bind('BaseCat') %>"></asp:Label>
                                            <asp:HiddenField ID="hidBase" runat="server" Value="<%#Bind('pkbasecategoryid') %>" />
                                        </legend>
                                        <asp:GridView ID="grdSub" runat="server" GridLines="None" ShowHeader="false" OnRowCommand="grdSub_RowCommand"
                                            OnRowDataBound="grdSub_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <fieldset id="fld" runat="server" style="background-color: #fff; width: 743px;">
                                                            <legend id="lgSub" runat="server">
                                                                <asp:Label ID="lblSubName" runat="server" Text="<%#Bind('SubCat') %>"></asp:Label>
                                                                <asp:HiddenField ID="hidBase_grdSub" runat="server" Value="<%#Bind('pkbasecategoryid') %>" />
                                                                <asp:HiddenField ID="hidSub" runat="server" Value="<%#Bind('pksubcategoryid') %>" />
                                                            </legend>
                                                            <asp:GridView ID="grdOrders" runat="server" AlternatingRowStyle-CssClass="alternate_row"
                                                                GridLines="None" HeaderStyle-CssClass="header_row" OnRowCommand="grdOrders_RowCommand"
                                                                OnRowDataBound="grdOrders_RowDataBound" RowStyle-CssClass="rowstyle">
                                                                <RowStyle CssClass="rowstyle" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-Width="110">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkProductName" runat="server" CommandArgument="<%#Bind('pkproductid') %>"
                                                                                CommandName="name" Text="<%#Bind('Product') %>"></asp:LinkButton>
                                                                            <asp:HiddenField ID="hidProductID" runat="server" Value="<%#Bind('pkproductid') %>" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="110px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div class="textbox115">
                                                                                <asp:DropDownList ID="ddlPacking" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPacking_SelectedIndexChanged"
                                                                                    Visible="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <div class="textbox_small">
                                                                                <asp:DropDownList ID="ddlQuantity" runat="server" AutoPostBack="true" Visible="true">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div class="textbox115">
                                                                                <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <div class="textbox_small">
                                                                                <asp:Label ID="LabelPrice" runat="server" Style="text-align: center;" Text=""></asp:Label>
                                                                                <asp:TextBox ID="lblPrice" runat="server" Style="text-align: center;" CssClass="filter_qty"></asp:TextBox>
                                                                                <span style="position: relative; float: right; left: 10px; line-height: 31px;">€</span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="109px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="40">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVat" runat="server" Text="23%"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="40px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" after VAT" ItemStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="76">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAfterVat" runat="server" Style="margin-left: -10px;" Text=""></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="76px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                                                                        ItemStyle-Width="80">
                                                                        <ItemTemplate>
                                                                            <div class="textbox_small_new">
                                                                                <asp:DropDownList ID="ddlQty" runat="server" AutoPostBack="true" Style="margin-left: -11px;"
                                                                                    OnSelectedIndexChanged="ddlQty_SelectedIndexChanged" Visible="false">
                                                                                </asp:DropDownList>
                                                                                <asp:TextBox ID="txtQty" runat="server" OnTextChanged="txtQty__TextChanged" AutoPostBack="true"
                                                                                    CssClass="filter_qty" Text="0"></asp:TextBox>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Subtotals">
                                                                        <ItemTemplate>
                                                                            <div class="textbox_small">
                                                                                <asp:Label ID="lblSubtotals" runat="server" Style="line-height: 33px; text-align: center;"
                                                                                    Text="00,00"></asp:Label>
                                                                                <span style="position: relative; float: right; left: 8px; line-height: 31px;">€</span>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="header_row" />
                                                                <AlternatingRowStyle CssClass="alternate_row" />
                                                            </asp:GridView>
                                                        </fieldset>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <br />
                                        <table cellpadding="0" cellspacing="0" border="0" style="float: right; margin-right: 54px;">
                                            <tr>
                                                <td style="width: 144px;">
                                                    Estimated Subtotal
                                                </td>
                                                <td>
                                                    <div class="textbox_small">
                                                        <asp:Label ID="lblEstimatedSubtotal" runat="server" Text="00,00" Style="line-height: 33px;
                                                            text-align: center;"></asp:Label>
                                                        <span style="position: relative; float: right; left: 8px; line-height: 31px;">€</span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <br />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table cellpadding="0" cellspacing="0" border="0" style="float: right; margin-right: 28px;">
                        <tr>
                            <td style="position: absolute; margin-left: -165px;">
                                <asp:ImageButton ID="imgBtnNextToFinal" runat="server" ImageUrl="../Images/btn_next.png"
                                    OnClick="imgBtnNextToFinal_Click" />
                            </td>
                            <td style="width: 160px; font-weight: bold;">
                                Estimated Grand Total
                            </td>
                            <td>
                                <div class="textbox115">
                                    <asp:Label ID="lblGrandSubtotal" runat="server" Text="00,00" Style="line-height: 33px;
                                        font-size: 15px; font-weight: bold; font-family: Arial; text-align: center;"></asp:Label>
                                    <span style="position: relative; float: right; left: 8px; line-height: 31px;">€</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:View>
            <asp:View ID="vFinalOrders" runat="server">
                <asp:Label ID="lblMessageTop" runat="server" Text="All Orders are successfully saved."
                    Style="color: Green; font-size: 15px; margin-left: 286px; display: none;"></asp:Label>
                <fieldset style="background-color: White;">
                    <legend>
                        <asp:Label ID="lblOrderStatus_For_Final_Orders" runat="server"></asp:Label>
                    </legend>
                    <table>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgSaveAllTop" runat="server" ImageUrl="../Images/btn_saveall.png"
                                    Style="margin-left: 120px;" OnClick="imgSaveAllTop_Click" />
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:UpdatePanel ID="upnlSuppliers" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdSuppliers" runat="server" GridLines="None" OnRowCommand="grdSuppliers_RowCommand"
                                            OnRowDataBound="grdSuppliers_RowDataBound" ShowHeader="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <fieldset style="width: 860px; background-color: #e8e8e8; padding: 0px;">
                                                            <legend id="lgd_supplier" runat="server">
                                                                <asp:Label ID="lblSupplierName" runat="server" Text="<%#Bind('sBrandName') %>" Style="padding-left: 5px;
                                                                    padding-right: 5px;"></asp:Label>
                                                                <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pkSupplierID') %>" />
                                                                <asp:HiddenField ID="hidFavorite" runat="server" Value="" />
                                                            </legend>
                                                            <table>
                                                                <tr style="display: none;">
                                                                    <td colspan="2">
                                                                        <div style="float: left;">
                                                                            <div class="textbox204" style="float: left;">
                                                                                <asp:DropDownList ID="ddlFavoriteMethod" runat="server">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div style="float: left; line-height: 27px; padding-left: 40px;">
                                                                            <asp:Label ID="lblOrderMessage" runat="server" Text="" Style="color: Green; font-size: 14px;"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                    <td align="right">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:GridView ID="grdOrders_FinalOrder" runat="server" AlternatingRowStyle-CssClass="alternate_row"
                                                                            GridLines="None" HeaderStyle-CssClass="header_row" OnRowCommand="grdOrders_FinalOrder_RowCommand"
                                                                            OnRowDataBound="grdOrders_FinalOrder_RowDataBound" RowStyle-CssClass="rowstyle">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Product" ItemStyle-Width="110">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkProductName" runat="server" CommandArgument="<%#Bind('pkproductid') %>"
                                                                                            CommandName="name" Text="<%#Bind('Product') %>"></asp:LinkButton>
                                                                                        <asp:HiddenField ID="hidProductID" runat="server" Value="<%#Bind('pkproductid') %>" />
                                                                                        <asp:HiddenField ID="hidPSupplierid" runat="server" Value="<%#Bind('pkSupplierID') %>" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="110px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Packing & Quantity">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox115">
                                                                                            <asp:DropDownList ID="ddlPacking" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPacking_SelectedIndexChanged"
                                                                                                Visible="true">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox115">
                                                                                            <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Price">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small">
                                                                                            <asp:Label ID="LabelPrice" runat="server" Style="text-align: center;" Text=""></asp:Label>
                                                                                            <span style="position: relative; left: 9px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="109px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="New Price">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small" style="background: url(../images/textbox_small_yellow.png) no-repeat;">
                                                                                            <%--<asp:Label ID="lblPrice" runat="server" Style="text-align: center;" Text=""></asp:Label>--%>
                                                                                            <asp:TextBox ID="lblPrice" runat="server" Style="text-align: center; width: 64px;"
                                                                                                OnTextChanged="lblPrice_TextChanged" AutoPostBack="true" CssClass="lblPrice_qty"
                                                                                                Text="00,00"></asp:TextBox>
                                                                                            <span style="position: relative; left: 9px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="109px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="difference">
                                                                                    <ItemTemplate>
                                                                                        <div style="float: right;">
                                                                                            <asp:Label ID="lblPriceDifference" runat="server" Style="font-weight: bold; float: left;"
                                                                                                Text="00,00 €"></asp:Label>
                                                                                            <img id="img" runat="server" width="13" src="" style="float: left; padding-left: 3px;
                                                                                                padding-top: 3px;" />
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="VAT" ItemStyle-Width="40">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVat" runat="server" Text="23%"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="40px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" after VAT" ItemStyle-HorizontalAlign="Center"
                                                                                    ItemStyle-Width="76">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAfterVat" runat="server" Style="margin-left: -10px;" Text=""></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="76px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Amount" ItemStyle-HorizontalAlign="Center"
                                                                                    ItemStyle-Width="80">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small_new">
                                                                                            <asp:DropDownList ID="ddlQty" runat="server" AutoPostBack="true" Style="margin-left: -11px;"
                                                                                                Visible="false" OnSelectedIndexChanged="ddlQty_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                            <asp:TextBox ID="txtQty" runat="server" OnTextChanged="txtQty__TextChanged" AutoPostBack="true"
                                                                                                CssClass="filter_qty" Text="0"></asp:TextBox>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Estimated Subtotal">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small">
                                                                                            <asp:Label ID="lblSubtotals" runat="server" Style="line-height: 33px; text-align: center;"
                                                                                                Text="00,00"></asp:Label>
                                                                                            <span style="position: relative; left: -2px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Final Subtotal">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small" style="background: url(../images/textbox_small_yellow.png) no-repeat;">
                                                                                            <asp:Label ID="lblFinalSubtotal" runat="server" Style="line-height: 33px; text-align: center;"
                                                                                                Text="00,00"></asp:Label>
                                                                                            <span style="position: relative; left: -2px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Difference">
                                                                                    <ItemTemplate>
                                                                                        <div id="divDifference" runat="server" class="textbox_small">
                                                                                            <asp:Label ID="lblFinalDifference" runat="server" Style="line-height: 33px; text-align: center;"
                                                                                                Text="00,00"></asp:Label>
                                                                                            <span style="position: relative; left: -2px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="header_row" />
                                                                            <AlternatingRowStyle CssClass="alternate_row" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="padding-top: 20px;">
                                                                <tr style="height: 50px;">
                                                                    <td align="left" style="width: 598px;">
                                                                        <div style="width: 198px;">
                                                                            <div style="float: left;">
                                                                                <div style="float: left; line-height: 28px; padding-right: 10px;">
                                                                                    Delivery Start Time :
                                                                                </div>
                                                                                <div style="float: left;">
                                                                                    <asp:TextBox ID="txtDeliveryTime" runat="server" Text="" Style="float: left;" CssClass="filter"></asp:TextBox>
                                                                                    <asp:ImageButton ID="btnDeliveryTime" runat="server" Style="margin-left: 5px;" ImageUrl="~/Images/btn_time.png"
                                                                                        Width="20" Height="20" OnClick="btnDeliveryTime_Click" />
                                                                                </div>
                                                                                <%--  <Ajaxified:TimePicker ID="TimePicker2" runat="server" TargetControlID="txtDeliveryTime"
                                                                                    MinuteStep="15" CloseOnSelection="true"></Ajaxified:TimePicker>--%>
                                                                            </div>
                                                                            <div style="float: right;">
                                                                                <div style="float: left; line-height: 28px; padding-right: 10px;">
                                                                                    Delivery End Time :
                                                                                </div>
                                                                                <div style="float: left">
                                                                                    <asp:TextBox ID="txtDeliveryTimeEnd" runat="server" Text="" Style="float: left;"
                                                                                        CssClass="filter"></asp:TextBox>
                                                                                    <asp:ImageButton ID="btnDeliveryTimeEnd" runat="server" Width="20" Height="20" Style="margin-left: 5px;"
                                                                                        ImageUrl="~/Images/btn_time.png" OnClick="btnDeliveryTimeEnd_Click" />
                                                                                </div>
                                                                                <%-- <Ajaxified:TimePicker ID="TimePicker1" runat="server" TargetControlID="txtDeliveryTimeEnd"
                                                                                    MinuteStep="15" CloseOnSelection="true"></Ajaxified:TimePicker>--%>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td style="width: 146px; display: none;">
                                                                        <asp:CheckBox ID="chkReady" runat="server" Text="Ready" />
                                                                    </td>
                                                                    <td align="right" style="width: 265px; display: none;">
                                                                        <div style="float: right; margin-right: 32px;">
                                                                            <div style="float: left; line-height: 28px; padding-right: 10px;">
                                                                                <asp:Label ID="lblSTotal" runat="server" Text="Big Time Supplier Total"></asp:Label>
                                                                            </div>
                                                                            <div class="textbox_small" style="float: left;">
                                                                                <asp:Label ID="lblSupplierTotal" runat="server" Text="00,00" Style="line-height: 30px;"></asp:Label>
                                                                                <span style="position: relative; float: right; left: 8px; line-height: 31px;">€</span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <div style="margin-left: 8px;">
                                                                            <table>
                                                                                <tr>
                                                                                    <td colspan="3">
                                                                                        <span>----------------------------------------------</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <div class="textbox_small">
                                                                                            <asp:Label ID="lblAggEstimated" runat="server" Style="text-align: center;" Text="00,00"></asp:Label>
                                                                                            <span style="position: relative; left: -2px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <div class="textbox_small" style="background: url(../images/textbox_small_yellow.png) no-repeat;">
                                                                                            <asp:Label ID="lblAggFinal" runat="server" Style="text-align: center;" Text="00,00"></asp:Label>
                                                                                            <span style="position: relative; left: -2px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <div id="divAggDifference" runat="server" class="textbox_small">
                                                                                            <asp:Label ID="lblAggDifference" runat="server" Style="text-align: center;" Text="00,00"></asp:Label>
                                                                                            <span style="position: relative; left: -2px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        Estimated
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Final
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        Difference
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <div class="textboxmulti_850">
                                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <div class="textboxmulti_850" style="background: url(../images/textbox_850_yellow.png) no-repeat;">
                                                                            <asp:TextBox ID="txtNewComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="1">
                                                                        <asp:CheckBox ID="chkInvoice" runat="server" Text="Invoice?" />
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:ImageButton ID="imgBtnSaveReceivedOrder" runat="server" ImageUrl="../Images/btn_save_account.png"
                                                                            OnClick="imgBtnSaveReceivedOrder_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    Invoice 1 Number
                                                                                </td>
                                                                                <td>
                                                                                    <div class="textbox115">
                                                                                        <asp:TextBox ID="txtInvoiceNumber" runat="server" Style="text-align: center; width: 90px;"></asp:TextBox>
                                                                                    </div>
                                                                                </td>
                                                                                <td>
                                                                                    Invoice 1 Amount
                                                                                </td>
                                                                                <td>
                                                                                    <div class="textbox115">
                                                                                        <asp:TextBox ID="txtInvoiceAmount" runat="server" Style="text-align: center; width: 90px;"
                                                                                            CssClass="lblPrice_qty" Text="00,00"></asp:TextBox>
                                                                                        <span style="position: relative; float: right; left: -10px; top: -7px; width: 0px;
                                                                                            line-height: 31px;">€</span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <div style="padding-left: 219px;">
                                                                            <table>
                                                                                <tr>
                                                                                    <td align="right" style="width: 126px;">
                                                                                        Non Invoice Amount
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox_small" style="width: 80px;">
                                                                                            <asp:TextBox ID="txtNonInvoiceAmount" runat="server" Style="text-align: center; width: 57px;"
                                                                                                CssClass="lblPrice_qty" Text="00,00"></asp:TextBox>
                                                                                            <span style="left: 13px; line-height: 31px; width: 0px;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px; padding-left: 10px;">
                                                                                        previous balance
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        order balance
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        Amount Paid
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        new balance
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        Order TOTAL
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <div class="textbox_small" style="background: url(../images/textbox_small_red.png) no-repeat;">
                                                                                            <asp:Label ID="lblOrderTotal" runat="server" Style="text-align: center; color: White;"
                                                                                                CssClass="lblPrice_qty" Text="00,00"></asp:Label>
                                                                                            <span style="left: 13px; line-height: 31px; width: 0px; color: White;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        <div class="textbox_small" style="background: url(../images/textbox_small_red.png) no-repeat;">
                                                                                            <asp:Label ID="lblPreviousBalance" runat="server" Style="text-align: center; color: White;"
                                                                                                CssClass="lblPrice_qty" Text="00,00"></asp:Label>
                                                                                            <span style="left: 13px; line-height: 31px; width: 0px; color: White;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        <div class="textbox_small" style="background: url(../images/textbox_small_red.png) no-repeat;">
                                                                                            <asp:Label ID="lblOrderBalance" runat="server" Style="text-align: center; color: White;"
                                                                                                CssClass="lblPrice_qty" Text="00,00"></asp:Label>
                                                                                            <span style="left: 13px; line-height: 31px; width: 0px; color: White;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        <div class="textbox_small">
                                                                                            <asp:Label ID="lblAmountPaid" runat="server" Style="text-align: center; color: White;
                                                                                                display: none;" CssClass="lblPrice_qty" Text="00,00"></asp:Label>
                                                                                            <asp:TextBox ID="txtAmountPaid" runat="server" Style="text-align: center; float: left;
                                                                                                font-size: 12px; font-weight: bold;" CssClass="lblPrice_qty" Text="00,00" AutoPostBack="true"
                                                                                                OnTextChanged="txtAmountPaid_TextChanged"></asp:TextBox>
                                                                                            <span style="left: 13px; line-height: 16px; width: 0px; float: left;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td align="center" style="padding-right: 10px;">
                                                                                        <div id="divNewBalance" runat="server" class="textbox_small" style="background: url(../images/textbox_small_green.png) no-repeat;">
                                                                                            <asp:Label ID="lblNewBalance" runat="server" Style="text-align: center; color: White;"
                                                                                                CssClass="lblPrice_qty" Text="00,00"></asp:Label>
                                                                                            <span style="left: 13px; line-height: 31px; width: 0px; color: White;">€</span>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td valign="bottom">
                                <asp:ImageButton ID="imgBtnSaveAllBottom" runat="server" ImageUrl="../Images/btn_saveall.png"
                                    OnClick="imgSaveAllTop_Click" Style="margin-left: 125px;" />
                            </td>
                            <td align="right" valign="bottom">
                                <asp:UpdatePanel ID="upnlGrand" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <div class="textbox115" style="width: 117px;">
                                                        <asp:TextBox ID="txtGrandEstimated" runat="server" Style="text-align: center; width: 90px;"
                                                            CssClass="lblPrice_qty" Text="00,00" ReadOnly="true"></asp:TextBox>
                                                        <span style="position: relative; float: right; left: -12px; top: -7px; width: 0px;
                                                            line-height: 31px;">€</span>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div class="textbox115" style="background: url(../images/textbox_light_yellow.png) no-repeat;
                                                        width: 117px;">
                                                        <asp:TextBox ID="txtGrandFinal" runat="server" Style="text-align: center; width: 90px;"
                                                            CssClass="lblPrice_qty" Text="00,00" ReadOnly="true"></asp:TextBox>
                                                        <span style="position: relative; float: right; left: -12px; top: -7px; width: 0px;
                                                            line-height: 31px;">€</span>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div id="divGrandDifference" runat="server" class="textbox115" style="width: 117px;">
                                                        <asp:TextBox ID="txtGrandDifference" runat="server" Style="text-align: center; width: 90px;"
                                                            CssClass="lblPrice_qty" Text="00,00" ReadOnly="true"></asp:TextBox>
                                                        <asp:Label ID="lblSpanGrandDifference" runat="server" Style="position: relative;
                                                            float: right; left: -12px; top: -7px; width: 0px; line-height: 31px;">€</asp:Label>
                                                        <span style="position: relative; float: right; left: -12px; top: -7px; width: 0px;
                                                            line-height: 31px; display: none;">€</span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    Estimated
                                                </td>
                                                <td align="center">
                                                    FINAL Total
                                                </td>
                                                <td align="center">
                                                    difference
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="background-color: #323136;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 10px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    Cash
                                                </td>
                                                <td align="center">
                                                    Paid
                                                </td>
                                                <td align="center">
                                                    Left
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <div class="textbox115" style="width: 117px;">
                                                        <asp:TextBox ID="txtCash" runat="server" Style="text-align: center; width: 90px;"
                                                            CssClass="lblPrice_qty" Text="00,00" ReadOnly="true"></asp:TextBox>
                                                        <span style="position: relative; float: right; left: -12px; top: -7px; width: 0px;
                                                            line-height: 31px;">€</span>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div class="textbox115" style="background: url(../images/textbox_light_yellow.png) no-repeat;
                                                        width: 117px;">
                                                        <asp:TextBox ID="txtPaid" runat="server" Style="text-align: center; width: 90px;"
                                                            CssClass="lblPrice_qty" Text="00,00" ReadOnly="true"></asp:TextBox>
                                                        <span style="position: relative; float: right; left: -12px; top: -7px; width: 0px;
                                                            line-height: 31px;">€</span>
                                                    </div>
                                                </td>
                                                <td align="center">
                                                    <div id="divLeft" runat="server" class="textbox115" style="width: 117px;">
                                                        <asp:TextBox ID="txtLeft" runat="server" Style="text-align: center; color: White;
                                                            width: 90px;" CssClass="lblPrice_qty" Text="00,00"></asp:TextBox>
                                                        <span style="position: relative; float: right; left: -12px; top: -7px; width: 0px;
                                                            color: White; line-height: 31px;">€</span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="display: none;">
                                <div style="float: right; margin-right: 42px;">
                                    <div style="float: left; line-height: 28px; padding-right: 10px; font-weight: bold;">
                                        Estimated Grand Total
                                    </div>
                                    <div class="textbox115" style="float: left; font-size: 19px; font-family: Arial;
                                        font-weight: bold; line-height: 28px;">
                                        <asp:Label ID="lblGrandFinalTotal" runat="server" Text="00,00"></asp:Label>
                                        <span style="position: relative; float: right; left: 8px; line-height: 31px;">€</span>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:View>
        </asp:MultiView>
    </div>
    <div id="divEmail" runat="server" style="display: none;">
        <table id="tblSampleText" runat="server" align="center">
        </table>
    </div>
     <cc1:ModalPopupExtender ID="MPEDate" runat="server" TargetControlID="btnDate" PopupControlID="Panel1"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnDate" runat="server" Style="display: none;" />
    <asp:Panel ID="Panel1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="lightbox-header_new">
                    <a href="#" title="Close" onclick="$find('<%=MPEDate.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center; width:400px;">
                    <table cellpadding="3" cellspacing="3" border="0" align="center" width="100%">
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                <asp:DataList ID="dlDeliveryTime" runat="server" OnItemDataBound="dlDeliveryTime_ItemDataBound"
                                    RepeatColumns="4" RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2"
                                    Width="100%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTime" runat="server" style="cursor:pointer;"></asp:Label>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
