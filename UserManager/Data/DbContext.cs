﻿using Bogus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.Enums;
using UserManager.Model;

namespace UserManager.Data
{
    public class DbContext
    {
        private List<User> _users;
        private readonly IConfiguration _config;

        public DbContext(IConfiguration configuration)
        {
            _config = configuration;
            _users = new List<User>();
            SeedUsers();
        }
        public List<User> Users
        {
            get { return _users; }
        }

        #region Seed

        private void SeedUsers()
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u. Uid, (f, u)=> f.Random.Guid())
                .RuleFor(u => u.Gender, (f, u) => (Gender)f.Person.Gender)
                .RuleFor(u => u.FirstName, (f, u) => f.Person.FirstName)
                .RuleFor(u => u.LastName, (f, u) => f.Person.LastName)
                .RuleFor(u => u.Email, (f, u) => f.Person.Email)
                .RuleFor(u => u.Login, (f, u) => f.Person.UserName)
                .RuleFor(u => u.Password, (f, u) => f.Internet.Password(f.Random.Int(8, 16), false));

            var usersCount = _config.GetSection("InitialUsers").Get<int>();
            _users.AddRange(userFaker.Generate(usersCount));
        }

        #endregion
    }
}
