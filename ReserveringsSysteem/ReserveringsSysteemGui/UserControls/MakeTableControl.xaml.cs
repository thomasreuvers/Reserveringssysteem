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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using ReserveringsSysteemGui.Enums;
using ReserveringsSysteemGui.Handlers;
using ReserveringsSysteemGui.Models;
using Table = ReserveringsSysteemGui.Models.Table;

namespace ReserveringsSysteemGui.UserControls
{
    /// <summary>
    /// Interaction logic for MakeTableControl.xaml
    /// </summary>
    public partial class MakeTableControl : UserControl
    {
        private readonly UserModel _user;
        public MakeTableControl(UserModel user)
        {
            InitializeComponent();
            _user = user;
            settingComboBox.ItemsSource = Enum.GetValues(typeof(SettingEnumeration)).Cast<SettingEnumeration>();
            UpdateAsync();
        }

        // Make an async API GET call every 5 sec and get all tables.
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
           await Dispatcher.InvokeAsync(FillTableGrid);
        }

        #endregion

        /// <summary>
        /// Get the current parent window and switch to the default control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_OnClick(object sender, EventArgs e)
        {
            var parent = Window.GetWindow(this);
            parent.Content = new DefaultControl(_user);
        }

        /// <summary>
        /// Create a new table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeTableBtn_OnClick(object sender, EventArgs e)
        {
            var apiHandler = new ApiHandler();

            var table = new Table
            {
                Setting = Enum.Parse<SettingEnumeration>(settingComboBox.Text)
            };

            // Get the result
            var result = Task.Run(() => apiHandler.CreateTableTask(table)).Result;

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
        /// Fill the table grid
        /// </summary>
        /// <returns></returns>
        private async Task FillTableGrid()
        {
            var apiHandler = new ApiHandler();
            var result = await Task.Run(() => apiHandler.GetTablesTask());

            var tables = JsonConvert.DeserializeObject<List<Table>>(result);

            var tableObjects = tables.Select(obj => new TableGridObject
                {
                    TableNumber = obj.TableNumber,
                    Setting = obj.Setting,
                    IsOccupied = obj.IsOccupied,
                })
                .ToList();
            tableGrid.ItemsSource = tableObjects;

            tableGrid.Columns[0].Header = "Number";
            tableGrid.Columns[1].Header = "Setting";
            tableGrid.Columns[2].Header = "Occupied";
        }
    }
}
