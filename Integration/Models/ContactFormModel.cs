using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Models
{
    public class ContactFormModel
    {
        public string FullName { get; set; }
        public EmailDetail Email { get; set; }
        public string Message { get; set; }
    }
    public class EmailDetail
    {
        public string Text { get; set; }
        public string  Type { get; set; }
    }
}
