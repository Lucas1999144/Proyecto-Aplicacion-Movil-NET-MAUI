using System;
using System.Collections.Generic;
using System.Text;

namespace Parcial_Moviles.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty; 
        public string Category { get; set; } = string.Empty;    
        public string Image { get; set; } = string.Empty;   
    }
}
