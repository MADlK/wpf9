using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace wpf8.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserFormPage.xaml
    /// </summary>
    public partial class UserFormPage :Page
    {
        private User _user;
        private ObservableCollection<User> _users;
        private void BackButton_Click (object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void AddButton_Click (object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new UserFormPage());
        }
        public UserFormPage ()
        {
            _user = new User ();
            InitializeComponent();
            DataContext = this;
        }
    }
}
