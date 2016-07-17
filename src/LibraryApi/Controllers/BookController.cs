using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using LibraryApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        [FromServices]
        public IDataRepository<BookItem> BookItems { get; set; }

        [HttpGet]
        public IEnumerable<BookItem> GetAll()
        {
            return BookItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetById(int id)
        {
            var item = BookItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return HttpBadRequest();
            }
            BookItems.Add(item);
            return CreatedAtRoute("GetBook", new { controller = "Book", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BookItem item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return HttpBadRequest();
            }

            var book = BookItems.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            BookItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            BookItems.Remove(id);
        }

    }
}
