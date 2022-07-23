using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private Random _random;
        private List<Owner> listOwners;

        public SeedDB(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _random = new Random();
            listOwners = new List<Owner>();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            //cria os 10 owners
            if (!_context.Owners.Any())
            {
                AddOwner("Manuel", "Matias");
                AddOwner("Blimunda", "Saramago");
                AddOwner("José", "Carneiro");
                AddOwner("Olivia", "Sousa");
                AddOwner("António", "Corrida");
                AddOwner("Filipe", "Estanislau");
                AddOwner("Gregório", "Tomás");
                AddOwner("Pedro", "Portas");
                AddOwner("Bruno", "Uva");
                AddOwner("Mateus", "Rosa");
                await _context.SaveChangesAsync();
            }

            //atribuir os owners a uma lista para percorrer e atribuir password a cada user de cada owner
            listOwners = _context.Owners.ToList();

            foreach (Owner owner in listOwners)
            {
                var result = await _userManager.AddPasswordAsync(owner.User, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in seeder");
                }
                    
            }
        }


        private void AddOwner(string firstName, string lastName)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(100000000, 999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(20000000, 299999999),
                CellPhone = _random.Next(90000000, 999999999),
                Adress = "Rua da Morada Qualquer " + _random.Next(99).ToString(),
                User = CreateUser(firstName, lastName)
            });

           // var result = await _userManager.AddPasswordAsync(owner.User, "123456");

        }

        private  User CreateUser(string firstName, string lastName)
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

            return user;
        }
    }
}
