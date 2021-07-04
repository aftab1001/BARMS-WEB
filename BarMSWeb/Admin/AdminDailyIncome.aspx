<%@ Page Language="C#" MasterPageFile="~/MasterPages/AdminMaster.master" AutoEventWireup="true" CodeFile="AdminDailyIncome.aspx.cs" Inherits="Admin_AdminDailyIncome" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.price_format.1.7.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function() {
            //var height = document.body.offsetHeight;
            //var width = document.body.offsetWidth;
            var height = document.body.clientWidth;
            var width = document.body.clientHeight;


            //var change = 0;  // show/hide popup dialog box for save changing for daily icome

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
            $("#ctl00_ContentPlaceHolder1_imgAddSpecialIncome").click(function() {
                $("#ctl00_ContentPlaceHolder1_trOtherIncome").show();
                return true;
            });

            $('.filter').change(function() {
                $("#ctl00_ContentPlaceHolder1_hidChange").val(1);
            });
            $('.note').change(function() {
                $("#ctl00_ContentPlaceHolder1_hidChange").val(1);
            });

            var startDate;
            var endDate;
            var Dateselect;

            var myDay = document.getElementById("ctl00_ContentPlaceHolder1_txtDay");
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
                        // __doPostBack('ctl00$ContentPlaceHolder1$upnlGo', '');
                        //SaveHelp();
                    },

                    beforeShowDay: function(date) {
                        var cssClass = '';

                        if (date >= startDate && date <= endDate)
                            cssClass = 'ui-datepicker-current-day';
                        //cssClass = '';
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
                        // __doPostBack('ctl00$ContentPlaceHolder1$upnlGo', '');
                        //SaveHelp();
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



            $('.filter').watermark('00,00');
            $('#ctl00_ContentPlaceHolder1_txtIncomeAmount').watermark('00,00');
            ApplyJquery();



        });

        function ApplyJquery() {

            $('.tab1').click(function() {
                $('#ctl00_ContentPlaceHolder1_trDailyIncomeMessage').hide();
                $('#ctl00_ContentPlaceHolder1_trMessageOtherIncome').hide();
                $('#ctl00_ContentPlaceHolder1_lblMessageRegister').hide();
                if ($("#ctl00_ContentPlaceHolder1_hidChange").val() == "1") {
                    $('#divConfirm').show();
                    return false;
                }
                $('#ctl00_ContentPlaceHolder1_divTab1').show();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $('#ctl00_ContentPlaceHolder1_divTab5').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
                $('.tab5').css("background-color", "#e8e8e8");
            });


            $('.tab2').click(function() {
                $('#ctl00_ContentPlaceHolder1_trDailyIncomeMessage').hide();
                $('#ctl00_ContentPlaceHolder1_trMessageOtherIncome').hide();
                $('#ctl00_ContentPlaceHolder1_lblMessageRegister').hide();
                if ($("#ctl00_ContentPlaceHolder1_hidChange").val() == "1") {
                    $('#divConfirm').show();
                    return false;
                }
                $('#ctl00_ContentPlaceHolder1_divTab2').show();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $('#ctl00_ContentPlaceHolder1_divTab5').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
                $('.tab5').css("background-color", "#e8e8e8");
            });

            $('.tab3').click(function() {
                $('#ctl00_ContentPlaceHolder1_trDailyIncomeMessage').hide();
                $('#ctl00_ContentPlaceHolder1_trMessageOtherIncome').hide();
                $('#ctl00_ContentPlaceHolder1_lblMessageRegister').hide();
                if ($("#ctl00_ContentPlaceHolder1_hidChange").val() == "1") {
                    $('#divConfirm').show();
                    return false;
                }
                $('#ctl00_ContentPlaceHolder1_divTab3').show();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $('#ctl00_ContentPlaceHolder1_divTab5').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
                $('.tab5').css("background-color", "#e8e8e8");
            });

            $('.tab4').click(function() {
                $('#ctl00_ContentPlaceHolder1_trDailyIncomeMessage').hide();
                $('#ctl00_ContentPlaceHolder1_trMessageOtherIncome').hide();
                $('#ctl00_ContentPlaceHolder1_lblMessageRegister').hide();
                if ($("#ctl00_ContentPlaceHolder1_hidChange").val() == "1") {
                    $('#divConfirm').show();
                    return false;
                }
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab5').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').show();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab5').css("background-color", "#e8e8e8");
            });
            $('.tab5').click(function() {
                $('#ctl00_ContentPlaceHolder1_trDailyIncomeMessage').hide();
                $('#ctl00_ContentPlaceHolder1_trMessageOtherIncome').hide();
                $('#ctl00_ContentPlaceHolder1_lblMessageRegister').hide();
                if ($("#ctl00_ContentPlaceHolder1_hidChange").val() == "1") {
                    $('#divConfirm').show();
                    return false;
                }
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $('#ctl00_ContentPlaceHolder1_divTab5').show();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });


            $('#ctl00$ContentPlaceHolder1$GrdUsers$ctl02$txtIncome').priceFormat({
                prefix: '',
                suffix: ''
            });
            $("#ctl00_ContentPlaceHolder1_imgBtnSaveChanges").click(function() {
                $("#ctl00_ContentPlaceHolder1_hidChange").val(0);
                $('#divConfirm').hide();
                $('#divBackground').css('display', 'none').hide();

            });
            $("#ctl00_ContentPlaceHolder1_imgBtnSaveTop").click(function() {
                $('#divConfirm').hide();
                $('#divBackground').css('display', 'none').hide();
            });
            $("#ctl00_ContentPlaceHolder1_imgCancelChanges").click(function() {
                $("#ctl00_ContentPlaceHolder1_hidChange").val(0);
                $('#divConfirm').hide();
                $('#divBackground').css('display', 'none').hide();
            });
            $('.filter').watermark('00,00');
            $('#ctl00_ContentPlaceHolder1_txtIncomeAmount').watermark('00,00');

        }
        function CorrectValue(obj) {
            $('#' + obj.id).bind('keyup blur', function() {
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

        function changeVal() {

            $("#ctl00_ContentPlaceHolder1_hidChange").val(0);

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
        function SaveHelp() {
            var ParamValues = 1;
            PageMethods.HelpSaved(ParamValues, HelpSaved, FailSave);
        }
        function HelpSaved(result) {

        }
        function FailSave(result) {

        }



        var num = 0;
        var commonid = "ctl00_ContentPlaceHolder1_GrdUsers_ctl0";

        var lastValue = 0;
        var tipLastValue = 0;
        var tipYellow = 0;
        var controlid;

        function Calculate(objtxt) {
            var USValue_income = 0;
            var USValue_netIncome = 0;

            $("#ctl00_ContentPlaceHolder1_hidChange").val(1);
            var rownum = (objtxt.id).substr(39, 1);

            var income = document.getElementById(commonid + rownum + "_" + "txtIncome");
            var netIncome = document.getElementById(commonid + rownum + "_" + "txtNetIncome");
            //var lblTotalofDay = document.getElementById("ctl00_ContentPlaceHolder1_lblTotalofDay");
            var divNetIncome = document.getElementById(commonid + rownum + "_" + "divNetIncome");
            var divTip = document.getElementById(commonid + rownum + "_" + "divTip");

            USValue_income = getNum(income.value);
            income.value = ChangeToUK(getNum2(USValue_income).toFixed(2));
            //lblTotalofDay.innerHTML = ChangeToUK(getNum2(USValue_income).toFixed(2));


            USValue_netIncome = getNum(netIncome.value);
            netIncome.value = ChangeToUK(getNum2(USValue_netIncome).toFixed(2));

            var hidTip = document.getElementById(commonid + rownum + "_" + "hidTipValue");

            var one = 1;
            if (one == 1) {

                var tip = document.getElementById(commonid + rownum + "_" + "txtTip");

                var grid = document.getElementById("<%= GrdUsers.ClientID %>");

                if (grid.rows.length > 0) {
                    for (i = rownum; i <= grid.rows.length; i++) {

                        var incomeTotal = document.getElementById(commonid + i + "_" + "lblIncomeSubtotal");
                        var netIncomeTotal = document.getElementById(commonid + i + "_" + "lblNetIncomeSubtotal");
                        var tipTotal = document.getElementById(commonid + i + "_" + "lblTipSubtotal");

                        if (incomeTotal != null) {

                            if (controlid == "txtIncome") {

                                var USValue_incomeT = 0;
                                USValue_incomeT = getNum(incomeTotal.innerHTML);

                                //var incomeT = getNum(incomeTotal.innerHTML);
                                USValue_incomeT = USValue_incomeT - lastValue;
                                var result = getNum2(USValue_incomeT) + getNum2(USValue_income);

                                if (getNum2(result) == 0) {
                                    incomeTotal.innerHTML = "00,00";
                                }
                                else {

                                    incomeTotal.innerHTML = ChangeToUK(getNum2(result).toFixed(2));
                                }
                                netIncome.value = ChangeToUK(getNum2(USValue_income).toFixed(2));
                                num = getNum(income.value) - getNum(netIncome.value);

                                var USValue_netIncomeT = 0;
                                USValue_netIncomeT = getNum(netIncomeTotal.innerHTML);
                                //var netIncomeT = getNum(netIncomeTotal.innerHTML);
                                USValue_netIncomeT = USValue_netIncomeT - USValue_netIncome;
                                var result2 = getNum2(USValue_netIncomeT) + getNum(netIncome.value);
                                if (getNum2(result2) == 0) {
                                    netIncomeTotal.innerHTML = "00,00";
                                }
                                else {
                                    netIncomeTotal.innerHTML = ChangeToUK(getNum2(result2).toFixed(2));
                                }
                                tipYellow = 0;
                            }
                            else if (controlid == "txtNetIncome") {

                                var USValue_netIncomeT = 0;
                                USValue_netIncomeT = getNum(netIncomeTotal.innerHTML);
                                //var netIncomeT = getNum(netIncomeTotal.innerHTML);
                                USValue_netIncomeT = USValue_netIncomeT - lastValue;
                                var result = getNum2(USValue_netIncomeT) + getNum2(USValue_netIncome);
                                if (getNum2(result) == 0) {
                                    netIncomeTotal.innerHTML = "00,00";
                                }
                                else {
                                    netIncomeTotal.innerHTML = ChangeToUK(getNum2(result).toFixed(2));
                                }
                                num = getNum(income.value) - getNum(netIncome.value);
                                tipYellow = 0;
                            }
                            else if (controlid == "txtTip") {

                                var checkTipValue = getNum(tip.value);
                                tip.value = ChangeToUK(getNum2(checkTipValue).toFixed(2));
                                if (checkTipValue > 50) {
                                    tipYellow = 1;
                                }
                                else {
                                    tipYellow = 2;
                                }
                            }
                            var resultTipIncome = 0;
                            if (tipYellow == 0) {
                                //var resultTipIncome = getNum(tipTotal.innerHTML) - tipLastValue;
                                resultTipIncome = getNum(tipTotal.innerHTML) - getNum(tip.value);
                                resultTipIncome = resultTipIncome + getNum2(num);
                            }
                            else {
                                resultTipIncome = getNum(tipTotal.innerHTML) - lastValue;
                                resultTipIncome = resultTipIncome + getNum2(tip.value);
                            }
                            if (getNum2(resultTipIncome) == 0) {
                                tipTotal.innerHTML = "";
                            }
                            else {
                                tipTotal.innerHTML = ChangeToUK(getNum2(resultTipIncome).toFixed(2));
                            }

                            break;
                        }
                    }
                }

                if (tipYellow == 0) {
                    if (num > 50) {
                        divNetIncome.setAttribute("class", "textbox_small_yellow");
                        divTip.setAttribute("class", "textbox_small_yellow");
                    }
                    else {
                        divNetIncome.setAttribute("class", "textbox_small_new");
                        divTip.setAttribute("class", "textbox_small_new");
                    }
                    hidTip.value = num.toFixed(2);
                    tip.value = ChangeToUK(num.toFixed(2));
                    //hidTip.value = tipLastValue;
                }
                else if (tipYellow == 1) {
                    divTip.setAttribute("class", "textbox_small_yellow");
                }
                else {
                    divTip.setAttribute("class", "textbox_small_new");
                    divNetIncome.setAttribute("class", "textbox_small_new");
                }
            }
            var userCount = document.getElementById("ctl00_ContentPlaceHolder1_hdnUserCount").value;
            //alert(userCount);
            var TotalSum = 0.00;
            var TotalCount = parseInt(userCount) + 1;
            //alert(TotalCount);
            for (icount = 2; icount <= TotalCount; icount++) {
                var strID = "ctl00_ContentPlaceHolder1_GrdUsers_ctl0" + icount + "_txtIncome";
                var RowSum = document.getElementById(strID).value;
                if (RowSum == '') {
                    RowSum = "00,00";
                }
                //alert(parseFloat("104,0"));
                // var getUSString = getNum(RowSum);
                var sValue = getNum(RowSum);
                //alert(RowSum + " & " + sValue);
                TotalSum = parseFloat(TotalSum) + parseFloat(sValue);
            }
            //var totalAmount = document.getElementById("ctl00_ContentPlaceHolder1_lblTotalofDay");
            //alert(ChangeToUK(getNum2(TotalSum).toFixed(2)));
            //TotalSum = TotalSum + ",00";
            //alert(TotalSum);
            //var getNumberFromString = getNum(TotalSum);
            document.getElementById('ctl00_ContentPlaceHolder1_lblTotalofDay').innerHTML = ChangeToUK(getNum2(TotalSum).toFixed(2)).toString() + " €";
            //var FinalTotalValue = document.getElementById("ctl00_ContentPlaceHolder1_lblTotalofDay").innerHTML;
            //alert(FinalTotalValue);
            //FinalTotalValue.innerHTML = TotalSum;  //+ " €";
            CheckPercentageValue(rownum, income);
        }
        function getLastValue(objtxt) {
            var val = objtxt.value;
            if (objtxt.value == '') {
                val = "0";
            }

            lastValue = getNum(val);
            controlid = objtxt.id.substr(41);
        }
        function getNum(value) {
            if (value == '') {
                value = "0";
            }
            value = ChangeToUS(value);
            value = parseFloat(value, 10);
            value = roundNumber(value, 2);
            //value = parseFloat(value.toFixed(2),10);
            return value;
        }
        function getNum2(value) {
            if (value == '') {
                value = "0";
            }

            value = parseFloat(value, 10);
            value = roundNumber(value, 2);
            return value;
        }
        function roundNumber(num, dec) {
            var result = Math.round(num * Math.pow(10, dec)) / Math.pow(10, dec);
            return result;
        }
        function CheckPercentageValue(rownum, income) {

            var h_Percentage = document.getElementById(commonid + rownum + "_" + "hidPercentage");
            var h_PercentageOver = document.getElementById(commonid + rownum + "_" + "hidPercentageOver");
            var h_MinimumPerDay = document.getElementById(commonid + rownum + "_" + "hidMinimumPerDay");
            var h_PercentageValue = document.getElementById(commonid + rownum + "_" + "hidPercentageValue");
            var res;
            if (h_Percentage.value != "0" && h_PercentageOver.value != "0" && h_PercentageOver.value != "0") {

                var salary = document.getElementById(commonid + rownum + "_" + "lblAgreedSalary");

                if (income.value != "") {
                    if (getNum2(income.value) > getNum2(h_PercentageOver.value)) {
                        res = (getNum2(h_Percentage.value) * getNum2(income.value)) / 100;
                        if (res.toString().indexOf('.') != -1) {
                            if (res.toString().substr(res.toString().indexOf('.') + 1, 1) < 6) {
                                res = res.toString().substr(0, res.toString().indexOf('.'));
                                res = res + ".00";
                            }
                            else {
                                res = Math.round(res);
                                res = res + ".00";
                            }
                        }
                        else {
                            res = res + ".00";
                        }
                    }
                    else {
                        res = h_MinimumPerDay.value + ".00";
                    }
                }
                else {
                    res = h_MinimumPerDay.value + ".00";
                }
                salary.innerHTML = ChangeToUK(getNum2(res).toFixed(2)) + " €";
            }
        }
        function fixedDecimalPlace(obj) {
            $("#ctl00_ContentPlaceHolder1_hidChange").val(1);
            var control = document.getElementById(obj.id);
            if (control.value != "") {
                if (control.value == "0") {
                    control.value = "";
                }
                else {
                    control.value = ChangeToUK(getNum(control.value).toFixed(2));
                }
            }
        }
        function fixedDecimalPlace2(obj) {
            var control = document.getElementById(obj.id);
            if (control.value != "") {
                if (control.value == "0") {
                    control.value = "";
                }
                else {
                    control.value = ChangeToUK(getNum(control.value).toFixed(2));
                }
            }
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
            //alert(retValue);
            retValue = retValue.substr(0, retValue.lastIndexOf('.')) + ',';
            retValue += AfterComma;
            return retValue;
        }
        
    </script>

    <asp:HiddenField ID="hidChange" runat="server" Value="0" />
    <div id="divBackground" style="width: 100%; height: 100%; background-color: Black;
        opacity: .5; position: absolute; left: 0; top: 0; bottom: 0; right: 0; display: none;">
    </div>
    <asp:TextBox ID="txtDay" runat="server" Style="display: none;" Text="-1"></asp:TextBox>
    <asp:HiddenField ID="hdnUserCount" runat="server" />
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
    <asp:TextBox ID="txtCheck" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
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
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text="Manage Daily Income"></asp:Label></div>
                <div class="clear_30">
                </div>
                <asp:UpdatePanel ID="upnlNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <ContentTemplate>
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
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td>
                                    <div class="demo textbox_date_new">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
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
    <div>
        <table width="100%">
            <tr>
                <td colspan="7" valign="top">
                    <div class="previousDay" style="display: none;">
                        <img src="../images/back_arrow.png" alt="" />
                    </div>
                    <div style="float: left; width: 880px; text-align: center; margin-top: 10px;" class="bold_label">
                        <%--<asp:UpdatePanel ID="upnlDate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                    <div class="nextDay" style="display: none;">
                        <img src="../images/next_arrow.png" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
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
                                                        <div style="background-color: Transparent; width: 860px;">
                                                            <div style="background-color: Transparent; width: 860px; float: left;">
                                                                <div style="background-color: #BFB8BB; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab1">
                                                                    Daily Reference</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab2">
                                                                    Daily Input</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab3">
                                                                    Other Income</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab4">
                                                                    Expenses</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab5">
                                                                    Registers</div>
                                                            </div>
                                                            <div style="background-color: Transparent; width: 860px; float: left;">
                                                                <div id="divTab1" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: block; padding-left: 10px;">
                                                                    <%--<asp:UpdatePanel ID="upnlGo" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>--%>
                                                                    <asp:UpdatePanel ID="upnlTotal" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table width="38%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <h3>
                                                                                            Income
                                                                                        </h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView ID="grdIncome" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                            EmptyDataText="Sorry! This day has not any income detail" GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" ShowHeader="true"
                                                                                            OnRowDataBound="grdIncome_RowDataBound">
                                                                                            <Columns>
                                                                                                <%--<asp:BoundField DataField="sSpecialityName" />--%>
                                                                                                <%--<asp:BoundField DataField="income" />--%>
                                                                                                <asp:TemplateField>
                                                                                                    <HeaderTemplate>
                                                                                                        <table width="100%">
                                                                                                            <tr>
                                                                                                                <td style="width: 175px;">
                                                                                                                    Position
                                                                                                                </td>
                                                                                                                <td style="width: 100px;">
                                                                                                                    Income
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td style="width: 175px;">
                                                                                                                    <asp:Label ID="lblSpeciality" runat="server" Text="<%#Bind(''sSpeciality') %>"></asp:Label>
                                                                                                                </td>
                                                                                                                <td style="width: 100px;">
                                                                                                                    <div id="divIncomeDailyReference" runat="server" class="textbox_small" style="float: left;">
                                                                                                                        <asp:TextBox ID="txtIncomeDailyReference" runat="server" CssClass="filter" Text="<%#Bind('fIncome') %>"
                                                                                                                            ReadOnly="true" Style="text-align: center; float: left;"></asp:TextBox>
                                                                                                                        <span style="position: relative; line-height: 31px;">€</span>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trSepratorDailyReference" runat="server" visible="false" style="background-color: Transparent;">
                                                                                                                <td id="tdSeparatorDailyReference" runat="server" colspan="2" style="height: 2px;
                                                                                                                    background-color: #999999; border: 0px;">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr id="trSubtotalDailtyReference" runat="server" visible="false" style="height: 97px;
                                                                                                                background-color: #e8e8e8;">
                                                                                                                <td>
                                                                                                                </td>
                                                                                                                <td valign="top">
                                                                                                                    <div style="float: right;">
                                                                                                                        <span style="font-weight: bold;">Subtotal</span>
                                                                                                                        <div id="divIncomeSubtotalDailyReference" runat="server" class="daily_income-subtotal"
                                                                                                                            style="float: left;">
                                                                                                                            <asp:Label ID="lblIncomeSubtotalDailtyReference" runat="server" Text="0.0"></asp:Label>
                                                                                                                        </div>
                                                                                                                        <span style="position: relative; float: left; left: 5px; line-height: 31px;">€</span>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </ItemTemplate>
                                                                                                    <%--<asp:Label ID="lblIncome" runat="server" Text="<%#Bind('income') %>"></asp:Label>--%>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <h3>
                                                                                            Other Income:
                                                                                        </h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <div id="divOtherIncomeTotalTab1" runat="server" style="float: left; width: 100px;
                                                                                            margin-left: 197px;">
                                                                                            <span style="font-weight: bold; float: left;">Subtotal</span>
                                                                                            <div class="daily_income-subtotal" style="background-color: #F5B9A2; float: left;">
                                                                                                <asp:Label ID="lblIncomeOther" runat="server" Text="0.0"></asp:Label>
                                                                                            </div>
                                                                                            <span style="position: relative; float: left; left: 5px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                        <asp:Label ID="lblOtherIncomeMessageTab1" runat="server" Text="Sorry! This day has not any other income detail "
                                                                                            Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <h3>
                                                                                            Expense
                                                                                        </h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="divExpenseTotalTab1" runat="server" style="float: left; width: 100px; margin-left: 197px;
                                                                                            display: block;">
                                                                                            <span style="font-weight: bold; float: left;">Subtotal</span>
                                                                                            <div class="daily_income-subtotal" style="background-color: #D2D5D6; float: left;">
                                                                                                <asp:Label ID="lblIExpense" runat="server" Text="0.0"></asp:Label>
                                                                                            </div>
                                                                                            <span style="position: relative; float: left; left: 5px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                        <asp:Label ID="lblExpenseMessageTab1" runat="server" Text="Sorry! This day has not any expense detail."
                                                                                            Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <h3>
                                                                                            Advance
                                                                                        </h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="divAdvanceTotalTab1" runat="server" style="float: left; width: 100px; margin-left: 197px;
                                                                                            display: block;">
                                                                                            <span style="font-weight: bold; float: left;">Subtotal</span>
                                                                                            <div class="daily_income-subtotal" style="background-color: #CCD609; float: left;">
                                                                                                <asp:Label ID="lblAdvance" runat="server" Text="0.0"></asp:Label>
                                                                                            </div>
                                                                                            <span style="position: relative; float: left; left: 5px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                        <asp:Label ID="lblAdvanceMessageTab1" runat="server" Text="Sorry! This day has not any advance detail."
                                                                                            Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <h3>
                                                                                            Register
                                                                                        </h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <div id="divRegisterTotalTab1" runat="server" style="float: left; width: 100px; margin-left: 197px;
                                                                                            display: block;">
                                                                                            <span style="font-weight: bold; float: left;">Subtotal</span>
                                                                                            <div class="daily_income-subtotal" style="background-color: #CCD609; float: left;">
                                                                                                <asp:Label ID="lblRegister" runat="server" Text="0.0"></asp:Label>
                                                                                            </div>
                                                                                            <span style="position: relative; float: left; left: 5px; line-height: 31px;">€</span>
                                                                                        </div>
                                                                                        <asp:Label ID="lblRegisterMessageTab1" runat="server" Text="Sorry! This day has not any Register Value."
                                                                                            Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="height: 50px;" id="tdTotal" runat="server" visible="false">
                                                                                        <div style="background-color: Aqua; height: 39px; line-height: 36px; width: 160px;
                                                                                            text-align: center; vertical-align: middle; margin-left: 140px;">
                                                                                            <span style="font-size: 15px; font-weight: bold;">
                                                                                                <asp:Label ID="lblTotal" runat="server" Text="Total : 0.00 €"></asp:Label></span>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <%-- </ContentTemplate>
                                                                    </asp:UpdatePanel>--%>
                                                                </div>
                                                                <div id="divTab2" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; padding-top: 10px; display: none;">
                                                                    <div id="divProgress" runat="server" style="position: absolute; left: 50%; top: 0px;
                                                                        display: none;">
                                                                        <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upnlDailyIncome"
                                                                            DynamicLayout="true">
                                                                            <ProgressTemplate>
                                                                                <img src="../Images/wloading.gif" />
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="upnlDailyIncome" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlUsers" runat="server" DefaultButton="imgBtnSaveTop" OnDataBinding="pnlUsers_DataBinding"
                                                                                onFocus="javascript:changeVal();">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr id="trDailyIncomeMessage" runat="server" visible="false">
                                                                                        <td align="center">
                                                                                            <div style="color: Green; font-size: 15px; font-weight: bold; border: 2px solid #ccc;
                                                                                                width: 275px; padding-top: 5px; padding-bottom: 5px; position: relative; top: 10px;">
                                                                                                <asp:Label ID="lblDailyIncomeMessage" runat="server"></asp:Label>
                                                                                            </div>
                                                                                            <br />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <div>
                                                                                                <div style="float: left">
                                                                                                </div>
                                                                                                <div style="float: right; padding-bottom: 10px;">
                                                                                                    <asp:ImageButton ID="imgBtnSaveTop" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                                                                                        OnClientClick="javascript:changeVal();" OnClick="imgBtnSaveTop_Click" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <div id="divUsers" runat="server" style="width: 854px;">
                                                                                                <asp:GridView ID="GrdUsers" runat="server" AutoGenerateColumns="false" Width="114%"
                                                                                                    EmptyDataText="Sorry! There is no any detail by daily income." GridLines="None"
                                                                                                    HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                    RowStyle-CssClass="rowstyle" OnRowDataBound="GrdUsers_RowDataBound" OnRowCommand="GrdUsers_RowCommand">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField>
                                                                                                            <HeaderTemplate>
                                                                                                                <table cellpadding="1" cellspacing="0" align="left">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 50px;">
                                                                                                                            Abbrv.
                                                                                                                        </td>
                                                                                                                        <td style="width: 90px;">
                                                                                                                            Name
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Income
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Net Income
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Salary
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Tip
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Advances
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Bonus
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            Penalty
                                                                                                                        </td>
                                                                                                                        <td style="">
                                                                                                                            Notes
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </HeaderTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <table cellpadding="0" cellspacing="0" align="left">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 50px;">
                                                                                                                            <asp:Label ID="lblpkUserID" runat="server" Visible="false"></asp:Label>
                                                                                                                            <asp:Label ID="lblpkUserWorkshiftID" runat="server" Visible="false"></asp:Label>
                                                                                                                            <asp:Label ID="lblSpeciality" runat="server"></asp:Label>
                                                                                                                            <asp:HiddenField ID="hidWorkshipid" runat="server" Value="<%#Bind('pkUserWorkshiftID') %>" />
                                                                                                                            <asp:HiddenField ID="hidAdvanceid" runat="server" Value="<%#Bind('pkUserAdvanceID') %>" />
                                                                                                                            <asp:HiddenField ID="hidIncomeid" runat="server" Value="<%#Bind('pkIncomID') %>" />
                                                                                                                            <asp:HiddenField ID="hidPercentage" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hidPercentageOver" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hidMinimumPerDay" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hidPercentageValue" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hidTipValue" runat="server" Value="" />
                                                                                                                        </td>
                                                                                                                        <td style="width: 90px; text-align: left;">
                                                                                                                            <asp:Label ID="lblFullName" runat="server" Style="float: left;"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <div id="divIncome" runat="server" class="textbox_small_new" style="float: left;">
                                                                                                                                <asp:TextBox ID="txtIncome" runat="server" CssClass="filter" Text="<%#Bind('fIncome') %>"
                                                                                                                                    Style="text-align: center;"></asp:TextBox>
                                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <div id="divNetIncome" runat="server" class="textbox_small_new" style="float: left;
                                                                                                                                margin-left: 2px;">
                                                                                                                                <asp:TextBox ID="txtNetIncome" runat="server" Text="<%#Bind('netIncome') %>" CssClass="filter"
                                                                                                                                    Style="text-align: center;"></asp:TextBox>
                                                                                                                                <%--<asp:Label ID="lblNetIncome" runat="server" CssClass="filter" Style="line-height: 35px;
                                                                                                                            text-align: left; display: none;"></asp:Label>--%>
                                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <asp:Label ID="lblAgreedSalary" runat="server" Text="" Style="text-align: center;
                                                                                                                                margin-left: 10px;"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <div id="divTip" runat="server" class="textbox_small_new" style="float: left;">
                                                                                                                                <asp:TextBox ID="txtTip" runat="server" Text="<%#Bind('userTip') %>" Style="text-align: center;"></asp:TextBox>
                                                                                                                                <asp:Label ID="lblTip" runat="server" Text="<%#Bind('userTip') %>" Style="text-align: center;"
                                                                                                                                    Visible="false"></asp:Label>
                                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <div class="textbox_small_new" style="float: left;">
                                                                                                                                <asp:TextBox ID="txtAdvances" runat="server" Text="<%#Bind('uAdvance') %>" CssClass="filter"
                                                                                                                                    onchange="javascript:fixedDecimalPlace(this);" Style="text-align: center;"></asp:TextBox>
                                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <div class="textbox_small_new" style="float: left;">
                                                                                                                                <asp:TextBox ID="txtBonus" Text="<%#Bind('Bonus') %>" CssClass="filter" runat="server"
                                                                                                                                    onchange="javascript:fixedDecimalPlace(this);" Style="text-align: center;"></asp:TextBox>
                                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td style="width: 65px;">
                                                                                                                            <div class="textbox_small_new" style="text-align: center;">
                                                                                                                                <asp:TextBox ID="txtPenalty" Text="<%#Bind('Penalty') %>" CssClass="filter" runat="server"
                                                                                                                                    onchange="javascript:fixedDecimalPlace(this);" Style="text-align: center;"></asp:TextBox>
                                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td style="">
                                                                                                                            <div class="textbox330">
                                                                                                                                <asp:TextBox ID="txtNotes" Text="<%#Bind('sNotes') %>" runat="server" CssClass="note"></asp:TextBox>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr id="trSeprator" runat="server" visible="false" style="background-color: Transparent;">
                                                                                                                        <td id="tdSeparator" runat="server" colspan="10" style="height: 2px; background-color: #999999;
                                                                                                                            border: 0px;">
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr id="trSubtotal" runat="server" visible="false" style="height: 50px; background-color: #e8e8e8;">
                                                                                                                        <td colspan="2" height="95" valign="top" style="border: 0px;">
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divIncomeSubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblIncomeSubtotal" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divNetIncomeSubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblNetIncomeSubtotal" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divSalarySubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblSalarySubtotal" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divTipSubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblTipSubtotal" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divAdvanceSubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblAdvanceSubtotal" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divBonusSubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblBonus" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                            <div>
                                                                                                                                <span style="font-weight: bold;">Subtotal</span>
                                                                                                                                <div id="divPenaltySubtotal" runat="server" class="daily_income_subtotal_new" style="float: left;">
                                                                                                                                    <asp:Label ID="lblPenalty" runat="server" Text="0.0" CssClass="subtoal_filter"></asp:Label>
                                                                                                                                </div>
                                                                                                                                <span style="position: relative; float: left; left: 1px; line-height: 31px;">€</span>
                                                                                                                            </div>
                                                                                                                        </td>
                                                                                                                        <td valign="top">
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <br />
                                                                                            <asp:ImageButton ID="imgBtnSaveBottom" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                                                                                OnClick="imgBtnSaveTop_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="tdTotalofDay" runat="server" align="center">
                                                                                            <div style="margin-top: 10px; font-weight: bold; width: 100px; margin-top: -40px;">
                                                                                                Total:</div>
                                                                                            <div style="background-color: Gray; color: white; height: 40px; line-height: 36px;
                                                                                                vertical-align: middle; width: 100px;">
                                                                                                <asp:UpdatePanel ID="upnlTotalOfDay" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>
                                                                                                        <asp:Label ID="lblTotalofDay" runat="server" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center">
                                                                                            <div style="margin-top: 30px; font-weight: bold;">
                                                                                                Comment of the Day:
                                                                                            </div>
                                                                                            <div class="textboxmulti">
                                                                                                <asp:TextBox ID="txtDayComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                                            </div>
                                                                                            <div>
                                                                                                <asp:ImageButton ID="imgBtnSaveDayComment" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                                    OnClick="imgBtnSaveDayComment_Click" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="divTab3" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                                    <asp:UpdatePanel ID="upnlOtherIncome" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlOtherIncome" runat="server" DefaultButton="imgBtnSaveOtherIncome">
                                                                                <table width="100%">
                                                                                    <tr id="trMessageOtherIncome" runat="server" visible="false">
                                                                                        <td colspan="4">
                                                                                            <h3>
                                                                                                <asp:Label ID="lblMessageOtherIncome" runat="server"></asp:Label>
                                                                                            </h3>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trAdd" runat="server">
                                                                                        <td style="width: 100px;" colspan="4">
                                                                                            <div style="float: left; width: 830px;">
                                                                                                <asp:ImageButton ID="imgAddSpecialIncome" runat="server" ImageUrl="~/Images/addincome_btn.png"
                                                                                                    OnClick="imgAddSpecialIncome_Click" />
                                                                                                <div style="float: right; position: relative;">
                                                                                                    <asp:ImageButton ID="imgBtnAddTempIncome" runat="server" ImageUrl="~/Images/askfornewincometype.png"
                                                                                                        OnClick="imgBtnAddTempIncome_Click" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trddlIncome" runat="server" visible="false">
                                                                                        <td colspan="4" align="left">
                                                                                            <div id="divIncomeDropDownList" runat="server">
                                                                                                <div class="textbox204">
                                                                                                    <asp:DropDownList ID="ddlIncomeTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIncomeTypes_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 5px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trLineBefore" runat="server">
                                                                                        <td colspan="4" style="background-color: #bfb8bb">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trOtherIncome" runat="server" visible="false">
                                                                                        <td style="width: 207px;">
                                                                                            <div>
                                                                                                Income Type:
                                                                                                <div class="textbox204" style="text-align: left; padding-left: 10px;">
                                                                                                    <asp:Label ID="lblOtherIncomeTypeName" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td style="width: 109px;">
                                                                                            Income Amount:
                                                                                            <div class="textbox_small" style="float: left; margin-left: 2px;">
                                                                                                <asp:TextBox ID="txtIncomeAmount" runat="server" Style="text-align: center;" CssClass="filter"
                                                                                                    onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                                                                                            </div>
                                                                                            <span style="position: relative; float: right; left: -20px; line-height: 31px;">€</span>
                                                                                        </td>
                                                                                        <td valign="bottom" style="width: 333px;">
                                                                                            Notes:
                                                                                            <div class="textbox330">
                                                                                                <asp:TextBox ID="txtNoteOtherIncome" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td valign="bottom">
                                                                                            <asp:ImageButton ID="imgBtnSaveOtherIncome" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                                                                                OnClick="imgBtnSaveOtherIncome_Click" />
                                                                                            <asp:ImageButton ID="ImgBtnEdit" runat="server" ImageUrl="~/Images/btn_edi_smallt.gif"
                                                                                                OnClick="ImgBtnEdit_Click" Visible="false" />
                                                                                            <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                                                                OnClick="imgBtnCancel_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trLineAfter" runat="server" visible="false">
                                                                                        <td colspan="4" style="background-color: #bfb8bb">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <asp:GridView ID="grdOtherIncomes" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                EmptyDataText="No Other Income added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                OnRowDataBound="grdOtherIncomes_RowDataBound" OnRowCommand="grdOtherIncomes_RowCommand">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Income Type">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkIncomeType" runat="server" CommandName="incometype" CommandArgument="<%#Bind('pkIncomID') %>"
                                                                                                                Text="<%#Bind('sIncomType') %>"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Income" ItemStyle-Width="100">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkIncome" runat="server" CommandName="income" CommandArgument="<%#Bind('pkIncomID') %>"
                                                                                                                Text="<%#Bind('fIncome') %>"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/close.png" CommandName="delincome"
                                                                                                                CommandArgument="<%#Bind('pkIncomID') %>" OnClientClick="javascript:return confirm('Are you sure that you want to remove this income record?');"
                                                                                                                onmouseover="javascript:OpenFeedbackWindow('Click to delete record')" onmouseout="javascript:CloseFeedBackWindow()" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="tdOtherIncomeTotal" runat="server" align="center" colspan="4">
                                                                                            <div style="font-weight: bold; width: 100px;">
                                                                                                Subtotal Total:</div>
                                                                                            <div style="background-color: Gray; color: white; height: 40px; line-height: 36px;
                                                                                                vertical-align: middle; width: 100px;">
                                                                                                <asp:Label ID="lblOtherIncomeSubtotal" runat="server" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="divTab4" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                                    <asp:UpdatePanel ID="upnlExpenses" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlExpenses" runat="server" DefaultButton="imgBtnSaveOtherIncome">
                                                                                <table width="100%">
                                                                                    <tr id="trMessageExpense" runat="server" visible="false">
                                                                                        <td colspan="5">
                                                                                            <h3>
                                                                                                <asp:Label ID="lblMessageExpense" runat="server"></asp:Label>
                                                                                            </h3>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trAddExpense" runat="server">
                                                                                        <td style="width: 100px;" colspan="5">
                                                                                            <div style="float: left; width: 830px;">
                                                                                                <asp:ImageButton ID="imgBtnAddExpense" runat="server" ImageUrl="~/Images/addexpense.png"
                                                                                                    OnClick="imgBtnAddExpense_Click" />
                                                                                                <div style="float: right; position: relative;">
                                                                                                    <asp:ImageButton ID="imgBtnAddNewExpenseType" runat="server" ImageUrl="~/Images/askfornewexpensetype.png"
                                                                                                        OnClick="imgBtnAddNewExpenseType_Click" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trddlExpense" runat="server" visible="false">
                                                                                        <td colspan="5" align="left">
                                                                                            <div id="divExpenseDropDownList" runat="server">
                                                                                                <div class="textbox204">
                                                                                                    <asp:DropDownList ID="ddlExpenses" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenses_SelectedIndexChanged">
                                                                                                    </asp:DropDownList>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5" style="height: 5px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trLineBeforeExpense" runat="server">
                                                                                        <td colspan="5" style="background-color: #bfb8bb">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trExpense" runat="server" visible="false">
                                                                                        <td style="width: 207px;" valign="bottom">
                                                                                            <div>
                                                                                                Expense Type:
                                                                                                <div class="textbox204" style="text-align: left; padding-left: 10px;">
                                                                                                    <asp:Label ID="lblExpenseTypeName" runat="server"></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td style="width: 109px;">
                                                                                            Expense Amount:
                                                                                            <div class="textbox_small" style="float: left; margin-left: 2px;">
                                                                                                <asp:TextBox ID="txtEnpenseAmount" runat="server" Style="text-align: center;" CssClass="filter"
                                                                                                    onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                                                                                            </div>
                                                                                            <span style="position: relative; float: right; left: -20px; line-height: 31px;">€</span>
                                                                                        </td>
                                                                                        <td valign="bottom" style="width: 333px;">
                                                                                            Notes:
                                                                                            <div class="textbox330">
                                                                                                <asp:TextBox ID="txtNoteExpense" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                       <td style="width: 109px;">
                                                                                            Invoiced Amount:
                                                                                            <div class="textbox_small" style="float: left; margin-left: 2px;">
                                                                                                <asp:TextBox ID="txtInvoicedAmount" runat="server" Style="text-align: center;" CssClass="filter"
                                                                                                    onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                                                                                            </div>
                                                                                            <span style="position: relative; float: right; left: -20px; line-height: 31px;">€</span>
                                                                                        </td>
                                                                                        <td style="width: 109px;">
                                                                                            Non Invoiced Amount:
                                                                                            <div class="textbox_small" style="float: left; margin-left: 2px;">
                                                                                                <asp:TextBox ID="txtNonInvoicedAmount" runat="server" Style="text-align: center;" CssClass="filter"
                                                                                                    onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                                                                                            </div>
                                                                                            <span style="position: relative; float: right; left: -20px; line-height: 31px;">€</span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trExpenseButton" runat="server" visible="false">
                                                                                    <td colspan="5" >
                                                                                      <asp:ImageButton ID="imgBtnSaveExpense" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                                                                                OnClick="imgBtnSaveExpense_Click" />
                                                                                            <asp:ImageButton ID="imgBtnEditExpense" runat="server" ImageUrl="~/Images/btn_edi_smallt.gif"
                                                                                                OnClick="imgBtnEditExpense_Click" />
                                                                                            <asp:ImageButton ID="imgBtnCancelExpense" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                                                                OnClick="imgBtnCancelExpense_Click" />
                                                                                    </td>
                                                                                    </tr>
                                                                                    <tr id="trLineAfterExpense" runat="server" visible="false">
                                                                                        <td colspan="5" style="background-color: #bfb8bb">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5">
                                                                                            <asp:GridView ID="grdExpenses" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                EmptyDataText="No Other Expense added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                OnRowCommand="grdExpenses_RowCommand" OnRowDataBound="grdExpenses_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Expense Type">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkExpenseType" runat="server" CommandName="expensetype" CommandArgument="<%#Bind('pkExpanseID') %>"
                                                                                                                Text="<%#Bind('sExpanseCategory') %>"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Expense" ItemStyle-Width="100">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkExpense" runat="server" CommandName="expense" CommandArgument="<%#Bind('pkExpanseID') %>"
                                                                                                                Text="<%#Bind('ExpanseAmount') %>"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="imgBtnDeleteExpense" runat="server" ImageUrl="~/Images/close.png"
                                                                                                                CommandName="delExpense" CommandArgument="<%#Bind('pkExpanseID') %>" OnClientClick="javascript:return confirm('Are you sure that you want to remove this income record?');"
                                                                                                                onmouseover="javascript:OpenFeedbackWindow('Click to delete record')" onmouseout="javascript:CloseFeedBackWindow()" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="tdExpenseTotal" runat="server" align="center" colspan="5">
                                                                                            <div style="font-weight: bold; width: 100px;">
                                                                                                Subtotal Total:</div>
                                                                                            <div style="background-color: Gray; color: white; height: 40px; line-height: 36px;
                                                                                                vertical-align: middle; width: 100px;">
                                                                                                <asp:Label ID="lblExpenseSubtotal" runat="server" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="divTab5" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                                    <asp:UpdatePanel ID="upnlRegister" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlRegister" runat="server" DefaultButton="imgBtnSaveRegisterValueTop">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <div style="float: left; margin-left: 321px; font-weight: bold; color: Green;">
                                                                                                <asp:Label ID="lblMessageRegister" runat="server" Visible="false" Text=""></asp:Label>
                                                                                            </div>
                                                                                            <div style="float: right;">
                                                                                                <asp:ImageButton ID="imgBtnSaveRegisterValueTop" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                                    OnClick="imgBtnSaveRegisterValueTop_Click" OnClientClick="changeVal();" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:GridView ID="grdRegisters" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                                                                HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                RowStyle-CssClass="rowstyle" Style="width: 100%;" OnRowCommand="grdRegisters_RowCommand"
                                                                                                OnRowDataBound="grdRegisters_RowDataBound" EmptyDataText="No any register found.">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="rName" HeaderText="Name" ItemStyle-Width="100" />
                                                                                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="300">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDescription" runat="server" Text="<%#Bind('rDescription') %>"></asp:Label>
                                                                                                            <asp:HiddenField ID="hidRegVat" runat="server" Value="<%#Bind('fkVatID') %>" />
                                                                                                            <asp:HiddenField ID="hidRegID" runat="server" Value="<%#Bind('pkRegisterID') %>" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Value">
                                                                                                        <ItemTemplate>
                                                                                                            <div id="divIncome" runat="server" class="textbox_small_new" style="float: left;">
                                                                                                                <asp:TextBox ID="txtRegisterValue" runat="server" CssClass="filter" Style="text-align: center;"></asp:TextBox>
                                                                                                                <span style="position: relative; float: right; left: 0px; line-height: 31px;">€</span>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Note">
                                                                                                        <ItemTemplate>
                                                                                                            <div class="textbox330">
                                                                                                                <asp:TextBox ID="txtRegisterNote" runat="server" CssClass="note"></asp:TextBox>
                                                                                                            </div>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <div style="float: right; padding-top: 10px;">
                                                                                                <asp:ImageButton ID="imgBtnSaveRegisterValueBottom" runat="server" ImageUrl="~/Images/btn_save.png"
                                                                                                    OnClick="imgBtnSaveRegisterValueTop_Click" OnClientClick="changeVal();" />
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="tdRegValTotal" runat="server" align="center" colspan="4">
                                                                                            <div style="font-weight: bold; width: 100px;">
                                                                                                Subtotal Total:</div>
                                                                                            <div style="background-color: Gray; color: white; height: 40px; line-height: 36px;
                                                                                                vertical-align: middle; width: 100px;">
                                                                                                <asp:Label ID="lblRegSubTotal" runat="server" Style="font-weight: bold; font-size: 15px;"
                                                                                                    Text="0.00 €"></asp:Label>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblFromAddress" runat="server" Text=""></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                To:
                            </td>
                            <td>
                                <div class="textbox204_message">
                                    <asp:Label ID="lblToAddress" runat="server" Text=""></asp:Label></div>
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
                                        font-size: 12px; background: ; margin-top: 8px;"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqSubject" runat="server" ErrorMessage="*" ControlToValidate="txtSubject"
                                        ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
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
    <asp:UpdatePanel ID="upnlSaveChanges" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divConfirm" style="position: fixed; top: 50%; left: 33%; bottom: 50%; right: 50%;
                width: 572px; height: 130px; vertical-align: middle; display: none;">
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$('#divConfirm').hide();$('#divBackground').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div style="padding-top: 10px; padding-left: 15px; background-color: White; border: solid 2px #e8e8e8;">
                    <table>
                        <tr>
                            <td colspan="2" align="center">
                                You have not saved your changes! Press Save button below to save your changes.
                                <br />
                                <br />
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 275px;">
                                <asp:ImageButton ID="imgBtnSaveChanges" runat="server" ImageUrl="~/Images/btn_save.png"
                                    OnClick="imgBtnSaveTop_Click" />
                                <br />
                                <br />
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="imgCancelChanges" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                    OnClick="imgCancelChanges_Click" />
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

