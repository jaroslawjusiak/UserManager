using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.Model;

namespace UserManager.Data
{
    public class DbContext
    {
        public List<User> Users { get; set; }
    }
}
