/*
    @Date			              : 23.06.2020
    @Author                       : Stein Lundbeck
*/

namespace Creator.Components.TagHelpers.Icon
{
    public interface IIconRecord
    {
        /// <summary>
        /// Icon name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Prefix for FontAwesome icons
        /// </summary>
        string FontAwesomePrefix { get; set; }

        /// <summary>
        /// Code for DevIcon icons
        /// </summary>
        string DevIconCode { get; set; }

        /// <summary>
        /// Friconix icon
        /// </summary>
        FriconixIcon FriconixIcon { get; set; }

        /// <summary>
        /// FontAwesome icon
        /// </summary>
        FontAwesomeIcon FontAwesomeIcon { get; set; }

        /// <summary>
        /// Captain Icon icon
        /// </summary>
        CaptainIcon CaptainIcon { get; set; }

        /// <summary>
        /// Code for Captain Icon
        /// </summary>
        string CaptainIconCode { get; set; }

        /// <summary>
        /// DevIcon icon
        /// </summary>
        DevIcon DevIcon { get; set; }
    }

    /// <summary>
    /// Contains information about either a FontAwesome or Friconix icon
    /// </summary>
    public sealed class IconRecord : IIconRecord
    {
        /// <summary>
        /// Creates a new FontAwesome record
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <param name="name">Name of icon</param>
        /// <param name="fontAwesomePrefix">Icon prefix</param>
        public IconRecord(FontAwesomeIcon icon, string name, string fontAwesomePrefix = "fas")
        {
            this.Name = name;
            this.FontAwesomePrefix = fontAwesomePrefix;
            this.FontAwesomeIcon = icon;
        }

        /// <summary>
        /// Creates a new Friconix record
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <param name="name">Name of icon</param>
        public IconRecord(FriconixIcon icon, string name)
        {
            this.Name = name;
            this.FriconixIcon = icon;
        }

        /// <summary>
        /// Create a new Captain Icon record
        /// </summary>
        /// <param name="icon">Type of record</param>
        /// <param name="code">Icon code</param>
        public IconRecord(CaptainIcon icon, string name, string code)
        {
            this.CaptainIcon = icon;
            this.Name = name;
            this.CaptainIconCode = code;
        }

        /// <summary>
        /// Creates a new DevIcon record
        /// </summary>
        /// <param name="icon">Type of icon</param>
        /// <param name="name">Name of the icon</param>
        /// <param name="code">Icon code</param>
        public IconRecord(DevIcon icon, string name, string code)
        {
            this.DevIcon = icon;
            this.Name = name;
            this.DevIconCode = code;
        }

        public string Name { get; set; }
        public string FontAwesomePrefix { get; set; } = "fas";
        public string DevIconCode { get; set; }
        public FriconixIcon FriconixIcon { get; set; } = FriconixIcon.None;
        public FontAwesomeIcon FontAwesomeIcon { get; set; } = FontAwesomeIcon.None;
        public CaptainIcon CaptainIcon { get; set; } = CaptainIcon.None;
        public string CaptainIconCode { get; set; }
        public DevIcon DevIcon { get; set; } = DevIcon.None;
    }
}
