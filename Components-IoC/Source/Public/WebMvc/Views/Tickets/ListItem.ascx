<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TicketSearchResult>" %>
<%@ Import Namespace="Public.WebMvc.Views.Tickets.Resources" %>
<td>
    <%= Html.ActionLink(List.LinkView, "details", new { id=Model.TicketId }) %>
</td>
<td>
    <%= Html.Encode(Model.TicketId.ToString("0000"))%>
</td>
<td>
    <%= Html.Encode(Model.TicketType.DisplayName)%>
</td>
<td>
    <%= Html.Encode(Model.AssignedTo)%>
</td>
<td>
    <%= Html.Encode(Model.Title)%>
</td>
<td>
    <%= Model.IsOpen ? List.LabelYes : List.LabelNo %>
</td>
