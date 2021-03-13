using System;
using SampleMongoCrud.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace SampleMongoCrud.Services
{
    public class BookService
    {
        private readonly string environmentConnectionString = "ConnectionString";
        protected readonly string environmentDatabaseName = "DatabaseName";
        protected readonly string environmentCollectionName = "CollectionName";
        private readonly IMongoCollection<Book> _books;

        public BookService()
        {
            string ConnectionString = Environment.GetEnvironmentVariable(environmentConnectionString);
            string DatabaseName = Environment.GetEnvironmentVariable(environmentDatabaseName);
            string CollectionName = Environment.GetEnvironmentVariable(environmentCollectionName);

            if (ConnectionString != null && DatabaseName != null && CollectionName != null)
            {
                var client = new MongoClient(ConnectionString);
                var database = client.GetDatabase(DatabaseName);

                _books = database.GetCollection<Book>(CollectionName);
            }
        }
        public virtual IEnumerable<Book> Get(int? skip) =>
            _books.Find(book => true).Skip(skip).Limit(10).ToEnumerable();

        public virtual Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public virtual Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public virtual void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public virtual void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}