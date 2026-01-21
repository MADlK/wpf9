using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage :Page
    {
        public ObservableCollection<User> Users { get; set; } = new();
        public User? SelectedUser { get; set; }


        private void DeleteItem_Click (object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            bool confirm = MessageBox.Show("Вы действительно хотите удалить запись?",
                                            "Подтверждение удаления",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Question
                                            ) == MessageBoxResult.Yes;
            if (confirm)
            {
                Users.Remove(SelectedUser);
            }
        }

        private void AddButton_Click (object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserFormPage());
        }

        public ListPage ()
        {
            Users.Add(new User() { Id= 1, Name = "Иван", Email = "ivan@mail.ru", Age = 12 });
            Users.Add(new User() { Id= 2, Name = "диама", Email = "атиттвата@mail.ru", Age = 13 });
            Users.Add(new User() { Id= 3, Name = "фыавфы", Email = "йцукйцуй@mail.ru", Age = 14 });
            Users.Add(new User() { Id= 4, Name = "выапфы", Email= "фпфыв@mail.ru", Age = 15 });
            Users.Add(new User() { Id= 5, Name = "ыавпы", Email= "фываф@mail.ru", Age = 17 });
            InitializeComponent();
            DataContext = this;
        }
    }
}
