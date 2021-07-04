<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master"
    AutoEventWireup="true" CodeFile="ManageSalary.aspx.cs" Inherits="Managers_ManageSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
        
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
            $('.filter').watermark('00,00');
            $("#ctl00_ContentPlaceHolder1_txtStartDate").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true
            });
            $("#ctl00_ContentPlaceHolder1_txtEndDate").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true
            });

            $("#ctl00_ContentPlaceHolder1_rdScaled").click(function() {
                $("#ctl00_ContentPlaceHolder1_divScaled").show();
                $("#ctl00_ContentPlaceHolder1_divStandard").hide();
                $("#ctl00_ContentPlaceHolder1_divPercentageSalary").hide();
            });

            $("#ctl00_ContentPlaceHolder1_rdStandardSalary").click(function() {
                $("#ctl00_ContentPlaceHolder1_divScaled").hide();
                $("#ctl00_ContentPlaceHolder1_divStandard").show();
                $("#ctl00_ContentPlaceHolder1_divPercentageSalary").hide();
            });

            $("#ctl00_ContentPlaceHolder1_rdPerSalary").click(function() {
                $("#ctl00_ContentPlaceHolder1_divScaled").hide();
                $("#ctl00_ContentPlaceHolder1_divStandard").hide();
                $("#ctl00_ContentPlaceHolder1_divPercentageSalary").show();
            });

        });
    </script>

    <div>
        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td>
                    <asp:LinkButton ID="lnkBack" runat="server" Text="Back To Manage Users" ForeColor="Blue" style="text-decoration: underline;"
                        OnClick="lnkBack_Click"></asp:LinkButton>
                </td>
                <td>
                    <h3>
                        <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Green"></asp:Label></h3>
                </td>
            </tr>
            <%--<tr style="display: none;">
                <td style="width: 180px;">
                    Users:
                </td>
                <td>
                    <div class="textbox204">
                        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <h3>
                        <asp:Label ID="lblUserName" runat="server" ForeColor="Green" Style="font-size: 18px;
                            text-decoration: underline;"></asp:Label></h3>
                </td>
            </tr>
            <tr>
                <td style="width: 180px;">
                    Employment Start Date:
                </td>
                <td>
                    <div class="demo textbox_date_new" style="width: 250px;">
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate"
                            ErrorMessage="*" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Employment End Date:
                </td>
                <td>
                    <div class="demo textbox_date_new" style="width: 250px;">
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndDate"
                            ErrorMessage="*" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Manage Bonus
                </td>
                <td>
                    <asp:CheckBox ID="chkBonus" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Salary Type:
                </td>
                <td>
                    <asp:RadioButton ID="rdScaled" runat="server" Text="Scaled" GroupName="sal" Checked="true" />
                    <asp:RadioButton ID="rdStandardSalary" runat="server" Text="Standard Salary" GroupName="sal" />
                    <asp:RadioButton ID="rdPerSalary" runat="server" Text="%Salary" GroupName="sal" />
                </td>
            </tr>
        </table>
        <div id="divScaled" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Scaled Salary:</h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180px">
                        Low Season:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtLowSeason" CssClass="filter" runat="server"></asp:TextBox>
                        </div><span style="font-weight:bold;line-height:3;margin-left:5px;"> €</span>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180px">
                        High Season:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtHighSeason" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:5px;"> €</span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStandard" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Standard Salary:
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180px">
                        Standard Salary:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtStandardSalary" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:5px;"> €</span> 
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPercentageSalary" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Percentage Salary:</h3>
                    </td>
                </tr>
                <tr>
                    <td style="width: 180px">
                        Percentage:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtPercentage" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:5px;"> %</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        Minimum/day:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtMinimumPerDay" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:5px;"> €</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        % Over:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtPercentageOver" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:5px;"> €</span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="clear_10">
        </div>
        <table>
            <tr>
                <td style="width: 185px">
                </td>
                <td>
                    <asp:ImageButton ID="imgBtnSubmit" runat="server" ImageUrl="~/Images/btn_save.png"
                        ValidationGroup="req" OnClick="imgBtnSubmit_Click" />
                </td>
            </tr>
        </table>
        <div class="clear_10">
        </div>
        <%-- <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                        Contract
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
        </table>--%>
    </div>
</asp:Content>
