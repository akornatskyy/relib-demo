<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<LogonViewData>" %>

<%@ Import Namespace="Public.WebMvc.Views.Account.Resources" %>
<asp:Content ID="loginTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(LogOn.Title)%>
</asp:Content>
<asp:Content ID="loginContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <% Html.RenderPartial(ViewData.AlternatePartialViewName()); %>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent"
    runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            ajaxPostOnSubmit();
            attachCaptchaReload();
        });
    </script>

</asp:Content>
