using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace Web.Site
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{assets}", new { assets = @".*\.(css|js|gif|jpg)(/.)?" });
            routes.Ignore("bundles/{*name}");
            
            var handler = new CultureRouteHandler("culture", "page")
            {
                WebRoot = "~/Pages/",
            };

            routes.Add(new Route(string.Empty, handler));
            routes.Add(new Route("{culture}/{*page}", handler));

            routes.EnableFriendlyUrls(new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            });
        }
    }
}
