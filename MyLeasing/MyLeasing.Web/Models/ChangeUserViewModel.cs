using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Document { get; set; }

        public string Address { get; set; }


    }
}
