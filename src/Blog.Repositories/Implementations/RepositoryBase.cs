﻿using Blog.Bases;
using Blog.Translations.Abstractions;

namespace Blog.Repositories.Implementations
{
    public class RepositoryBase
    {
        internal readonly ITranslationResource _translateResource;

        public RepositoryBase(ITranslationResource translateResource)
        {
            _translateResource = translateResource;
        }

        internal IEnumerable<ErrorBase> GenerateErrorInformation(Exception ex, string key, object[] values)
        {
            string message = values != null
                ? string.Format(_translateResource.GetResource(key), values)
                : _translateResource.GetResource(key);

            return new List<ErrorBase>()
            {
                new ErrorBase()
                {
                    Code = _translateResource.GetCodeResource(key),
                    Message = message,
                    InternalMessage = ex.ToString()
                }
            };
        }
    }
}
