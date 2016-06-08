using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models
{
    public class BookSqlRepository : IDataRepository<BookItem>
    {
        private readonly AppDbContext _db;

        public BookSqlRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<BookItem> GetAll()
        {
            return _db.Books.ToList();
        }

        public void Add(BookItem item)
        {
            _db.Books.Add(item);
            _db.SaveChanges();
        }

        public BookItem Find(int id)
        {
            return _db.Books.FirstOrDefault(c => c.Id == id);
        }

        public BookItem Remove(int id)
        {
            var item = _db.Books.FirstOrDefault(c => c.Id == id);
            _db.Books.Remove(item);
            _db.SaveChanges();
            return item;
        }

        public void Update(BookItem item)
        {
            _db.Books.Update(item);
            _db.SaveChanges();
        }
    }
}
