using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestApiExercise.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("SSN")]
        public string SSN { get; set; }
    }
}
