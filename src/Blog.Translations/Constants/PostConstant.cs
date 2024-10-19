namespace Blog.Translations.Constants
{
    public static class PostConstant
    {
        #region Validation
        public const string ValidationsIdEmpty = "Post.Validations.IdEmpty";
        public const string ValidationsAuthorIdEmpty = "Post.Validations.AuthorIdEmpty";
        public const string ValidationsPostEmptyTitle = "Post.Validations.EmptyTitle";
        public const string ValidationsPostNullTitle = "Post.Validations.NullTitle";
        public const string ValidationsPostEmptyMessage = "Post.Validations.EmptyMessage";
        public const string ValidationsPostNullMessage = "Post.Validations.NullMessage";
        public const string ValidationsPostFilterPage = "Post.Validations.FilterPage";
        public const string ValidationsPostFilterRecordsPerPage = "Post.Validations.FilterRecordsPerPage";
        #endregion

        #region Repository        
        public const string RepositoryPostSelect = "Post.Repository.Info.PostSelect";
        public const string RepositoryPostCreated = "Post.Repository.Info.PostCreated";
        public const string RepositoryPostUpdated = "Post.Repository.Info.PostUpdated";
        public const string RepositoryPostRemoved = "Post.Repository.Info.PostRemoved";
        public const string RepositoryFilterSuccess = "Post.Repository.Info.FilterSuccess";
        public const string RepositoryPostNotFound = "Post.Repository.Warning.PostNotFound";
        public const string RepositoryFilterNotFound = "Post.Repository.Warning.FilterPostNotFound";
        #endregion

        #region Errors
        
        public const string RepositorySelectError = "Post.Repository.Error.SelectError";
        public const string RepositoryFilterPostError = "Post.Repository.Error.FilterPostError";
        public const string RepositoryCreatePostError = "Post.Repository.Error.CreatePostError";
        public const string RepositoryUpdatePostError = "Post.Repository.Error.UpdatePostError";
        public const string RepositoryRemovePostError = "Post.Repository.Error.RemovePostError";
        #endregion
    }
}
