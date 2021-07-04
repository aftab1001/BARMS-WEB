<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master"
    AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Statistics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {

        });
        function getLocation(url) {
            window.location = url;
        }
        function redirect() {
            var id = $("#hidUserid").val();
            alert(id);
            window.location = 'EditUser.aspx?id=' + id;
        }
        function printreport() {

            var a = window.open("../AccountManager/PrintStats.aspx", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function showModel() {

            var modalPopupBehaviorCtrl = $('ModalPopupBehaviorID');

            modalPopupBehaviorCtrl.show();

        }



        function printreport() {

            var txtParam = document.getElementById("ctl00_ContentPlaceHolder1_txtParams");
            var a = window.open("../AccountManager/PrintStats.aspx", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');

        }
        function pdfReport() {
            var txtParam = document.getElementById("ctl00_ContentPlaceHolder1_txtParams");
            var a = window.open("../AccountManager/PrintStats.aspx?r=ps", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');

        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <fieldset style="background-color: White;">
            <legend>Statistics </legend>
            <div style="width: 100%; float: left;">
                <table border="0" cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkIncome" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkIncome_Click">Income Statistics</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkRegister" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkRegister_Click">Register Statistics</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkSalaries" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkSalaries_Click">Salaries Statistics</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkExpense" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkExpense_Click">Expense Statistics</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkSupply" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkSupply_Click">Supply Statistics</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkProduct" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkProduct_Click">Product Statistics</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lnkPositionIncome" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkPositionIncome_Click">Position Income</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkOther" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkOther_Click">Other Income</asp:LinkButton>
                        </td>
                        <td>
                            <div style="height: 15px; width: 2px; background-color: Gray;">
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkStaff" runat="server" Style="color: Blue; text-decoration: underline;"
                                OnClick="lnkStaff_Click">Staff Statistics</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="background-color: Gray; height: 1px; width: 100%; float: left;">
            </div>
            <div id="divPositionsTop" runat="server" visible="false" style="float: left; width: 100%;">
                <div style="float: left; width: 100%;">
                    <table border="0" cellpadding="3" cellspacing="3" width="100%">
                        <tr>
                            <td>
                                <fieldset>
                                    <legend style="font-weight: bold;">Positions Included </legend>
                                    <asp:DataList ID="dtlPositions" runat="server" RepeatColumns="3">
                                        <ItemTemplate>
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkPosition" runat="server" />
                                                            <asp:HiddenField ID="hidPosition" runat="server" Value="<%#Bind('pkSpecialityID') %>" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPosition" runat="server" Text="<%#Bind('sSpeciality') %>"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imgBtnApplyPosition" runat="server" ImageUrl="../Images/apply_btn.png"
                                                OnClick="imgBtnApplyPosition_Click" />
                                        </FooterTemplate>
                                    </asp:DataList>
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="background-color: Gray; height: 1px; width: 100%; float: left;">
                </div>
            </div>
            <div style="float: left; width: 100%;">
                <table width="100%" align="center">
                    <tr id="trStaff" runat="server" visible="false">
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <div class="textbox115">
                                            <asp:DropDownList ID="ddlStaffYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffYear_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="textbox115">
                                            <asp:DropDownList ID="ddlStaffSpecialty" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStaffSpecialty_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="textbox115">
                                            <asp:DropDownList ID="ddlStaffName" runat="server">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqst" runat="server" ControlToValidate="ddlStaffName"
                                                ValidationGroup="staff" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <div class="textbox115">
                                            <asp:DropDownList ID="ddlStaffWeek" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="textbox115">
                                            <asp:DropDownList ID="ddlStaffDisplayOptions" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="right">
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="../images/clear_btn.png"
                                            OnClick="imgbtnClear_Click" />
                                    </td>
                                    <td align="left">
                                        <asp:ImageButton ID="imgbtnApply" runat="server" ImageUrl="../images/apply_green_btn.png"
                                            OnClick="imgbtnApply_Click" ValidationGroup="staff" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblStatisticsTitle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr id="trReset" runat="server">
                        <td align="left" style="width: 135px;">
                            <div class="textbox115">
                                <asp:DropDownList ID="ddlOptions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOptions_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td align="left">
                            <div class="textbox115">
                                <asp:DropDownList ID="ddlWeek" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="width: 443px;">
                            <asp:ImageButton ID="imgBtnReset" runat="server" ImageUrl="../Images/reset_btn.png"
                                OnClick="imgBtnReset_Click" />
                            <div id="divExpense" runat="server" visible="false">
                                <div class="textbox115">
                                    <asp:DropDownList ID="ddlExpense" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExpense_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divSuppliers" runat="server" visible="false">
                                <div class="textbox115">
                                    <asp:DropDownList ID="ddlSuppliers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSuppliers_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divOtherIncome" runat="server" visible="false">
                                <div class="textbox115">
                                    <asp:DropDownList ID="ddlOtherIncome" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOtherIncome_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div id="divProducts" runat="server" visible="false" style="float; left;">
                                <div class="textbox115" style="float: left;">
                                    <asp:DropDownList ID="ddlBaseCat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBaseCat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="textbox115" style="float: left;">
                                    <asp:DropDownList ID="ddlSubCat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="textbox115" style="float: left;">
                                    <asp:DropDownList ID="ddlProduct" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqpro" runat="server" ControlToValidate="ddlProduct"
                                        ErrorMessage="*" ValidationGroup="pro" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <asp:ImageButton ID="imgBtnFilter" runat="server" ImageUrl="../images/btn_filter.png"
                                        OnClick="imgBtnFilter_Click" ValidationGroup="pro" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="background-color: Gray; height: 1px; width: 100%; float: left;">
            </div>
            <div id="divYear" runat="server" style="float: left; width: 100%; padding-top: 15px;
                padding-bottom: 15px; padding: 5px;">
                <div style="width: 50%; float: left;">
                    <table width="100" border="0" cellpadding="3" cellspacing="3" style="height: 105px;">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="../Images/print.png" OnClick="imgbtnPrint_Click"
                                    Width="20" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnpdf" runat="server" ImageUrl="../Images/pdf.png" Width="20"
                                    OnClick="imgbtnpdf_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnGraph" runat="server" ImageUrl="../Images/graph.png" OnClick="imgbtnGraph_Click"
                                    Width="20" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr style="vertical-align: bottom;">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <div class="textbox115">
                                    <asp:DropDownList ID="ddlStartYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStartYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div class="textbox115">
                                    <asp:DropDownList ID="ddlEndYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStartYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left; width: 50%;">
                    <fieldset style="background-color: #cccccc;">
                        <legend>TOTALS</legend>
                        <table align="center" cellpadding="2" cellspacing="2" border="0">
                            <tr>
                                <td style="width: 167px;">
                                    € Difference
                                </td>
                                <td>
                                    % Difference
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divDiffValue" runat="server" class="textbox115">
                                        <asp:Label ID="lblDifferenceValue" runat="server" Text="-2.612 €"></asp:Label></div>
                                </td>
                                <td>
                                    <div id="divDiffPer" runat="server" class="textbox115">
                                        <asp:Label ID="lblDifferencePercentage" runat="server" Text="-17.23 %"></asp:Label></div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
            <div id="divContent" runat="server" style="float: left; width: 100%;">
                <table width="100%" border="0" cellpadding="3" cellspacing="3">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblStartYear" runat="server" Text="Income 2011"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblEndYear" runat="server" Text="Income 2012"></asp:Label>
                        </td>
                        <td style="left: 100px; position: relative;">
                            € Difference
                        </td>
                        <td style="left: 25px; position: relative;">
                            % Difference
                        </td>
                    </tr>
                    <td>
                    </td>
                </table>
            </div>
            <div id="divStaffInfo" runat="server" visible="false" style="float: left; width: 100%;">
                <hr style="color: Black; width: 100%;" />
                <table id="tblMain" style="width: 100%; height: 100%" border="0" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                        </td>
                        <td style="font-weight: bold; font-size: 20px;">
                            <asp:Label ID="lblStaffName" runat="server">Amjad Latif</asp:Label>
                        </td>
                        <td>
                            <%--<img alt="SMS" src="../Images/Message.png" onclick="javascript:Message();" style="cursor: pointer;" />--%>
                            <asp:ImageButton ID="imgBtnMessageIcon" runat="server" ImageUrl="../Images/Message.png"
                                OnClick="imgBtnMessageIcon_Click" />
                        </td>
                        <td>
                            <a href="#" id="ankUser" runat="server">
                                <img alt="Edit" src="../Images/edit_icon.png" " " style="cursor: pointer;" /></a>
                            <asp:HiddenField ID="hidUserid" runat="server" Value="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100px;">
                            <img id="imgUser" runat="server" alt="User Image" src="../Images/no_image.gif" style="width: 150px;
                                height: 150px;" />
                        </td>
                        <td style="padding-top: 53px; padding-left: 15px;">
                            <table id="tblUserInfo">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server">User Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUserName" runat="server">Amjad Latif</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server">Email:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEmail" runat="server">amjad@leadconcept.com</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server">Mobile:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblmobile" runat="server">1234567</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server">Address:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbladdress" runat="server">35 H block new muslim town, lahore 54000 pakistan</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table id="tblWeek">
                    <tr>
                        <td style="width: 200px;">
                        </td>
                        <td>
                            <asp:Label ID="lblWeekNumbers" runat="server">Week# 17 till Week# 19: Sunday 24/04/2012 till saturday 14/05/2012</asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="divFieldSet">
                    <fieldset>
                        <legend>GRAND TOTALS </legend>
                        <table id="tblInnerContents" width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width: 100px;">
                                </td>
                                <td align="right">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 545px;">
                                        <tr>
                                            <td>
                                                Income
                                            </td>
                                            <td>
                                                Salary
                                            </td>
                                            <td>
                                                Tip
                                            </td>
                                            <td>
                                                Bonus
                                            </td>
                                            <td>
                                                Penalty
                                            </td>
                                            <td>
                                                Advance
                                            </td>
                                            <td>
                                                Late
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblIncomeGrand" runat="server" Text="00,00"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblSalaryGrand" runat="server" Text="00,00"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblTipGrand" runat="server" Text="00,00"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblBonusGrand" runat="server" Text="00,00"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblPenaltyGrand" runat="server" Text="00,00"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblAdvanceGrand" runat="server" Text="00,00"></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:Label ID="lblLateGramd" runat="server" Text="0"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
            <div id="divWeeks" runat="server" style="float: left; width: 100%;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:GridView ID="grdWeeks" runat="server" AutoGenerateColumns="false" ShowFooter="false"
                                BorderColor="Transparent" ShowHeader="false" OnRowDataBound="grdWeeks_RowDataBound"
                                Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <fieldset>
                                                <legend>
                                                    <asp:Label ID="lblWeekTitle" runat="server" Text=""></asp:Label>
                                                </legend>
                                                <table id="tblSave" runat="server" width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <th style="text-align: right; background-color: #CFE2FC; height: 34px;">
                                                                        <asp:Label ID="lblHeadingTotal" runat="server" Text="TOTALS:" Style="position: relative;
                                                                            right: 84px;"></asp:Label>
                                                                        <asp:Label ID="lblWeekTotal1" runat="server" Text="9.922 €" Style="position: relative;
                                                                            right: 8px;"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grdWeekDetail1" runat="server" AutoGenerateColumns="false" RowStyle-CssClass="rowstyle"
                                                                            ShowHeader="false" ShowFooter="false" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            GridLines="None" OnRowDataBound="grdWeekDetail1_RowDataBound" OnRowCommand="grdWeekDetail1_RowCommand"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                                                                    ItemStyle-Width="175">
                                                                                    <ItemTemplate>
                                                                                        <div id="divDays" runat="server">
                                                                                            <asp:Label ID="lblDays" runat="server" Text="Suday 14/05/2011" Style="position: relative;
                                                                                                right: 35px;"></asp:Label>
                                                                                            <asp:LinkButton ID="lnkDays" runat="server" CommandName="order" Style="position: relative;
                                                                                                right: 35px;"></asp:LinkButton>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTotal" runat="server" Style="position: relative; right: 8px;"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2px; background-color: #9c9c9c;">
                                                        </td>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <th style="text-align: left; background-color: #CFE2FC; height: 34px;">
                                                                        <asp:Label ID="lblWeekTotal2" runat="server" Text="9.922 €" Style="position: relative;
                                                                            left: 8px;"></asp:Label>
                                                                        <asp:Label ID="Label1" runat="server" Text="TOTALS:" Style="display: none;"></asp:Label>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:GridView ID="grdWeekDetail2" runat="server" AutoGenerateColumns="false" RowStyle-CssClass="rowstyle"
                                                                            ShowHeader="false" ShowFooter="false" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            GridLines="None" OnRowDataBound="grdWeekDetail2_RowDataBound" OnRowCommand="grdWeekDetail2_RowCommand"
                                                                            Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <div id="divDays" runat="server">
                                                                                            <asp:Label ID="lblTotal" runat="server" Style="position: relative; left: 8px;"></asp:Label>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="175">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDays" runat="server" Text="Suday 14/05/2011" Style="position: relative;
                                                                                            left: 35px;"></asp:Label>
                                                                                        <asp:LinkButton ID="lnkDays" runat="server" CommandName="order" Style="position: relative;
                                                                                            left: 35px;"></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2px; background-color: #9c9c9c;">
                                                        </td>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 187px;
                                                                width: 100%;" align="center">
                                                                <tr>
                                                                    <th valign="top" style="position: relative; background-color: #CFE2FC;" align="center">
                                                                        <div id="divDifference" runat="server" class="textbox115" style="width: 115px;">
                                                                            <asp:Label ID="lblDifference" runat="server" Text="00,00"></asp:Label>
                                                                        </div>
                                                                    </th>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblSaturdayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="alternate_row">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblFridayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblThursdayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="alternate_row">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblWednesdayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTuesdayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="alternate_row">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblMondayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblSundayDiff" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2px; background-color: #9c9c9c;">
                                                        </td>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 187px;
                                                                width: 100%;" align="center">
                                                                <tr>
                                                                    <th valign="top" style="position: relative; background-color: #CFE2FC;" align="center">
                                                                        <div id="divPercentage" runat="server" class="textbox115" style="width: 115px;">
                                                                            <asp:Label ID="lblPercentage" runat="server" Text="00,00"></asp:Label>
                                                                        </div>
                                                                    </th>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblSaturdayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="alternate_row">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblFridayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblThursdayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="alternate_row">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblWednesdayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblTuesdayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="alternate_row">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblMondayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr class="rowstyle">
                                                                    <td align="center">
                                                                        <asp:Label ID="lblSundayPer" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table id="tblStaff" runat="server" visible="false" width="100%" border="0" cellpadding="0"
                                                    cellspacing="0">
                                                    <tr>
                                                        <th style="width: 274px;">
                                                            WEEK TOTALS:
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblIncome_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblSalary_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblTips_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblBonus_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblPenalty_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblAdvance_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                        <th style="width: 12px;">
                                                        </th>
                                                        <th>
                                                            <div class="textbox_small">
                                                                <asp:Label ID="lblLate_WeekTotal" runat="server" Text="00,00"></asp:Label>
                                                            </div>
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10">
                                                            <asp:GridView ID="grdSalaries" runat="server" AutoGenerateColumns="false" GridLines="Vertical"
                                                                RowStyle-CssClass="rowstyle" ShowFooter="false" HeaderStyle-CssClass="header_row"
                                                                AlternatingRowStyle-CssClass="alternate_row" OnRowDataBound="grdSalaries_RowDataBound"
                                                                Width="100%">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="150">
                                                                        <ItemTemplate>
                                                                            <div id="divDaysForSalaries" runat="server">
                                                                                <asp:Label ID="lblDays" runat="server" Style="position: relative; left: 8px;" Text="<%#Bind('DayType') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Position" ItemStyle-Width="110">
                                                                        <ItemTemplate>
                                                                            <div id="divDays2" runat="server">
                                                                                <asp:Label ID="lblPosition" runat="server" Style="position: relative; left: 8px;"
                                                                                    Text="<%#Bind('position') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Income" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays3" runat="server">
                                                                                <asp:Label ID="lblIncome" runat="server" Style="position: relative; left: 8px;" Text="<%#Bind('income') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Salary" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays4" runat="server">
                                                                                <asp:Label ID="lblSalary" runat="server" Style="position: relative; left: 8px;" Text="<%#Bind('Salary') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tips" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays5" runat="server">
                                                                                <asp:Label ID="lblTips" runat="server" Style="position: relative; left: 8px;" Text="<%#Bind('Tip') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bonus" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays6" runat="server">
                                                                                <asp:Label ID="lblBonus" runat="server" Style="position: relative; left: 8px;" Text="<%#Bind('Bonus') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Penalty" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays7" runat="server">
                                                                                <asp:Label ID="lblAdvance" runat="server" Style="position: relative; left: 8px;"
                                                                                    Text="<%#Bind('Advance') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Advance" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays8" runat="server">
                                                                                <asp:Label ID="lblPenalty" runat="server" Style="position: relative; left: 8px;"
                                                                                    Text="<%#Bind('Penalty') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ontime">
                                                                        <ItemTemplate>
                                                                            <div id="divDays9" runat="server">
                                                                                <asp:CheckBox ID="chkOnTime" runat="server" Checked="<%#Bind('ontime') %>" Enabled="false" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Late" ItemStyle-Width="70">
                                                                        <ItemTemplate>
                                                                            <div id="divDays10" runat="server">
                                                                                <asp:Label ID="lblLate" runat="server" Style="position: relative; left: 8px;" Text="<%#Bind('latehour') %>"></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
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
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="Button1" runat="server" Style="display: none;" />
    <asp:Panel ID="Panel1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 800px !important;">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <asp:GridView ID="grdOrderDetail" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        PageSize="15" Width="100%" GridLines="None" HeaderStyle-CssClass="header_row"
                        EmptyDataText="No Order Found!" AlternatingRowStyle-CssClass="alternate_row"
                        RowStyle-CssClass="rowstyle" OnRowDataBound="grdOrderDetail_RowDataBound" OnPageIndexChanging="grdOrderDetail_PageIndexChanging"
                        OnRowCommand="grdOrderDetail_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDate" runat="server" CommandName="order" CommandArgument="<%#Bind('pkBaseOrderID') %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderNumber" runat="server" Text="<%#Bind('SessionOrderID') %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubTotal" runat="server" Text="<%#Bind('GrandSubtotal') %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 932px !important;">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                        <tr>
                            <td colspan="5">
                                <div id="chart_div" style="width: 900px; height: 500px;">
                                    <img id="imgGraph" runat="server" src="" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="btnExtent1"
        PopupControlID="pnlAddresses" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent1" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlAddresses" runat="server">
        <asp:UpdatePanel ID="upnlAddresses" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender4.ClientID %>').hide();return false;">
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
                                        font-size: 12px; background: ; margin-top: 8px;"> </asp:TextBox></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Message:
                            </td>
                            <td>
                                <div class="textboxmulti" style="float: left;">
                                    <asp:TextBox ID="txtMessage" runat="server" ValidationGroup="req" TextMode="MultiLine"> </asp:TextBox>
                                </div>
                                <span style="float: left;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtMessage"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnMessage" runat="server" ImageUrl="~/Images/btn_send.gif"
                                    ValidationGroup="req" OnClick="imgBtnMessage_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <div style="display: none;">
        <table>
            <tr>
                <td style="width: 639px">
                    <asp:Panel ID="htmlConvertControlPanel" runat="server" Width="100%">
                        <table cellspacing="0" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" style="width: 635px">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" style="width: 436px">
                                                            <tr>
                                                                <td style="width: 130px; height: 20px">
                                                                    <asp:CheckBox ID="cbLiveLinksEnabled" runat="server" Text="Live HTTP links" Checked="True"
                                                                        ToolTip="Make the HTTP links live in the generated PDF"></asp:CheckBox>
                                                                </td>
                                                                <td style="width: 8px; height: 20px;">
                                                                </td>
                                                                <td style="height: 20px; width: 105px;">
                                                                    <asp:CheckBox ID="cbBookmarks" runat="server" Text="Bookmarks" Checked="False" ToolTip="Bookmark H1 and H2 tags">
                                                                    </asp:CheckBox>
                                                                </td>
                                                                <td style="width: 6px; height: 20px;">
                                                                </td>
                                                                <td style="width: 158px; height: 20px">
                                                                    &nbsp;<asp:CheckBox ID="cbFitWidth" runat="server" Text="Fit Width" Checked="True"
                                                                        ToolTip="When is checked the HTML content will be resized if necessary to fit the PDF page">
                                                                    </asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 28px; width: 130px;">
                                                                    <asp:CheckBox ID="cbScriptsEnabled" runat="server" Text="Scripts Enabled" Checked="True"
                                                                        ToolTip="Enable JavaScripts and Java applets during conversion"></asp:CheckBox>
                                                                </td>
                                                                <td style="width: 8px">
                                                                </td>
                                                                <td style="width: 105px">
                                                                    <asp:CheckBox ID="cbEmbedFonts" runat="server" Text="Embed Fonts" Checked="False"
                                                                        ToolTip="When is checked the true type fonts will be embedded in the PDF document">
                                                                    </asp:CheckBox>
                                                                </td>
                                                                <td style="width: 6px">
                                                                </td>
                                                                <td style="width: 158px">
                                                                    &nbsp;<asp:CheckBox ID="cbJpegCompression" runat="server" Text="JPEG Compression"
                                                                        Checked="True" ToolTip="When is checked the images are compressed with JPEG in PDF"
                                                                        Width="145px"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 15px">
                                                        <asp:LinkButton ID="lnkBtnSettings" OnClick="lnkBtnSettings_Click" runat="server">More Converter Settings >></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 639px">
                    <asp:Panel ID="pnlRenderMoreOptions" runat="server" Visible="false">
                        <table cellspacing="0" cellpadding="0" style="width: 394px">
                            <tbody>
                                <tr>
                                    <td style="width: 424px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 424px">
                                        <asp:Panel ID="pnlRenderCommonOptions" runat="server">
                                            <table style="width: 292px" id="commonOptionsTable" cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="font-weight: bold; width: 246px; height: 30px">
                                                            Web page size:
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 246px">
                                                            <table style="width: 285px" cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 21px; height: 30px;">
                                                                            <asp:RadioButton ID="radioAutodetectWebPageSize" runat="server" Text="Autodetect"
                                                                                OnCheckedChanged="radioAutodetectWebPageSize_CheckedChanged" AutoPostBack="True"
                                                                                GroupName="WebPageSize" Width="89px"></asp:RadioButton>
                                                                        </td>
                                                                        <td style="height: 30px">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="width: 116px; height: 30px">
                                                                            <asp:RadioButton ID="radioCustomWebPageSize" runat="server" Text="Custom" OnCheckedChanged="radioCustomWebPageSize_CheckedChanged"
                                                                                AutoPostBack="True" GroupName="WebPageSize" Checked="True"></asp:RadioButton>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <asp:Panel ID="pnlCustomPageSize" runat="server" Visible="true">
                                                                                <table cellspacing="0" cellpadding="0" style="width: 268px">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td style="width: 71px">
                                                                                                Set Page Width:
                                                                                            </td>
                                                                                            <td style="width: 55px">
                                                                                                <asp:TextBox ID="textBoxCustomWebPageWidth" runat="server" Columns="4" MaxLength="10"
                                                                                                    Width="71px">1024</asp:TextBox><asp:CustomValidator ID="cvCustomPageWidth" runat="server"
                                                                                                        Display="Dynamic" OnServerValidate="cvCustomPageWidth_ServerValidate">*</asp:CustomValidator>
                                                                                            </td>
                                                                                            <td style="width: 53px">
                                                                                                px
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 246px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 424px">
                                        <asp:Panel ID="pnlImageRenderOptions" runat="server" Visible="false">
                                            <table cellspacing="0" cellpadding="0">
                                                <tbody>
                                                    <tr>
                                                        <td style="font-weight: bold">
                                                            Image format:
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlImageFormat" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 424px">
                                        <asp:Panel ID="pnlPDFRenderOptions" runat="server">
                                            <table id="PDFRenderOptionsTable" cellspacing="0" cellpadding="0" style="width: 355px">
                                                <tbody>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-weight: bold" colspan="2">
                                                            PDF Document Options
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                            PDF page format:
                                                        </td>
                                                        <td style="width: 64px; height: 23px;">
                                                            <asp:DropDownList ID="ddlPDFPageFormat" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                            PDF Compression Level:
                                                        </td>
                                                        <td style="width: 64px; height: 23px;">
                                                            <asp:DropDownList ID="ddlCompressionLevel" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                            PDF Page Orientation:
                                                        </td>
                                                        <td style="width: 64px; height: 23px">
                                                            <asp:DropDownList ID="ddlPageOrientation" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 23px">
                                                            PDF Standard:
                                                        </td>
                                                        <td style="width: 64px; height: 23px;">
                                                            <asp:DropDownList ID="ddlPdfSubset" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 24px" colspan="3">
                                                                            Header &amp; Footer:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="height: 20px">
                                                                            <asp:CheckBox ID="cbShowheader" runat="server" Text="Show header" OnCheckedChanged="cbShowheader_CheckedChanged"
                                                                                AutoPostBack="true"></asp:CheckBox>
                                                                        </td>
                                                                        <td style="height: 20px; width: 16px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="height: 20px">
                                                                            <asp:CheckBox ID="cbShowFooter" runat="server" Text="Show Footer" OnCheckedChanged="cbShowFooter_CheckedChanged"
                                                                                AutoPostBack="true"></asp:CheckBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 25px">
                                                                            Document Margins:
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0">
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Left:
                                                                                            <asp:TextBox ID="textBoxLeftMargin" runat="server" Columns="5" MaxLength="10">0</asp:TextBox>pt
                                                                                            <asp:CustomValidator ID="cvLeftMargin" runat="server" OnServerValidate="cvLeftMargin_ServerValidate"
                                                                                                Display="Dynamic">*</asp:CustomValidator>
                                                                                        </td>
                                                                                        <td style="width: 47px">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td>
                                                                                            Right: &nbsp; &nbsp;
                                                                                            <asp:TextBox ID="textBoxRightMargin" runat="server" Columns="5" MaxLength="10">0</asp:TextBox>pt
                                                                                            <asp:CustomValidator ID="cvRightMargin" runat="server" OnServerValidate="cvRightMargin_ServerValidate"
                                                                                                Display="Dynamic">*</asp:CustomValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 48px">
                                                                                            Top:
                                                                                            <asp:TextBox ID="textBoxTopMargin" runat="server" Columns="5" MaxLength="10">0</asp:TextBox>pt
                                                                                            <asp:CustomValidator ID="cvTopMargin" runat="server" OnServerValidate="cvTopMargin_ServerValidate"
                                                                                                Display="Dynamic">*</asp:CustomValidator>
                                                                                        </td>
                                                                                        <td style="width: 47px">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td style="height: 48px">
                                                                                            Bootom:
                                                                                            <asp:TextBox ID="textBoxBottomMargin" runat="server" Columns="5" MaxLength="10">0</asp:TextBox>pt
                                                                                            <asp:CustomValidator ID="cvBottomMargin" runat="server" OnServerValidate="cvBottomMargin_ServerValidate"
                                                                                                Display="Dynamic">*</asp:CustomValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="height: 25px">
                                                            <asp:CheckBox ID="cbGenerateSelectablePdf" runat="server" Text="Generate selectable text"
                                                                Checked="True"></asp:CheckBox>
                                                            &nbsp;
                                                            <asp:CheckBox ID="cbAvoidImageBreak" runat="server" Text="Avoid image break" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnlPDFHeaderOptions" runat="server" Visible="false">
                                                                <table cellspacing="0" cellpadding="0" style="width: 347px">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="font-weight: bold; height: 20px;" colspan="2">
                                                                                PDF Header Options:
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 23px">
                                                                                Header Title:
                                                                            </td>
                                                                            <td style="width: 230px; height: 23px;">
                                                                                <asp:TextBox ID="textBoxHeaderText" runat="server" Width="213px">Title</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 23px">
                                                                                Header Text Color:
                                                                            </td>
                                                                            <td style="width: 230px; height: 23px;">
                                                                                <asp:DropDownList ID="ddlHeaderColor" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="height: 23px">
                                                                                Header Subtitle:
                                                                            </td>
                                                                            <td style="width: 230px; height: 23px;">
                                                                                <asp:TextBox ID="textBoxHeaderSubtitle" runat="server" Width="211px">Subtitle</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="height: 23px">
                                                                                <asp:CheckBox ID="cbDrawHeaderLine" runat="server" Text="Draw Header Line" Checked="True">
                                                                                </asp:CheckBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Panel ID="pnlPDFFooterOptions" runat="server" Visible="false">
                                                                <table cellspacing="0" cellpadding="0" style="width: 345px">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="font-weight: bold; width: 70px; height: 20px;">
                                                                                PDF Footer Options:
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 125px; height: 23px;">
                                                                                Footer Text:
                                                                            </td>
                                                                            <td style="height: 23px">
                                                                                <asp:TextBox ID="textBoxFooterText" runat="server" Width="205px">Footer</asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 125px; height: 23px;">
                                                                                Footer Text Color:
                                                                            </td>
                                                                            <td style="height: 23px">
                                                                                <asp:DropDownList ID="ddlFooterTextColor" runat="server">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="height: 23px">
                                                                                <asp:CheckBox ID="cbShowPageNumber" runat="server" Text="Show Page Number" Checked="True">
                                                                                </asp:CheckBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 125px; height: 24px;">
                                                                                Page Number Text:
                                                                            </td>
                                                                            <td style="height: 24px">
                                                                                <asp:TextBox ID="textBoxPageNmberText" runat="server" Width="204px">Page </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="height: 23px">
                                                                                <asp:CheckBox ID="cbDrawFooterLine" runat="server" Text="Draw Footer Line" Checked="True">
                                                                                </asp:CheckBox>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
