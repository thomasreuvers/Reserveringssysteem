using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ReserveringsSysteemGui.Enums;

namespace ReserveringsSysteemGui.Models
{
    public class ReservationTableObject
    {

        public string BookingName { get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfGuests { get; set; }

        public SettingEnumeration Setting { get; set; }

        public string BookingDateTime { get; set; }
    }
}
