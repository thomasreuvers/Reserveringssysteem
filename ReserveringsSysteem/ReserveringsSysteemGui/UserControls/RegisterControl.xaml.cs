using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;
using ReserveringsSysteemGui.Handlers;
using ReserveringsSysteemGui.Models;
using ReserveringsSysteemGui.Models.API_Models;

namespace ReserveringsSysteemGui.UserControls
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        public RegisterControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Onclick validates fields and registers user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            // Check if fields are empty, if so return
            if (EmailBox.Text == string.Empty || PasswordBox.Password == string.Empty ||
                PasswordConfirmBox.Password == string.Empty)
            {
                ErrorMessage.Content = "Please fill in all fields";
                return;
            }

            var apiHandler = new ApiHandler();

            var registerModel = new RegisterModel
            {
                EmailAddress = EmailBox.Text,
                Password = PasswordBox.Password
            };

            // Get result from LoginUserAsync async
            var result = Task.Run(() => apiHandler.RegisterUserAsyncTask(registerModel)).Result;

            try
            {
                // convert to UserModel object
                var userModel = JsonConvert.DeserializeObject<UserModel>(result);

                // Register was successful switch to main app screen
                var main = new MainWindow(userModel);
                var parent = Window.GetWindow(this);

                Application.Current.MainWindow = main;
                parent.Close();
                main.Show();
            }
            catch (Exception exception)
            {
                ErrorMessage.Content = JsonConvert.DeserializeObject<string>(result);
            }
        }

        /// <summary>
        /// Switches back to the loginControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new LoginControl();
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindowBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Close();
        }


        #region Password match system

        private void PasswordConfirmBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Equals(PasswordConfirmBox.Password))
            {
                ErrorMessage.Content = string.Empty;
                RegisterBtn.IsEnabled = true;
                return;
            }

            ErrorMessage.Content = "Passwords do not match";
            RegisterBtn.IsEnabled = false;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Equals(PasswordConfirmBox.Password))
            {
                ErrorMessage.Content = string.Empty;
                RegisterBtn.IsEnabled = true;
                return;
            }

            ErrorMessage.Content = "Passwords do not match";
            RegisterBtn.IsEnabled = false;
        }

        #endregion

    }
}
