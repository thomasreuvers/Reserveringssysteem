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
    /// Interaction logic for MakeBookingControl.xaml
    /// </summary>
    public partial class MakeBookingControl : UserControl
    {
        private readonly UserModel _user;
        public MakeBookingControl(UserModel model)
        {
            InitializeComponent();

            _user = model;
        }

        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new DefaultControl(_user);
        }
    }
}
