

using System.ComponentModel.DataAnnotations;

namespace ShirtCompany.Models
{
    public class UserModel
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Please enter a Name")]
        public required string Name {get; set;}
        [Required(ErrorMessage = "Please enter a Username")]
        public required string Username {get; set;}
        [Required(ErrorMessage = "ex. johndoe@gmail.com")]
        public required string Email {get; set;}
    
    }
}