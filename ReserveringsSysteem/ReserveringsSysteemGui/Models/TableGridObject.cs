using System;
using System.Collections.Generic;
using System.Text;
using ReserveringsSysteemGui.Enums;

namespace ReserveringsSysteemGui.Models
{
    class TableGridObject
    {
        public int TableNumber { get; set; }

        public SettingEnumeration Setting { get; set; }

        public bool IsOccupied { get; set; }
    }
}
