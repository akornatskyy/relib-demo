<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<TicketListViewData>" %>
<%@ Import Namespace="Public.WebMvc.Views.Tickets.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(List.Title)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">
    <% Html.RenderPartial(ViewData.AlternatePartialViewName()); %>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent"
    runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            ajaxPagerClick();
        });
    </script>

</asp:Content>
