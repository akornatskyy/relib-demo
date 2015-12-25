<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Public.WebMvc.Views.Shared.Resources" %>
<label for="turingnumber">
    <%= CaptchaHtmlHelper.Render() %></label>
<%= Html.TextBox("turingnumber", "", new { 
    title = Captcha.TuringNumberTitle, 
    maxlength = 4, 
    autocomplete = "off" })%>
<%= Html.ValidationMessage("turingnumber")%>
