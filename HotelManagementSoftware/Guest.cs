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
        public Membership Member { get; set; }
        public bool IsCheckedin { get; set; }
        public Guest() { }
        public Guest(string? n, string? ppnum,Membership m,bool ischeck)
        {
            Name = n;
            PassportNum = ppnum;
            Member = m;
            IsCheckedin = ischeck;
        }
    }
}
