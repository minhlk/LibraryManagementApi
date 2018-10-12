using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class Author
    {
        public Author()
        {
            Book = new HashSet<Book>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string YearOfBirth { get; set; }

        public ICollection<Book> Book { get; set; }
    }
}
