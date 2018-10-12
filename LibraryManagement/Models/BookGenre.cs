using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class BookGenre
    {
        public long Id { get; set; }
        public long IdGenre { get; set; }
        public long IdBook { get; set; }

        public Book IdBookNavigation { get; set; }
        public Genre IdGenreNavigation { get; set; }
    }
}
