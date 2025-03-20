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
            if (AppState.CurrentUserId == 0)
            {
                MessageBox.Show("Пользователь не авторизован!");
                return;
            }

            FrameHelper.mainFrame.Navigate(new UserPage(AppState.CurrentUserId));
        }

        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            if (AppState.CurrentUserId == 0)
            {
                MessageBox.Show("Пользователь не авторизован!");
                return;
            }

            // Передаем ID пользователя в конструктор MainPage
            FrameHelper.mainFrame.Navigate(new MainPage(AppState.CurrentUserId));
        }
        public void UpdateAdminVisibility()
        {
            AdminButton.Visibility = App.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
        }
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            FrameHelper.mainFrame.Navigate(new AdminPage());
        }
    }
}
