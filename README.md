# web-forms
Asp.Net Web Froms localization MVP




RouteConfig.cs

////

    var handler = new CultureRouteHandler("culture", "page")
    {
        WebRoot = "~/Pages/",
    };

    routes.Add(new Route(string.Empty, handler));
    routes.Add(new Route("{culture}/{*page}", handler));
///