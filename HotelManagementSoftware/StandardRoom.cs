using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class StandardRoom: Room
    {
        public bool RequireWifi { get; set; }

        public bool RequireBreakfast { get; set; }

        public StandardRoom(){ }

        public StandardRoom(int r, string bedcfg, double dr, bool isavl): base(r,bedcfg, dr, isavl) { }

        public override double CalculateCharges()
        {
            double TotalAddOn = 0;
            if (RequireWifi)
            {
                TotalAddOn += 10;
            }
            if (RequireBreakfast) 
            {
                TotalAddOn += 20;
            }

            return TotalAddOn;
        }

        public override string ToString()
        {
            return base.ToString() + " RequireWifi: " + RequireWifi + " RequireBreakfast: " + RequireBreakfast;
        }
    }
}
