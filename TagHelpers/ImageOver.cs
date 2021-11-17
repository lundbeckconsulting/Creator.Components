/*
    @Date			: 21.07.2020
    @Author         : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Components;
using LundbeckConsulting.Components.Core.Components.Repos;
using LundbeckConsulting.Components.Core.Components.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Creator.Components.TagHelpers
{
    [HtmlTargetElement("img-over", Attributes = "src", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ImageOverTagHelper : TagHelperCustom
    {
        public ImageOverTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {
            
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcessAsync(context, output);

            TagBuilderCustom tag = new TagBuilderCustom("img");
            tag.TagRenderMode = TagRenderMode.SelfClosing;
            tag.AddAttribute("src", this.Src);
            tag.Attributes.Add(GetScript(true));
            tag.Attributes.Add(GetScript(false));

            AddContent(tag);

            await base.ProcessAsync();
        }

        private string GetOverName() => this.SrcOver.Null() ? this.Src.Substring(0, this.Src.LastIndexOf(".")) + this.Suffix + this.Src.Substring(this.Src.LastIndexOf(".")) : this.SrcOver;

        private TagHelperAttribute GetScript(bool onOver)
        {
            string val;

            if (onOver)
            {
                val = "this.src='" + GetOverName() + "'";
            }
            else
            {
                val = "this.src='" + this.Src + "'";
            }

            return new TagHelperAttribute(onOver ? "onmouseover" : "onmouseout", val);
        }

        [HtmlAttributeName("src")]
        public string Src { get; set; }

        /// <summary>
        /// The suffix to add to Src. Default is "-over"
        /// </summary>
        /// <example>
        /// Src = "img.jpg", Suffix = "-alt". On over result: "img-alt.jpg"
        /// </example>
        [HtmlAttributeName("suffix")]
        public string Suffix { get; set; } = "-over";

        /// <summary>
        /// Overrides the default naming method and uses the supplied value
        /// </summary>
        [HtmlAttributeName("src-over")]
        public string SrcOver { get; set; }
    }
}
