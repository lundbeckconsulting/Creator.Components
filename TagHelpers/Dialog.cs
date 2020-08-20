/*
    @Date			: 10.12.2019
    @Author         : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core;
using LundbeckConsulting.Components.Core.Repos;
using LundbeckConsulting.Components.Core.TagHelpers;
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
            await base.PreProcess(context, output);

            output.Content.AppendLine(GetTag());

            await base.ProcessCustom();
        }

        private ITagBuilderCustom GetTag()
        {
            ITagBuilderCustom result = new TagBuilderCustom("dialog", ContentPosition.PostElement, TagRenderMode.Normal, true, new string[] { "id", "style", "class" }, false, true);
            TagBuilder content = new TagBuilder("content");
            TagBuilder header = new TagBuilder("header");
            TagBuilder title = new TagBuilder("span");
            TagBuilder closeIcon = new TagBuilder("i");
            TagBuilder body = new TagBuilder("section");
            TagBuilder okCommand = new TagBuilder("button");

            result.AddAttribute("role", "dialog", false);
            result.AddAttribute("aria-labelledby", "title", false);

            if (!this.Description.Null())
            {
                result.AddAttribute("aria-describedby", this.Description, false);
            }

            title.AddCssClass("title");
            title.InnerHtml.Append(this.Title);
            closeIcon.AddCssClass("close-command");
            body.InnerHtml.AppendHtml(this.InnerContent.GetContent());
            body.AddCssClass("body");
            okCommand.AddCssClass("ok-command");
            okCommand.InnerHtml.Append("Ok");

            if (this.SolidHeaderColor)
            {
                header.AddCssClass("solid");
            }

            title.InnerHtml.Append(this.Title);
            title.AddCssClass("title");
            header.InnerHtml.AppendHtml(title);
            header.InnerHtml.AppendHtml(closeIcon);
            content.InnerHtml.AppendHtml(header);
            content.InnerHtml.AppendHtml(body);
            result.InnerHtml.AppendHtml(content);

            if (this.Show)
            {
                result.AddAttribute("open", "open", false);
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
        public new string Title { get; set; } = default;

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
