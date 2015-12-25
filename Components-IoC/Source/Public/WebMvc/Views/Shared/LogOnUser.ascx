<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Public.WebMvc.Views.Shared.Resources" %>
<%
    if (Request.IsAuthenticated)
    {
%>
<%= LogOnUser.Welcome %>
<b><%= Html.Encode(Page.User.Identity.Name) %></b>! 
    [<%= Html.ActionLink(LogOnUser.LogOff, "logoff", "account")%>]
<%
    }
    else
    {
%>[<%= Html.ActionLink(LogOnUser.LogOn, "logon", "account")%>]<%
    }
%>
