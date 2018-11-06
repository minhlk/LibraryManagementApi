using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class Book
    {
        public Book()
        {
            BookGenre = new HashSet<BookGenre>();
            UserBook = new HashSet<UserBook>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long IdAuthor { get; set; }
        public int Amount { get; set; }
        public DateTime CreateTime { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public Author IdAuthorNavigation { get; set; }
        public ICollection<BookGenre> BookGenre { get; set; }
        public ICollection<UserBook> UserBook { get; set; }
    }
}
