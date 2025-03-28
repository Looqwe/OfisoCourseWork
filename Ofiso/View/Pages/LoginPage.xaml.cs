﻿using Ofiso.AppData;
using Ofiso.Models;
using Ofiso.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Data.Entity;

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.currentUser = App.context.Users
         .Include(u => u.UserType1)
         .FirstOrDefault(user =>
             user.NumberPhone == PhoneTb.Text &&
             user.Password == PasswordPb.Password);

            if (App.currentUser != null)
            {
                // Проверка прав администратора
                if (App.currentUser.UserType1?.Type == "Admin")
                {
                    App.IsAdmin = true;
                }

                AppState.CurrentUserId = App.currentUser.ID;

                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    // Скрываем кнопку "Назад"
                    mainWindow.SetBackButtonVisibility(false);

                    // Обновляем интерфейс
                    mainWindow.UpdateAdminVisibility();
                    mainWindow.AuthorizationPage.Navigate(new MainPage(AppState.CurrentUserId));
                }
            }
            else
            {
                MessageBox.Show("Неверный номер телефона или пароль!");
            }
            App.currentUser = App.context.Users
            .Include(u => u.UserType1)
            .FirstOrDefault(user =>
            user.NumberPhone == PhoneTb.Text &&
            user.Password == PasswordPb.Password);

            if (App.currentUser != null)
            {
                // Проверка прав администратора
                if (App.currentUser.UserType1?.Type == "Admin")
                {
                    App.IsAdmin = true;
                }

                AppState.CurrentUserId = App.currentUser.ID;

                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    // Скрываем кнопку "Назад"
                    mainWindow.SetBackButtonVisibility(false);

                    // Обновляем интерфейс
                    mainWindow.UpdateAdminVisibility();
                    mainWindow.AuthorizationPage.Navigate(new MainPage(AppState.CurrentUserId));
                }
            }
            
        }
       
    }
}
