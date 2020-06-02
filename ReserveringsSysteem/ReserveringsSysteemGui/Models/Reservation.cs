using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using ReserveringsSysteemGui.Enums;

namespace ReserveringsSysteemGui.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public string UserCode { get; set; }

        public string BookingName { get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfGuests { get; set; }

        public SettingEnumeration Setting { get; set; }

        public string BookingDateTime { get; set; }

        public string BookingEndDateTime { get; set; }

        public List<Table> Tables { get; set; }
    }
}
