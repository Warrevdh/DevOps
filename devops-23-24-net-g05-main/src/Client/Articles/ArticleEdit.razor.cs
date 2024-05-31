using Client.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Shared.Articles;

namespace Client.Articles
{
    public partial class ArticleEdit
    {
        private IBrowserFile? image;
        private ArticleDto.Detail article = new();
        [Parameter] public long Id { get; set; }
        [Inject] public IArticleService ArticleService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        ArticleDto.Detail.Validator validator = new ArticleDto.Detail.Validator();
        [Inject] public IStorageService StorageService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetDetailAsync();
        }


        private async Task GetDetailAsync()
        {
            article = await ArticleService.GetDetailAsync(Id);
            
        }

        private async Task EditArticleAsync()
        {
            var results = validator.Validate(article);

            if (!results.IsValid)
            {
                var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));

                // Display all error messages in a single alert
                await JSRuntime.InvokeVoidAsync("alert", errorMessages);
            }
            else
            {
                await ArticleService.EditAsync(Id, article);
                //await StorageService.UploadImageAsync(article.ImageUrl, image!);
                NavigationManager.NavigateTo($"Nieuws/{Id}");
            }
           
        }

        public void OnChange(string html)
        {
            article.Content = html;
        }
        private async void LoadImage(InputFileChangeEventArgs e)
        {
            image = e.File;
            article.ImageUrl = image.ContentType;
        }
    }
}
