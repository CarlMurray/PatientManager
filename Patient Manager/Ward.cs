using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Manager
{
    public class Ward
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public string printWard => $"{Name}, {Capacity}";

        public Ward(string name, int capacity, ObservableCollection<Patient> patients = null)
        {
            Name = name;
            Capacity = capacity;
            Patients = patients ?? new ObservableCollection<Patient>();
        }
    }
}
