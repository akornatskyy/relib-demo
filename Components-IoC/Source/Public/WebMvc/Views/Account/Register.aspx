<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<RegisterViewData>" %>
<%@ Import Namespace="Public.WebMvc.Views.Account.Resources" %>
<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent"
    runat="server">
    <%= Html.PageTitle(Register.Title)%>
</asp:Content>
<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent"
    runat="server">
    <div id="register">
        <h2>
            <%= Register.Header %></h2>
        <p>
            <%= Register.Description %>
        </p>
        <p>
            <%= Register.PasswordRules %>
        </p>
        <%= Html.ShowModelError() %>
        <% var credentials = Model.Details.Credentials;
           var info = Model.Details.Info;
           using (Html.BeginForm())
           { %>
        <div>
            <fieldset>
                <legend><%= Register.FormLegend %></legend>
                <p>
                    <label for="username">
                        <%= Register.LabelUsername %></label>
                    <%= Html.TextBox("username", credentials.UserName, new { autocomplete = "off" })%>
                    <%= Html.ValidationMessage("username") %>
                </p>
                <p>
                    <label for="displayName">
                        <%= Register.LabelDisplayname %></label>
                    <%= Html.TextBox("displayName", info.DisplayName, new { autocomplete = "off" })%>
                    <%= Html.ValidationMessage("displayName")%>
                </p>
                <p>
                    <label for="email">
                        <%= Register.LabelEmail %></label>
                    <%= Html.TextBox("email", info.Email, new { autocomplete = "off" })%>
                    <%= Html.ValidationMessage("email") %>
                </p>
                <p>
                    <label for="password">
                        <%= Register.LabelPassword %></label>
                    <%= Html.Password("password", credentials.Password, new { maxlength = 12, autocomplete = "off" })%>
                    <%= Html.ValidationMessage("password") %>
                </p>
                <p>
                    <label for="confirmPassword">
                        <%= Register.LabelConfirmPassword %></label>
                    <%= Html.Password("confirmPassword", Model.ConfirmPassword, new { maxlength = 12, autocomplete = "off" })%>
                    <%= Html.ValidationMessage("confirmPassword") %>
                </p>
                <p>
                    <label for="questionid">
                        <%= Register.LabelSecurityQuestion %></label>
                    <%= Html.DropDownList("questionid", Model.PasswordQuestionSelectList)%>
                    <%= Html.ValidationMessage("questionid")%>
                </p>
                <p>
                    <label for="answer">
                        <%= Register.LabelAnswer %></label>
                    <%= Html.TextBox("answer", Model.Details.Answer, new { size = 30, autocomplete = "off" })%>
                    <%= Html.ValidationMessage("answer")%>
                </p>
                <p>
                    <% Html.RenderPartial("Captcha2"); %>
                </p>
                <p>
                    <input name="register" type="submit" value="<%= Register.ButtonRegister %>" />
                    <%= Html.AntiForgeryTokenPatch(AntiForgeryTokenSaltNames.Register) %>
                </p>
            </fieldset>
        </div>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {
            attachCaptchaReload();
        });
    </script>
</asp:Content>