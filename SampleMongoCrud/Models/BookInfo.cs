using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SampleMongoCrud.Models
{
    public class BookInfo
    {
        [BsonElement("Name")]
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}