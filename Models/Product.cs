using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;

namespace Parcial_Moviles.Models
{
    [Table ("Products")]
    public class Product
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty; 
        public string Category { get; set; } = string.Empty;    
        public string Image { get; set; } = string.Empty;   
    }
}
