﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Enitites;
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductImageUrl { get; set; }
    public string ProductDescription { get; set; }
    public string CategoryId { get; set; }
    [BsonIgnore]
    public Category Category { get; set; }
    [BsonIgnore]
    public ProductDetail ProductDetail { get; set; }
    [BsonIgnore]
    public List<ProductImage> ProductImages { get; set; }
}
