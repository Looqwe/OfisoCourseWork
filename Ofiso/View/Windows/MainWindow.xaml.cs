using Ofiso.AppData;
using Ofiso.View.Pages;
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

namespace Ofiso.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameHelper.mainFrame = AuthorizationPage;
            FrameHelper.mainFrame.Navigate(new AuthorizationPage());
            AuthorizationPage.Navigated += AuthorizationPage_Navigated;
        }

        private void AuthorizationPage_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrameHelper.mainFrame.Navigate(new UserPage());
        }
    }
}
