Client firstUser = new User();
firstUser = new CommunityBadge(firstUser);
firstUser = new CommunityBadge(firstUser);

Console.WriteLine(firstUser.GetReputation());
firstUser.GetPriveleges();

firstUser = new HundredPosts(firstUser);
Console.WriteLine(firstUser.GetReputation());

firstUser = new BannedBadge(firstUser);
Console.WriteLine(firstUser.GetReputation());

public abstract class Client
{
    public abstract int GetReputation();
    public abstract void GetPriveleges();
} 

public class User : Client
{
    private int _reputation;

    public override int GetReputation()
    {
        return _reputation;
    }

    public override void GetPriveleges()
    {
        _grantBasicAccess();
    }

    private void _grantBasicAccess()
    {
        Console.WriteLine("User has basic access.");
    }
}


public abstract class Badges : Client
{
    protected Client _user;

    public Badges(Client user)
    {
        _user = user;
    }
}

public class CommunityBadge : Badges
{
    public CommunityBadge(Client user) : base(user)
    {
    }

    public override void GetPriveleges()
    {
        _grantGroupAccess();
        _user.GetPriveleges();
    }

    public override int GetReputation()
    {
        return _user.GetReputation() + 5;
    }

    private void _grantGroupAccess()
    {
        Console.Write("Has Group Access. ");
    }
}

public class BannedBadge : Badges
{
    public BannedBadge(Client user) : base(user)
    {
    }

    public override void GetPriveleges()
    {
        _blockAccess();
    }

    public override int GetReputation()
    {
        return _user.GetReputation() * 0;
    }

    private void _blockAccess()
    {
        Console.WriteLine("User access restricted.");
    }
}

public class HundredPosts : Badges
{
    public HundredPosts(Client user) : base(user)
    {
    }

    public override void GetPriveleges()
    {
        _user.GetPriveleges();
    }

    public override int GetReputation()
    {
        return _user.GetReputation() + 100;
    }
}