using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class AuthorSqlRepository : IDataRepository<AuthorItem>
    {
        private readonly AppDbContext _db;

        public AuthorSqlRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<AuthorItem> GetAll()
        {
            return _db.Authors.ToList();
            //return _db.Authors.AsEnumerable();
        }

        public void Add(AuthorItem item)
        {
            _db.Authors.Add(item);
            _db.SaveChanges();
        }

        public AuthorItem Find(int id)
        {
            return _db.Authors.FirstOrDefault(c => c.Id == id);
        }

        public AuthorItem Remove(int id)
        {
            var item = _db.Authors.FirstOrDefault(c => c.Id == id);
            _db.Authors.Remove(item);
            _db.SaveChanges();
            return item;
        }

        public void Update(AuthorItem item)
        {
            _db.Authors.Update(item);
            _db.SaveChanges();
        }
    }
}
