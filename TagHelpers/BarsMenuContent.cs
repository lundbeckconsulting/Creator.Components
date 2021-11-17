/*
    @Date			              : 28.08.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Components.Repos;
using LundbeckConsulting.Components.Core.Components.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Creator.Components.TagHelpers
{
    [HtmlTargetElement("nav", ParentTag = "bars", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class BarsMenuContentTagHelper : TagHelperCustom, ITagHelperCustom
    {
        public BarsMenuContentTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {
            
        }

        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            var content = await output.GetChildContentAsync();
            string cont = "\t" + handleItem(content.GetContent().ToString());

            output.TagName = "nav";
            output.Attributes.Add("class", "bars-nav");
            output.Content.SetHtmlContent(cont);

            string handleItem(string str) 
            {
                str = "\t\n" + str.Trim();

                if (str.IndexOf(",") > 0 && this.AppendBreak)
                {
                    str = str.Replace(",", $"\n{GetBreakTag(this.BreakTagName)}\n");
                }

                if (str.IndexOf("<a") > 0)
                {
                    str = str.Replace("<a", "<a class=\"item\" ");
                }
                else if (str.IndexOf("<input") > 0)
                {
                    str = str.Replace("<input", $"<input class=\"item\" ");
                }
                else if (str.IndexOf("<button") > 0)
                {
                    str = str.Replace("<button", $"<button class=\"item\" ");
                }
                else if (str.IndexOf("<img") > 0)
                {
                    str = str.Replace("<img", "<img class=\"item\" ");
                }

                str += "\n";

                return str;
            }

            static string GetBreakTag(BreakTag tag)
            {
                string result = tag switch
                {
                    BreakTag.br => "<br />",
                    BreakTag.comma => ", ",
                    BreakTag.line => "\n",
                    BreakTag.semicolon => "; ",
                    _ => throw new ArgumentException($"{tag.ToLower()} not supported"),
                };

                return result;
            }
        }

        /// <summary>
        /// Indicates if a Break line will be appended between each menu item
        /// </summary>
        [HtmlAttributeName("append-break")]
        public bool AppendBreak { get; set; } = true;

        /// <summary>
        /// The value to use as separator
        /// </summary>
        [HtmlAttributeName("break")]
        public BreakTag BreakTagName { get; set; } = BreakTag.br;
    }
}

public enum BreakTag { 
    br,
    comma,
    semicolon,
    line
}