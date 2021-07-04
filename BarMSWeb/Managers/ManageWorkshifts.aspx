<%@ Page Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master" AutoEventWireup="true"
    CodeFile="ManageWorkshifts.aspx.cs" Inherits="Managers_ManageWorkshifts" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>
<style>
    .Monday
    {
    	}
    	.Monday:hover
    {
    	text-decoration:underline;
    	}
</style>
    <script type="text/javascript">
    function lblMondayDateClick(Btn)
    {
    __doPostBack('ctl00$ContentPlaceHolder1$'+Btn, '');
    }
  function  lblTuesdayDateClick()
  {
  __doPostBack('ctl00$ContentPlaceHolder1$lblTuesdayDate', '');
  }
   function  lblWednesdayDateClick()
  {
  __doPostBack('ctl00$ContentPlaceHolder1$lblWednesdayDate', '');
  }
   function  lblThursdayDateClick()
  {
  __doPostBack('ctl00$ContentPlaceHolder1$lblThursdayDate', '');
  }
    function  lblFridayDateClick()
  {
  __doPostBack('ctl00$ContentPlaceHolder1$lblFridayDate', '');
  }
  function lblSaturdayDateClick()
  {
   __doPostBack('ctl00$ContentPlaceHolder1$lblSaturdayDate', '');
  }
  function lblSundayDateClick()
  {
  __doPostBack('ctl00$ContentPlaceHolder1$lblSundayDate', '');
  }
        $(function() {
            //            $("#ctl00_ContentPlaceHolder1_datepicker").datepicker({
            //                showOn: "button",
            //                buttonImage: "../images/calender.png",
            //                buttonImageOnly: true
            //            });

            var startDate;
            var endDate;
            var Dateselect;

            var myDay = document.getElementById("ctl00_ContentPlaceHolder1_txtMyDay");
            var check = document.getElementById("ctl00_ContentPlaceHolder1_txtCheck");

            if (check.value != "1") {
                $("#Calendardate").datepicker({
                    defaultDate: myDay.value + 'd',
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    onSelect: function(dateText, inst) {
                        var date = $(this).datepicker('getDate');

                        Dateselect = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                        startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                        endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);

                        var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;

                        $('#ctl00_ContentPlaceHolder1_datepicker').text($.datepicker.formatDate(dateFormat, Dateselect, inst.settings));

                        selectCurrentWeek();
                        var txt = document.getElementById("ctl00_ContentPlaceHolder1_datepicker");
                        txt.value = $('#ctl00_ContentPlaceHolder1_datepicker').text();
                        __doPostBack('ctl00$ContentPlaceHolder1$btnGo', '');

                    },

                    beforeShowDay: function(date) {
                        var cssClass = '';

                        if (date >= startDate && date <= endDate)
                            cssClass = 'ui-datepicker-current-day';
                        return [true, cssClass];
                    },
                    onChangeMonthYear: function(year, month, inst) {
                        selectCurrentWeek();
                    }
                });

            }
            else {
                $("#Calendardate").datepicker({

                    showOtherMonths: true,
                    selectOtherMonths: true,
                    onSelect: function(dateText, inst) {
                        var date = $(this).datepicker('getDate');

                        Dateselect = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                        startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                        endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                        var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;

                        $('#ctl00_ContentPlaceHolder1_datepicker').text($.datepicker.formatDate(dateFormat, Dateselect, inst.settings));

                        selectCurrentWeek();
                        var txt = document.getElementById("ctl00_ContentPlaceHolder1_datepicker");
                        txt.value = $('#ctl00_ContentPlaceHolder1_datepicker').text();
                        __doPostBack('ctl00$ContentPlaceHolder1$btnGo', '');
                    },

                    beforeShowDay: function(date) {
                        var cssClass = '';
                        if (date >= startDate && date <= endDate)
                            cssClass = 'ui-datepicker-current-day';
                        return [true, cssClass];
                    },
                    onChangeMonthYear: function(year, month, inst) {
                        selectCurrentWeek();
                    }
                });
            }
            var selectCurrentWeek = function() {
                window.setTimeout(function() {
                    $('#Calendardate').find('.ui-datepicker-current-day a').addClass('ui-state-active')
                }, 1);
            }


            $('#Calendardate .ui-datepicker-calendar tr').live('mousemove', function() {
                $(this).find('td a').addClass('ui-state-hover');
            });
            $('#Calendardate .ui-datepicker-calendar tr').live('mouseleave', function() {
                $(this).find('td a').removeClass('ui-state-hover');
            });



        });
    </script>

    <script language="javascript" type="text/javascript">

        function EditRecord(id, weekpart) {
            if (weekpart == 1) {
                $("#ctl00_ContentPlaceHolder1_hidWeekPart").val(weekpart);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_hidWeekPart").val(0);
            }

            var txtdel = document.getElementById("ctl00_ContentPlaceHolder1_txtdel");
            txtdel.value = id;
            __doPostBack('ctl00$ContentPlaceHolder1$btnEditRecord', '');
        }


        function DeleteRecord(id) {
            //alert(id);
            var txtdel = document.getElementById("ctl00_ContentPlaceHolder1_txtdel");
            txtdel.value = id;
            __doPostBack('ctl00$ContentPlaceHolder1$btndel', '');

            //alert(day);
            //hs.addEventListener(window, 'load', function() {document.getElementById('autoloadDirectlogin').onclick();document.getElementById('lightboxDirectLogin').style.display='block'; });
            //document.getElementById('autoload').onclick();

        }

        function CopyValues(sptype, sp, day, weekpart) {
            if (weekpart == 1) {
                $("#ctl00_ContentPlaceHolder1_hidWeekPart").val(weekpart);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_hidWeekPart").val(0);
            }
            //alert(sp + " + " + day);
            //alert(day);
            //hs.addEventListener(window, 'load', function() {document.getElementById('autoloadDirectlogin').onclick();document.getElementById('lightboxDirectLogin').style.display='block'; });
            var spTypeid = document.getElementById("ctl00_ContentPlaceHolder1_txtSpecialityTypeID");
            var txtDay = document.getElementById("ctl00_ContentPlaceHolder1_txtDay");
            var txtsp = document.getElementById("ctl00_ContentPlaceHolder1_txtSp");
            spTypeid.value = sptype;
            txtDay.value = day;
            txtsp.value = sp;
            __doPostBack('ctl00$ContentPlaceHolder1$btnAdd', '');
            //document.getElementById('autoload').onclick();

        }
        function printreport() {
            
            var txtParam = document.getElementById("ctl00_ContentPlaceHolder1_txtParams");
            var a = window.open("../Managers/PrintworkShift.aspx" + txtParam.value, 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');

        }
        function pdfReport() {
            var txtParam = document.getElementById("ctl00_ContentPlaceHolder1_txtParams");
            var a = window.open("../Managers/PrintworkShift.aspx" + txtParam.value + "&r=ps", 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');

        }
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
    <asp:TextBox ID="txtMyDay" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:TextBox ID="txtCheck" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <div style="display: none;">
        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
        <asp:TextBox ID="txtSpecialityTypeID" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtDay" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtSp" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtdel" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtParams" runat="server"></asp:TextBox>
        <asp:Button ID="btndel" runat="server" Text="Add" OnClick="btndel_Click" />
        <asp:Button ID="btnEditRecord" runat="server" OnClick="btnEditRecord_Click" />
        <asp:HiddenField ID="hidWeekPart" runat="server" />
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
                <div style="margin: 0 auto; text-align: center;">
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text="Manage Workshifts"></asp:Label></div>
                <div class="clear_30">
                </div>
                <table cellpadding="1" cellspacing="1" border="0" align="center">
                    <tr>
                        <td colspan="3" style="text-align: center;">
                            <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Enter Date"></asp:Label>
                        </td>
                        <td>
                            <div class="demo textbox_date_new">
                                <%--<input id="datepicker" type="text" />--%>
                                <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="datepicker"
                                CssClass="Error" ErrorMessage="Date Required" ValidationGroup="load"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="3" style="height: 5px;">
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                        </td>
                        <td>
                            <%-- <asp:ImageButton ID="btnGO" runat="server" ImageUrl="~/Images/btn_load.png" OnClick="btnGO_Click"
                                ValidationGroup="load" />--%>
                            <asp:Button ID="btnGO" runat="server" OnClick="btnGO_Click" UseSubmitBehavior="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <div>
                                <div id="Calendardate">
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
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
    <div id="ShowWorkshift" runat="server" style="display: none;">
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
                    <td class="whitebox_centermiddlebg">
                        <div class="rounded_box_big">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td valign="top">
                                        <table border="0" cellspacing="2" cellpadding="2" style="width: 100%;">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:LinkButton ID="lnkChangeDefaultTime" runat="server" Style="text-decoration: underline;"
                                                        OnClick="lnkChangeDefaultTime_Click">Change Default Time</asp:LinkButton>
                                                </td>
                                                <td colspan="5" align="center" valign="top">
                                                    <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:Button ID="btnPDF" runat="server" Text="Pdf" OnClick="btnPDF_Click" Style="display: none;" />
                                                    
                                                </td>
                                                <td align="right" valign="top">
                                                    <input type="button" value="print" onclick="javascript:printreport();" />
                                                    <input type="button" value="Pdf" onclick="javascript:pdfReport();" />
                                                </td>
                                            </tr>
                                            <tr class="headerstylenew">
                                                <td>
                                                    <a href="../Managers/ManagePositions.aspx" style="text-decoration: underline;">Manage
                                                        Positions </a>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblSundayDate');" >
                                                    Sunday
                                                    <asp:LinkButton runat="server" ID="lblSundayDate" Text="" 
                                                        onclick="lblSundayDate_Click"></asp:LinkButton>
                                                    <%--<asp:Label ID="lblSundayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblMondayDate');" >
                                                    Monday
                                                    <asp:LinkButton runat="server" ID="lblMondayDate" Text="" 
                                                        onclick="lblMondayDate_Click"></asp:LinkButton>
                                                   <%-- <asp:Label ID="lblMondayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblTuesdayDate');" >
                                                    Tuesday
                                                    <asp:LinkButton runat="server" ID="lblTuesdayDate" Text="" 
                                                        onclick="lblTuesdayDate_Click"></asp:LinkButton>
                                                    <%--<asp:Label ID="lblTuesdayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblWednesdayDate');" >
                                                    Wednesday
                                                    <asp:LinkButton runat="server" ID="lblWednesdayDate" Text="" 
                                                        onclick="lblWednesdayDate_Click"></asp:LinkButton>
                                                    <%--<asp:Label ID="lblWednesdayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblThursdayDate');" >
                                                    Thursday
                                                    <asp:LinkButton runat="server" ID="lblThursdayDate" Text="" 
                                                        onclick="lblThursdayDate_Click"></asp:LinkButton>
                                                    <%--<asp:Label ID="lblThursdayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblFridayDate');" >
                                                    Friday
                                                    <asp:LinkButton runat="server" ID="lblFridayDate" Text="" 
                                                        onclick="lblFridayDate_Click"></asp:LinkButton>
                                                   <%-- <asp:Label ID="lblFridayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                                <td style="cursor:pointer;" class="Monday" onclick="javascript:lblMondayDateClick('lblSaturdayDate');" >
                                                    Saturday
                                                    <asp:LinkButton runat="server" ID="lblSaturdayDate" Text="" 
                                                        onclick="lblSaturdayDate_Click"></asp:LinkButton>
                                                    <%--<asp:Label ID="lblSaturdayDate" runat="server" Text=""></asp:Label>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="upnlWorkshift" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table id="tblWork" runat="server" style="width: 100%;">
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
        <%--Previous week off days--%>
        <div class="clear_10">
        </div>
        <div style="margin: 0 auto; width: 430px; text-align: center;">
            <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Staff DAYS OFF for previous week"></asp:Label>
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
                    <asp:DataList ID="DLPreviousOffdays" runat="server" RepeatDirection="Horizontal"
                        RepeatColumns="7" OnItemDataBound="DLPreviousOffdays_ItemDataBound" Width="100%">
                        <ItemTemplate>
                            <table style="background: #d6efff; width: 100%;" cellpadding="1" cellspacing="1"
                                border="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text="" CssClass="alternate_row_new"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
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
        <%--OffDay Grids--%>
        <div class="clear_10">
        </div>
        <div style="margin: 0 auto; width: 430px;">
            <asp:Label runat="server" Font-Bold="true" Text="Available Staff (that will take a DAY OFF if not selected to work)"></asp:Label>
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
                    <div class="rounded_box_big">
                        <table border="0" cellspacing="1" cellpadding="0" class="table_border" style="border-left: none 0;
                            border-top: none 0; border-bottom: none 0;">
                            <tr class="headerstylenew">
                                <%--<td style="background: #fff; border-right: solid 1px #d6efff;">
                                    &nbsp;
                                </td>--%>
                                <td>
                                    Sunday
                                    <asp:Label ID="lblSundayDate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    Monday
                                    <asp:Label ID="lblMondayDate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    Tuesday
                                    <asp:Label ID="lblTuesdayDate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    Wednesday
                                    <asp:Label ID="lblWednesdayDate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    Thursday
                                    <asp:Label ID="lblThursdayDate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    Friday
                                    <asp:Label ID="lblFridayDate1" runat="server" Text=""></asp:Label>
                                </td>
                                <td>
                                    Saturday
                                    <asp:Label ID="lblSaturdayDate1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <%-- <td style="border-right: solid 1px #d6efff;">
                                    &nbsp;
                                </td>--%>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdSunday" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdSunday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdMonday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdMonday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdTuesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdTuesday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdWednesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdWednesday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdThursday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdThursday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdFriday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdFriday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                                <td class="align_left" valign="top" style="border-bottom: solid 1px #d6efff;">
                                    <asp:GridView ID="grdSaturday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                        AlternatingRowStyle-CssClass="rowstyle_1" RowStyle-CssClass="alternate_row_1"
                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                        GridLines="None" OnRowDataBound="grdSaturday_RowDataBound">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FullName" />--%>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUser" runat="server" Style="float: left;"></asp:LinkButton>
                                                    <div id="mydiv" runat="server" style="float: right;">
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
    <%--Jquery Popup Code--%>
    <div class="small-lightbox-container" id="lightbox-s">
        <div class="lightbox-header">
            <h3>
                Work Shift</h3>
            <a id="small-lightbox-close" href="#" onclick="hs.close(this)">
                <img src="../images/lightbox-close.png" alt="stäng" title="Stäng" /></a>
        </div>
        <div class="small-lightbox-content" id="div_msg" runat="server">
            <%--<img id="icon-check" src="../images/icon-cross.png" />--%>
            <div>
                <%--Pop Up Html Start--%>
                <table cellpadding="3" cellspacing="3" border="0" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="lblUser" runat="server" Font-Bold="true" Text="Users"></asp:Label>
                        </td>
                        <td colspan="3">
                            <div class="textbox204">
                                <asp:DropDownList ID="ddlusers" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="clear_30" style="font-size: 15px;">
                            <asp:Label ID="lblWeekPartAdd" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="padding-right: 15px; text-align: center;">
                            <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td style="padding-right: 15px; text-align: center;">
                            <asp:Label ID="Label8" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlStartHour" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20px; text-align: center;">
                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text=":"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlStartMin" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEndHour" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20px; text-align: center;">
                            <asp:Label ID="Label5" runat="server" Font-Bold="true" Text=":"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEndMin" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; padding-right: 10px;">
                            <asp:Button ID="btnSave" UseSubmitBehavior="false" runat="server" Style="display: none;"
                                OnClick="btnSave_Click" />
                            <%--<img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$btnSave','');" />--%>
                            <img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:callme();" />
                            <%--Pop Up HTML end--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="clear_10">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <a id="autoload" onclick="hs.htmlExpand(this,{ contentId: 'lightbox-s' } )"></a>
    <%--Jquery popup for edit time--%>
    <div class="small-lightbox-container" id="Editlightbox-s">
        <div class="lightbox-header">
            <h3>
                Work Shift</h3>
            <a id="small-lightbox-close" href="#" onclick="hs.close(this)">
                <img src="../images/lightbox-close.png" alt="stäng" title="Stäng" /></a>
        </div>
        <div class="small-lightbox-content" id="div2" runat="server">
            <%--<img id="icon-check" src="../images/icon-cross.png" />--%>
            <div>
                <%--Pop Up Html Start--%>
                <table cellpadding="3" cellspacing="3" border="0" align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="User Name :"></asp:Label>
                        </td>
                        <td colspan="3">
                            <div class="textbox204">
                                <asp:Label ID="lblEditUser" runat="server"></asp:Label>
                                <asp:TextBox ID="lblID" runat="server" Style="display: none;"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="clear_30">
                            <asp:Label ID="lblWeekPartEdit" runat="server" ForeColor="Green"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="padding-right: 15px; text-align: center;">
                            <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td style="padding-right: 15px; text-align: center;">
                            <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlStartHourEdit" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20px; text-align: center;">
                            <asp:Label ID="Label14" runat="server" Font-Bold="true" Text=":"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlStartMinEdit" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEndHourEdit" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="04" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20px; text-align: center;">
                            <asp:Label ID="Label16" runat="server" Font-Bold="true" Text=":"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlEndMinEdit" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; padding-right: 10px;">
                            <asp:Button ID="btnEdit" UseSubmitBehavior="false" runat="server" Style="display: none;"
                                OnClick="btnEdit_Click" />
                            <%--<img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$btnSave','');" />--%>
                            <img id="Img1" src="../Images/btn_save.png" alt="" onclick="javascript:Editme();" />
                            <%--Pop Up HTML end--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="clear_10">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <a id="Editautoload" onclick="hs.htmlExpand(this,{ contentId: 'Editlightbox-s' } )">
    </a>
    <div class="small-lightbox-container" id="Div1">
        <div class="lightbox-header">
            <h3>
                Work Shift</h3>
            <a id="small-lightbox-close" href="#" onclick="hs.close(this)">
                <img src="../images/lightbox-close.png" alt="stäng" title="Stäng" /></a>
        </div>
        <div class="small-lightbox-content" id="div3" runat="server">
            <%--<img id="icon-check" src="../images/icon-cross.png" />--%>
            <div>
                <%--Pop Up Html Start--%>
                <table cellpadding="3" cellspacing="3" border="0" align="center">
                    
                    <tr>
                        <td colspan="4" class="clear_30">
                        <h3><asp:Label ID="Label31" runat="server" ForeColor="Green"></asp:Label></h3>

                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="padding-right: 15px; text-align: center;">
                            <asp:Label ID="Label32" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td style="padding-right: 15px; text-align: center;">
                            <asp:Label ID="Label33" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label34" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20px; text-align: center;">
                            <asp:Label ID="Label35" runat="server" Font-Bold="true" Text=":"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label36" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="04" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                    <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                    <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 20px; text-align: center;">
                            <asp:Label ID="Label37" runat="server" Font-Bold="true" Text=":"></asp:Label>
                            <asp:HiddenField runat="server" ID="hdnf" Value="" />
                        </td>
                        <td>
                            <div class="textbox_small">
                                <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; padding-right: 10px;">
                            <asp:Button ID="btnAddbtnEdit" UseSubmitBehavior="false" runat="server" Style="display: none;"
                                OnClick="btnAddbtnEdit_Click" />
                            <%--<img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$btnSave','');" />--%>
                            <img id="Img2" src="../Images/btn_save.png" alt="" onclick="javascript:EditmeNew();" />
                            <%--Pop Up HTML end--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="clear_10">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <a id="autoloadNew" onclick="hs.htmlExpand(this,{ contentId: 'Div1' } )">
    </a>
    <div style="display: none;">
        <asp:TextBox ID="txtsh" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtsm" runat="server"></asp:TextBox>
        <asp:TextBox ID="txteh" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtem" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtddl" runat="server"></asp:TextBox>
    </div>

    <script language="javascript" type="text/javascript">
     function EditmeNew() {
            var sh = document.getElementById("ctl00_ContentPlaceHolder1_txtsh");
            //var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_StartHour");
            var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList1");
            //sh.value = oldsh.value;
            sh.value = oldsh.options[oldsh.selectedIndex].text;

            var sm = document.getElementById("ctl00_ContentPlaceHolder1_txtsm");
            //var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_StartMin");
            var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList2");
            //sm.value = oldsm.value;
            sm.value = oldsm.options[oldsm.selectedIndex].text;

            var eh = document.getElementById("ctl00_ContentPlaceHolder1_txteh");
            //    var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_EndHour");
            //    eh.value = oldeh.value;
            var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList3");
            eh.value = oldeh.options[oldeh.selectedIndex].text;

            var em = document.getElementById("ctl00_ContentPlaceHolder1_txtem");
            //    var oldem = document.getElementById("ctl00_ContentPlaceHolder1_EndMin");
            //    em.value = oldem.value;
            var oldem = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList4");
            em.value = oldem.options[oldem.selectedIndex].text;

            var e = document.getElementById("ctl00_ContentPlaceHolder1_hdnf");
            var txtddl = document.getElementById("ctl00_ContentPlaceHolder1_txtddl");
            txtddl.value = e.value;
             
            __doPostBack('ctl00$ContentPlaceHolder1$btnAddbtnEdit', '');
        }
        function Editme() {
            var sh = document.getElementById("ctl00_ContentPlaceHolder1_txtsh");
            //var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_StartHour");
            var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartHourEdit");
            //sh.value = oldsh.value;
            sh.value = oldsh.options[oldsh.selectedIndex].text;

            var sm = document.getElementById("ctl00_ContentPlaceHolder1_txtsm");
            //var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_StartMin");
            var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartMinEdit");
            //sm.value = oldsm.value;
            sm.value = oldsm.options[oldsm.selectedIndex].text;

            var eh = document.getElementById("ctl00_ContentPlaceHolder1_txteh");
            //    var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_EndHour");
            //    eh.value = oldeh.value;
            var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndHourEdit");
            eh.value = oldeh.options[oldeh.selectedIndex].text;

            var em = document.getElementById("ctl00_ContentPlaceHolder1_txtem");
            //    var oldem = document.getElementById("ctl00_ContentPlaceHolder1_EndMin");
            //    em.value = oldem.value;
            var oldem = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMinEdit");
            em.value = oldem.options[oldem.selectedIndex].text;

            var e = document.getElementById("ctl00_ContentPlaceHolder1_lblID");
            var txtddl = document.getElementById("ctl00_ContentPlaceHolder1_txtddl");
            txtddl.value = e.value;

            __doPostBack('ctl00$ContentPlaceHolder1$btnEdit', '');
        }

        function callme() {
            var sh = document.getElementById("ctl00_ContentPlaceHolder1_txtsh");
            //var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_StartHour");
            var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartHour");
            //sh.value = oldsh.value;
            sh.value = oldsh.options[oldsh.selectedIndex].text;

            var sm = document.getElementById("ctl00_ContentPlaceHolder1_txtsm");
            //var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_StartMin");
            var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartMin");
            //sm.value = oldsm.value;
            sm.value = oldsm.options[oldsm.selectedIndex].text;

            var eh = document.getElementById("ctl00_ContentPlaceHolder1_txteh");
            //    var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_EndHour");
            //    eh.value = oldeh.value;
            var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndHour");
            eh.value = oldeh.options[oldeh.selectedIndex].text;

            var em = document.getElementById("ctl00_ContentPlaceHolder1_txtem");
            //    var oldem = document.getElementById("ctl00_ContentPlaceHolder1_EndMin");
            //    em.value = oldem.value;
            var oldem = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMin");
            em.value = oldem.options[oldem.selectedIndex].text;

            var e = document.getElementById("ctl00_ContentPlaceHolder1_ddlusers");
            var txtddl = document.getElementById("ctl00_ContentPlaceHolder1_txtddl");
            txtddl.value = e.options[e.selectedIndex].value;

            __doPostBack('ctl00$ContentPlaceHolder1$btnSave', '');
        }

        function DefaultTime() {
            alert('a');
            __doPostBack('ctl00$ContentPlaceHolder1$Button1', '');
        }
    
    </script>

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
                    <table cellpadding="3" cellspacing="3" border="0" align="center">
                        <%--<tr>
                            <td colspan="4">
                                Change Default Time
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="4" align="left" style="color: Green;">
                                <h3>
                                    Week days Default Time:</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="padding-right: 15px; text-align: center;">
                                <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td style="padding-right: 15px; text-align: center;">
                                <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlStartHoursChangeWeek" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 20px; text-align: center;">
                                <asp:Label ID="Label21" runat="server" Font-Bold="true" Text=":"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlStartMinutesChangeWeek" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEndHoursChangeWeek" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 20px; text-align: center;">
                                <asp:Label ID="Label23" runat="server" Font-Bold="true" Text=":"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEndMinutesChangeWeek" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left" style="color: Green;">
                                <h3>
                                    Weekend Default Time:</h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="padding-right: 15px; text-align: center;">
                                <asp:Label ID="Label17" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                            </td>
                            <td>
                            </td>
                            <td style="padding-right: 15px; text-align: center;">
                                <asp:Label ID="Label24" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label25" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlStartHoursChangeWeekend" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 20px; text-align: center;">
                                <asp:Label ID="Label26" runat="server" Font-Bold="true" Text=":"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlStartMinutesChangeWeekend" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label27" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEndHoursChangeWeekend" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td style="width: 20px; text-align: center;">
                                <asp:Label ID="Label28" runat="server" Font-Bold="true" Text=":"></asp:Label>
                            </td>
                            <td>
                                <div class="textbox_small">
                                    <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEndMinutesChangeWeekend" runat="server" AutoPostBack="false">
                                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; padding-right: 10px;">
                                <asp:Button ID="Button1" UseSubmitBehavior="false" runat="server" Style="display: none;"
                                    OnClick="btnSaveDefault_Click" />
                                <%--<img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$btnSave','');" />--%>
                                <%--<img id="Img2" src="../Images/btn_save.png" alt="" onclick="javascript:callme();" />--%>
                                <asp:ImageButton ID="imgBtnSaveDefautTime" runat="server" ImageUrl="~/Images/btn_save.png"
                                    AlternateText="" OnClick="imgBtnSaveDefautTime_Click" />
                                <%--Pop Up HTML end--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="clear_10">
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="Button2" runat="server" Style="display: none;" />
    <asp:Panel ID="Panel1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" align="center">
                        <tr>
                            <td style="text-align: center;">
                                Sorry, deleting any staff on workshift currently not be possible.
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <img id="img" src="../Images/btn_ok.png" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;" />
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
