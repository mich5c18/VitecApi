using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VitecApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        //RegularExpression should make it so that you can only write numbers whithout whitespace
        [RegularExpression(@"^[0-9""'\s-]*$")]
        [DataType(DataType.Currency)]
        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
