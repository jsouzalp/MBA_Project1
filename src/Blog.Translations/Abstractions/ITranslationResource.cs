using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Translations.Abstractions
{
    public interface ITranslationResource
    {
        string GetResource(string key);
        string GetCodeResource(string path);
    }
}
