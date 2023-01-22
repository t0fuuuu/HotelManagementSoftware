using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();
List<Room> roomList = new List<Room>();

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

void CreateRoom(List<Room> roomList, List<Guest> guestList)
{
    string[] roomLine = File.ReadAllLines("Rooms.csv");
    for(int i = 1; i < roomLine.Length; i++)
    {
        bool temA = false;
        string[] each = roomLine[i].Split(',');
        int roomNumber = Convert.ToInt32(each[1]);
        foreach(Guest guest in guestList)
        {
            if (guest.IsCheckedIn == true)
            {
                foreach(Room r in guest.HotelStay.RoomList)
                {
                    if (r.RoomNumber == roomNumber)
                    {
                        temA = true;
                        break;
                    }
                }
            }
        }
        if (each[0] == "Standard")
        {
            Room room = new StandardRoom(roomNumber, each[2], Convert.ToDouble(each[3]), temA);
            roomList.Add(room);
        }
        else if (each[0] == "Deluxe")
        {
            Room room = new DeluxeRoom(roomNumber, each[2], Convert.ToDouble(each[3]), temA);
            roomList.Add(room);
        }
    }
}

void DisplayGuests(List<Guest> guestList)
{
    foreach (Guest guest in guestList) 
    {
        Console.WriteLine("Name: {0,-7}   Passport Number: {1,-9}   Membership Status: {2,-8}   Membership Points: {3,-5}   IsCheckedIn: {4,0}",
            guest.Name, guest.PassportNum, guest.Member.Status, guest.Member.Points, guest.IsCheckedIn);
    }
}

void DisplayRoom(List<Room> roomList)
{
    foreach (Room room in roomList)
    {
        Console.WriteLine(room.IsAvail);
    }
}

CreateGuests(guestList);
DisplayGuests(guestList);
CreateRoom(roomList, guestList);
DisplayRoom(roomList);

