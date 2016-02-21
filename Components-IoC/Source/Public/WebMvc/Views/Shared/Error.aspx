<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>
<%@ Import Namespace="Public.WebMvc.Views.Shared.Resources" %>
<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(GeneralError.Title)%>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <h2>
        <%= Html.PageTitle(GeneralError.Description)%>
    </h2>
</asp:Content>
