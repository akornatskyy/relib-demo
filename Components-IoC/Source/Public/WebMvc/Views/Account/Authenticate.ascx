<%@ Control Language="C#" Inherits="ViewUserControl<LogonViewData>" %>
<%@ Import Namespace="Public.WebMvc.Views.Account.Resources" %>
<div id="logon">
    <h2>
        <%= LogOn.Header %></h2>
    <p>
        <%= LogOn.Description.FormatWith(Html.ActionLink(LogOn.Register, "register"))%>
    </p>
    <%= Html.ShowModelError() %>
    <% var details = Model.Details;
       using (Html.BeginForm())
       { %>
    <div>
        <fieldset>
            <legend>
                <%= LogOn.AccountInfo %></legend>
            <p>
                <label for="username">
                    <%= LogOn.LabelUsername %></label>
                <%= Html.TextBox("username", details.UserName) %>
                <%= Html.ValidationMessage("username") %>
            </p>
            <p>
                <label for="password">
                    <%= LogOn.LabelPassword %></label>
                <%= Html.Password("password", string.Empty, new { maxlength = 12, autocomplete = "off" })%>
                <%= Html.ValidationMessage("password") %>
            </p>
            <p>
                <%= Html.CheckBox("rememberMe", Model.RememberMe) %>
                <label class="inline" for="rememberMe">
                    <%= LogOn.LabelRememberMe %></label>
            </p>
            <p>
                <% Html.RenderPartial("Captcha"); %>
            </p>
            <p>
                <input name="logon" type="submit" value="<%= LogOn.ButtonLogOn %>" />
                <%= Html.AntiForgeryToken(new [] { AntiForgeryTokenSaltNames.Login/*, details.Id*/ }) %>
            </p>
        </fieldset>
        <i>
            <%= LogOn.DemoPassword %></i>
    </div>
    <% } %>
</div>
