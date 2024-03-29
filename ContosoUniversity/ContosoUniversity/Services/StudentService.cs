using System.Collections.Generic;
using ContosoUniversity.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ContosoUniversity.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> students;

        public StudentService(IContosoDBSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString("ContosoDB"));
            IMongoDatabase database = client.GetDatabase("ContosoDB");
            students = database.GetCollection<Student>("Students");
        }
        
        
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _books.DeleteOne(book => book.Id == id);
    }
    }
}