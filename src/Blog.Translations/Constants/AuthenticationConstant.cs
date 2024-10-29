namespace Blog.Translations.Constants
{
    public static class AuthenticationConstant
    {
        #region Validation
        public const string ServiceValidationNullFullName = "Authentication.Service.Validations.NullFullName";
        public const string ServiceValidationEmptyFullName = "Authentication.Service.Validations.EmptyFullName";
        public const string ServiceValidationNullEmail = "Authentication.Service.Validations.NullEmail";
        public const string ServiceValidationEmptyEmail = "Authentication.Service.Validations.EmptyEmail";
        public const string ServiceValidationNullPassword = "Authentication.Service.Validations.NullPassword";
        public const string ServiceValidationEmptyPassword = "Authentication.Service.Validations.EmptyPassword";
        #endregion

        #region Service        
        public const string ServiceLoginSuccess = "Authentication.Service.Info.Success";
        #endregion

        #region Errors
        public const string ServiceLoginNotDefined = "Authentication.Service.Error.NotDefined";
        public const string ServiceLoginBlocked = "Authentication.Service.Error.LoginBlocked";
        public const string ServiceLoginNotAllowed = "Authentication.Service.Error.NotAllowed";
        #endregion

    }
}
