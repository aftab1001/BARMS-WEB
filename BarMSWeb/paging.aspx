<%@ Page Language="C#" AutoEventWireup="true" CodeFile="paging.aspx.cs" Inherits="paging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <script src="JavaScript/jquery.min.js" type="text/javascript"></script>


    <script src="JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <link href="JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    
    <script src="JavaScript/quickpager.jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            alert('a');

            $("table.pageme tbody").quickPager({
                pageSize: 5,
                currentPage: 1,
                holder: ".pager"
            });
        });
    </script>
<style type="text/css">


p {
	background: #e5e5e5;
	margin-bottom:1px;
	margin-top:0px;
}

ul.paging li {
    padding: 10px;
    background: #83bd63;
    font-family: georgia;
    font-size: 24px;
    color: #fff;
    line-height: 1;
    width: 180px;
    margin-bottom: 1px;
}

ul.pageNav li{
    display:block;
    floaT: left;
    padding: 3px;
    font-family: georgia;
}

ul.pageNav li a{
    color: #333;
    text-decoration: none;
}

li.currentPage {
	background: red;
        background: #83bd63;	
}

ul.pageNav li.currentPage a {
	color: #fff;	
}

table.pageme {
    border-collapse: collapse;
    border: 1px solid #ccc;
}  

table.pageme td {
    border-collapse: collapse;
    border: 1px solid #ccc;
}      

table, div {
	display:block;
	clear: both
}


</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="pager">
        </div>
        <asp:GridView ID="grdData" runat="server" AutoGenerateColumns="false" CssClass="pageme">
            <Columns>
                <asp:BoundField DataField="pkuserid" HeaderText="ID" />
                <asp:BoundField DataField="sfirstname" HeaderText="Name" />
            </Columns>
        </asp:GridView>
        <div class="pager">
        </div>
    </div>
    </form>
</body>
</html>
