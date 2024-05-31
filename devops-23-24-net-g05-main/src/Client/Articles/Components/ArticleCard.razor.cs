using Microsoft.AspNetCore.Components;
using Shared.Articles;

namespace Client.Articles.Components
{
    public partial class ArticleCard
    {
        [Parameter] public ArticleDto.Index Article { get; set; } = default!;
        [Parameter] public long Id { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        MarkupString articleDescription { get; set; } = default!;

        protected override void OnInitialized()
        {
            articleDescription = new MarkupString(Article.Description);
        }

        private void NavigateToDetail()
        {
            NavigationManager.NavigateTo($"Nieuws/{Article.Id}");
        }
    }
}
