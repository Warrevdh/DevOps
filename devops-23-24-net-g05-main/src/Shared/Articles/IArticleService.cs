

using Shared.EyeConditions;

namespace Shared.Articles
{
    public interface IArticleService
    {
        Task<ArticleResult.Index> GetIndexAsync(ArticleRequest.Index request);
        //Task EditAsync(long id, ArticleDto.Mutate edit);
        Task<ArticleResult.Create> CreateAsync(ArticleDto.Mutate model);
        Task<ArticleDto.Detail> GetDetailAsync(long Id);
        Task DeleteAsync(long id);
        Task EditAsync(long id, ArticleDto.Detail model);
    }
}
