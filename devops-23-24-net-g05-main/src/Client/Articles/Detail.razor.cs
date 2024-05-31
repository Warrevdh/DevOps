using Microsoft.AspNetCore.Components;
using Shared.Articles;
using System.Threading.Tasks;

namespace Client.Articles;
public partial class Detail
{
    private ArticleDto.Detail? article;

    [Parameter] public long Id { get; set; }
    [Inject] public IArticleService ArticleService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnParametersSetAsync() 
    {
        await GetEyeConditionAsync();
    }
    private async Task GetEyeConditionAsync() 
    {
        article = await ArticleService.GetDetailAsync(Id);
    }
    private void GoBack() {
        NavigationManager.NavigateTo("/Nieuws");
    }
}
