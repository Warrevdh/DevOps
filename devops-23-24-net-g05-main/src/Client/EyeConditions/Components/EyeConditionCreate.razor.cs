using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Shared.EyeConditions;
using static System.Net.Mime.MediaTypeNames;
using System;
using Client.Files;

namespace Client.EyeConditions.Components
{
    public partial class EyeConditionCreate
    {
        private IBrowserFile? image;
        private EyeConditionDto.Mutate eyeCondition = new();
        IDictionary<long,string> symptoms = new Dictionary<long, string>();
        private List<long> selectedSymptoms = new List<long>();
        public List<SymptomDto.Index> officialSymptoms { get; set; } = new List<SymptomDto.Index>();
        [Inject] public ISymptomService SymptomService { get; set; } = default!;
        [Inject] public IEyeConditionService EyeConditionService { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Inject] public IStorageService StorageService { get; set; }

        EyeConditionDto.Mutate.Validator validator = new EyeConditionDto.Mutate.Validator();
        public bool symptomForm = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetSymptoms();
        }

        private void ShowSymptomForm()
        {
            symptomForm = !symptomForm;
        }

        private async Task GetSymptoms()
        {
            var result = await SymptomService.GetIndexAsync(new SymptomRequest.Index());
            officialSymptoms = result.Symptoms.ToList();
            foreach (var res in officialSymptoms) {
                symptoms.Add(res.Id, res.Name);
            }
        }

        private async Task CreateEyeConditionAsync()
        {
            eyeCondition.Symptoms = officialSymptoms.Where(x => selectedSymptoms.Contains(x.Id)).ToList();

            var results = validator.Validate(eyeCondition);

            if (!results.IsValid)
            {
                var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));
            /*            var symptomsSelected = officialSymptoms.Where(x => selectedSymptoms.Contains(x.Id)).ToList();
                        foreach (var symptom in symptomsSelected)
                        {
                            eyeCondition.Symptoms.Add(symptom);
                        }*/
                EyeConditionResult.Create result = await EyeConditionService.CreateAsync(eyeCondition);

                // Display all error messages in a single alert
                await JSRuntime.InvokeVoidAsync("alert", errorMessages);
            }
            else
            {
                EyeConditionResult.Create result = await EyeConditionService.CreateAsync(eyeCondition);
                await StorageService.UploadImageAsync(result.UploadUri, image!);
                NavigationManager.NavigateTo($"Oogaandoening/{result.EyeConditionId}");
            }
        }

        public void OnChange(string html)
        {
            eyeCondition.Body = html;
        }

        private void LoadImage(InputFileChangeEventArgs e)
        {
            image = e.File;
            eyeCondition.ImageContentType = image.ContentType;
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
