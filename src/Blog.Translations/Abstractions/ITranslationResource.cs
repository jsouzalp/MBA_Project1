namespace Blog.Translations.Abstractions
{
    public interface ITranslationResource
    {
        string GetResource(string key);
        string GetCodeResource(string path);
    }
}
