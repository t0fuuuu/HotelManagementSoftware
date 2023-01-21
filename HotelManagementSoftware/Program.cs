using HotelManagementSoftware;

List<Guest> guestList = new List<Guest>();

void CreateMembership(List<Membership> membershipList)
{
    string[] lines = File.ReadAllLines("Guest.csv");
    for(int i = 0; i < lines.Length; i++)
    {
        string[] each = lines[i].Split(',');
        Membership member = new Membership(each[2], Convert.ToInt32(each[3]));
        membershipList.Add(member);
    }
}
void CreateGuests(List<Guest> guestList, List<Membership> membershipList)
{
    string[] lines = File.ReadAllLines("Guest.csv");
    for (int i = 0; i < lines.Length; i++)
    {
        string[] each = lines[i].Split(',');
        Guest guest = new Guest(each[0], each[1], membershipList[i],)
    }
}


