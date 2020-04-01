using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CafeMoenenAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TableModel> Tables { get; set; }

        public DbSet<ReservationModel> Reservations { get; set; }
    }
}
