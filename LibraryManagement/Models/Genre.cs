using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class Genre
    {
        public Genre()
        {
            BookGenre = new HashSet<BookGenre>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookGenre> BookGenre { get; set; }
    }
}
