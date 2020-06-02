using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using ReserveringsSysteemGui.Handlers;
using ReserveringsSysteemGui.Models;

namespace ReserveringsSysteemGui.UserControls
{
    /// <summary>
    /// Interaction logic for AdminViewAllReservationsControl.xaml
    /// </summary>
    public partial class AdminViewAllReservationsControl : UserControl
    {
        private readonly UserModel _user;

        public AdminViewAllReservationsControl(UserModel user)
        {
            InitializeComponent();

            _user = user;

            _ = UpdateAsync();
        }

        // Make an async API GET call every 5 sec and get all reservations and delete all overdue reservations.
        // We make this call on the background thread so the main UI will not freeze.
        #region Periodic API call

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
            await Dispatcher.InvokeAsync(async() =>
            {
                var apiHandler = new ApiHandler();
                await Task.Run(() => apiHandler.DeleteReservations());
                await GetAllReservations();
            });

        }

        #endregion

        /// <summary>
        /// Get the current parent window and switch to the default control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new DefaultControl(_user);
        }

        /// <summary>
        /// Get all the reservations of all users
        /// </summary>
        private async Task GetAllReservations()
        {
            var apiHandler = new ApiHandler();

            var result = await Task.Run(() => apiHandler.GetAllReservationsTask());
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
