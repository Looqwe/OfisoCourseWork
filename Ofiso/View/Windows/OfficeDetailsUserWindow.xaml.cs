using Microsoft.Win32;
using Ofiso.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для OfficeDetailsUserWindow.xaml
    /// </summary>
    public partial class OfficeDetailsUserWindow : Window
    {
        private readonly Entities _context = new Entities();
        private readonly Offices _office;
        private string _newPhotoPath;
        public event Action DataUpdated;

        public OfficeDetailsUserWindow(Offices office)
        {
            InitializeComponent();
            _office = _context.Offices
            .Include(o => o.Users)
            .FirstOrDefault(o => o.ID == office.ID);
            DataContext = _office;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_newPhotoPath))
                {
                    SaveImageToAppFolder();
                }

                _context.Entry(_office).State = EntityState.Modified;
                _context.SaveChanges();

                _context.SaveChanges();
                DataUpdated?.Invoke(); // Уведомляем об изменениях
                Close();

                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ну наконец-то");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить этот офис?",
                "Подтверждение",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Offices.Remove(_office);
                    _context.SaveChanges();
                    MessageBox.Show("Офис удален!");
                    Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}");
                }
            }
        }

        private void ChangePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.png)|*.jpg;*.png",
                Title = "Выберите изображение офиса"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _newPhotoPath = openFileDialog.FileName;

                // Временное отображение выбранного изображения
                var bitmap = new BitmapImage(new Uri(_newPhotoPath));
                ImageControl.Source = bitmap;

                // Обновляем привязку
                _office.Photo = _newPhotoPath; // Временный путь для предпросмотра
            }
        }

        private void SaveImageToAppFolder()
        {
            try
            {
                var imagesFolder = System.IO.Path.Combine( // Явное указание пространства имён
                    AppDomain.CurrentDomain.BaseDirectory,
                    "OfficeImages"
                );

                Directory.CreateDirectory(imagesFolder);

                // Удаление старого изображения
                if (!string.IsNullOrEmpty(_office.Photo))
                {
                    var oldFileName = System.IO.Path.GetFileName(_office.Photo); // Исправлено
                    var oldPath = System.IO.Path.Combine(imagesFolder, oldFileName); // Исправлено

                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                // Генерация нового имени и сохранение
                var newFileName = $"{Guid.NewGuid()}{System.IO.Path.GetExtension(_newPhotoPath)}"; // Исправлено
                var newPath = System.IO.Path.Combine(imagesFolder, newFileName); // Исправлено
                File.Copy(_newPhotoPath, newPath);

                // Обновление отображения
                var bitmap = new BitmapImage(new Uri(newPath));
                ImageControl.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения изображения: {ex.Message}");
                _office.Photo = null;
            }
        }
    }
}
