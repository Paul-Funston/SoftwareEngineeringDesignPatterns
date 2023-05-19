using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStack
{
    public abstract class Client
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public bool accessDisabled { get; set; }
        public IAccessHandler AccessHandler { get; set; }

        public virtual bool HandleAccess()
        {
            return AccessHandler.GetAccess(default ,accessDisabled);
        }
       
    }

    public class User : Client
    {
        public int Reputation { get; set; } = 0;
        public User()
        {
            AccessHandler = new HasReputation();
        }

        public override bool HandleAccess()
        {
            return AccessHandler.GetAccess(Reputation, accessDisabled);
        }

    }

    public class Manager : Client
    {
        public Manager()
        {
            AccessHandler = new HasAccessAutomatic();
        }

    }

    public class Admin : Client
    {
        public Admin()
        {
            AccessHandler = new HasAccessAutomatic();
        }

    }

    public interface IAccessHandler
    {
        public bool GetAccess(int? reputation = 0, bool accessDisabled = false);
    }

    public class HasReputation : IAccessHandler
    {
        public bool GetAccess(int? reputation = 0, bool accessDisabled = false)
        {
            if (accessDisabled)
            {
                return false;
            }

            if(reputation > 20)
            {
                return true;
            }

            return false;

        }
    } 

    public class HasAccessAutomatic : IAccessHandler
    {
        public bool GetAccess(int? reputation, bool accessDisabled = false)
        {
            if (accessDisabled)
            {
                return false;
            }
            return true;
        }
    }
}
