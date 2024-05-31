using System;
using Microsoft.AspNetCore.Components;
using Shared.EyeConditions;

namespace Client.EyeConditions.Components;
public partial class EyeConditionFilter
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;
    [Parameter] public List<SymptomDto.Index> symptoms { get; set; } = new List<SymptomDto.Index>();
    [Parameter, EditorRequired] public string? Searchterm { get; set; } = default!;
    [Parameter] public EventCallback<List<long>> OnListUpdated { get; set; }

    public List<long>? SymptomIds { get;set; }

    IDictionary<long, string> symptomDict = new Dictionary<long, string>();

    private string? searchTerm;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        foreach (var res in symptoms)
        {
            symptomDict.Add(res.Id, res.Name);
        }
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        searchTerm = Searchterm;
    }
    private void SearchTermChanged(ChangeEventArgs args)
    {
        searchTerm = args.Value?.ToString();
        FilterEyeConditions();
    }

    private void TagChanged()
    {
        OnListUpdated.InvokeAsync(SymptomIds);
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

