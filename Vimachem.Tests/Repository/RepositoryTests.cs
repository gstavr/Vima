using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vimachem.Models.Domain;
using Vimachem.Repository;
using FluentAssertions;
using Vimachem.Tests.Data;

namespace Vimachem.Tests.Repository
{   
    public class RepositoryTests
    {
        private readonly TestDbContext _context;
        private readonly Repository<Book> _repository;

        //public RepositoryTests()
        //{
        //    var options = new DbContextOptionsBuilder<TestDbContext>()
        //        .UseInMemoryDatabase(databaseName: "TestDatabase")
        //        .Options;
        //    _context = new TestDbContext(options);
        //    _repository = new Repository<Book>(_context);
        //}

        //[Fact]
        //public async Task CreateAsync_AddsEntityToDatabase()
        //{
        //    var book = new Book {Id = 1, Name= "Test Book" };
        //    await _repository.CreateAsync(book);

        //    var storedBook = await _context.Books.FirstOrDefaultAsync(b => b.Name == "Test Book");
        //    Assert.NotNull(storedBook);
        //    Assert.Equal("Test Book", storedBook.Name);
        //}


        //[Fact]
        //public async Task CreateAsync_EntityIsAdded()
        //{

        //    // Arrange
        //    var repository = new Repository<Book>(_dbContext);
        //    var book = new Book (){ Id = 1, Name = "Book One", CategoryId = 1 };

        //    // Act
        //    await repository.CreateAsync(book);

        //    // Assert
        //    _dbContext.Books.Should().ContainSingle(); // FluentAssertions
        //    _dbContext.Books.Single().Title.Should().Be("Book One");
        //}


        //[Fact]
        //public async Task DeleteAsync_EntityIsRemoved()
        //{
        //    // Arrange
        //    var repository = new Repository<Book>(_dbContext);
        //    var book = new Book () { Id = 1, Name = "Book One", CategoryId = 1 };
        //    _dbContext.Books.Add(book);
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    await repository.DeleteAsync(book);

        //    // Assert
        //    _dbContext.Books.Should().BeEmpty(); // FluentAssertions
        //}


        //[Fact]
        //public async Task GetAsync_WithNoFilter_ReturnsFirstEntity()
        //{
        //    // Arrange
        //    var repository = new Repository<Book>(_dbContext);
        //    _dbContext.Books.Add(new Book { Id = 1, Name = "First Book", Description = "Author Name" });
        //    _dbContext.Books.Add(new Book { Id = 2, Name = "Second Book", Description = "Another Author" });
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var book = await repository.GetAsync();

        //    // Assert
        //    book.Title.Should().Be("First Book");
        //}

        //[Fact]
        //public async Task GetAsync_WithFilter_ReturnsCorrectEntity()
        //{
        //    // Arrange
        //    var repository = new Repository<Book>(_dbContext);
        //    _dbContext.Books.Add(new Book { Id = 1, Name = "First Book", Description = "Author Name" });
        //    _dbContext.Books.Add(new Book { Id = 2, Name = "Second Book", Description = "Another Author" });
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var book = await repository.GetAsync(b => b.Description == "Second Book");

        //    // Assert
        //    book.Title.Should().Be("Second Book");
        //}


        //[Fact]
        //public async Task GetAllAsync_WithNoFilter_ReturnsAllEntities()
        //{
        //    // Arrange
        //    var repository = new Repository<Book>(_dbContext);
        //    _dbContext.Books.Add(new Book {Id = 1, Name = "First Book", Description = "Author Name" });
        //    _dbContext.Books.Add(new Book {Id = 2, Name = "Second Book", Description = "Another Author" });
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var books = await repository.GetAllAsync();

        //    // Assert
        //    books.Count.Should().Be(2);
        //}

        //[Fact]
        //public async Task GetAllAsync_WithFilter_ReturnsFilteredEntities()
        //{
        //    // Arrange
        //    var repository = new Repository<Book>(_dbContext);
        //    _dbContext.Books.Add(new Book {Id = 1, Name = "First Book", Description = "Author Name" });
        //    _dbContext.Books.Add(new Book {Id = 2, Name = "Second Book", Description = "Another Author" });
        //    await _dbContext.SaveChangesAsync();

        //    // Act
        //    var books = await repository.GetAllAsync(b => b.Description == "Another Author");

        //    // Assert
        //    books.Count.Should().Be(1);
        //    books.First().Name.Should().Be("Second Book");
        //}



    }

}
