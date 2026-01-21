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
    /// Логика взаимодействия для LoginPage1.xaml
    /// </summary>
    public partial class LoginPage1 : Page
    {
        public LoginPage1()
        {
            InitializeComponent();
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
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string idText = IdTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(idText) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите ID и пароль");
                return;
            }

            if (!int.TryParse(idText, out int doctorId))
            {
                MessageBox.Show("ID должен быть числом");
                IdTextBox.SelectAll();
                IdTextBox.Focus();
                return;
            }

            var doctors = LoadAllDoctors();
            var doctor = doctors.FirstOrDefault(d => d.Id == doctorId && d.Password == password);

            if (doctor == null)
            {
                MessageBox.Show("Неверный ID или пароль");
                return;
            }

            NavigationService.Navigate(new MainPage(doctor));
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}

