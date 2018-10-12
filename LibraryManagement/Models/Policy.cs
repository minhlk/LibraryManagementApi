using System;
using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public partial class Policy
    {
        public long Id { get; set; }
        public string Keyword { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
    }
}
