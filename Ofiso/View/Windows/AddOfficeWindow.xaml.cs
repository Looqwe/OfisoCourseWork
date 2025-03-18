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
        public AddOfficeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddOffice();
        }

        public void AddOffice()
        {
            if (string.IsNullOrWhiteSpace(TitleTb.Text) ||
                string.IsNullOrWhiteSpace(AddressTb.Text) ||
                string.IsNullOrWhiteSpace(FloorTb.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTb.Text) ||
                string.IsNullOrWhiteSpace(PriceTb.Text)
                )
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }

            try
            {
                Offices newOff = new Offices()
                {
                    Title = TitleTb.Text, // Заголовок из TextBox
                    Address = AddressTb.Text, // Адрес из TextBox
                    Floor = int.TryParse(FloorTb.Text, out int floor) ? floor : (int?)null, // Этаж (с проверкой на число)
                    Description = DescriptionTb.Text, // Описание
                    PricePerMont = decimal.Parse(PriceTb.Text), // Цена (обязательное поле)
                    CreatedDate = DateTime.Now, // Текущая дата (устанавливается автоматически)
                };

                App.context.Offices.Add(newOff);
                App.context.SaveChanges();
                MessageBox.Show("Объявление добавлено!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        Offices offices = new Offices();

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            foreach (var photo in fileDialog.FileNames)
            {
                offices.Photo = File.ReadAllBytes(photo);
                App.context.Offices.Add(offices);

            }
            MessageBox.Show("Фотография добавлена");
        }
    }
}

