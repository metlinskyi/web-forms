using System.Globalization;
using System.Web.UI;

namespace Web.CodeBehind.Controls.Abstracts
{
    public abstract class CultureUserControl : System.Web.UI.UserControl
    {
        public override void RenderControl(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(AppRelativeVirtualPath))
            {
                base.RenderControl(writer);
                return;
            }

            ITemplate template = null;

            var culture = CultureInfo.CurrentCulture.Name;

            var key = string.Concat(culture, AppRelativeVirtualPath);

            var appRelativeVirtualPath = Cache[key] as string;

            if (string.IsNullOrEmpty(appRelativeVirtualPath))
            {
                appRelativeVirtualPath = AppRelativeVirtualPath.Replace(".ascx", $".{culture}.ascx");

                try
                {
                    template = Page.LoadTemplate(appRelativeVirtualPath);

                    Cache[key] = appRelativeVirtualPath;
                }
                catch
                {
                    Cache[key] = appRelativeVirtualPath = AppRelativeVirtualPath;
                }
            }

            if (template == null && string.CompareOrdinal(appRelativeVirtualPath, AppRelativeVirtualPath) != 0)
            {
                template = Page.LoadTemplate(appRelativeVirtualPath);
            }

            if (template != null)
            {
                template.InstantiateIn(this);
            }

            base.RenderControl(writer);
        }
    }
}
