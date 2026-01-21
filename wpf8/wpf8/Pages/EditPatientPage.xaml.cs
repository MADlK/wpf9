using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace wpf8.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPatientPage.xaml
    /// </summary>
    public partial class EditPatientPage : Page
    {
        
            private Doctor _currentDoctor;
            private Pacient _currentPatient;

            public EditPatientPage(Doctor doctor, Pacient patient)
            {
                InitializeComponent();
                _currentDoctor = doctor;
                _currentPatient = patient;

                LoadPatientData();
            }
            public static void SavePatient(Pacient patient)
            {
                if (!Directory.Exists("Patients"))
                    Directory.CreateDirectory("Patients");

                string fileName = $"P_{patient.Id}.json";
                string filePath = Path.Combine("Patients", fileName);

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(patient, options);
                File.WriteAllText(filePath, json);
            }
            private void LoadPatientData()
            {
                PatientIdText.Text = $"ID пациента: {_currentPatient.Id}";
                LastNameTextBox.Text = _currentPatient.LastName;
                NameTextBox.Text = _currentPatient.Name;
                MiddleNameTextBox.Text = _currentPatient.MiddleName;
                PhoneTextBox.Text = _currentPatient.PhoneNumber;

                if (!string.IsNullOrEmpty(_currentPatient.Birthday))
                {
                    if (DateTime.TryParseExact(_currentPatient.Birthday, "dd.MM.yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime birthday))
                    {
                        BirthdayDatePicker.SelectedDate = birthday;
                    }
                }

                BirthdayDatePicker.DisplayDateEnd = DateTime.Today;
                LastNameTextBox.Focus();
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
                if (string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(MiddleNameTextBox.Text) ||
                    !BirthdayDatePicker.SelectedDate.HasValue ||
                    string.IsNullOrWhiteSpace(PhoneTextBox.Text))
                {
                    MessageBox.Show("Все поля обязательны");
                    return;
                }

                if (BirthdayDatePicker.SelectedDate > DateTime.Today)
                {
                    MessageBox.Show("Дата рождения не может быть в будущем");
                    return;
                }

                string phone = PhoneTextBox.Text.Trim();
                if (!phone.StartsWith("+"))
                {
                    MessageBox.Show("Телефон должен начинаться с +");
                    return;
                }

                _currentPatient.LastName = LastNameTextBox.Text.Trim();
                _currentPatient.Name = NameTextBox.Text.Trim();
                _currentPatient.MiddleName = MiddleNameTextBox.Text.Trim();
                _currentPatient.Birthday = BirthdayDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy");
                _currentPatient.PhoneNumber = phone;

                SavePatient(_currentPatient);

                MessageBox.Show("Данные пациента обновлены");

                NavigationService.GoBack();
            }

            private void BackButton_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.GoBack();
            }
        }
    
}
