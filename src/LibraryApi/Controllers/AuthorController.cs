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
    public class AuthorController : Controller
    {
        [FromServices]
        public IDataRepository<AuthorItem> AuthorItems { get; set; }
        
        [HttpGet]
        public IEnumerable<AuthorItem> GetAll()
        {
            return AuthorItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetById(int id)
        {
            var item = AuthorItems.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return HttpBadRequest();
            }
            AuthorItems.Add(item);
            return CreatedAtRoute("GetAuthor", new { controller = "Author", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AuthorItem item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return HttpBadRequest();
            }

            var author = AuthorItems.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            AuthorItems.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var author = AuthorItems.Find(id);
            if (author != null)
            {
                AuthorItems.Remove(id);
            }
        }

    }
}
