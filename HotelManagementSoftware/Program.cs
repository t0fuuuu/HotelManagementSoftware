using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();


void CreateGuests(List<Guest> guestList)
{
    string[] guestLine = File.ReadAllLines("Guests.csv");
    for (int i = 1; i < guestLine.Length; i++)
    {
        string[] each = guestLine[i].Split(',');
        string[] stayLine = File.ReadAllLines("Stays.csv");
        for (int j = 1; j < stayLine.Length; j++)
        {
            string[] each2 = stayLine[j].Split(",");
            // match the passport num to identify the guest
            if (each2[1] == each[1])
            {
                Stay tempStay= new Stay(Convert.ToDateTime(each2[3]), Convert.ToDateTime(each2[4]));
                Membership tempMember = new Membership(each[2], Convert.ToInt32(each[3]));
                Guest guest = new Guest(each[0], each[1], tempStay, tempMember);
                guest.IsCheckedIn = Convert.ToBoolean(each2[2]);
                guestList.Add(guest);
            }
            
        }
    }
}

void DisplayGuests(List<Guest> guestList)
{
    foreach (Guest guest in guestList) 
    {
        Console.WriteLine(guest.Name);
    }
}

CreateGuests(guestList);
DisplayGuests(guestList);

