using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private Random _random;

        public SeedDB(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

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
        }

        private void AddOwner(string firstName, string lastName)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(9999999),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(299999999),
                CellPhone = _random.Next(999999999),
                Adress = "Rua da Morada Qualquer " + _random.Next(99).ToString(),
            });
        }
    }
}
