using HotalAppLibrary.Databases;
using HotalAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotalAppLibrary.Data
{
    public class SqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "sqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;

        }

        // method to get available room types 
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                                                        new { startDate, endDate },
                                                          connectionStringName,
                                                        true);
        }

        // method to book a guest
        public void BookGuest(string firstName,
                              string lastName,
                              DateTime startDate,
                              DateTime endDate,
                              int roomTypeId)
        {
            // get guest record
            GuestModel guest = _db.LoadData<GuestModel, dynamic>("dbo.spGuests_Insert",
                                                                 new {firstName, lastName },
                                                                 connectionStringName,
                                                                 true).First();

            RoomTypeModel roomType = _db.LoadData<RoomTypeModel, dynamic>("select * from dbo.RoomTypes where Id=@Id",
                                                                 new { Id = roomTypeId},
                                                                 connectionStringName,
                                                                 false).First();

            var stayingTime = endDate.Date.Subtract(startDate);

            // get available rooms for specific roomtypeId
            List<RoomModel> availableRooms = _db.LoadData<RoomModel, dynamic>("dbo.spRooms_GetAvailableRooms",
                                                       new { startDate, endDate, roomTypeId}, connectionStringName, true);

            // booking guest
            _db.SaveData("dbo.spBookings_Insert",
                         new {
                             roomId = availableRooms.First().Id, 
                             guestId = guest.Id, 
                             startDate, endDate, 
                             totalCost = stayingTime.Days * roomType.Price 
                         },
                         connectionStringName, 
                         true);

        }


        // method for searching booking
        public List<BookingFullModel> SearchBooking(string lastName)
        {
            return _db.LoadData<BookingFullModel, dynamic>("dbo.spBookingsSearch", 
                  new {
                      lastName, startDate = DateTime.Now.Date },
                  connectionStringName,
                  true);

        }


        public void CheckInGuest(int bookingId)
        {
            _db.SaveData("dbo.spBookings_Checkin", new {Id=bookingId},connectionStringName, true);
        }
    }
}
