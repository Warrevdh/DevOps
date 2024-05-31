using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.EyeConditions;

namespace Client.Symptoms.Components
{
    public partial class SymptomCreate
    {
        private SymptomDto.Mutate symptom { get; set; } = new();
        SymptomDto.Mutate.Validator validator = new SymptomDto.Mutate.Validator();
        [Inject] public ISymptomService SymptomService { get; set; } = default!;

        [Parameter]
        public List<SymptomDto.Index> Symptoms { get; set; }

        [Parameter]
        public bool SymptomForm { get; set; }

        [Parameter]
        public List<long> SelectedSymptoms { get; set; }
        [Parameter]
        public IDictionary<long,string> SymptomsDict { get; set; }

        [Parameter]
        public EventCallback<(List<SymptomDto.Index>, bool, List<long>, Dictionary<long,string>)> SymptomsChanged { get; set; }


        private async Task CreateSymptomAsync()
        {
            var results = validator.Validate(symptom);

            if (!results.IsValid)
            {
                var errorMessages = string.Join("\n", results.Errors.Select(failure => $"{failure.ErrorMessage}"));

                // Display all error messages in a single alert
                await JSRuntime.InvokeVoidAsync("alert", errorMessages);
            }
            else
            {
                SymptomResult.Create result = await SymptomService.CreateAsync(symptom);
                SymptomDto.Index createdSymptom = new SymptomDto.Index
                {
                    Id = result.SymptomId,
                    Name = result.Name
                };
                
                Symptoms.Add(createdSymptom);
                SymptomForm = false;
                SelectedSymptoms.Add(createdSymptom.Id);
                SymptomsDict.Add(createdSymptom.Id, createdSymptom.Name);
                await UpdateValues();
            }
        }

        // Method to update values
        private async Task UpdateValues()
        {
            await SymptomsChanged.InvokeAsync(((List<SymptomDto.Index>, bool, List<long>, Dictionary<long, string>))(Symptoms, SymptomForm, SelectedSymptoms, SymptomsDict));
        }

    }
}
