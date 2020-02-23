namespace RedSharp.Dali.Common.Data
{
    public sealed class ApplicationSettings
    {
        public Shortcut TransparenceShortcut { get; set; } = new Shortcut(63, Enums.HotkeyModifier.Alt);
        public Shortcut CloseTransparentWindowShortcut { get; set; } = new Shortcut(46, Enums.HotkeyModifier.Alt);
    }
}
