using System;
using System.Collections.Generic;
using System.Text;
using ReserveringsSysteemGui.Enums;

namespace ReserveringsSysteemGui.Models
{
    public class Table
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }

        public SettingEnumeration Setting { get; set; }
    }
}
