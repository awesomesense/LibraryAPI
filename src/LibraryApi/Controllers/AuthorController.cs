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

        private const string _messageNotFound = "Author not found";
        private const string _messageInvalidObject = "Invalid object of Author";

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
                //return HttpNotFound();
                return new HttpNotFoundObjectResult(new { Message = _messageNotFound });
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                //return HttpBadRequest();
                return new BadRequestObjectResult(_messageInvalidObject);
            }
            AuthorItems.Add(item);
            return CreatedAtRoute("GetAuthor", new { controller = "Author", id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AuthorItem item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                //return HttpBadRequest();
                return new BadRequestObjectResult(_messageInvalidObject);
            }

            var author = AuthorItems.Find(id);
            if (author == null)
            {
                //return HttpNotFound();
                return new HttpNotFoundObjectResult(new { Message = _messageNotFound });
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
