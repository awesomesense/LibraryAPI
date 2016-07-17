using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;
using LibraryApi;
using LibraryApi.Controllers;
using LibraryApi.Models;
using Xunit;

namespace LibraryApi.Tests
{
    public class AuthorControllerTests
    {
        private AppDbContext _context;
        private AuthorController _controller;

        private int _JackLondonId;

        public AuthorControllerTests()
        {
            // Initialize DbContext
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Database=LibraryApiDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            _context = new AppDbContext(optionsBuilder.Options);

            // Sample Data
            // If there are no test data, let's insert
            var item = _context.Authors.FirstOrDefault(c => c.LastName == "London");
            if (item == null)
            {
                _JackLondonId = _context.Authors.Add(
                    new AuthorItem { FirstName = "Jack", LastName = "London" }).Entity.Id;

                _context.SaveChanges();
            }
            else
            {
                _JackLondonId = item.Id;
            }

            // Create a test subject
            _controller = new AuthorController();
            _controller.AuthorItems = new AuthorSqlRepository(_context);
        }

        [Fact]
        public void GetAll_ShouldReturnAtLeastOneItem()
        {
            var result = _controller.GetAll() as List<AuthorItem>;

            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetById_ShouldReturnItem()
        {
            var result = _controller.GetById(_JackLondonId) as ObjectResult;

            Assert.IsType(typeof(AuthorItem), result.Value); // IsType with two params
            AuthorItem author = (AuthorItem)result.Value;
            Assert.NotNull(author);
            Assert.Equal("Jack", author.FirstName);
            Assert.Equal("London", author.LastName);

            //Assert.Equal(testProducts.First(f => f.Id == 1).FirstName, author.FirstName);
        }

        [Fact]
        public void GetById_ShouldNotReturnItem_InvalidId()
        {
            var result = _controller.GetById(99999); // define an unreal number

            Assert.IsType<HttpNotFoundResult>(result); // IsType with generic
        }

        [Fact]
        public void Create_ShouldInsertItem()
        {
            string insertedFirstName = "TestedInsertFirstName";
            string insertedLastName = "TestedInsertLastName";

            var insertedAuthor = new AuthorItem
            {
                FirstName = insertedFirstName,
                LastName = insertedLastName
            };
            var result = (AuthorItem)(_controller.Create(insertedAuthor) as ObjectResult).Value;

            Assert.Equal(insertedFirstName, result.FirstName);
            Assert.Equal(insertedLastName, result.LastName);
        }

        [Fact]
        public void Update_ShouldUpdateItem()
        {
            string updatedFirstName = "Джек";
            string updatedLastName = "Лондон";

            var updatedAuthor = new AuthorItem
            {
                Id = _JackLondonId,
                FirstName = updatedFirstName,
                LastName = updatedLastName
            };
            var result = _controller.Update(_JackLondonId, updatedAuthor);

            Assert.IsType<NoContentResult>(result);

            var author = _context.Authors.FirstOrDefault(c => c.Id == _JackLondonId);
            Assert.Equal(updatedFirstName, author.FirstName);
            Assert.Equal(updatedLastName, author.LastName);
        }

        [Fact]
        public void Update_ShouldNotUpdateItem_InvalidId()
        {
            string updatedFirstName = "Edgar";
            string updatedLastName = "Poe";

            var updatedAuthor = new AuthorItem
            {
                Id = 99999,
                FirstName = updatedFirstName,
                LastName = updatedLastName
            };
            var result = _controller.Update(99999, updatedAuthor);  // define an unreal number

            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void Delete_ShouldRemoveItem()
        {
            var resultBefore = _context.Authors.FirstOrDefault(c => c.Id == _JackLondonId);
            Assert.NotNull(resultBefore);

            _controller.Delete(_JackLondonId);

            var resultAfter = _context.Authors.FirstOrDefault(c => c.Id == _JackLondonId);
            Assert.Null(resultAfter);
        }
    }
}
