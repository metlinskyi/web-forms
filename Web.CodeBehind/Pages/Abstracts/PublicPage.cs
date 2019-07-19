using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web.CodeBehind.Pages.Abstracts
{
    public abstract class PublicPage : Page
    {
        private static readonly string[] _attrs = new[] { "href", "src" };

        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            Culture = UICulture = CultureInfo.CurrentCulture.Name;
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            CultureHandling(Controls, CultureInfo.CurrentCulture.Name.ToLower());

            base.OnPreRenderComplete(e);
        }

        private static void CultureHandling(ControlCollection controls, string culture)
        {
            foreach (Control ctr in controls)
            {
                if (ctr.HasControls())
                    CultureHandling(ctr.Controls, culture);

                AttributeCollection attributes;

                switch (ctr)
                {
                    case WebControl web:
                        {
                            attributes = web.HasAttributes ? web.Attributes : null;
                            break;
                        }

                    case HtmlControl html:
                        {
                            attributes = html.Attributes.Count > 0 ? html.Attributes : null;
                            break;
                        }
                    default:
                        continue;
                }

                if (attributes == null)
                    continue;

                foreach (var key in attributes.Keys.OfType<string>().Intersect(_attrs))
                {
                    var value = attributes[key];
                    if (value.Contains("{culture}"))
                        attributes[key] = value.Replace("{culture}", culture);
                }
            }
        }
    }
}
