namespace Domain.EyeConditions;

public class EyeCondition : Entity {

    private string name = default!;
    public string Name {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(Name));
    }

    private string description = default!;
    public string Description
    {
        get => description;
        set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description));
    }

    private string body = default!;
    public string Body
	{
        get => body;
        set => body = Guard.Against.NullOrWhiteSpace(value,nameof(Body));
	}

    private string brochureUrl = default!;
    public string BrochureUrl
    {
        get => brochureUrl;
        set => brochureUrl = Guard.Against.Null(value, nameof(BrochureUrl));
    }

    private string imageUrl = default!;
    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl));

    }

    private readonly List<Symptom> symptoms = new();
	public IReadOnlyCollection<Symptom> Symptoms => symptoms.AsReadOnly();

    //Database Constructor
    private EyeCondition() { }

    public EyeCondition(string name, string description, string body, string imageUrl, string brochureUrl) {
        Name = name;
        Description = description;
        Body = body;
        ImageUrl = imageUrl;
        BrochureUrl = brochureUrl;
    }

    public void Symptom(Symptom symptom)
    {
        Guard.Against.Null(symptom, nameof(symptom));
        if (symptoms.Contains(symptom))
        {
            throw new ApplicationException($"{nameof(EyeCondition)} '{name}' already contains the symptom:{symptom.Name}");
        }
        symptoms.Add(symptom);
    }

    public void RemoveSymptom(Symptom symptom)
    {
        Guard.Against.Null(symptom, nameof(symptom));

        if (!symptoms.Contains(symptom))
        {
            throw new ApplicationException($"{nameof(EyeCondition)} '{name}' does not have this specialization");
        }
        symptoms.Remove(symptom);
    }
}
