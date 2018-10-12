using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class User
    {
        public User()
        {
            UserBook = new HashSet<UserBook>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IdRole { get; set; }
        public string YearOfBirth { get; set; }
        public string Phone { get; set; }

        public ICollection<UserBook> UserBook { get; set; }
    }
}
