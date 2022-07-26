using System;
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

        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty ? $"https://myleasingwebct.azurewebsites.net/images/lessee/lessee.png" :
            $"https://myleasingctstorage.blob.core.windows.net/lessee/{ImageId}";
    }
}
