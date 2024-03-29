﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class Stay
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<Room> RoomList { get; set; } = new List<Room>();
        public Stay() { }
        public Stay(DateTime CID, DateTime COD)
        {
            CheckInDate = CID;
            CheckOutDate = COD;
        }
        public void AddRoom(Room r)
        {
            RoomList.Add(r);
        }
        public double CalculateTotal()
        {
            int days = CheckOutDate.Subtract(CheckInDate).Days;
            double total = 0;
            foreach(Room r in RoomList) 
            {
                total += days * r.CalculateCharges();
            }
            return total;
        }
        public override string ToString()
        {
            return "Check In Date: " + CheckInDate + " Check Out Date: " + CheckOutDate;
        }
    }
}
