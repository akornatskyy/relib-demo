<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<TicketSearchViewData>" %>

<%@ Import Namespace="Public.WebMvc.Views.Tickets.Resources" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(Index.Title)%>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="ticketssearch">
        <h2>
            <%= Index.Header %></h2>
        <p>
            Curabitur tellus. Phasellus tellus <a href="#">turpis iaculis</a>
            in, faucibus lobortis, posuere in, lorem. Vivamus varius
            justo amet porttitor iaculis, ipsum massa aliquet nulla,
            non elementum mi elit a mauris. In hac habitasse platea.</p>
        <%= Html.ShowModelError() %>
        <% using (Html.BeginForm("search", "tickets", FormMethod.Get))
           { %>
        <fieldset>
            <legend><%= Index.FormLegend %></legend>
            <p>
                <label for="title">
                    <%= Index.LabelTitle %></label>
                <%= Html.TextBox("title", Model.Specification.Title) %>
                <%= Html.ValidationMessage("title")%>
            </p>
            <p>
                <label for="ticketType">
                    <%= Index.LabelType %></label>
                <%= Html.DropDownList("ticketTypeId", Model.TicketTypeSelectList)%>
                <%= Html.ValidationMessage("ticketTypeId")%>
            </p>
            <p>
                <%= Html.CheckBox("includeClosed", Model.Specification.IncludeClosed)%>
                <label class="inline" for="includeClosed">
                    <%= Index.LabelIncludeClosed %></label>
            </p>
            <p>
                <input type="submit" value="<%= Index.ButtonSearch %>" />
            </p>
        </fieldset>
        <% } %>
    </div>
</asp:Content>
