using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CafeMoenenAPI.Enums;

namespace CafeMoenenAPI.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public string UserCode { get; set; }

        public string BookingName { get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfGuests { get; set; }

        public SettingEnumeration Setting { get; set; }

        public string BookingDateTime { get; set; }

        public List<TableModel> Tables { get; set; }
    }
}
