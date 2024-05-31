namespace Domain.Users.Doctors;
public class Specialization : Entity {

    private string name = default!;
    public string Name
    {
        get => name;
        set => name = Guard.Against.NullOrWhiteSpace(value, nameof(name));
    }
    
    // Database constructor
    private Specialization() { }

    public Specialization(string name)
    {
        Name = name;
    }
}
