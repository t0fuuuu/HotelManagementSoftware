using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    abstract class Room
    {
        public int RoomNumber { get; set; }
        public string? BedConfiguration { get; set; }
        public double DailyRate { get; set; }
        public bool IsAvail { get; set; }
        public Room() { }
        public Room(int r, string bedcfg,double dr, bool isavl)
        {
            RoomNumber = r;
            BedConfiguration = bedcfg;
            DailyRate = dr;
            IsAvail = isavl;
        }
        public abstract double CalculateCharges();
        public override string ToString()
        {
            return "Room Number: " + RoomNumber + "Bed Configuration: " + BedConfiguration + "Daily Rate: " + DailyRate + "IsAvailable: " + IsAvail;
        }
    }
}
