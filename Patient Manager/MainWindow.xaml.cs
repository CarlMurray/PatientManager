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
        public ObservableCollection<Patient> patientList { get; set; }
        public ObservableCollection<Ward> wardList { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            patientList = new ObservableCollection<Patient>();
            wardList = new ObservableCollection<Ward>();

            patientListBox.ItemsSource = patientList;
            wardListBox.ItemsSource = wardList;

            patientList.Add(new Patient("carl murray", new DateTime(1990, 2, 2), Patient.Blood_Type.A));
            patientList.Add(new Patient("john doe", new DateTime(1993, 2, 2), Patient.Blood_Type.AB));

            wardList.Add(new Ward("Adult's ward", 9, patientList));
            wardList.Add(new Ward("Children's ward", 6, patientList));

            var jsonfile = JsonSerializer.Serialize(patientList);
            File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/data.json", jsonfile);

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
            if (Radio_BloodType_A.IsChecked== true)
            {
                bloodType = Patient.Blood_Type.A;
            }
            else if (Radio_BloodType_B.IsChecked ==true)
            {
                bloodType= Patient.Blood_Type.B;
            }
            else if (Radio_BloodType_AB.IsChecked==true)
            {
                bloodType = Patient.Blood_Type.AB;
            }
            else
            {
                bloodType = Patient.Blood_Type.O;
            }

            patientList.Add(new Patient(addPatientName.Text.Trim(), addPatientDOB.DisplayDate, bloodType));
        }

        private void AddPatientName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(((TextBox)sender).Text))
            {
                addPatientBtn.IsEnabled = true;
            }
            else addPatientBtn.IsEnabled = false;
        }
    }
}