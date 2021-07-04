<%@ Page Language="C#" AutoEventWireup="false" CodeFile="CrystalTest.aspx.cs" Inherits="Test_CrystalTest" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="Data Source=192.168.1.222\SQLEXPRESS; Initial Catalog=Budget; User Name=sa; Password=sa;"
            SelectCommand="SELECT * FROM Budget" runat="server"></asp:SqlDataSource>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="">
                <DataSources>
                    <CR:DataSourceRef DataSourceID="SqlDataSource1" />
                </DataSources>
            </Report>
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" ReportSourceID="CrystalReportSource1"
            runat="server" AutoDataBind="false" />
    </div>
    </form>
</body>
</html>
