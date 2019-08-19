﻿namespace Core.Model
{
    using System.ComponentModel.DataAnnotations;

    public class Share
    {
        public int ID { get; set; }
        [MaxLength(128)]
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Trader Owner { get; set; }

        public override string ToString() => $"{ID}. {Name},    {Price}$,    {Quantity}";
    }
}