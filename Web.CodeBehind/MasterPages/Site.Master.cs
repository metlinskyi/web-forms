using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.CodeBehind.MasterPages
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Culture_Click(object sender, EventArgs e)
        {
            var ctr = (LinkButton)sender;
            var current = Request.Url.Segments[1].Trim(new[] { '/' });

            Response.Redirect(Request.Url.ToString().Replace(current, ctr.CommandArgument.ToLower()));
        }
    }
}