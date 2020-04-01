using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CafeMoenenAPI.Enums;
using CafeMoenenAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeMoenenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public ReservationController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/reservation/gettables
        [HttpGet("gettables")]
        public IActionResult GetTables()
        {
            return Ok(_dbContext.Tables.Where(x => x.IsOccupied == false).ToList());
        }

        // POST: api/reservation/makereservation
        [HttpPost("makereservation")]
        public IActionResult MakeReservation([FromBody]ReservationModel model)
        {
            var tables = _dbContext.Tables.Where(x => x.IsOccupied == false && x.Setting == model.Setting).ToList();
            var neededTables = (int) Math.Ceiling(decimal.Divide(model.NumberOfGuests, 6));

            var reservation = new ReservationModel {Tables = new List<TableModel>()};


            // Check if there are enough tables left for this reservation
            if (tables.Count < neededTables) return BadRequest($"The are not enough tables left for this reservation! {model.Setting}");

            // Set the reservation object values
            for (var i = 0; i < neededTables; i++)
            {
                tables[i].IsOccupied = true;
                reservation.Tables.Add(tables[i]);
            }
            reservation.UserCode = model.UserCode;
            reservation.BookingDateTime = model.BookingDateTime;
            reservation.NumberOfGuests = model.NumberOfGuests;
            reservation.Setting = model.Setting;
            reservation.BookingName = model.BookingName;
            reservation.PhoneNumber = model.PhoneNumber;

            _dbContext.Add(reservation);
            _dbContext.SaveChanges();

            return Ok($"Successfully saved reservation on name {model.BookingName} for {reservation.BookingDateTime}");
        }

        // POST: api/reservation/getreservations
        [HttpPost("getreservations")]
        public IActionResult GetReservations([FromBody] string userCode)
        {
            return Ok(_dbContext.Reservations.Where(x => x.UserCode == userCode).ToList());
        }

        // DELETE: api/reservation/deletereservations
        /// <summary>
        /// Delete all expired reservations
        /// </summary>
        [HttpDelete("deletereservations")]
        public IActionResult DeleteReservations()
        {
            var reservations = _dbContext.Reservations.OrderBy(x => x.Id).ToList();
            var tables = _dbContext.Tables.OrderBy(x => x.Id).ToList();

            foreach (var reservation in reservations.Where(reservation => Convert.ToDateTime(reservation.BookingDateTime) <= DateTime.Now))
            {
                foreach (var table in reservation.Tables)
                {
                    table.IsOccupied = false;
                }

                _dbContext.Remove(reservation);
            }

            _dbContext.SaveChanges();
            return Ok();
        }

    }
}