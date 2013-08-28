<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<% string link = "https://accounts.google.com/o/oauth2/auth?" +
                  "response_type=code&" +
                  "client_id=780082496750.apps.googleusercontent.com&" +
                  "redirect_uri=http://localhost:51073/results.aspx&" +
                  "scope=https://www.googleapis.com/auth/userinfo.profile"; %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
</head>
<body>
    <div>
    <a href="<%Response.Write(link); %>">Get Authorization Code</a>
    </div>
</body>
</html>
