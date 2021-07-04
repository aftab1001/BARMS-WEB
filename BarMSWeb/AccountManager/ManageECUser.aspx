<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master" AutoEventWireup="true" CodeFile="ManageECUser.aspx.cs" Inherits="AccountManager_ManageECUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <table cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
            <td style="width: 150px;">
                <%--<a id="ankViewall" runat="server" style="font-weight:bold;" href="ManageUsers.aspx" visible="false">View All</a>--%>
                <asp:LinkButton ID="lnkViewAll" runat="server" OnClick="lnkViewAll_Click" Visible="false"
                    ForeColor="Blue">View All</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                Year:
            </td>
            <td>
                <div class="textbox204">
                    <asp:DropDownList ID="ddlYears" runat="server">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr style="display:none;">
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
                    <div id="divFilter" runat="server" style="color: Green; font-weight: normal;">
                    </div>
                    <%--<asp:Label ID="lblFilterMessage" runat="server" ForeColor="Green" Style="font-size: 15px;"></asp:Label><br />
                    <br />--%>
                    <asp:UpdatePanel ID="upnlRefreshUsers" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                EmptyDataText="Sorry! No Data Found." HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                RowStyle-CssClass="rowstyle" Width="100%" OnRowCommand="grdUsers_RowCommand"
                                OnRowDataBound="grdUsers_RowDataBound" CellPadding="0" BorderStyle="None" BorderWidth="0"
                                GridLines="None">
                                <Columns>
                                    <%--<asp:BoundField DataField="FullName" HeaderText="Name" ItemStyle-Width="200px" />--%>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td class="align_left" style="width: 400px;">
                                                        <asp:LinkButton ID="lnkUser" runat="server"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgActive" runat="server" CommandName="Active" CommandArgument='<%# Bind("pkUserID") %>'
                                                ImageUrl="../images/activate_icon.gif" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salary" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnSalary" runat="server" ImageUrl="~/Images/Dollar.png"
                                                CommandName="Salary" CommandArgument='<%# Bind("pkUserID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bonus" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnBonus" runat="server" ImageUrl="~/Images/icon_b_greypng.png"
                                                CommandName="Bonus" CommandArgument='<%# Bind("pkUserID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contract" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtnContract" runat="server" ImageUrl="~/Images/icon_c_red.png"
                                                OnClientClick="return false;" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="bActiveByUser" HeaderText="By User" ItemStyle-Width="200px" />--%>
                                    <%--<asp:BoundField DataField="bActiveByAdmin" HeaderText="By Admin" ItemStyle-Width="200px" />--%>
                                    <asp:TemplateField HeaderText="Social Links" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a id="ankFacebook" runat="server" href="<%#Bind('sFaceBookProfile') %>" target="_blank">
                                                            <img src="../Images/facebook_m.png" width="16" /></a> <a id="ankTwitter" runat="server"
                                                                href="<%#Bind('sTwitterProfile') %>" target="_blank">
                                                                <img src="../Images/twitter_m.png" width="16" /></a> <a id="ankMsn" runat="server"
                                                                    onclick="return false;" href="#" style="cursor: auto;">
                                                                    <img src="../Images/msn_m.png" width="16" /></a> <a id="ankSkype" runat="server"
                                                                        onclick="return false;" href="#" style="cursor: auto;">
                                                                        <img src="../Images/skype_m.png" width="16" /></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Message">
                                        <ItemTemplate>
                                            <table border="0" cellspacing="0" cellpadding="0" style="width: 100px;">
                                                <tr>
                                                    <%--<td class="align_right">
                                    <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkUserID") %>'
                                        ImageUrl="../images/close.png" ToolTip="Delete" />
                                </td>--%>
                                                    <td class="align_left" style="width: 200px;">
                                                        <asp:ImageButton ID="imgMessage" runat="server" CommandName="Message" CommandArgument='<%# Bind("pkUserID") %>'
                                                            ImageUrl="~/Images/Message.png" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnExtent2"
        PopupControlID="pnlBonus" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent2" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlBonus" runat="server">
        <asp:Panel ID="Panel1" runat="server">
        <asp:UpdatePanel ID="upnlBonus" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%" align="center">
                    <tr >
                    <td align="left">
                    Last Bonus:
                    </td>
                    <td align="left">
                     <div class="textbox_small" style="float:left;margin-left:2px;">
                            <asp:TextBox ID="lblLastBouns" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> €</span>
                    
                    </td>
                    </tr>
                    
                    <tr>
                    <td>
                    <asp:Label runat="server" ID="lblMessagepopup" Visible="false"></asp:Label>
                    </td>
                    </tr>
                    <tr id="trSalaryType" runat="server">
                <td>
                    Salary Type:
                </td>
                <td>
                    <asp:RadioButton ID="rdScaled" runat="server" Text="Scaled" GroupName="sal" Checked="true" />
                    <asp:RadioButton ID="rdStandardSalary" runat="server" Text="Standard Salary" GroupName="sal" />
                    <asp:RadioButton ID="rdPerSalary" runat="server" Text="%Salary" GroupName="sal" />
                </td>
            </tr>
                    <tr id="trSalary" runat="server">
                    <td colspan="2">
                     <div id="divScaled" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Scaled Salary</h3>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        Low Season:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtLowSeason" CssClass="filter" runat="server"></asp:TextBox>
                        </div><span style="font-weight:bold;line-height:3;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        High Season:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtHighSeason" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStandard" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Standard Salary
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        Standard Salary:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtStandardSalary" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:1px;float:left;"> €</span> 
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPercentageSalary" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Percentage Salary</h3>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        Percentage:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtPercentage" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> %</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Minimum/day:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtMinimumPerDay" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        % Over:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtPercentageOver" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
            </table>
        </div>
                    </td>
                    </tr>
                        <tr>
                        <td colspan="2">
                        
                        <div id="divActiveDeactiveBonusMessage" runat="server" visible="false"></div>
                        </td>
                        </tr>
                        
                        <tr>
                        <td align="left">
                        Bonus Amount:
                        </td>
                        <td>
                        <div class="textbox_small" style="float: left; margin-left: 2px;">
                  <asp:TextBox ID="txtBonusAmount" runat="server"  Style="text-align: center;" CssClass="filter"
                  onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                         </div>
                        <span style="position: relative; float: left;font-weight:bold; left: 1px; line-height: 31px;">€</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" style="margin-left:-18px;float:left;" runat="server" ControlToValidate="txtBonusAmount"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                        
                        </td>
                            
                        </tr>
                         <tr>
                        <td align="left">
                        Notes:
                        </td>
                        <td>
                        <div class="textboxmulti"  style="margin-left: 2px;">
                            <asp:TextBox ID="txtNoteBonus" TextMode="MultiLine" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNoteBonus"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                          </div>
                        </td>
                        </tr>
                        
                        <tr>
                            <td align="right">
                                <asp:CheckBox ID="chkBonus" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblBonusActive" runat="server" Text="You can allow bonus by clicking on Checkbox"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnActiveBonus" ValidationGroup="Register" runat="server" ImageUrl="~/Images/btn_submit.png"
                                    OnClick="imgBtnActiveBonus_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    </asp:Panel>
</asp:Content>

