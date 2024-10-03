using System.ComponentModel.DataAnnotations;
using ShirtCompany.Filters.ActionFilters;

namespace ShirtCompany.Models;

public class Shirt {

        public int ShirtID {get; set;}
        
        [Required]      

        public string? Brand {get; set;}
        
        [Required]
        public string? Color{get; set;}
        
        [Shirt_EnsureCorrectSizing]
        public int? Size {get; set;}

        [Required]
        public decimal? Price {get; set;}

        public string? Gender{get; set;}
      
       // public string? Created_Date {get; set;}
       
      //  public string? Updated_Date {get; set;}
           
}