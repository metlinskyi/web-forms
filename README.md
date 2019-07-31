# web-forms
Asp.Net Web Froms localization MVP


RouteConfig.cs

```C#

    var handler = new CultureRouteHandler("culture", "page")
    {
        WebRoot = "~/Pages/",
    };

    routes.Add(new Route(string.Empty, handler));
    routes.Add(new Route("{culture}/{*page}", handler));

```

```
.
+-- Pages
|   +-- Account.aspx
|   +-- Account.es-US.aspx

```

Web.config

```XML

    <controls>
        <add assembly="Web.Localization" namespace="Web.Localization.UI" tagPrefix="asp"/>

```

Site.Master

```ASP

    <asp:Localization runat="server">
        <asp:Culture runat="server">
            <!-- Default template for all culture -->
        </asp:Culture>
        <asp:Culture runat="server" Name="es-US">
            <!-- Specified es-US culture template -->
        </asp:Culture>
    </asp:Localization>

```

Tag {culture}

```ASP

    <a class="nav-link" runat="server" href="~/{culture}/Account"><asp:Literal runat="server" Text="<%$ Resources: UI, AccountTitle %>" /></a>

```