using Microsoft.AspNetCore.Components;
using Shared.Articles;

namespace Client.Articles
{
    public partial class Index
    {
        [Inject] public IArticleService ArticleService { get; set; } = default!;

        private IEnumerable<ArticleDto.Index>? articles;
        [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
        [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
        [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }
        [Parameter, SupplyParameterFromQuery] public int? TagId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            ArticleRequest.Index request = new()
            {
                Searchterm = Searchterm,
                Page = Page.HasValue ? Page.Value : 1,
                PageSize = PageSize.HasValue ? PageSize.Value : 25,
                TagId = TagId
            };

            var response = await ArticleService.GetIndexAsync(request);
            articles = response.Articles.OrderBy(ec => ec.Title).ToList();
        }
    }
}
