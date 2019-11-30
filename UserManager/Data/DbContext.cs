using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.Model;

namespace UserManager.Data
{
    public class DbContext
    {
        private List<User> _users;

        public DbContext()
        {
            _users = new List<User>();
        }
        public List<User> Users
        {
            get { return _users; }
        }
    }
}
