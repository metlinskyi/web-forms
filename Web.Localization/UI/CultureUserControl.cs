using System;
using System.Globalization;
using System.Web.UI;

namespace Web.Localization.UI
{
    /// <summary>
    /// Localized base <see cref="UserControl"/> 
    /// </summary>
    public abstract class CultureUserControl : UserControl
    {
        protected override void Construct()
        {
            base.Construct();
        }

        protected override void OnInit(EventArgs e)
        {
            // UserControl without Template
            if (string.IsNullOrEmpty(AppRelativeVirtualPath))
            {
                return;
            }

            ITemplate template = null;

            var culture = CultureInfo.CurrentCulture.Name;

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

            if (template == null && string.CompareOrdinal(appRelativeVirtualPath, AppRelativeVirtualPath) != 0)
            {
                template = LoadTemplate(appRelativeVirtualPath);
            }

            // Bind template with the UserControl
            if (template != null)
            {
                //template.InstantiateIn(this);
            }

            base.OnInit(e);

        }

    }
}
