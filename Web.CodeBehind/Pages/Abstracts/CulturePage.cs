using System;
using System.Globalization;
using System.Web.UI;

namespace Web.CodeBehind.Pages.Abstracts
{
    /// <summary>
    /// Localized base <see cref="Page"/> 
    /// </summary>
    public abstract class CulturePage : Page
    {
        protected override void InitializeCulture()
        {
            base.InitializeCulture();

            // Set current culture name like en-US
            Culture = UICulture = CultureInfo.CurrentCulture.Name;
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Controls.CultureHandling(CultureInfo.CurrentCulture.Name.ToLower());

            base.OnPreRenderComplete(e);
        }

    }
}
