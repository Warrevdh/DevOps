using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using static System.Net.Mime.MediaTypeNames;
using System;
using Shared.EyeConditions;

namespace Client.EyeConditions.Components
{
    public partial class EyeConditionEdit
    {
        private IBrowserFile? image;

        private EyeConditionDto.Mutate eyeCondition = new();
        [Parameter] public long Id { get; set; }
        [Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public ISymptomService SymptomService { get; set; } = default!;
        IDictionary<long, string> symptoms = new Dictionary<long, string>();
        EyeConditionDto.Mutate.Validator validator = new EyeConditionDto.Mutate.Validator();

        public bool symptomForm = false;
        public List<SymptomDto.Index> officialSymptoms { get; set; } = new List<SymptomDto.Index>();
        private List<long> selectedSymptoms = new List<long>();



        protected override async Task OnParametersSetAsync()
        {
            await GetEyeConditionAsync();
            await GetSymptoms();
        }
        private async Task GetEyeConditionAsync()
        {
            eyeCondition = await EyeConditionService.GetMutateAsync(Id);

            Console.WriteLine(eyeCondition.Symptoms);

        }

        private void ShowSymptomForm()
        {
            symptomForm = !symptomForm;
        }

        private async Task GetSymptoms()
        {
            var result = await SymptomService.GetIndexAsync(new SymptomRequest.Index());
            officialSymptoms = result.Symptoms.ToList();
            foreach (var res in officialSymptoms)
            {
                symptoms.Add(res.Id, res.Name);
            }
        }

        private void LoadImage(InputFileChangeEventArgs e)
        {
            image = e.File;
            eyeCondition.ImageContentType = image.ContentType;
        }

        public void OnChange(string html)
        {
            eyeCondition.Body = html;
        }

        private async Task EditEyeConditionAsync()
        {
            eyeCondition.Symptoms = officialSymptoms.Where(x => selectedSymptoms.Contains(x.Id)).ToList();
            var results = validator.Validate(eyeCondition);

            if (!results.IsValid)
            {
                var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));

                // Display all error messages in a single alert
                await JSRuntime.InvokeVoidAsync("alert", errorMessages);
            }
            else
            {
                await EyeConditionService.EditAsync(Id, eyeCondition);

                NavigationManager.NavigateTo($"Oogaandoening/{Id}");
            }
        }

        private void HandleSymptomsChanged((List<SymptomDto.Index>, bool, List<long>, Dictionary<long, string>) updatedValues)
        {
            officialSymptoms = updatedValues.Item1;
            symptomForm = updatedValues.Item2;
            selectedSymptoms = updatedValues.Item3;
            symptoms = updatedValues.Item4;
            StateHasChanged();
        }

    }
}
