namespace Blog.Bases.Settings
{
    public sealed class AppSettings
    {
        public string DefaultLanguage { get; set; }
        public TranslateResources TranslateResources { get; set; }
    }

    public sealed class TranslateResources
    {
        public Language[] Languages { get; set; }
    }

    public sealed class Language
    {
        public string Culture { get; set; }
        public string Path { get; set; }
    }
}
