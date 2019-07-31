using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web.Localization.UI
{
    /// <summary>
    /// Localization helper
    /// </summary>
    public static class CultureHelper
    {
        /// <summary>
        /// Attributes with the culture tag (by default "{culture}").
        /// </summary>
        private static readonly string[] _attributes = new[] { "href", "src" };

        /// <summary>
        /// Localization handling
        /// </summary>
        /// <param name="controls">controls for handling</param>
        /// <param name="culture">current culture</param>
        public static void CultureHandling(this ControlCollection controls, string culture, string tag = "{culture}")
        {
            AttributeCollection attributes;

            foreach (Control ctr in controls)
            {
                // recursive search
                if (ctr.HasControls())
                    ctr.Controls.CultureHandling(culture, tag);

                switch (ctr)
                {
                    case WebControl web:
                        {
                            attributes = web.HasAttributes ? web.Attributes : null;
                        }
                        break;

                    case HtmlControl html:
                        {
                            attributes = html.Attributes.Count > 0 ? html.Attributes : null;
                        }
                        break;

                    default:
                        {
                            attributes = null;
                        }
                        continue;
                }

                if (attributes == null)
                    continue;

                // Get the only attributes of Control from the _attributes array.
                foreach (var key in attributes.Keys.OfType<string>().Intersect(_attributes))
                {
                    var value = attributes[key];
                    if (value.Contains(tag))
                        attributes[key] = value.Replace(tag, culture);
                }
            }
        }
    }
}
