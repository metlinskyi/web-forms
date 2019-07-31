# Asp.Net Web Froms Localization

The proof of concept application with demonstration of localization for Asp.Net Web Froms.

#### Web.config

Set a default culture.

```XML
<system.web>
    <globalization uiCulture="en" culture="en" />
```

Set extend Asp.Net localization controls.

```XML
<controls>
    <add assembly="Web.Localization" namespace="Web.Localization.UI" tagPrefix="asp"/>
```

#### Literals

Create culture resource files.

```
.
+-- App_GlobalResources
|   +-- UI.resx
|   +-- UI.es-US.resx
```

Using the expression builder in the ASP.NET Web Form page.

```ASP
<asp:Literal runat="server" Text="<%$ Resources: UI, BrandName %>" />
```

#### Routing

Set culture routing handler in RouteConfig.cs

```C#
var handler = new CultureRouteHandler("culture", "page")
{
    WebRoot = "~/Pages/",
};

routes.Add(new Route(string.Empty, handler));
routes.Add(new Route("{culture}/{*page}", handler));
```

Different culture pages.

```
.
+-- Pages
|   +-- Account.aspx
|   +-- Account.es-US.aspx
```

Use links with {culture} tag.

```ASP
<a runat="server" href="~/{culture}/Account"><asp:Literal runat="server" Text="<%$ Resources: UI, AccountTitle %>" /></a>
```

#### UI

Different culture templates on a page.

```ASP
<asp:Localization runat="server">
    <asp:Culture runat="server">
        <!-- Default template for all cultures -->
    </asp:Culture>
    <asp:Culture runat="server" Name="es-US">
        <!-- Specified es-US culture template -->
    </asp:Culture>
</asp:Localization>
```

Different culture templates of UserControl.

```
.
+-- Controls
|   +-- UserProfile.ascx
|   +-- UserProfile.es-US.ascx
```