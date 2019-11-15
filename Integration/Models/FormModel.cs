using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.Models
{
    public class FormModel
    {
        public string FormId { get; set; }
        public string SubmissionId { get; set; }
        public string WebhookURL { get; set; }
        public string IP { get; set; }
        public string FormTitle { get; set; }
        public string Pretty { get; set; }
        public string Username { get; set; }
        public string RawRequest { get; set; }
        public string  Type { get; set; }
    }
}
