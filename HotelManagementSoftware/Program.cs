using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();
List<Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();

int DisplayMenu()
{
    Console.WriteLine("---Hotel Management System---");
    Console.WriteLine("[1] Display Hotel Guests");
    Console.WriteLine("[2] Display Available Rooms");
    Console.WriteLine("[3] Display Guest Stay Details");
    Console.WriteLine("[4] Register New Hotel Guest");
    Console.WriteLine("[5] Check in Guest");
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
                g.IsCheckedIn = Convert.ToBoolean(each[2]);
                break;
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

void DisplayAvailRoom(List<Room> roomList)
{
    foreach (Room room in roomList)
    {
        if (room.IsAvail == true)
        {
            Console.WriteLine("{0}{1}{2}",room.RoomNumber, room.BedConfiguration, room.DailyRate);
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

Guest SearchGuest(List<Guest> guestList, string search)
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

CreateGuests(guestList);
CreateStay(stayList, guestList);
DisplayGuests(guestList);
CreateRoom(roomList, guestList);
DisplayAvailRoom(roomList);

while(true)
{
    int option = DisplayMenu();
    if (option == 0)
    {
        Console.WriteLine("Thank you for using Hotel Management System 1.0!");
        break;
    }
}

