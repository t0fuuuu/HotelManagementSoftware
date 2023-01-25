//======================================
// Student Number : S102421111
// Student Name : Ryan Ma
//======================================


//METHODS//
using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();
List<Room> roomList = new List<Room>();
List<Stay> stayList = new List<Stay>();

void DisplayMenu()
{
    Console.WriteLine("===Hotel Management System===");
    Console.WriteLine("[1] Display Hotel Guests");
    Console.WriteLine("[2] Display Available Rooms");
    Console.WriteLine("[3] Register New Hotel Guest");
    Console.WriteLine("[4] Check in Guest");
    Console.WriteLine("[5] Display Guest Stay Details");
    Console.WriteLine("[6] Extend Number of Stays");
    Console.WriteLine("[7] Display Monthly Charges");
    Console.WriteLine("[8] Check out Guest");
    Console.WriteLine("[0] Exit");
    Console.WriteLine("=============================");
    Console.WriteLine();
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
                    Room temp = room;
                    if (each[j] == "")
                    {
                        break;
                    }
                    else if (temp.RoomNumber == Convert.ToInt32(each[j]))
                    {
                        foreach (Guest guest in guestList)
                        {
                            if (guest.PassportNum == each[1])
                            {
                                if (temp is StandardRoom)
                                {
                                    StandardRoom temp2 = (StandardRoom)temp;
                                    temp2.RequireWifi = Convert.ToBoolean(each[j + 1]);
                                    temp2.RequireBreakfast = Convert.ToBoolean(each[j + 2]);
                                    guest.HotelStay.AddRoom(temp);
                                }
                                else if (temp is DeluxeRoom)
                                {
                                    DeluxeRoom temp2 = (DeluxeRoom)temp;
                                    temp2.AdditionalBed = Convert.ToBoolean(each[j + 3]);
                                    guest.HotelStay.AddRoom(temp);
                                }
                                
                                if (guest.IsCheckedIn == false)
                                {
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
}

void CreateRoom(List<Room> roomList)
{
    string[] roomLine = File.ReadAllLines("Rooms.csv");
    for (int i = 1; i < roomLine.Length; i++)
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

//1
void DisplayGuests(List<Guest> guestList)
{
    Console.WriteLine();
    Console.WriteLine("Hotel Guests: ");
    Console.WriteLine();
    Console.WriteLine("{0,-10} {1,-18} {2,-19} {3,-20} {4,0}",
            "Name", "Passport Number", "Membership Status", "Membership Points", "IsCheckedIn");
    foreach (Guest guest in guestList)
    {
        Console.WriteLine("{0,-13} {1,-19} {2,-22} {3,-16} {4,0}",
            guest.Name, guest.PassportNum, guest.Member.Status, guest.Member.Points, guest.IsCheckedIn);
    }
    Console.WriteLine();
}

//2
void DisplayAvailRoom(List<Room> roomList)
{
    Console.WriteLine("{0,-12}{1,-17}{2,-23}{3}","Room Type", "Room Number", "Bed Configuration", "Daily Rate");
    foreach (Room room in roomList)
    {
        if (room.IsAvail == true)
        {
            if (room is StandardRoom)
            {
                Console.WriteLine("{0,-12}{1,-17}{2,-23}{3}", "Standard", room.RoomNumber, room.BedConfiguration, room.DailyRate);
            }
            else if (room is DeluxeRoom)
            {
                Console.WriteLine("{0,-12}{1,-17}{2,-23}{3}", "Deluxe", room.RoomNumber, room.BedConfiguration, room.DailyRate);
            }
        }
    }
}

void DisplayStay(List<Guest> guestList)
{
    Console.WriteLine();
    Console.WriteLine("Guest List:");
    Console.WriteLine();
    Console.WriteLine("{0,-10} {1,-18}","Name", "Passport Number");
    foreach (Guest guest in guestList)
    {
        Console.WriteLine("{0,-13} {1,-19}", guest.Name, guest.PassportNum);
    }
    Console.WriteLine();
    while (true)
    {
        try
        {
            Console.Write("Enter Guest Passport Number: ");
            string? searchpass = Console.ReadLine();
            if (SearchGuestPass(guestList, searchpass) == false)
            {
                Console.WriteLine("Guest Not Found. Please Try Again!");
                Console.WriteLine();
            }
            else
            {
                Guest foundguest = GetGuest(guestList, searchpass);
                Console.WriteLine();
                Console.WriteLine("======================Guest Details======================");
                Console.WriteLine("Name: {0,0}  Passport Number: {1,0}  IsCheckedIn: {2,0}", foundguest.Name, foundguest.PassportNum,foundguest.IsCheckedIn);
                Console.WriteLine();
                Console.WriteLine("=====================Stay Details=====================");
                Console.WriteLine("Check-In Date: {0,0}  Check-Out Date: {1,0}",foundguest.HotelStay.CheckInDate.ToString("dd/MM/yyyy"),foundguest.HotelStay.CheckOutDate.ToString("dd/MM/yyyy"));
                Console.WriteLine();
                Console.WriteLine("===========================Deluxe Room Details===========================");
                Console.WriteLine();
                Console.WriteLine("{0,-13} {1,-19} {2,-13} {3,-9} {4,-15}",
                    "Room Number","Bed Configuration","Daily Rate","IsAvail","Additional Bed");
                foreach (Room room in foundguest.HotelStay.RoomList)
                {
                    if (room is DeluxeRoom)
                    {
                        DeluxeRoom dr= (DeluxeRoom)room;
                        Console.WriteLine("    {0,-14} {1,-18} {2,-10} {3,-11} {4,0}", dr.RoomNumber, dr.BedConfiguration, dr.DailyRate, dr.IsAvail, dr.AdditionalBed);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("==================================Standard Room Details==================================");
                Console.WriteLine();
                Console.WriteLine("{0,-13} {1,-19} {2,-13} {3,-9} {4,-13} {5,-18}",
                    "Room Number", "Bed Configuration", "Daily Rate", "IsAvail", "RequireWifi", "RequireBreakfast");
                foreach (Room room in foundguest.HotelStay.RoomList)
                {
                    if (room is StandardRoom)
                    {
                        StandardRoom sr = (StandardRoom)room;
                        Console.WriteLine("    {0,-14} {1,-18} {2,-10} {3,-11} {4,-15} {5,-15}",
                            sr.RoomNumber, sr.BedConfiguration, sr.DailyRate, sr.IsAvail, sr.RequireWifi, sr.RequireBreakfast);
                    }
                }
                Console.WriteLine();

                break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input! Please Enter A Guest Name!");
        }
    }
}

Guest GetGuest(List<Guest> guestList, string search)
{
    foreach (Guest guest in guestList)
    {
        if (guest.PassportNum == search)
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

//3
void RegisterGuest(List<Guest> guestList)
{
    while (true)
    {
        try
        {
            Console.Write("Enter Name: ");
            string? guestname = Console.ReadLine();
            Console.Write("Enter Passport Number: ");
            string? guestpass = Console.ReadLine();
            bool checkfirst = Char.IsLetter(guestpass[0]);
            bool checklast = Char.IsLetter(guestpass[guestpass.Length - 1]);
            if (SearchGuestPass(guestList, guestpass) == true)
            {
                Console.WriteLine("The guest you have entered has already been registered!");
            }
            else if (guestpass.Length > 9 || guestpass.Length < 9)
            {
                Console.WriteLine("Invalid Passport Number Entered!");
            }
            else if (checkfirst == false || checklast == false)
            {
                Console.WriteLine("Invalid Passport Number Entered!");
            }
            else
            {
                Membership newmember = new Membership("Ordinary", 0);
                Guest newguest = new Guest(guestname, guestpass, null, newmember);
                newguest.IsCheckedIn = false;
                guestList.Add(newguest);
                string data = guestname + "," + guestpass + "," + newguest.Member.Status + "," + Convert.ToString(newguest.Member.Points);
                using (StreamWriter sw = new StreamWriter("Guests.csv", true))
                {
                    sw.WriteLine(data);
                }
                Console.WriteLine();
                Console.WriteLine("Guest Registered!");
                Console.WriteLine("Name: {0,-9} Passport Number: {1,-9}  Membership Status: {2,-9}  Membership Points: {3,-5}  IsCheckedIn: {4,0}",
                    newguest.Name, newguest.PassportNum, newguest.Member.Status, newguest.Member.Points, newguest.IsCheckedIn);
                Console.WriteLine();
                break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid Input! Please Enter Words!");
        }
    }
}

void CheckInRooms(Stay stay)
{
    while (true)
    {
        try
        {
            Console.Write("Enter Room Number: "); //uses room number to select room
            int roomNum = Convert.ToInt32(Console.ReadLine());
            Room roomChosen = null;
            bool check2 = false;
            foreach (Room room in roomList)
            {
                if (room.IsAvail == true && room.RoomNumber == roomNum)
                {
                    roomChosen = room;
                    check2 = true;
                    break;
                }

            }
            if (check2 == false)
            {
                Console.WriteLine("Room Number Not Available/ Does Not Exist!");
                continue;
            }
            if (roomChosen is StandardRoom)
            {
                StandardRoom roomChosen2 = (StandardRoom)roomChosen;
                string wifi = null;
                string breakfast = null;
                while (true)
                {
                    Console.Write("Is Wifi Required [Y/N]: ");
                    wifi = Console.ReadLine();
                    if (wifi == "Y" || wifi == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter A Valid Option!");
                    }
                }
                while (true)
                {
                    Console.Write("Is Breakfast Required [Y/N]: ");
                    breakfast = Console.ReadLine();
                    if (breakfast == "Y" || breakfast == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter A Valid Option!");
                    }
                }
                if (wifi == "Y")
                {
                    roomChosen2.RequireWifi = true;
                }
                else
                {
                    roomChosen2.RequireWifi = false;
                }
                if (breakfast == "Y")
                {
                    roomChosen2.RequireBreakfast = true;
                }
                else
                {
                    roomChosen2.RequireBreakfast = false;
                }
                roomChosen2.IsAvail = false;
                stay.RoomList.Add(roomChosen2);

            }
            else if (roomChosen is DeluxeRoom)
            {
                DeluxeRoom roomChosen2 = (DeluxeRoom)roomChosen;
                string bed = null;
                while (true)
                {
                    Console.Write("Is Additional Bed Required [Y/N]: ");
                    bed = Console.ReadLine();
                    if (bed == "Y" || bed == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please Enter A Valid Option!");
                    }
                }
                if (bed == "Y")
                {
                    roomChosen2.AdditionalBed = true;
                }
                else
                {
                    roomChosen2.AdditionalBed = false;
                }
                roomChosen2.IsAvail = false;
                stay.RoomList.Add(roomChosen2);
            }
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please Enter A Valid Option!");
            continue;
        }
    }
}

//4
void CheckInGuest(List<Guest> guestlist, List<Stay> stayList, List<Room> roomList)
{
    DisplayGuests(guestlist);
    Console.WriteLine();
    Guest guestChosen = null;
    while (true)
    {
        Console.Write("Enter Passport Number: "); //retrieve guests based on their passport num
        string ppnum = Console.ReadLine();
        bool check = false;
        foreach (Guest guest in guestlist)
        {
            if (guest.PassportNum == ppnum)
            {
                guestChosen = guest;
                check = true;
                break;
            }
        }
        if (check == false)
        {
            Console.WriteLine("Guest Not Found. Please Try Again!");
            continue;
        }
        else if (guestChosen.IsCheckedIn == true)
        {
            Console.WriteLine("Guest is Already Checked In. You Can Extend Your Stay Instead!");
            continue;
        }
        else
        {
            break;
        }
    }
    Stay stay = null;
    while (true)
    {
        try
        {
            Console.Write("Enter Check In Date: ");
            DateTime checkIn = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter Check Out Date: ");
            DateTime checkOut = Convert.ToDateTime(Console.ReadLine());
            stay = new Stay(checkIn, checkOut);
            break;
        }
        catch (FormatException)
        {
            Console.WriteLine("Enter A Valid Date!");
        }
    }
    Console.WriteLine();

    DisplayAvailRoom(roomList);
    bool choosingRoom = true;
    do
    {
        CheckInRooms(stay);
        string ans = null;
        while (true)
        {
            Console.Write("Do You Want To Select Another Room [Y/N]: ");
            ans = Console.ReadLine();
            if (ans == "Y" || ans == "N")
            {
                break;
            }
            else
            {
                Console.WriteLine("Please Enter A Valid Option!");
                continue;
            }
        }
        if (ans == "N")
        {
            choosingRoom = false;
        }
    }
    while (choosingRoom);
    guestChosen.HotelStay = stay;
    guestChosen.IsCheckedIn = true;
    Console.WriteLine("Check In Succesful\nEnjoy Your Stay!");    
}

void CheckOutGuest(List<Guest> guestList)
{
    Console.WriteLine();
    Console.WriteLine("{0,-10} {1,-18} {2,-19}",
            "Name", "Passport Number","IsCheckedIn");
    foreach (Guest guest in guestList)
    {
        Console.WriteLine("{0,-13} {1,-19} {2,-22}",
            guest.Name, guest.PassportNum, guest.IsCheckedIn);
    }
    Console.WriteLine("");
    Console.Write("Enter Passport Number: ");
    string? guestpass = Console.ReadLine();
    bool results = SearchGuestPass(guestList, guestpass);
    if (results == false)
    {
        Console.WriteLine("Guest Not Found. Please Try Again!");
        Console.WriteLine();
    }
    else if (results == true)
    {
        Guest foundguest = GetGuest(guestList, guestpass);
        if (foundguest.IsCheckedIn == false)
        {
            Console.WriteLine("Guest has already checked out!");
        }
        else
        {
            double amt = foundguest.HotelStay.CalculateTotal() * 1.0;
            Console.WriteLine("Bill Amount: ${0,0}", amt);
            Console.WriteLine();
            Console.WriteLine("Membership Status: {0,0}  Membership Points: {1,0}",foundguest.Member.Status,foundguest.Member.Points);
            if (foundguest.Member.Status == "Ordinary" || foundguest.Member.Points < 100)
            {
                Console.WriteLine();
                Console.WriteLine("Ordinary Member. Unable to Redeem");
                Console.WriteLine();
                Console.WriteLine("Final Bill Amount: {0,0}", amt);
                Console.Write("Press Anywhere To Pay: ");
                Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Thank you for making payment!");
                Console.WriteLine();
                foundguest.Member.EarnPoints(amt);
                foundguest.IsCheckedIn = false;
            }
            else
            {
                Console.Write("Points to redeem: ");
                int points = Convert.ToInt32(Console.ReadLine());
                bool check = foundguest.Member.RedeemPoints(points);
                if (check == true)
                {
                    Console.WriteLine("Final Bill Amount: ${0,0}", amt - points);
                    Console.Write("Press Anywhere To Pay: ");
                    Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Thank you for making payment!");
                    Console.WriteLine();
                    foundguest.Member.EarnPoints(amt - points);
                    foundguest.IsCheckedIn = false;
                }
                else
                {
                    Console.WriteLine("Insufficient Points to redeem.");
                }

            }

        }
    }
}




//MAIN PROGRAM//

CreateGuests(guestList);
CreateStay(stayList, guestList);
CreateRoom(roomList);
AddRoom(guestList, roomList);


while (true)
{
    DisplayMenu();
    Console.WriteLine();
    try
    {
        Console.Write("Enter your option: ");
        int option = Convert.ToInt32(Console.ReadLine());
        if (option == 0)
        {
            Console.WriteLine("Thank you for using Hotel Management System! Have a Nice Day!");
            break;
        }
        else if (option == 1)
        {
            DisplayGuests(guestList);
            break;
        }
        else if (option == 2)
        {
            DisplayAvailRoom(roomList);
            break;
        }
        else if (option == 3)
        {
            RegisterGuest(guestList);
            break;
        }
        else if (option == 4)
        {
            CheckInGuest(guestList, stayList, roomList);
            break;
        }
        else if (option == 5)
        {
            DisplayStay(guestList);
            break;
        }
        else if (option == 6)
        {
            break;
        }
        else if (option == 7)
        {

        }
        else if (option == 8)
        {
            CheckOutGuest(guestList);
            break;
        }
        else
        {
            Console.WriteLine("Please Enter A Valid Option!");
        }
        Console.WriteLine();
    }
    catch (FormatException)
    {
        Console.WriteLine("Please Enter A Valid Option! (0 to 6)");
    }
}



