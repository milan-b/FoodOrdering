using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SavedBook
    {
        public int SavedBookId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string SavedName { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
