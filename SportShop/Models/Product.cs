using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShop.Models{

    public class Product{
        public long ProductID{get;set;}
        [Required]
        [MaxLength(100)]
        public string Name{get;set;}  

        [Required]      
        [Column(TypeName="decimal(8, 2)")]
        public decimal Price{get;set;}

        [Required]
        [MaxLength(150)]
        public string Category{get;set;}
        public string Description{get;set;}
    }
}