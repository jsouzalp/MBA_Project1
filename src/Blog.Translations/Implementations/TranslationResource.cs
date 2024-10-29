using Blog.Bases.Constants;
using Blog.Bases.Settings;
using Blog.Translations.Abstractions;
using Blog.Translations.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blog.Translations.Implementations
{
    public class TranslationResource : ITranslationResource
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly Dictionary<string, List<Resource>> _translations;

        private string Language
        {
            get
            {
                StringValues languageValue = new();
                _httpContextAccessor?.HttpContext?.Request?.Headers?.TryGetValue(RequestConstant.Language, out languageValue);
                return (languageValue.FirstOrDefault() ?? _appSettings.DefaultLanguage)[..2];
            }
        }

        public TranslationResource(IOptions<AppSettings> settings,
            IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = settings.Value;
            _httpContextAccessor = httpContextAccessor;

            _translations = new Dictionary<string, List<Resource>>();
            foreach (Language language in settings.Value.TranslateResources.Languages)
            {
                string path = Path.Combine(AppContext.BaseDirectory, language.Path);
                LoadTranslations(language.Culture.ToUpper(), path);
            }
        }

        private void LoadTranslations(string culture, string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var jsonObject = JsonConvert.DeserializeObject<JObject>(json);

                if (jsonObject == null)
                {
                    return;
                }

                foreach (var obj in jsonObject)
                {
                    if (!string.Equals(obj.Key, "@metadata", StringComparison.OrdinalIgnoreCase))
                    {
                        AddResourceRecursive(obj.Value, culture, obj.Key);
                    }
                }
            }
            //else
            //{
            //    _logger.RegisterLogInTerminal($"Translations of culture: {culture} in path {filePath} NOT FOUND!");
            //}
        }

        private void AddResourceRecursive(JToken token, string culture, string key)
        {
            foreach (var item in token.Children())
            {
                if (item.HasValues)
                {
                    AddResourceRecursive(item, culture, key);
                }
                else
                {
                    string section = string.Empty;
                    if (token is JProperty property)
                    {
                        section = property.Name;
                    }

                    Resource translationValue = new()
                    {
                        Path = item.Path,
                        Section = section,
                        Value = item.ToString()
                    };

                    if (!_translations.Any(x => x.Key == culture))
                    {
                        _translations.Add(culture, new List<Resource> { translationValue });
                    }
                    else
                    {
                        var translation = _translations.First(x => x.Key == culture);
                        translation.Value.Add(translationValue);
                    }
                }
            }
        }

        public string GetResource(string key)
        {
            if (_translations.TryGetValue(Language.ToUpper(), out var translations))
            {
                var localizedPath = translations.FirstOrDefault(x => string.Equals(x.Path, key, StringComparison.OrdinalIgnoreCase));
                if (localizedPath != null)
                {
                    return $"{localizedPath.Value}";
                }
            }
            
            return key;
        }

        public string GetCodeResource(string path)
        {
            if (_translations.TryGetValue(Language.ToUpper(), out var translations))
            {
                var localizedPath = translations.FirstOrDefault(x => string.Equals(x.Path, path, StringComparison.OrdinalIgnoreCase));
                if (localizedPath != null)
                {
                    return localizedPath.Section;
                }
            }

            return path;
        }
    }
}
