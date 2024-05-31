using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Shared.Articles;
using Shared.EyeConditions;
using System.Drawing;
using System.Security.Policy;

namespace Client.Admin.Components.Articles
{
    public partial class ArticleGrid
    {
        RadzenDataGrid<ArticleDto.Index> articleGrid;
        List<ArticleDto.Index>? articles;
        [Inject] public IArticleService ArticleService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; }
        private bool open = false;
        private ArticleDto.Index deleteRequest = null;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ArticleResult.Index res = await ArticleService.GetIndexAsync(new ArticleRequest.Index());
            articles = res.Articles.ToList();
        }

        async Task EditRow(ArticleDto.Index article)
        {
            long articleId = article.Id;
            NavigationManager.NavigateTo($"/Nieuws/edit/{articleId}");
        }

        async Task DeleteRow(ArticleDto.Index article)
        {
            open = !open;
            deleteRequest = article;
        }
        async Task CreateArticle()
        {
            NavigationManager.NavigateTo($"/Nieuws/nieuw");
        }

        private void CloseDeletePopUp()
        {
            open = !open;
            deleteRequest = null;
        }

        private async Task ConfirmDelete()
        {
            await ArticleService.DeleteAsync(deleteRequest.Id);
            open = !open;
            articles.Remove(deleteRequest);
            await articleGrid.Reload();
        }

    }
}
