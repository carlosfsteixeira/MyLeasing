using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int FixedPhone { get; set; }

        [Required]
        public int CellPhone { get; set; }

        public string Adress { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public User User { get; set; }

        public string Image { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return null;
                }
                else
                {
                    return $"https://localhost:44323{Image.Substring(1)}";
                }
            }
        }
    }
}
