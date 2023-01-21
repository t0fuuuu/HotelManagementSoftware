using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class Guest
    {
        public string? Name { get; set; }
        public string? PassportNum { get; set; }
        public Stay HotelStay { get; set; }
        public Membership Member { get; set; }
        public bool IsCheckedIn { get; set; }
        public Guest() { }
        public Guest(string? n, string? ppnum, Stay s, Membership m)
        {
            Name = n;
            PassportNum = ppnum;
            HotelStay = s;
            Member = m;
        }
        public override string ToString()
        {
            return "Name: " + Name + "PassportNum: " + PassportNum + "Member: " + Member + "IsCheckedIn: " + IsCheckedIn ;
        }
    }
}
