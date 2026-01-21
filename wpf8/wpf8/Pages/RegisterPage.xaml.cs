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
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            LastNameTextBox.Focus();
            SpecialisationComboBox.SelectedIndex = 0;
        }
        public static void SaveDoctor(Doctor doctor)
        {
            if (!Directory.Exists("Doctors"))
                Directory.CreateDirectory("Doctors");
            string fileName = $"D_{doctor.Id}.json";
            string filePath = Path.Combine("Doctors", fileName);
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(doctor, options);
            File.WriteAllText(filePath, json);
        }

        public static List<Doctor> LoadAllDoctors()
        {
            var doctors = new List<Doctor>();

            if (!Directory.Exists("Doctors"))
                return doctors;

            string[] files = Directory.GetFiles("Doctors", "D_*.json");

            foreach (string file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var doctor = JsonSerializer.Deserialize<Doctor>(json);

                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (fileName.StartsWith("D_") && int.TryParse(fileName.Substring(2), out int id))
                    {
                        doctor.Id = id;
                    }

                    doctors.Add(doctor);
                }
                catch (Exception ex)
                {
                }
            }

            return doctors;
        }

        public static int GenerateDoctorId(List<Doctor> existingDoctors)
        {
            Random rnd = new Random();
            int newId;
            bool isUnique;

            do
            {
                isUnique = true;
                newId = rnd.Next(10000, 99999);

                foreach (var doctor in existingDoctors)
                {
                    if (doctor.Id == newId)
                    {
                        isUnique = false;
                        break;
                    }
                }
            } while (!isUnique);

            return newId;
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(MiddleNameTextBox.Text) ||
                SpecialisationComboBox.SelectedItem == null ||
                string.IsNullOrWhiteSpace(PasswordBox1.Password) ||
                string.IsNullOrWhiteSpace(PasswordBox2.Password))
            {
                MessageBox.Show("Все поля обязательны");
                return;
            }

            if (PasswordBox1.Password != PasswordBox2.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                PasswordBox1.Focus();
                return;
            }

            var existingDoctors = LoadAllDoctors();
            int newId = GenerateDoctorId(existingDoctors);

            var newDoctor = new Doctor
            {
                Id = newId,
                LastName = LastNameTextBox.Text.Trim(),
                Name = NameTextBox.Text.Trim(),
                MiddleName = MiddleNameTextBox.Text.Trim(),
                Specialisation = (SpecialisationComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "",
                Password = PasswordBox1.Password
            };

            SaveDoctor(newDoctor);

            MessageBox.Show($"врач зарегистрирован ID: {newId}");

            NavigationService.GoBack();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
