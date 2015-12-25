<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TicketListViewData>" %>
<%@ Import Namespace="Public.WebMvc.Views.Tickets.Resources" %>
<div id="ticketslist">
    <h2>
        <%= List.Header %>
    </h2>
    <p>
        Curabitur tellus. Phasellus tellus&nbsp;<a href="#">turpis
            iaculis</a>&nbsp; in, faucibus lobortis, posuere in,
        lorem. Vivamus varius justo amet porttitor iaculis, ipsum
        massa aliquet nulla, non elementum mi elit a mauris. In
        hac habitasse platea.
    </p>
    <% if (!Model.Items.IsNullOrEmpty())
       { %>
    <table border="1" cellspacing="0" cellpadding="5" class="list">
        <% Html.RenderPartial("ListHeader"); %>
        <tbody>
            <% Model.Items.ForEach(item =>
                   { %>
            <tr>
                <% Html.RenderPartial("ListItem", item); %>
            </tr>
            <% }, itemAlt =>
               { %>
            <tr class="row-alternate">
                <% Html.RenderPartial("ListItem", itemAlt); %>
            </tr>
            <% }); %>
        </tbody>
    </table>
    <div class="pager">
        <%= Html.ListPager("list")%>
        <%= Html.ListPageSize("list")%>
    </div>
    <% }
       else
       { %>
    <%= Html.NoRecordsFound()%>
    <% } %>
    <p>
        <%= Html.ActionLink(List.LinkBack, "index") %>
    </p>
</div>
