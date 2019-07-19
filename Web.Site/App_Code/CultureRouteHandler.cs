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
    public class CultureRouteHandler : IRouteHandler
    {
        private readonly string _cultureKey;

        private readonly string _pageKey;

        private readonly IDictionary<(string, string), string> _chache;

        public string WebRoot { get; set; } = "~/";

        public string DefaultPage { get; set; } = "Default";

        public int StatusCode { get; set; } = 301;

        public CultureRouteHandler(string cultureKey, string pageKey)
        {
            _cultureKey = cultureKey;
            _pageKey = pageKey;
            _chache = new ConcurrentDictionary<(string, string), string>();
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            IHttpHandler handler = null;

            var culture = requestContext.RouteData.Values[_cultureKey] as string;
            if (string.IsNullOrEmpty(culture))
            {
                HttpContext.Current.Response.Redirect(CultureInfo.CurrentCulture.Name.ToLower() + "/", false);
                HttpContext.Current.Response.StatusCode = StatusCode;
                HttpContext.Current.Response.End();
            }

            var page = requestContext.RouteData.Values[_pageKey] as string;
            if (string.IsNullOrEmpty(page))
            {
                page = DefaultPage;
            }

            CultureInfo.CurrentCulture = new CultureInfo(culture, false);
            culture = CultureInfo.CurrentCulture.Name;

            StringBuilder virtualPathBuilder = null;

            var key = (culture, page);
            if (!_chache.TryGetValue(key, out string virtualPath))
            {
                virtualPathBuilder = new StringBuilder(WebRoot)
                        .Append(page)
                        .Append(".")
                        .Append(culture)
                        .Append(".aspx");

                virtualPath = virtualPathBuilder.ToString();
            }

            try
            {
                handler = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page)) as IHttpHandler;
            }
            catch
            {
                virtualPath = virtualPathBuilder
                    .Replace(culture, string.Empty)
                    .Replace("..", ".")
                    .ToString();
                try
                {
                    handler = BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(Page)) as IHttpHandler;
                }
                catch
                {
                    //TODO: A logger should be added 
                }
            }
            finally
            {
                if (handler == null)
                {
#if (!DEBUG)
                    HttpContext.Current.Response.StatusCode = 404;
                    HttpContext.Current.Response.End();
#endif
                }

                if (handler != null && virtualPathBuilder != null)
                {
                    _chache.Add(key, virtualPath);
                }

            }

            return handler;
        }
    }
}