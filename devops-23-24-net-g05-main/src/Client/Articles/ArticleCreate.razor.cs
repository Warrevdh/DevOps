using Client.Files;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Shared.Articles;

namespace Client.Articles
{
    public partial class ArticleCreate
    {
        private IBrowserFile? image;
        private ArticleDto.Mutate article = new();
        [Inject] public IArticleService ArticleService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        ArticleDto.Mutate.Validator validator = new ArticleDto.Mutate.Validator();
        [Inject] public IStorageService StorageService { get; set; }

        private async Task CreateArticleAsync()
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
                ArticleResult.Create result = await ArticleService.CreateAsync(article);
                await StorageService.UploadImageAsync(result.UploadUri, image!);
                NavigationManager.NavigateTo($"Nieuws/{result.Id}");
            }
           
        }

        public void OnChange(string html)
        {
            article.Content = html;
        }
        private void LoadImage(InputFileChangeEventArgs e)
        {
            image = e.File;
            article.ImageContentType = image.ContentType;
        }
    }
}
