using Microsoft.Win32;
using Ofiso.AppData;
using Ofiso.Models;
using Ofiso.View.Pages;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Ofiso.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOfficeWindow.xaml
    /// </summary>
    public partial class AddOfficeWindow : Window
    {
        private readonly Entities _context = new Entities();
        private readonly int _currentUserId;

        public AddOfficeWindow(int userId)
        {
            InitializeComponent();
            _currentUserId = userId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            AddOffice();

        }

        public void AddOffice()
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(TitleTb.Text) ||
                string.IsNullOrWhiteSpace(AddressTb.Text) ||
                string.IsNullOrWhiteSpace(PriceTb.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!");
                return;
            }

            try
            {
                // Создание нового объекта
                var newOffice = new Offices
                {
                    Title = TitleTb.Text,
                    Address = AddressTb.Text,
                    Floor = int.TryParse(FloorTb.Text, out int floor) ? floor : (int?)null,
                    Description = DescriptionTb.Text,
                    PricePerMont = decimal.Parse(PriceTb.Text),
                    CreatedDate = DateTime.Now,
                    Photo = PhotoTb.Text,
                    UserID = _currentUserId
                };

                // Добавление в контекст
                _context.Offices.Add(newOffice);

                // Сохранение изменений
                _context.SaveChanges();

                MessageBox.Show("Офис успешно добавлен!");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n{(ex.InnerException?.Message ?? "")}");
            }
        }

    }
}

