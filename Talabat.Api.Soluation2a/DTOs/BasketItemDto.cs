﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.Api.Soluation2a.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price Must Be Greater than zero")]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must Be at least one Item")]
        public int Quantity { get; set; }
    }
}