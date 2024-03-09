using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;


namespace Patient_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string FILE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/data.json";
        public ObservableCollection<Patient> patientList { get; set; }
        public ObservableCollection<Ward> wardList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            patientList = new ObservableCollection<Patient>();
            wardList = new ObservableCollection<Ward>();

            //patientListBox.ItemsSource = wardList;
            wardListBox.ItemsSource = wardList;

            //patientList.Add(new Patient("Carl Murray", new DateTime(1997, 1, 17), Patient.Blood_Type.A));
            //patientList.Add(new Patient("John Doe", new DateTime(1993, 4, 2), Patient.Blood_Type.AB));
            //patientList.Add(new Patient("James White", new DateTime(1967, 5, 22), Patient.Blood_Type.O));

            //wardList.Add(new Ward("Green Ward", 9, patientList));
            //wardList.Add(new Ward("Yellow Ward", 6, patientList));

            //var data = new { Wards = wardList };

            //var jsonfile = JsonSerializer.Serialize(data);
            //File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/data.json", jsonfile);

        }

        // Add Ward on click handler
        private void AddWardBtn_OnClick(object sender, RoutedEventArgs e)
        {
            wardList.Add(new Ward(addWardName.Text.Trim(), (int)addWardCapacity.Value));
        }

        // Enable/disable addWardBtn
        private void AddWardName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                addWardBtn.IsEnabled = true;
            }
            else addWardBtn.IsEnabled = false;
        }

        private void AddPatientBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Patient.Blood_Type bloodType;
            if (Radio_BloodType_A.IsChecked == true)
            {
                bloodType = Patient.Blood_Type.A;
            }
            else if (Radio_BloodType_B.IsChecked == true)
            {
                bloodType = Patient.Blood_Type.B;
            }
            else if (Radio_BloodType_AB.IsChecked == true)
            {
                bloodType = Patient.Blood_Type.AB;
            }
            else
            {
                bloodType = Patient.Blood_Type.O;
            }

            Ward x =wardListBox.SelectedItem as Ward;
            x.Patients.Add(new Patient(addPatientName.Text.Trim(), addPatientDOB.DisplayDate, bloodType));
            //Patients.Add(new Patient(addPatientName.Text.Trim(), addPatientDOB.DisplayDate, bloodType));
            //patientList.Add(new Patient(addPatientName.Text.Trim(), addPatientDOB.DisplayDate, bloodType));
        }

        private void AddPatientName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                addPatientBtn.IsEnabled = true;
            }
            else addPatientBtn.IsEnabled = false;
        }

        

        private void LoadData_OnClick(object sender, RoutedEventArgs e)
        {
            wardList.Clear();
            patientList.Clear();
            var file = File.ReadAllText("C:\\Users\\carlj\\Desktop\\data.json");
            ObservableCollection<Ward> deserialize = JsonSerializer.Deserialize<ObservableCollection<Ward>>(file);

            foreach (var ward in deserialize)
            {
             wardList.Add(ward);
             foreach (var patient in ward.Patients)
             {
              patientList.Add(patient);   
             }
            }
        }

        private void SaveData_OnClick(object sender, RoutedEventArgs e)
        {
            var data = wardList;

            var jsonfile = JsonSerializer.Serialize(data);
            File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/data.json", jsonfile);        }
        }
    }