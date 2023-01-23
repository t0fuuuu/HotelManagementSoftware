using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();
List<Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();

void CreateGuests(List<Guest> guestList)
{
    string[] guestLine = File.ReadAllLines("Guests.csv");
    for (int i = 1; i < guestLine.Length; i++)
    {
        string[] each = guestLine[i].Split(',');
        string[] stayLine = File.ReadAllLines("Stays.csv");
        Membership tempMember = new Membership(each[2], Convert.ToInt32(each[3]));
        Guest guest = new Guest(each[0], each[1], null, tempMember);
        guestList.Add(guest);
           
    }
}

void CreateStay(List<Stay> stayList, List<Guest> guestList)
{
    string[] stayLine = File.ReadAllLines("Stays.csv");
    for (int i = 1; i < stayLine.Length; i++)
    {
        string[] each = stayLine[i].Split(",");
        Stay tempStay = new Stay(Convert.ToDateTime(each[3]), Convert.ToDateTime(each[4]));
        stayList.Add(tempStay);
        foreach (Guest g in guestList)
        {
            if (g.PassportNum == each[1])
            {
                g.HotelStay = tempStay;
                g.IsCheckedIn= Convert.ToBoolean(each[2]);
                break;
            }
        }
    }
}

void AddRoom(List<Guest> guestList, List<Room> roomList)
{
    string[] stayLine = File.ReadAllLines("Stays.csv");
    for (int i = 1; i < stayLine.Length; i++)
    {
        string[] each = stayLine[i].Split(",");
        string[] header = stayLine[0].Split(",");
        for (int j = 0; j < header.Length; j++)
        {
            if (header[j] == "RoomNumber")
            {
                foreach (Room room in roomList)
                {
                    if (each[j] == "")
                    {
                        break;
                    }
                    else if (room.RoomNumber == Convert.ToInt32(each[j]))
                    {
                        foreach (Guest guest in guestList)
                        {
                            if (guest.PassportNum == each[1])
                            {
                                guest.HotelStay.AddRoom(room);
                                if (guest.IsCheckedIn == false)
                                room.IsAvail = true;
                                break;
                                
                            }
                        }
                    }
                }
            }

        }
    }
}

void CreateRoom(List<Room> roomList)
{
    string[] roomLine = File.ReadAllLines("Rooms.csv");
    for(int i = 1; i < roomLine.Length; i++)
    {
        string[] each = roomLine[i].Split(',');
        int roomNumber = Convert.ToInt32(each[1]);
        if (each[0] == "Standard")
        {
            Room room = new StandardRoom(roomNumber, each[2], Convert.ToDouble(each[3]), false);
            roomList.Add(room);
        }
        else if (each[0] == "Deluxe")
        {
            Room room = new DeluxeRoom(roomNumber, each[2], Convert.ToDouble(each[3]), false);
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

void DisplayAvailRoom(List<Room> roomList)
{
    Console.WriteLine("{0,-17}{1,-23}{2}", "Room Number", "Bed Configuration", "Daily Rate");
    foreach (Room room in roomList)
    {
        if (room.IsAvail == true)
        {
            Console.WriteLine("{0,-17}{1,-23}{2}", room.RoomNumber, room.BedConfiguration, room.DailyRate);
        }
    }
}

CreateGuests(guestList);
CreateStay(stayList, guestList);
CreateRoom(roomList);
AddRoom(guestList, roomList);
DisplayGuests(guestList);

DisplayAvailRoom(roomList);




