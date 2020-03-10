namespace RedSharp.Dali.Common.Data
{
    public sealed class ApplicationSettings
    {
        public Shortcut TransparenceShortcut { get; set; } = new Shortcut(Enums.KeyEnum.T, Enums.HotkeyModifier.Alt);
        public Shortcut CloseTransparentWindowShortcut { get; set; } = new Shortcut(Enums.KeyEnum.C, Enums.HotkeyModifier.Alt);
    }
}
