using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
    /// Логика взаимодействия для AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
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

        private Doctor _currentDoctor;
        private Pacient _currentPatient;

        public AppointmentPage(Doctor doctor, Pacient patient)
        {
            InitializeComponent();
            _currentDoctor = doctor;
            _currentPatient = patient;

            PatientInfoText.Text = $"Пациент: {_currentPatient.LastName} {_currentPatient.Name} {_currentPatient.MiddleName}";
            AppointmentsListView.ItemsSource = _currentPatient.AppointmentStories;
            DiagnosisTextBox.Focus();
        }

        private void SaveAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiagnosisTextBox.Text))
            {
                MessageBox.Show("Введите диагноз");
                DiagnosisTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(RecommendationsTextBox.Text))
            {
                MessageBox.Show("Введите рекомендации");
                RecommendationsTextBox.Focus();
                return;
            }

            var newAppointment = new Pacient.AppointmentStory
            {
                date = DateTime.Now.ToString("dd.MM.yyyy"),
                doctor_id = _currentDoctor.Id,
                Diagnos = DiagnosisTextBox.Text.Trim(),
                Recomendations = RecommendationsTextBox.Text.Trim()
            };

            _currentPatient.AppointmentStories.Add(newAppointment);

            SavePatient(_currentPatient);

            AppointmentsListView.ItemsSource = null;
            AppointmentsListView.ItemsSource = _currentPatient.AppointmentStories;

            DiagnosisTextBox.Clear();
            RecommendationsTextBox.Clear();
            DiagnosisTextBox.Focus();

            MessageBox.Show("Приём сохранён");
        }

        private void EditPatientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditPatientPage(_currentDoctor, _currentPatient));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
