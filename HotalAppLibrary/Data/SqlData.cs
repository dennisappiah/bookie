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

        // get available room types based on start date and end date
        public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            return _db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableTypes",
                                                        new { startDate, endDate },
                                                        connectionStringName,
                                                        true);
        }



    }
}
