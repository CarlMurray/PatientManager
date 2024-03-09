using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Patient_Manager
{
    public class Patient
    {
        public string Name { get; set; }
        private DateTime _dateOfBirth;


        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Date of birth cannot be in the future.");
                }
                _dateOfBirth = value;
                Age = (int)Math.Floor((DateTime.Now - _dateOfBirth).TotalDays / 365);
            }
        }
        public int Age { get; private set; }
        public Blood_Type BloodType { get; set; }
        public string printPatient => $"{Name} ({Age}) Type {BloodType}";


        public Patient(string name, DateTime dateOfBirth, Blood_Type bloodType)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            BloodType = bloodType;


        }

        public enum Blood_Type
        {
            A, B, AB, O
        }
    }
}
