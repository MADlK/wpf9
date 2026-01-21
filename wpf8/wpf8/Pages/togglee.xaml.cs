using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для togglee.xaml
    /// </summary>
    public partial class togglee : Page
    {
        public togglee()
        {
            InitializeComponent();
        }

        

        private void DEF(object sender, RoutedEventArgs e)
        {
            ThemeHelper.Def();
        }

        private void DARK(object sender, RoutedEventArgs e)
        {
            ThemeHelper.Dark();
        }
    }
}
