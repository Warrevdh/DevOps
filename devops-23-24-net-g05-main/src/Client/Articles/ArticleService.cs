using Client.Classes;
using Microsoft.AspNetCore.Components;
using Client.Extensions;
using Shared.Articles;
using System.Net.Http.Json;
using Shared.EyeConditions;
using System.Net;
using Shared.EyeConditions;

namespace Client.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly PublicClient publicClient;
        private readonly HttpClient authenticatedClient;
        private const string endpoint = "api/Article";
        public ArticleService(HttpClient authenticatedClient, PublicClient publicClient)
        {
            this.authenticatedClient = authenticatedClient;
            this.publicClient = publicClient;
        }

        public async Task<ArticleResult.Index> GetIndexAsync(ArticleRequest.Index request)
        {
            var response = await publicClient.Client.GetFromJsonAsync<ArticleResult.Index>($"{endpoint}?{request.AsQueryString()}");
            return response!;
        }

        public async Task<ArticleDto.Mutate> GetMutateAsync(long Id)
        {
            var response = await publicClient.Client.GetFromJsonAsync<ArticleDto.Mutate>($"{endpoint}/{Id}");
            return response!;
        }

        public async Task EditAsync(long id, ArticleDto.Mutate edit)
        {
            var response = await publicClient.Client.PutAsJsonAsync($"{endpoint}/{id}", edit);
            response.EnsureSuccessStatusCode();
        }

        public async Task<ArticleResult.Create> CreateAsync(ArticleDto.Mutate model)
        {
            var response = await authenticatedClient.PostAsJsonAsync(endpoint, model);
            return await response.Content.ReadFromJsonAsync<ArticleResult.Create>();
        }

        public async Task EditAsync(long Id, ArticleDto.Detail edit)
        {
            var response = await authenticatedClient.PutAsJsonAsync($"{endpoint}/{Id}", edit);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // Handle 404 error
                    // For example:
                    Console.WriteLine($"Article with ID {Id} not found.");
                }
                else
                {
                    // Handle other non-success status codes
                    response.EnsureSuccessStatusCode();
                }
            }
        }

        public async Task<ArticleDto.Detail> GetDetailAsync(long Id)
        {
            var response = await publicClient.Client.GetFromJsonAsync<ArticleDto.Detail>($"{endpoint}/{Id}");
            return response;
        }

        public async Task DeleteAsync(long id)
        {
            await authenticatedClient.DeleteAsync($"{endpoint}/{id}");
        }
    }
}
