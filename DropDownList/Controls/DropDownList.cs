using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Controls
{
    /// <summary>
    /// Custom DropDownList implementation example.
    /// 
    /// The main idea is to extend default DropDownList and override its render method.
    /// To make it work as default DropDownList we also render hidden field which
    /// will contain selected value and will be accessible via back end.
    /// 
    /// IMPORTANT: Add references for System.Web, System.Web.Extensions, System.Design, System.Drawing
    /// </summary>
    [ToolboxData("<{0}:DropDownList runat=server></{0}:DropDownList>")]
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        /// <summary>
        /// Overridden render method
        /// 
        /// We are going to render span with value,
        /// unordered list with available options
        /// and hidden field with selected value
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("span");
            Attributes["class"] = String.Format("{0} ddl", CssClass).Trim();
            foreach (string key in Attributes.Keys)
            {
                writer.WriteAttribute(key, Attributes[key]);
            }
            writer.Write(HtmlTextWriter.TagRightChar);

            RenderValue(writer);
            RenderOptions(writer);
            RenderHiddenField(writer);

            writer.WriteEndTag("span");
        }

        /// <summary>
        /// Render selected value
        /// </summary>
        /// <param name="writer"></param>
        protected void RenderValue(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("span");
            writer.WriteAttribute("class", "value");
            writer.Write(HtmlTextWriter.TagRightChar);
            if (SelectedItem != null)
            {
                writer.Write(SelectedItem.Text);
            }
            writer.WriteEndTag("span");
        }

        /// <summary>
        /// Render available options as unordered list
        /// </summary>
        /// <param name="writer"></param>
        protected void RenderOptions(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("ul");
            writer.WriteAttribute("class", "options");
            writer.Write(HtmlTextWriter.TagRightChar);
            foreach (ListItem item in Items)
            {
                RenderOption(item, writer);
            }
            writer.WriteEndTag("ul");
        }

        /// <summary>
        /// Render option
        /// </summary>
        /// <param name="item"></param>
        /// <param name="writer"></param>
        protected void RenderOption(ListItem item, HtmlTextWriter writer)
        {
            item.Attributes["class"] = String.Format("{0} option {1}", item.Attributes["class"], item.Selected ? "active" : "").Trim();

            writer.WriteBeginTag("li");
            writer.WriteAttribute("data-value", item.Value);
            foreach (string key in item.Attributes.Keys)
            {
                writer.WriteAttribute(key, item.Attributes[key]);
            }
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.Write(item.Text);
            writer.WriteEndTag("li");
        }

        /// <summary>
        /// Hidden field for selected value
        /// 
        /// Notice how we are using UniqueID and ClientID - this will allow us to access selected value at back end
        /// </summary>
        /// <param name="writer"></param>
        protected void RenderHiddenField(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("input");
            writer.WriteAttribute("type", "hidden");
            writer.WriteAttribute("name", UniqueID);
            writer.WriteAttribute("id", ClientID);
            writer.WriteAttribute("value", SelectedValue);
            writer.WriteAttribute("class", String.Format("{0} hidden", CssClass).Trim());
            writer.Write(HtmlTextWriter.SelfClosingTagEnd);
        }

        /// <summary>
        /// Embed scripts and styles
        /// 
        /// We are going to embed our scripts and styles into page.
        /// Notice that they will be included only once - no matter how much controls there is on page.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            EmbedStylesheet();
            EmbedJavascript();
        }

        /// <summary>
        /// Embed styles
        /// 
        /// IMPORTANT: Do not forget to add [assembly: System.Web.UI.WebResource("Controls.DropDownList.css", "text/css")] to AssemblyInfo.cs
        /// IMPORTANT: Do not forget to prefix file name with assembly namespace like so "Controls.DropDownList.css"
        /// IMPORTANT: Do not forget to set "Build Action" for file as "Embedded Resource"
        /// NOTICE: Notice typeof(Controls.DropDownList) it used to avoid troubles with controls that may extend our custom control
        /// </summary>
        protected void EmbedStylesheet()
        {
            var link = new HtmlLink();
            link.Href = Page.ClientScript.GetWebResourceUrl(typeof(Controls.DropDownList), "Controls.DropDownList.css");
            link.Attributes.Add("rel", "stylesheet");
            Page.Header.Controls.Add(link);
        }

        /// <summary>
        /// Embed scripts
        /// 
        /// IMPORTANT: Do not forget to add [assembly: System.Web.UI.WebResource("Controls.DropDownList.js", "text/javascript")] to AssemblyInfo.cs
        /// IMPORTANT: Do not forget to prefix file name with assembly namespace like so "Controls.DropDownList.js"
        /// IMPORTANT: Do not forget to set "Build Action" for file as "Embedded Resource"
        /// NOTICE: Notice typeof(Controls.DropDownList) it used to avoid troubles with controls that may extend our custom control
        /// </summary>
        protected void EmbedJavascript()
        {
            Page.ClientScript.RegisterClientScriptResource(typeof(Controls.DropDownList), "Controls.DropDownList.js");
        }
    }
}
