using System;
using System.Collections.Generic;
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
using Table = ReserveringsSysteemGui.Models.Table;
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


            // _ = RunPeriodicAsync(OnTick, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5), CancellationToken.None);
            _ = UpdateAsync();

        }

        // Every 5 seconds these calls are executed (delete and get);
        #region Periodic API call

        // private async Task RunPeriodicAsync(Action onTick, TimeSpan dueTime, TimeSpan interval, CancellationToken token)
        // {
        //     if (dueTime > TimeSpan.Zero)
        //     {
        //         await Task.Delay(dueTime, token);
        //
        //         while (!token.IsCancellationRequested)
        //         {
        //             onTick?.Invoke();
        //
        //             if (interval > TimeSpan.Zero)
        //             {
        //                 await Task.Delay(interval, token);
        //             }
        //         }
        //     }
        // }

        private async Task UpdateAsync()
        {
            while (true)
            {
                await OnTick();
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private async Task OnTick()
        {
            var apiHandler = new ApiHandler();
            await Task.Run(() => apiHandler.DeleteReservations());
            GetUserReservations(_user.UserCode);
        }

        #endregion

        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new DefaultControl(_user);
        }

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
                BookingDateTime = (BookingDatePicker.SelectedDate.Value + BookingTimePicker.SelectedTime.Value.TimeOfDay).ToString(CultureInfo.CurrentCulture)
            };

            // Get result from LoginUserAsync async
            var result = Task.Run(() => apiHandler.MakeReservationTask(reservation)).Result;

            try
            {
                // convert to UserModel object
                MessageBox.Show(JsonConvert.DeserializeObject<string>(result));
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception}\n{JsonConvert.DeserializeObject<string>(result)}");
                MessageBox.Show(exception.Message);
            }
        }

        private void GetUserReservations(string userCode)
        {
            var apiHandler = new ApiHandler();

            var result = Task.Run(() => apiHandler.GetReservationsTask(userCode)).Result;
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(result);

            if (reservations != null)
            {

                var reservationTableObjects = reservations.Select(obj => new ReservationTableObject
                    {
                        BookingName = obj.BookingName,
                        BookingDateTime = obj.BookingDateTime,
                        NumberOfGuests = obj.NumberOfGuests,
                        PhoneNumber = obj.PhoneNumber,
                        Setting = obj.Setting
                    })
                    .ToList();

                reservationsGrid.Visibility = Visibility.Visible;
                reservationsGrid.ItemsSource = reservationTableObjects;
            }
            else
            {
                reservationsGrid.Visibility = Visibility.Hidden;
            }
        }

    }
}
