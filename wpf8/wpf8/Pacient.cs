using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpf8
{
    public class Pacient : INotifyPropertyChanged
    {


        public class AppointmentStory : INotifyPropertyChanged
        {
            private string _date = "";
            private int _doctor_id;
            private string _diagnos = "";
            private string _recomendations = "";

            public string date
            {
                get => _date;
                set
                {
                    if (_date != value)
                    {
                        _date = value;
                        OnPropertyChanged();
                    }
                }
            }

            public int doctor_id
            {
                get => _doctor_id;
                set
                {
                    if (_doctor_id != value)
                    {
                        _doctor_id = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Diagnos
            {
                get => _diagnos;
                set
                {
                    if (_diagnos != value)
                    {

                        _diagnos = value;
                        OnPropertyChanged();
                    }
                }
            }

            public string Recomendations
            {
                get => _recomendations;
                set
                {
                    if (_recomendations != value)
                    {

                        _recomendations = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private int _id;
        private string _name = "";
        private string _lastName = "";
        private string _middleName = "";
        private string _birthday = "";
        private string _phoneNumber = "";
        private ObservableCollection<AppointmentStory> _appointmentStories;


        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {

                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {

                    _lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName != value)
                {
                    _middleName = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }


            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }

            }
        }

        public ObservableCollection<AppointmentStory> AppointmentStories
        {
            get => _appointmentStories ??= new ObservableCollection<AppointmentStory>();
            set
            {
                if (_appointmentStories != value)
                {

                    _appointmentStories = value;
                    OnPropertyChanged();
                }
            }
        }

        public Pacient()
        {
            _appointmentStories = new ObservableCollection<AppointmentStory>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
