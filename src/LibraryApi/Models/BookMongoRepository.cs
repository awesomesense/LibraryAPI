using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;

namespace LibraryApi.Models
{
    public class BookMongoRepository : IDataRepository<BookItem>
    {
		private readonly AppMongoSettings _settings;
		private readonly MongoDatabase _db;

        public BookMongoRepository(IOptions<AppMongoSettings> settings)
        {
			_settings = settings.Value;
			_db = Connect();
        }

		private MongoDatabase Connect()
		{
			MongoClient client = new MongoClient(_settings.MongoConnection);
			MongoServer server = client.GetServer();
			MongoDatabase db = server.GetDatabase(_settings.Database);

			return db;
		}
		
        public MongoCollection<BookItem> Books
        {
            get
            {
                return _db.GetCollection<BookItem>("Books");
            }
        }

        public IEnumerable<BookItem> GetAll()
        {
            return Books.FindAll();
        }

        public void Add(BookItem item)
        {
            int id = 0;
            if (Books.Count() > 0)
            {
                id = Books.FindAll().Max(c => c.Id);
            }
            item.Id = ++id;
            Books.Insert(item);
        }

        public BookItem Find(int id)
        {
            return Books.FindOneById(id);
        }

        public BookItem Remove(int id)
        {
            var item = Find(id);

            var query = Query<BookItem>.EQ(e => e.Id, id);
            if (query != null)
            {
                Books.Remove(query);
                return item;
            }
            
            return null;
        }

        public void Update(BookItem item)
        {
            Books.Save(item);
        }
    }
}
