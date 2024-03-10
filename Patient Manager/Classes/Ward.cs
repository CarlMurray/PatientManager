using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Patient_Manager.Classes;

public class Ward
{
    // Default Constructor
    public Ward()
    {
        Name = $"Ward_{WardCount + 1}";
        Capacity = 10;
        Patients = [];
        WardCount++;
    }

    public Ward(string name) : this()
    {
        Name = name;
    }

    public Ward(string name, int capacity) : this(name)
    {
        Capacity = capacity;
    }

    public Ward(string name, int capacity, ObservableCollection<Patient> patients) : this(name, capacity)
    {
        Patients = patients;
    }

    public string Name { get; set; }
    public int Capacity { get; set; }

    // Collection of Patients in the Ward
    public ObservableCollection<Patient> Patients { get; set; }

    // A count of number of Wards
    public static int WardCount { get; set; }

    [JsonIgnore] public string PrintWard => ToString();

    public override string ToString()
    {
        return $"{Name}, {Capacity}";
    }
}