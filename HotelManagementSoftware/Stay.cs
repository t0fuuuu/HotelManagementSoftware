using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class Stay
    {
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<Room> RoomList { get; set; } = new List<Room>();
        public Stay() { }
        public Stay(DateTime CID, DateTime COD)
        {
            CheckinDate = CID;
            CheckOutDate = COD;
        }
        public AddRoom(Room r)
        {
            RoomList.Add(r);
        }
        public double CalculateTotal()
        {

        }
    }
}
