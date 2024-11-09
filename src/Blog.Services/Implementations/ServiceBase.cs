using Blog.Bases;
using Blog.Services.Entities;
using Blog.Translations.Abstractions;
using Blog.Translations.Constants;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Services.Implementations
{
    public class ServiceBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        internal readonly ITranslationResource _translateResource;
        internal readonly Guid _authorId;
        internal readonly bool _isAdmin;

        public ServiceBase(IHttpContextAccessor httpContextAccessor, ITranslationResource translateResource)
        {
            _contextAccessor = httpContextAccessor;
            _translateResource = translateResource;

            var user = _contextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                // Obter o claim "sub" (ou "NameIdentifier") para o User ID
                var subClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                               ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                if (!string.IsNullOrWhiteSpace(subClaim))
                {
                    _authorId = Guid.Parse(subClaim);
                }

                // Verificar se o usuário possui a role "Admin"
                _isAdmin = user.IsInRole("ADMIN");
            }
            else
            {
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    var jwtContent = authorizationHeader.Substring("Bearer".Length).Trim();

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(jwtContent);

                    string sub = jwtToken.Claims.FirstOrDefault(x => string.Equals(x.Type, "Sub", StringComparison.OrdinalIgnoreCase))?.Value;
                    string admin = jwtToken.Claims.FirstOrDefault(x => string.Equals(x.Type, "Role", StringComparison.OrdinalIgnoreCase) && string.Equals(x.Value, "Admin", StringComparison.OrdinalIgnoreCase))?.Value;

                    _authorId = !string.IsNullOrWhiteSpace(sub) ? Guid.Parse(sub) : Guid.Empty;
                    _isAdmin = !string.IsNullOrWhiteSpace(admin);
                }
            }
        }

        internal bool ValidateOwnerOrAdmin<T>(Guid id, ServiceOutput<T> result)
        {
            if (_authorId == id || _isAdmin)
            {
                return true;
            }
            else
            {
                result.Errors = new List<ErrorBase>()
                {
                    new ErrorBase()
                    {
                        Code = _translateResource.GetCodeResource(AuthorConstant.ValidationsAuthorNotOwnerOrAdmin),
                        Message = _translateResource.GetResource(AuthorConstant.ValidationsAuthorNotOwnerOrAdmin)
                     }
                };
                return false;
            }
        }
    }
}
