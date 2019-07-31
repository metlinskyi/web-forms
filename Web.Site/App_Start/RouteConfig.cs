using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;
using Web.Localization.Routing;

namespace Web.Site
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // The static resources is ignored.
            routes.Ignore("{assets}", new { assets = @".*\.(css|js|gif|jpg)(/.)?" });
            routes.Ignore("bundles/{*name}");

            // IRouteHandler implementation for routing pages with language difference.
            var handler = new CultureRouteHandler("culture", "page")
            {
                WebRoot = "~/Pages/",
            };

            // Default route, is redirected to a default culture url like /en-us/.
            routes.Add(new Route(string.Empty, handler));

            // Page route, for typical pages like /en-us/account (~/Pages/Account.aspx).
            routes.Add(new Route("{culture}/{*page}", handler));

            routes.EnableFriendlyUrls(new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            });
        }
    }
}
