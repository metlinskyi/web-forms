# Asp.Net Web Froms Localization

The proof of concept application with demonstration of localization for Asp.Net Web Froms.
The main target is minimize of copying a code.

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
|   +-- UI.resx             // common resource
|   +-- UI.es-US.resx       // localized resource
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
When the route handler does not find a specific culture page, then system to get a default page.

```
.
+-- Pages
|   +-- Account.aspx            // common page 
|   +-- Account.es-US.aspx      // localized page 
```

Use links with {culture} tag.
All values of href or src attributes with {culture}  tag will be replaced to current culture.

```ASP
<a runat="server" href="~/{culture}/Account"><asp:Literal runat="server" Text="<%$ Resources: UI, AccountTitle %>" /></a>
```

#### UI

Different culture templates on a page.
If the Localization control does not find a specific culture template, then will be rendered a default template.

```ASP
<asp:Localization runat="server">
    <asp:Culture runat="server">
        <!-- Default template for all cultures -->
    </asp:Culture>
    <asp:Culture runat="server" Name="es-US">
        <!-- Specific es-US culture template -->
    </asp:Culture>
</asp:Localization>
```

Different culture templates of UserControl.
If a UserControl does not find a specific culture template, then will be rendered a default template.

```
.
+-- Controls
|   +-- UserProfile.ascx          // common control template 
|   +-- UserProfile.es-US.ascx    // localized control template
```