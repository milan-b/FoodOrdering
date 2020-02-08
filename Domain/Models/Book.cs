using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }

        public ICollection<SavedBook> BookUsers { get; set; }
    }
}
