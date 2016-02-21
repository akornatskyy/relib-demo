<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<TicketDetailsViewData>" %>
<%@ Import Namespace="Public.WebMvc.Views.Tickets.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(Details.Title)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="ticketsdetails">
        <h2>
            <%= Details.Header %></h2>
        <p>
            Curabitur tellus. Phasellus tellus <a href="#">turpis iaculis</a>
            in, faucibus lobortis, posuere in, lorem. Vivamus varius
            justo amet porttitor iaculis, ipsum massa aliquet nulla,
            non elementum mi elit a mauris. In hac habitasse platea.
        </p>
        <% var details = Model.Details; %>
        <fieldset>
            <legend><%= Details.FormLegend %></legend>
            <p>
                <%= Details.LabelTicketId %>
                <%= Html.Encode(details.TicketId.ToString("0000"))%>
                (<%= details.IsOpen ? Details.StatusOpened : Details.StatusClosed %>)
            </p>
            <p>
                <%= Details.LabelType %>
                <%= Html.Encode(details.TicketType.DisplayName)%>
            </p>
            <p>
                <%= Details.LabelAssigned %>
                <%= Html.Encode(details.AssignedTo)%>
            </p>
            <p>
                <%= Details.LabelOpened %>
                <%= Html.Encode(String.Format("{0:g}", details.OpenedOn))%>
            </p>
            <p>
                <%= Details.LabelModified %>
                <%= Html.Encode(String.Format("{0:g}", details.ModifiedOn))%>
            </p>
            <p>
                <%= Details.LabelClosed %>
                <%= Html.Encode(details.ClosedOn != null ? String.Format("{0:g}", details.ClosedOn) : "N/A")%>
            </p>
            <p>
                <%= Details.LabelTitle %>
                <%= Html.Encode(details.Title)%>
            </p>
            <p>
                <code>
                    <%= Html.Encode(details.Description)%>
                </code>
            </p>
        </fieldset>
        <p>
            <%=Html.ActionLink(Details.LinkBack, "index") %>
        </p>
    </div>
</asp:Content>
