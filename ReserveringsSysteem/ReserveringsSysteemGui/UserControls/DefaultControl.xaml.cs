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
using MahApps.Metro.IconPacks;
using MaterialDesignThemes.Wpf;
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

            _user = model;
            WelcomeMessage.Content = $"Welcome, {_user.EmailAddress}";

            if (_user.Role.Equals("ADMIN"))
            {
                InitAdminView();
            }
        }

        /// <summary>
        /// Get the parent window and close it and show the login window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            var parent = Window.GetWindow(this);

            parent.Close();
            Application.Current.MainWindow = login;
            login.Show();
        }

        /// <summary>
        /// Get the parent window and set it's content as MakingBookingControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new MakeBookingControl(_user);
        }

        /// <summary>
        /// Get the parent window and set it's content as AdminViewAllReservationsControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchToAllReservationsControl_OnClick (object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new AdminViewAllReservationsControl(_user);
        }

        /// <summary>
        /// Get the parent window and set it's content as SwitchToTableCreation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchToTableCreation_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new MakeTableControl(_user);
        }

        // Initialize admin styles
        private void InitAdminView()
        {
            #region AllReservationsBtn

            var btn = new Button();
            btn.Click += SwitchToAllReservationsControl_OnClick;
            btn.Height = 50;
            btn.Background = Brushes.Transparent;
            btn.BorderThickness = new Thickness(2);
            btn.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 165, 0));
            btn.Margin = new Thickness(5);

            var innerStackpanel = new StackPanel { Orientation = Orientation.Vertical };

            var icon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.BookSearch,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 0))
            };

            var description = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new FontFamily("Helvitica"),
                Text = "All reservations"
            };

            innerStackpanel.Children.Add(icon);
            innerStackpanel.Children.Add(description);

            btn.Content = innerStackpanel;

            #endregion

            #region TableCreationBtn

            var tableBtn = new Button();
            tableBtn.Click += SwitchToTableCreation_OnClick;
            tableBtn.Height = 50;
            tableBtn.Background = Brushes.Transparent;
            tableBtn.BorderThickness = new Thickness(2);
            tableBtn.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 165, 0));
            tableBtn.Margin = new Thickness(5);

            var tableInnerStackpanel = new StackPanel { Orientation = Orientation.Vertical };

            var tableIcon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.Desk,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 0))
            };

            var tableDescription = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontFamily = new FontFamily("Helvitica"),
                Text = "New Table"
            };

            tableInnerStackpanel.Children.Add(tableIcon);
            tableInnerStackpanel.Children.Add(tableDescription);

            tableBtn.Content = tableInnerStackpanel;

            #endregion

            MainStack.Children.Add(btn);
            MainStack.Children.Add(tableBtn);
        }
    }
}
