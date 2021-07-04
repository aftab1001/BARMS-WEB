<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    EnableEventValidation="false" AutoEventWireup="true" CodeFile="ManageSupplier.aspx.cs"
    Inherits="DepartmentAdmin_ManageSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script src="./JavaScript/jquery.ui.datepicker.js" type="text/javascript"></script>

    <script type="text/javascript">




        $(function() {



            //            var $scrollingDiv = $("#ctl00_ContentPlaceHolder1_scrollingDiv");

            //            $(window).scroll(function() {
            //                $scrollingDiv
            //            .stop()
            //            .animate({ "marginTop": ($(window).scrollTop() + 10) + "px" }, "slow");
            //            });

            $('input.filterNum').bind('keyup blur', function() {
                if (this.value.match(/[^-+()0-9]/g)) {
                    this.value = this.value.replace(/[^-+()0-9]/g, '');
                }
            });
            $('input.filter').bind('keyup blur', function() {
                if (this.value.match(/[^,.0-9]/g)) {
                    this.value = this.value.replace(/[^,.0-9]/g, '');
                }
                else {
                    if (this.value.split(',').length > 2) {
                        this.value = this.value.substring(0, this.value.lastIndexOf(','));
                    }
                }
            });
            $('#ctl00_ContentPlaceHolder1_trAddSupplier').mouseover(function() {
                $('#ctl00_ContentPlaceHolder1_btnHide').css('display', 'none');
            });
            $('#ctl00_ContentPlaceHolder1_trAddSupplier').mouseout(function() {
                $('#ctl00_ContentPlaceHolder1_btnHide').css('display', 'none');
            });
            //*****************************************************************************************
            $('#ctl00_ContentPlaceHolder1_trAddCompany').mouseover(function() {
                $('#ctl00_ContentPlaceHolder1_btnHideCompany').css('display', 'none');
            });
            $('#ctl00_ContentPlaceHolder1_trAddCompany').mouseout(function() {
                $('#ctl00_ContentPlaceHolder1_btnHideCompany').css('display', 'none');
            });
            //*********************************************************************************************
            $('#ctl00_ContentPlaceHolder1_trAddContactPeople').mouseover(function() {
                $('#ctl00_ContentPlaceHolder1_btnHideContact').css('display', 'none');
            });
            $('#ctl00_ContentPlaceHolder1_trAddContactPeople').mouseout(function() {
                $('#ctl00_ContentPlaceHolder1_btnHideContact').css('display', 'none');
            });
            //*********************************************************************************************
            $('#ctl00_ContentPlaceHolder1_txtSupplierFaxNote').watermark('Note');
            $('#ctl00_ContentPlaceHolder1_txtSupplierFax').watermark('Fax');
            $("#ctl00_ContentPlaceHolder1_lblRecordMessage").hide();
            ApplyJqueryForProductLinking();

            ApplyJquery();
            ShowDataPicker();
            filter();
            ShowDataPicker();



        });





        function GridDate(txt) {

            $("#" + txt.id).datepicker({
                dateFormat: 'dd/mm/yy'
            });
        }

        function loadDate() {
            var rownum = 2;
            var commonid = "ctl00_ContentPlaceHolder1_grdProductPrices_ctl";

            var grid = document.getElementById("<%= grdProductPrices.ClientID %>");

            var income;
            for (i = 1; i < grid.rows.length; i++) {

                if (i > 8)
                    income = document.getElementById(commonid + (i + 1).toString() + "_" + "txtDate");
                else
                    income = document.getElementById(commonid + "0" + (i + 1).toString() + "_" + "txtDate");

                if (income != null) {

                    $("#" + income.id).datepicker({
                        dateFormat: 'dd/mm/yy'
                    });
                }
            }
        }

        function ShowDataPicker() {

            $("#ctl00_ContentPlaceHolder1_txtFromDate").datepicker({
                dateFormat: 'dd/mm/yy'

            });
            $("#ctl00_ContentPlaceHolder1_txtTillDate").datepicker({
                dateFormat: 'dd/mm/yy'
            });

            $("#ctl00_ContentPlaceHolder1_txtFromDatePopup").datepicker({
                dateFormat: 'dd/mm/yy'

            });
            $("#ctl00_ContentPlaceHolder1_txtEndDatePopup").datepicker({
                dateFormat: 'dd/mm/yy'
            });

            $("#ctl00_ContentPlaceHolder1_txtFromDatePopup").datepicker({
                dateFormat: 'dd/mm/yy'
            });

            $("#ctl00_ContentPlaceHolder1_txtEndDatePopup").datepicker({
                dateFormat: 'dd/mm/yy'
            });

        }
        function RecordSaved() {
            //            $("#ctl00_ContentPlaceHolder1_lblRecordMessage").show().delay(100).fadeOut();
            $("#ctl00_ContentPlaceHolder1_lblRecordMessage").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblRecordMessage");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblRecordMessage").fadeOut('slow');
                }
            }, 4000);
        }
        function DatePicker() {
            $("#ctl00_ContentPlaceHolder1_txtFromDatePopup").datepicker({
                dateFormat: 'dd/mm/yy'
            });
        }
        function SupplierSaved() {
            $("#ctl00_ContentPlaceHolder1_lblAddSupplierMessage").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblAddSupplierMessage");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblAddSupplierMessage").fadeOut('slow');
                }
            }, 4000);

        }
        function filter() {
            $('input.filter').bind('keyup blur', function() {
                if (this.value.match(/[^,.0-9]/g)) {
                    this.value = this.value.replace(/[^,.0-9]/g, '');
                }
                else {
                    if (this.value.split(',').length > 2) {
                        this.value = this.value.substring(0, this.value.lastIndexOf(','));
                    }
                }
            });
        }
        function Pdf() {
            var a = window.open("../pdfGenerator.aspx?r=pdfo&id=", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function ApplyJqueryForProductLinking() {

            $('#divSuppierBaseTitle').click(function() {

                $('#divSupplierBase').toggle();
                if ($('#divSupplierBase:visible').length > 0) {
                    $('#divSuppierBaseTitle').html("Collapse");
                }
                else {
                    $('#divSuppierBaseTitle').html("Expand");
                }

            });

            $('#divSupplierSubTitle').click(function() {

                $('#divSupplierSub').toggle();
                if ($('#divSupplierSub:visible').length > 0) {
                    $('#divSupplierSubTitle').html("Collapse");
                }
                else {
                    $('#divSupplierSubTitle').html("Expand");
                }

            });

            $('#divSupplierProTitle').click(function() {

                $('#divSupplierPro').toggle();
                if ($('#divSupplierPro:visible').length > 0) {
                    $('#divSupplierProTitle').html("Collapse");
                }
                else {
                    $('#divSupplierProTitle').html("Expand");
                }

            });


        }
        function ApplyJquery() {

            $('.tab1').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab1').show();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();

                $(this).css("background-color", "#BFB8BB");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });


            $('.tab2').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab2').show();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });

            $('.tab3').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab3').show();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });
            $('.tab4').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab4').show();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
            });
        }
        
    </script>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

  <%--  <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });

        function drawChart() {
            google.setOnLoadCallback(drawChart);
            var dtPoints = new google.visualization.DataTable();
            dtPoints.addColumn('string', 'Date');
            dtPoints.addColumn('number', 'Price');

            var options = {
                title: 'Company Performance'
            };

            var sWeek = '2001,2005,2006';
            var sAmount = '10,15,20';
            var splittedWeeks = sWeek.split(',');
            var splittedAmount = sAmount.split(',');

            if (splittedWeeks.length == splittedAmount.length) {
                for (var a = 0; a < splittedAmount.length; a++) {

                    var arrayValues = new Array();
                    arrayValues[0] = splittedWeeks[a];
                    arrayValues[1] = parseInt(splittedAmount[a]);

                    dtPoints.addRow(arrayValues);
                }
                var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
                chart.draw(dtPoints, options);
            }
        }
    </script>--%>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
      
        <table style="display: none;">
            <tr>
                <td class="height30">
                    <img src="../images/horizontal_line.png" alt="" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="imgBtnAddCompany" runat="server" ImageUrl="~/Images/btn_add_company.png"
                        OnClick="imgBtnAddCompany_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Div1" runat="server">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="width: 480px;">
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
                                        Companies<div class="clear_10">
                                        </div>
                                        <asp:GridView ID="grdCompanies" runat="server" AutoGenerateColumns="false" Width="100%"
                                            AllowPaging="true" PageSize="5" EmptyDataText="No any Supplier Exists." GridLines="None"
                                            HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                            RowStyle-CssClass="rowstyle" OnRowCommand="grdCompanies_RowCommand" OnRowDataBound="grdCompanies_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Brand" ItemStyle-Width="300">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCompany" runat="server" CommandName="name" CommandArgument="<%#Bind('pkCompanyID') %>"
                                                            Text="<%#Bind('cBrandName') %>"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="dModifiedDate" HeaderText="Modified Date" />
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
                <td class="height30">
                    <img src="../images/horizontal_line.png" alt="" />
                </td>
            </tr>
        </table>
        <div class="clear_10">
        </div>
        <div id="ShowWorkshift" runat="server" style="display: block;">
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
                        <td class="whitebox_centermiddlebg" style="padding: 0;">
                            <div class="rounded_box_big">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <div style="background-color: Transparent; width: 775px;">
                                                <div style="background-color: Transparent; width: 775px; float: left;">
                                                    <div style="background-color: #BFB8BB; width: 150px; height: 40px; line-height: 36px;
                                                        float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab1">
                                                        Supplier</div>
                                                    <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                        float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab2">
                                                        Supplier Orders</div>
                                                    <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                        float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab3">
                                                        Supplier Products</div>
                                                    <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                        float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab4">
                                                        Products Linking</div>
                                                </div>
                                                <div style="background-color: Transparent; width: 775px; float: left;">
                                                    <div id="divTab1" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                        width: auto; display: block; padding-left: 10px;">
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="imgBtnAddSupplier" runat="server" ImageUrl="~/Images/btn_addsuplier.png"
                                                                        OnClick="imgBtnAddSupplier_Click" />
                                                                    <asp:ImageButton ID="imgBtnAddSupplierProducts" runat="server" ImageUrl="~/Images/btn_addsupplier.png"
                                                                        Style="display: none;" OnClick="imgBtnAddSupplierProducts_Click" />
                                                                    <asp:LinkButton ID="lnkBackToSpplier" runat="server" Visible="false" Style="font-size: 12px;
                                                                        font-weight: normal; color: #619ae9" OnClick="lnkBackToSpplier_Click1">
                                                                    <img src="../images/back_arrow.png" alt="" />Back to Suppliers List
                                                                    </asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAddSupplierMessage" runat="server" Text="Successfully Added!" Style="color: Green;
                                                                        position: relative; left: 105px; font-weight: bold; display: none;"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:MultiView ID="mvTab1" runat="server" ActiveViewIndex="2">
                                                            <asp:View ID="vAddSupplier" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="0" cellspacing="0" class="width100percent">
                                                                                <tr>
                                                                                    <td class="width22percent align_center_top">
                                                                                        <img id="imgNewSupplierPic" runat="server" src="../Images/no_image.gif" alt="" /><br />
                                                                                        <br />
                                                                                        <asp:FileUpload ID="fpNewFileUploadSupplier" runat="server" EnableViewState="true"
                                                                                            Style="width: 151px;" />
                                                                                    </td>
                                                                                    <td class="width4percent align_center_top" style="width: 18%;">
                                                                                        <img src="../images/vertical_line.png" alt="" />
                                                                                    </td>
                                                                                    <td class="width44percent align_top">
                                                                                        <table border="0" align="center" cellpadding="4" cellspacing="4">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Name:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="textbox204">
                                                                                                        <asp:TextBox ID="txtAddSupplierName" runat="server"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Style="float: right;
                                                                                                            position: relative; top: 6px;" ControlToValidate="txtAddSupplierName" Display="Dynamic"
                                                                                                            ErrorMessage="*" ValidationGroup="reqadd"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="align_left">
                                                                                                    Contact Method:
                                                                                                </td>
                                                                                                <td class="align_center">
                                                                                                    <asp:RadioButton ID="rdAddEmail" runat="server" Text="Email" GroupName="add" Checked="true" />
                                                                                                    <asp:RadioButton ID="rdAddFax" runat="server" Text="Phone" GroupName="add" />
                                                                                                    <asp:RadioButton ID="rdAddPhone" runat="server" Text="Fax" GroupName="add" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="height30">
                                                                            <img src="../images/horizontal_line.png" alt="" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                                                                <tr>
                                                                                    <td class="label_bold">
                                                                                        Email
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtAddEmailSupplier" runat="server"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Style="float: right;
                                                                                                position: relative; top: 6px;" ControlToValidate="txtAddEmailSupplier" Display="Dynamic"
                                                                                                ErrorMessage="*" ValidationGroup="reqadd"></asp:RequiredFieldValidator>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"
                                                                                                ControlToValidate="txtAddEmailSupplier" EnableClientScript="true" Display="Dynamic"
                                                                                                ErrorMessage="*" Text="Incorrect Email format." Style="position: relative; line-height: 16px;"
                                                                                                ValidationGroup="reqadd"></asp:RegularExpressionValidator>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="label_bold">
                                                                                        <span class="width15percent label_bold">Fax</span>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            <asp:TextBox ID="txtAddFaxSupplier" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label_bold">
                                                                                        Mobile Phone
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtAddMobileSupplier" runat="server" CssClass="filterNum"></asp:TextBox></div>
                                                                                    </td>
                                                                                    <td class="label_bold">
                                                                                        Web Site
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtWebSite" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="height30">
                                                                            <img src="../images/horizontal_line.png" alt="" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="0" cellspacing="0" class="width100percent">
                                                                                <tr>
                                                                                    <td class="label_bold" style="width: 114px;">
                                                                                        Social Profile 1
                                                                                    </td>
                                                                                    <td style="width: 247px;">
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtSocialProfile1" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                    <td class="label_bold" style="width: 123px;">
                                                                                        Social Profile 2
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtSocialProfile2" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="height30">
                                                                            <img src="../images/horizontal_line.png" alt="" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                                                                <tr>
                                                                                    <td class="label_bold" width="26%">
                                                                                        Address
                                                                                    </td>
                                                                                    <td width="72%">
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtAddAddressSuppplier" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="label_bold">
                                                                                        Town
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtAddTownSupplier" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label_bold">
                                                                                        Post Code
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtAddPostCodeSupplier" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                    <td class="label_bold">
                                                                                        Region
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:TextBox ID="txtAddRegionSupplier" runat="server"></asp:TextBox></div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="label_bold">
                                                                                        Country
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            &nbsp;<asp:DropDownList ID="ddlAddCountries" runat="server" AutoPostBack="false">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:ImageButton ID="imgBtnSaveSupplier" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                ValidationGroup="reqadd" OnClick="imgBtnSaveSupplier_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:View>
                                                            <asp:View ID="vEditSupplier" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="min-height: 25px;">
                                                                                <asp:Button ID="btnHide" runat="server" Text="Hide" Style="position: relative; float: right;
                                                                                    top: 0; right: 0; display: none;" OnClick="btnHide_Click" Visible="false" />
                                                                            </div>
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td class="width22percent align_center_top">
                                                                                                    <img id="supplierImage" runat="server" width="149" src="../images/no_image.gif" alt="" /><br />
                                                                                                    <br />
                                                                                                    <asp:FileUpload ID="fpUploadPic" runat="server" />
                                                                                                    <br />
                                                                                                    <br />
                                                                                                    <asp:ImageButton ID="imgBtnImageUploadTop" runat="server" ImageUrl="~/Images/btn_update.png"
                                                                                                        Style="margin-left: -42px;" OnClick="imgBtnImageUploadTop_Click" />
                                                                                                </td>
                                                                                                <td class="width4percent align_center_top" style="width: 15%">
                                                                                                    <img src="../images/vertical_line.png" alt="" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                                                                                        <tr>
                                                                                                            <td style="width: 206px; padding-right: 15px;" align="right">
                                                                                                                Brand Name:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="textbox204" style="float: left;">
                                                                                                                    <asp:TextBox ID="txtSupplierBrand" runat="server"></asp:TextBox>
                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Style="float: right;
                                                                                                                        position: relative; top: 6px;" ControlToValidate="txtSupplierBrand" Display="Dynamic"
                                                                                                                        ErrorMessage="*" ValidationGroup="req"></asp:RequiredFieldValidator>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="width: 171px; padding-right: 15px;" align="right">
                                                                                                                webSite:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="textbox204" style="float: left;">
                                                                                                                    <asp:TextBox ID="txtSupplierWebsite" runat="server"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="width: 171px; padding-right: 15px;" align="right">
                                                                                                                Social Profile 1:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="textbox204" style="float: left;">
                                                                                                                    <asp:TextBox ID="txtSupplierSocialProfile1" runat="server"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td style="width: 171px; padding-right: 15px;" align="right">
                                                                                                                Social Profile 2:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="textbox204" style="float: left;">
                                                                                                                    <asp:TextBox ID="txtSupplierSocialProfile2" runat="server"></asp:TextBox>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td valign="top" style="width: 171px; padding-right: 15px;" align="right">
                                                                                                                Contact Method:
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:RadioButton ID="rdTelephone" runat="server" Text="Phone" GroupName="rd" Checked="true" /><br />
                                                                                                                <asp:RadioButton ID="rdContactEmail" runat="server" Text="EmailAddress" GroupName="rd" /><br />
                                                                                                                <asp:RadioButton ID="rdFax" runat="server" Text="Fax" GroupName="rd" /><br />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <div class="bottom_graybox_right" style="float: left;">
                                                                                                                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/Images/btn_update.png"
                                                                                                                        ValidationGroup="req" OnClick="btnUpdate_Click" />
                                                                                                                </div>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
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
                                                                                                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSupplierEmail">
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
                                                                                                                                    Email Address
                                                                                                                                    <div class="clear_10">
                                                                                                                                    </div>
                                                                                                                                    <asp:GridView ID="grdEmails" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="header_row"
                                                                                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Width="100%"
                                                                                                                                        EmptyDataText="Sorry! No Email." CellPadding="0" BorderStyle="None" BorderWidth="0"
                                                                                                                                        GridLines="None" Style="font-weight: normal;" OnRowCommand="grdEmails_RowCommand"
                                                                                                                                        OnRowDataBound="grdEmails_RowDataBound">
                                                                                                                                        <Columns>
                                                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="align_left">
                                                                                                                                                                <asp:Label ID="lblemail" runat="server" Text="<%#Bind('sEmail') %>"></asp:Label>
                                                                                                                                                            </td>
                                                                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                                                                <asp:ImageButton ID="imgbtnSetActiveEmail" runat="server" CommandName="active" CommandArgument='<%# Bind("pkSupplierEmails") %>'
                                                                                                                                                                    Width="16" ImageUrl="~/Images/Star Gray.png" ToolTip="Non-Active" />
                                                                                                                                                            </td>
                                                                                                                                                            <td class="align_right" style="width: 20px; display: none;">
                                                                                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkSupplierEmails") %>'
                                                                                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
                                                                                                                                                            </td>
                                                                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="edt" CommandArgument='<%# Bind("pkSupplierEmails") %>'
                                                                                                                                                                    ImageUrl="../images/edit_icon.png" ToolTip="" />
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
                                                                                                                        <asp:TextBox ID="txtSupplierEmail" runat="server"></asp:TextBox>
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Style="float: right;
                                                                                                                            position: relative; top: 6px;" ControlToValidate="txtSupplierEmail" Display="Dynamic"
                                                                                                                            ErrorMessage="*" ValidationGroup="re"></asp:RequiredFieldValidator>
                                                                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"
                                                                                                                            ControlToValidate="txtSupplierEmail" EnableClientScript="true" Display="Dynamic"
                                                                                                                            ErrorMessage="*" Text="Incorrect Email format." Style="position: relative; line-height: 39px;"
                                                                                                                            ValidationGroup="re"></asp:RegularExpressionValidator>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td class="width55percent">
                                                                                                                    <asp:ImageButton ID="btnSupplierEmail" runat="server" ImageUrl="../images/btn_addanotheremail.gif"
                                                                                                                        OnClick="btnSupplierEmail_Click" ValidationGroup="re" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                                <td class="width30">
                                                                                                </td>
                                                                                                <td class="align_top">
                                                                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSupplierMobile">
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
                                                                                                                                    Mobile Phone
                                                                                                                                    <div class="clear_10">
                                                                                                                                    </div>
                                                                                                                                    <asp:GridView ID="grdMobile" AllowSorting="True" AllowPaging="false" runat="server"
                                                                                                                                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                                                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                                                                                        EmptyDataText="Sorry! No Phone." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                                                                                        OnRowCommand="grdMobile_RowCommand" OnRowDataBound="grdMobile_RowDataBound">
                                                                                                                                        <Columns>
                                                                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                                                        <tr>
                                                                                                                                                            <td class="align_left">
                                                                                                                                                                <asp:Label ID="lblMobile" runat="server" Text="<%#Bind('phone') %>"></asp:Label>
                                                                                                                                                            </td>
                                                                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                                                                <asp:ImageButton ID="imgBtnActiveMobile" runat="server" CommandName="active" CommandArgument='<%# Bind("pkSupplierPhoneID") %>'
                                                                                                                                                                    Width="16" ImageUrl="~/Images/Star Gray.png" ToolTip="" />
                                                                                                                                                            </td>
                                                                                                                                                            <td class="align_right" style="width: 20px; display: none;">
                                                                                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkSupplierPhoneID") %>'
                                                                                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
                                                                                                                                                            </td>
                                                                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                                                                <asp:ImageButton ID="imgEdit" runat="server" CommandName="edt" CommandArgument='<%# Bind("pkSupplierPhoneID") %>'
                                                                                                                                                                    ImageUrl="../images/edit_icon.png" ToolTip="Edit" />
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
                                                                                                                        <asp:TextBox ID="txtSupplierMobile" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Style="float: right;
                                                                                                                            position: relative; top: 6px;" ControlToValidate="txtSupplierMobile" Display="Dynamic"
                                                                                                                            ErrorMessage="*" ValidationGroup="rm"></asp:RequiredFieldValidator>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td class="width55percent">
                                                                                                                    <asp:ImageButton ID="btnSupplierMobile" runat="server" ImageUrl="../images/btn_anotherphone.gif"
                                                                                                                        OnClick="btnSupplierMobile_Click" ValidationGroup="rm" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
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
                                                                                    <td>
                                                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <td class="align_top">
                                                                                                        <asp:Panel ID="pnlFax" runat="server" DefaultButton="btnSupplierfax">
                                                                                                            <table border="0" cellspacing="0" cellpadding="3" width="48%">
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
                                                                                                                                        Faxes
                                                                                                                                        <div class="clear_10">
                                                                                                                                        </div>
                                                                                                                                        <asp:GridView ID="grdFax" AllowSorting="True" AllowPaging="false" runat="server"
                                                                                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                                                                                            EmptyDataText="Sorry! No Fax." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                                                                                            OnRowCommand="grdFax_RowCommand" OnRowDataBound="grdFax_RowDataBound">
                                                                                                                                            <Columns>
                                                                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                                                                                                                                            <tr>
                                                                                                                                                                <td class="align_left" style="width: 26%;">
                                                                                                                                                                    <asp:Label ID="lblFax" runat="server" Text="<%#Bind('sFax') %>"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td class="align_left" style="width: 100px;">
                                                                                                                                                                    <asp:Label ID="lblFaxNote" runat="server" Text="<%#Bind('faxNote') %>"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td class="align_right" style="width: 20px;">
                                                                                                                                                                    <asp:ImageButton ID="imgBtnActiveFaxes" runat="server" CommandName="active" CommandArgument='<%# Bind("pkSupplierFaxID") %>'
                                                                                                                                                                        Width="16" ImageUrl="~/Images/Star Gray.png" ToolTip="" />
                                                                                                                                                                </td>
                                                                                                                                                                <td class="align_right" style="width: 20px; display: none;">
                                                                                                                                                                    <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkSupplierFaxID") %>'
                                                                                                                                                                        Visible="false" ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
                                                                                                                                                                </td>
                                                                                                                                                                <td class="align_right" style="width: 20px;">
                                                                                                                                                                    <asp:ImageButton ID="imgEdit" runat="server" CommandName="edt" CommandArgument='<%# Bind("pkSupplierFaxID") %>'
                                                                                                                                                                        ImageUrl="../images/edit_icon.png" ToolTip="Edit" />
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
                                                                                                                    <td class="width45percent" style="width: 3%;">
                                                                                                                        <div class="textbox204">
                                                                                                                            <asp:TextBox ID="txtSupplierFax" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Style="float: right;
                                                                                                                                position: relative; top: 6px;" ControlToValidate="txtSupplierFax" Display="Dynamic"
                                                                                                                                ErrorMessage="*" ValidationGroup="rf"></asp:RequiredFieldValidator>
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="width45percent" style="width: 3%;">
                                                                                                                        <div class="textbox204">
                                                                                                                            <asp:TextBox ID="txtSupplierFaxNote" runat="server"></asp:TextBox>
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="width55percent">
                                                                                                                        <asp:ImageButton ID="btnSupplierfax" runat="server" ImageUrl="../images/btn_addanother.png"
                                                                                                                            OnClick="btnSupplierfax_Click" ValidationGroup="rf" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="height30">
                                                                                        <img src="../images/horizontal_line.png" alt="" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table border="0" cellspacing="2" cellpadding="2" width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table border="0" cellspacing="2" cellpadding="2" width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <table border="0" cellspacing="4" cellpadding="3" width="100%">
                                                                                                                    <tr>
                                                                                                                        <td class="label_bold" colspan="4">
                                                                                                                            Add Another Address
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="label_bold" style="width: 113px;">
                                                                                                                            Address
                                                                                                                        </td>
                                                                                                                        <td style="width: 327px;">
                                                                                                                            <div class="textbox204">
                                                                                                                                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtAddress" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="ra"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td class="label_bold" style="width: 100px;">
                                                                                                                            Town
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <div class="textbox204">
                                                                                                                                <asp:TextBox ID="txtTown" runat="server"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtTown" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="ra"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="label_bold">
                                                                                                                            Post Code
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <div class="textbox204">
                                                                                                                                <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtPostCode" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="ra"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td class="label_bold">
                                                                                                                            Region
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <div class="textbox204">
                                                                                                                                <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtRegion" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="ra"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="label_bold">
                                                                                                                            Country
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <div class="textbox204">
                                                                                                                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="false">
                                                                                                                                </asp:DropDownList>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td class="label_bold">
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:ImageButton ID="btnAddress" runat="server" ImageUrl="../images/btn_addanotheraddress.gif"
                                                                                                                                OnClick="btnAddress_Click" ValidationGroup="ra" />
                                                                                                                        </td>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td class="height30">
                                                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                        <tr>
                                                                                                            <td>
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
                                                                                                                                            Addresses
                                                                                                                                            <div class="clear_10">
                                                                                                                                            </div>
                                                                                                                                            <asp:GridView ID="grdAddress" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                                                                                AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                                                                RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                                                                                                BorderWidth="0" GridLines="None" OnRowCommand="grdAddress_RowCommand" OnRowDataBound="grdAddress_RowDataBound"
                                                                                                                                                Style="font-weight: normal;" EmptyDataText="Sorry! No Address.">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:BoundField DataField="sAddressStreet" HeaderText="Address" ItemStyle-CssClass="align_left"
                                                                                                                                                        ItemStyle-Width="124px" />
                                                                                                                                                    <asp:BoundField DataField="sAddressTown" HeaderText="Town" ItemStyle-CssClass="align_left"
                                                                                                                                                        ItemStyle-Width="124px" />
                                                                                                                                                    <asp:BoundField DataField="sAddressRegion" HeaderText="Region" ItemStyle-CssClass="align_left"
                                                                                                                                                        ItemStyle-Width="124px" />
                                                                                                                                                    <asp:BoundField DataField="sAddressPostCode" HeaderText="Post Code" ItemStyle-CssClass="align_left"
                                                                                                                                                        ItemStyle-Width="124px" />
                                                                                                                                                    <asp:BoundField DataField="sCountry" HeaderText="Country" ItemStyle-CssClass="align_left"
                                                                                                                                                        ItemStyle-Width="124px" />
                                                                                                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:ImageButton ID="imgBtnActiveAddress" runat="server" CommandName="active" CommandArgument='<%# Bind("pkSupplierAddressID") %>'
                                                                                                                                                                            Width="16" ImageUrl="~/Images/Star Gray.png" ToolTip="" />
                                                                                                                                                                    </td>
                                                                                                                                                                    <td style="display: none;">
                                                                                                                                                                        <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkSupplierAddressID") %>'
                                                                                                                                                                            ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
                                                                                                                                                                    </td>
                                                                                                                                                                    <td>
                                                                                                                                                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="edt" CommandArgument='<%# Bind("pkSupplierAddressID") %>'
                                                                                                                                                                            ImageUrl="../images/edit_icon.png" ToolTip="Edit" />
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
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="height30">
                                                                                        <img src="../images/horizontal_line.png" alt="" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="display: none;">
                                                                                    <td>
                                                                                        <h3 style="text-decoration: underline;">
                                                                                            Company Info
                                                                                        </h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trAddCompany" runat="server" style="display: none;">
                                                                                    <td>
                                                                                        <div style="min-height: 25px;">
                                                                                            <asp:Button ID="btnHideCompany" runat="server" Text="Hide" Style="position: relative;
                                                                                                float: right; top: 0; right: 0; display: none;" OnClick="btnHideCompany_Click" />
                                                                                        </div>
                                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table>
                                                                                                        <tr>
                                                                                                            <td class="width22percent align_center_top">
                                                                                                                <img id="imgCompanyLogo" runat="server" width="149" src="../images/no_image.gif"
                                                                                                                    alt="" /><br />
                                                                                                                <br />
                                                                                                                <asp:FileUpload ID="fpCompanyPic" runat="server" />
                                                                                                            </td>
                                                                                                            <td class="width4percent align_center_top" style="width: 15%">
                                                                                                                <img src="../images/vertical_line.png" alt="" />
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <table>
                                                                                                                    <tr style="display: none;">
                                                                                                                        <td>
                                                                                                                            Suppliers:
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <div class="textbox204" style="float: left;">
                                                                                                                                <asp:DropDownList ID="ddlSuppliers" runat="server">
                                                                                                                                </asp:DropDownList>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="ddlSuppliers" Display="Dynamic"
                                                                                                                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqc"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            Brand Name:
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <div class="textbox204" style="float: left;">
                                                                                                                                <asp:TextBox ID="txtCompanyBrand" runat="server"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtCompanyBrand" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="reqc"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td valign="top">
                                                                                                                            Contact Method:
                                                                                                                        </td>
                                                                                                                        <td>
                                                                                                                            <asp:RadioButton ID="rdCompanyTelephone" runat="server" Text="Phone" GroupName="rdc"
                                                                                                                                Checked="true" /><br />
                                                                                                                            <asp:RadioButton ID="rdCompanyEmail" runat="server" Text="EmailAddress" GroupName="rdc" /><br />
                                                                                                                            <asp:RadioButton ID="rdCompanyFax" runat="server" Text="Fax" GroupName="rdc" /><br />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="2">
                                                                                                                            <div class="clear">
                                                                                                                            </div>
                                                                                                                            <div class="bottom_graybox_right">
                                                                                                                                <asp:ImageButton ID="imgBtnSaveCompany" runat="server" ImageUrl="~/Images/btn_update.png"
                                                                                                                                    ValidationGroup="reqc" OnClick="imgBtnSaveCompany_Click" />
                                                                                                                            </div>
                                                                                                                            <div class="clear_30">
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="height30">
                                                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trcEmail" runat="server" style="display: none;">
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
                                                                                                                                            Email Address
                                                                                                                                            <div class="clear_10">
                                                                                                                                            </div>
                                                                                                                                            <asp:GridView ID="grdEmailCompany" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="header_row"
                                                                                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Width="100%"
                                                                                                                                                EmptyDataText="Sorry! No Email." CellPadding="0" BorderStyle="None" BorderWidth="0"
                                                                                                                                                GridLines="None" Style="font-weight: normal;" OnRowCommand="grdEmailCompany_RowCommand"
                                                                                                                                                OnRowDataBound="grdEmailCompany_RowDataBound">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td class="align_left">
                                                                                                                                                                        <asp:Label ID="lblemail" runat="server" Text="<%#Bind('cEmails') %>"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td class="align_right" style="width: 20px;">
                                                                                                                                                                        <asp:ImageButton ID="imgbtnSetActiveEmail" runat="server" CommandName="active" CommandArgument='<%# Bind("pkCompanyEmailID") %>'
                                                                                                                                                                            ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                                                                                    </td>
                                                                                                                                                                    <td class="align_right" style="width: 20px;">
                                                                                                                                                                        <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkCompanyEmailID") %>'
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
                                                                                                                                <asp:TextBox ID="txtCompanyEmail" runat="server"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtCompanyEmail" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="rec"></asp:RequiredFieldValidator>
                                                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"
                                                                                                                                    ControlToValidate="txtCompanyEmail" EnableClientScript="true" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" Text="Incorrect Email format." Style="position: relative; line-height: 39px;"
                                                                                                                                    ValidationGroup="rec"></asp:RegularExpressionValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td class="width55percent">
                                                                                                                            <asp:ImageButton ID="btnCompanyEmail" runat="server" ImageUrl="../images/btn_addanotheremail.gif"
                                                                                                                                ValidationGroup="rec" OnClick="btnCompanyEmail_Click" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                            <td class="width30">
                                                                                                            </td>
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
                                                                                                                                            Mobile Phone
                                                                                                                                            <div class="clear_10">
                                                                                                                                            </div>
                                                                                                                                            <asp:GridView ID="grdMobileCompany" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                                                                                AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                                                                RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                                                                                                EmptyDataText="Sorry! No Phone." BorderWidth="0" GridLines="None" Style="font-weight: normal;"
                                                                                                                                                OnRowCommand="grdMobileCompany_RowCommand" OnRowDataBound="grdMobileCompany_RowDataBound">
                                                                                                                                                <Columns>
                                                                                                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                        <ItemTemplate>
                                                                                                                                                            <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                                                                <tr>
                                                                                                                                                                    <td class="align_left">
                                                                                                                                                                        <asp:Label ID="lblMobile" runat="server" Text="<%#Bind('cPhones') %>"></asp:Label>
                                                                                                                                                                    </td>
                                                                                                                                                                    <td class="align_right" style="width: 20px;">
                                                                                                                                                                        <asp:ImageButton ID="imgBtnActiveMobile" runat="server" CommandName="active" CommandArgument='<%# Bind("pkCompanyPhoneID") %>'
                                                                                                                                                                            ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                                                                                    </td>
                                                                                                                                                                    <td class="align_right" style="width: 20px;">
                                                                                                                                                                        <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkCompanyPhoneID") %>'
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
                                                                                                                                <asp:TextBox ID="txtCompanyMobile" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Style="float: right;
                                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtCompanyMobile" Display="Dynamic"
                                                                                                                                    ErrorMessage="*" ValidationGroup="rmc"></asp:RequiredFieldValidator>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td class="width55percent">
                                                                                                                            <asp:ImageButton ID="btnCompanyMobile" runat="server" ImageUrl="../images/btn_anotherphone.gif"
                                                                                                                                ValidationGroup="rmc" OnClick="btnCompanyMobile_Click" />
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
                                                                                            <tr id="trcEmailSplit" runat="server" style="display: none;">
                                                                                                <td class="height30">
                                                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trcFax" runat="server" style="display: none;">
                                                                                                <td>
                                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <td class="align_top">
                                                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="48%">
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
                                                                                                                                                Faxes
                                                                                                                                                <div class="clear_10">
                                                                                                                                                </div>
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
                                                                                                                            <td class="width45percent" style="width: 3%;">
                                                                                                                                <div class="textbox204">
                                                                                                                                    <asp:TextBox ID="txtCompanyFax" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Style="float: right;
                                                                                                                                        position: relative; top: 6px;" ControlToValidate="txtCompanyFax" Display="Dynamic"
                                                                                                                                        ErrorMessage="*" ValidationGroup="rfc"></asp:RequiredFieldValidator>
                                                                                                                                </div>
                                                                                                                            </td>
                                                                                                                            <td class="width55percent">
                                                                                                                                <asp:ImageButton ID="btnCompanyFax" runat="server" ImageUrl="../images/btn_addanother.png"
                                                                                                                                    ValidationGroup="rfc" OnClick="btnCompanyFax_Click" />
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </td>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trcFaxSplit" runat="server" style="display: none;">
                                                                                                <td class="height30">
                                                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trcAddress" runat="server" style="display: none;">
                                                                                                <td>
                                                                                                    <table border="0" cellspacing="2" cellpadding="2" width="100%">
                                                                                                        <tr>
                                                                                                            <td>
                                                                                                                <table border="0" cellspacing="2" cellpadding="2" width="100%">
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <table border="0" cellspacing="4" cellpadding="3" width="100%">
                                                                                                                                <tr>
                                                                                                                                    <td class="label_bold" colspan="4">
                                                                                                                                        Add Another Address
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="label_bold" style="width: 113px;">
                                                                                                                                        Address
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 327px;">
                                                                                                                                        <div class="textbox204">
                                                                                                                                            <asp:TextBox ID="txtAddressCompany" runat="server"></asp:TextBox>
                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Style="float: right;
                                                                                                                                                position: relative; top: 6px;" ControlToValidate="txtAddressCompany" Display="Dynamic"
                                                                                                                                                ErrorMessage="*" ValidationGroup="rac"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                    <td class="label_bold" style="width: 100px;">
                                                                                                                                        Town
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <div class="textbox204">
                                                                                                                                            <asp:TextBox ID="txtTownCompany" runat="server"></asp:TextBox>
                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Style="float: right;
                                                                                                                                                position: relative; top: 6px;" ControlToValidate="txtTownCompany" Display="Dynamic"
                                                                                                                                                ErrorMessage="*" ValidationGroup="rac"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="label_bold">
                                                                                                                                        Post Code
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <div class="textbox204">
                                                                                                                                            <asp:TextBox ID="txtPostCodeCompany" runat="server"></asp:TextBox>
                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Style="float: right;
                                                                                                                                                position: relative; top: 6px;" ControlToValidate="txtPostCodeCompany" Display="Dynamic"
                                                                                                                                                ErrorMessage="*" ValidationGroup="rac"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                    <td class="label_bold">
                                                                                                                                        Region
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <div class="textbox204">
                                                                                                                                            <asp:TextBox ID="txtRegionCompany" runat="server"></asp:TextBox>
                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Style="float: right;
                                                                                                                                                position: relative; top: 6px;" ControlToValidate="txtRegionCompany" Display="Dynamic"
                                                                                                                                                ErrorMessage="*" ValidationGroup="rac"></asp:RequiredFieldValidator>
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td class="label_bold">
                                                                                                                                        Country
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <div class="textbox204">
                                                                                                                                            <asp:DropDownList ID="ddlCountryCompany" runat="server" AutoPostBack="false">
                                                                                                                                            </asp:DropDownList>
                                                                                                                                        </div>
                                                                                                                                    </td>
                                                                                                                                    <td class="label_bold">
                                                                                                                                    </td>
                                                                                                                                    <td>
                                                                                                                                        <asp:ImageButton ID="btnAddressCompany" runat="server" ImageUrl="../images/btn_addanotheraddress.gif"
                                                                                                                                            ValidationGroup="rac" OnClick="btnAddressCompany_Click" />
                                                                                                                                    </td>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="height30">
                                                                                                                            <img src="../images/horizontal_line.png" alt="" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
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
                                                                                                                                                        Department
                                                                                                                                                        <div class="clear_10">
                                                                                                                                                        </div>
                                                                                                                                                        <asp:GridView ID="grdAddressCompany" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                                                                                            AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                                                                            RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                                                                                                            BorderWidth="0" GridLines="None" Style="font-weight: normal;" EmptyDataText="Sorry! No Address."
                                                                                                                                                            OnRowCommand="grdAddressCompany_RowCommand" OnRowDataBound="grdAddressCompany_RowDataBound">
                                                                                                                                                            <Columns>
                                                                                                                                                                <asp:BoundField DataField="sAddressStreet" HeaderText="Address" ItemStyle-CssClass="align_left"
                                                                                                                                                                    ItemStyle-Width="124px" />
                                                                                                                                                                <asp:BoundField DataField="sAddressTown" HeaderText="Town" ItemStyle-CssClass="align_left"
                                                                                                                                                                    ItemStyle-Width="124px" />
                                                                                                                                                                <asp:BoundField DataField="sAddressRegion" HeaderText="Region" ItemStyle-CssClass="align_left"
                                                                                                                                                                    ItemStyle-Width="124px" />
                                                                                                                                                                <asp:BoundField DataField="sAddressPostCode" HeaderText="Post Code" ItemStyle-CssClass="align_left"
                                                                                                                                                                    ItemStyle-Width="124px" />
                                                                                                                                                                <asp:BoundField DataField="sCountry" HeaderText="Country" ItemStyle-CssClass="align_left"
                                                                                                                                                                    ItemStyle-Width="124px" />
                                                                                                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                                                                                    <ItemTemplate>
                                                                                                                                                                        <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                                                                            <tr>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <asp:ImageButton ID="imgBtnActiveAddress" runat="server" CommandName="active" CommandArgument='<%# Bind("pkCompanyAddressID") %>'
                                                                                                                                                                                        ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                                                                                                </td>
                                                                                                                                                                                <td>
                                                                                                                                                                                    <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkCompanyAddressID") %>'
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
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="height30">
                                                                                                                            <img src="../images/horizontal_line.png" alt="" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <%-- <asp:UpdatePanel ID="upnlContactPeople" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>--%>
                                                                            <asp:Panel ID="pnlContact" runat="server" DefaultButton="imgBtnAddContactPeople">
                                                                                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                                                                    <tr style="height: 50px; vertical-align: top;">
                                                                                        <td colspan="4">
                                                                                            Company Contacts
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 50px; vertical-align: top;">
                                                                                        <td>
                                                                                            Title
                                                                                        </td>
                                                                                        <td style="width: 241px;">
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactTitle" runat="server"></asp:TextBox>
                                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" Style="float: right;
                                                                                                    position: relative; top: 6px;" ControlToValidate="txtContactTitle" Display="Dynamic"
                                                                                                    ErrorMessage="*" ValidationGroup="reqcc"></asp:RequiredFieldValidator>--%>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td colspan="2" align="left">
                                                                                            <asp:ImageButton ID="imgBtnAddContactPeople" runat="server" ImageUrl="~/Images/btn_addcontact.png"
                                                                                                OnClick="imgBtnAddContactPeople_Click" ValidationGroup="reqcc" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Name:
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Style="float: right;
                                                                                                    position: relative; top: 6px;" ControlToValidate="txtContactName" Display="Dynamic"
                                                                                                    ErrorMessage="*" ValidationGroup="reqcc"></asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td style="width: 74px;">
                                                                                            Phone 1:
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactPhone1" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Style="float: right;
                                                                                                    position: relative; top: 6px;" ControlToValidate="txtContactPhone1" Display="Dynamic"
                                                                                                    ErrorMessage="*" ValidationGroup="reqcc"></asp:RequiredFieldValidator>--%>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Email 1:
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactEmail1" runat="server"></asp:TextBox>
                                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Style="float: right;
                                                                                                    position: relative; top: 6px;" ControlToValidate="txtContactEmail1" Display="Dynamic"
                                                                                                    ErrorMessage="*" ValidationGroup="reqcc"></asp:RequiredFieldValidator>--%>
                                                                                            </div>
                                                                                            <br />
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"
                                                                                                ControlToValidate="txtContactEmail1" EnableClientScript="true" Display="Dynamic"
                                                                                                ErrorMessage="*" Text="Incorrect Email format." ValidationGroup="reqcc"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                        <td>
                                                                                            Phone 2:
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactPhone2" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Email 2:
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactEmail2" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                            <br />
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ValidationExpression="^[a-z0-9_\+-]+(\.[a-z0-9_\+-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*\.([a-z]{2,4})$"
                                                                                                ControlToValidate="txtContactEmail2" EnableClientScript="true" Display="Dynamic"
                                                                                                ErrorMessage="*" Text="Incorrect Email format." ValidationGroup="reqcc"></asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                        <td>
                                                                                            Fax:
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox204" style="float: left;">
                                                                                                <asp:TextBox ID="txtContactFax" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Style="float: right;
                                                                                                    position: relative; top: 6px;" ControlToValidate="txtContactFax" Display="Dynamic"
                                                                                                    ErrorMessage="*" ValidationGroup="reqcc"></asp:RequiredFieldValidator>--%>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="vertical-align: top;">
                                                                                            Note:
                                                                                        </td>
                                                                                        <td colspan="3">
                                                                                            <div class="textboxmulti" style="float: left; background: url('../images/textbox_522.png') repeat scroll 0 0 transparent;
                                                                                                width: 522px;">
                                                                                                <asp:TextBox ID="txtContactNote" runat="server" TextMode="MultiLine" Style="min-width: 506px;"> </asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trAddContactPeople" runat="server" visible="false">
                                                                                        <td>
                                                                                            <div style="min-height: 25px;">
                                                                                                <asp:Button ID="btnHideContact" runat="server" Text="Hide" Style="position: relative;
                                                                                                    float: right; top: 0; right: 0; display: block;" OnClick="btnHideContact_Click"
                                                                                                    Visible="false" />
                                                                                            </div>
                                                                                            <table cellpadding="3" cellspacing="3" border="0">
                                                                                                <tr style="display: none;">
                                                                                                    <td>
                                                                                                        Reference:
                                                                                                    </td>
                                                                                                    <td colspan="2">
                                                                                                        <div class="textbox204" style="float: left;">
                                                                                                            <asp:DropDownList ID="ddlContactSupplier" runat="server">
                                                                                                            </asp:DropDownList>
                                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Style="float: right;
                                        position: relative; top: 6px;" ControlToValidate="ddlSuppliers" Display="Dynamic"
                                        InitialValue="0" ErrorMessage="*" ValidationGroup="reqc"></asp:RequiredFieldValidator>--%>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                </tr>
                                                                                                <tr style="display: none;">
                                                                                                    <td>
                                                                                                        Contact Address:
                                                                                                    </td>
                                                                                                    <td colspan="2">
                                                                                                        <div class="textbox204" style="float: left;">
                                                                                                            <asp:TextBox ID="txtContactAddress" runat="server"></asp:TextBox>
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Style="float: right;
                                                                                                                position: relative; top: 6px;" ControlToValidate="txtContactAddress" Display="Dynamic"
                                                                                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="2" align="right">
                                                                                                        <asp:ImageButton ID="imgBtnSaveContact" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                                            ValidationGroup="reqcc" OnClick="imgBtnSaveContact_Click" />
                                                                                                        <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                                                                            ValidationGroup="rmmmm" OnClick="imgBtnCancel_Click1" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                            <table id="tblGrdContactPeople" runat="server">
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="Div2" runat="server">
                                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="width: 735px;">
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
                                                                                                            Contact People
                                                                                                            <div class="clear_10">
                                                                                                            </div>
                                                                                                            <asp:GridView ID="grdContactPeople" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                                AllowPaging="true" PageSize="5" EmptyDataText="No any Contact People Exists."
                                                                                                                GridLines="None" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                                RowStyle-CssClass="rowstyle" OnRowCommand="grdContactPeople_RowCommand" OnPageIndexChanging="grdContactPeople_PageIndexChanging"
                                                                                                                OnRowDataBound="grdContactPeople_RowDataBound">
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="Title" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblTitle" runat="server" Text="<%#Bind('Title') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="lnkContactPeople" runat="server" CommandName="name" CommandArgument="<%#Bind('pkContactPeopleID') %>"
                                                                                                                                Visible="false" Text="<%#Bind('pName') %>"></asp:LinkButton>
                                                                                                                            <asp:Label ID="lblName" runat="server" Text="<%#Bind('pName') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Email 1" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblEmail1" runat="server" Text="<%#Bind('pEmail1') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Email 2" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblEmail2" runat="server" Text="<%#Bind('pEmail2') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Phone 1" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblPhone1" runat="server" Text="<%#Bind('Phone1') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Phone 2" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblPhone2" runat="server" Text="<%#Bind('Phone2') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Fax" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblFax" runat="server" Text="<%#Bind('fax') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Notes" ItemStyle-Width="155">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="lblNote" runat="server" Text="<%#Bind('ContactNote') %>"></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:ImageButton ID="imgBtnActiveAddress" runat="server" CommandName="active" CommandArgument='<%# Bind("pkContactPeopleID") %>'
                                                                                                                                Width="16" ImageUrl="~/Images/Star Gray.png" ToolTip="" />
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField>
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:ImageButton ID="imgEdit" runat="server" CommandName="edt" CommandArgument='<%# Bind("pkContactPeopleID") %>'
                                                                                                                                ImageUrl="../images/edit_icon.png" ToolTip="Edit" />
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
                                                                            </table>
                                                                            <asp:MultiView ID="mvMainContact" runat="server" ActiveViewIndex="0" Visible="false">
                                                                                <asp:View ID="vAddContact" runat="server">
                                                                                    <table style="display: none;">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table border="0" cellpadding="0" cellspacing="0" class="width100percent">
                                                                                                    <tr>
                                                                                                        <td class="width22percent align_center_top">
                                                                                                            <img id="imgAddContact" runat="server" src="../Images/no_image.gif" alt="" /><br />
                                                                                                            <br />
                                                                                                            <asp:FileUpload ID="FileUpload1" runat="server" EnableViewState="true" Style="width: 151px;" />
                                                                                                        </td>
                                                                                                        <td class="width4percent align_center_top" style="width: 18%;">
                                                                                                            <img src="../images/vertical_line.png" alt="" />
                                                                                                        </td>
                                                                                                        <td class="width44percent align_top">
                                                                                                            <table border="0" align="center" cellpadding="4" cellspacing="4">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        Name:
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <div class="textbox204">
                                                                                                                            <asp:TextBox ID="txtAddContactName" runat="server"></asp:TextBox>
                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" Style="float: right;
                                                                                                                                position: relative; top: 6px;" ControlToValidate="txtAddContactName" Display="Dynamic"
                                                                                                                                ErrorMessage="*" ValidationGroup="reqadd"></asp:RequiredFieldValidator>
                                                                                                                        </div>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                                <tr>
                                                                                                                    <td class="align_left">
                                                                                                                        Contact Method:
                                                                                                                    </td>
                                                                                                                    <td class="align_center">
                                                                                                                        <asp:RadioButton ID="rdAddContactEmail" runat="server" Text="Email" GroupName="add"
                                                                                                                            Checked="true" />
                                                                                                                        <asp:RadioButton ID="rdAddContactPhone" runat="server" Text="Phone" GroupName="add" />
                                                                                                                        <asp:RadioButton ID="rdAddContactFAx" runat="server" Text="Fax" GroupName="add" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="height30">
                                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                                                                                    <tr>
                                                                                                        <td class="label_bold">
                                                                                                            Email
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:TextBox ID="txtAddContactEmail" runat="server"></asp:TextBox>
                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Style="float: right;
                                                                                                                    position: relative; top: 6px;" ControlToValidate="txtAddContactEmail" Display="Dynamic"
                                                                                                                    ErrorMessage="*" ValidationGroup="reqadd"></asp:RequiredFieldValidator>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td class="label_bold">
                                                                                                            <span class="width15percent label_bold">Fax</span>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                <asp:TextBox ID="txtAddFaxContact" runat="server" CssClass="filterNum"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="label_bold">
                                                                                                            Mobile Phone
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:TextBox ID="txtAddMobileContact" runat="server" CssClass="filterNum"></asp:TextBox></div>
                                                                                                        </td>
                                                                                                        <td colspan="2">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="height30">
                                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                                                                                    <tr>
                                                                                                        <td class="label_bold" width="26%">
                                                                                                            Address
                                                                                                        </td>
                                                                                                        <td width="72%">
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:TextBox ID="txtAddAddressContact" runat="server"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                        <td class="label_bold">
                                                                                                            Town
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:TextBox ID="txtAddTownContact" runat="server"></asp:TextBox></div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="label_bold">
                                                                                                            Post Code
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:TextBox ID="txtAddPostCodeContact" runat="server"></asp:TextBox></div>
                                                                                                        </td>
                                                                                                        <td class="label_bold">
                                                                                                            Region
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:TextBox ID="txtAddRegionContact" runat="server"></asp:TextBox></div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td class="label_bold">
                                                                                                            Country
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <div class="textbox204">
                                                                                                                &nbsp;<asp:DropDownList ID="ddlContactCountry" runat="server" AutoPostBack="false">
                                                                                                                </asp:DropDownList>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td align="right">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:View>
                                                                                <asp:View ID="vEditContact" runat="server">
                                                                                </asp:View>
                                                                                <asp:View ID="vGrdContact" runat="server">
                                                                                    <table>
                                                                                    </table>
                                                                                </asp:View>
                                                                            </asp:MultiView>
                                                                            <table>
                                                                            </table>
                                                                            <%--</ContentTemplate>
                                                                            </asp:UpdatePanel>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:View>
                                                            <asp:View ID="vGrdSuppliers" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <div id="scrollingDiv" runat="server">
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
                                                                                                Suppliers
                                                                                                <div class="clear_10">
                                                                                                </div>
                                                                                                <asp:GridView ID="grdSuppliers" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                    AllowPaging="true" PageSize="20" EmptyDataText="No any Supplier Exists." GridLines="None"
                                                                                                    HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                    RowStyle-CssClass="rowstyle" OnRowCommand="grdSuppliers_RowCommand" OnRowDataBound="grdSuppliers_RowDataBound"
                                                                                                    OnPageIndexChanging="grdSuppliers_PageIndexChanging" OnSorting="grdSuppliers_Sorting">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField ItemStyle-Width="150">
                                                                                                            <HeaderTemplate>
                                                                                                                <asp:LinkButton ID="lnkSupplierHeader" runat="server" CommandName="sort" Text="Company Name"
                                                                                                                    CommandArgument="<%#Bind('pkSupplierID') %>"></asp:LinkButton>
                                                                                                            </HeaderTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="lnkSupplier" runat="server" CommandName="name" CommandArgument="<%#Bind('pkSupplierID') %>"
                                                                                                                    Text="<%#Bind('sBrandName') %>"></asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Email" ItemStyle-Width="150">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="lnkEmail" runat="server" CommandName="email" CommandArgument="<%#Bind('pkSupplierID') %>"
                                                                                                                    Text="<%#Bind('sEmail') %>"></asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Fax" ItemStyle-Width="125">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="lnkFax" runat="server" CommandName="fax" CommandArgument="<%#Bind('pkSupplierID') %>"
                                                                                                                    Text="<%#Bind('sFax') %>"></asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:BoundField DataField="phone" HeaderText="phone" ItemStyle-Width="125" />
                                                                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="150">
                                                                                                            <ItemTemplate>
                                                                                                                <div id="divAmount" runat="server" class="textbox115">
                                                                                                                    <asp:LinkButton ID="lnkAmount" runat="server" CommandName="amount" CommandArgument="<%#Bind('pkSupplierID') %>"
                                                                                                                        Text="<%#Bind('amount') %>"></asp:LinkButton>
                                                                                                                </div>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Active">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:ImageButton ID="imgBtnActive" CommandName="active" CommandArgument="<%#Bind('pkSupplierID') %>"
                                                                                                                    runat="server" ImageUrl="../Images/activate_icon.gif" />
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <asp:GridView ID="grdFaxCompany" runat="server" AllowPaging="True" AllowSorting="True"
                                                                                                    AlternatingRowStyle-CssClass="alternate_row" AutoGenerateColumns="False" BorderStyle="None"
                                                                                                    BorderWidth="0" CellPadding="0" EmptyDataText="Sorry! No Fax." GridLines="None"
                                                                                                    HeaderStyle-CssClass="header_row" OnRowCommand="grdFaxCompany_RowCommand" OnRowDataBound="grdFaxCompany_RowDataBound"
                                                                                                    RowStyle-CssClass="rowstyle" Style="font-weight: normal;" Width="100%">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                            <ItemTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                                                    <tr>
                                                                                                                        <td class="align_left">
                                                                                                                            <asp:Label ID="lblFax" runat="server" Text="<%#Bind('cFax') %>"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td class="align_right" style="width: 20px;">
                                                                                                                            <asp:ImageButton ID="imgBtnActiveFaxes" runat="server" CommandArgument='<%# Bind("pkCompanyFaxID") %>'
                                                                                                                                CommandName="active" ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                                        </td>
                                                                                                                        <td class="align_right" style="width: 20px;">
                                                                                                                            <asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Bind("pkCompanyFaxID") %>'
                                                                                                                                CommandName="Del" ImageUrl="../images/close.png" OnClientClick="javascript: return confirm('Are you sure your want to delete?');"
                                                                                                                                ToolTip="Delete" />
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
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:View>
                                                            <asp:View ID="vAddSupplierProduct" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            Suppliers:
                                                                        </td>
                                                                        <td>
                                                                            <div class="textbox204">
                                                                                <asp:DropDownList ID="ddlSuppliersForProduct" runat="server" AutoPostBack="false">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Style="float: right;
                                                                                    position: relative; top: 6px;" ControlToValidate="ddlSuppliersForProduct" Display="Dynamic"
                                                                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Base Categories:
                                                                        </td>
                                                                        <td>
                                                                            <%--<div class="textbox204">
                                                                                <asp:DropDownList ID="ddlBaseCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBaseCategories_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Style="float: right;
                                                                                    position: relative; top: 6px;" ControlToValidate="ddlBaseCategories" Display="Dynamic"
                                                                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                                                                            </div>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Sub Categories:
                                                                        </td>
                                                                        <td>
                                                                            <%--<div class="textbox204">
                                                                                <asp:DropDownList ID="ddlSubCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategories_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Style="float: right;
                                                                                    position: relative; top: 6px;" ControlToValidate="ddlSubCategories" Display="Dynamic"
                                                                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                                                                            </div>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Products:
                                                                        </td>
                                                                        <td>
                                                                            <div class="textbox204">
                                                                                <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Style="float: right;
                                                                                    position: relative; top: 6px;" ControlToValidate="ddlProducts" Display="Dynamic"
                                                                                    InitialValue="0" ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Price:
                                                                        </td>
                                                                        <td>
                                                                            <div class="textbox204">
                                                                                <asp:TextBox ID="txtProductPrice" runat="server" CssClass="filter"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" Style="float: right;
                                                                                    position: relative; top: 6px;" ControlToValidate="txtProductPrice" Display="Dynamic"
                                                                                    ErrorMessage="*" ValidationGroup="reqPro"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Price:
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgBtnSaveSupplierProducts" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                ValidationGroup="reqPro" OnClick="imgBtnSaveSupplierProducts_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:View>
                                                        </asp:MultiView>
                                                    </div>
                                                    <div id="divTab2" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                        width: auto; padding: 10px; padding-top: 10px; display: none;">
                                                        <asp:UpdatePanel ID="upnlSupplierOrders" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div>
                                                                    suppliers:
                                                                    <div class="textbox204">
                                                                        <asp:DropDownList ID="ddlSupplierOrders" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplierOrders_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                </div>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td rowspan="6">
                                                                            <img id="imgCompanyLogoO" runat="server" src="../Images/no_image.gif" style="width: 149px;" />
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompanyBrandOrder" runat="server" Text="" Style="font-size: 30px;
                                                                                font-weight: bold; font-family: Arial;"></asp:Label>
                                                                            <%--Company Brand Name--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompanyAddressOrder" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <%--Address, street 34, Region, Postcode, Country--%>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompayPhoneEmailOrder" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <%--Fav. Phone:123456789 - Fav. Email: someone@email.com - Fav. FAX: 123456789 --%>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompanyContactOrder" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <%--Contact Person Name: Someone - Contact Person Phone and email: blahblahblah--%>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                </div>
                                                                <div>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left">
                                                                                Please Select Dates:
                                                                            </td>
                                                                            <td align="left">
                                                                                From:
                                                                            </td>
                                                                            <td align="left">
                                                                                <div class="textbox115" style="float: left;">
                                                                                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtFromDate"
                                                                                    ErrorMessage="*" ValidationGroup="rmm" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td align="right">
                                                                                Till:
                                                                            </td>
                                                                            <td align="left">
                                                                                <div class="textbox115" style="float: left;">
                                                                                    <asp:TextBox ID="txtTillDate" runat="server"></asp:TextBox>
                                                                                </div>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtTillDate"
                                                                                    ErrorMessage="*" ValidationGroup="rmm" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="imgBtnFilterOrder" runat="server" ImageUrl="../Images/btn_filter.png"
                                                                                    ValidationGroup="rmm" OnClick="imgBtnFilterOrder_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <br />
                                                                <div style="float: right; margin-right: 25px;">
                                                                    <asp:ImageButton ID="imgBtnPdf" runat="server" Width="32" ImageUrl="../Images/pdf.png"
                                                                        OnClick="imgBtnPdf_Click" />
                                                                    <img id="imgPdf" src="../Images/pdf.png" width="32" style="display: none;" />
                                                                    <asp:ImageButton ID="imgBtnPrint" runat="server" Width="32" ImageUrl="../Images/print.png"
                                                                        OnClick="imgBtnPrint_Click" />
                                                                    <img id="imgPrint" src="../Images/print.png" width="32" style="display: none;" />
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div id="divTotals" runat="server" style="margin-left: 102px;">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                Invoice Sum
                                                                            </td>
                                                                            <td>
                                                                                No Invoice Sum
                                                                            </td>
                                                                            <td>
                                                                                Total Orders
                                                                            </td>
                                                                            <td>
                                                                                Paid
                                                                            </td>
                                                                            <td>
                                                                                Due
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                TOTALS:
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox115">
                                                                                    <asp:Label ID="lblInvoiceSum" runat="server"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox115">
                                                                                    <asp:Label ID="lblNoInvoiceSum" runat="server"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox115" style="background: url(../images/textbox_pink.png) no-repeat;">
                                                                                    <asp:Label ID="lblTotalOrders" runat="server" Style="color: Black;"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox115" style="background: url(../images/textbox_green.png) no-repeat;
                                                                                    color: White;">
                                                                                    <asp:Label ID="lblPaid" runat="server" Style="color: White;"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox115" style="background: url(../images/textbox_red.png) no-repeat;
                                                                                    color: White;">
                                                                                    <asp:Label ID="lblDue" runat="server" Style="color: White;"></asp:Label>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div id="divDetail" runat="server">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:GridView ID="grdOrderDetail" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                                                    PageSize="15" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                    EmptyDataText="No Order Found!" AlternatingRowStyle-CssClass="alternate_row"
                                                                                    RowStyle-CssClass="rowstyle" OnRowDataBound="grdOrderDetail_RowDataBound" OnPageIndexChanging="grdOrderDetail_PageIndexChanging"
                                                                                    OnRowCommand="grdOrderDetail_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDate" runat="server" Visible="false"></asp:Label>
                                                                                                <asp:LinkButton ID="lnkDate" runat="server" CommandName="order" CommandArgument="<%#Bind('pkBaseOrderID') %>"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Order ID" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <%--<asp:Label ID="lblOrderNumber" runat="server" Text="<%#Bind('SessionOrderID') %>"></asp:Label>--%>
                                                                                                <asp:LinkButton ID="lnkOrder" runat="server" Text="<%#Bind('SessionOrderID') %>"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 68px;">
                                                                                                            Invoice #.
                                                                                                        </td>
                                                                                                        <td style="width: 70px;">
                                                                                                            Inv. Sum
                                                                                                        </td>
                                                                                                        <td style="width: 90px;">
                                                                                                            Non Inv. Sum
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:GridView ID="grdInvoices" runat="server" GridLines="None" ShowHeader="false"
                                                                                                    AutoGenerateColumns="false" OnRowDataBound="grdInvoices_RowDataBound">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField ItemStyle-Width="112">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblInvoiceNum" runat="server" Text="<%#Bind('InvoiceNumber') %>"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField ItemStyle-Width="112">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblInvSum" runat="server" Text="<%#Bind('InvoiceAmount') %>"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField>
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblNonInvSum" runat="server" Text="<%#Bind('InvoiceAmount') %>"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                                <table id="tblInvoices" runat="server" visible="false" border="0" cellpadding="0"
                                                                                                    cellspacing="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 90px;">
                                                                                                            -
                                                                                                        </td>
                                                                                                        <td style="width: 90px;">
                                                                                                            -
                                                                                                        </td>
                                                                                                        <td style="width: 90px;">
                                                                                                            -
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Sub Total Sum">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSubTotal" runat="server" Text="<%#Bind('subtotal') %>"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPaidAmount" runat="server" Text="<%#Bind('paid') %>"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Due Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDueAmount" runat="server" Text="<%#Bind('due') %>"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div id="divTab3" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                        width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                        <asp:UpdatePanel ID="upnlSupplierProducts" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div>
                                                                    suppliers:
                                                                    <div class="textbox204">
                                                                        <asp:DropDownList ID="ddlEditSupplierProducts" runat="server" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlEditSupplierProducts_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                </div>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td rowspan="6">
                                                                            <img id="imgCompanyLogoP" runat="server" src="../Images/no_image.gif" style="width: 149px;" />
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompanyBrand" runat="server" Text="" Style="font-size: 30px; font-weight: bold;
                                                                                font-family: Arial;"></asp:Label>
                                                                            <%--Company Brand Name--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblCompanyAddress" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <%--Address, street 34, Region, Postcode, Country--%>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblPhoneEmailFax" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <%--Fav. Phone:123456789 - Fav. Email: someone@email.com - Fav. FAX: 123456789 --%>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Label ID="lblContact" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <%--Contact Person Name: Someone - Contact Person Phone and email: blahblahblah--%>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                </div>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr id="trFilter" runat="server" visible="false">
                                                                        <td>
                                                                            Filter Products :
                                                                        </td>
                                                                        <td style="width: 250px;">
                                                                            <div class="textbox204">
                                                                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*"
                                                                                    ControlToValidate="txtSearch" Display="Dynamic" ValidationGroup="preq"></asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </td>
                                                                        <td style="width: 80px;">
                                                                            <asp:ImageButton ID="imgBtnSearchProducts" runat="server" ImageUrl="../Images/btn_filter.png"
                                                                                OnClick="imgBtnSearchProducts_Click" ValidationGroup="preq" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgBtnClearFilter" runat="server" ImageUrl="../Images/btn_clearfilter.png"
                                                                                OnClick="imgBtnClearFilter_Click" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="height: 30px;">
                                                                            Products Linked To Supplier:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:GridView ID="grdProductPrices" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                                                PageSize="15" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdProductPrices_RowDataBound"
                                                                                OnPageIndexChanging="grdProductPrices_PageIndexChanging" OnRowCommand="grdProductPrices_RowCommand">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Product Name">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProductName" runat="server" Text="<%#Bind('sProductName') %>" Visible="false"></asp:Label>
                                                                                            <asp:LinkButton ID="lnkProductName" runat="server" Text="<%#Bind('sProductName') %>"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Packaging">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPacking" runat="server" Text="<%#Bind('pName') %>"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblQuantity" runat="server" Text="<%#Bind('qName') %>"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Price">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkOldPrice" runat="server" CommandName="graph" Text=""></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="New Price">
                                                                                        <ItemTemplate>
                                                                                            <div class="textbox_small">
                                                                                                <asp:TextBox ID="txtNewPrice" runat="server" Text="" CssClass="filter"></asp:TextBox>
                                                                                                <asp:HiddenField ID="hidid" runat="server" />
                                                                                                <asp:HiddenField ID="hidSupplierid" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                <asp:HiddenField ID="hidrelid" runat="server" Value="<%#Bind('pkProductPackingQuantityRelID') %>" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Date">
                                                                                        <ItemTemplate>
                                                                                            <div id="divDate" runat="server" class="textbox115">
                                                                                                <%--<asp:DropDownList ID="ddlDate" runat="server">
                                                                                                </asp:DropDownList>--%>
                                                                                                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imgBtnEdit" CommandName="edt" runat="server" ImageUrl="../Images/btn_edit.png" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Difference">
                                                                                        <ItemTemplate>
                                                                                            <div style="float: left;">
                                                                                                <asp:Label ID="lblDifferenceValue" runat="server" Style="float: left; font-weight: bold;
                                                                                                    width: 50px;"></asp:Label>
                                                                                                <img id="imgAero" runat="server" style="float: right;" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                    <img src="../images/horizontal_line.png" alt="" />
                                                                </div>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:ImageButton ID="imgBtnSaveAll" runat="server" ImageUrl="../Images/btn_saveall.png"
                                                                                Visible="false" OnClick="imgBtnSaveAll_Click" Style="margin-right: 55px;" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:Label ID="lblSupplierProductMessage" runat="server" Text="There is no any supplier product!"
                                                                    Visible="false"></asp:Label>
                                                                <asp:MultiView ID="mvTab3" runat="server" ActiveViewIndex="1" Visible="false">
                                                                    <asp:View ID="vEditSupplierProduct" runat="server">
                                                                        <asp:Panel ID="pnlPrice" runat="server" DefaultButton="imgBtnSaveEditPrice">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        price:
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox204">
                                                                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="filter"> </asp:TextBox>
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" Style="float: right;
                                                                                                position: relative; top: 6px;" ControlToValidate="txtPrice" Display="Dynamic"
                                                                                                ErrorMessage="*" ValidationGroup="reqpp"></asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="imgBtnSaveEditPrice" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                            ValidationGroup="reqpp" OnClick="imgBtnSaveEditPrice_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </asp:View>
                                                                    <asp:View ID="vSupplierProducts" runat="server">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <div id="Div3" runat="server">
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
                                                                                                        <div class="clear_10">
                                                                                                        </div>
                                                                                                        <asp:GridView ID="grdSupplierProducts" runat="server" AutoGenerateColumns="false"
                                                                                                            Width="100%" AllowPaging="true" PageSize="5" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                            Style="width: 100%" AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle"
                                                                                                            OnRowCommand="grdSupplierProducts_RowCommand">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="Product Name">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:LinkButton ID="lnkProductName" runat="server" CommandName="name" CommandArgument="<%#Bind('pkSupplierProductID') %>"
                                                                                                                            Text="<%#Bind('sProductName') %>"></asp:LinkButton>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:BoundField DataField="UnitPrice" HeaderText="Price" ItemStyle-Width="40" />
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblCurrencySign" runat="server" Text="€"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField>
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkSupplierProductID") %>'
                                                                                                                            ImageUrl="../images/close.png" ToolTip="Delete" OnClientClick="javascript: return confirm('Are you sure your want to delete?');" />
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
                                                                        </table>
                                                                    </asp:View>
                                                                </asp:MultiView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div id="divTab4" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                        width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                        <asp:UpdatePanel ID="upnlProductLinking" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td style="width: 200px; vertical-align: top; text-align: left;">
                                                                                <%-- <asp:Label ID="lblVertialBaseName" runat="server" Text=" yrogetaC esaB" Style="writing-mode: tb-rl;
                                                                                    white-space: nowrap; x='-50'; y='10'; font-family='tahoma'; font-size='12'; transform='rotate(-90)';
                                                                                    text-rendering='optimizespeed'; fill='#888';" Visible="false"></asp:Label>--%>
                                                                                Base Category
                                                                            </td>
                                                                            <td valign="top">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px;" valign="top">
                                                                                <div class="textbox204">
                                                                                    <asp:DropDownList ID="ddlBaseCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBaseCategories_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                            <td valign="top" style="width: 500px;">
                                                                                <div id="divSuppierBaseTitle" style="height: 30px; background-color: #e8e8e8; text-align: center;
                                                                                    vertical-align: middle; font-weight: bold; line-height: 30px; cursor: pointer;">
                                                                                    Collapse
                                                                                </div>
                                                                                <div id="divSupplierBase">
                                                                                    <%--<asp:GridView ID="grdSuppliersNamesForBaseCategory" runat="server" AutoGenerateColumns="false"
                                                                                        ShowHeader="false" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdSuppliersNamesForBaseCategory_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkActive" runat="server" Text="<%#Bind('sBrandName') %>" AutoPostBack="true"
                                                                                                        OnCheckedChanged="chkActiveBase_Clicked" />
                                                                                                    <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                    <asp:HiddenField ID="hidSupplierBaseID" runat="server" Value="<%#Bind('SupplierBaseID') %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>--%>
                                                                                    <asp:DataList ID="dtlSuppliersNamesForBaseCategory" runat="server" RepeatColumns="2"
                                                                                        RepeatDirection="Horizontal" ShowHeader="false" Width="100%" GridLines="None"
                                                                                        OnItemDataBound="dtlSuppliersNamesForBaseCategory_RowDataBound">
                                                                                        <ItemTemplate>
                                                                                            <div>
                                                                                                <asp:CheckBox ID="chkActive" runat="server" Text="<%#Bind('sBrandName') %>" AutoPostBack="true"
                                                                                                    OnCheckedChanged="chkActiveBase_Clicked" />
                                                                                                <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                <asp:HiddenField ID="hidSupplierBaseID" runat="server" Value="<%#Bind('SupplierBaseID') %>" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="height30" colspan="2">
                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="writing-mode: tb-rl; white-space: nowrap; width: 200px; vertical-align: top;
                                                                                text-align: left;">
                                                                                <%--<asp:Label ID="lblVerticalSub" runat="server" Text=" yrogetaC buS" Visible="false"></asp:Label>--%>
                                                                                Sub Category
                                                                            </td>
                                                                            <td valign="top">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 100px;" valign="top">
                                                                                <div class="textbox204">
                                                                                    <asp:DropDownList ID="ddlSubCategories" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategories_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <div id="divSupplierSubTitle" style="height: 30px; background-color: #e8e8e8; text-align: center;
                                                                                    vertical-align: middle; font-weight: bold; line-height: 30px; cursor: pointer;">
                                                                                    Collapse
                                                                                </div>
                                                                                <div id="divSupplierSub">
                                                                                    <%-- <asp:GridView ID="grdSupplierNamesForSubcategory" runat="server" AutoGenerateColumns="false"
                                                                                        ShowHeader="false" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdSupplierNamesForSubcategory_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkActive" runat="server" Text="<%#Bind('sBrandName') %>" AutoPostBack="true"
                                                                                                        OnCheckedChanged="chkActiveSub_Clicked" />
                                                                                                    <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                    <asp:HiddenField ID="hidSupplierSubID" runat="server" Value="<%#Bind('SupplierSubID') %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>--%>
                                                                                    <asp:DataList ID="dtlSupplierNamesForSubcategory" runat="server" RepeatColumns="2"
                                                                                        RepeatDirection="Horizontal" ShowHeader="false" Width="100%" GridLines="None"
                                                                                        OnItemDataBound="dtlSupplierNamesForSubcategory_RowDataBound">
                                                                                        <ItemTemplate>
                                                                                            <div>
                                                                                                <asp:CheckBox ID="chkActive" runat="server" Text="<%#Bind('sBrandName') %>" AutoPostBack="true"
                                                                                                    OnCheckedChanged="chkActiveSub_Clicked" />
                                                                                                <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                <asp:HiddenField ID="hidSupplierSubID" runat="server" Value="<%#Bind('SupplierSubID') %>" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="height30" colspan="2">
                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr style="height: 27px;">
                                                                            <td colspan="2" align="center" valign="top">
                                                                                <asp:Label ID="lblRecordMessage" runat="server" Text="Record Saved" Style="font-weight: bold;
                                                                                    color: #03a02c; display: none;"></asp:Label>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblVerticalIndividual" runat="server" Text="Individual Products"></asp:Label>
                                                                            </td>
                                                                            <td valign="top">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" colspan="2">
                                                                                <div class="textbox204">
                                                                                    <asp:DropDownList ID="ddlBaseCategoryIndividual" runat="server" AutoPostBack="true"
                                                                                        OnSelectedIndexChanged="ddlBaseCategoryIndividual_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" valign="top">
                                                                                <div class="textbox204">
                                                                                    <asp:DropDownList ID="ddlSubCategoryIndividual" runat="server" AutoPostBack="true"
                                                                                        OnSelectedIndexChanged="ddlSubCategoryIndividual_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <div class="textbox204">
                                                                                    <asp:DropDownList ID="ddlProductsIndividual" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProductsIndividual_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                            <td valign="top">
                                                                                <div id="divSupplierProTitle" style="height: 30px; background-color: #e8e8e8; text-align: center;
                                                                                    vertical-align: middle; font-weight: bold; line-height: 30px; cursor: pointer;">
                                                                                    Collapse
                                                                                </div>
                                                                                <div id="divSupplierPro">
                                                                                    <%-- <asp:GridView ID="grdSupplierNamesForProducts" runat="server" AutoGenerateColumns="false"
                                                                                        ShowHeader="false" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdSupplierNamesForProducts_RowDataBound">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkActive" runat="server" Text="<%#Bind('sBrandName') %>" AutoPostBack="true"
                                                                                                        OnCheckedChanged="chkActivePro_Clicked" />
                                                                                                    <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                    <asp:HiddenField ID="hidSupplierProID" runat="server" Value="<%#Bind('SupplierProID') %>" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>--%>
                                                                                    <asp:DataList ID="dtlSupplierNamesForProducts" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                                                        ShowHeader="false" Width="100%" GridLines="None" OnItemDataBound="dtlSupplierNamesForProducts_RowDataBound">
                                                                                        <ItemTemplate>
                                                                                            <div>
                                                                                                <asp:CheckBox ID="chkActive" runat="server" Text="<%#Bind('sBrandName') %>" AutoPostBack="true"
                                                                                                    OnCheckedChanged="chkActivePro_Clicked" />
                                                                                                <asp:HiddenField ID="hidSupplierID" runat="server" Value="<%#Bind('pksupplierid') %>" />
                                                                                                <asp:HiddenField ID="hidSupplierProID" runat="server" Value="<%#Bind('SupplierProID') %>" />
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
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
            <div class="clear_10">
            </div>
        </div>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnExtent1"
        PopupControlID="pnlAddresses" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent1" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlAddresses" runat="server">
        <asp:UpdatePanel ID="upnlAddresses" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                        <tr>
                            <td>
                                From:
                            </td>
                            <td>
                                <div class="textbox204_message">
                                    <asp:Label ID="lblFromAddress" runat="server" Text="Amjad Latif"></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                To:
                            </td>
                            <td>
                                <div class="textbox204_message">
                                    <asp:Label ID="lblToAddress" runat="server" Text="Hamid"></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Subject:
                            </td>
                            <td>
                                <div class="textbox204_message">
                                    <asp:TextBox ID="txtSubject" runat="server" Style="border-style: none; border-color: inherit;
                                        border-width: 0px; color: Black; text-align: center; font-family: Verdana,Geneva,sans-serif;
                                        font-size: 12px; background: ; margin-top: 8px;" ValidationGroup="reqmes"> </asp:TextBox></div>
                                <span style="float: left;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtSubject"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="reqmes"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Message:
                            </td>
                            <td>
                                <div class="textboxmulti" style="float: left;">
                                    <asp:TextBox ID="txtMessage" runat="server" ValidationGroup="reqmes" TextMode="MultiLine"> </asp:TextBox>
                                </div>
                                <span style="float: left;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtMessage"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="reqmes"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnMessage" runat="server" ImageUrl="~/Images/btn_send.gif"
                                    ValidationGroup="reqmes" OnClick="imgBtnMessage_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnExtent2"
        PopupControlID="pnlPriceGraph" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent2" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlPriceGraph" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 932px !important;">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblPriceDetail" runat="server" Style="font-size: 15px; font-family: Arial;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                From:
                            </td>
                            <td align="left">
                                <div class="textbox204">
                                    <asp:TextBox ID="txtFromDatePopup" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Style="float: right;
                                        position: relative; top: 6px;" ControlToValidate="txtFromDatePopup" Display="Dynamic"
                                        ErrorMessage="*" ValidationGroup="reqGraph"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td align="right">
                                To
                            </td>
                            <td align="right">
                                <div class="textbox204">
                                    <asp:TextBox ID="txtEndDatePopup" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Style="float: right;
                                        position: relative; top: 6px;" ControlToValidate="txtEndDatePopup" Display="Dynamic"
                                        ErrorMessage="*" ValidationGroup="reqGraph"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgBtnGraph" runat="server" ImageUrl="~/Images/btn_filter.png"
                                    OnClick="imgBtnGraph_Click" ValidationGroup="reqGraph" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div id="chart_div" style="width: 900px; height: 500px;">
                                    <img id="imgGraph" runat="server" src="" alt="" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
          </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="btnExtent3"
        PopupControlID="pnlOrder" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent3" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlOrder" runat="server">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 800px !important;">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender3.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td rowspan="6">
                                <img id="imgCompanyLogoPop" runat="server" src="../Images/no_image.gif" style="width: 149px;" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblCompanyBrandPop" runat="server" Text="" Style="font-size: 30px;
                                    font-weight: bold; font-family: Arial;"></asp:Label>
                                <%--Company Brand Name--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCompanyAddressPOp" runat="server" Text=""></asp:Label>
                            </td>
                            <%--Address, street 34, Region, Postcode, Country--%>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCompanyPhoneEmailPop" runat="server" Text=""></asp:Label>
                            </td>
                            <%--Fav. Phone:123456789 - Fav. Email: someone@email.com - Fav. FAX: 123456789 --%>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblCompanyContactPop" runat="server" Text=""></asp:Label>
                            </td>
                            <%--Contact Person Name: Someone - Contact Person Phone and email: blahblahblah--%>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblOrderOF" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkInvoicePop" runat="server" Text="Invoice" Enabled="false" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblInvoicePop" runat="server" Text="Invoice#:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceNumPOp"></asp:Label>
                            </td>
                            <td align="right">
                                <img id="img1" src="../Images/pdf.png" width="32" />
                            </td>
                            <td align="left">
                                <img id="img2" src="../Images/print.png" width="32" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:GridView ID="grdOrderDetailPop" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="15" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                                    AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdOrderDetailPop_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text="<%#Bind('sProductName') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packaging">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacking" runat="server" Text="<%#Bind('pName') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text="<%#Bind('qName') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkOldPrice" runat="server" Text="<%#Bind('ProudctPrice') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VAT">
                                            <ItemTemplate>
                                                <div class="textbox115">
                                                    <asp:Label ID="lblAfterVat" runat="server" Text="<%#Bind('vat') %>"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <div class="textbox115">
                                                    <asp:Label ID="lblQty" runat="server" Text="<%#Bind('Quantity') %>"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubTotal">
                                            <ItemTemplate>
                                                <div class="textbox115">
                                                    <asp:Label ID="lblSubtotalPop" runat="server" Text="<%#Bind('subtotal') %>"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <div class="height30" colspan="4" style="margin-top: 20px;">
                                    <img src="../images/horizontal_line.png" alt="" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" align="right">
                                <div style="float: right; margin-right: 35px;">
                                    <div style="float: left;">
                                        Invoice Total :
                                    </div>
                                    <div class="textbox115" style="position: relative; top: -4px; float: left;">
                                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
