using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }

        public int Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int FixedPhone { get; set; }

        public int CellPhone { get; set; }

        public string Adress { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
