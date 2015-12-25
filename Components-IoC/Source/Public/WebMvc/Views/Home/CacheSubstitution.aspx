<%@ OutputCache VaryByParam="none" Duration="900" %>

<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Public.WebMvc.Views.Home.Resources" %>
<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(CacheSubstitution.Title)%>
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="cs">
        <h2>
            <%= CacheSubstitution.Header %></h2>
        <p>
            <%= CacheSubstitution.Description %>
        </p>
        <p>
            <b>
                <%= Html.RenderSubstitution(Substitutions.CurrentTime) %></b>
        </p>
        <p>
            <%= CacheSubstitution.RenderedAt %>
            <%= DateTime.Now.ToString() %>.
        </p>
    </div>
</asp:Content>
