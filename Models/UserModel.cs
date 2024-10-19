using System.ComponentModel.DataAnnotations;

namespace ShirtCompany.Models
{
    public class UserModel
    {
        [Key] 
        public int UserId { get; set; }  

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }  = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }  = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Postal Code is required")]
        public string PostalCode { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }  = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }  = string.Empty;
    }
}
