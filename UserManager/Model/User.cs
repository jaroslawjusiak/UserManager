﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.Enums;

namespace UserManager.Model
{
    public class User
    {
        public Guid Uid { get; set; }
        public Gender Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
