# Asp.Net Web Froms Localization

The proof of concept application with demonstration of localization for Asp.Net Web Froms.
The main target is to minimize the copying of a code.

#### Web.config

Set a default culture.

```XML
<system.web>
    <globalization uiCulture="en-US" culture="en-US" />
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
|   +-- UI.resx             // default resource
|   +-- UI.es-US.resx       // localized resource
```

Use the expression builder in the ASP.NET Web Form page.

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
When the route handler does not find a specific culture page, then system will get a common page.

```
.
+-- Pages
|   +-- Account.aspx            // common page
|   +-- Account.es-US.aspx      // specific page 
```

Use links with {culture} tag.
All values of 'href' or 'src' attributes with {culture} tag will be replaced to current culture.

```ASP
<a runat="server" href="~/{culture}/Account"><asp:Literal runat="server" Text="<%$ Resources: UI, AccountTitle %>" /></a>
```

#### UI

Different culture templates on a page.
If the Localization control does not find a specific culture template, then a default template will be rendered.

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
If a UserControl does not find a specific culture template, then a default template will be rendered.

```
.
+-- Controls
|   +-- UserProfile.ascx          // common control template 
|   +-- UserProfile.es-US.ascx    // specific control template
```

#### Performance

All searching process results will be cached, for example:

```C#
// Trying to find a valid template path in a cache.
var key = string.Concat(culture, AppRelativeVirtualPath);
var appRelativeVirtualPath = Cache[key] as string;
if (string.IsNullOrEmpty(appRelativeVirtualPath))
{
    // The template path not found in cache, creating a new template path with current culture.
    appRelativeVirtualPath = AppRelativeVirtualPath.Replace(".ascx", $".{culture}.ascx");

    try
    {
        // Trying to load the localized template.
        template = LoadTemplate(appRelativeVirtualPath);
        Cache[key] = appRelativeVirtualPath;
    }
    catch
    {
        // The localized template not found, will be use default.
        Cache[key] = appRelativeVirtualPath = AppRelativeVirtualPath;
    }
}
```

&nbsp;
============
&copy; [The Best Software Engineer In The Universe!](https://www.linkedin.com/in/metlinskyi/)
