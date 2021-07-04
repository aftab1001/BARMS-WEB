﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
    <title>FCBKcomplete Demo</title>
    <%--<link rel="stylesheet" href="style.css" type="text/css" media="screen" title="Test Stylesheet"
        charset="utf-8" />--%>
    <link href="Styles/jstyle.css" rel="stylesheet" type="text/css" />
    <%--<script src="jquery.js" type="text/javascript" charset="utf-8"></script>--%>

    <script src="JavaScript/jquery.js" type="text/javascript"></script>

    <%--<script src="fcbkcomplete.min.js" type="text/javascript" charset="utf-8"></script>--%>

    <script src="JavaScript/fcbkcomplete.min.js" type="text/javascript"></script>
</head>
<body id="test">
    
    <div id="text">
    </div>
    <form id="form1" runat="server" action="test_submit" method="get" accept-charset="utf-8">
    <div>
        <ol>
            <li id="facebook-list" class="input-text">
                
                <input type="text" value="" id="facebook-demo" />
                
                <ul id="preadded" style="display: none">
                    <li value="1">Jorge Luis Borges</li>
                    <li value="2">Julio Cortazar</li>
                </ul>
                <div id="facebook-auto">
                    <div class="default">
                        Type the name of an argentine writer you like</div>
                    <ul id="feed">
                        <li value="test1">tester 1</li>
                        <li value="test2">tester 2</li>
                        <li value="test3">tester 3</li>
                        <li value="test4">t3</li>
                    </ul>
                </div>
            </li>
        </ol>
    </div>
    
    
    
    
    
    
    </form>
    <script type="text/javascript">
        $(document).ready(function() {
        $.facebooklist('#facebook-demo', '#preadded', '#facebook-auto', { url: 'fetched.php', cache: 1 }, 10, { userfilter: 1, casesensetive: 0 });
        });
    </script>
</body>
</html>
