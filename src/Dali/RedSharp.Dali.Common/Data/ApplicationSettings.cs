namespace RedSharp.Dali.Common.Data
{
    /// <summary>
    /// Class to store application settings. Have correct default values.
    /// </summary>
    /// <remarks>
    /// Should not contain any logic.
    /// </remarks>
    public sealed class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets shortcut to enable input transparency on Work window.
        /// </summary>
        public Shortcut TransparenceShortcut { get; set; } = new Shortcut(Enums.KeyEnum.T, Enums.HotkeyModifier.Alt);

        /// <summary>
        /// Gets or sets shortcut to close Work window.
        /// </summary>
        public Shortcut CloseTransparentWindowShortcut { get; set; } = new Shortcut(Enums.KeyEnum.C, Enums.HotkeyModifier.Alt);
    }
}
