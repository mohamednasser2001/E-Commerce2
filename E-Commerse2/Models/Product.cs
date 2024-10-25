﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace E_Commerse2.Models
{
    public class Product
    {
       
            public int Id { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(255)]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            [Range(0,1000)]
            public decimal Price { get; set; }
             [ValidateNever]
            public string ImgUrl { get; set; }
            [Range(0,5)]
            public double Rate { get; set; }

            public int CategoryId { get; set; }
            [ValidateNever]
            public Category Category { get; set; }
             [ValidateNever]
            public Company Company { get; set; }
  
            public int? CompanyId { get; set; }
          
    }
}
