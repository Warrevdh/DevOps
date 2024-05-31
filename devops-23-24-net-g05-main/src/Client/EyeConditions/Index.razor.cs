using Microsoft.AspNetCore.Components;
using System.Diagnostics.SymbolStore;
using Shared.EyeConditions;

namespace Client.EyeConditions;

public partial class Index
{
	[Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;
    [Inject] public ISymptomService SymptomService { get; set; } = default!;
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    private IEnumerable<EyeConditionDto.Index>? eyeConditions;
    private IEnumerable<SymptomDto.Index>? symptoms;

    private bool open = false;
    private bool openInfo = false;

    private long deleteRequestId = default;

    [Parameter, SupplyParameterFromQuery] public string? Searchterm { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? Page { get; set; }
    [Parameter, SupplyParameterFromQuery] public int? PageSize { get; set; }

     public List<long>? symptomIds { get; set; }


    async Task UpdateList(List<long> updatedList)
    {
        symptomIds = updatedList;
        await OnParametersSetAsync();
    }

    protected override async Task OnParametersSetAsync()
	{
		EyeConditionRequest.Index request = new()
		{
            Searchterm = Searchterm,
            Page = Page.HasValue ? Page.Value : 1,
            PageSize = PageSize.HasValue ? PageSize.Value : 25,
            SymptomIds = symptomIds
        };

		var response = await EyeConditionService.GetIndexAsync(request);
        eyeConditions = response.EyeConditions.OrderBy(ec => ec.Name).ToList();

        var symptomResponse = await SymptomService.GetIndexAsync(new SymptomRequest.Index());
        symptoms = symptomResponse.Symptoms.OrderBy(ec => ec.Name).ToList();
    }

    private void NavigateToCreate()
    {

        NavigationManager.NavigateTo($"Oogaandoeningen/nieuw");
    }
    private void ToggleDeletePopUp(long id)
    {
        open = !open;
        deleteRequestId = id;
    }
    private void CloseDeletePopUp()
    {
        open = false;
        deleteRequestId = default;
    }

    private async Task ConfirmDelete()
    {
        await EyeConditionService.DeleteAsync(deleteRequestId);
        NavigationManager.NavigateTo("Oogaandoeningen",true);
    }

    private void ToglleInfoPopUp()
    {
        openInfo = !openInfo;
    }

}