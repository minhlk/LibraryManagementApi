using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class UserBook
    {
        public long Id { get; set; }
        public long IdUser { get; set; }
        public long IdBook { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public byte NumberOfDays { get; set; }

        public Book IdBookNavigation { get; set; }
        public User IdUserNavigation { get; set; }
    }
}
