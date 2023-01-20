using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class Membership
    {
        public string Status { get; set; }
        
        public int Points { get; set; }

        public Membership() { }
        public Membership(string s, int p )
        {
            Status = s;
            Points = p;
        }

        public override string ToString()
        {
            return "Status: " + Status + " Points: " + Points;
        }
    }
}
