using System.ComponentModel.DataAnnotations;
using ShirtCompany.Filters.ActionFilters;

namespace ShirtCompany.Models;

public class Shirt {
      [Key]
      public int ProductID {get; set;}
        
      [Required]

      public string? Name {get; set;}    

      public string? Brand {get; set;}

      public string? Description {get; set;}
        
      [Required]
      public string? Color{get; set;}
        
      [Shirt_EnsureCorrectSizing]
      public int? Size {get; set;}

      [Required]
      public decimal? Price {get; set;}

      public string? Gender{get; set;}

      public string? Category {get; set;}

      public DateTime CreatedDate { get; set; }

      // Property to track when the shirt was last updated
      public DateTime UpdatedDate { get; set; }
      public Shirt()
      {
        CreatedDate = DateTime.Now;  // Sets the creation time when an object is created
        UpdatedDate = DateTime.Now;  // Initially, UpdatedDate is the same as CreatedDate
      }

    // Method to update the UpdatedDate (can be called whenever the shirt is modified)
      public void Update()
      {
            UpdatedDate = DateTime.Now;
      } 
           
}