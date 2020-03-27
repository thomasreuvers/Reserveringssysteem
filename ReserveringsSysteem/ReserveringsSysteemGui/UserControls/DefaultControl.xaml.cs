using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReserveringsSysteemGui.Models;

namespace ReserveringsSysteemGui.UserControls
{
    /// <summary>
    /// Interaction logic for DefaultControl.xaml
    /// </summary>
    public partial class DefaultControl : UserControl
    {
        private readonly UserModel _user;
        public DefaultControl(UserModel model)
        {
            InitializeComponent();

            // Set welcome message as user email
            _user = model;
            WelcomeMessage.Content = $"Welcome, {_user.EmailAddress}";
        }

        // Get the parent window and close it and show the login window
        private void LogoutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            var parent = Window.GetWindow(this);

            parent.Close();
            Application.Current.MainWindow = login;
            login.Show();
        }

        private void BookBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new MakeBookingControl(_user);
        }
    }
}
