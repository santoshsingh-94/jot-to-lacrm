using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Models
{
    public class ContactFormModel
    {
        public string FullName { get; set; }
        public Emails[] Email { get; set; }
        public Phones[] Phone { get; set; }
        public string Message { get; set; }
        public Websites[] Website { get; set; }
    }
    public class Emails
    {
        public string Text { get; set; }
        public string  Type { get; set; }        
    }
    public class Websites
    {
        public string  Text { get; set; }
    }
    public class Phones
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }
}
