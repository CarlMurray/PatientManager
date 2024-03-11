using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Patient_Manager.Classes;

namespace Patient_Manager;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // File path to json data
    private readonly string FILE_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/data.json";

    public MainWindow()
    {
        InitializeComponent();
        _ = MessageBox.Show(
            "Instructions:\nSTEP 1: Add Ward(s);\nSTEP 2: Add Patient(s) to each Ward(s);\nSTEP 3: Save - File named data.json will be saved to DESKTOP;\nSTEP 4: Restart app and click Load.");
        wardList = [];
        wardListBox.ItemsSource = wardList;
        Ward.WardCount = 0;
    }

    public ObservableCollection<Ward> wardList { get; set; }

    // Add Ward on click handler
    private void AddWardBtn_OnClick(object sender, RoutedEventArgs e)
    {
        wardList.Add(new Ward(addWardName.Text.Trim(), (int)addWardCapacity.Value));
        UpdateWardsHeaderUI();
    }

    // Enable/disable addWardBtn
    private void AddWardName_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        addWardBtn.IsEnabled = !string.IsNullOrWhiteSpace(((TextBox)sender).Text);
    }

    // Add Patient when btn clicked
    private void AddPatientBtn_OnClick(object sender, RoutedEventArgs e)
    {
        // Check that ward is selected
        if (wardListBox.SelectedItem is not Ward selectedWard)
        {
            _ = MessageBox.Show("Please add/select a Ward before adding Patients.");
            return;
        }

        // Check ward capacity
        if (selectedWard.Patients.Count >= selectedWard.Capacity)
        {
            _ = MessageBox.Show("Ward capacity exceeded.");
            return;
        }

        if (addPatientDOB.SelectedDate > DateTime.Now)
        {
            _ = MessageBox.Show("Date of birth cannot be in the future.");
            return;
        }

        Patient.Blood_Type bloodType = Patient.Blood_Type.A; // default

        // Check which radio selected
        if (Radio_BloodType_B.IsChecked == true)
        {
            bloodType = Patient.Blood_Type.B;
        }
        else if (Radio_BloodType_AB.IsChecked == true)
        {
            bloodType = Patient.Blood_Type.AB;
        }
        else if (Radio_BloodType_O.IsChecked == true)
        {
            bloodType = Patient.Blood_Type.O;
        }



        // Add patient to selected ward
        selectedWard.Patients.Add(new Patient(addPatientName.Text.Trim(), addPatientDOB.DisplayDate, bloodType));
    }

    // Handle Add Patient button enable/disable
    private void AddPatientName_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        addPatientBtn.IsEnabled = !string.IsNullOrWhiteSpace(((TextBox)sender).Text);
    }

    // Loads data from FILE_PATH
    private void LoadData_OnClick(object sender, RoutedEventArgs e)
    {

        wardList.Clear();
        try
        {
            string file = File.ReadAllText(FILE_PATH);

            ObservableCollection<Ward>? deserialize = JsonSerializer.Deserialize<ObservableCollection<Ward>>(file);
            Ward.WardCount = 0;
            foreach (Ward ward in deserialize)
            {
                wardList.Add(ward);
                Ward.WardCount++;
            }
            UpdateWardsHeaderUI();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Tried to load data but failed. To load data, please place a file named 'data.json' on your Desktop.");
        }
    }

    // Updates Wards Header in UI with number of Wards
    public void UpdateWardsHeaderUI()
    {
        headerWards.Text = $"Wards ({Convert.ToString(Ward.WardCount)})";
    }

    // Write data to file on Save
    private void SaveData_OnClick(object sender, RoutedEventArgs e)
    {
        ObservableCollection<Ward> data = wardList;
        string jsonfile = JsonSerializer.Serialize(data);
        File.WriteAllText(FILE_PATH, jsonfile);
    }

    // Initialises program with data on load
    private void InitData_OnLoad(object sender, RoutedEventArgs e)
    {
        // Tries to load data.json file, otherwise starts with no data
        LoadData_OnClick(sender, e);
    }
}