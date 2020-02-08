using Domain.Data;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

//TODO make custom Exception for db exceptions

namespace Service
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Delete(int id);
    }
    public class BookService: IBookService
    {
        private BookcaseContext _context;

        public BookService(BookcaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books;
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        
    }
}
