using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public byte Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> User { get; set; }
    }
}
