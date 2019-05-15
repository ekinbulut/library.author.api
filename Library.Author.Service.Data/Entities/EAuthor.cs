using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Author.Service.Data.Entities
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class EAuthor
    {
        public EAuthor()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId] public string Id { get; set; }

        public string Bio { get; set; }
        public string Name { get; set; }
        public string[] Books { get; set; }
        public DateTime? Dead { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public DateTime Birthday { get; set; }
    }
}