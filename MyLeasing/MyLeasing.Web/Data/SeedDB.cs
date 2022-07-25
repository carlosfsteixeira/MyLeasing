using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private Random _random;
        private IUserHelper _userHelper;
        List<Owner> owners;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
            owners = new List<Owner>();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            //cria os 10 owners
            if (!_context.Owners.Any())
            {
                var tupleListNames = new (string FirstName, string LastName)[]
                {
                  ("Manuel", "Matias"),
                  ("Blimunda", "Saramago"),
                  ("Jose", "Carneiro"),
                  ("Olivia", "Sousa"),
                  ("António", "Corrida"),
                  ("Filipe", "Estanislau"),
                  ("Gregório", "Tomás"),
                  ("Pedro", "Portas"),
                  ("Bruno", "Uva"),
                  ("Mateus", "Rosa")
                 };

                for (int i = 0; i < 10; i++)
                {
                    Owner ownerCreated = AddOwner(tupleListNames[i].FirstName, tupleListNames[i].LastName);

                    var result = await _userHelper.AddUserAsync(ownerCreated.User, "123456");

                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not create user in seeder");
                    }

                }

                await _context.SaveChangesAsync();
            }


        }


        private Owner AddOwner(string firstName, string lastName)
        {
            Owner owner = new Owner
            {
                Document = _random.Next(100000000, 999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(20000000, 299999999),
                CellPhone = _random.Next(90000000, 999999999),
                Adress = "Rua da Morada Qualquer " + _random.Next(99).ToString(),
                User = CreateUser(firstName, lastName),
                Image = $"~/images/owners/user.png",
        };

            _context.Owners.Add(owner);

            return owner;

        }

        private User CreateUser(string firstName, string lastName)
        {
            var username = "user" + _random.Next(1000, 9999).ToString() + "@gmail.com";

            User user = new User
            {
                Document = _random.Next(100000000, 999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                Email = username,
                UserName = username,
                PhoneNumber = _random.Next(100000000, 999999999).ToString(),
                Address = "Rua da Morada Qualquer " + _random.Next(99).ToString(),
            };

            _context.Users.Add(user);

            return user;
        }
    }
}
