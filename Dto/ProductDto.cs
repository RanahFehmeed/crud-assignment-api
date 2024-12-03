﻿namespace ProductManagementAPI.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<string> TagNames { get; set; }
    }

}