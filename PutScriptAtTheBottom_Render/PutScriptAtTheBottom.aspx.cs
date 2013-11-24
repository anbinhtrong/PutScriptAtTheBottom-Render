using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PutScriptAtTheBottom_Render
{
    public partial class PutScriptAtTheBottom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            //html minifier & JS at bottom
            // tested .NET 4.0
            if (this.Request.Headers["X-MicrosoftAjax"] != "Delta=true")
            {
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"<script[^>]*>[\w|\t|\r|\W]*?</script>");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                System.IO.StringWriter sw = new System.IO.StringWriter(sb);
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                base.Render(hw);
                string html = sb.ToString();
                System.Text.RegularExpressions.MatchCollection mymatch = reg.Matches(html);
                html = reg.Replace(html, string.Empty);
                reg = new System.Text.RegularExpressions.Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}|(?=[\r])\s{2,}");
                html = reg.Replace(html, string.Empty);
                reg = new System.Text.RegularExpressions.Regex(@"</body>");
                string str = string.Empty;
                foreach (System.Text.RegularExpressions.Match match in mymatch)
                {
                    str += match.ToString();
                }
                html = reg.Replace(html, str + "</body>");
                writer.Write(html);
            }
            else
                base.Render(writer);
        }
    }
}