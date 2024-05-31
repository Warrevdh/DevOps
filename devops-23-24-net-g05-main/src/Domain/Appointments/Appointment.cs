using Domain.Users.Patients;

namespace Domain.Appointments;

public class Appointment : Entity{

    private Patient patient = default!;
    public Patient Patient
    {
        get => patient;
        set => patient = Guard.Against.Null(value);
    }

    private Timeslot timeslot = default!;
    public Timeslot Timeslot
    {
        get => timeslot;
        set => timeslot = Guard.Against.Null(value);
    }

    private string reason = default!;
    public string Reason
    {
        get => reason;
        set => reason = Guard.Against.NullOrWhiteSpace(value);
    }

    private string note = default!;
    public string Note
    {
        get => note;
        set => note = Guard.Against.Null(value);
    }

    //DOKTER NOTEEEE

    // Database constructor
    private Appointment() { }

    public Appointment(Patient patient, Timeslot timeslot, string reason, string note)
    {
        Patient = patient;
        Timeslot = timeslot;
        Reason = reason;
        Note = note;
    }
}
