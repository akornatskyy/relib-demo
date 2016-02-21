<%@ OutputCache VaryByParam="none" Shared="true" Duration="900" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Public.WebMvc.Views.Tickets.Resources" %>
<tr>
    <th>
    </th>
    <th>
        <%= ListHeader.TicketId %>
    </th>
    <th>
        <%= ListHeader.Type %>
    </th>
    <th>
        <%= ListHeader.AssignedTo %>
    </th>
    <th>
        <%= ListHeader.Title %>
    </th>
    <th>
        <%= ListHeader.Open %>
    </th>
</tr>
