using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class ResultModel<T> where T : class
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }
}
