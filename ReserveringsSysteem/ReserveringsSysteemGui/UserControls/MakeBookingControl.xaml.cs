using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using ReserveringsSysteemGui.Enums;
using ReserveringsSysteemGui.Handlers;
using ReserveringsSysteemGui.Models;
using TimeSpan = System.TimeSpan;

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

            settingComboBox.ItemsSource = Enum.GetValues(typeof(SettingEnumeration)).Cast<SettingEnumeration>();

            UpdateAsync();
        }

        // Make an async API GET call every 5 sec and get all reservations by usercode and delete all overdue reservations.
        // We make this call on the background thread so the main UI will not freeze.
        #region Periodic API call

        private async void UpdateAsync()
        {
            while (true)
            {
                await OnTick();
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
        
        private async Task OnTick()
        {
            await Dispatcher.InvokeAsync(async () =>
            {
                var apiHandler = new ApiHandler();
                await Task.Run(() => apiHandler.DeleteReservations());
                await GetUserReservations(_user.UserCode);
            });
        }

        #endregion


        /// <summary>
        /// Get the current parent window and switch to the default control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new DefaultControl(_user);
        }

        /// <summary>
        /// Onclick execute this function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var apiHandler = new ApiHandler();

            if (CustomerNameBox.Text == string.Empty || CustomerPhonenumberBox.Text == string.Empty ||
                AmountOfPersonsBox.Text == string.Empty || settingComboBox.Text == string.Empty ||
                !BookingDatePicker.SelectedDate.HasValue || !BookingTimePicker.SelectedTime.HasValue) return;

            if (!int.TryParse(AmountOfPersonsBox.Text, out var i)) return;

            var reservation = new Reservation
            {
                UserCode = _user.UserCode,
                Setting = Enum.Parse<SettingEnumeration>(settingComboBox.Text),
                BookingName = CustomerNameBox.Text,
                PhoneNumber = CustomerPhonenumberBox.Text,
                NumberOfGuests = i,
                BookingDateTime = (BookingDatePicker.SelectedDate.Value + BookingTimePicker.SelectedTime.Value.TimeOfDay).ToString(CultureInfo.CurrentCulture),
                BookingEndDateTime = (BookingDatePicker.SelectedDate.Value + BookingTimePicker.SelectedTime.Value.TimeOfDay.Add(TimeSpan.FromHours(1))).ToString(CultureInfo.CurrentCulture)
            };

            // Get result from LoginUserAsync async
            var result = Task.Run(() => apiHandler.MakeReservationTask(reservation)).Result;

            CustomerNameBox.Text = string.Empty;
            CustomerPhonenumberBox.Text = string.Empty;
            AmountOfPersonsBox.Text = string.Empty;
            settingComboBox.Text = string.Empty;
            BookingDatePicker.SelectedDate = null;
            BookingTimePicker.SelectedTime = null;

            try
            {
                MessageBox.Show(JsonConvert.DeserializeObject<string>(result));
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception}\n{JsonConvert.DeserializeObject<string>(result)}");
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Get the reservations of the currently logged in user and fill the reservationsGrid
        /// </summary>
        /// <param name="userCode"></param>
        private async Task GetUserReservations(string userCode)
        {
            var apiHandler = new ApiHandler();

            var result = await Task.Run(() => apiHandler.GetReservationsTask(userCode));
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(result);

            var reservationTableObjects = reservations.Select(obj => new ReservationTableObject
                {
                    BookingName = obj.BookingName,
                    BookingDateTime = obj.BookingDateTime,
                    NumberOfGuests = obj.NumberOfGuests,
                    PhoneNumber = obj.PhoneNumber,
                    Setting = obj.Setting
                })
                .ToList();
            reservationsGrid.ItemsSource = reservationTableObjects;

            reservationsGrid.Columns[0].Header = "Reservation name";
            reservationsGrid.Columns[1].Header = "Phone number";
            reservationsGrid.Columns[2].Header = "Number of guests";
            reservationsGrid.Columns[3].Header = "Setting";
            reservationsGrid.Columns[4].Header = "Reservation time";
        }

    }
}
