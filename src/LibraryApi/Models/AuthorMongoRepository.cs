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
    public class AuthorMongoRepository : IDataRepository<AuthorItem>
    {
		private readonly AppMongoSettings _settings;
		private readonly MongoDatabase _db;

        public AuthorMongoRepository(IOptions<AppMongoSettings> settings)
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
		
        public MongoCollection<AuthorItem> Authors
        {
            get
            {
                return _db.GetCollection<AuthorItem>("Authors");
            }
        }

        public IEnumerable<AuthorItem> GetAll()
        {
            return Authors.FindAll();
        }

        public void Add(AuthorItem item)
        {
            int id = 0;
            if (Authors.Count() > 0)
            {
                id = Authors.FindAll().Max(c => c.Id);
            }
            item.Id = ++id;
            Authors.Insert(item);
        }

        public AuthorItem Find(int id)
        {
            return Authors.FindOneById(id);
        }

        public AuthorItem Remove(int id)
        {
            var item = Find(id);

            var query = Query<AuthorItem>.EQ(e => e.Id, id);
            if (query != null)
            {
                Authors.Remove(query);
                return item;
            }
            
            return null;
        }

        public void Update(AuthorItem item)
        {
            Authors.Save(item);
        }
    }
}
