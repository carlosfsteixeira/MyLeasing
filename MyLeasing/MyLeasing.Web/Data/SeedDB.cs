﻿using Microsoft.AspNetCore.Identity;
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
        List<Lessee> lessee;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
            owners = new List<Owner>();
            lessee = new List<Lessee>();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Lessee");

            //cria os 10 owners
            if (!_context.Owners.Any())
            {
                // atribuir admin
                User userAdmin = new User
                {
                    Document = _random.Next(100000000, 999999999).ToString(),
                    FirstName = "Carlos",
                    LastName = "Teixeira",
                    Email = "carlost2410@gmail.com",
                    UserName = "carlost2410@gmail.com",
                    PhoneNumber = _random.Next(100000000, 999999999).ToString(),
                    Address = "Rua da Morada Qualquer " + _random.Next(99).ToString(),
                };

                _context.Users.Add(userAdmin);

                var addUser = await _userHelper.AddUserAsync(userAdmin, "123456");

                if (addUser != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in seeder");
                }

                //atribuir role de Admin ao user Carlos Teixeira
                var isInRole = await _userHelper.IsUserInRoleAsync(userAdmin, "Admin");

                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(userAdmin, "Admin");
                }

                var tupleListOwners = new (string FirstName, string LastName)[]
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
                    Owner ownerCreated = AddOwner(tupleListOwners[i].FirstName, tupleListOwners[i].LastName);

                    var result = await _userHelper.AddUserAsync(ownerCreated.User, "123456");

                    await _userHelper.AddUserToRoleAsync(ownerCreated.User, "Owner");

                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not create user in seeder");
                    }
                }
            }

            if (!_context.Lessee.Any())
            {
                var tupleListLessee = new (string FirstName, string LastName)[]
                {
                  ("Casemiro", "Enchada"),
                  ("Rosa", "Cachucha"),
                  ("Henriqueta", "Bartolomeu"),
                  ("Evangelista", "Saraiva"),
                  ("Ildefonso", "Sardinha"),
                 };

                for (int i = 0; i < 5; i++)
                {
                    Lessee lesseeCreated = AddLessee(tupleListLessee[i].FirstName, tupleListLessee[i].LastName);

                    var result = await _userHelper.AddUserAsync(lesseeCreated.User, "123456");

                    await _userHelper.AddUserToRoleAsync(lesseeCreated.User, "Lessee");

                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not create user in seeder");
                    }
                }
                
                await _context.SaveChangesAsync();
            }
        }

        private Lessee AddLessee(string firstName, string lastName)
        {
            Lessee lessee = new Lessee
            {
                Document = _random.Next(100000000, 999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(20000000, 299999999),
                CellPhone = _random.Next(90000000, 999999999),
                Adress = "Rua do Inquilino " + _random.Next(200).ToString(),
                User = CreateUser(firstName, lastName),
                //ImageId = $"~/images/lessee/lessee.png",
            };

            _context.Lessee.Add(lessee);

            return lessee;
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
                //ImageId = $"~/images/owners/user.png",
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
