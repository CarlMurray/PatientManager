using System.Text.Json.Serialization;

namespace Patient_Manager.Classes;

public class Patient
{
    public enum Blood_Type
    {
        A,
        B,
        AB,
        O
    }

    private DateTime _dateOfBirth;

    // Default constructor
    public Patient()
    {
        Name = "Unnamed";
        DateOfBirth = DateTime.Now;
        BloodType = Blood_Type.A;
    }

    public Patient(string name) : this()
    {
        Name = name;
    }

    public Patient(string name, DateTime dateOfBirth) : this(name)
    {
        DateOfBirth = dateOfBirth;
    }

    public Patient(string name, DateTime dateOfBirth, Blood_Type bloodType) : this(name, dateOfBirth)
    {
        BloodType = bloodType;
    }

    public string Name { get; set; }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateTime.Now) throw new ArgumentException("Date of birth cannot be in the future.");
            _dateOfBirth = value;

            // Calculate Age property
            Age = (int)Math.Floor((DateTime.Now - _dateOfBirth).TotalDays / 365);
        }
    }

    [JsonIgnore] public int Age { get; private set; }

    public Blood_Type BloodType { get; set; }

    [JsonIgnore] public string PrintPatient => ToString();

    public override string ToString()
    {
        return $"{Name} ({Age}) Type {BloodType}";
    }
}