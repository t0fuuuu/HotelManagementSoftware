using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class DeluxeRoom: Room
    {
        public bool AdditionalBed { get; set; }

        public DeluxeRoom() { }

        public DeluxeRoom(int r, string bedcfg, double dr, bool isavl) : base(r, bedcfg, dr, isavl) { }

        public override double CalculateCharges()
        {
            double TotalAddOn = 0;
            if (AdditionalBed)
            {
                TotalAddOn += 25;
            }

            return TotalAddOn;
        }

        public override string ToString()
        {
            return base.ToString() + " AdditionalBed: " + AdditionalBed;
        }
    }
}
