using Microsoft.AspNetCore.Components;
using Shared.EyeConditions;
using Radzen.Blazor;

namespace Client.Admin.Components.EyeConditions
{
    public partial class EyeConditionGrid
    {
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        RadzenDataGrid<EyeConditionDto.Index> eyeConditionGrid;
        List<EyeConditionDto.Index>? eyeConditions;
        private bool open = false;
        [Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;
        private EyeConditionDto.Index deleteRequest = null;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            EyeConditionResult.Index res = await EyeConditionService.GetIndexAsync(new EyeConditionRequest.Index());
            eyeConditions = res.EyeConditions.ToList();
        }

        async Task EditRow(EyeConditionDto.Index eyeCondition)
        {
            long eyeConditionId = eyeCondition.Id;
            NavigationManager.NavigateTo($"/Oogaandoeningen/edit/{eyeConditionId}");
        }

        private void CloseDeletePopUp()
        {
            open = !open;
            deleteRequest = null;
        }

        async Task DeleteRow(EyeConditionDto.Index eyeCondition)
        {
            open = !open;
            deleteRequest = eyeCondition;
        }

        private async Task ConfirmDelete()
        {
            await EyeConditionService.DeleteAsync(deleteRequest.Id);
            open = !open;
            eyeConditions.Remove(deleteRequest);
            await eyeConditionGrid.Reload();
        }

        async Task CreateEyeCondition()
        {
            NavigationManager.NavigateTo($"/Oogaandoeningen/nieuw");
        }
    }
}
