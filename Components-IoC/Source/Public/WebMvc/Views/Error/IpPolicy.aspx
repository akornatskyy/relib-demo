<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Public.WebMvc.Views.Error.Resources" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(HttpError.TitleHttp403)%>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="welcome">
        <h2>
            <%= HttpError.HeaderIpPolicy %>
        </h2>
        <p>
            <%= HttpError.DescriptionIpPolicy%>
        </p>
    </div>
</asp:Content>
