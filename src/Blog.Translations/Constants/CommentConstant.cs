namespace Blog.Translations.Constants
{
    public static class CommentConstant
    {
        #region Validation
        public const string ValidationsIdEmpty = "Comment.Validations.IdEmpty";
        public const string ValidationsPostIdEmpty = "Comment.Validations.PostIdEmpty";
        public const string ValidationsCommentAuthorIdEmpty = "Comment.Validations.CommentAuthorIdEmpty";
        public const string ValidationsCommentEmptyMessage = "Comment.Validations.EmptyMessage";
        public const string ValidationsCommentNullMessage = "Comment.Validations.NullMessage";
        #endregion

        #region Repository
        public const string RepositoryCommentCreated = "Comment.Repository.Info.CommentCreated";
        public const string RepositoryCommentUpdated = "Comment.Repository.Info.CommentUpdated";
        public const string RepositoryCommentRemoved = "Comment.Repository.Info.CommentRemoved";
        public const string RepositoryCommentNotFound = "Comment.Repository.Warning.CommentNotFound";
        #endregion

        #region Errors
        public const string RepositoryCreateCommentError = "Comment.Repository.Error.CreateCommentError";
        public const string RepositoryUpdateCommentError = "Comment.Repository.Error.UpdateCommentError";
        public const string RepositoryRemoveCommentError = "Comment.Repository.Error.RemoveCommentError";
        #endregion
    }
}
