﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserLink.ascx.cs" Inherits="Web.Site.Controls.UserLink" %>
<a class="nav-link" runat="server" href="~/{culture}/Account"><asp:Literal runat="server" Text="<%$ Resources: UI, AccountTitle %>" /></a>