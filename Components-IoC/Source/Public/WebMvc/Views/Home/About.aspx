<%@ OutputCache VaryByParam="none" Duration="900" %>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Public.WebMvc.Views.Home.Resources" %>
<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(About.Title)%>
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="about">
        <h2>
            <%= About.Header %></h2>
        <p>
            <%= About.Description %>
        </p>
        <p>
            <%= About.TryActionResults %>
        </p>
        <ul>
            <li>
                <%= Html.ActionLink("BadRequest", "badrequest")%></li>
            <li>
                <%= Html.ActionLink("Forbidden", "forbidden")%></li>
            <li>
                <%= Html.ActionLink("FileNotFound", "filenotfound")%></li>
            <li>
                <%= Html.ActionLink("InternalError", "internalerror")%></li>
            <li>
                <%= Html.ActionLink("AntiForgeryException", "antiforgery")%></li>
            <li>
                <%= Html.ActionLink("HttpRequestValidationException", "requestvalidation")%></li>
            <li>
                <%= Html.ActionLink("IpPolicyException", "ippolicy")%></li>
            <li>
                <%= Html.AbsoluteRouteLink("AbsoluteRouteLink (Scheme)", "SchemeExample")%>
                <%= Url.AbsoluteRouteUrl("SchemeExample")%>
            </li>
            <li>
                <%= Html.AbsoluteRouteLink("AbsoluteRouteLink (Domain)", "DomainExample")%>
                <%= Url.AbsoluteRouteUrl("DomainExample")%>
            </li>
            <li>
                <%= Html.AbsoluteRouteLink("AbsoluteRouteLink (Scheme + Domain)", "SchemeAndDomainExample")%>
                <%= Url.AbsoluteRouteUrl("SchemeAndDomainExample")%>
            </li>
        </ul>
    </div>
</asp:Content>
