<%@ OutputCache VaryByParam="none" Shared="true" Duration="900" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Public.WebMvc.Views.Shared.Resources" %>
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