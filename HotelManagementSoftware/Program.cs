using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();
List<Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();

int DisplayMenu()
{
    Console.WriteLine("---Hotel Management System---");
    Console.WriteLine("[1] Display Hotel Guests");
    Console.WriteLine("[2] Display Available Rooms");
    Console.WriteLine("[3] Register New Hotel Guest");
    Console.WriteLine("[4] Check in Guest");
    Console.WriteLine("[5] Display Guest Stay Details");
    Console.WriteLine("[6] Extend Number of Stays");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("-----------------------------");
    Console.WriteLine("");
    Console.Write("Enter your option: ");
    int option = Convert.ToInt32(Console.ReadLine());
    return option;
}

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
    Console.WriteLine("");
    Console.WriteLine("Hotel Guests: ");
    Console.WriteLine("");
    foreach (Guest guest in guestList)
    {
        Console.WriteLine("Name: {0,-7}   Passport Number: {1,-9}   Membership Status: {2,-8}   Membership Points: {3,-5}   IsCheckedIn: {4,0}",
            guest.Name, guest.PassportNum, guest.Member.Status, guest.Member.Points, guest.IsCheckedIn);
    }
    Console.WriteLine("");
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

void DisplayStay(List<Guest> guestList)
{
    Console.WriteLine("Guest List:");
    foreach(Guest guest in guestList)
    {
        Console.WriteLine(guest.Name);
    }
    Console.WriteLine("");
    while(true)
    {
        try
        {
            Console.Write("Enter Guest Name: ");
            string? search = Console.ReadLine();
            if (SearchGuest(guestList, search) == null)
            {
                Console.WriteLine("Guest Not Found. Please Try Again!");
                Console.WriteLine("");
            }
            else
            {
                Guest foundguest = SearchGuest(guestList, search);
                Console.WriteLine();
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Invalid Input! Please Enter A Guest Name!");
        }
    }
}

Guest SearchGuestName(List<Guest> guestList, string search)
{
    foreach (Guest guest in guestList)
    {
        if (guest.Name == search)
        {
            return guest;
        }
    }
    return null;
}

bool SearchGuestPass(List<Guest> guestList, string search)
{
    foreach (Guest guest in guestList)
    {
        if (guest.PassportNum == search)
        {
            return true;
        }
    }
    return false;
}

void RegisterGuest(List<Guest> guestList)
{
    Console.Write("Enter Name: ");
    string? guestname = Console.ReadLine();
    Console.Write("Enter Passport Number: ");
    string? guestpass = Console.ReadLine();

}

CreateGuests(guestList);
CreateStay(stayList, guestList);
CreateRoom(roomList);
AddRoom(guestList, roomList);
DisplayGuests(guestList);

DisplayAvailRoom(roomList);


while (true)
{
    int option = DisplayMenu();
    if (option == 0)
    {
        Console.WriteLine("Thank you for using Hotel Management System 1.0!");
        break;
    }
    if (option == 1)
    {
        DisplayGuests(guestList);
    }
    if (option == 2)
    {
        DisplayAvailRoom(roomList);
    }
    if (option == 3)
    {

    }
}

