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

    private string _grantBasicAccess()
    {
        return "User has basic access.";
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
        throw new NotImplementedException();
    }

    public override int GetReputation()
    {
        throw new NotImplementedException();
    }
}

public class BannedBadge : Badges
{
    public BannedBadge(Client user) : base(user)
    {
    }

    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override int GetReputation()
    {
        throw new NotImplementedException();
    }
}

public class HundredPosts : Badges
{
    public HundredPosts(Client user) : base(user)
    {
    }

    public override void GetPriveleges()
    {
        throw new NotImplementedException();
    }

    public override int GetReputation()
    {
        throw new NotImplementedException();
    }
}