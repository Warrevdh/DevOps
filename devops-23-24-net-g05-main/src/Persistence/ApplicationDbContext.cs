using Domain.Users.Employees.Availabilities;
using Microsoft.EntityFrameworkCore;
using Domain.Appointments;
using Domain.Articles;
using Domain.Articles.Fragments;
using Domain.EyeConditions;
using Domain.Users.Doctors;
using Domain.Users.Patients;
using Persistence.Triggers;
using System.Reflection;
using Domain.Users.Employees;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Bio> Biographies => Set<Bio>();
    public DbSet<Employee> Employees => Set<Employee>();
	public DbSet<Availability> Availabilities => Set<Availability>();
    public DbSet<Timeslot> Timeslots => Set<Timeslot>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<EyeCondition> EyeConditions => Set<EyeCondition>();   
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Fragment> Fragments => Set<Fragment>();
    public DbSet<Symptom> Symptoms => Set<Symptom>();   

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseTriggers(options =>
        {
            options.AddTrigger<EntityBeforeSaveTrigger>();
        });
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}