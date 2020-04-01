using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeMoenenAPI.Enums;

namespace CafeMoenenAPI.Models
{
    public class TableModel
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }

        public SettingEnumeration Setting { get; set; }

        public bool IsOccupied { get; set; }
    }
}
