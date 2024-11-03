namespace Blog.Translations.Constants
{
    public static class AuthorConstant
    {
        #region Validation
        public const string ValidationsAuthorEmptyName = "Autor.Validations.EmptyName";
        public const string ValidationsAuthorNullName = "Autor.Validations.NullName";
        public const string ValidationsAuthorPostWithoutTitle = "Autor.Validations.PostWithoutTitle";
        public const string ValidationsAuthorPostWithoutMessage = "Autor.Validations.PostWithoutMessage";
        public const string ValidationsAuthorNotOwnerOrAdmin = "Autor.Validations.NotOwnerOrAdmin";
        #endregion

        #region Repository
        public const string RepositoryAuthorFound = "Autor.Repository.Info.AuthorFound";
        public const string RepositoryAuthorCreated = "Autor.Repository.Info.AuthorCreated";
        public const string RepositoryAuthorUpdated = "Autor.Repository.Info.AuthorUpdated";
        public const string RepositoryAuthorRemoved = "Autor.Repository.Info.AuthorRemoved";
        public const string RepositoryAuthorNotFound = "Autor.Repository.Warning.AuthorNotFound";
        #endregion

        #region Errors
        public const string RepositoryGetAuthorError = "Autor.Repository.Error.GetAuthorError";
        public const string RepositoryInsertAuthorError = "Autor.Repository.Error.InsertAuthorError";
        public const string RepositoryUpdateAuthorError = "Autor.Repository.Error.UpdateAuthorError";
        public const string RepositoryRemoveAuthorError = "Autor.Repository.Error.RemoveAuthorError";
        #endregion
    }
}
