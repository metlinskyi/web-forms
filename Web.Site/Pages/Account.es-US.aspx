<%@ Page Title="<%$ Resources: UI, AccountTitle %>" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" Inherits="Web.CodeBehind.Pages.Account" %>
<%@ Register TagName="UserImage" TagPrefix="account" Src="~/Controls/UserImage.ascx" %>
<%@ Register TagName="UserProfile" TagPrefix="account" Src="~/Controls/UserProfile.ascx" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <h2><%: Title %>.</h2>
            <account:UserImage runat="server" />
        </div>
        <div class="col-md-8">
            <h2><asp:Literal runat="server" Text="<%$ Resources: UI, UserProfile %>" /></h2>
            <account:UserProfile runat="server" />
        </div>
    </div>
</asp:Content>
