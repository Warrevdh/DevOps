using Domain.EyeConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EyeConditions;

public class Symptom : Entity
{
    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
    }


    //public List<EyeConditionSymptom> EyeConditionSymptoms { get; set; }


    private readonly List<EyeCondition> conditions = new();
    public IReadOnlyCollection<EyeCondition> Conditions => conditions.AsReadOnly();

    private Symptom() { }

    public Symptom(string name)
    {
        Name = name;
    }

}

