namespace Domain.Articles.Fragments;

public class Fragment : Entity {

    private string title = default!;

    public string Title {
        get => title;
        set => title = Guard.Against.NullOrWhiteSpace(value, nameof(Title));
    }

    private string description = default!;
    public string Description
    {
        get => description;
        set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description));
    }

    //Database Constructor
    private Fragment() { }

    public Fragment(string title, string description) {
        Title = title;
        Description = description;
    }
}
