using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;

namespace Web.Site
{
    /// <summary>
    /// <see cref="System.Web.Routing.IRouteHandler"/> implementation for routing pages with language difference.
    /// </summary>
    public class CultureRouteHandler : IRouteHandler
    {
        private readonly string _cultureKey;

        private readonly string _pageKey;

        // A cache provider, dictionary for simple example.
        private readonly IDictionary<(string, string), string> _chache;

        /// <summary>
        /// Gets or sets a root folder for pages, by default ~/ .
        /// </summary>
        public string WebRoot { get; set; } = "~/";

        /// <summary>
        /// Gets or sets a default page name, by default (Default.aspx)
        /// </summary>
        public string DefaultPage { get; set; } = "Default";

        /// <summary>
        /// Gets or sets redirect status code, by default 301. 
        /// </summary>
        public int StatusCode { get; set; } = 301;

        /// <summary>
        /// CultureRouteHandler constructor
        /// </summary>
        /// <param name="cultureKey">route data culture key</param>
        /// <param name="pageKey">route data page key</param>
        public CultureRouteHandler(string cultureKey, string pageKey) : this()
        {
            _cultureKey = cultureKey;
            _pageKey = pageKey;
        }

        private CultureRouteHandler()
        {
            _chache = new ConcurrentDictionary<(string, string), string>();
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            IHttpHandler handler = null;

            // Get a culture in the current url.
            var culture = requestContext.RouteData.Values[_cultureKey] as string;
            if (string.IsNullOrEmpty(culture))
            {
                HttpContext.Current.Response.Redirect(CultureInfo.CurrentCulture.Name.ToLower() + "/", false);
                HttpContext.Current.Response.StatusCode = StatusCode;
                HttpContext.Current.Response.End();
            }

            // Get a page path in the current url.
            var page = requestContext.RouteData.Values[_pageKey] as string;
            if (string.IsNullOrEmpty(page))
            {
                page = DefaultPage;
            }

            // Set a new culture for the thread.
            CultureInfo.CurrentCulture = new CultureInfo(culture, false);
            culture = CultureInfo.CurrentCulture.Name;

            StringBuilder virtualPathBuilder = null;

            // Trying to find a valid page path in a cache.
            var key = (culture, page);
            if (!_chache.TryGetValue(key, out string virtualPath))
            {
                // The path not found in cache, creating a new page path with culture from url.

                virtualPathBuilder = new StringBuilder(WebRoot)
                        .Append(page)
                        .Append(".")
                        .Append(culture)
                        .Append(".aspx");

                virtualPath = virtualPathBuilder.ToString();
            }

            try
            {
                // Trying to get the Page.
                handler = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page)) as IHttpHandler;
            }
            catch
            {
                // The Page not found, a culture will be removed from the page path.
                virtualPath = virtualPathBuilder
                    .Replace(culture, string.Empty)
                    .Replace("..", ".")
                    .ToString();

                try
                {
                    // Trying to get the Page again
                    handler = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page)) as IHttpHandler;
                }
                catch
                {
                    // 404 Page not found.
                    // TODO: A logger should be added.
                }
            }
            finally
            {
                // Unknown page or wrong url.
                if (handler == null)
                {
#if (!DEBUG)
                    HttpContext.Current.Response.StatusCode = 404;
                    HttpContext.Current.Response.End();
#endif
                }

                // The valid page path will be stored in cache.
                if (handler != null && virtualPathBuilder != null)
                {
                    _chache.Add(key, virtualPath);
                }
            }

            return handler;
        }
    }
}