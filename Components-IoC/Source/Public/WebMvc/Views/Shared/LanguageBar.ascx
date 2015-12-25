<%@ OutputCache VaryByParam="none" Shared="true" Duration="900" %>
<%@ Import Namespace="Public.WebMvc.Views.Shared.Resources" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<i>
    <% 
        var englishRouteValues = new RouteValueDictionary(Html.ViewContext.RouteData.Values);
        englishRouteValues["language"] = "en";
        var russianRouteValues = new RouteValueDictionary(Html.ViewContext.RouteData.Values);
        russianRouteValues["language"] = "ru";
    %>
    <%= Html.ActionLink(LanguageBar.En, null, englishRouteValues)%>
    |
    <%= Html.ActionLink(LanguageBar.Ru, null, russianRouteValues)%>
</i>