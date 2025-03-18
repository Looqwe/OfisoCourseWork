using Ofiso.AppData;
using Ofiso.Models;
using Ofiso.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users _currentUser;
        private static Entities context = new Entities();

        public UserPage(int userId)
        {
            InitializeComponent();
            LoadData(userId);

        }
        private void LoadData(int userId)
        {
            try
            {
                // Получаем конкретного пользователя по ID
                _currentUser = context.Users
                    .FirstOrDefault(u => u.ID == userId);

                DataContext = _currentUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {

            new ChangePasswordWindow().ShowDialog();

        }
    }
}
