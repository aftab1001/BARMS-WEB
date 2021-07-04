<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestChart.aspx.cs" Inherits="Test_TestChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
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
    </script>

</head>
<body>
    <div id="chart_div" style="width: 900px; height: 500px;">
    </div>
</body>
</html>
