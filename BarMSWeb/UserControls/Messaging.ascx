<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Messaging.ascx.cs" Inherits="UserControls_Messaging" %>

<script type="text/javascript">
    function SelectAll(objChk) {
        var id = objChk.id;
        //alert(id);
        //get reference of GridView control
        var grid = document.getElementById("<%= grdUsers.ClientID %>");
        //variable to contain the cell of the grid
        var cell;

        if (grid.rows.length > 0) {
            //loop starts from 1. rows[0] points to the header.
            for (i = 1; i < grid.rows.length; i++) {
                //get the reference of first column
                cell = grid.rows[i].cells[0];

                //loop according to the number of childNodes in the cell
                for (j = 0; j < cell.childNodes.length; j++) {
                    //if childNode type is CheckBox                 
                    if (cell.childNodes[j].type == "checkbox") {
                        //assign the status of the Select All checkbox to the cell checkbox within the grid
                        cell.childNodes[j].checked = document.getElementById(id).checked;

                    }

                }
            }
        }

    }


    var allChecked_OnlyForPage = false;

    function SelectAll_OnlyForPage(chk) {
        var trs = document.getElementById("ctl00_ContentPlaceHolder1_myID_grdUsers");
        var rows = trs.getElementsByTagName("tr");
        var count = 1;
        var chkname;
        var myCount = 0;
        var singleCheckbox = document.getElementById("ctl00_ContentPlaceHolder1_myID_grdUsers_ctl01_chkAll");
        if (singleCheckbox.checked) {allChecked_OnlyForPage = true;}
        else if (!singleCheckbox.checked) {allChecked_OnlyForPage = false;}
       
        for (i = 0; i < rows.length; i++) {
            count = count + 1;
            if (count < 10) {chkname = "ctl00_ContentPlaceHolder1_myID_grdUsers_ctl0" + count + "_chk";}
            else {chkname = "ctl00_ContentPlaceHolder1_myID_grdUsers_ctl0" + count+ "_chk";}
            if (document.getElementById(chkname) != null) {document.getElementById(chkname).checked = allChecked_OnlyForPage;}
        }
    }

    function CheckUncheck_SingleCheckbox() {
        var trs = document.getElementById("ctl00_ContentPlaceHolder1_myID_grdUsers");
        var rows = trs.getElementsByTagName("tr");
        var chkname;
        var falseFlag = 0;
       
        for (var i = 1; i < rows.length; i++) {
            chkname = "ctl00_ContentPlaceHolder1_myID_grdUsers_ctl0" + String(i+1) + "_chk";
            var chkTemp = document.getElementById(chkname);
            
            if (chkTemp == null) {break;}
            if (chkTemp.checked == false) {falseFlag = 1;}
        }

        if (falseFlag == 1) {
            var chkmain = document.getElementById("ctl00_ContentPlaceHolder1_myID_grdUsers_ctl01_chkAll");
            chkmain.checked = false;
        }
        else {
            var chkmain = document.getElementById("ctl00_ContentPlaceHolder1_myID_grdUsers_ctl01_chkAll");
            chkmain.checked = true;
        }
    }    
    
    
    function inbox() {
//        __doPostBack('btnInbox_Click', 'OnClick');
        window.location = 'Messages.aspx'
     }
    
    $(function() {
        if ($("#ctl00_ContentPlaceHolder1_myID_hidAllUsers").val() != "") {
            var myArray = $("#ctl00_ContentPlaceHolder1_myID_hidAllUsers").val().split(',');
            var ul = $("#feed");
            for (j = 0; j < myArray.length; j++) {
                var singleUser = myArray[j].split(':');
                var new_element = document.createElement('li');
                new_element.innerHTML = singleUser[1];
                new_element.setAttribute("value", singleUser[0]);
                ul.append(new_element);
            }
        }
    });

    function OpenFeedbackWindow(FeedBack) {
        var feedbackText;
        feedbackText = FeedBack;
        document.getElementById("divBalloon").innerHTML = feedbackText;
        ShowContent("divFeedbackWindow");
    }
    function CloseFeedBackWindow() {

        HideContent("divFeedbackWindow");
    }
    
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
                                    <%--gdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg< gdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg--%>
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
<table border="0" cellspacing="0" cellpadding="0" align="center">
    <tr>
        <td>
            <asp:HiddenField ID="hidUsers" runat="server" Value="0" />
            <asp:HiddenField ID="hidAllUsers" runat="server" Value="0" />
            <asp:HiddenField ID="hidReplyInboxID" runat="server" />
            <asp:HiddenField ID="hidReplySentboxID" runat="server" />
        </td>
    </tr>
</table>
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
            <div style="margin-top:-70px;position:absolute;"> <asp:LinkButton ID="lnkChangeDepartment" runat="server" Visible="false" Style="text-decoration: underline;"
                Text="Change Department Request<br /><br />" OnClick="lnkChangeDepartment_Click"></asp:LinkButton></div>
            <div class="rounded_box">
                <%--<asp:Button ID="btnSent" runat="server" Text="Sent" Style="float: right; margin-left: 5px;" OnClick="btnSent_Click" />--%>
                <asp:ImageButton ID="btnSent" runat="server" ImageUrl="~/Images/btn_sent.png" Style="float: right;
                    margin-left: 5px;" OnClick="btnSent_Click" />
                <%--<asp:Button ID="btnUsers" runat="server" Text="Users" Style="float: right; margin-left: 5px;" OnClick="btnUsers_Click" />--%>
                <asp:ImageButton ID="btnUsers" runat="server" ImageUrl="~/Images/btn_users.png" Style="float: right;
                    margin-left: 5px;" OnClick="btnUsers_Click" />
                <%--<asp:Button ID="btnCompose" runat="server" Text="Compose" Style="float: right; margin-left: 5px;" OnClick="btnCompose_Click" />--%>
                <asp:ImageButton ID="btnCompose" runat="server" ImageUrl="~/Images/btn_compose.png"
                    Style="float: right; margin-left: 5px;" OnClick="btnCompose_Click" />
                <div id="divDoPostBack" onclick="javascript:void inbox()" style="float: right; cursor: pointer;">
                    <span id="spInbox" runat="server" style="position: absolute; float: right; padding-left: 60px;
                        color: #fff; font-size: 11px; font-weight: normal; padding-top: 3px;"></span>
                    <%--<asp:Button ID="btnInbox" runat="server" Text="InBox" Style="float: right;" OnClick="btnInbox_Click" />--%>
                    <asp:ImageButton ID="btnInbox" runat="server" ImageUrl="~/Images/btn_Inbox.png" Style="float: right;"
                        OnClick="btnInbox_Click" />
                </div>
                <asp:MultiView ID="MV1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ViewInBox" runat="server">
                        <%--<asp:Button ID="btnDeleteInBox" runat="server" Text="Delete" OnClick="btnDeleteInBox_Click" />--%>
                        <asp:ImageButton ID="btnDeleteInBox" runat="server" ImageUrl="~/Images/btn_delete.png"
                            OnClick="btnDeleteInBox_Click" />
                        <div class="clear_10">
                        </div>
                        <asp:GridView ID="grdReceive" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="header_row"
                            AllowPaging="true" ShowHeader="false" AlternatingRowStyle-CssClass="alternate_row"
                            RowStyle-CssClass="rowstyle" CellPadding="0" BorderStyle="None" BorderWidth="0"
                            GridLines="None" OnRowCommand="grdReceive_RowCommand" OnRowDataBound="grdReceive_RowDataBound"
                            EmptyDataText="Sorry! Your Inbox have no messages." OnPageIndexChanging="grdReceive_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkFrom" runat="server" />
                                        <asp:Label ID="lblInBoxID" runat="server" Text="<%#Bind('pkInBoxID') %>" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 200px; text-align: left;">
                                                    <asp:Label ID="lblFromAddress" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 500px; text-align: left;">
                                                    <asp:Label ID="lblFromMessage" runat="server"></asp:Label>
                                                    <asp:LinkButton ID="lnkSubjectFrom" runat="server" CommandName="message" CommandArgument="<%#Bind('pkInBoxID') %>"></asp:LinkButton>
                                                </td>
                                                <td style="width: 300px; text-align: left;">
                                                    <asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewCompose" runat="server">
                        <div class="clear_10">
                        </div>
                        <div>
                            <ol>
                                <li id="facebook-list" class="input-text">
                                    <asp:Panel ID="pnlCompose" runat="server" DefaultButton="btnSubmitComposeMessage">
                                        <table cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <td style="font-weight: bold;">
                                                    To:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="fd" runat="server" CssClass="subject" BackColor="#f0f0f0" Text=""></asp:TextBox>
                                                    <div id="facebook-auto">
                                                        <div class="default">
                                                            Please start typing a user’s name</div>
                                                        <ul id="feed">
                                                            <%-- <li value="test1">tester 1</li>
                                        <li value="test2">tester 2</li>
                                        <li value="test3">tester 3</li>
                                        <li value="test4">t3</li>--%>
                                                        </ul>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold;">
                                                    Subject:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSubject" runat="server" BackColor="#f0f0f0" Style="width: 790px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="rmq"
                                                        ControlToValidate="txtSubject" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 3px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold;">
                                                    Message:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtComposeMessage" runat="server" BackColor="#f0f0f0" TextMode="MultiLine"
                                                        Width="700" Style="padding: 5px;" Height="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="rmq"
                                                        ControlToValidate="txtComposeMessage" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <%--<asp:Button ID="btnSubmitComposeMessage" runat="server" Text="Submit" OnClick="btnSubmitComposeMessage_Click"
                                                        ValidationGroup="rmq" />--%>
                                                    <asp:ImageButton ID="btnSubmitComposeMessage" Style="width: auto;" runat="server"
                                                        OnClick="btnSubmitComposeMessage_Click" ImageUrl="~/Images/btn_submit.png" ValidationGroup="rmq" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <ul id="preadded" style="display: none" runat="server">
                                        <%--<li id="1">Jorge Luis Borges</li>
                        <li id="2">Julio Cortazar</li>--%>
                                    </ul>
                                </li>
                            </ol>
                        </div>

                        <script type="text/javascript">
                            $(document).ready(function() {



                                $.facebooklist('#ctl00_ContentPlaceHolder1_myID_fd', '#ctl00_ContentPlaceHolder1_myID_preadded', '#facebook-auto', { url: '../UserControls/fetched.txt', cache: 1 }, 10, { userfilter: 1, casesensetive: 0 });

                                $("#ctl00_ContentPlaceHolder1_myID_btnSubmitComposeMessage").click(function() {

                                    var hid = $("#ctl00_ContentPlaceHolder1_myID_hidUsers");


                                    hid.val("");
                                    var u = '';
                                    $('.bit-box').each(function(index) {
                                        u += $(this).text() + ':' + $(this).attr('value') + ','
                                    });
                                    
                                    if (u == '') {
                                        alert("Pls Select User");
                                        return false;
                                    }
                                    u = u.substring(0, u.length - 1);
                                    hid.val(u);

                                });



                            });
                        </script>

                    </asp:View>
                    <asp:View ID="ViewUsers" runat="server">
                        <div class="clear_10">
                        </div>
                        <asp:GridView ID="grdUsers" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                            RowStyle-CssClass="rowstyle" Width="30%" OnRowCommand="grdUsers_RowCommand" OnRowDataBound="grdUsers_RowDataBound"
                            CellPadding="0" BorderStyle="None" BorderWidth="0" GridLines="None">
                            <Columns>
                                <%--<asp:BoundField DataField="FullName" HeaderText="Name" ItemStyle-Width="200px" />--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="javascript:SelectAll_OnlyForPage(this);"
                                            Text="All" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" onclick="javascript:CheckUncheck_SingleCheckbox();" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0px">
                                    <ItemTemplate>
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="align_left" style="width: 0px;">
                                                    <asp:Label ID="lblUserID" runat="server" Style="display: none;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="align_left" style="width: 200px;">
                                                    <asp:LinkButton ID="lnkUser" runat="server"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <%--<asp:Button ID="btnGridUsers" runat="server" Text="Select User(s)" OnClick="btnGridUsers_Click" />--%>
                        <asp:ImageButton ID="btnGridUsers" runat="server" OnClick="btnGridUsers_Click" ImageUrl="~/Images/btn_selectusers.png" />
                    </asp:View>
                    <asp:View ID="ViewSent" runat="server">
                        <%--<asp:Button ID="btnDeleteOutBox" runat="server" Text="Delete" OnClick="btnDeleteOutBox_Click" />--%>
                        <asp:ImageButton ID="btnDeleteOutBox" runat="server" ImageUrl="~/Images/btn_delete.png"
                            OnClick="btnDeleteOutBox_Click" />
                        <div class="clear_10">
                        </div>
                        <asp:GridView ID="grdSent" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="header_row"
                            AllowPaging="true" ShowHeader="false" AlternatingRowStyle-CssClass="alternate_row"
                            RowStyle-CssClass="rowstyle" CellPadding="0" BorderStyle="None" BorderWidth="0"
                            GridLines="None" OnRowCommand="grdSent_RowCommand" OnRowDataBound="grdSent_RowDataBound"
                            EmptyDataText="Sorry! Your Sentbox have no messages" OnPageIndexChanging="grdSent_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTo" runat="server" />
                                        <asp:Label ID="lblOutBoxID" runat="server" Text="<%#Bind('pkSentBoxID') %>" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 200px; text-align: left;">
                                                    <asp:Label ID="lblToAddress" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 500px; text-align: left;">
                                                    <asp:Label ID="lblToMessage" runat="server"></asp:Label>
                                                    <asp:LinkButton ID="lnkSubject" runat="server" CommandName="message" CommandArgument="<%#Bind('pkSentBoxID') %>"></asp:LinkButton>
                                                </td>
                                                <td style="width: 300px; text-align: left;">
                                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:View>
                    <asp:View ID="ViewDetail" runat="server">
                        <asp:HiddenField ID="hidDetailToUserID" runat="server" />
                        <asp:HiddenField ID="hidDetailFromUserID" runat="server" />
                        <table width="100%">
                            <tr>
                                <td style="width: 100px;">
                                    From:
                                </td>
                                <td>
                                    <asp:Label ID="lblFrom" Font-Bold="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    To:
                                </td>
                                <td>
                                    <asp:Label ID="lblTo" Font-Bold="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date:
                                </td>
                                <td>
                                    <asp:Label ID="lblDate" Font-Bold="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Subject:
                                </td>
                                <td>
                                    <asp:Label ID="lblSubject" Font-Bold="false" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Message:
                                </td>
                                <td>
                                    <div id="divDetail" style="font-weight: normal;" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <h3>
                            <asp:Label ID="lblReplyMessage" runat="server" Visible="false" Text="Reply Messages"></asp:Label></h3>
                        <asp:GridView ID="grdReplies" runat="server" GridLines="None" AutoGenerateColumns="false"
                            ShowHeader="false" CellPadding="0" BorderStyle="None" BorderWidth="0" OnRowDataBound="grdReplies_RowDataBound"
                            Style="width: 100%">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 100px;">
                                                    From:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFromGrid" Font-Bold="false" runat="server" Text="<%#Bind('SenderName') %>"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    To:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblToGrid" Font-Bold="false" runat="server" Text="<%#Bind('ReceiverName') %>"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Date:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDateGrid" Font-Bold="false" runat="server" Text="<%#Bind('dReplyDate') %>"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Subject:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubjectGrid" Font-Bold="false" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Message:
                                                </td>
                                                <td>
                                                    <div id="divDetailGrid" style="font-weight: normal;" runat="server">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="background-color: Black; height: 1px;">
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <%--<asp:Button ID="btnReply" runat="server" Text="Reply" OnClick="btnReply_Click" />--%>
                                    <asp:ImageButton ID="btnReply" runat="server" ImageUrl="~/Images/btn_reply.png" OnClick="btnReply_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="ViewSendRequest" runat="server">
                        <table style="margin-top: -5px;">
                            <tr>
                                <td>
                                    <div class="textbox204">
                                        <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="false" Style="background: transparent;
                                            border-width: 0; border-style: none; width: 185px; color: #727272; font-size: 11px;
                                            margin-top: 8px;">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnSendRequest" runat="server" ImageUrl="../images/btn_sendrequest.png"
                                        OnClick="btnSendRequest_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblSendDeparmentMessage" runat="server" Visible="false" ForeColor="Red"
                                        Style="font-weight: bold;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
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
