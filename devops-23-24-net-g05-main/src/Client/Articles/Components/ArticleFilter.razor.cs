using Microsoft.AspNetCore.Components;

namespace Client.Articles.Components
{
    public partial class ArticleFilter
    {
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Parameter, EditorRequired] public string? Searchterm { get; set; } = default!;

        private string? searchTerm;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
        private void SearchTermChanged(ChangeEventArgs args)
        {
            searchTerm = args.Value?.ToString();
            FilterEyeConditions();
        }

        private void FilterEyeConditions()
        {
            Dictionary<string, object?> parameters = new();

            parameters.Add(nameof(searchTerm), searchTerm);

            var uri = NavigationManager.GetUriWithQueryParameters(parameters);

            NavigationManager.NavigateTo(uri);
        }
    }
}
