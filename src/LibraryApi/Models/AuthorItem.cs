using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryApi.Models
{
    public class AuthorItem
    {
        //[Key]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
