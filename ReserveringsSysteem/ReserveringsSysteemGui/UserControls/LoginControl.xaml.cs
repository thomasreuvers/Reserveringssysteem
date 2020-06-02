using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Close and dispose window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindowBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Close();
        }

        /// <summary>
        /// Close the parent and show the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {

            #region API

            var apiHandler = new ApiHandler();

            var login = new LoginModel
            {
                Email = EmailBox.Text,
                Password = PasswordBox.Password
            };

            // Get result from LoginUserAsync async
            var result = Task.Run(() => apiHandler.LoginUserAsyncTask(login)).Result;

            try
            {
                // Try to convert result to UserModel object
                var userModel = JsonConvert.DeserializeObject<UserModel>(result);

                // Authentication was successful switch to main app screen
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

            #endregion
        }

        /// <summary>
        /// Get the parent window and set it's content as Register control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new RegisterControl();
        }
    }
}
