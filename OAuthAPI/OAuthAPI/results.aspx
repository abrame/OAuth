<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<% 
  string code = Request.QueryString.Get("code");
  if (code != null && code != "")
  {
    // i want to create the post request to obtain authorization token
  }  
%>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title></title>
</head>
<body>
    <div>
      <% Response.Write(Request.QueryString["code"]); %>
    </div>
</body>
</html>
