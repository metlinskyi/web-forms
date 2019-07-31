using System;
using System.Web.UI;

namespace Web.Localization.UI
{
    [ToolboxData("<{0}:Localization runat=\"server\"></{0}>")]
    [ParseChildren(true, "Templates")]
    [PersistChildren(false)]
    public class Localization : Control, ITemplate
    {
        public Localization()
        {
            Templates = new TemplateCollection();
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TemplateCollection Templates { get; set; }

        /// <summary>
        /// Find a template with the given name.
        /// Returns null if there is no such template.
        /// </summary>
        public ITemplate FindTemplate(string name)
        {
            ITemplate @default = null;

            foreach (var template in Templates)
            {
                if (template.Name == name)
                {
                    return template;
                }

                if (string.IsNullOrEmpty(template.Name))
                {
                    @default = template;
                }
            }

            return @default;
        }

        public void InstantiateIn(Control container)
        {
            var template = FindTemplate(System.Globalization.CultureInfo.CurrentCulture.Name);
            if (template != null)
            {
                template.InstantiateIn(container);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InstantiateIn(this);
        }
    }
}
