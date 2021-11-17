/*
    @Date			              : 28.08.2020
    @Author                       : Stein Lundbeck
*/

using Creator.Components;
using LundbeckConsulting.Components.Core.Components;
using LundbeckConsulting.Components.Core.Components.Repos;
using LundbeckConsulting.Components.Core.Components.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Creator.TagHelpers
{
    [HtmlTargetElement("bars", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class BarsMenuTagHelper : TagHelperCustom, ITagHelperCustom
    {
        public BarsMenuTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        { }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcessAsync(context, output);

            TagHelperContent inner = await output.GetChildContentAsync();
            TagBuilderCustom burger = new TagBuilderCustom("i");

            burger.InnerHtml.SetHtmlContent(inner.ToHtmlString());
            burger.AddAttribute("id", "creatorBarsMenu");
            burger.AddCssClassRange("creator-bars-icon", $"size-{this.Size.ToLower()}");

            AddContent(burger);

            await base.ProcessAsync();
        }

        [HtmlAttributeName("size")]
        public CommonSizes Size { get; set; } = CommonSizes.MD;
    }
}
