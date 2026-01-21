using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        private Doctor _currentDoctor;
        private Pacient _selectedPatient;
        private int _doctorsCount;
        private int _patientsCount;
        private ObservableCollection<Pacient> _patients;
        private string _doctorInfoString;

        public Doctor CurrentDoctor
        {
            get => _currentDoctor;
            set
            {
                _currentDoctor = value;
                OnPropertyChanged();
                UpdateDoctorInfoString();
            }
        }

        public Pacient SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public int DoctorsCount
        {
            get => _doctorsCount;
            set
            {
                _doctorsCount = value;
                OnPropertyChanged();
            }
        }

        public int PatientsCount
        {
            get => _patientsCount;
            set
            {
                _patientsCount = value;
                OnPropertyChanged();
            }
        }

        public string DoctorInfoString
        {
            get => _doctorInfoString;
            set
            {
                _doctorInfoString = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pacient> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                OnPropertyChanged();
                PatientsCount = _patients?.Count ?? 0;
            }
        }

        public MainPage(Doctor doctor)
        {
            InitializeComponent();

            DataContext = this;

            CurrentDoctor = doctor;
            LoadAllPatients();
            UpdateDoctorsCount();

            Loaded += Page_Loaded;
        }

        private void UpdateDoctorInfoString()
        {
            if (CurrentDoctor == null)
            {
                DoctorInfoString = "Врач: не выбран";
            }
            else
            {
                DoctorInfoString = $"Врач: {CurrentDoctor.LastName} {CurrentDoctor.Name} ({CurrentDoctor.Specialisation})";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAllData();
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
        public static Pacient FindPatientById(int patientId)
        {
            string fileName = $"P_{patientId}.json";
            string filePath = Path.Combine("Pacient", fileName);

            if (!File.Exists(filePath))
                return null;

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Pacient>(json);
            }
            catch
            {
                return null;
            }
        }
        private void RefreshAllData()
        {
            UpdateDoctorsCount();
            LoadAllPatients();
            if (CurrentDoctor != null)
            {
                var refreshedDoctor = FindDoctorById(CurrentDoctor.Id);
                if (refreshedDoctor != null)
                {
                    CurrentDoctor = refreshedDoctor;
                }
            }
            if (SelectedPatient != null)
            {
                var refreshedPatient = FindPatientById(SelectedPatient.Id);
                if (refreshedPatient != null)
                {
                    SelectedPatient = refreshedPatient;
                }
                else
                {
                    SelectedPatient = null;
                }
            }
        }

        public static List<Pacient> LoadAllPatientss()
        {
            var patients = new List<Pacient>();

            if (!Directory.Exists("Pacients"))
                return patients;

            string[] files = Directory.GetFiles("Pacients", "P_*.json");

            foreach (string file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var patient = JsonSerializer.Deserialize<Pacient>(json);

                    string fileName = Path.GetFileNameWithoutExtension(file);
                    if (fileName.StartsWith("P_") && int.TryParse(fileName.Substring(2), out int id))
                    {
                        patient.Id = id;
                    }

                    patients.Add(patient);
                }
                catch (Exception ex)
                {
                }
            }

            return patients;
        }

        public static Doctor FindDoctorById(int id)
        {
            string fileName = $"{id}.json";
            string filePath = Path.Combine("Doctors", fileName);

            if (!File.Exists(filePath))
                return null;

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Doctor>(json);
            }
            catch
            {
                return null;
            }
        }
        private void UpdateDoctorsCount()
        {
            var doctors = LoadAllDoctors();
            DoctorsCount = doctors.Count;
        }

        private void LoadAllPatients()
        {
            var patients = LoadAllPatientss();
            Patients = new ObservableCollection<Pacient>(patients);
        }


        private void CreatePatientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePatientPage(CurrentDoctor));
        }

        private void StartAppointmentButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Сначала выберите пациента из списка");
                return;
            }

            NavigationService.Navigate(new AppointmentPage(CurrentDoctor, SelectedPatient));
        }

        private void EditPatientButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Сначала выберите пациента из списка");
                return;
            }

            NavigationService.Navigate(new EditPatientPage(CurrentDoctor, SelectedPatient));
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage1());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click (object sender, RoutedEventArgs e)
        {
            if (SelectedPatient == null)
            {
                MessageBox.Show("Пациент н выбран");
                return;
            }

            string DelP = SelectedPatient.Id.ToString();
            string PathD = Path.Combine("Pacients", $"P_{DelP}.json");
            if (File.Exists(PathD))
            {
                File.Delete(PathD);
                MessageBox.Show("Пациент удален");
            }
            else
            {
                MessageBox.Show("такой файл не найден");
            }

            LoadAllPatients();


        }
    }
}
