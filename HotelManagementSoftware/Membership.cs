﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSoftware
{
    class Membership
    {
        public string? Status { get; set; }
        public int Points { get; set; }

        public Membership() { }
        public Membership(string? s, int p )
        {
            Status = s;
            Points = p;
        }

        // Add points based off the final amt paid and change the status respectively
        public void EarnPoints(double amt)
        {
            int change = Convert.ToInt32(amt);
            Points = Points + (change / 10);

            if (Status == "Ordinary")
            {
                if (Points >= 200)
                {
                    Status = "Gold";
                }
                else if (Points >= 100)
                {
                    Status = "Silver";
                }
            }
            else if (Status == "Silver")
            {
                if (Points >= 200)
                {
                    Status = "Gold";
                }
            }
        }

        //Minus points if gold or silver and if it is possible or not
        public bool RedeemPoints(int p)
        {
            if (Points >= p)
            {
                Points -= p;
                return true;
            }
            if (Status == "Ordinary")
            {
                if (Points >= 200)
                {
                    Status = "Gold";
                }
                else if (Points >= 100)
                {
                    Status = "Silver";
                }
            }
            else if (Status == "Silver")
            {
                if (Points >= 200)
                {
                    Status = "Gold";
                }
            }

            return false;
        }


        public override string ToString()
        {
            return "Status: " + Status + " Points: " + Points;
        }
    }
}
