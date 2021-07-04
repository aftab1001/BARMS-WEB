<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="MangeSeasonDate.aspx.cs" Inherits="DepartmentAdmin_MangeSeasonDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(function() {
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
            $("#ctl00_ContentPlaceHolder1_txtStartDate2").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true
            });
            $("#ctl00_ContentPlaceHolder1_txtEndDate2").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true
            });

            $("#ctl00_ContentPlaceHolder1_txtHighSeasonStart").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true
            });
            $("#ctl00_ContentPlaceHolder1_txtHighSeasonEnd").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
                buttonImageOnly: true
            });
        });
        function numbersCheck(txt) {
            //var lblerror = document.getElementById("ctl00_lblMsg");
            if (txt.value == "") {
                return;
            }
            var m = regIsNumber(txt.value);
            if (!m) {
                txt.value = txt.value.substr(0, txt.value.length - 1);

            }
        }
        var isWhole_re = /^\s*\d+\s*$/;
        function regIsNumber(s) {
            return String(s).search(isWhole_re) != -1


        }
    </script>

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
                   <h3> Low Season: <asp:Label ID="lblMessage" runat="server" Visible="false" Text="Successfully Updated!" ForeColor="Green"  style="position:absolute;margin-top:-22px;padding-left:238px;"></asp:Label>     </h3>
                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <div class="textbox_small_date">
                                    <asp:TextBox ID="txtFromDate1" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtFromDate1" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>                                    
                                </div>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <asp:DropDownList ID="ddlFromMonth1" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                Till
                            </td>
                            <td>
                                <div class="textbox_small_date">
                                    <asp:TextBox ID="txtTillDate1" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtTillDate1" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>                                    
                                </div>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <asp:DropDownList ID="ddlTillMonth1" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                AND
                            </td>
                        </tr>
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <div class="textbox_small_date">
                                    <asp:TextBox ID="txtFromDate2" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtFromDate2" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>                                    
                                </div>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <asp:DropDownList ID="ddlFromMonth2" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                Till
                            </td>
                            <td>
                                <div class="textbox_small_date">
                                    <asp:TextBox ID="txtTillDate2" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtTillDate2" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>                                    
                                </div>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <asp:DropDownList ID="ddlTillMonth2" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
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
                   <h3>High Season:</h3> 
                    <table cellpadding="2" cellspacing="2">
                        <tr>
                            <td>
                                From
                            </td>
                            <td>
                                <div class="textbox_small_date">
                                    <asp:TextBox ID="txtFromHighDate" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtFromHighDate" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>                                    
                                </div>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <asp:DropDownList ID="ddlFromHighMonth" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                Till
                            </td>
                            <td>
                                <div class="textbox_small_date">
                                    <asp:TextBox ID="txtTillHighDate" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txtTillHighDate" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator>                                    
                                </div>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <asp:DropDownList ID="ddlTillHighMonth" runat="server" AutoPostBack="false">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
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
    <div class="clear_10">
    </div>
    <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/btn_save.png"
        OnClick="imgBtnSave_Click" ValidationGroup="req" />
</asp:Content>
