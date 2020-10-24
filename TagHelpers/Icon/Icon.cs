/* 
    @Date           : 25.09.2019
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
using System;
using System.Threading.Tasks;

namespace Creator.Components.TagHelpers.Icon
{
    /// <summary>
    /// Displays icons from FontAwesome, Friconix, Captain Icon and DevIcons
    /// </summary>
    [HtmlTargetElement("icon", Attributes = "awesome", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("icon", Attributes = "friconix", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("icon", Attributes = "captain", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("icon", Attributes = "devicon", TagStructure = TagStructure.WithoutEndTag)]
    public sealed class IconTagHelper : TagHelperCustom, ITagHelperCustom
    {
        private readonly IIconHelper _iconHelper = new IconHelper();

        public IconTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {

        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.PreProcessAsync(context, output);

            if (this.AwesomeSymbol != FontAwesomeIcon.None)
            {
                AddContent(GetFontAwesomeTag());
            }
            else if (this.FriconixSymbol != FriconixIcon.None)
            {
                AddContent(GetFriconixTag());
            }
            else if (this.CaptainIconSymbol != CaptainIcon.None)
            {
                AddContent(GetCaptainIconTag());
            }
            else if (this.DevIconSymbol != DevIcon.None)
            {
                AddContent(GetDevIconTag());
            }
            else
            {
                throw new ArgumentException("No symbol property is set");
            }

            await base.ProcessCustomAsync();
        }

        private ITagBuilderCustom GetDevIconTag()
        {
            if (this.DevIconSymbol == DevIcon.None)
            {
                throw new ArgumentException("DevIcon icon not set");
            }

            IIconRecord icon = _iconHelper.GetIcon(this.DevIconSymbol);
            ITagBuilderCustom tag = new TagBuilderCustom("i", TagRenderMode.Normal, false);
            tag.AddCssClassRange("di-icon", $"devicon-{icon.Name}", $"size-{this.Size}");

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetFontAwesomeTag()
        {
            IIconRecord icon = _iconHelper.GetIcon(this.AwesomeSymbol);

            if (this.AwesomeSymbol == FontAwesomeIcon.None)
            {
                throw new ArgumentException("FontAwesome icon not set");
            }

            ITagBuilderCustom tag = new TagBuilderCustom("i", TagRenderMode.Normal, false);
            tag.AddCssClassRange("fa-icon", icon.FontAwesomePrefix, $"fa-{icon.Name.ToLower()}", _iconHelper.GetSize(this.Size).FontAwesomeSize);

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetFriconixTag()
        {
            IIconRecord icon = _iconHelper.GetIcon(this.FriconixSymbol);

            if (this.FriconixSymbol == FriconixIcon.None)
            {
                throw new ArgumentException("Friconix icon not set");
            }

            ITagBuilderCustom tag = new TagBuilderCustom("i", TagRenderMode.Normal, false);
            tag.AddCssClass("fi-icon");
            tag.AddCssClass($"fi-{GetShape(this.Shape)}{GetThickness(this.Thickness)}{GetStyle(this.FriconixStyle)}{GetDirection(this.Direction)}{GetEffect(this.Effect)}{_iconHelper.GetSize(this.Size).FriconixSize}-{icon.Name.ToLower()}");

            #region Misc Friconix functions
            char GetShape(FriconixShape shape)
            {
                char result = 'x';

                switch (shape)
                {
                    case FriconixShape.Triangle:
                        result = 't';
                        break;

                    case FriconixShape.Equilateral:
                        result = 'e';
                        break;

                    case FriconixShape.Circle:
                        result = 'c';
                        break;

                    case FriconixShape.Square:
                        result = 's';
                        break;

                    case FriconixShape.Hexagon:
                        result = 'h';
                        break;

                    case FriconixShape.Octagon:
                        result = 'o';
                        break;
                }

                return result;
            }

            char GetThickness(FriconixThickness thickness)
            {
                char result = 'x';

                switch (thickness)
                {
                    case FriconixThickness.Thin:
                        result = 't';
                        break;

                    case FriconixThickness.Normal:
                        result = 'n';
                        break;

                    case FriconixThickness.Wide:
                        result = 'w';
                        break;
                }

                return result;
            }

            char GetStyle(FriconixStyle style)
            {
                char result = 'x';

                switch (style)
                {
                    case FriconixStyle.Line:
                        result = 'l';
                        break;

                    case FriconixStyle.Solid:
                        result = 's';
                        break;

                    case FriconixStyle.Prohibited:
                        result = 'p';
                        break;
                }

                return result;
            }

            char GetDirection(FriconixDirection direction)
            {
                char result = 'x';

                switch (direction)
                {
                    case FriconixDirection.Up:
                        result = 'u';
                        break;

                    case FriconixDirection.Down:
                        result = 'd';
                        break;

                    case FriconixDirection.Left:
                        result = 'l';
                        break;

                    case FriconixDirection.Right:
                        result = 'r';
                        break;
                }

                return result;
            }

            char GetEffect(FriconixEffect effect)
            {
                char result = 'x';

                switch (effect)
                {
                    case FriconixEffect.Horizontal:
                        result = 'h';
                        break;

                    case FriconixEffect.Vertical:
                        result = 'v';
                        break;

                    case FriconixEffect.Spin:
                        result = 's';
                        break;

                    case FriconixEffect.Pulse:
                        result = 'p';
                        break;
                }

                return result;
            }
            #endregion

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetCaptainIconTag()
        {
            IIconRecord icon = _iconHelper.GetIcon(this.CaptainIconSymbol);

            if (this.CaptainIconSymbol == CaptainIcon.None)
            {
                throw new ArgumentException("Captain Icon not set");
            }

            ITagBuilderCustom tag = new TagBuilderCustom("i");
            tag.AddCssClass("ci-icon");
            tag.AddCssClass($"ci-icon-{_iconHelper.GetCaptainIconName(icon.CaptainIcon)}");
            tag.AddAttribute("style", $"font-size: {_iconHelper.GetSize(this.Size).CaptainIconSize};", false);

            return GetWrapTag(tag);
        }

        private ITagBuilderCustom GetWrapTag(ITagBuilderCustom iconTag)
        {
            ITagBuilderCustom result = new TagBuilderCustom(_iconHelper.GetFormatTagName(this.Format), false);
            result.AddCssClassRange("creator-icon", "flex-center-center", this.GetColorProfile());
            result.ConsumeAttributes = true;
            result.AddChild(iconTag);


            if (this.Format == IconOutputFormat.Anchor)
            {
                result = _iconHelper.ProcessAnchorTag(result, this.Href, this.Target);
            }

            if (!this.Text.Null())
            {
                ITagBuilderCustom txt = new TagBuilderCustom("span");
                txt.AddCssClass("icon-text");
                txt.AddCssClass("mrg-sm-left");
                txt.InnerHtml.SetHtmlContent(this.Text);
                result.InnerHtml.AppendHtml(txt);
            }

            return result;
        }

        private string GetColorProfile()
        {
            string color = this.ColorProfile.ToString();

            switch (this.AwesomeSymbol)
            {
                case FontAwesomeIcon.Close:
                case FontAwesomeIcon.CloseFull:
                case FontAwesomeIcon.CloseCircle:
                case FontAwesomeIcon.CloseCircleFull:
                    color = CreatorColorProfiles.Danger.ToString();
                    break;
            }

            return "cp-" + color.ToLower();
        }

        #region Properties
        /// <summary>
        /// Name of the icon to use with Type equal FontAwesome
        /// </summary>
        [HtmlAttributeName("awesome")]
        public FontAwesomeIcon AwesomeSymbol { get; set; } = FontAwesomeIcon.None;

        /// <summary>
        /// Name of icon in the Friconix set of icons. Overrides FontAwesome if set
        /// </summary>
        [HtmlAttributeName("friconix")]
        public FriconixIcon FriconixSymbol { get; set; } = FriconixIcon.None;

        /// <summary>
        /// Name of icon in the Captain Icon set of icons. Overrides FontAwesome and Friconix if set
        /// </summary>
        [HtmlAttributeName("captain")]
        public CaptainIcon CaptainIconSymbol { get; set; } = CaptainIcon.None;

        [HtmlAttributeName("devicon")]
        public DevIcon DevIconSymbol { get; set; } = DevIcon.None;

        /// <summary>
        /// Text to appear to the right of the icon
        /// </summary>
        [HtmlAttributeName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Output format of Font Awesome icon
        /// </summary>
        [HtmlAttributeName("format")]
        public IconOutputFormat Format { get; set; } = IconOutputFormat.Italic;

        /// <summary>
        /// The method of button if Symbol Output is button
        /// </summary>
        [HtmlAttributeName("form-method")]
        public TagFormMethod FormMethod { get; set; } = TagFormMethod.Post;

        /// <summary>
        /// Indicates if to wrap the tag in a form when output format is set to Button
        /// </summary>
        /// <remarks>Default is true</remarks>
        [HtmlAttributeName("create-form")]
        public bool CreateForm { get; set; } = true;

        /// <summary>
        /// The color profile to apply the symbol
        /// </summary>
        [HtmlAttributeName("color")]
        public CreatorColorProfiles ColorProfile { get; set; } = CreatorColorProfiles.Default;

        /// <summary>
        /// Type of button if format is button
        /// </summary>
        [HtmlAttributeName("button")]
        public TagButtonType ButtonType { get; set; } = TagButtonType.Submit;

        /// <summary>
        /// Anchor href value if format is anchor
        /// </summary>
        [HtmlAttributeName("href")]
        public string Href { get; set; }

        /// <summary>
        /// Anchor target value if format er anchor
        /// </summary>
        [HtmlAttributeName("target")]
        public TagAnchorTarget Target { get; set; } = TagAnchorTarget.None;

        /// <summary>
        /// The shape of a Friconix icon
        /// </summary>
        [HtmlAttributeName("shape")]
        public FriconixShape Shape { get; set; } = FriconixShape.None;

        /// <summary>
        /// The direction of a Friconix icon
        /// </summary>
        [HtmlAttributeName("direction")]
        public FriconixDirection Direction { get; set; } = FriconixDirection.None;

        /// <summary>
        /// The style of a Friconix icon
        /// </summary>
        [HtmlAttributeName("friconix-style")]
        public FriconixStyle FriconixStyle { get; set; } = FriconixStyle.None;

        /// <summary>
        /// The thickness of a Friconix icon
        /// </summary>
        [HtmlAttributeName("thickness")]
        public FriconixThickness Thickness { get; set; } = FriconixThickness.None;

        /// <summary>
        /// The effect of a Friconix icon
        /// </summary>
        [HtmlAttributeName("effect")]
        public FriconixEffect Effect { get; set; } = FriconixEffect.None;

        /// <summary>
        /// Icon size
        /// </summary>
        [HtmlAttributeName("size")]
        public IconSize Size { get; set; } = IconSize.Normal;
        #endregion
    }
}
