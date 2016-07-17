using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryApi.Models
{
    public class BookItem
    {
        //[Key]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public int Year { get; set; }
        public int NumberOfPages { get; set; }

        // Navigation property
        public /*virtual*/ AuthorItem Author { get; set; }
    }
}
