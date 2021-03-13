using SampleMongoCrud.Models;
using System.Collections.Generic;

namespace SampleMongoCrud.Services
{
    public class BookServiceMock : BookService
    {
        private readonly IEnumerable<Book> allData;
        public BookServiceMock()
        {
            // allData = JsonFile
            //     .Read<IEnumerable<Book>>(
            //         @"Services\MockData.json");
        }

        public override IEnumerable<Book> Get(int? skip) =>
           new List<Book>();

        public override Book Get(string id) =>
            new Book();

        public override Book Create(Book book)
        {
            return new Book();
        }

        public override void Update(string id, Book bookIn) =>
            new Book();

        public override void Remove(string id) =>
            new Book();
    }
}