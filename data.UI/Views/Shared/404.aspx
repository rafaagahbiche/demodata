<%@ Page Language="C#" AutoEventWireup="true" Inherits="demo.UI.Views.Shared._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>404 Not Found</title>
</head>
<body>
    <div>
    <% Response.StatusCode = 404; %>
    </div>
</body>
</html>
