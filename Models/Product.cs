using System.ComponentModel.DataAnnotations;

namespace ShirtCompany.Models;

public class Product {

      public int ProductID {get; set;}

      public string? Name {get; set;}    

      public string? Category {get; set;}
      public string? Brand {get; set;}

      public string? Description {get; set;}
        
      public string? Color{get; set;}
        
      public int? Size {get; set;}

      public decimal? Price {get; set;}

      public string? Gender{get; set;}
      [Required]

      public string? Image {get; set;}

      public DateTime CreatedDate { get; set; }

      public DateTime UpdatedDate { get; set; }

    // Method to update the UpdatedDate (can be called whenever the shirt is modified)
      public void Update()
      {
            UpdatedDate = DateTime.Now;
      } 
           
}