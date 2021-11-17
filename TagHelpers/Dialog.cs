/*
    @Date			: 10.12.2019
    @Author         : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Components;
using LundbeckConsulting.Components.Core.Components.Repos;
using LundbeckConsulting.Components.Core.Components.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Creator.Components.TagHelpers
{
    /// <summary>
    /// Implements the Dialog element from Creator
    /// </summary>
    [HtmlTargetElement("dialog", Attributes = "[size], [title]", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DialogTagHelper : TagHelperCustom, ITagHelperCustom
    {
        public DialogTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcessAsync(context, output);

            output.Content.AppendLine(GetTag());

            await base.ProcessAsync();
        }

        private TagBuilderCustom GetTag()
        {
            TagBuilderCustom result = new TagBuilderCustom("dialog");
            TagBuilder content = new TagBuilder("content");
            TagBuilder header = new TagBuilder("header");
            TagBuilder title = new TagBuilder("span");
            TagBuilder closeIcon = new TagBuilder("i");
            TagBuilder body = new TagBuilder("section");
            TagBuilder okCommand = new TagBuilder("button");

            result.AddAttribute("role", "dialog");
            result.AddAttribute("aria-labelledby", "title");

            if (!this.Description.Null())
            {
                result.AddAttribute("aria-describedby", this.Description);
            }

            title.AddCssClass("title");
            title.InnerHtml.Append(this.SiteTitle);
            closeIcon.AddCssClass("close-command");
            body.AddCssClass("body");
            okCommand.AddCssClass("ok-command");
            okCommand.InnerHtml.Append("Ok");

            if (this.SolidHeaderColor)
            {
                header.AddCssClass("solid");
            }

            title.InnerHtml.Append(this.SiteTitle);
            title.AddCssClass("title");
            header.InnerHtml.AppendHtml(title);
            header.InnerHtml.AppendHtml(closeIcon);
            content.InnerHtml.AppendHtml(header);
            content.InnerHtml.AppendHtml(body);
            result.InnerHtml.AppendHtml(content);

            if (this.Show)
            {
                result.AddAttribute("open", "open");
            }

            result.AddCssClass("dialog-" + this.Color.ToLower() + "-" + this.Size.ToLower());

            return result;
        }

        /// <summary>
        /// Color of dialog
        /// </summary>
        [HtmlAttributeName("color")]
        public CreatorColorProfiles Color { get; set; } = CreatorColorProfiles.Default;

        /// <summary>
        /// If true the default header with gradient is not used
        /// </summary>
        [HtmlAttributeName("solid-header")]
        public bool SolidHeaderColor { get; set; } = false;

        /// <summary>
        /// Size of dialog
        /// </summary>
        [HtmlAttributeName("size")]
        public DialogSizes Size { get; set; }

        /// <summary>
        /// Title of dialog
        /// </summary>
        [HtmlAttributeName("title")]
        public new string SiteTitle { get; set; } = default;

        /// <summary>
        /// If true the dialog will be visible on load
        /// </summary>
        [HtmlAttributeName("show")]
        public bool Show { get; set; } = false;

        [HtmlAttributeName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The text on the Ok button
        /// </summary>
        [HtmlAttributeName("ok-button-text")]
        public string OkButtonText { get; set; } = "Ok";
    }
}
