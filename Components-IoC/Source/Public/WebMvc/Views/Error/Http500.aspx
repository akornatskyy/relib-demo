<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Public.WebMvc.Views.Error.Resources" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(HttpError.TitleHttp500)%>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="welcome">
        <h2>
            <%= HttpError.HeaderHttp500 %>
        </h2>
        <p>
            <%= HttpError.DescriptionHttp500 %>
        </p>
    </div>
</asp:Content>
