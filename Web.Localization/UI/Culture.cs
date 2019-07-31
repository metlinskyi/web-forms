using System.ComponentModel;
using System.Web.UI;

namespace Web.Localization.UI
{
    [ToolboxData("<{0}:Culture runat=\"server\"></{0}>")]
    [ParseChildren(true, "Content")]
    [PersistChildren(false)]
    public class Culture : Control, INamingContainer, ITemplate
    {
        [Browsable(true)]
        public string Name { get; set; }

        [Browsable(false)]
        [TemplateContainer(typeof(TemplateContainer), BindingDirection.TwoWay)]
        [TemplateInstance(TemplateInstance.Multiple)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate Content { get; set; }

        public void InstantiateIn(Control container)
        {
            Content.InstantiateIn(this);
            container.Controls.Add(this);
        }
    }
}
